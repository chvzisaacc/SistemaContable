using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona operaciones de transferencia de dinero desde cuentas bancarias a la caja chica.
    /// Obtiene información disponible, saldos y ejecuta transferencias con sincronización automática.
    /// La caja chica es el fondo de efectivo para gastos menores de la parroquia.
    /// </summary>
    public class clsEnviarACajaChica
    {
        private Clsconexion conexion = new Clsconexion();

        /// <summary>
        /// Obtiene cuentas bancarias disponibles para realizar transferencias a caja chica ejecutando procedimiento sp_ObtenerCuentasDisponiblesCajaChica.
        /// Parámetro parroquiaId: filtra cuentas pertenecientes a la parroquia especificada.
        /// Retorna DataTable con: cuenta_id, nombre_cuenta, saldo_disponible, tipo_cuenta, estado.
        /// Se utiliza para poblar ComboBox en formularios de transferencia a caja chica.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCuentasDisponibles(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentasDisponiblesCajaChica", conexion.sc);
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
                throw new Exception("Error al obtener cuentas disponibles: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene saldo actual de la caja chica ejecutando procedimiento sp_ObtenerSaldoCajaChica.
        /// Retorna valor decimal del saldo disponible en la caja chica.
        /// Se utiliza para mostrar saldo actual en interfaces y validar límites de transferencia.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public decimal ObtenerSaldoCajaChica()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerSaldoCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                int result = conexion.EjecutarScalarYEnviar(cmd);

                return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener saldo de caja chica: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Transfiere dinero desde una cuenta bancaria a la caja chica ejecutando procedimiento sp_EnviarDineroCajaChica.
        /// Parámetros: id_origen (cuenta de origen), monto (cantidad a transferir), parroquiaId (identificación),
        /// usuarioId (quién realiza la transferencia para auditoría).
        /// Retorna true si transferencia fue exitosa, false/excepción si falla.
        /// Utiliza parámetros OUTPUT (@Exitoso bit, @Mensaje varchar) para validación del procedimiento.
        /// Dispara NotificarSP automáticamente para sincronizar transferencia con servidor remoto.
        /// 
        /// Operación crítica:
        /// - Decrementa saldo de cuenta origen
        /// - Incrementa saldo de caja chica
        /// - Registra transacción para auditoría
        /// </summary>
        public bool EnviarDineroCajaChica(int id_origen, decimal monto, int parroquiaId, int usuarioId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_EnviarDineroCajaChica", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada (Input)
                cmd.Parameters.AddWithValue("@IdOrigen", id_origen);
                cmd.Parameters.AddWithValue("@Monto", monto);
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);
                cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);

                // Parámetros de salida (Output) para validación y mensajes del procedimiento
                SqlParameter paramExitoso = new SqlParameter("@Exitoso", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(paramExitoso);
                cmd.Parameters.Add(paramMensaje);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);

                // Valida resultado del procedimiento
                bool exitoso = Convert.ToBoolean(paramExitoso.Value);

                if (!exitoso)
                {
                    string mensaje = paramMensaje.Value?.ToString() ?? "Error desconocido";
                    throw new Exception(mensaje);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene lista de tipos de cuenta disponibles ejecutando procedimiento sp_cargatipcuenta.
        /// Retorna DataTable con: tipo_cuenta_id, nombre_tipo (Ahorro, Cheque, etc), descripcion.
        /// Se utiliza para poblar ComboBox en formularios de selección de tipo de cuenta.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerTiposCuenta()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_cargatipcuenta", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                    {
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar tipos de cuenta: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}