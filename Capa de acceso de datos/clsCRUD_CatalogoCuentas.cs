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

        public DataTable ObtenerNivel1()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerNivel1", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener nivel 1: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public DataTable ObtenerHijosPorPadre(int id_padre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerHijosPorPadre", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener hijos: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
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
                throw new Exception("Error al obtener catálogo: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public DataRow BuscarCatalogoCuentaPorId(int id_cuenta)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarCatalogoCuentaPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public bool AgregarCatalogoCuenta(string codigo, string nombre, int id_padre,
                                          string detalle, bool es_detalle = true)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_AgregarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                cmd.Parameters.AddWithValue("@detalle", string.IsNullOrWhiteSpace(detalle)
                                                           ? (object)DBNull.Value : detalle);
                cmd.Parameters.AddWithValue("@es_detalle", es_detalle ? 1 : 0);
                cmd.Parameters.AddWithValue("@activa", 1);

                // ExecuteScalar para obtener el nuevo id
                var resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public bool ModificarCatalogoCuenta(int id_cuenta, string codigo, string nombre,
                                            int id_padre, string detalle)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ModificarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                cmd.Parameters.AddWithValue("@detalle", string.IsNullOrWhiteSpace(detalle)
                                                          ? (object)DBNull.Value : detalle);
                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public bool CambiarEstadoCuenta(int id_cuenta, bool activa)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_CambiarEstadoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
                cmd.Parameters.AddWithValue("@activa", activa ? 1 : 0);
                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public bool CatalogoCuentaExiste(string nombre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_CatalogoCuentaExiste", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                SqlParameter existe = new SqlParameter("@existe", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(existe);
                cmd.ExecuteNonQuery();
                return Convert.ToBoolean(existe.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        public string ObtenerProximoCodigo(int id_padre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerProximoCodigoCatalogo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                var resultado = cmd.ExecuteScalar();
                return resultado?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo código: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }
    }
}
