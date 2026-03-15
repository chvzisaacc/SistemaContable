using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class clsEnviarACajaChica
    {


        /// <summary>
        /// The conexion
        /// </summary>
        private Clsconexion conexion = new Clsconexion();

        /// <summary>
        /// Obteners the cuentas disponibles.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener cuentas disponibles: " + ex.Message</exception>
        public DataTable ObtenerCuentasDisponibles(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasDisponiblesCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cuentas disponibles: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obteners the saldo caja chica.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener saldo de caja chica: " + ex.Message</exception>
        public decimal ObtenerSaldoCajaChica()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerSaldoCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener saldo de caja chica: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Enviars the dinero caja chica.
        /// </summary>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="monto">The monto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Error al enviar dinero a caja chica: " + ex.Message
        /// </exception>
        public bool EnviarDineroCajaChica(int id_origen, decimal monto, int parroquiaId, int usuarioId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_EnviarDineroCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                // ESTOS 4 SON LOS PARÁMETROS DE ENTRADA (Input)
                cmd.Parameters.AddWithValue("@IdOrigen", id_origen);
                cmd.Parameters.AddWithValue("@Monto", monto);
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);
                cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);

                // ESTOS 2 SON LOS PARÁMETROS DE SALIDA (Output)
                SqlParameter paramExitoso = new SqlParameter("@Exitoso", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(paramExitoso);
                cmd.Parameters.Add(paramMensaje);

                cmd.ExecuteNonQuery();

                // Leemos los resultados del SP
                bool exitoso = Convert.ToBoolean(paramExitoso.Value);

                if (!exitoso)
                {
                    string mensaje = paramMensaje.Value?.ToString() ?? "Error desconocido";
                    throw new Exception(mensaje);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Esto lanzará el error exacto que venga de SQL
                throw new Exception("Error al procesar: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerTiposCuenta()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_cargatipcuenta", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar tipos de cuenta: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }





    }
}
