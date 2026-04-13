using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Clase encargada de gestionar la comunicación remota con la Web API a través del túnel ngrok.
    /// Proporciona métodos para ejecutar procedimientos almacenados de forma asincrónica y verificar la conectividad.
    /// </summary>
    public class AccesoRemoto
    {
        /// <summary>
        /// Instancia única de <see cref="HttpClient"/> reutilizada para todas las solicitudes HTTP.
        /// Se utiliza una instancia estática para mejorar el rendimiento y evitar el agotamiento de puertos.
        /// </summary>
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// URL base del túnel ngrok que actúa como intermediario hacia la API remota.
        /// Nota: Esta URL cambia cada vez que se reinicia el túnel. Debe actualizarse manualmente.
        /// Formato: https://[random-id].ngrok-free.dev
        /// </summary>
        private static readonly string urlNgrok = "https://rozella-exanthematic-jeffrey.ngrok-free.dev";

        /// <summary>
        /// Constructor estático que inicializa la configuración global de <see cref="HttpClient"/>.
        /// Ejecuta una única vez al cargar la clase y establece parámetros de conexión.
        /// </summary>
        static AccesoRemoto()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("ngrok-skip-browser-warning"))
            {
                _httpClient.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
            }
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado de forma remota en la Web API.
        /// Serializa los parámetros a JSON, envía la solicitud POST al servidor remoto y retorna el estado.
        /// Este método es el puente para sincronizar operaciones locales con la base de datos remota.
        /// </summary>
        /// <param name="nombreSp">Nombre del procedimiento almacenado a ejecutar en SQL Server remoto.</param>
        /// <param name="parametros">Diccionario con parámetros que deben coincidir con los del procedimiento almacenado.</param>
        /// <returns><c>true</c> si la ejecución fue exitosa (status 2xx), <c>false</c> en caso de error o problemas de conectividad.</returns>
        public static async Task<bool> EjecutarSpRemoto(string nombreSp, Dictionary<string, object> parametros)
        {
            string endpoint = $"{urlNgrok}/api/Data/ejecutar-sp";

            var body = new
            {
                SpName = nombreSp,
                Parametros = parametros
            };

            try
            {
                string json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[Sync OK] SP: {nombreSp}");
                    return true;
                }
                else
                {
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[API Error {response.StatusCode}] {errorMsg}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Network Fail] Error al conectar con ngrok: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verifica la disponibilidad del servidor remoto realizando una solicitud de prueba (PING).
        /// Valida que la API remota esté respondiendo correctamente antes de operaciones críticas.
        /// </summary>
        /// <returns><c>true</c> si el servidor responde (status &lt; 500), <c>false</c> si está offline o hay error de conectividad.</returns>
        public static async Task<bool> VerificarConexion()
        {
            string endpoint = $"{urlNgrok}/api/Data/ejecutar-sp";

            try
            {
                var testBody = new { SpName = "PING", Parametros = new Dictionary<string, object>() };
                string json = JsonConvert.SerializeObject(testBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);

                return (int)response.StatusCode < 500;
            }
            catch
            {
                return false;
            }
        }
    }
}