using Microsoft.Data.SqlClient;
using System.Data;


namespace Capa_de_acceso_de_datos
{
    public class Clsconexion
    {
        //CADENA DE CONEXION SERVIDOR EN LA NUBE - NO TOCAR
        //string conexion = "Server=tcp:parroquiashn.database.windows.net,1433;Initial Catalog=BD_Arquidiocesis;Persist Security Info=False;User ID=ArquidiocesisAdmin;Password=IsaacEmanuel123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //BASE DE DATOS LOCAL - CAMBIAR SEGUN EL USUARIO
        string conexion = "Data Source=DESKTOP-NRLBBAA\\SQLEXPRESS;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//Isaac
        //string conexion = "Data Source=LENOVO-AFCM\\SQLEXPRESS;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//AAAA
         //string conexion = "Data Source=LAPTOP-D4F8GK1K\\MSSQLSERVER01;Initial Catalog=BASE DE SISTEMA - LOCAL;Integrated Security=True;TrustServerCertificate=True;";//Diego

        public SqlConnection sc = new();

        public Clsconexion()
        {
            sc.ConnectionString = conexion;
        }
        public void Abrir()
        {
            try
            {
                if (sc.State == ConnectionState.Closed)
                    sc.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la conexión: " + ex.Message, ex);
            }


        }
        public void Cerrar()
        {
            try
            {
                if (sc.State == ConnectionState.Open)
                {
                    sc.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cerrar la conexión: " + ex.Message, ex);
            }
        }

    }
}

