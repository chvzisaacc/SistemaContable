using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class clsCRUD_Usuarios
    {

        /// <summary>
        /// The conexion
        /// </summary>
        private Clsconexion conexion;

        /// <summary>
        /// Initializes a new instance of the <see cref="clsCRUD_Usuarios"/> class.
        /// </summary>
        public clsCRUD_Usuarios()
        {
            conexion = new Clsconexion();
        }
        /// <summary>
        /// Agregars the usuario.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <param name="apellido">The apellido.</param>
        /// <param name="correo">The correo.</param>
        /// <param name="usuario">The usuario.</param>
        /// <param name="password">The password.</param>
        /// <param name="idrol">The idrol.</param>
        /// <param name="id_parroquia">The identifier parroquia.</param>
        /// <param name="id_estado">The identifier estado.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al agregar usuario: " + ex.Message</exception>
        public int AgregarUsuario(string nombre, string apellido, string correo, string usuario,
                                   string password, int idrol, int id_parroquia, int id_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@correo", string.IsNullOrEmpty(correo) ? (object)DBNull.Value : correo);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@idRol", idrol);
                cmd.Parameters.AddWithValue("@idParroquia", id_parroquia);
                cmd.Parameters.AddWithValue("@idEstado", id_estado);

                // Parámetro de salida para obtener el ID generado
                SqlParameter nuevo_id = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevo_id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevo_id);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(nuevo_id.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // OBTENER todos los usuarios
        /// <summary>
        /// Obteners the usuarios.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener usuarios: " + ex.Message</exception>
        public DataTable ObtenerUsuarios()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarios", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

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

        // BUSCAR usuario por ID
        /// <summary>
        /// Buscars the usuario por identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al buscar usuario: " + ex.Message</exception>
        public DataRow BuscarUsuarioPorId(int id)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_BuscarUsuarioPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

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
                throw new Exception("Error al buscar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // MODIFICAR usuario
        /// <summary>
        /// Modificars the usuario.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="apellido">The apellido.</param>
        /// <param name="correo">The correo.</param>
        /// <param name="usuario">The usuario.</param>
        /// <param name="password">The password.</param>
        /// <param name="id_rol">The identifier rol.</param>
        /// <param name="id_parroquia">The identifier parroquia.</param>
        /// <param name="id_estado">The identifier estado.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al modificar usuario: " + ex.Message</exception>
        public bool ModificarUsuario(int id, string nombre, string apellido, string correo,
                                    string usuario, string password, int id_rol, int id_parroquia, int id_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ModificarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@correo", string.IsNullOrEmpty(correo) ? (object)DBNull.Value : correo);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@idRol", id_rol);
                cmd.Parameters.AddWithValue("@idParroquia", id_parroquia);
                cmd.Parameters.AddWithValue("@idEstado", id_estado);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // ELIMINAR usuario
        /// <summary>
        /// Eliminars the usuario.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al eliminar usuario: " + ex.Message</exception>
        public bool EliminarUsuario(int id)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // INHABILITAR usuario (soft delete)
        /// <summary>
        /// Inhabilitars the usuario.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nuevo_estado">The nuevo estado.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al inhabilitar usuario: " + ex.Message</exception>
        public bool InhabilitarUsuario(int id, int nuevo_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_InhabilitarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nuevoEstado", nuevo_estado);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inhabilitar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }


        /// <summary>
        /// Habilitars the usuario.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nuevo_estado">The nuevo estado.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al inhabilitar usuario: " + ex.Message</exception>
        public bool HabilitarUsuario(int id, int nuevo_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_InhabilitarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nuevoEstado", nuevo_estado);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inhabilitar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // VALIDAR si usuario existe
        /// <summary>
        /// Usuarioes the existe.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al validar usuario: " + ex.Message</exception>
        public bool UsuarioExiste(string usuario)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_UsuarioExiste", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", usuario);

                SqlParameter existe = new SqlParameter("@existe", SqlDbType.Bit);
                existe.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(existe);

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(existe.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obteners the proximo identifier.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener próximo ID: " + ex.Message</exception>
        public int ObtenerProximoId()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerProximoId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                int proximoId = (int)cmd.ExecuteScalar();
                return proximoId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo ID: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // OBTENER roles para ComboBox
        /// <summary>
        /// Obteners the roles.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener roles: " + ex.Message</exception>
        public DataTable ObtenerRoles()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerRoles", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener roles: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // OBTENER parroquias para ComboBox
        /// <summary>
        /// Obteners the parroquias.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener parroquias: " + ex.Message</exception>
        public DataTable ObtenerParroquias()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerParroquias", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener parroquias: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obteners the estados.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener estados: " + ex.Message</exception>
        public DataTable ObtenerEstados()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerEstados", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estados: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }


        /// <summary>
        /// Obteners the correo por usuario.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        public string ObtenerCorreoPorUsuario(int usuario_id)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.usp_GetCorreoUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuario_id;

                var obj = cmd.ExecuteScalar();
                return obj?.ToString() ?? string.Empty;
            }
            finally
            {
                conexion.Cerrar();
            }
        }



    }
}
