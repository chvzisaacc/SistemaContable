using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Capa_de_procesamiento_de_datos
{
    public class LocalDbOff
    {
        // ── Cliente HTTP estático ──────────────────────────────────────────────
        private static readonly HttpClient _http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(6)
        };

        // ── URL del servidor ngrok ─────────────────────────────────────────────
        private static string _urlBase = CargarUrlNgrok();

        //  MÉTODOS DE COLA LOCAL (Entity Framework)

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
                // Ver exactamente qué falló
                Console.WriteLine($"[LocalDB] Error guardando en cola: {ex.InnerException?.Message}");
                Console.WriteLine($"[LocalDB] SP: {tipo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LocalDB] Error inesperado: {ex.Message}");
            }
        }

        public List<ProcesosLocales> ObtenerPendientes()
        {
            using (var db = new LocalDbContext())
            {
                return db.ColaSincronizacion
                         .Where(p => !p.Sincronizado)
                         .ToList();
            }
        }

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

        public int ContarPendientes()
        {
            using (var db = new LocalDbContext())
            {
                return db.ColaSincronizacion.Count(p => !p.Sincronizado);
            }
        }

        // =====================================================================
        //  DETECCIÓN DEL TÚNEL NGROK
        // =====================================================================

        public static async Task<bool> ServidorDisponibleAsync()
        {
            if (string.IsNullOrWhiteSpace(_urlBase)) return false;

            try
            {
                var req = new HttpRequestMessage(
                    HttpMethod.Get, _urlBase.TrimEnd('/'));
                req.Headers.Add("ngrok-skip-browser-warning", "true");

                await _http.SendAsync(req);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // =====================================================================
        //  ENVÍO AL SERVIDOR — método público, acepta spName + json directo
        //  Lo llaman: SincronizarConNube (Clsconexion) y ProcesarColaSincronizacion
        // =====================================================================

        public async Task<bool> EnviarAlServidorAsync(string spName, string json)
        {
            try
            {
                var url = $"{_urlBase.TrimEnd('/')}/api/Data/ejecutar-sp";
                var req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                req.Headers.Add("ngrok-skip-browser-warning", "true");

                var resp = await _http.SendAsync(req);

                // ✅ Log para saber qué responde el servidor
                if (!resp.IsSuccessStatusCode)
                {
                    string body = await resp.Content.ReadAsStringAsync();
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

        // =====================================================================
        //  PROCESAMIENTO DE COLA — el timer de Program.cs llama esto cada 30s
        // =====================================================================

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
                    // Reutiliza el mismo método público de envío
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
                    Console.WriteLine($"[Sync] Error en Id={registro.Id}: {ex.Message}");
                    break;
                }
            }
        }

        // =====================================================================
        //  GESTIÓN DEL ARCHIVO ngrok_url.txt
        // =====================================================================

        private static string CargarUrlNgrok()
        {
            try
            {
                string ruta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "ngrok_url.txt");

                if (File.Exists(ruta))
                    return File.ReadAllText(ruta).Trim();
            }
            catch { }
            return string.Empty;
        }

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

        public static string ObtenerUrlActual() => _urlBase;
    }
}