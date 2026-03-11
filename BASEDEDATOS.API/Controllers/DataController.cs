using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace BASEDEDATOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        // Configuración de la conexión
        static string servidorLocal = Environment.MachineName + "\\SQLEXPRESS";
        private readonly string conexion = $"Data Source={servidorLocal};Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";

        [HttpPost("ejecutar-sp")]
        public IActionResult EjecutarSP([FromBody] SpRequest request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(request.SpName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros dinámicamente con conversión de tipos
                    if (request.Parametros != null)
                    {
                        foreach (var param in request.Parametros)
                        {
                            object valorFinal = param.Value;

                            // Lógica para desempaquetar JsonElement y convertirlo a tipo nativo de C#
                            if (valorFinal is JsonElement elemento)
                            {
                                valorFinal = elemento.ValueKind switch
                                {
                                    JsonValueKind.String => elemento.GetString(),
                                    JsonValueKind.Number => elemento.TryGetInt32(out int intVal) ? intVal : elemento.GetDouble(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => elemento.GetRawText()
                                };
                            }

                            cmd.Parameters.AddWithValue("@" + param.Key.Replace("@", ""), valorFinal ?? DBNull.Value);
                        }
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return Ok(new { mensaje = "Operación exitosa en " + request.SpName });
                }
            }
            catch (Exception ex)
            {
                // Esto devolverá el error detallado si algo falla en SQL
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    public class SpRequest
    {
        public string SpName { get; set; }
        public Dictionary<string, object> Parametros { get; set; }
    }
}