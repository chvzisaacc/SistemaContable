using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class PatidasDobles : Clsconexion
    {
        /// <summary>
        /// Cargars the partidas.
        /// </summary>
        /// <param name="id_transaccion">The identifier transaccion.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar las partidas: " + ex.Message</exception>
        public DataTable CargarPartidas(int id_transaccion)
        {
            DataTable dtPartidas = new DataTable();

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("SP_CargarPartidas", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id_Transaccion", id_transaccion);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dtPartidas);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las partidas: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return dtPartidas;
        }
    }
}
