using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    public class clsTransferenciaEntreCuentas
    {
        private Clsconexion conexion = new Clsconexion();

        public DataTable ObtenerCuentasBanco()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerOrigenFuentes", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
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


        public bool TransferirEntreCuentas(int cuenta_origen, int cuenta_destino, decimal monto)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_TransferirEntreCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CuentaOrigen", cuenta_origen);
                cmd.Parameters.AddWithValue("@CuentaDestino", cuenta_destino);
                cmd.Parameters.AddWithValue("@Monto", monto);

                cmd.ExecuteNonQuery();
                return true;
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
    }
}
