using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Capa_de_acceso_de_datos
{
    public class AccesoRemoto
    {
        // 1. URL base de ngrok (Sin la diagonal al final para evitar el error 404)
        private static readonly string urlNgrok = "https://rozella-exanthematic-jeffrey.ngrok-free.dev";

        /// <summary>
        /// Envía una petición a la Web API para ejecutar un Stored Procedure de forma remota.
        /// </summary>
        public static async Task<bool> EjecutarSpRemoto(string nombreSp, object parametros)
        {
            // Construcción limpia del endpoint
            string endpoint = $"{urlNgrok}/api/Data/ejecutar-sp";

            // Objeto anónimo con la estructura exacta que espera tu SpRequest en la API
            var body = new
            {
                spName = nombreSp,
                parametros = parametros
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // IMPORTANTE: Evita la página de advertencia de ngrok que bloquea la petición
                    client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");

                    // Serializamos el objeto a JSON
                    string json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Enviamos la petición POST
                    HttpResponseMessage response = await client.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Si quieres ver el éxito en consola mientras programas:
                        Console.WriteLine($"Sincronización exitosa: {nombreSp}");
                        return true;
                    }
                    else
                    {
                        // Si la API responde con error (400, 500, etc.)
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        // Solo mostramos mensaje si es crítico, para no interrumpir al usuario
                        Console.WriteLine($"Error API ({response.StatusCode}): {errorMsg}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Error de red (ngrok apagado, sin internet, etc.)
                Console.WriteLine($"Fallo de red al sincronizar: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Método rápido para verificar si el servidor central está disponible.
        /// </summary>
        public static async Task<bool> VerificarConexion()
        {
            string endpoint = $"{urlNgrok}/api/Data/ejecutar-sp";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5); // Tiempo de espera corto
                    client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");

                    var body = new { spName = "PING", parametros = new { } };
                    string json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(endpoint, content);

                    // Si responde cualquier cosa, el túnel está vivo
                    return response.IsSuccessStatusCode || (int)response.StatusCode < 500;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}