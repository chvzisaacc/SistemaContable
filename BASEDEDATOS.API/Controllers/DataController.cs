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

        /// <summary>
        /// Ejecuta un procedimiento almacenado en la base de datos con parámetros dinámicos.
        /// Realiza el mapeo automático de tipos JSON a tipos SQL, ejecuta la operación y retorna un mensaje de éxito.
        /// </summary>
        /// <param name="request">Objeto <see cref="SpRequest"/> que contiene el nombre del procedimiento y sus parámetros.</param>
        /// <returns>
        /// Retorna <see cref="IActionResult"/> con un mensaje de operación exitosa si se ejecuta correctamente.
        /// En caso de error en SQL retorna estado 500, en otros casos retorna BadRequest (400).
        /// </returns>
        [HttpPost("ejecutar-sp")]
        public IActionResult EjecutarSP([FromBody] SpRequest request)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(request.SpName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    /// <summary>
                    /// Mapeo dinámico de parámetros desde el JSON a tipos SQL nativos.
                    /// Itera sobre cada parámetro, realiza conversiones de tipos automáticas y los agrega al comando SQL.
                    /// Los parámetros que inician con "_" se ignoran (propiedades internas).
                    /// </summary>
                    if (request.Parametros != null)
                    {
                        foreach (var param in request.Parametros)
                        {
                            // Omite parámetros internos que inician con guión bajo
                            if (param.Key.StartsWith("_")) continue;

                            object valorFinal = param.Value;

                            /// <summary>
                            /// Conversión de tipos de JsonElement a tipos C# nativos.
                            /// Detecta automáticamente el tipo JSON y lo convierte al tipo SQL apropiado:
                            /// - String: intenta convertir a DateTime, si falla retorna String
                            /// - Number: intenta convertir a Int32, si falla retorna Double
                            /// - True/False: convierte a booleano
                            /// - Null: convierte a DBNull.Value
                            /// </summary>
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

                            /// <summary>
                            /// Normaliza el nombre del parámetro agregando el prefijo "@" si no lo posee.
                            /// Esto asegura compatibilidad con la sintaxis de SQL Server.
                            /// </summary>
                            string nombreParam = param.Key.StartsWith("@") ? param.Key : "@" + param.Key;
                            cmd.Parameters.AddWithValue(nombreParam, valorFinal ?? DBNull.Value);
                        }
                    }

                    conn.Open();

                    /// <summary>
                    /// Ejecuta el procedimiento almacenado sin esperar resultados (ExecuteNonQuery).
                    /// Se utiliza para operaciones DML como INSERT, UPDATE, DELETE o procedimientos sin retorno de datos.
                    /// Ejemplos: Login, Ingresos, actualizaciones de datos.
                    /// </summary>
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
                /// <summary>
                /// Captura errores específicos de la base de datos SQL Server.
                /// Incluye: errores de permisos, procedimientos inexistentes, violaciones de restricciones, etc.
                /// Retorna estado HTTP 500 (Internal Server Error) con detalles del error.
                /// </summary>
                return StatusCode(500, new { error = "Error en Base de Datos", detalle = sqlEx.Message });
            }
            catch (Exception ex)
            {
                /// <summary>
                /// Captura cualquier otra excepción no contemplada.
                /// Retorna estado HTTP 400 (Bad Request) con el mensaje del error.
                /// </summary>
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    /// <summary>
    /// Modelo de solicitud para ejecutar procedimientos almacenados de forma dinámica.
    /// Contiene el nombre del procedimiento y sus parámetros en formato diccionario.
    /// </summary>
    public class SpRequest
    {
        /// <summary>
        /// Nombre del procedimiento almacenado a ejecutar en SQL Server.
        /// Ejemplo: "sp_InsertarUsuario", "sp_ActualizarCuenta", etc.
        /// </summary>
        public string SpName { get; set; }

        /// <summary>
        /// Diccionario con los parámetros del procedimiento almacenado.
        /// Clave: nombre del parámetro (con o sin prefijo "@")
        /// Valor: valor del parámetro en formato JSON (string, number, boolean, null, etc.)
        /// Ejemplo: {"usuario": "admin", "contraseña": "123456", "activo": true}
        /// </summary>
        public Dictionary<string, object> Parametros { get; set; }

        /// <summary>
        /// Identificador interno de la parroquia asociada a la solicitud.
        /// Parámetro de contexto que inicia con "_" y es ignorado en el mapeo de parámetros SQL.
        /// Utilizado para rastrear desde qué parroquia se originó la solicitud.
        /// Valor: null o identificador numérico de la parroquia.
        /// </summary>
        public int? _ParroquiaId { get; set; }
    }
}