using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona operaciones CRUD de cuentas bancarias: creación, modificación, consulta y gestión de saldos.
    /// Todos los métodos que modifican datos disparan sincronización remota automática.
    /// Las cuentas bancarias son fuentes de fondos donde se registran ingresos y egresos.
    /// </summary>
    public class ClsCRUD_CuentasBancarias
    {
        private Clsconexion conexion;

        public ClsCRUD_CuentasBancarias()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Agrega nueva cuenta bancaria ejecutando procedimiento sp_AgregarCuentaBanco.
        /// Parámetros: cuenta_bancaria_id (ID origen), nombre (identificación), saldo (inicial nullable).
        /// Retorna ID autogenerado de la nueva cuenta para referencias futuras.
        /// Utiliza parámetro OUTPUT @nuevoId para obtener ID generado automáticamente.
        /// Dispara NotificarSP automáticamente para sincronizar con servidor remoto.
        /// </summary>
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

                SqlParameter nuevoid = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevoid.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevoid);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);

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
        /// Obtiene todas las cuentas bancarias de una parroquia ejecutando procedimiento sp_ObtenerCuentasBancarias.
        /// Parámetro parroquiaId: filtra cuentas pertenecientes a la parroquia especificada.
        /// Retorna DataTable con: ID, nombre, saldo, tipo de cuenta, estado, etc.
        /// Se utiliza para poblar controles de selección en formularios de transacciones.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCuentasBancarias(int parroquiaId)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasBancarias", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);

                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }

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
        /// Modifica el saldo de una cuenta bancaria ejecutando procedimiento sp_ModificarSaldo.
        /// Parámetros: id_origen (identifica cuenta), saldo (nuevo valor con precisión 18.2).
        /// Retorna true si modificación fue exitosa, false si ocurre error.
        /// Utiliza parámetro con precisión y escala específicas (18.2) para exactitud financiera.
        /// Dispara NotificarSP automáticamente para sincronizar cambio con servidor remoto.
        /// </summary>
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

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
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
        /// Incrementa el saldo de una cuenta bancaria ejecutando procedimiento sp_AgregarSaldo.
        /// Parámetros: id_origen (identifica cuenta), monto (cantidad a sumar con precisión 18.2), usuarioId (auditoría).
        /// Retorna true si incremento fue exitoso, false si ocurre error.
        /// Utiliza parámetro con precisión y escala específicas (18.2) para exactitud financiera.
        /// Se utiliza para registrar ingresos, depósitos y transferencias entrantes.
        /// Dispara NotificarSP automáticamente para sincronizar cambio con servidor remoto.
        /// </summary>
        public bool AgregarSaldo(int id_origen, decimal monto, int usuarioId)
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

                cmd.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuarioId;

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
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
        /// Crea nueva cuenta bancaria completa ejecutando procedimiento sp_AgregarCuentaBanco.
        /// Parámetros: nombre (identificación), saldo (inicial con precisión 18.2), idTipo (2=Ahorro, 3=Cheque, etc),
        /// parroquiaId (propietaria), out nuevo_id (ID autogenerado por procedimiento).
        /// Retorna true si creación fue exitosa y nuevo_id > 0, false si ocurre error.
        /// Valida que saldo sea ingresado como string.Empty si está vacío.
        /// Utiliza parámetro OUTPUT @nuevo_Id para retornar ID generado automáticamente.
        /// Dispara NotificarSP automáticamente para sincronizar con servidor remoto.
        /// </summary>
        public bool CrearCuentaBanco(string nombre, decimal saldo, int idTipo, int parroquiaId, out int nuevo_id)
        {
            nuevo_id = 0;
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_AgregarCuentaBanco", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = nombre ?? string.Empty;

                var pSaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                pSaldo.Precision = 18;
                pSaldo.Scale = 2;
                pSaldo.Value = saldo;

                cmd.Parameters.Add("@IdOrigenTipo", SqlDbType.Int).Value = idTipo;
                cmd.Parameters.Add("@Parroquia_ID", SqlDbType.Int).Value = parroquiaId;

                var pOut = cmd.Parameters.Add("@nuevo_Id", SqlDbType.Int);
                pOut.Direction = ParameterDirection.Output;

                conexion.EjecutarYEnviar(cmd, sincronizar: true);

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

        /// <summary>
        /// Modifica cuenta bancaria existente ejecutando procedimiento sp_ModificarOrigenFuente.
        /// Parámetros: id_origen (identifica registro), nombre (nuevo), saldo (nuevo con precisión 18.2),
        /// idTipo (tipo nuevo: 2=Ahorro, 3=Cheque, etc), parroquiaId (propietaria).
        /// Retorna true si modificación fue exitosa, false si ocurre error.
        /// Utiliza parámetros con precisión y escala específicas (18.2) para exactitud financiera.
        /// Valida que nombre sea ingresado como string.Empty si está vacío.
        /// Dispara NotificarSP automáticamente para sincronizar cambios con servidor remoto.
        /// </summary>
        public bool ModificarCuentaBanco(int id_origen, string nombre, decimal saldo, int idTipo, int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("dbo.sp_ModificarOrigenFuente", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Origen", SqlDbType.Int).Value = id_origen;
                cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = nombre ?? string.Empty;

                var pSaldo = cmd.Parameters.Add("@saldo", SqlDbType.Decimal);
                pSaldo.Precision = 18;
                pSaldo.Scale = 2;
                pSaldo.Value = saldo;

                cmd.Parameters.Add("@IdOrigenTipo", SqlDbType.Int).Value = idTipo;
                cmd.Parameters.Add("@Parroquia_ID", SqlDbType.Int).Value = parroquiaId;

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cuenta bancaria: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene fecha del último ingreso registrado en una cuenta ejecutando procedimiento sp_ObtenerFechaUltimoIngreso.
        /// Parámetros: parroquiaId (filtra por parroquia), nombreCuenta (identifica cuenta específica).
        /// Retorna DateTime con fecha del último movimiento entrante, o DateTime.MinValue si no hay registros.
        /// Se utiliza para validar periodicidad de ingresos y controles de reconciliación.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DateTime ObtenerFechaUltimoIngreso(int parroquiaId, string nombreCuenta)
        {
            try
            {
                conexion.Abrir();
                using var cmd = new SqlCommand("sp_ObtenerFechaUltimoIngreso", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Parroquia_ID", SqlDbType.Int).Value = parroquiaId;
                cmd.Parameters.Add("@NombreCuenta", SqlDbType.NVarChar).Value = nombreCuenta;

                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDateTime(result);
                }

                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener fecha de validación: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}