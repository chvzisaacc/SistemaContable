using System.Data;
using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;

namespace Capa_de_procesamiento_de_datos
{
    // Estado de Resultados (ingresos vs egresos) por parroquia y período
    public class EstadoResultados : Clsconexion
    {
        /// <summary>Retorna un DataSet con totales y detalle del Estado de Resultados.</summary>
        public DataSet ObtenerEstadoResultados(int parroquia_id, DateTime desde, DateTime hasta)
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
                    // SqlDataAdapter llena varias tablas del SP en un solo viaje a la BD
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