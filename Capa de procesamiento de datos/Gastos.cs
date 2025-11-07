using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;

namespace Capa_de_procesamiento_de_datos
{

    public class Detalle_gasto {
        public string NombreCuenta { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

    }
    public class Resultado
    {
        public int FilasGuardadas { get; set; }
        public bool HuboError { get; set; }
        public string Mensaje { get; set; }
    }

    public class Gastos : Clsconexion
    {
        public int IngresarGastos(DateTime fecha, string descripcion, decimal monto, string referencia, int usuarioId, int idOrigen, string nombre)
        {
            int nuevaTransa = 0;
            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("ingresargastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fecha_transaccion", fecha);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_historico", monto);
                    command.Parameters.AddWithValue("@numero_de_referencia", referencia);
                    command.Parameters.AddWithValue("@usuario_id", usuarioId);
                    command.Parameters.AddWithValue("@id_origen", idOrigen);
                    command.Parameters.AddWithValue("@nombre", nombre);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nuevaTransa = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ingresar el gasto: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return nuevaTransa;
        }
    }
}
