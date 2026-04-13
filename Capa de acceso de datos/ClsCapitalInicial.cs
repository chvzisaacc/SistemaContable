using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona operaciones de capital inicial de parroquias: ingreso, modificación, consulta y validación.
    /// El capital inicial es el monto base con que inicia la parroquia en el período contable.
    /// Todos los métodos que modifican datos disparan sincronización remota automática.
    /// </summary>
    public class ClsCapitalInicial
    {
        private readonly Clsconexion _cn = new Clsconexion();

        /// <summary>
        /// Registra capital inicial para una parroquia ejecutando procedimiento sp_IngresarCapitalInicial.
        /// Parámetros: usuarioId (auditoría), parroquiaId (identificación), monto (capital base).
        /// Retorna true si se registró exitosamente, false si ocurre error.
        /// Dispara NotificarSP automáticamente para sincronizar con servidor remoto.
        /// 
        /// Validaciones del procedimiento:
        /// - Verifica que parroquia exista
        /// - Valida que no tenga capital inicial previo (primera carga únicamente)
        /// - Confirma que monto sea válido y positivo
        /// 
        /// Crítico: Este es el punto de partida contable de la parroquia.
        /// </summary>
        public bool IngresarCapitalInicial(int usuarioId, int parroquiaId, decimal monto)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_IngresarCapitalInicial", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);
                    cmd.Parameters.AddWithValue("@Monto", monto);

                    _cn.EjecutarYEnviar(cmd, sincronizar: true);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Modifica capital inicial existente ejecutando procedimiento sp_ModificarCapitalInicial.
        /// Parámetros: monto (nuevo valor), parroquiaId (identificación), usuarioId (auditoría).
        /// Retorna true si modificación fue exitosa, false si ocurre error.
        /// Dispara NotificarSP automáticamente para sincronizar cambio con servidor remoto.
        /// 
        /// Nota: No puede crear capital, solo modifica uno existente.
        /// Validaciones del procedimiento:
        /// - Confirma que parroquia exista
        /// - Verifica que tenga capital inicial previo
        /// - Valida monto y permisos del usuario
        /// </summary>
        public bool ModificarCapitalInicial(decimal monto, int parroquiaId, int usuarioId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ModificarCapitalInicial", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@monto_nuevo", monto);
                    cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);
                    cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);

                    _cn.EjecutarYEnviar(cmd, sincronizar: true);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si parroquia tiene capital inicial registrado ejecutando procedimiento sp_TieneCapitalInicial.
        /// Parámetro: parroquiaId (identificación a validar).
        /// Retorna true si existe capital inicial, false si no existe o parroquia no está registrada.
        /// No dispara sincronización (operación de lectura únicamente).
        /// 
        /// Usos principales:
        /// - Evita intentos de cargar capital inicial múltiples veces
        /// - Determina flujo de inicialización de parroquia nueva
        /// - Valida restricciones de negocio en capa presentación
        /// 
        /// Nota: Abre/cierra conexión explícitamente en bloque try-finally.
        /// </summary>
        public bool TieneCapitalInicial(int parroquiaId)
        {
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_TieneCapitalInicial", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                            return Convert.ToBoolean(dt.Rows[0]["TieneCapital"]);

                        return false;
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene monto de capital inicial registrado ejecutando procedimiento sp_ObtenerCapitalInicialSeguro.
        /// Parámetros: usuarioId (validación de permisos), parroquiaId (identificación).
        /// Retorna valor decimal del capital inicial, o 0 si no existe.
        /// No dispara sincronización (operación de lectura únicamente).
        /// 
        /// Seguridad implementada en procedimiento:
        /// - Valida permisos de usuario para acceder a capital de esa parroquia
        /// - Protege contra acceso no autorizado a datos de otras parroquias
        /// - Lanza UnauthorizedAccessException si usuario no tiene permisos
        /// 
        /// Manejo de excepciones:
        /// - SqlException con "permisos" en mensaje: relanza UnauthorizedAccessException
        /// - Otras SqlException: se propagan hacia capa superior
        /// 
        /// Nota: Abre/cierra conexión explícitamente en bloque try-finally.
        /// </summary>
        public decimal ObtenerCapitalInicial(int usuarioId, int parroquiaId)
        {
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerCapitalInicialSeguro", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquiaId);

                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("permisos"))
                    throw new UnauthorizedAccessException("No tiene permisos para ver el capital de esta parroquia");
                else
                    throw;
            }
            finally
            {
                _cn.Cerrar();
            }
        }
    }
}