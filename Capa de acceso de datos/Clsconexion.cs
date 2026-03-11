using Microsoft.Data.SqlClient;
using System.Data;


namespace Capa_de_acceso_de_datos
{
    public class Clsconexion
    {
        //CADENA DE CONEXION SERVIDOR EN LA NUBE
        //string conexion = "Server=tcp:parroquiashn.database.windows.net,1433;Initial Catalog=BD_Arquidiocesis;Persist Security Info=False;User ID=ArquidiocesisAdmin;Password=IsaacEmanuel123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //string conexion = "Data Source=DESKTOP-NRLBBAA\\SQLEXPRESS;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//Isaac
        //<<<<<<< HEAD
        //     string conexion = "Data Source=EDWINRODRIGUEZ;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//AAAA
        //string conexion = "Data Source=LAPTOP-D4F8GK1K\\MSSQLSERVER01;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//Diego
        //=======
        //string conexion = "Data Source=LENOVO-AFCM\\SQLEXPRESS;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//AAAA
        //     string conexion = "Data Source=LAPTOP-D4F8GK1K\\MSSQLSERVER01;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//Diego
        //>>>>>>> new

        static string servidorLocal = Environment.MachineName + "\\SQLEXPRESS";
        string conexion = $"Data Source={servidorLocal};Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection sc = new();

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

        // --- MOTOR DE SINCRONIZACIÓN PRIVADO ---
        private void SincronizarConNube(SqlCommand cmd)
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