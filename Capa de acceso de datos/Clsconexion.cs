using Microsoft.Data.SqlClient;
using System.Data;


namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class Clsconexion
    {
        //string conexion = "Data Source=LENOVO-AFCM\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";
        //string conexion = "Data Source=LPFABY\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//Luis
        /// <summary>
        /// The conexion
        /// </summary>
        string conexion = "Data Source=DESKTOP-NRLBBAA\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//Isaac
                                                                                                                                                           //string conexion = "Data Source=LAPTOP-D4F8GK1K\\MSSQLSERVER01;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//Diego
                                                                                                                                                           //string conexion = "Data Source=DESKTOP-NRLBBAA\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//Isaac
                                                                                                                                                           //string conexion = "Data Source=LAPTOP-D4F8GK1K\\MSSQLSERVER01;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//Diego

        //string conexion = "Data Source=EDWINRODRIGUEZ;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//EDWIN NO LA BORREN

        /// <summary>
        /// The sc
        /// </summary>
        public SqlConnection sc = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Clsconexion"/> class.
        /// </summary>
        public Clsconexion()
        {
            sc.ConnectionString = conexion;
        }
        /// <summary>
        /// Abrirs this instance.
        /// </summary>
        /// <exception cref="System.Exception">Error al abrir la conexión: " + ex.Message</exception>
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
        /// <summary>
        /// Cerrars this instance.
        /// </summary>
        /// <exception cref="System.Exception">Error al cerrar la conexión: " + ex.Message</exception>
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

