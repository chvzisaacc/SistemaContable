using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Capa_de_acceso_de_datos
{
    public class Clsconexion
    {
        //string conexion = "Data Source=LENOVO-AFCM\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;"; raphinha
        //string conexion = "Data Source=LPFABY\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";//Luis
       // string conexion = "Data Source=EDWINRODRIGUEZ\\SQLEXPRESS;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";
        string conexion = "Data Source=LAPTOP-D4F8GK1K\\MSSQLSERVER01;Initial Catalog=BD_Arquidiocesis;Integrated Security=True;TrustServerCertificate=True;";

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

