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

        public DataTable ObtenerGastosPorParroquia(int parroquiaId, DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ReporteGastos", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);
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
                _cn.Abrir();   // asumiendo que ya tienes: private readonly Clsconexion _cn = new Clsconexion();

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

        public string ObtenerNombreParroquia(int parroquiaId)
        {
            string nombre = null;

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerNombreParroquia", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);

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
    }
}

