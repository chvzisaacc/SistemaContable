using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;
namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class Alerta : Clsconexion
    {
        /// <summary>
        /// Cargars the alerta.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar la alerta: " + ex.Message</exception>
        public DataTable CargarAlerta()
        {
            DataTable dt = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_MOSTRAR_TAREAS", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dt.Load(dr);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la alerta: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Modificars the configuracion alerta.
        /// </summary>
        /// <param name="diasLimite">The dias limite.</param>
        /// <param name="alarmaActiva">if set to <c>true</c> [alarma activa].</param>
        /// <exception cref="System.Exception">Error al modificar la configuración de la alerta: " + ex.Message</exception>
        public void ModificarConfiguracionAlerta(int diasLimite, bool alarmaActiva)
        {
            try
            {
                Abrir();
                using (SqlCommand command = new SqlCommand("SP_ModificarAlerta", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DiasLimite", diasLimite);
                    command.Parameters.AddWithValue("@AlarmaActiva", alarmaActiva);
                    // Ejecutamos el comando sin esperar un valor de retorno
                    EjecutarYEnviar(command);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la configuración de la alerta: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Mensajes the alerta.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al verificar la alerta: " + ex.Message</exception>
        public DataTable MensajeAlerta()
        {
            DataTable dtResultado = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand command = new SqlCommand("sp_ObtenerMensajeAlertaActiva", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = EjecutarReaderYEnviar(command))
                    {
                        dtResultado.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la alerta: " + ex.Message, ex);
            }
            finally
            {
                Cerrar(); // Cierra la conexión
            }
            return dtResultado;
        }
    }
}