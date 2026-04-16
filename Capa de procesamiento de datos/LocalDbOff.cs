using Capa_de_acceso_de_datos;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Capa_de_procesamiento_de_datos
{
    // Gestiona la sincronización offline: encola operaciones en la BD local
    // y las reenvía al servidor remoto vía ngrok cuando hay conexión disponible
    public class LocalDbOff
    {
        // Cliente HTTP compartido entre todas las instancias; timeout corto para no bloquear la UI
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(6)
        };

        // URL del túnel ngrok; se carga desde archivo al iniciar la aplicación
        private static string _urlBase = CargarUrlNgrok();

        // ── COLA LOCAL (Entity Framework) ─────────────────────────────────────

        /// <summary>Agrega una operación pendiente a la cola de sincronización local.</summary>
        public void RegistrarProcesoLocal(string tipo, string json)
        {
            try
            {
                using (var db = new LocalDbContext())
                {
                    var nuevo = new ProcesosLocales
                    {
                        TipoObjeto = tipo,
                        DatosJson = json,
                        Sincronizado = false,
                        Fecha = DateTime.Now
                    };
                    db.ColaSincronizacion.Add(nuevo);
                    db.SaveChanges();
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine($"[LocalDB] Error guardando en cola: {ex.InnerException?.Message}");
                Console.WriteLine($"[LocalDB] SP: {tipo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LocalDB] Error inesperado: {ex.Message}");
            }
        }

        /// <summary>Retorna todos los registros que aún no han sido sincronizados con el servidor.</summary>
        public List<ProcesosLocales> ObtenerPendientes()
        {
            using (var db = new LocalDbContext())
            {
                return db.ColaSincronizacion
                         .Where(p => !p.Sincronizado)
                         .ToList();
            }
        }

        /// <summary>Marca un registro como sincronizado para que no vuelva a procesarse.</summary>
        public void MarcarComoSincronizado(int id)
        {
            using (var db = new LocalDbContext())
            {
                var registro = db.ColaSincronizacion.Find(id);
                if (registro != null)
                {
                    registro.Sincronizado = true;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>Retorna la cantidad de registros pendientes de sincronización.</summary>
        public int ContarPendientes()
        {
            using (var db = new LocalDbContext())
            {
                return db.ColaSincronizacion.Count(p => !p.Sincronizado);
            }
        }

        // ── DETECCIÓN DEL SERVIDOR ────────────────────────────────────────────

        /// <summary>Verifica si el servidor remoto está accesible antes de intentar enviar datos.</summary>
        public static async Task<bool> ServidorDisponibleAsync()
        {
            if (string.IsNullOrWhiteSpace(_urlBase)) return false;

            try
            {
                var req = new HttpRequestMessage(HttpMethod.Get, _urlBase.TrimEnd('/'));
                // Header requerido por ngrok para evitar la página de advertencia del navegador
                req.Headers.Add("ngrok-skip-browser-warning", "true");
                await _http.SendAsync(req);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ── ENVÍO AL SERVIDOR ─────────────────────────────────────────────────
        // Llamado tanto por SincronizarConNube (Clsconexion) como por ProcesarColaSincronizacion

        /// <summary>Envía una operación al servidor remoto y retorna si fue aceptada.</summary>
        public async Task<bool> EnviarAlServidorAsync(string spName, string json)
        {
            try
            {
                string jsonLimpio = json;
                try
                {
                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;
                    var dict = new Dictionary<string, object>();

                    foreach (var prop in root.EnumerateObject())
                    {
                        // _ParroquiaId es metadata interna; no se envía al servidor
                        if (prop.Name == "_ParroquiaId") continue;
                        dict[prop.Name] = prop.Value.Clone();
                    }
                    jsonLimpio = JsonSerializer.Serialize(dict);
                }
                catch { } // Si el JSON no es parseable se envía tal como está

                var url = $"{_urlBase.TrimEnd('/')}/api/Data/ejecutar-sp";
                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonLimpio, Encoding.UTF8, "application/json")
                };
                req.Headers.Add("ngrok-skip-browser-warning", "true");

                var resp = await _http.SendAsync(req);

                if (!resp.IsSuccessStatusCode)
                {
                    string body = await resp.Content.ReadAsStringAsync();
                    // Los errores del servidor se registran en archivo para diagnóstico posterior
                    File.AppendAllText("sync_errors.log",
                        $"{DateTime.Now} - Servidor rechazó [{resp.StatusCode}]: {body}\n");
                }

                return resp.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                File.AppendAllText("sync_errors.log",
                    $"{DateTime.Now} - HTTP Error: {ex.Message}\n");
                return false;
            }
        }

        // ── PROCESAMIENTO DE COLA ─────────────────────────────────────────────
        // Un timer en Program.cs invoca este método cada 30 segundos

        /// <summary>Intenta sincronizar todos los registros pendientes con el servidor remoto.</summary>
        public async Task ProcesarColaSincronizacion()
        {
            bool disponible = await ServidorDisponibleAsync();
            if (!disponible)
            {
                Console.WriteLine("[Sync] Sin servidor. Reintentando en el próximo ciclo.");
                return;
            }

            var pendientes = ObtenerPendientes();
            if (pendientes.Count == 0) return;

            Console.WriteLine($"[Sync] {pendientes.Count} registros pendientes...");

            foreach (var registro in pendientes)
            {
                try
                {
                    bool ok = await EnviarAlServidorAsync(registro.TipoObjeto, registro.DatosJson);

                    if (ok)
                    {
                        MarcarComoSincronizado(registro.Id);
                        Console.WriteLine($"[Sync] ✓ Id={registro.Id} sincronizado.");
                    }
                    else
                    {
                        Console.WriteLine($"[Sync] ✗ Id={registro.Id} rechazado por servidor.");
                    }
                }
                catch (Exception ex)
                {
                    // Se interrumpe el ciclo para no marcar registros posteriores como procesados
                    Console.WriteLine($"[Sync] Error en Id={registro.Id}: {ex.Message}");
                    break;
                }
            }
        }

        // ── GESTIÓN DE LA URL NGROK ───────────────────────────────────────────

        // Lee la URL del túnel desde ngrok_url.txt ubicado junto al ejecutable
        private static string CargarUrlNgrok()
        {
            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "ngrok_url.txt");

                if (File.Exists(ruta))
                {
                    string url = File.ReadAllText(ruta).Trim();
                    if (!string.IsNullOrEmpty(url))
                        return url;
                }

                // Si no existe o está vacío, crear el archivo y avisar
                File.WriteAllText(ruta, "");
                MessageBox.Show(
                    "No se encontró la URL del servidor",
                    "Configuración requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch { }
            return string.Empty;
        }

        /// <summary>Actualiza la URL del túnel en memoria y la persiste en ngrok_url.txt.</summary>
        public static void ActualizarUrlNgrok(string nuevaUrl)
        {
            _urlBase = nuevaUrl.Trim();
            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "ngrok_url.txt");
                File.WriteAllText(ruta, _urlBase);
            }
            catch { }
        }

        /// <summary>Retorna la URL del túnel ngrok actualmente configurada.</summary>
        public static string ObtenerUrlActual() => _urlBase;
    }
}