using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Capa_de_procesamiento_de_datos
{
    public class Alerta:Clsconexion
    {
        public DataTable CargarAlerta()
        {
            DataTable dt = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_MOSTRAR_TAREAS", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
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
                    command.ExecuteNonQuery();
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

        public DataTable MensajeAlerta()
        {
            DataTable dtResultado = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand command = new SqlCommand("sp_ObtenerMensajeAlertaActiva", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dtResultado);
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
