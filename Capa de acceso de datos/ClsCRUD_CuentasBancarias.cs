using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class ClsCRUD_CuentasBancarias
    {
        private Clsconexion conexion;

        public ClsCRUD_CuentasBancarias()
        {
            conexion = new Clsconexion();
        }

        public int AgregarCuentaBancaria(int CuentaBancariaID, decimal? saldo, decimal? tasa_interes, decimal? ganancia_generada)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_cuentaBanco", CuentaBancariaID);
                cmd.Parameters.AddWithValue("@saldo", saldo);
                cmd.Parameters.AddWithValue("@tasa_interes", tasa_interes);
                cmd.Parameters.AddWithValue("@ganancia_generada", ganancia_generada);

                SqlParameter nuevoId = new SqlParameter("@nuevoId", System.Data.SqlDbType.Int);
                nuevoId.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(nuevoId);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(nuevoId.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cuenta bancaria: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }

        }

        public DataTable ObtenerCuentasBancarias()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasBancarias", conexion.sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
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
    }
}
