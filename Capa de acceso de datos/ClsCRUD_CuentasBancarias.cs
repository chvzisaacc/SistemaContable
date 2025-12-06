using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsCRUD_CuentasBancarias
    {
        /// <summary>
        /// The conexion
        /// </summary>
        private Clsconexion conexion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClsCRUD_CuentasBancarias"/> class.
        /// </summary>
        public ClsCRUD_CuentasBancarias()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Agregars the cuenta bancaria.
        /// </summary>
        /// <param name="cuenta_bancaria_id">The cuenta bancaria identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="saldo">The saldo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al agregar cuenta bancaria: " + ex.Message</exception>
        public int AgregarCuentaBancaria(int cuenta_bancaria_id, string nombre, decimal? saldo)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Origen", cuenta_bancaria_id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@saldo", saldo);
                ;

                SqlParameter nuevoid = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevoid.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevoid);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(nuevoid.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cuenta bancaria: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }

        }

        /// <summary>
        /// Obteners the cuentas bancarias.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener Cuentas Bancarias: " + ex.Message</exception>
        public DataTable ObtenerCuentasBancarias()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasBancarias", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Cuentas Bancarias: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerCuentaPorId(int idOrigen)
        {
            // Usamos el objeto DataTable para almacenar los resultados
            DataTable dt = new DataTable();
            try
            {
                conexion.Abrir();

                // 1. Definición del comando: Indica el SP y la conexión
                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentaBancariaPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                // 2. Añadir el parámetro requerido por el Stored Procedure
                // El nombre del parámetro debe coincidir con el del SP
                cmd.Parameters.AddWithValue("@Id_Origen", idOrigen);

                // 3. Adaptador para llenar el DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // 4. Llenar el DataTable con los resultados del SP
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cuenta bancaria por ID: " + ex.Message, ex);
            }
            finally
            {
                // Asegura que la conexión se cierre después de su uso
                conexion.Cerrar();
            }

            return dt;
        }

        // ... (Tu método ModificarSaldo)

        /// <summary>
        /// Modificars the saldo.
        /// </summary>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="saldo">The saldo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al modificar saldo: " + ex.Message</exception>
        public bool ModificarSaldo(int id_origen, decimal saldo)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("sp_ModificarSaldo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = id_origen;

                var psaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                psaldo.Precision = 10;
                psaldo.Scale = 2;
                psaldo.Value = saldo;

                int filas = cmd.ExecuteNonQuery(); // esperado: 1 si actualiza una fila
                return filas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar saldo: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
        /// <summary>
        /// Agregars the saldo.
        /// </summary>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="monto">The monto.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al agregar saldo: " + ex.Message</exception>
        public bool AgregarSaldo(int id_origen, decimal monto)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_AgregarSaldo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = id_origen;

                var pmonto = cmd.Parameters.Add("@monto", SqlDbType.Decimal);
                pmonto.Precision = 10;
                pmonto.Scale = 2;
                pmonto.Value = monto;

                int filas = cmd.ExecuteNonQuery();   // esperado: 1 si actualiza una fila
                return filas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar saldo: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
        /// <summary>
        /// Crears the cuenta banco.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <param name="saldo">The saldo.</param>
        /// <param name="nuevo_id">The nuevo identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al crear cuenta bancaria: " + ex.Message</exception>
        public bool CrearCuentaBanco(String nombre, decimal saldo, out int nuevo_id)
        {
            nuevo_id = 0;
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@Id_cuentaBanco", SqlDbType.Int).Value = idCuentaBanco;
                var pNombre = cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = nombre ?? string.Empty;
                //pNombre.Value = Nombre ?? string.Empty;

                var pSaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                pSaldo.Precision = 10;
                pSaldo.Scale = 2;
                pSaldo.Value = saldo;

                var pOut = cmd.Parameters.Add("@nuevo_Id", SqlDbType.Int);
                pOut.Direction = ParameterDirection.Output;

                int filas = cmd.ExecuteNonQuery();
                if (pOut.Value != DBNull.Value && (int)pOut.Value > 0)
                {
                    nuevo_id = (int)pOut.Value;

                    return true;
                }
                else
                {
                    return false; // no hubo id generad
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear cuenta bancaria: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
