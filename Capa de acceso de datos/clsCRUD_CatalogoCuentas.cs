using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_CatalogoCuentas
    {
        private Clsconexion conexion;

        public clsCRUD_CatalogoCuentas()
        {
            conexion = new Clsconexion();
        }

        public int AgregarCatalogoCuenta(int id_cuenta, string nombre, String detalle, decimal? saldo)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_AgregarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCuenta", id_cuenta);
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

                SqlParameter nuevoid = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevoid.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevoid);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(nuevoid.Value);
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

        public DataRow BuscarCatalogoCuentaPorId(int cod_cuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_BuscarCatalogoCuentaPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cod_cuenta);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
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

        public bool ModificarCatalogoCuenta(int cod_cuenta, int id_cuenta, string nombre, String detalle, decimal? saldo)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ModificarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codCuenta", cod_cuenta);
                cmd.Parameters.AddWithValue("@idCuenta", id_cuenta);
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



        public bool CatalogoCuentaExiste(string nombre_cuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_CatalogoCuentaExiste", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreCuenta", nombre_cuenta);

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

                int proximo_codigo = (int)cmd.ExecuteScalar();
                return proximo_codigo;
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
