using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    // DTO (Data Transfer Object) con el desglose de un gasto individual
    public class Detalle_gasto
    {
        public string nombre_cuenta { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
    }

    // DTO de respuesta genérica para operaciones de escritura
    public class Resultado
    {
        public int filas_guardadas { get; set; }
        public bool hubo_error { get; set; }
        public string mensaje { get; set; }
    }

    // CRUD de gastos; hereda conexión a BD desde Clsconexion
    public class Gastos : Clsconexion
    {
        /// <summary>Registra un nuevo gasto y retorna el ID de la transacción creada.</summary>
        public int IngresarGastos(DateTime fecha_transaccion, string descripcion, decimal monto,
            int referencia, int idUsuario, int id_origen_nuevo, string nombre_cuenta, int idParroquia)
        {
            int nuevaTransaccion = 0;
            try
            {
                Abrir();
                using (SqlCommand command = new SqlCommand("ingresargastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fecha_transaccion", fecha_transaccion);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_historico", monto);
                    command.Parameters.AddWithValue("@numero_de_referencia", referencia);
                    command.Parameters.AddWithValue("@usuario_id", idUsuario);
                    command.Parameters.AddWithValue("@id_origen", id_origen_nuevo);
                    command.Parameters.AddWithValue("@nombre", nombre_cuenta);
                    command.Parameters.AddWithValue("@Parroquia_ID", idParroquia);
                    // Los gastos no tienen cuenta destino; se envía DBNull para satisfacer el parámetro del SP
                    command.Parameters.AddWithValue("@id_cuenta_destino", DBNull.Value);

                    nuevaTransaccion = EjecutarScalarYEnviar(command, sincronizar: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ingresar el gasto: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return nuevaTransaccion;
        }

        /// <summary>Actualiza un gasto existente y retorna las filas afectadas.</summary>
        public int ModificarGastos(int id_tansaccion, DateTime fecha, string descripcion,
            decimal monto, int referencia, int usuario_id, int id_origen, string nombre)
        {
            int filas_afectadas = 0;
            try
            {
                Abrir();
                using (SqlCommand command = new SqlCommand("sp_ModificarGastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_transaccion", id_tansaccion);
                    command.Parameters.AddWithValue("@fecha_transaccion", fecha);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_nuevo", monto);
                    command.Parameters.AddWithValue("@Numero_de_Referencia", referencia);
                    command.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@Id_Origen", id_origen);
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    filas_afectadas = EjecutarScalarYEnviar(command, sincronizar: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el gasto: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return filas_afectadas;
        }
    }
}