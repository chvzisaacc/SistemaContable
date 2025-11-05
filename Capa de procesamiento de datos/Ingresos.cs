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
    }
}
