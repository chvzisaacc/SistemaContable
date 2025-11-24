using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class clsEnviarACajaChica
    {


        private Clsconexion conexion = new Clsconexion();

        public DataTable ObtenerCuentasDisponibles()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasDisponiblesCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public bool EnviarDineroCajaChica(int id_origen, decimal monto)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_EnviarDineroCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdOrigen", id_origen);
                cmd.Parameters.AddWithValue("@Monto", monto);

                SqlParameter paramExitoso = new SqlParameter("@Exitoso", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramExitoso);

                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramMensaje);

                cmd.ExecuteNonQuery();

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
                throw new Exception("Error al enviar dinero a caja chica: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }



    }
}
