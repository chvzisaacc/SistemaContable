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
        public DataTable ObtenerCuentasBancarias(int parroquiaId)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasBancarias", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);

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
                psaldo.Precision = 18;
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
                pmonto.Precision = 18;
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
        public bool CrearCuentaBanco(string nombre, decimal saldo, int idTipo, int parroquiaId, 
                             int? idCuentaCatalogo, out int nuevo_id)
        {
            nuevo_id = 0;
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros existentes
                cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = nombre ?? string.Empty;

                var pSaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                pSaldo.Precision = 18;
                pSaldo.Scale = 2;
                pSaldo.Value = saldo;

                //Parámetro para el tipo de cuenta (2 = Ahorro, 3 = Cheque, etc.)
                cmd.Parameters.Add("@IdOrigenTipo", SqlDbType.Int).Value = idTipo;

                //Parámetro para la Parroquia actual
                cmd.Parameters.Add("@Parroquia_ID", SqlDbType.Int).Value = parroquiaId;

                // ← NUEVO: mapeo con catalogo (puede ser null si no se selecciona)
                cmd.Parameters.Add("@id_cuenta_catalogo", SqlDbType.Int).Value =
                    idCuentaCatalogo.HasValue ? (object)idCuentaCatalogo.Value : DBNull.Value;

                // Parámetro de salida
                var pOut = cmd.Parameters.Add("@nuevo_Id", SqlDbType.Int);
                pOut.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                if (pOut.Value != DBNull.Value && (int)pOut.Value > 0)
                {
                    nuevo_id = (int)pOut.Value;
                    return true;
                }

                return false;
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

        public DataTable ObtenerCuentasCatalogoParaMapeo(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("sp_ObtenerCuentasCatalogoParaMapeo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Parroquia_ID", SqlDbType.Int).Value = parroquiaId;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cuentas del catálogo: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

    }
}
