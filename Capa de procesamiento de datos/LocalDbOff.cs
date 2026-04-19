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
                var url = $"{_urlBase.TrimEnd('/')}/api/Data/health";
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Add("ngrok-skip-browser-warning", "true");

                var resp = await _http.SendAsync(req);

                // ← CLAVE: verificar que la respuesta sea exitosa, no solo que llegue algo
                return resp.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // ── ENVÍO AL SERVIDOR ─────────────────────────────────────────────────
        // Llamado tanto por SincronizarConNube (Clsconexion) como por ProcesarColaSincronizacion

        /// <summary>Envía una operación al servidor remoto y retorna si fue aceptada.</summary>
        public async Task<(bool exitoso, bool esErrorNegocio)> EnviarAlServidorAsync(string spName, string json)
        {
            try
            {
                string jsonFinal = json;
                try
                {
                    using var doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    var parametros = new Dictionary<string, object>();
                    if (root.TryGetProperty("Parametros", out var parametrosEl))
                    {
                        foreach (var prop in parametrosEl.EnumerateObject())
                            parametros[prop.Name] = prop.Value.Clone();
                    }

                    int? parroquiaId = null;
                    if (root.TryGetProperty("_ParroquiaId", out var pidEl) &&
                        pidEl.ValueKind == JsonValueKind.Number)
                        parroquiaId = pidEl.GetInt32();

                    var spRequest = new
                    {
                        SpName = spName,
                        Parametros = parametros,
                        _ParroquiaId = parroquiaId
                    };

                    jsonFinal = JsonSerializer.Serialize(spRequest);
                }
                catch { }

                var url = $"{_urlBase.TrimEnd('/')}/api/Data/ejecutar-sp";
                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(jsonFinal, Encoding.UTF8, "application/json")
                };
                req.Headers.Add("ngrok-skip-browser-warning", "true");

                var resp = await _http.SendAsync(req);

                if (resp.IsSuccessStatusCode)
                    return (true, false);

                string body = await resp.Content.ReadAsStringAsync();
                File.AppendAllText("sync_errors.log",
                    $"{DateTime.Now} - Servidor rechazó [{resp.StatusCode}]: {body}\n");

                // 4xx o 5xx = error de negocio/datos, no reintentar
                bool esErrorNegocio = (int)resp.StatusCode >= 400;
                return (false, esErrorNegocio);
            }
            catch (Exception ex)
            {
                // Excepción = error de red, sí reintentar
                File.AppendAllText("sync_errors.log",
                    $"{DateTime.Now} - HTTP Error: {ex.Message}\n");
                return (false, false);
            }
        }

        // ── PROCESAMIENTO DE COLA ─────────────────────────────────────────────
        // Un timer en Program.cs invoca este método cada 30 segundos

        /// <summary>Intenta sincronizar todos los registros pendientes con el servidor remoto.</summary>
        public async Task ProcesarColaSincronizacion()
        {
            bool disponible = await ServidorDisponibleAsync();
            if (!disponible) return;

            var pendientes = ObtenerPendientes();
            if (pendientes.Count == 0) return;

            foreach (var registro in pendientes)
            {
                try
                {
                    var (ok, esErrorNegocio) = await EnviarAlServidorAsync(registro.TipoObjeto, registro.DatosJson);

                    if (ok)
                    {
                        MarcarComoSincronizado(registro.Id);
                    }
                    else if (esErrorNegocio)
                    {
                        // El servidor rechazó los datos — marcar como sincronizado para no reintentar
                        MarcarComoSincronizado(registro.Id);
                        Console.WriteLine($"[Sync] ✗ Id={registro.Id} descartado (error de negocio).");
                    }
                    // Si fue error de red, se deja pendiente para el próximo ciclo
                }
                catch (Exception ex)
                {
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


        public async Task PullDesdeServidorAsync(int miParroquiaId)
        {
            if (!await ServidorDisponibleAsync()) return;

            try
            {
                var url = $"{_urlBase.TrimEnd('/')}/api/Data/cambios-pendientes/{miParroquiaId}";
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Add("ngrok-skip-browser-warning", "true");

                var resp = await _http.SendAsync(req);
                if (!resp.IsSuccessStatusCode) return;

                string json = await resp.Content.ReadAsStringAsync();
                var cambios = JsonSerializer.Deserialize<List<CambioRemoto>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (cambios == null || cambios.Count == 0) return;

                var cambiosAjenos = cambios.Where(c => c.OrigenParroquiaId != miParroquiaId).ToList();

                if (cambiosAjenos.Count == 0) return;

                foreach (var cambio in cambiosAjenos)
                {
                    bool aplicado = await AplicarCambioLocalAsync(cambio);
                    if (aplicado)
                        await MarcarEntregadoEnServidorAsync(cambio.Id, miParroquiaId);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("sync_errors.log", $"{DateTime.Now} - Pull error: {ex.Message}\n");
            }
        }

        private async Task<bool> AplicarCambioLocalAsync(CambioRemoto cambio)
        {
            try
            {
                var url = "http://localhost:5145/api/Data/aplicar-cambio-recibido";
                var body = JsonSerializer.Serialize(new
                {
                    Hash = cambio.Hash,
                    SpName = cambio.SpName,
                    ParametrosJson = cambio.ParametrosJson,
                    OrigenParroquiaId = cambio.OrigenParroquiaId
                });

                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json")
                };

                var resp = await _http.SendAsync(req);
                var responseBody = await resp.Content.ReadAsStringAsync();

                
                if (responseBody.Contains("DUPLICADO"))
                {
                    Console.WriteLine($"[PULL] Cambio {cambio.Id} ya estaba aplicado (duplicado)");
                    return true; // Considerar como exitoso para marcarlo como entregado
                }

                return resp.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PULL] Error aplicando cambio: {ex.Message}");
                return false;
            }
        }

        private async Task MarcarEntregadoEnServidorAsync(int cambioId, int parroquiaId)
        {
            try
            {
                var url = $"{_urlBase.TrimEnd('/')}/api/Data/marcar-entregado";
                var body = JsonSerializer.Serialize(new { CambioId = cambioId, DestinoParroquiaId = parroquiaId });
                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json")
                };
                req.Headers.Add("ngrok-skip-browser-warning", "true");
                await _http.SendAsync(req);
            }
            catch { }
        }

        public class CambioRemoto
        {
            public int Id { get; set; }
            public string SpName { get; set; }
            public string ParametrosJson { get; set; }
            public string Hash { get; set; }
            public int OrigenParroquiaId { get; set; }
        }
    }
}