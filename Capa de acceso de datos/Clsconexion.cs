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

        // 1. PARA INSERT, UPDATE, DELETE (No devuelven datos)
        public void EjecutarYEnviar(SqlCommand cmd)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                cmd.ExecuteNonQuery();
                SincronizarConNube(cmd); // Notificación asíncrona
            }
            finally { Cerrar(); }
        }

        // 2. PARA LOGINS O CONSULTAS (Devuelven filas)
        public SqlDataReader EjecutarReaderYEnviar(SqlCommand cmd)
        {
            Abrir();
            cmd.Connection = sc;
            // CommandBehavior.CloseConnection cierra la conexión automáticamente al cerrar el reader
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SincronizarConNube(cmd);
            return reader;
        }

        // 3. PARA OBTENER IDs (Devuelven un solo valor)
        public int EjecutarScalarYEnviar(SqlCommand cmd)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;
                object res = cmd.ExecuteScalar();
                SincronizarConNube(cmd);
                return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
            }
            finally { Cerrar(); }
        }
        public DataTable EjecutarAdapterYEnviar(SqlCommand cmd)
        {
            try
            {
                Abrir();
                cmd.Connection = sc;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                SincronizarConNube(cmd);

                return dt;
            }
            finally
            {
                Cerrar();
            }
        }
        // --- MOTOR DE SINCRONIZACIÓN PRIVADO ---
        protected void SincronizarConNube(SqlCommand cmd)
        {
            string nombreSp = cmd.CommandText;
            var parametros = new Dictionary<string, object>();

            foreach (SqlParameter p in cmd.Parameters)
            {
                parametros.Add(p.ParameterName.Replace("@", ""), p.Value ?? DBNull.Value);
            }

            // Se dispara en segundo plano. No detiene el flujo local.
            _ = Task.Run(() => AccesoRemoto.EjecutarSpRemoto(nombreSp, parametros));
        }

        public void Abrir() { if (sc.State == ConnectionState.Closed) sc.Open(); }
        public void Cerrar() { if (sc.State == ConnectionState.Open) sc.Close(); }
    }
}