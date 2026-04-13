using Capa_de_acceso_de_datos;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    //La clase herada de clsConexion para la logica de apertura/cierra
    //de conexion
    public class PatidasDobles : Clsconexion
    {
        public DataTable CargarPartidas(int id_transaccion)
        {
            DataTable dtPartidas = new DataTable();

            try
            {
                //abre la conexion heredada de ClsConexion
                Abrir();

                //se usa 'using' para garantizar que SqlComman libere sus recursos
                using (SqlCommand command = new SqlCommand("SP_CargarPartidas", sc))
                {
                    //Indica que el comando es un Procedimiento Almacenado y no un Query
                    command.CommandType = CommandType.StoredProcedure;

                    //
                    command.Parameters.AddWithValue("@Id_Transaccion", id_transaccion);

                    //SqlAdapter ejecuta el comando y llena el DataTable 
                    //using asegura la liberacion del adapter despues el fill
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dtPartidas);
                    }
                }
            }
            catch (Exception ex)
            {
                //Se re-lanza una expecion con mensaje descriptivo
                throw new Exception("Error al cargar las partidas: " + ex.Message, ex);
            }
            finally
            {
                //Se ejecuta SIEMPRE para cerrar la conexion.
                Cerrar();
            }

            //Se retorna el data table
            return dtPartidas;
        }
    }
}
