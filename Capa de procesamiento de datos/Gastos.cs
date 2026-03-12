using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{

    /// <summary>
    /// 
    /// </summary>
    public class Detalle_gasto
    {
        /// <summary>
        /// Gets or sets the nombre cuenta.
        /// </summary>
        /// <value>
        /// The nombre cuenta.
        /// </value>
        public string nombre_cuenta { get; set; }
        /// <summary>
        /// Gets or sets the descripcion.
        /// </summary>
        /// <value>
        /// The descripcion.
        /// </value>
        public string descripcion { get; set; }
        /// <summary>
        /// Gets or sets the monto.
        /// </summary>
        /// <value>
        /// The monto.
        /// </value>
        public decimal monto { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class Resultado
    {
        /// <summary>
        /// Gets or sets the filas guardadas.
        /// </summary>
        /// <value>
        /// The filas guardadas.
        /// </value>
        public int filas_guardadas { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [hubo error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hubo error]; otherwise, <c>false</c>.
        /// </value>
        public bool hubo_error { get; set; }
        /// <summary>
        /// Gets or sets the mensaje.
        /// </summary>
        /// <value>
        /// The mensaje.
        /// </value>
        public string mensaje { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class Gastos : Clsconexion
    {
        /// <summary>
        /// Ingresars the gastos.
        /// </summary>
        /// <param name="fecha_transaccion">The fecha transaccion.</param>
        /// <param name="descripcion">The descripcion.</param>
        /// <param name="monto">The monto.</param>
        /// <param name="referencia">The referencia.</param>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <param name="id_origen_nuevo">The identifier origen nuevo.</param>
        /// <param name="nombre_cuenta">The nombre cuenta.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al ingresar el gasto: " + ex.Message</exception>
        public int IngresarGastos(DateTime fecha_transaccion, string descripcion, decimal monto, int referencia, int idUsuario, int id_origen_nuevo, string nombre_cuenta, int parroquiaId, int? id_cuenta_destino = null)
        {
            int nuevaTransaccion = 0;

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("ingresargastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fecha_transaccion", fecha_transaccion);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_historico", monto);
                    command.Parameters.AddWithValue("@numero_de_referencia", referencia);
                    command.Parameters.AddWithValue("@usuario_id", idUsuario);
                    command.Parameters.AddWithValue("@id_origen", id_origen_nuevo);
                    command.Parameters.AddWithValue("@nombre", nombre_cuenta);
                    command.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);
                    command.Parameters.AddWithValue("@id_cuenta_destino", id_cuenta_destino.HasValue ? (object)id_cuenta_destino.Value : DBNull.Value);  // ← NUEVO


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


        /// <summary>
        /// Modificars the gastos.
        /// </summary>
        /// <param name="id_tansaccion">The identifier tansaccion.</param>
        /// <param name="fecha">The fecha.</param>
        /// <param name="descripcion">The descripcion.</param>
        /// <param name="monto">The monto.</param>
        /// <param name="referencia">The referencia.</param>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="nombre">The nombre.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al modificar el gasto: " + ex.Message</exception>
        public int ModificarGastos(int id_tansaccion, DateTime fecha, string descripcion, decimal monto, int referencia, int usuario_id, int id_origen, string nombre)
        {
            int filas_afectadas = 0;
            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("sp_ModificarGastos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_transaccion", id_tansaccion);

                    command.Parameters.AddWithValue("@fecha_transaccion", fecha);
                    command.Parameters.AddWithValue("@descripcion", descripcion);
                    command.Parameters.AddWithValue("@monto_nuevo", monto);
                    command.Parameters.AddWithValue("@Numero_de_Referencia", referencia);
                    command.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@Id_Origen", id_origen);
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                        filas_afectadas = count;
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

            return filas_afectadas;

        }
    }
}
