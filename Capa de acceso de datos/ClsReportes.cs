using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona generación de reportes financieros y contables para parroquias.
    /// Proporciona acceso a reportes: estado de resultados, libro mayor, ingresos, gastos y datos de curia.
    /// Todos los métodos son de lectura únicamente, sin sincronización remota.
    /// </summary>
    public class ClsReportes
    {
        private readonly Clsconexion _cn = new Clsconexion();

        /// <summary>
        /// Obtiene estado de resultados de una parroquia en rango de fechas ejecutando procedimiento SP_EstadoResultados.
        /// Parámetros: parroquia_id (identifica parroquia), desde/hasta (rango de fechas inclusive).
        /// Retorna DataSet con: ingresos totales, gastos totales, diferencia neta, variaciones por origen.
        /// Se utiliza para análisis financiero y presentación de balances periódicos.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataSet ObtenerEstadoResultados(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataSet ds = new DataSet();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_EstadoResultados", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fechaInicio", desde.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", hasta.Date);
                    cmd.Parameters.AddWithValue("@idParroquia", parroquia_id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return ds;
        }

        /// <summary>
        /// Obtiene libro mayor (detalle de todas las transacciones contables) ejecutando procedimiento sp_LibroMayor.
        /// Parámetros: parroquia_id (identifica parroquia), desde/hasta (rango de fechas inclusive).
        /// Retorna DataSet con: fecha, cuenta, descripción, débito, crédito, saldo acumulado para cada transacción.
        /// Se utiliza para auditoría completa, validación de asientos contables y análisis de cuentas.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataSet ObtenerLibroMayor(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataSet ds = new DataSet();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_LibroMayor", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fechaInicio", desde.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", hasta.Date);
                    cmd.Parameters.AddWithValue("@idParroquia", parroquia_id);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Libro Mayor: " + ex.Message, ex);
            }
            finally
            {
                _cn.Cerrar();
            }

            return ds;
        }

        /// <summary>
        /// Obtiene detalle de ingresos por parroquia en rango de fechas ejecutando procedimiento sp_ReporteIngresos.
        /// Parámetros: parroquia_id (identifica parroquia), desde/hasta (rango de fechas inclusive).
        /// Retorna DataTable con: fecha, origen (fuente ingreso), monto, concepto, usuario, estado.
        /// Se utiliza para análisis de ingresos, composición de fondos y presupuestos.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerIngresosPorParroquia(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ReporteIngresos", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obtiene detalle de gastos por parroquia en rango de fechas ejecutando procedimiento sp_ReporteGastos.
        /// Parámetros: parroquia_id (identifica parroquia), desde/hasta (rango de fechas inclusive).
        /// Retorna DataTable con: fecha, cuenta gasto, monto, descripción, usuario, departamento.
        /// Se utiliza para control de gastos, análisis de presupuesto y auditoría de erogaciones.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerGastosPorParroquia(int parroquia_id, DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ReporteGastos", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obtiene lista de todas las parroquias del sistema ejecutando procedimiento sp_ObtenerParroquias.
        /// Retorna DataTable con: parroquia_id, nombre, correo, estado, ciudad, responsable.
        /// Se utiliza para poblar ComboBox/filtros en formularios de reportes.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerParroquias()
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerParroquias", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obtiene nombre de una parroquia específica ejecutando procedimiento sp_ObtenerNombreParroquia.
        /// Parámetro parroquia_id: identifica la parroquia.
        /// Retorna string con nombre de la parroquia, null si no existe.
        /// Se utiliza para mostrar nombre en encabezados de reportes y validación.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public string ObtenerNombreParroquia(int parroquia_id)
        {
            string nombre = null;

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerNombreParroquia", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ParroquiaId", parroquia_id);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        nombre = result.ToString();
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return nombre;
        }

        /// <summary>
        /// Obtiene lista de tipos de reportes disponibles en el sistema ejecutando procedimiento sp_ObtenerTiposReporte.
        /// Retorna DataTable con: tipo_id, nombre_tipo (Estado Resultados, Libro Mayor, Ingresos, etc), descripcion.
        /// Se utiliza para poblar menú/ComboBox de selección de tipo de reporte.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerTiposReporte()
        {
            DataTable dt = new DataTable();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTiposReporte", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Obtiene datos de actividades sacramentales (curia) filtradas por usuario/sacerdote ejecutando procedimiento sp_ReporteCuria.
        /// Parámetros: usuarioId (sacerdote que realizó), desde/hasta (rango de fechas inclusive).
        /// Retorna DataSet con: fecha, tipo servicio (bautismo, matrimonio, misa), persona, detalles, observaciones.
        /// Se utiliza para reportes de actividades pastorales y estadísticas de servicios religiosos.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataSet ObtenerDatosCuriaPorUsuario(int usuarioId, DateTime desde, DateTime hasta)
        {
            DataSet ds = new DataSet();

            try
            {
                _cn.Abrir();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_ReporteCuria", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(ds);
                }
            }
            finally
            {
                _cn.Cerrar();
            }

            return ds;
        }

        /// <summary>
        /// Obtiene nombre completo de un sacerdote ejecutando procedimiento sp_ObtenerNombreSacerdote.
        /// Parámetro usuarioId: identifica el sacerdote/usuario.
        /// Retorna string con nombre completo (nombre + apellido), vacío si no existe.
        /// Se utiliza para mostrar nombre en reportes y encabezados de documentos.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public string ObtenerNombreSacerdote(int usuarioId)
        {
            string nombreCompleto = "";
            try
            {
                _cn.Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerNombreSacerdote", _cn.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    using (SqlDataReader dr = _cn.EjecutarReaderYEnviar(cmd))
                    {
                        if (dr.Read())
                        {
                            nombreCompleto = dr["NombreCompleto"]?.ToString() ?? "";
                        }
                    }
                }
            }
            finally
            {
                _cn.Cerrar();
            }
            return nombreCompleto;
        }
    }
}