using Capa_de_acceso_de_datos;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Capa_de_procesamiento_de_datos
{

    public class Detalle_gasto
    {
        public string nombre_cuenta { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }

    }
    public class Resultado
    {
        public int filas_guardadas { get; set; }
        public bool hubo_error { get; set; }
        public string mensaje { get; set; }
    }

    public class Gastos : Clsconexion
    {
        public int IngresarGastos(DateTime fecha_transaccion, string descripcion, decimal monto, int referencia, int idUsuario, int id_origen_nuevo, string nombre_cuenta)
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

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        nuevaTransaccion = Convert.ToInt32(result);
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


        public int ModificarGastos(int id_tansaccion, DateTime fecha, string descripcion, decimal monto, int referencia, int usuario_id, int id_origen, string nombre)
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

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                        filas_afectadas = count;
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
