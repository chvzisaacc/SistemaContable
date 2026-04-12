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
        private readonly string conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=""BASE DE SISTEMA - LOCAL"";Integrated Security=True;TrustServerCertificate=True;";

        [HttpPost("ejecutar-sp")]
        public IActionResult EjecutarSP([FromBody] SpRequest request)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(request.SpName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Mapeo dinámico de parámetros
                    if (request.Parametros != null)
                    {
                        foreach (var param in request.Parametros)
                        {
                            if (param.Key.StartsWith("_")) continue;

                            object valorFinal = param.Value;
                            if (valorFinal is JsonElement elemento)
                            {
                                valorFinal = elemento.ValueKind switch
                                {
                                    JsonValueKind.String => elemento.TryGetDateTime(out DateTime dt) ? dt : elemento.GetString(),
                                    JsonValueKind.Number => elemento.TryGetInt32(out int intVal) ? intVal : elemento.GetDouble(),
                                    JsonValueKind.True => true,
                                    JsonValueKind.False => false,
                                    JsonValueKind.Null => DBNull.Value,
                                    _ => elemento.GetRawText()
                                };
                            }

                            string nombreParam = param.Key.StartsWith("@") ? param.Key : "@" + param.Key;
                            cmd.Parameters.AddWithValue(nombreParam, valorFinal ?? DBNull.Value);
                        }
                    }

                    conn.Open();

                    // Se utiliza ExecuteNonQuery para acciones como Login o Ingresos
                    cmd.ExecuteNonQuery();

                    return Ok(new
                    {
                        mensaje = "Operación exitosa",
                        procedimiento = request.SpName,
                        timestamp = DateTime.Now
                    });
                }
            }
            catch (SqlException sqlEx)
            {
                // Captura errores específicos de SQL (permisos, nombres de SP mal escritos, etc.)
                return StatusCode(500, new { error = "Error en Base de Datos", detalle = sqlEx.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    public class SpRequest
    {
        public string SpName { get; set; }
        public Dictionary<string, object> Parametros { get; set; }
        public int? _ParroquiaId { get; set; }
    }
}