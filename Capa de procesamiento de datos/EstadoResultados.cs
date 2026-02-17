using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;

namespace Capa_de_procesamiento_de_datos
{

    public class EstadoResultados : Clsconexion
    {
        /// <summary>
        /// Obtiene el Estado de Resultados por parroquia y rango de fechas
        /// </summary>
        /// <param name="parroquia_id">Id de la parroquia</param>
        /// <param name="desde">Fecha inicio</param>
        /// <param name="hasta">Fecha fin</param>
        /// <returns>DataSet con Totales y Detalle</returns>
        public DataSet ObtenerEstadoResultados(
            int parroquia_id,
            DateTime desde,
            DateTime hasta)
        {
            DataSet resultado = new DataSet();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_EstadoResultados", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fechaInicio", desde);
                    cmd.Parameters.AddWithValue("@fechaFin", hasta);
                    cmd.Parameters.AddWithValue("@idParroquia", parroquia_id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Estado de Resultados: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }
    }
}
