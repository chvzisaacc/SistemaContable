using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_Historial
    {
        private Clsconexion conexion;

        public clsCRUD_Historial()
        {
            conexion = new Clsconexion();
        }

        public DataTable ObtenerHistorial(int? parroquia_id = null, int? usuario_id = null)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorial", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@parroquiaId", parroquia_id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@usuarioId", usuario_id ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerUsuariosPorParroquia(int? parroquia_id = null)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuariosPorParroquia", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parroquiaId", parroquia_id ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public void RegistrarInicioSesion(int usuario_id)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_RegistrarInicioSesion", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario_id", usuario_id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar inicio de sesión: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerHistorialSacerdote()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorialSacerdote", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public void RegistrarActividad(int usuario_id, int modulo_id, string tarea, string descripcion)
        {
            try
            {
                conexion.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_RegistrarActividad", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_usuario", usuario_id);
                    cmd.Parameters.AddWithValue("@id_modulo", modulo_id);
                    cmd.Parameters.AddWithValue("@tarea_realizada", tarea);
                    cmd.Parameters.AddWithValue("@descripcion_tarea", descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public void RegistrarAccionUsuario(int usuario_id, string modulo, string accion, decimal? monto, string descripcion)
        {
            try
            {
                conexion.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarAccionUsuario", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario_id);
                    cmd.Parameters.AddWithValue("@Modulo", modulo);
                    cmd.Parameters.AddWithValue("@Accion", accion);
                    cmd.Parameters.AddWithValue("@Monto", (object)monto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar acción de usuario: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene el historial de acciones para un usuario específico.
        /// </summary>
        public DataTable ObtenerHistorialUsuario(int usuario_id)
        {
            try
            {
                conexion.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerHistorialUsuario", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario_id);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el historial del usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
        

    }
}
