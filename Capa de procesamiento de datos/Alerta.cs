using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    // Gestión de alertas de tareas; hereda la conexión a BD desde Clsconexion
    public class Alerta : Clsconexion
    {
        /// <summary>Obtiene todas las tareas para evaluar alertas.</summary>
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

        /// <summary>Actualiza los días límite y si la alarma está activa.</summary>
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
                    // sincronizar: true propaga el cambio a otros contextos activos
                    EjecutarYEnviar(command, sincronizar: true);
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

        /// <summary>Retorna el mensaje de la alerta activa, si existe.</summary>
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
                Cerrar();
            }
            return dtResultado;
        }
    }
}