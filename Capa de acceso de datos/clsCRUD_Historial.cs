using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona registro y consulta del historial de acciones y eventos del sistema.
    /// Registra inicios de sesión, actividades de usuarios, acciones contables y cambios realizados.
    /// Todos los métodos que registran datos disparan sincronización remota automática.
    /// </summary>
    public class clsCRUD_Historial
    {
        private Clsconexion conexion;

        public clsCRUD_Historial()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Obtiene historial de acciones con paginación y filtros ejecutando procedimiento sp_ObtenerHistorial.
        /// Parámetros: totalRegistros (output: cantidad total sin paginar), parroquia_id (filtro nullable),
        /// usuario_id (filtro nullable), fechaDesde/fechaHasta (rango de fechas nullable),
        /// pagina (página actual, default 1), tamanoPagina (registros por página, default 50).
        /// Retorna DataTable con acciones paginadas. Utiliza parámetro OUTPUT @TotalRegistros para contar total.
        /// Se utiliza para bitácora/auditoría general con búsquedas avanzadas y paginación.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
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
        /// Obtiene lista de usuarios pertenecientes a una parroquia ejecutando procedimiento sp_ObtenerUsuariosPorParroquia.
        /// Parámetro parroquia_id: nullable, si es null obtiene usuarios de todas las parroquias.
        /// Retorna DataTable con: usuario_id, nombre, correo, rol, estado, etc.
        /// Se utiliza para poblar ComboBox de selección de usuarios en filtros de búsqueda.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
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
        /// Registra inicio de sesión de usuario ejecutando procedimiento sp_RegistrarInicioSesion.
        /// Parámetro usuario_id: identifica el usuario que inicia sesión.
        /// Almacena: ID usuario, fecha, hora y dirección IP de conexión para auditoría.
        /// Dispara NotificarSP automáticamente para sincronizar evento de login con servidor remoto.
        /// </summary>
        public void RegistrarInicioSesion(int usuario_id)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_RegistrarInicioSesion", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario_id", usuario_id);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
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
        /// Obtiene historial de acciones específico para rol sacerdote ejecutando procedimiento sp_ObtenerHistorialSacerdote.
        /// Retorna DataTable con acciones registradas por sacerdotes: bautismos, matrimonios, misas, etc.
        /// Se utiliza en bitácora de sacerdotes para auditar actividades relacionadas a administración parroquial.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
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
        /// Registra actividad genérica de usuario ejecutando procedimiento sp_RegistrarActividad.
        /// Parámetros: usuario_id (quién realiza), modulo_id (en qué módulo: 1=Ingresos, 2=Gastos, etc),
        /// tarea (acción específica: "Crear", "Editar", "Eliminar"), descripcion (detalles adicionales).
        /// Almacena: usuario, módulo, tarea, descripción, fecha y hora automáticas.
        /// Se utiliza para auditoría detallada de acciones en cada módulo del sistema.
        /// Dispara NotificarSP automáticamente para sincronizar evento con servidor remoto.
        /// </summary>
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

                    conexion.EjecutarYEnviar(cmd, sincronizar: true);
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
        /// Registra acción de usuario con información financiera ejecutando procedimiento sp_RegistrarAccionUsuario.
        /// Parámetros: usuario_id (quién realiza), modulo (nombre módulo: "Ingresos", "Gastos", "Bancos"),
        /// accion (tipo: "Crear", "Editar", "Transferencia"), monto (nullable, para acciones monetarias),
        /// descripcion (detalles adicionales del evento).
        /// Almacena: usuario, módulo, acción, monto opcional, descripción, fecha y hora automáticas.
        /// Se utiliza para auditoría con contexto financiero (ingresos, egresos, transferencias).
        /// No dispara sincronización por defecto (sin parámetro sincronizar).
        /// </summary>
        public void RegistrarAccionUsuario(int usuario_id, string modulo, string accion, string descripcion)
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
        /// Obtiene historial de todas las acciones registradas por usuario específico ejecutando procedimiento sp_ObtenerHistorialUsuario.
        /// Parámetros: usuario_id (obligatorio), fechaDesde (nullable, si no se especifica desde el inicio),
        /// fechaHasta (nullable con suma de 23:59:59 para incluir todo el día).
        /// Retorna DataTable con: acción, módulo, fecha, descripción, monto (si aplica), etc.
        /// Se utiliza para auditoría individual: ver qué hizo específicamente un usuario en rango de fechas.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
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