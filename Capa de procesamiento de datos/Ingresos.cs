using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class DetalleIngreso
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
    public class ResultadoGuardado
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
    public class Ingresos : Clsconexion
    {
        /// <summary>
        /// Ingresars the ingresos.
        /// </summary>
        /// <param name="fecha">The fecha.</param>
        /// <param name="descripcion">The descripcion.</param>
        /// <param name="monto">The monto.</param>
        /// <param name="referencia">The referencia.</param>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="nombre">The nombre.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al ingresar la nueva transacción de tipo ingreso: " + ex.Message</exception>
        public int IngresarIngresos(DateTime fecha, string descripcion, decimal monto, int referencia, int usuario_id, int id_origen, string nombre)
        {
            int nuevaTransa = 0;
            try
            {
                DateTime fechaValidada = (fecha < new DateTime(1753, 1, 1) || fecha == DateTime.MinValue)
                                         ? DateTime.Now : fecha;

                using (SqlCommand command = new SqlCommand("IngresarIngresos"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@fecha_transaccion", SqlDbType.DateTime).Value = fechaValidada;
                    command.Parameters.Add("@descripcion", SqlDbType.NVarChar, 100).Value = descripcion ?? (object)DBNull.Value;
                    command.Parameters.Add("@monto_historico", SqlDbType.Decimal).Value = monto;
                    command.Parameters.Add("@Numero_de_Referencia", SqlDbType.Int).Value = referencia;
                    command.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuario_id;
                    command.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = id_origen;
                    command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = nombre ?? (object)DBNull.Value;

                    // EjecutarScalarYEnviar debe devolver el primer valor de la primera fila (nuestro SELECT)
                    var resultado = EjecutarScalarYEnviar(command);

                    if (resultado != null && int.TryParse(resultado.ToString(), out int idGenerado))
                    {
                        nuevaTransa = idGenerado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ingresar el ingreso: " + ex.Message, ex);
                nuevaTransa = 0;
            }

            return nuevaTransa;
        }

        /// <summary>
        /// Modificars the ingreso.
        /// </summary>
        /// <param name="id_transaccion">The identifier transaccion.</param>
        /// <param name="fecha">The fecha.</param>
        /// <param name="descripcion">The descripcion.</param>
        /// <param name="monto">The monto.</param>
        /// <param name="referencia">The referencia.</param>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="nombre">The nombre.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al modificar el ingreso: " + ex.Message</exception>
        public int ModificarIngreso(int id_transaccion, DateTime fecha, string descripcion, decimal monto, int referencia, int usuario_id, int id_origen, string nombre)
        {
            int filas_afectadas = 0;
            try
            {

                DateTime fechaValidada = fecha;
                if (fecha < new DateTime(1753, 1, 1) || fecha == DateTime.MinValue)
                {
                    fechaValidada = DateTime.Now;
                }

                Abrir();

                using (SqlCommand command = new SqlCommand("sp_ModificarIngresos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_transaccion", id_transaccion);
                    command.Parameters.AddWithValue("@fecha_transaccion", fechaValidada);
                    command.Parameters.AddWithValue("@descripcion", descripcion ?? "");
                    command.Parameters.AddWithValue("@monto_nuevo", monto);
                    command.Parameters.AddWithValue("@Numero_de_Referencia", referencia);
                    command.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@Id_Origen", id_origen);
                    command.Parameters.AddWithValue("@Nombre", nombre ?? "");

                    filas_afectadas = EjecutarScalarYEnviar(command);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el ingreso: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return filas_afectadas;
        }
    }
}