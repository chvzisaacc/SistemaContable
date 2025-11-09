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
        public int IngresarGastos(DateTime fechaTransaccion, string descripcion, decimal monto, int referencia, int idUsuario, int idOrigenNuevo, string nombreCuenta)
        {
            int nuevaTransaccion = 0;

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("ingresargastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fecha_transaccion", fechaTransaccion);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_historico", monto);
                    command.Parameters.AddWithValue("@numero_de_referencia", referencia);
                    command.Parameters.AddWithValue("@usuario_id", idUsuario);
                    command.Parameters.AddWithValue("@id_origen", idOrigenNuevo);
                    command.Parameters.AddWithValue("@nombre", nombreCuenta);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        nuevaTransaccion = Convert.ToInt32(result);
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

            return nuevaTransaccion;
        }


        public int ModificarGastos(int idTransaccion, DateTime fecha, string descripcion, decimal monto, int referencia, int usuarioId, int idOrigen, string nombre)
        {
            int filasAfectadas = 0;
            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("sp_ModificarGastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_transaccion", idTransaccion);

                    command.Parameters.AddWithValue("@fecha_transaccion", fecha);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_nuevo", monto);
                    command.Parameters.AddWithValue("@Numero_de_Referencia", referencia);
                    command.Parameters.AddWithValue("@Usuario_id", usuarioId);
                    command.Parameters.AddWithValue("@Id_Origen", idOrigen);
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                        filasAfectadas = count;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el gasto: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return filasAfectadas;

        }
    }
}
