using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class clsTransferenciaEntreCuentas
    {
        /// <summary>
        /// The conexion
        /// </summary>
        private Clsconexion conexion = new Clsconexion();

        /// <summary>
        /// Obteners the cuentas banco.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener cuentas bancarias: " + ex.Message</exception>
        public DataTable ObtenerCuentasBanco(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerOrigenFuentes", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cuentas bancarias: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }


        /// <summary>
        /// Transferirs the entre cuentas.
        /// </summary>
        /// <param name="cuenta_origen">The cuenta origen.</param>
        /// <param name="cuenta_destino">The cuenta destino.</param>
        /// <param name="monto">The monto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al realizar la transferencia: " + ex.Message</exception>
        public bool TransferirEntreCuentas(int cuenta_origen, int cuenta_destino, decimal monto,
                                   int parroquiaId, int usuarioId, string descripcion = null)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_TransferirEntreCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CuentaOrigen", cuenta_origen);
                cmd.Parameters.AddWithValue("@CuentaDestino", cuenta_destino);
                cmd.Parameters.AddWithValue("@Monto", monto);
                cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);

                // Si no se proporciona descripción, usar valor por defecto
                string desc = string.IsNullOrWhiteSpace(descripcion) ? "Transferencia" : descripcion;
                cmd.Parameters.AddWithValue("@descripcion", desc);

                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);

                conexion.EjecutarYEnviar(cmd);
                return true;
            }
            catch (SqlException ex)
            {
                // Manejar errores específicos del SP
                string mensaje = ex.Number switch
                {
                    50001 => "Saldo insuficiente en la cuenta de origen.",
                    50002 => "La cuenta de origen no existe.",
                    50003 => "La cuenta de destino no existe.",
                    50004 => "Las cuentas de origen y destino deben ser diferentes.",
                    _ => "Error al realizar la transferencia: " + ex.Message
                };
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar la transferencia: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerCajaChica(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCajaChicaParroquia", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);
                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Caja Chica: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

    }
}