using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;


namespace Capa_de_procesamiento_de_datos
{
    public class LibroMayor : Clsconexion
    {
        /// <summary>
        /// Obtiene el Libro Mayor por parroquia y rango de fechas
        /// </summary>
        public DataTable ObtenerLibroMayor(
            int parroquia_id,
            DateTime desde,
            DateTime hasta)
        {
            DataTable resultado = new DataTable();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_LibroMayor", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fechaInicio", desde.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", hasta.Date);
                    cmd.Parameters.AddWithValue("@idParroquia", parroquia_id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Libro Mayor: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }
    }
}
