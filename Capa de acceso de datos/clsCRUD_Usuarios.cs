using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_Usuarios
    {

        private Clsconexion conexion;

        public clsCRUD_Usuarios()
        {
            conexion = new Clsconexion();
        }
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
