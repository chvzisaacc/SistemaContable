using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona operaciones CRUD de usuarios del sistema: creación, modificación, consulta, habilitación y validación.
    /// Todos los métodos que modifican datos disparan sincronización remota automática.
    /// Los usuarios están asociados a roles y parroquias específicas.
    /// </summary>
    public class clsCRUD_Usuarios
    {
        private Clsconexion conexion;

        public clsCRUD_Usuarios()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Crea nuevo usuario ejecutando procedimiento sp_AgregarUsuario.
        /// Parámetros: nombre, apellido, correo (nullable), usuario (login), password (encriptada en BD),
        /// idrol (2=Administrador, 3=Empleado, 4=Sacerdote, etc), id_parroquia (asignación), id_estado (1=activo).
        /// Retorna ID autogenerado del usuario utilizando parámetro OUTPUT @nuevoId.
        /// Maneja NULL en correo si está vacío.
        /// Dispara NotificarSP automáticamente para sincronizar nueva cuenta con servidor remoto.
        /// </summary>
        public int AgregarUsuario(string nombre, string apellido, string correo, string usuario,
                                   string password, int idrol, int id_parroquia, int id_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@correo", string.IsNullOrEmpty(correo) ? (object)DBNull.Value : correo);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@idRol", idrol);
                cmd.Parameters.AddWithValue("@idParroquia", id_parroquia);
                cmd.Parameters.AddWithValue("@idEstado", id_estado);

                SqlParameter nuevo_id = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevo_id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevo_id);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);

                return Convert.ToInt32(nuevo_id.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario", ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene lista de todos los usuarios del sistema ejecutando procedimiento sp_ObtenerUsuarios.
        /// Retorna DataTable con: usuario_id, nombre, apellido, correo, usuario, rol, parroquia, estado, fecha creación.
        /// Se utiliza para poblar grillas de administración y listados generales de usuarios.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerUsuarios()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarios", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }

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

        /// <summary>
        /// Busca información completa de un usuario específico ejecutando procedimiento sp_BuscarUsuarioPorId.
        /// Parámetro id: usuario_id a buscar.
        /// Retorna DataRow con: usuario_id, nombre, apellido, correo, usuario, rol, parroquia, estado, etc.
        /// Retorna null si usuario no existe.
        /// Se utiliza para cargar datos en formularios de edición y perfiles.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataRow BuscarUsuarioPorId(int id)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_BuscarUsuarioPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }

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

        /// <summary>
        /// Modifica usuario existente ejecutando procedimiento sp_ModificarUsuario.
        /// Parámetros: id (identifica registro), nombre, apellido, correo (nullable), usuario (login),
        /// password (encriptada en BD), id_rol (rol nuevo), id_parroquia (parroquia nueva), id_estado (estado nuevo).
        /// Retorna true si modificación fue exitosa, false si ocurre error.
        /// Maneja NULL en correo si está vacío.
        /// Dispara NotificarSP automáticamente para sincronizar cambios con servidor remoto.
        /// </summary>
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

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
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

        /// <summary>
        /// Inhabilita usuario (soft delete) ejecutando procedimiento sp_InhabilitarUsuario.
        /// Parámetros: id (usuario a inhabilitar), nuevo_estado (0=inactivo, u otro estado desactivado).
        /// Retorna true si inhabilitación fue exitosa, false si ocurre error.
        /// No elimina el registro, solo marca el estado como inactivo para auditoría.
        /// Dispara NotificarSP automáticamente para sincronizar cambio de estado con servidor remoto.
        /// </summary>
        public bool InhabilitarUsuario(int id, int nuevo_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_InhabilitarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nuevoEstado", nuevo_estado);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
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
        /// Habilita usuario ejecutando procedimiento sp_InhabilitarUsuario (mismo SP que inhabilitar).
        /// Parámetros: id (usuario a habilitar), nuevo_estado (1=activo, u otro estado activado).
        /// Retorna true si habilitación fue exitosa, false si ocurre error.
        /// Restaura el estado activo de un usuario previamente inhabilitado.
        /// Dispara NotificarSP automáticamente para sincronizar cambio de estado con servidor remoto.
        /// </summary>
        public bool HabilitarUsuario(int id, int nuevo_estado)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_InhabilitarUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nuevoEstado", nuevo_estado);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
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
        /// Valida si nombre de usuario ya existe en el sistema ejecutando procedimiento sp_UsuarioExiste.
        /// Parámetro usuario: nombre de usuario (login) a validar.
        /// Utiliza parámetro OUTPUT @existe (bit) para retornar resultado.
        /// Retorna true si usuario existe, false si no existe o disponible.
        /// Se utiliza en validación de formularios para evitar duplicados antes de crear cuenta.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
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

                conexion.EjecutarYEnviar(cmd);

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
        /// Obtiene siguiente ID secuencial disponible para nuevo usuario ejecutando procedimiento sp_ObtenerProximoId.
        /// Retorna número entero del próximo ID a asignar.
        /// Se utiliza para mostrar ID provisional antes de crear usuario (en formularios).
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public int ObtenerProximoId()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerProximoId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                int proximoId = conexion.EjecutarScalarYEnviar(cmd);
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

        /// <summary>
        /// Obtiene lista de roles disponibles ejecutando procedimiento sp_ObtenerRoles.
        /// Retorna DataTable con: rol_id (2=Administrador, 3=Empleado, 4=Sacerdote, etc), nombre_rol, descripcion.
        /// Se utiliza para poblar ComboBox en formularios de creación/edición de usuarios.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerRoles()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerRoles", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }

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

        /// <summary>
        /// Obtiene lista de parroquias disponibles ejecutando procedimiento sp_ObtenerParroquias.
        /// Retorna DataTable con: parroquia_id, nombre_parroquia, correo, estado, etc.
        /// Se utiliza para poblar ComboBox en formularios de creación/edición de usuarios para asignar parroquia.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerParroquias()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerParroquias", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }

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
        /// Obtiene lista de estados disponibles para usuarios ejecutando procedimiento sp_ObtenerEstados.
        /// Retorna DataTable con: estado_id (1=activo, 0=inactivo, etc), nombre_estado, descripcion.
        /// Se utiliza para poblar ComboBox en formularios de creación/edición de usuarios para asignar estado.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerEstados()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerEstados", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }

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
        /// Obtiene correo electrónico de un usuario ejecutando procedimiento usp_GetCorreoUsuario.
        /// Parámetro usuario_id: identifica el usuario.
        /// Retorna string con correo del usuario, vacío si no existe o es NULL.
        /// Se utiliza para envío de notificaciones, recuperación de contraseña y comunicaciones.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public string ObtenerCorreoPorUsuario(int usuario_id)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.usp_GetCorreoUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuario_id;
                object obj = cmd.ExecuteScalar();
                return obj != null && obj != DBNull.Value ? obj.ToString() : string.Empty;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene nombre completo de un usuario ejecutando procedimiento usp_GetNombreUsuario.
        /// Parámetro usuario_id: identifica el usuario.
        /// Retorna string con nombre del usuario, vacío si no existe o es NULL.
        /// Se utiliza para mostrar información de usuario en interfaces, logs y reportes.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public string ObtenerNombrePorUsuario(int usuario_id)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.usp_GetNombreUsuario", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuario_id;
                object obj = cmd.ExecuteScalar();
                return obj != null && obj != DBNull.Value ? obj.ToString() : string.Empty;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}