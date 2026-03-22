using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsReportes
    {
        /// <summary>
        /// The cn
        /// </summary>
        private readonly Clsconexion _cn = new Clsconexion();


        /// <summary>
        /// Obteners the estado resultados.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public DataSet ObtenerEstadoResultados(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataSet ds = new DataSet();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_EstadoResultados", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fechaInicio", desde.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", hasta.Date);
                    cmd.Parameters.AddWithValue("@idParroquia", parroquia_id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return ds;
        }
        public DataSet ObtenerLibroMayor(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataSet ds = new DataSet();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_LibroMayor", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fechaInicio", desde.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", hasta.Date);
                    cmd.Parameters.AddWithValue("@idParroquia", parroquia_id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Libro Mayor: " + ex.Message, ex);
            }
            finally
            {
                _cn.Cerrar();
            }

            return ds;
        }


        /// <summary>
        /// Obteners the ingresos por parroquia.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public DataTable ObtenerIngresosPorParroquia(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ReporteIngresos", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }




        /// <summary>
        /// Obteners the gastos por parroquia.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public DataTable ObtenerGastosPorParroquia(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ReporteGastos", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obteners the parroquias.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerParroquias()
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerParroquias", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obteners the nombre parroquia.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <returns></returns>
        public string ObtenerNombreParroquia(int parroquia_id)
        {
            string nombre = null;

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerNombreParroquia", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        nombre = result.ToString();
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return nombre;
        }

        /// <summary>
        /// Obteners the tipos reporte.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerTiposReporte()
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTiposReporte", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obteners the balance general.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="fecha_inicio">The fecha inicio.</param>
        /// <param name="fecha_corte">The fecha corte.</param>
        /// <returns></returns>
        public DataSet ObtenerBalanceGeneral(int parroquia_id, DateTime fecha_inicio, DateTime fecha_corte)
        {
            DataSet ds = new DataSet();
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_GenerarBalanceGeneral", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);
                    cmd.Parameters.AddWithValue("@FechaInicio", fecha_inicio.Date);
                    cmd.Parameters.AddWithValue("@FechaCorte", fecha_corte.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }
            return ds;
        }

        /// <summary>
        /// Obteners the datos curia.
        /// </summary>
        /// <param name="parroquiaId">The parroquia identifier.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public DataSet ObtenerDatosCuriaPorUsuario(int usuarioId, DateTime desde, DateTime hasta)
        {
            DataSet ds = new DataSet();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_ReporteCuria", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(ds);
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return ds;
        }


        /// <summary>
        /// Obteners the nombre sacerdote.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        /// <returns></returns>
        public string ObtenerNombreSacerdote(int usuarioId)
        {
            string nombreCompleto = "";
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerNombreSacerdote", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    using (SqlDataReader dr = _cn.EjecutarReaderYEnviar(cmd))
                    {
                        if (dr.Read())
                        {
                            nombreCompleto = dr["NombreCompleto"]?.ToString() ?? "";
                        }
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }
            return nombreCompleto;
        }
    }
}



