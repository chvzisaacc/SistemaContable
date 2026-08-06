using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Capa_de_acceso_de_datos
{
    public class Parroquia
    {
        public string nombre { get; set; }

        public Parroquia(int id, string nombre)
        {
            this.nombre = nombre;
        }
    }

    public class Reporte
    {
        public string nombre { get; set; }

        public Reporte(string nombre)
        {
            this.nombre = nombre;
        }
    }

    public class Origen
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public decimal Saldo { get; set; }

        public Origen(int id, string nombre, decimal saldo = 0)
        {
            this.id = id;
            this.nombre = nombre;
            this.Saldo = saldo;
        }
    }

    public class ResultadoLogin
    {
        public int usuario_id { get; set; } = 0;
        public int rol_id { get; set; } = 0;
        public int id_parroquia { get; internal set; } = 0;
    }

    public class ClsAccionesDB : Clsconexion
    {
        /// <summary>
        /// Autentica usuario ejecutando procedimiento IngresoLogin.
        /// Retorna objeto ResultadoLogin con usuario_id, rol_id e id_parroquia.
        /// Dispara NotificarSP para sincronización remota automática.
        /// </summary>
        public ResultadoLogin ValidarCredenciales(string usuario, string contraseña, int parroquia_id)
        {
            ResultadoLogin resultado = new ResultadoLogin();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("IngresoLogin", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Password", contraseña);

                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd, sincronizar: true))
                    {
                        if (dr.Read())
                        {
                            resultado.usuario_id = dr.GetInt32(dr.GetOrdinal("UsuarioID"));
                            resultado.rol_id = dr.GetInt32(dr.GetOrdinal("RolID"));

                            int parroquia_ordinal = dr.GetOrdinal("Parroquia_Id");
                            resultado.id_parroquia = dr.IsDBNull(parroquia_ordinal) ? 0 : dr.GetInt32(parroquia_ordinal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ValidarCredenciales: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }

        /// <summary>
        /// Ejecuta procedimiento CambiarContrasena para actualizar contraseña de usuario.
        /// Parámetros: usuario (nombre), correo (validación), nuevaa_contraseña (nueva clave).
        /// Dispara NotificarSP automáticamente para sincronizar cambio con servidor remoto.
        /// </summary>
        public bool CambiarContraseña(string usuario, string correo, string nuevaa_contraseña)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CambiarContrasena", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@NuevaContrasena", nuevaa_contraseña);

                    EjecutarYEnviar(cmd, sincronizar: true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Busca usuario por nombre y correo mediante procedimiento ObtenerUsuarioIdPorCorreo.
        /// Retorna ID del usuario o 0 si no existe.
        /// Se usa en flujo de recuperación de contraseña.
        /// Dispara NotificarSP para sincronización remota.
        /// </summary>
        public int ObtenerUsuarioIdPorCorreo(string usuario, string correo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerUsuarioIdPorCorreo", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioNombre", usuario);
                    cmd.Parameters.AddWithValue("@CorreoParroquia", correo);

                    return EjecutarScalarYEnviar(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ID de usuario: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Guarda código temporal de recuperación mediante procedimiento GuardarCodigoRecuperacion.
        /// Código debe ser generado previamente (ej: 6 dígitos aleatorios).
        /// Base de datos gestiona expiración automática del código.
        /// Dispara NotificarSP para sincronización con servidor remoto.
        /// </summary>
        public void GuardarCodigoRecuperacion(int usuario_id, string codigo)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("GuardarCodigoRecuperacion", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuario_id);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    EjecutarYEnviar(cmd, sincronizar: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar código de recuperación: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Valida código ingresado contra código almacenado mediante procedimiento ValidarCodigoRecuperacion.
        /// Retorna estado: "VALIDO" (código correcto y no expirado), "EXPIRADO" (correcto pero vencido),
        /// "INVALIDO" (código incorrecto), o "SIN_RESULTADO" (error inesperado).
        /// No dispara sincronización (solo lectura).
        /// </summary>
        public string ValidarCodigoRecuperacion(int usuario_id, string codigo)
        {
            string resultado = string.Empty;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("ValidarCodigoRecuperacion", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuario_id);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        if (dr.Read())
                        {
                            resultado = dr["Resultado"]?.ToString() ?? "SIN_RESULTADO";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el código: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return resultado;
        }

        /// <summary>
        /// Crea nuevo certificado de depósito ejecutando procedimiento sp_guardar_certificado.
        /// Parámetros: nombre_certificado (descripción), id_parroquia (propietario), fecha (fecha creación).
        /// Retorna ID autogenerado por la base de datos del nuevo certificado, o 0 si falla.
        /// Dispara NotificarSP para sincronizar creación con servidor remoto.
        /// </summary>
        public int GuardarCertificado(string nombre_certificado, int id_parroquia, DateTime fecha)
        {
            int idgenerado = 0;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_guardar_certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre_certificado", nombre_certificado);
                    cmd.Parameters.AddWithValue("@IdParroquia", id_parroquia);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);

                    object result = EjecutarScalarYEnviar(cmd, sincronizar: true);
                    if (result != null && result != DBNull.Value)
                    {
                        idgenerado = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar certificado de depósito: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return idgenerado;
        }

        /// <summary>
        /// Carga todos los certificados de depósito del sistema mediante procedimiento sp_Mostrarcertificados.
        /// Retorna DataTable con columnas del resultado del procedimiento.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable CargarCertificados()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Mostrarcertificados", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dt.Load(dr);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los certificados: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Carga certificados de depósito filtrados por parroquia mediante procedimiento sp_Mostrarcertificados_Usuario.
        /// Retorna DataTable con certificados pertenecientes a la parroquia especificada.
        /// Utilizado en vista de usuario para mostrar solo sus propios certificados.
        /// No dispara sincronización (solo lectura).
        /// </summary>
        public DataTable MostrarCertificadosUsuario(int idParroquia)
        {
            DataTable dtCertificados = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Mostrarcertificados_Usuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Parroquia_ID", idParroquia);
                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dtCertificados.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar certificados del usuario: " + ex.Message, ex);
            }
            return dtCertificados;
        }

        /// <summary>
        /// Carga todas las cuentas bancarias del sistema mediante procedimiento sp_mostrarCuentas.
        /// Retorna DataTable con información de cuentas: ID, número, banco, tipo, saldo, etc.
        /// Se utiliza para poblar ComboBox y DataGridView en formularios.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable CargarCuentasBancarias()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrarCuentas", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dt.Load(dr);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las cuentas bancarias: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Modifica certificado existente ejecutando procedimiento sp_editar_certificado.
        /// Permite cambiar: nombre_certificado y id_parroquia (propietario).
        /// Parámetro codigo_certificado identifica qué certificado editar.
        /// Dispara NotificarSP para sincronizar edición con servidor remoto.
        /// </summary>
        public bool editarcertificado(int codigo_certificado, string nombre_certificado, int id_parroquia)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_editar_certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigo_certificado);
                    cmd.Parameters.AddWithValue("@Nombre_certificado", nombre_certificado);
                    cmd.Parameters.AddWithValue("@IdParroquia", id_parroquia);

                    EjecutarYEnviar(cmd, sincronizar: true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar certificado de depósito: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Renueva certificado vencido ejecutando procedimiento sp_renovar_Certificado.
        /// Reinicia período de vigencia manteniendo condiciones originales del certificado.
        /// Parámetro codigo_certificado identifica qué certificado renovar.
        /// Dispara NotificarSP para sincronizar renovación con servidor remoto.
        /// </summary>
        public bool renovarCertificado(int codigo_certificado)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_renovar_Certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigo_certificado);

                    EjecutarYEnviar(cmd, sincronizar: true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al renovar el certificado de depósito: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Cancela certificado de depósito ejecutando procedimiento sp_cancelar_Certificado.
        /// Parámetro detalle contiene motivo de cancelación (se almacena para auditoría).
        /// Operación es irreversible: certificado se marca como cancelado en base de datos.
        /// Dispara NotificarSP para sincronizar cancelación con servidor remoto.
        /// </summary>
        public void cancelarCertificado(int codigo_certificado, string detalle)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_cancelar_Certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigo_certificado);
                    cmd.Parameters.AddWithValue("@Detalle", detalle);
                    EjecutarYEnviar(cmd, sincronizar: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cancelar el certificado de depósito: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Busca ID de parroquia por nombre exacto ejecutando procedimiento sp_ObtenerIdParroquiaPorNombre.
        /// Retorna ID numérico de la parroquia o 0 si no se encuentra.
        /// Búsqueda es sensible a mayúsculas/minúsculas según configuración SQL Server.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public int ObtenerIdParroquiaPorNombre(string nombreParroquia)
        {
            int idParroquia = 0;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerIdParroquiaPorNombre", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreParroquia", nombreParroquia);

                    idParroquia = EjecutarScalarYEnviar(cmd);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error SQL al obtener ID de parroquia: " + ex.Message);
                throw new Exception("Error en la base de datos al buscar la parroquia: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general al obtener ID de parroquia: " + ex.Message);
                throw;
            }
            finally
            {
                Cerrar();
            }
            return idParroquia;
        }

        /// <summary>
        /// Obtiene lista de fuentes de fondos (orígenes) para parroquia específica ejecutando SP_ObtenerFuentesDeFondos.
        /// Retorna List{Origen} con: ID, nombre descriptivo y saldo actual de cada fuente.
        /// Se utiliza para poblar controles que muestran opciones de fondos disponibles.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public List<Origen> ObtenerListaOrigenes(int parroquiaId)
        {
            List<Origen> listaorigenes = new List<Origen>();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerFuentesDeFondos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Parroquia_ID", parroquiaId);

                    using (SqlDataReader reader = EjecutarReaderYEnviar(cmd))
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string nombre = reader.GetString(reader.GetOrdinal("NombreOrigen"));
                            decimal saldoReal = reader.GetDecimal(reader.GetOrdinal("saldo"));

                            listaorigenes.Add(new Origen(id, nombre, saldoReal));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar origen de fondos: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return listaorigenes;
        }

        /// <summary>
        /// Obtiene lista de nombres de todas las parroquias del sistema ejecutando procedimiento nom_parroquia.
        /// Retorna List{string} con nombres únicamente (sin IDs).
        /// Se utiliza para poblar ComboBox y listas desplegables de selección de parroquias.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public List<string> ObtenerListaParroquias()
        {
            List<string> listaparroquias = new List<string>();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("nom_parroquia", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = EjecutarReaderYEnviar(cmd))
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["Parroquia_nombre"].ToString();
                            listaparroquias.Add(nombre);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar parroquias: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return listaparroquias;
        }

        /// <summary>
        /// Obtiene DataTable de parroquias ejecutando procedimiento SP_autocompletar_parroquias.
        /// Retorna tabla con datos: ID, nombre y otras columnas para autocompletado en controles.
        /// Se utiliza en controles de búsqueda y autocompletado que necesitan más datos que solo nombre.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerParroquias()
        {
            DataTable dtParroquia = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_autocompletar_parroquias", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dtParroquia.Load(dr);
                    }
                }
                return dtParroquia;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las parroquias " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Obtiene lista de tipos de reportes disponibles ejecutando procedimiento tipo_reporte.
        /// Retorna List{string} con descripciones: "Estado de Resultados", "Libro Mayor", "Balance General", etc.
        /// Se utiliza para poblar menú de selección de reportes en formularios.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public List<string> ObtenerTipoReporte()
        {
            List<string> reportes = new List<string>();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("tipo_reporte", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = EjecutarReaderYEnviar(cmd))
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["descripcion"].ToString();
                            reportes.Add(nombre);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los reportes: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return reportes;
        }

        /// <summary>
        /// Carga cuentas contables de ingresos ejecutando procedimiento SP_mostrar_cuentasingresos.
        /// Retorna DataTable con: código, nombre, descripción, estado y otras propiedades de cuentas de ingresos.
        /// Se utiliza para poblar ComboBox en formularios de registro de ingresos.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCuentasIngreso()
        {
            DataTable dtcuentas = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_mostrar_cuentasingresos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dtcuentas.Load(dr);
                    }
                }
                return dtcuentas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las cuentas de ingresos: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Carga cuentas contables de gastos ejecutando procedimiento SP_mostrar_cuentasgastos.
        /// Retorna DataTable con: código, nombre, descripción, límite presupuesto y otras propiedades.
        /// Se utiliza para poblar ComboBox en formularios de registro de gastos y control presupuestario.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public DataTable ObtenerCuentasGastos()
        {
            DataTable dtcuentas = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_mostrar_cuentasgastos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        dtcuentas.Load(dr);
                    }
                }
                return dtcuentas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las cuentas de gastos: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Obtiene tupla (Item1, Item2) ejecutando procedimiento SP_ObtenerUsuarioId.
        /// Item1: ID usuario (0 si no existe) | Item2: ID parroquia (0 si no asignado).
        /// Se utiliza después de autenticación para obtener información contextual del usuario.
        /// No dispara sincronización (operación de lectura únicamente).
        /// </summary>
        public Tuple<int, int> ObtenerUsuarioIdPorNombreUsuario(string nombre_usuario)
        {
            int id_usuario = 0;
            int parroquia_id = 0;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioId", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombre_usuario);

                    using (SqlDataReader reader = EjecutarReaderYEnviar(cmd))
                    {
                        if (reader.Read())
                        {
                            id_usuario = reader.GetInt32(reader.GetOrdinal("Usuario_id"));
                            parroquia_id = reader.IsDBNull(reader.GetOrdinal("Parroquia_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Parroquia_id"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ID de usuario por nombre de usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return Tuple.Create(id_usuario, parroquia_id);
        }

        /// <summary>
        /// Crea nueva parroquia ejecutando procedimiento sp_AgregarParroquia.
        /// Parámetros: nombre (oficial), correo (para comunicaciones).
        /// Retorna ID autogenerado de la parroquia creada, o 0 si falla.
        /// Dispara NotificarSP para sincronizar creación con servidor remoto.
        /// </summary>
        public int AgregarParroquia(string nombre, string correo)
        {
            int idGenerado = 0;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_AgregarParroquia", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    object result = EjecutarScalarYEnviar(cmd, sincronizar: true);

                    if (result != null && result != DBNull.Value)
                    {
                        idGenerado = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la parroquia: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return idGenerado;
        }

        /// <summary>
        /// Modifica parroquia existente ejecutando procedimiento sp_EditarParroquia.
        /// Parámetros: idParroquia (identifica registro), nombre (nuevo), correo (nuevo).
        /// Cambios se aplican en base de datos local e inmediatamente se sincronizan.
        /// Dispara NotificarSP para sincronizar edición con servidor remoto automáticamente.
        /// </summary>
        public bool EditarParroquia(int idParroquia, string nombre, string correo)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_EditarParroquia", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Parroquia_ID", idParroquia);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    EjecutarYEnviar(cmd, sincronizar: true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la parroquia: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Guarda código de edición especial con límite de 3 diarios por módulo.
        /// Módulos: 2=Ingresos, 3=Gastos, 4=Caja Chica, 5=Bancos, 9=Capital.
        /// Lanza excepción con mensaje del SP si se alcanzó el límite diario.
        /// </summary>
        public bool GuardarCodigoEdicionEspecial(int usuario_id, string codigo, int modulo)
        {
            try
            {
                Abrir();
                SqlCommand cmd = new SqlCommand("GuardarCodigoEdicionEspecial", sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioId", usuario_id);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Modulo", modulo);

                SqlParameter paramExitoso = new SqlParameter("@Exitoso", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(paramExitoso);
                cmd.Parameters.Add(paramMensaje);

                EjecutarYEnviar(cmd, sincronizar: true);

                bool exitoso = Convert.ToBoolean(paramExitoso.Value);
                if (!exitoso)
                    throw new Exception(paramMensaje.Value?.ToString() ?? "Error desconocido");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Valida código de edición especial para el módulo indicado.
        /// Retorna: CODIGO_VALIDO, CODIGO_INCORRECTO, CODIGO_EXPIRADO, CODIGO_AGOTADO, SIN_CODIGO.
        /// A diferencia de recuperación de contraseña, NO bloquea la cuenta al agotar intentos.
        /// </summary>
        public string ValidarCodigoEdicionEspecial(int usuario_id, string codigo, int modulo)
        {
            string resultado = string.Empty;
            try
            {
                Abrir();
                SqlCommand cmd = new SqlCommand("ValidarCodigoEdicionEspecial", sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioId", usuario_id);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Modulo", modulo);

                using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                {
                    if (dr.Read())
                        resultado = dr["Resultado"]?.ToString() ?? "SIN_RESULTADO";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar código de edición especial: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return resultado;
        }
    }
}