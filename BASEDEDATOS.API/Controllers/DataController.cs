using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BASEDEDATOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly string conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=""BASE DE SISTEMA - LOCAL"";Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Ejecuta un procedimiento almacenado en la base de datos con parámetros dinámicos.
        /// </summary>
        [HttpPost("ejecutar-sp")]
        public IActionResult EjecutarSP([FromBody] SpRequest request)
        {

            if (request._ParroquiaId == null || request._ParroquiaId == 0)
            {
                if (request.Parametros != null && request.Parametros.ContainsKey("_ParroquiaId"))
                {
                    var val = request.Parametros["_ParroquiaId"];
                    if (val is JsonElement el)
                    {
                        if (el.ValueKind == JsonValueKind.Number) request._ParroquiaId = el.GetInt32();
                        else if (int.TryParse(el.GetString(), out int idConv)) request._ParroquiaId = idConv;
                    }
                    else if (int.TryParse(val?.ToString(), out int idConv))
                    {
                        request._ParroquiaId = idConv;
                    }
                }
            }

            // ========== LOG 1: PETICIÓN RECIBIDA ==========
            Console.WriteLine("========================================");
            Console.WriteLine($"[API] EjecutarSP llamado a las {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"[API] SpName: {request.SpName}");
            Console.WriteLine($"[API] _ParroquiaId: {request._ParroquiaId}");
            Console.WriteLine($"[API] Parametros contiene 'Hash': {request.Parametros?.ContainsKey("Hash")}");

            if (request.Parametros != null && request.Parametros.ContainsKey("Hash"))
            {
                Console.WriteLine($"[API] Valor del Hash: {request.Parametros["Hash"]}");
            }

            // Verificar conexión a BD
            try
            {
                using (var testConn = new SqlConnection(conexion))
                {
                    testConn.Open();
                    Console.WriteLine("[API] Conexión a BD exitosa");
                    testConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] ERROR de conexión a BD: {ex.Message}");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    Console.WriteLine("[API] Conexión abierta correctamente");

                    SqlCommand cmd = new SqlCommand(request.SpName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Mapeo de parámetros
                    if (request.Parametros != null)
                    {
                        Console.WriteLine($"[API] Mapeando {request.Parametros.Count} parámetros...");
                        foreach (var param in request.Parametros)
                        {
                            // ✅ CORRECCIÓN: excluir Hash además de los parámetros internos con "_"
                            if (param.Key.StartsWith("_") || param.Key == "Hash")
                            {
                                Console.WriteLine($"[API] Parámetro ignorado: {param.Key}");
                                continue;
                            }

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
                            Console.WriteLine($"[API] Parámetro agregado: {nombreParam} = {valorFinal}");
                        }
                    }

                    // Ejecutar el SP principal
                    Console.WriteLine($"[API] Ejecutando SP principal: {request.SpName}");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"[API] SP principal ejecutado correctamente");

                    // Registrar el cambio si tiene Hash
                    if (request.Parametros != null && request.Parametros.ContainsKey("Hash"))
                    {
                        Console.WriteLine("[API] 🔴 Detectado Hash - Registrando cambio...");

                        using (SqlCommand cmdReg = new SqlCommand("sp_RegistrarCambio", conn))
                        {
                            cmdReg.CommandType = CommandType.StoredProcedure;
                            object hashRaw = request.Parametros["Hash"];
                            string hashValue = null;

                            if (hashRaw is JsonElement element)
                            {
                                hashValue = element.ValueKind == JsonValueKind.String ? element.GetString() : element.GetRawText();
                            }
                            else
                            {
                                hashValue = hashRaw?.ToString();
                            }

                            string parametrosJson = JsonSerializer.Serialize(request.Parametros);

                            cmdReg.Parameters.AddWithValue("@SpName", request.SpName);
                            cmdReg.Parameters.AddWithValue("@ParametrosJson", parametrosJson);
                            cmdReg.Parameters.AddWithValue("@Hash", hashValue ?? (object)DBNull.Value);
                            cmdReg.Parameters.AddWithValue("@OrigenParroquiaId", request._ParroquiaId ?? (object)DBNull.Value);

                            Console.WriteLine($"[API] Ejecutando sp_RegistrarCambio con:");
                            Console.WriteLine($"[API]   @SpName = {request.SpName}");
                            Console.WriteLine($"[API]   @ParametrosJson = {parametrosJson}");
                            Console.WriteLine($"[API]   @Hash = {hashValue}");
                            Console.WriteLine($"[API]   @OrigenParroquiaId = {request._ParroquiaId}");

                            int filasAfectadas = cmdReg.ExecuteNonQuery();
                            Console.WriteLine($"[API] 🔴 sp_RegistrarCambio ejecutado. Filas afectadas: {filasAfectadas}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[API] No se detectó Hash - No se registra cambio");
                    }

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
                Console.WriteLine($"[API] ❌ SQL Error: {sqlEx.Message}");
                Console.WriteLine($"[API] Procedimiento: {sqlEx.Procedure}");
                Console.WriteLine($"[API] Línea: {sqlEx.LineNumber}");
                return StatusCode(500, new { error = "Error en Base de Datos", detalle = sqlEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] ❌ Error general: {ex.Message}");
                Console.WriteLine($"[API] StackTrace: {ex.StackTrace}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("cambios-pendientes/{parroquiaId}")]
        public IActionResult ObtenerCambiosPendientes(int parroquiaId)
        {
            Console.WriteLine($"[API] ObtenerCambiosPendientes para parroquia: {parroquiaId}");

            try
            {
                using var conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand("sp_ObtenerCambiosPendientes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DestinoParroquiaId", parroquiaId);

                conn.Open();
                var adapter = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);

                var cambios = dt.AsEnumerable().Select(row => new
                {
                    Id = row.Field<int>("Id"),
                    SpName = row.Field<string>("SpName"),
                    ParametrosJson = row.Field<string>("ParametrosJson"),
                    Hash = row.Field<string>("Hash"),
                    OrigenParroquiaId = row.Field<int>("OrigenParroquiaId")
                }).ToList();

                Console.WriteLine($"[API] Se encontraron {cambios.Count} cambios pendientes para parroquia {parroquiaId}");
                return Ok(cambios);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[API] ❌ SQL Error en ObtenerCambiosPendientes: {sqlEx.Message}");
                return StatusCode(500, new { error = sqlEx.Message });
            }
        }

        [HttpPost("marcar-entregado")]
        public IActionResult MarcarEntregado([FromBody] EntregaRequest request)
        {
            Console.WriteLine($"[API] MarcarEntregado - CambioId: {request.CambioId}, DestinoParroquiaId: {request.DestinoParroquiaId}");

            try
            {
                using var conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand("sp_MarcarEntregado", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CambioId", request.CambioId);
                cmd.Parameters.AddWithValue("@DestinoParroquiaId", request.DestinoParroquiaId);

                conn.Open();
                int filas = cmd.ExecuteNonQuery();
                Console.WriteLine($"[API] MarcarEntregado ejecutado. Filas afectadas: {filas}");
                return Ok(new { mensaje = "Marcado como entregado" });
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[API] ❌ SQL Error en MarcarEntregado: {sqlEx.Message}");
                return StatusCode(500, new { error = sqlEx.Message });
            }
        }

        /// <summary>
        /// Aplica un cambio recibido desde el servidor central en la base de datos local.
        /// </summary>
        [HttpPost("aplicar-cambio-recibido")]
        public IActionResult AplicarCambioRecibido([FromBody] AplicarCambioRequest request)
        {
            Console.WriteLine($"[API] AplicarCambioRecibido - Hash: {request.Hash}, SpName: {request.SpName}, OrigenParroquiaId: {request.OrigenParroquiaId}");

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_AplicarCambioRecibido", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Hash", request.Hash);
                    cmd.Parameters.AddWithValue("@SpName", request.SpName);
                    cmd.Parameters.AddWithValue("@ParametrosJson", request.ParametrosJson);
                    cmd.Parameters.AddWithValue("@OrigenParroquiaId", request.OrigenParroquiaId);

                    object resultadoObj = cmd.ExecuteScalar();
                    string resultado = resultadoObj?.ToString() ?? "APLICADO";

                    Console.WriteLine($"[API] Resultado de sp_AplicarCambioRecibido: {resultado}");
                    return Ok(new { resultado, hash = request.Hash });
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[API] ❌ SQL Error en AplicarCambioRecibido: {sqlEx.Message}");
                return StatusCode(500, new { error = "Error en Base de Datos", detalle = sqlEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] ❌ Error general en AplicarCambioRecibido: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    /// <summary>
    /// Modelo de solicitud para ejecutar procedimientos almacenados de forma dinámica.
    /// </summary>
    public class SpRequest
    {
        public string SpName { get; set; }
        public Dictionary<string, object> Parametros { get; set; }

        /// <summary>
        /// Identificador interno de la parroquia asociada a la solicitud.
        /// Parámetro de contexto que inicia con "_" y es ignorado en el mapeo de parámetros SQL.
        /// </summary>
        [JsonPropertyName("_ParroquiaId")]
        public int? _ParroquiaId { get; set; }
    }

    public class EntregaRequest
    {
        public int CambioId { get; set; }
        public int DestinoParroquiaId { get; set; }
    }

    public class AplicarCambioRequest
    {
        public string Hash { get; set; }
        public string SpName { get; set; }
        public string ParametrosJson { get; set; }
        public int OrigenParroquiaId { get; set; }
    }
}