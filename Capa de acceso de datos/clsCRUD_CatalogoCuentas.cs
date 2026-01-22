using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class clsCRUD_CatalogoCuentas
    {
        /// <summary>
        /// The conexion
        /// </summary>
        private Clsconexion conexion;

        /// <summary>
        /// Initializes a new instance of the <see cref="clsCRUD_CatalogoCuentas"/> class.
        /// </summary>
        public clsCRUD_CatalogoCuentas()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Agregars the catalogo cuenta.
        /// </summary>
        /// <param name="id_cuenta">The identifier cuenta.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="saldo">The saldo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al agregar cuenta al catálogo: " + ex.Message</exception>
        // CAMBIAR TODO EL CONTENIDO DEL MÉTODO:
        public bool AgregarCatalogoCuenta(string codigo_cuenta, int id_cuenta, string nombre, String detalle, decimal? saldo, int id_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_AgregarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigoCuenta", codigo_cuenta);
                cmd.Parameters.AddWithValue("@idCuenta", id_cuenta);
                cmd.Parameters.AddWithValue("@nombre", nombre);

                if (!string.IsNullOrWhiteSpace(detalle))
                    cmd.Parameters.AddWithValue("@detalle", detalle);
                else
                    cmd.Parameters.AddWithValue("@detalle", DBNull.Value);

                if (saldo.HasValue)
                    cmd.Parameters.AddWithValue("@saldo", saldo.Value);
                else
                    cmd.Parameters.AddWithValue("@saldo", DBNull.Value);

                cmd.Parameters.AddWithValue("@idEstado", id_estado);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
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

        /// <summary>
        /// Obteners the catalogo cuentas.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener catálogo de cuentas: " + ex.Message</exception>
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

        public bool CambiarEstadoCuenta(String cod_cuenta, int nuevo_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_CambiarEstadoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codCuenta", cod_cuenta);
                cmd.Parameters.AddWithValue("@nuevoEstado", nuevo_estado);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Buscars the catalogo cuenta por identifier.
        /// </summary>
        /// <param name="cod_cuenta">The cod cuenta.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al buscar cuenta: " + ex.Message</exception>
        public DataRow BuscarCatalogoCuentaPorId(String cod_cuenta)
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

        /// <summary>
        /// Modificars the catalogo cuenta.
        /// </summary>
        /// <param name="cod_cuenta">The cod cuenta.</param>
        /// <param name="id_cuenta">The identifier cuenta.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="saldo">The saldo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al modificar cuenta: " + ex.Message</exception>
        public bool ModificarCatalogoCuenta(String cod_cuenta, int id_cuenta, string nombre, String detalle, decimal? saldo)
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



        /// <summary>
        /// Catalogoes the cuenta existe.
        /// </summary>
        /// <param name="nombre_cuenta">The nombre cuenta.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al validar cuenta: " + ex.Message</exception>
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

        /// <summary>
        /// Obteners the proximo codigo.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener próximo código: " + ex.Message</exception>
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

        /// <summary>
        /// Obteners the cuentas.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener cuentas: " + ex.Message</exception>
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

        /// <summary>
        /// Obteners the tipo transaccion.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener cuentas: " + ex.Message</exception>
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
