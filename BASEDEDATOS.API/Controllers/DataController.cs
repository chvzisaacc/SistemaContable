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
        private readonly string conexion;
        public DataController(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado en la base de datos con parametros dinamicos.
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

            Console.WriteLine("========================================");
            Console.WriteLine($"[API] EjecutarSP llamado a las {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"[API] SpName: {request.SpName}");
            Console.WriteLine($"[API] _ParroquiaId: {request._ParroquiaId}");
            Console.WriteLine($"[API] Parametros contiene 'Hash': {request.Parametros?.ContainsKey("Hash")}");

            if (request.Parametros != null && request.Parametros.ContainsKey("Hash"))
            {
                Console.WriteLine($"[API] Valor del Hash: {request.Parametros["Hash"]}");
            }

            try
            {
                using (var testConn = new SqlConnection(conexion))
                {
                    testConn.Open();
                    Console.WriteLine("[API] Conexion a BD exitosa");
                    testConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] ERROR de conexion a BD: {ex.Message}");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    conn.Open();
                    Console.WriteLine("[API] Conexion abierta correctamente");

                    SqlCommand cmd = new SqlCommand(request.SpName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (request.Parametros != null)
                    {
                        Console.WriteLine($"[API] Mapeando {request.Parametros.Count} parametros...");
                        foreach (var param in request.Parametros)
                        {
                            if (param.Key.StartsWith("_") || param.Key == "Hash")
                            {
                                Console.WriteLine($"[API] Parametro ignorado: {param.Key}");
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
                            Console.WriteLine($"[API] Parametro agregado: {nombreParam} = {valorFinal}");
                        }
                    }

                    Console.WriteLine($"[API] Ejecutando SP principal: {request.SpName}");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"[API] SP principal ejecutado correctamente");

                    if (request.Parametros != null && request.Parametros.ContainsKey("Hash"))
                    {
                        Console.WriteLine("[API] Detectado Hash - Registrando cambio...");

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
                            Console.WriteLine($"[API] sp_RegistrarCambio ejecutado. Filas afectadas: {filasAfectadas}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[API] No se detecto Hash - No se registra cambio");
                    }

                    return Ok(new
                    {
                        mensaje = "Operacion exitosa",
                        procedimiento = request.SpName,
                        timestamp = DateTime.Now
                    });
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[API] SQL Error: {sqlEx.Message}");
                Console.WriteLine($"[API] Procedimiento: {sqlEx.Procedure}");
                Console.WriteLine($"[API] Linea: {sqlEx.LineNumber}");
                return StatusCode(500, new { error = "Error en Base de Datos", detalle = sqlEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] Error general: {ex.Message}");
                Console.WriteLine($"[API] StackTrace: {ex.StackTrace}");
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la lista de cambios pendientes de sincronizar para una parroquia especifica.
        /// </summary>
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
                Console.WriteLine($"[API] SQL Error en ObtenerCambiosPendientes: {sqlEx.Message}");
                return StatusCode(500, new { error = sqlEx.Message });
            }
        }

        /// <summary>
        /// Obtiene TODOS los cambios pendientes sin filtrar por parroquia.
        /// </summary>
        [HttpGet("cambios-pendientes-todos")]
        public IActionResult ObtenerCambiosPendientesTodos()
        {
            Console.WriteLine($"[API] ObtenerCambiosPendientesTodos - Todos los cambios");

            try
            {
                using var conn = new SqlConnection(conexion);
                SqlCommand cmd = new SqlCommand("sp_ObtenerCambiosPendientesTodos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

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

                Console.WriteLine($"[API] Se encontraron {cambios.Count} cambios pendientes en total");
                return Ok(cambios);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[API] SQL Error en ObtenerCambiosPendientesTodos: {sqlEx.Message}");
                return StatusCode(500, new { error = sqlEx.Message });
            }
        }

        /// <summary>
        /// Marca un cambio especifico como entregado a una parroquia destino.
        /// </summary>
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
                Console.WriteLine($"[API] SQL Error en MarcarEntregado: {sqlEx.Message}");
                return StatusCode(500, new { error = sqlEx.Message });
            }
        }

        /// <summary>
        /// Aplica un cambio recibido desde un servidor remoto en la base de datos local.
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
                Console.WriteLine($"[API] SQL Error en AplicarCambioRecibido: {sqlEx.Message}");
                return StatusCode(500, new { error = "Error en Base de Datos", detalle = sqlEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] Error general en AplicarCambioRecibido: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Realiza una sincronizacion pull desde una API remota, obteniendo y aplicando cambios pendientes.
        /// Si ParroquiaDestinoId es null o 0, trae TODOS los cambios.
        /// Si ParroquiaDestinoId tiene un valor valido, trae solo los cambios de esa parroquia.
        /// </summary>
        [HttpPost("jalar-cambios")]
        public async Task<IActionResult> JalarCambios([FromBody] JalarCambiosRequest request)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"[API] Iniciando sincronizacion pull desde: {request.UrlRemota}");

            bool filtrarPorParroquia = request.ParroquiaDestinoId.HasValue && request.ParroquiaDestinoId.Value > 0;
            int parroquiaId = filtrarPorParroquia ? request.ParroquiaDestinoId.Value : 0;

            Console.WriteLine($"[API] Tipo de sincronizacion: {(filtrarPorParroquia ? $"Filtrada por parroquia {parroquiaId}" : "Global (todas las parroquias)")}");
            Console.WriteLine($"[API] Hora: {DateTime.Now:HH:mm:ss}");

            if (string.IsNullOrEmpty(request.UrlRemota))
                return BadRequest(new { error = "La URL remota es requerida" });

            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(2);

            int cambiosAplicados = 0;
            int errores = 0;

            try
            {
                string urlCambios;
                if (filtrarPorParroquia)
                {
                    urlCambios = $"{request.UrlRemota.TrimEnd('/')}/api/Data/cambios-pendientes/{parroquiaId}";
                    Console.WriteLine($"[API] Consultando cambios filtrados por parroquia {parroquiaId} en: {urlCambios}");
                }
                else
                {
                    urlCambios = $"{request.UrlRemota.TrimEnd('/')}/api/Data/cambios-pendientes-todos";
                    Console.WriteLine($"[API] Consultando TODOS los cambios en: {urlCambios}");
                }

                HttpResponseMessage response = await httpClient.GetAsync(urlCambios);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[API] Error al obtener cambios: {response.StatusCode} - {errorContent}");
                    return BadRequest(new
                    {
                        error = $"No se pudo conectar a {request.UrlRemota}",
                        detalle = errorContent,
                        statusCode = response.StatusCode
                    });
                }

                var cambios = await response.Content.ReadFromJsonAsync<List<CambioPendienteDto>>();

                if (cambios == null || cambios.Count == 0)
                {
                    Console.WriteLine("[API] No hay cambios pendientes");
                    return Ok(new
                    {
                        mensaje = "No hay cambios pendientes",
                        cambiosAplicados = 0,
                        timestamp = DateTime.Now
                    });
                }

                Console.WriteLine($"[API] Se encontraron {cambios.Count} cambios pendientes");

                foreach (var cambio in cambios)
                {
                    try
                    {
                        Console.WriteLine($"[API] Aplicando cambio ID: {cambio.Id}, SP: {cambio.SpName}");

                        var aplicarRequest = new AplicarCambioRequest
                        {
                            Hash = cambio.Hash,
                            SpName = cambio.SpName,
                            ParametrosJson = cambio.ParametrosJson,
                            OrigenParroquiaId = cambio.OrigenParroquiaId,
                            ParroquiaDestinoId = parroquiaId
                        };

                        var aplicarResultado = await AplicarCambioRecibidoInternal(aplicarRequest);

                        if (aplicarResultado.Exitoso)
                        {
                            cambiosAplicados++;
                            Console.WriteLine($"[API] Cambio {cambio.Id} aplicado correctamente");

                            // Solo marcar entregado si NO es sincronizacion inicial
                            if (!request.EsSincronizacionInicial)
                            {
                                await MarcarEntregadoRemoto(httpClient, request.UrlRemota, cambio.Id, parroquiaId);
                            }
                        }
                        else
                        {
                            errores++;
                            Console.WriteLine($"[API] Error aplicando cambio {cambio.Id}: {aplicarResultado.Error}");
                        }
                    }
                    catch (Exception ex)
                    {
                        errores++;
                        Console.WriteLine($"[API] Excepcion en cambio {cambio.Id}: {ex.Message}");
                    }
                }

                Console.WriteLine($"[API] Sincronizacion completada - Aplicados: {cambiosAplicados}, Errores: {errores}");

                return Ok(new
                {
                    mensaje = "Sincronizacion completada",
                    cambiosEncontrados = cambios.Count,
                    cambiosAplicados = cambiosAplicados,
                    errores = errores,
                    timestamp = DateTime.Now
                });
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("[API] Timeout al conectar con la API remota");
                return StatusCode(408, new { error = "Timeout: La API remota no respondio" });
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[API] Error de conexion HTTP: {ex.Message}");
                return StatusCode(502, new { error = $"Error de conexion: {ex.Message}" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] Error general: {ex.Message}");
                Console.WriteLine($"[API] StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            try
            {
                using var conn = new SqlConnection(conexion);
                conn.Open();
                return Ok(new { status = "ok" });
            }
            catch
            {
                return StatusCode(503, new { status = "db_unavailable" });
            }
        }

        /// <summary>
        /// Metodo interno que aplica un cambio recibido sin exponer un endpoint HTTP.
        /// </summary>
        private async Task<(bool Exitoso, string Error)> AplicarCambioRecibidoInternal(AplicarCambioRequest request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_AplicarCambioRecibido", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Hash", request.Hash ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SpName", request.SpName);
                        cmd.Parameters.AddWithValue("@ParametrosJson", request.ParametrosJson);
                        cmd.Parameters.AddWithValue("@OrigenParroquiaId", request.OrigenParroquiaId);
                        cmd.Parameters.AddWithValue("@ParroquiaDestinoId", request.ParroquiaDestinoId);

                        object resultadoObj = await cmd.ExecuteScalarAsync();
                        string resultado = resultadoObj?.ToString() ?? "APLICADO";
                        return (resultado == "APLICADO", resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Marca un cambio como entregado en una API remota.
        /// </summary>
        private async Task MarcarEntregadoRemoto(HttpClient httpClient, string urlRemota, int cambioId, int destinoParroquiaId)
        {
            string url = $"{urlRemota.TrimEnd('/')}/api/Data/marcar-entregado";
            var request = new { CambioId = cambioId, DestinoParroquiaId = destinoParroquiaId };

            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[API] Cambio {cambioId} marcado como entregado en API remota");
                }
                else
                {
                    Console.WriteLine($"[API] No se pudo marcar cambio {cambioId} como entregado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[API] Error al marcar entregado remoto: {ex.Message}");
            }
        }
    }

    public class SpRequest
    {
        public string SpName { get; set; }
        public Dictionary<string, object> Parametros { get; set; }

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
        public int ParroquiaDestinoId { get; set; }
    }

    public class JalarCambiosRequest
    {
        public string UrlRemota { get; set; }
        public int? ParroquiaDestinoId { get; set; }

        public bool EsSincronizacionInicial { get; set; } = false;
    }

    public class CambioPendienteDto
    {
        public int Id { get; set; }
        public string SpName { get; set; }
        public string ParametrosJson { get; set; }
        public string Hash { get; set; }
        public int OrigenParroquiaId { get; set; }
    }
}