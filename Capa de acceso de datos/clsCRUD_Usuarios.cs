using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_Usuarios
    {

        private Clsconexion conexion;

        public clsCRUD_Usuarios()
        {
            conexion = new Clsconexion();
        }




        public bool AgregarUsuario(string nombre, string apellido, string correo, string usuario,
                                   string password, int idRol, int idParroquia, int idEstado)
        {
            try
            {
                conexion.Abrir();

                string query = @"INSERT INTO dbo.usuario 
                                (usuario_nombre, usuario_apellido, usuario_correo, usuario, 
                                 usuario_password, Id_estado_cuenta, Rol_Id, Parroquia_Id) 
                                VALUES 
                                (@nombre, @apellido, @correo, @usuario, @password, @idEstado, @idRol, @idParroquia)";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                if (string.IsNullOrEmpty(correo))
                    cmd.Parameters.AddWithValue("@correo", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@correo", correo);
                                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@idRol", idRol);
                cmd.Parameters.AddWithValue("@idParroquia", idParroquia);
                cmd.Parameters.AddWithValue("@idEstado", idEstado);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
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

        public DataTable ObtenerUsuarios()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                                    u.Usuario_id AS ID,
                                    u.usuario_nombre AS Nombre,
                                    u.usuario_apellido AS Apellido,
                                    u.usuario_correo AS Correo,
                                    u.usuario AS Usuario,
                                    u.usuario_password AS Contraseña,
                                    u.Rol_Id AS Rol,
                                    u.Parroquia_Id AS Parroquia,
                                    u.Id_estado_cuenta AS Estado
                                FROM dbo.usuario u
                                ORDER BY u.Usuario_id";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
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

        public bool ModificarUsuario(int id, string nombre, string apellido, string correo,
                                    string usuario, string password, int idRol, int idParroquia, int idEstado)
        {
            try
            {
                conexion.Abrir();

                string query = @"UPDATE dbo.usuario 
                                SET usuario_nombre = @nombre,
                                    usuario_apellido = @apellido,
                                    usuario_correo = @correo,
                                    usuario = @usuario,
                                    usuario_password = @password,
                                    Rol_Id = @idRol,
                                    Parroquia_Id = @idParroquia,
                                    Id_estado_cuenta = @idEstado
                                WHERE Usuario_id = @id";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                if (string.IsNullOrEmpty(correo))
                    cmd.Parameters.AddWithValue("@correo", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@correo", correo);
                
                                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@idRol", idRol);
                cmd.Parameters.AddWithValue("@idParroquia", idParroquia);
                cmd.Parameters.AddWithValue("@idEstado", idEstado);

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

        public bool EliminarUsuario(int id)
        {
            try
            {
                conexion.Abrir();

                string query = "DELETE FROM dbo.usuario WHERE Usuario_id = @id";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
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

        public bool InhabilitarUsuario(int id, int nuevoEstado)
        {
            try
            {
                conexion.Abrir();

                string query = "UPDATE dbo.usuario SET Id_estado_cuenta = @estado WHERE Usuario_id = @id";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@estado", nuevoEstado);

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

        public bool UsuarioExiste(string usuario)
        {
            try
            {
                conexion.Abrir();

                string query = "SELECT COUNT(*) FROM dbo.usuario WHERE usuario = @usuario";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
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

        public DataTable ObtenerRoles()
        {
            try
            {
                conexion.Abrir();

                string query = "SELECT Rol_Id, Rol_descripcion FROM dbo.Rol ORDER BY Rol";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
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

        public DataTable ObtenerParroquias()
        {
            try
            {
                conexion.Abrir();

                string query = "SELECT Parroquia_id, Parroquia_nombre FROM dbo.Parroquia ORDER BY Parroquia_nombre";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
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

                string query = "SELECT Id_estado_cuenta, descripcion FROM dbo.Estado_cuenta ORDER BY descripcion";

                SqlCommand cmd = new SqlCommand(query, conexion.sc);
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


    }
}
