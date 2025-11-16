using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_procesamiento_de_datos
{
    public class PatidasDobles:Clsconexion
    {
        public DataTable CargarPartidas(int idTransaccion)
        {
            DataTable dtPartidas = new DataTable();

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("SP_CargarPartidas", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id_Transaccion", idTransaccion);

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
