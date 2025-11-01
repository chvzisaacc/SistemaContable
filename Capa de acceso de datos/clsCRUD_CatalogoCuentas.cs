using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_CatalogoCuentas
    {
        private Clsconexion conexion;

        public clsCRUD_CatalogoCuentas()
        {
            conexion = new Clsconexion();
        }

        public int AgregarCatalogoCuenta(int idCuenta, string nombre, String detalle, decimal? saldo)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_AgregarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                if (!string.IsNullOrWhiteSpace(detalle))
                    cmd.Parameters.AddWithValue("@detalle", detalle);
                else
                    cmd.Parameters.AddWithValue("@detalle", DBNull.Value);

                // Parámetro saldo como decimal
                if (saldo.HasValue)
                    cmd.Parameters.AddWithValue("@saldo", saldo.Value);
                else
                    cmd.Parameters.AddWithValue("@saldo", DBNull.Value);

                SqlParameter nuevoId = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevoId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevoId);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(nuevoId.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cuenta al catálogo: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerCatalogoCuentas()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCatalogoCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener catálogo de cuentas: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataRow BuscarCatalogoCuentaPorId(int codCuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_BuscarCatalogoCuentaPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", codCuenta);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable() ;
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public bool ModificarCatalogoCuenta(int codCuenta, int idCuenta, string nombre, String detalle, decimal? saldo)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ModificarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codCuenta", codCuenta);
                cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                if (!string.IsNullOrWhiteSpace(detalle))
                    cmd.Parameters.AddWithValue("@detalle", detalle);
                else
                    cmd.Parameters.AddWithValue("@detalle", DBNull.Value);

                cmd.Parameters.AddWithValue("@saldo", saldo.HasValue ? (object)saldo.Value : DBNull.Value);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        

        public bool CatalogoCuentaExiste(string nombreCuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_CatalogoCuentaExiste", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreCuenta", nombreCuenta);

                SqlParameter existe = new SqlParameter("@existe", SqlDbType.Bit);
                existe.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(existe);

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(existe.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public int ObtenerProximoCodigo()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerProximoCodigoCatalogo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                int proximoCodigo = (int)cmd.ExecuteScalar();
                return proximoCodigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo código: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerCuentas()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cuentas: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerTipoTransaccion()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerTiposTransaccion", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cuentas: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }


    }
}
