using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class clsCRUD_Historial
    {
        /// <summary>
        /// The conexion
        /// </summary>
        private Clsconexion conexion;

        /// <summary>
        /// Initializes a new instance of the <see cref="clsCRUD_Historial"/> class.
        /// </summary>
        public clsCRUD_Historial()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Obteners the historial.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener historial: " + ex.Message</exception>
        public DataTable ObtenerHistorial(
    out int totalRegistros,
    int? parroquia_id = null,
    int? usuario_id = null,
    DateTime? fechaDesde = null,
    DateTime? fechaHasta = null,
    int pagina = 1,
    int tamanoPagina = 50)
        {
            totalRegistros = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorial", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@parroquiaId",
                    parroquia_id.HasValue ? (object)parroquia_id.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@usuarioId",
                    usuario_id.HasValue ? (object)usuario_id.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaDesde",
                    fechaDesde.HasValue ? (object)fechaDesde.Value.Date : DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaHasta",
                    fechaHasta.HasValue ? (object)fechaHasta.Value.Date : DBNull.Value);
                cmd.Parameters.AddWithValue("@Pagina", pagina);
                cmd.Parameters.AddWithValue("@TamanoPagina", tamanoPagina);

                SqlParameter paramTotal = new SqlParameter("@TotalRegistros", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramTotal);
                DataTable dt = conexion.EjecutarAdapterYEnviar(cmd);

                totalRegistros = paramTotal.Value != DBNull.Value
                    ? Convert.ToInt32(paramTotal.Value)
                    : 0;

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

        /// <summary>
        /// Obteners the usuarios por parroquia.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener usuarios: " + ex.Message</exception>
        public DataTable ObtenerUsuariosPorParroquia(int? parroquia_id = null)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuariosPorParroquia", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parroquiaId", parroquia_id ?? (object)DBNull.Value);

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
        /// Registrars the inicio sesion.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <exception cref="System.Exception">Error al registrar inicio de sesión: " + ex.Message</exception>
        public void RegistrarInicioSesion(int usuario_id)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_RegistrarInicioSesion", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario_id", usuario_id);

                conexion.EjecutarYEnviar(cmd);
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

        /// <summary>
        /// Obteners the historial sacerdote.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener el historial: " + ex.Message</exception>
        public DataTable ObtenerHistorialSacerdote()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorialSacerdote", conexion.sc);
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
                throw new Exception("Error al obtener el historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Registrars the actividad.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="modulo_id">The modulo identifier.</param>
        /// <param name="tarea">The tarea.</param>
        /// <param name="descripcion">The descripcion.</param>
        /// <exception cref="System.Exception">Error al obtener el historial: " + ex.Message</exception>
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

                    conexion.EjecutarYEnviar(cmd);
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

        /// <summary>
        /// Registrars the accion usuario.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="modulo">The modulo.</param>
        /// <param name="accion">The accion.</param>
        /// <param name="monto">The monto.</param>
        /// <param name="descripcion">The descripcion.</param>
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
                    conexion.EjecutarYEnviar(cmd);
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
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener el historial del usuario: " + ex.Message</exception>
        public DataTable ObtenerHistorialUsuario(int usuario_id,
                                           DateTime? fechaDesde = null,
                                           DateTime? fechaHasta = null)
        {
            try
            {
                conexion.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerHistorialUsuario", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuario_id);
                    cmd.Parameters.AddWithValue("@FechaDesde",
                    fechaDesde.HasValue ? (object)fechaDesde.Value.Date : DBNull.Value);

                    cmd.Parameters.AddWithValue("@FechaHasta",
                    fechaHasta.HasValue ? (object)fechaHasta.Value.Date.AddDays(1).AddSeconds(-1) : DBNull.Value);

                    DataTable dt = new DataTable();
                    using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historial del usuario: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }


    }
}