using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    // Modelo con el desglose de un ingreso individual
    public class DetalleIngreso
    {
        public string nombre_cuenta { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
    }

    // Modelo de respuesta genérica para operaciones de escritura
    public class ResultadoGuardado
    {
        public int filas_guardadas { get; set; }
        public bool hubo_error { get; set; }
        public string mensaje { get; set; }
    }

    // CRUD de ingresos; hereda conexión a BD desde Clsconexion
    public class Ingresos : Clsconexion
    {
        /// <summary>Registra un nuevo ingreso y retorna el ID de la transacción creada.</summary>
        public int IngresarIngresos(DateTime fecha, string descripcion, decimal monto,
            int referencia, int usuario_id, int id_origen, string nombre)
        {
            int nuevaTransa = 0;
            try
            {
                // SQL Server no acepta fechas anteriores a 1753; se usa la fecha actual como fallback
                DateTime fechaValidada = (fecha < new DateTime(1753, 1, 1) || fecha == DateTime.MinValue)
                                         ? DateTime.Now : fecha;

                using (SqlCommand command = new SqlCommand("IngresarIngresos"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Se usan tipos explícitos para evitar conversiones implícitas problemáticas
                    command.Parameters.Add("@fecha_transaccion", SqlDbType.DateTime).Value = fechaValidada;
                    command.Parameters.Add("@descripcion", SqlDbType.NVarChar, 100).Value = descripcion ?? (object)DBNull.Value;
                    command.Parameters.Add("@monto_historico", SqlDbType.Decimal).Value = monto;
                    command.Parameters.Add("@Numero_de_Referencia", SqlDbType.Int).Value = referencia;
                    command.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuario_id;
                    command.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = id_origen;
                    command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = nombre ?? (object)DBNull.Value;

                    var resultado = EjecutarScalarYEnviar(command, sincronizar: true);

                    // El SP retorna el ID generado; se valida antes de asignarlo
                    if (resultado != null && int.TryParse(resultado.ToString(), out int idGenerado))
                    {
                        nuevaTransa = idGenerado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ingresar el ingreso: " + ex.Message, ex);
            }

            return nuevaTransa;
        }

        /// <summary>Actualiza un ingreso existente y retorna las filas afectadas.</summary>
        public int ModificarIngreso(int id_transaccion, DateTime fecha, string descripcion,
            decimal monto, int referencia, int usuario_id, int id_origen, string nombre)
        {
            int filas_afectadas = 0;
            try
            {
                // SQL Server no acepta fechas anteriores a 1753; se usa la fecha actual como fallback
                DateTime fechaValidada = (fecha < new DateTime(1753, 1, 1) || fecha == DateTime.MinValue)
                                         ? DateTime.Now : fecha;

                Abrir();

                using (SqlCommand command = new SqlCommand("sp_ModificarIngresos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_transaccion", id_transaccion);
                    command.Parameters.AddWithValue("@fecha_transaccion", fechaValidada);
                    command.Parameters.AddWithValue("@descripcion", descripcion ?? "");
                    command.Parameters.AddWithValue("@monto_nuevo", monto);
                    command.Parameters.AddWithValue("@Numero_de_Referencia", referencia);
                    command.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@Id_Origen", id_origen);
                    command.Parameters.AddWithValue("@Nombre", nombre ?? "");

                    filas_afectadas = EjecutarScalarYEnviar(command, sincronizar: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el ingreso: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return filas_afectadas;
        }
    }
}