using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    public class Clsconexion
    {
        static string conexion = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
        public SqlConnection sc = new SqlConnection(conexion);
        public Clsconexion() => sc.ConnectionString = conexion;

        // Evento que dispara cuando se ejecuta un SP
        // La Capa de Presentación se suscribe a esto
        public static event Action<string, Dictionary<string, object>>? OnSpEjecutado;

        public void EjecutarYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                cmd.ExecuteNonQuery();
                if (sincronizar) NotificarSP(cmd);
            }
            finally { Cerrar(); }
        }

        public SqlDataReader EjecutarReaderYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            Abrir();
            cmd.Connection = sc;
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sincronizar) NotificarSP(cmd);
            return reader;
        }

        public int EjecutarScalarYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                object res = cmd.ExecuteScalar();
                if (sincronizar) NotificarSP(cmd);
                return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
            }
            finally { Cerrar(); }
        }

        public DataTable EjecutarAdapterYEnviar(SqlCommand cmd, bool sincronizar = false)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (sincronizar) NotificarSP(cmd);
                return dt;
            }
            finally { Cerrar(); }
        }

        // se dispara el evento
        private void NotificarSP(SqlCommand cmd)
        {
            if (OnSpEjecutado == null) return;

            string nombreSp = cmd.CommandText;
            var parametros = new Dictionary<string, object>();
            foreach (SqlParameter p in cmd.Parameters)
            {
                string key = p.ParameterName.Replace("@", "");
                object valor = (p.Value == null || p.Value == DBNull.Value) ? null : p.Value;
                parametros[key] = valor;
            }

            if (!parametros.ContainsKey("_ParroquiaId"))
                parametros["_ParroquiaId"] = Sesion1.id_parroquia;

            _ = Task.Run(() => OnSpEjecutado?.Invoke(nombreSp, parametros));
        }

        public void Abrir() { if (sc.State == ConnectionState.Closed) sc.Open(); }
        public void Cerrar() { if (sc.State == ConnectionState.Open) sc.Close(); }
    }
}