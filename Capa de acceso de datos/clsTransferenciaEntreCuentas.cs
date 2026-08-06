using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona transferencias de dinero entre cuentas bancarias de una parroquia.
    /// Proporciona acceso a cuentas disponibles, realiza transferencias con sincronización y accede a caja chica.
    /// Las transferencias son operaciones críticas que deben replicarse en el servidor remoto automáticamente.
    /// </summary>
    public class clsTransferenciaEntreCuentas
    {
        private Clsconexion conexion = new Clsconexion();

        /// <summary>
        /// Obtiene lista de cuentas bancarias disponibles para transferencias ejecutando procedimiento sp_ObtenerOrigenFuentes.
        /// Parámetro parroquiaId: filtra cuentas pertenecientes a la parroquia especificada.
        /// Retorna DataTable con: cuenta_id, nombre_cuenta, saldo_disponible, tipo_cuenta, estado.
        /// Se utiliza para poblar ComboBox de origen/destino en formularios de transferencia.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCuentasBanco(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerOrigenFuentes", conexion.sc);
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
                throw new Exception("Error al obtener cuentas bancarias: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Transfiere dinero entre dos cuentas bancarias ejecutando procedimiento sp_TransferirEntreCuentas.
        /// Parámetros: cuenta_origen (de donde sale dinero), cuenta_destino (donde llega dinero),
        /// monto (cantidad a transferir), parroquiaId (contexto), usuarioId (auditoría),
        /// descripcion (opcional, default "Transferencia" si no se proporciona).
        /// Retorna true si transferencia fue exitosa, false/excepción si falla.
        /// 
        /// Validaciones del procedimiento (errores específicos):
        /// - 50001: Saldo insuficiente en la cuenta de origen
        /// - 50002: La cuenta de origen no existe
        /// - 50003: La cuenta de destino no existe
        /// - 50004: Las cuentas de origen y destino deben ser diferentes
        /// 
        /// Operación crítica:
        /// - Decrementa saldo de cuenta origen
        /// - Incrementa saldo de cuenta destino
        /// - Registra transacción para auditoría
        /// - Dispara NotificarSP automáticamente para sincronizar con servidor remoto
        /// </summary>
        public bool TransferirEntreCuentas(int cuenta_origen, int cuenta_destino, decimal monto,
                             int parroquiaId, int usuarioId, string descripcion = null)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_TransferirEntreCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CuentaOrigen", cuenta_origen);
                cmd.Parameters.AddWithValue("@CuentaDestino", cuenta_destino);
                cmd.Parameters.AddWithValue("@Monto", monto);
                cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);
                cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrWhiteSpace(descripcion) ? "Transferencia" : descripcion);
                cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);

                SqlParameter paramExitoso = new SqlParameter("@Exitoso", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(paramExitoso);
                cmd.Parameters.Add(paramMensaje);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);

                bool exitoso = Convert.ToBoolean(paramExitoso.Value);
                if (!exitoso)
                {
                    string mensajeSP = paramMensaje.Value?.ToString() ?? "Error desconocido";
                    throw new Exception(mensajeSP);
                }

                return true;
            }
            catch (SqlException ex)
            {
                // Errores con número específico del SP (THROW 500xx)
                string mensaje = ex.Number switch
                {
                    50001 => "Saldo insuficiente en la cuenta de origen.",
                    50002 => "La cuenta de origen no existe.",
                    50003 => "La cuenta de destino no existe.",
                    50004 => "Las cuentas de origen y destino deben ser diferentes.",
                    _ => "Error al realizar la transferencia: " + ex.Message
                };
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar la transferencia: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene información de la caja chica de una parroquia ejecutando procedimiento sp_ObtenerCajaChicaParroquia.
        /// Parámetro parroquiaId: identifica la parroquia.
        /// Retorna DataTable con: caja_chica_id, saldo_actual, limite_maximo, estado, fecha_actualizacion.
        /// Se utiliza para mostrar información de caja chica antes de transferencias y validar límites.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCajaChica(int parroquiaId)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCajaChicaParroquia", conexion.sc);
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
                throw new Exception("Error al obtener Caja Chica: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }
    }
}