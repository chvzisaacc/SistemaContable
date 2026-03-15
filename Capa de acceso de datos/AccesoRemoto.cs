using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capa_de_acceso_de_datos
{
    public class AccesoRemoto
    {
        // Instancia única de HttpClient para mejorar el rendimiento
        private static readonly HttpClient _httpClient = new HttpClient();

        // 1. URL base de ngrok (Actualízala cuando reinicies el túnel)
        private static readonly string urlNgrok = "https://rozella-exanthematic-jeffrey.ngrok-free.dev";

        // Constructor estático correcto: el modificador 'static' debe ir antes del tipo y nombre del miembro
        static AccesoRemoto()
        {
            // Configuración global para evitar la página de advertencia de ngrok
            if (!_httpClient.DefaultRequestHeaders.Contains("ngrok-skip-browser-warning"))
            {
                _httpClient.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
            }
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }

        /// <summary>
        /// Envía una petición a la Web API para ejecutar un Stored Procedure de forma remota.
        /// </summary>
        public static async Task<bool> EjecutarSpRemoto(string nombreSp, Dictionary<string, object> parametros)
        {
            string endpoint = $"{urlNgrok}/api/Data/ejecutar-sp";

            // Los nombres de las propiedades deben coincidir con SpRequest.cs de tu API (PascalCase)
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
        /// Verifica si el túnel de ngrok y la API están respondiendo.
        /// </summary>
        public static async Task<bool> VerificarConexion()
        {
            string endpoint = $"{urlNgrok}/api/Data/ejecutar-sp";

            try
            {
                // Enviamos una petición vacía para probar el túnel
                var testBody = new { SpName = "PING", Parametros = new Dictionary<string, object>() };
                string json = JsonConvert.SerializeObject(testBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);

                // Si responde 200 (OK) o 400 (BadRequest), significa que la API está viva
                return (int)response.StatusCode < 500;
            }
            catch
            {
                return false;
            }
        }
    }
}