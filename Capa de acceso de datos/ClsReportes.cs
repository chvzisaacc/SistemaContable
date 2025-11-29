using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class ClsReportes
    {
        private readonly Clsconexion _cn = new Clsconexion();

        
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

        public DataTable ObtenerBalanceGeneral(int parroquia_id, DateTime fecha_inicio, DateTime fecha_corte)
        {
            DataTable dt = new DataTable();
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_GenerarBalanceGeneral", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id); // Si usas parroquias
                    cmd.Parameters.AddWithValue("@FechaInicio", fecha_inicio.Date);
                    cmd.Parameters.AddWithValue("@FechaCorte", fecha_corte.Date);

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
    }

}



