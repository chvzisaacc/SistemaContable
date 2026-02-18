using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class ClsCapitalInicial
    {
        private readonly Clsconexion _cn = new Clsconexion();

        public bool IngresarCapitalInicial(int usuarioId, int parroquiaId, decimal monto)
        {
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_IngresarCapitalInicial", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);
                    cmd.Parameters.AddWithValue("@Monto", monto);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            finally
            {
                _cn.Cerrar();
            }
        }
        public bool TieneCapitalInicial(int parroquiaId)
        {
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_TieneCapitalInicial", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                            return Convert.ToBoolean(dt.Rows[0]["TieneCapital"]);

                        return false;
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }
        }

        public decimal ObtenerCapitalInicial(int parroquiaId)
        {
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_TieneCapitalInicial", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                            return Convert.ToDecimal(dt.Rows[0]["CapitalInicial"]);

                        return 0;
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }
        }
    }
}
