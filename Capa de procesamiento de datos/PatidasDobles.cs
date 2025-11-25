using Capa_de_acceso_de_datos;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Capa_de_procesamiento_de_datos
{
    public class PatidasDobles : Clsconexion
    {
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
