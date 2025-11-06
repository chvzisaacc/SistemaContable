using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_procesamiento_de_datos
{
    public class DetalleIngreso
    {
        public string NombreCuenta { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

    }

    public class ResultadoGuardado
    {
        public int FilasGuardadas { get; set; }
        public bool HuboError { get; set; }
        public string Mensaje { get; set; }
    }

    public class Ingresos : Clsconexion
    {
        public int IngresarIngresos(DateTime fecha, string descripcion, decimal monto, string referencia, int usuarioId, int idOrigen, string nombre)
        {
            int nuevaTransa = 0;
            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("IngresarIngresos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fecha_transaccion", fecha);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_historico", monto);
                    command.Parameters.AddWithValue("@Numero_de_Referencia", referencia);
                    command.Parameters.AddWithValue("@Usuario_id", usuarioId);
                    command.Parameters.AddWithValue("@Id_Origen", idOrigen);
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nuevaTransa = Convert.ToInt32(result);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ingresar la nueva transacción de tipo ingreso: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return nuevaTransa;
        }

        public DataTable CargarTransaccionesActivas()
        {
            DataTable dt = new DataTable();
            try
            {
                Abrir();
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerTransaccionesActivas", sc))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las transacciones activas: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return dt;
        }
    }
}


    




