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

        public int AgregarCuentaBancaria(int CuentaBancariaID,string Nombre, decimal? saldo, decimal? tasa_interes)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Origen", CuentaBancariaID);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@saldo", saldo);
                cmd.Parameters.AddWithValue("@tasa_interes", tasa_interes);
                ;

                SqlParameter nuevoId = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevoId.Direction = ParameterDirection.Output;
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
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Cuentas Bancarias: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
        public bool ModificarSaldo(int Id_Origen, decimal saldo)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("sp_ModificarSaldo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = Id_Origen;

                var pSaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                pSaldo.Precision = 18;
                pSaldo.Scale = 2;
                pSaldo.Value = saldo;

                int filas = cmd.ExecuteNonQuery(); // esperado: 1 si actualiza una fila
                return filas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar saldo: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
        public bool AgregarSaldo(int Id_Origen, decimal monto)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_AgregarSaldo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = Id_Origen;

                var pMonto = cmd.Parameters.Add("@monto", SqlDbType.Decimal);
                pMonto.Precision = 18;
                pMonto.Scale = 2;
                pMonto.Value = monto;

                int filas = cmd.ExecuteNonQuery();   // esperado: 1 si actualiza una fila
                return filas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar saldo: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
        public bool CrearCuentaBanco(String Nombre, decimal saldo, decimal tasaInteres, out int nuevo_Id)
        {
            nuevo_Id = 0;
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@Id_cuentaBanco", SqlDbType.Int).Value = idCuentaBanco;
                var pNombre = cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = Nombre ?? string.Empty;
                //pNombre.Value = Nombre ?? string.Empty;
                
                var pSaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                pSaldo.Precision = 10; 
                pSaldo.Scale = 2; 
                pSaldo.Value = saldo;

                var pTasa = cmd.Parameters.Add("@tasa_interes", SqlDbType.Decimal);
                pTasa.Precision = 4;
                pTasa.Scale = 2; 
                pTasa.Value = tasaInteres;

               var pOut = cmd.Parameters.Add("@nuevo_Id", SqlDbType.Int);
                pOut.Direction = ParameterDirection.Output;

                int filas = cmd.ExecuteNonQuery();
                if (pOut.Value != DBNull.Value && (int)pOut.Value > 0)
                {
                    nuevo_Id = (int)pOut.Value;

                    return true;
                }
                else
                {
                    return false; // no hubo id generad
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear cuenta bancaria: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
