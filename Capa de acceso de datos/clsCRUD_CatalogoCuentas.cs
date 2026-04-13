using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Gestiona operaciones CRUD del catálogo de cuentas contables: niveles jerárquicos, búsquedas, creación, edición y cambios de estado.
    /// Todos los métodos que modifican datos disparan sincronización remota automática.
    /// La estructura es jerárquica: nivel 1 (raíz) → hijos → detalles.
    /// </summary>
    public class clsCRUD_CatalogoCuentas
    {
        private Clsconexion conexion;

        public clsCRUD_CatalogoCuentas()
        {
            conexion = new Clsconexion();
        }

        /// <summary>
        /// Obtiene cuentas de nivel 1 (raíz) del catálogo ejecutando procedimiento sp_ObtenerNivel1.
        /// Retorna DataTable con todas las cuentas padre que no tienen padre superior.
        /// Se utiliza para poblar estructura jerárquica principal en controles de árbol.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerNivel1()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerNivel1", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener nivel 1: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Obtiene cuentas hijo de una cuenta padre ejecutando procedimiento sp_ObtenerHijosPorPadre.
        /// Parámetro id_padre: identifica la cuenta padre para traer sus subcuentas.
        /// Retorna DataTable con todas las cuentas que dependen del padre especificado.
        /// Se utiliza para expandir nodos en estructura jerárquica del catálogo.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerHijosPorPadre(int id_padre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerHijosPorPadre", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener hijos: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Obtiene todas las cuentas del catálogo ejecutando procedimiento sp_ObtenerCatalogoCuentas.
        /// Retorna DataTable con estructura completa: código, nombre, jerarquía, estado, etc.
        /// Se utiliza para listados, reportes y exportaciones de catálogo completo.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCatalogoCuentas()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCatalogoCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener catálogo: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Busca una cuenta específica por ID ejecutando procedimiento sp_BuscarCatalogoCuentaPorId.
        /// Parámetro id_cuenta: identifica la cuenta a recuperar.
        /// Retorna DataRow con información completa de la cuenta (código, nombre, padre, estado, etc).
        /// Retorna null si la cuenta no existe.
        /// Se utiliza para cargar datos en formularios de edición y visualización.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataRow BuscarCatalogoCuentaPorId(int id_cuenta)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarCatalogoCuentaPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Crea nueva cuenta en el catálogo ejecutando procedimiento sp_AgregarCatalogoCuenta.
        /// Parámetros: codigo (único), nombre, id_padre (jerarquía), detalle (descripción opcional),
        /// es_detalle (indica si es cuenta de detalle para transacciones), id_estado (activo/inactivo).
        /// Retorna true si se creó exitosamente, false si ocurre error.
        /// Maneja NULL en detalle si está vacío. Convierte bool es_detalle a bit (1/0).
        /// Dispara NotificarSP automáticamente para sincronizar con servidor remoto.
        /// </summary>
        public bool AgregarCatalogoCuenta(string codigo, string nombre, int id_padre,
                                   string detalle, bool es_detalle = true,
                                   int id_estado = 1)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_AgregarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id_padre", id_padre);

                cmd.Parameters.AddWithValue("@detalle", string.IsNullOrWhiteSpace(detalle)
                                                         ? (object)DBNull.Value : detalle);

                cmd.Parameters.AddWithValue("@es_detalle", es_detalle ? 1 : 0);
                cmd.Parameters.AddWithValue("@Id_estado_cuenta", id_estado);

                var resultado = conexion.EjecutarScalarYEnviar(cmd, sincronizar: true);
                return resultado != 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Modifica una cuenta existente ejecutando procedimiento sp_ModificarCatalogoCuenta.
        /// Parámetros: id_cuenta (identifica registro), codigo (nuevo), nombre (nuevo),
        /// id_padre (nueva jerarquía), detalle (nueva descripción opcional).
        /// Retorna true si modificación fue exitosa, false si ocurre error.
        /// Maneja NULL en detalle si está vacío.
        /// Dispara NotificarSP automáticamente para sincronizar cambio con servidor remoto.
        /// </summary>
        public bool ModificarCatalogoCuenta(int id_cuenta, string codigo, string nombre,
                                    int id_padre, string detalle)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ModificarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                cmd.Parameters.AddWithValue("@detalle", string.IsNullOrWhiteSpace(detalle)
                                                          ? (object)DBNull.Value : detalle);

                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Cambia estado de una cuenta ejecutando procedimiento sp_CambiarEstadoCuenta.
        /// Parámetros: id_cuenta (identifica registro), id_estado (1=activo, 0=inactivo u otro).
        /// Retorna true si cambio fue exitoso, false si ocurre error.
        /// Se utiliza para activar/desactivar cuentas sin eliminarlas (auditoría).
        /// Dispara NotificarSP automáticamente para sincronizar cambio con servidor remoto.
        /// </summary>
        public bool CambiarEstadoCuenta(int id_cuenta, int id_estado)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_CambiarEstadoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
                cmd.Parameters.AddWithValue("@Id_estado_cuenta", id_estado);
                conexion.EjecutarYEnviar(cmd, sincronizar: true);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Obtiene lista de estados disponibles para cuentas ejecutando procedimiento sp_ObtenerEstadosCuenta.
        /// Retorna DataTable con: id_estado, nombre_estado (ejemplo: "Activo", "Inactivo").
        /// Se utiliza para poblar ComboBox en formularios de creación/edición de cuentas.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerEstados()
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerEstadosCuenta", conexion.sc);
                DataTable dt = new DataTable();
                using (SqlDataReader dr = conexion.EjecutarReaderYEnviar(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estados: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Valida si una cuenta con nombre especificado ya existe ejecutando procedimiento sp_CatalogoCuentaExiste.
        /// Parámetro nombre: nombre de la cuenta a validar.
        /// Utiliza parámetro OUTPUT @existe (bit) para retornar resultado.
        /// Retorna true si cuenta existe, false si no existe.
        /// Se utiliza para validar unicidad antes de crear nuevas cuentas.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public bool CatalogoCuentaExiste(string nombre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_CatalogoCuentaExiste", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                SqlParameter existe = new SqlParameter("@existe", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(existe);
                conexion.EjecutarYEnviar(cmd);
                return Convert.ToBoolean(existe.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar cuenta: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Obtiene siguiente código secuencial disponible para una cuenta padre ejecutando procedimiento sp_ObtenerProximoCodigoCatalogo.
        /// Parámetro id_padre: cuenta padre para generar código hijo basado en su patrón.
        /// Retorna string con código generado (ejemplo: "1.1.1"), vacío si error.
        /// Se utiliza para asignar código automáticamente al crear nuevas cuentas (mantiene coherencia jerárquica).
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public string ObtenerProximoCodigo(int id_padre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerProximoCodigoCatalogo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_padre", id_padre);
                object resultado = cmd.ExecuteScalar();
                return resultado != null && resultado != DBNull.Value ? resultado.ToString() : "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo código: " + ex.Message, ex);
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Busca ID de una cuenta por su nombre ejecutando procedimiento sp_BuscarIdCuentaPorNombre.
        /// Parámetro nombre: nombre de la cuenta a buscar.
        /// Retorna ID numérico de la cuenta si existe, 0 si no existe.
        /// Se utiliza para resolver referencias de nombre a ID en operaciones contables.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public int BuscarIdCuentaPorNombre(string nombre)
        {
            try
            {
                conexion.Abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarIdCuentaPorNombre", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                var resultado = conexion.EjecutarScalarYEnviar(cmd);
                return resultado;
            }
            catch { return 0; }
            finally { conexion.Cerrar(); }
        }
    }
}