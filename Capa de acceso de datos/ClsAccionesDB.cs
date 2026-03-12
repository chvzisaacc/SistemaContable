using Microsoft.Data.SqlClient;
using System.Data;
using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;

// Nota: Asumo que Clsconexion es la clase base que contiene sc (SqlConnection), Abrir() y Cerrar().

namespace Capa_de_acceso_de_datos
{

    /// <summary>
    /// 
    /// </summary>
    public class Parroquia
    {
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string nombre { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parroquia"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        public Parroquia(int id, string nombre)
        {

            this.nombre = nombre;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Reporte
    {
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string nombre { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reporte"/> class.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        public Reporte(string nombre)
        {
            this.nombre = nombre;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Origen
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string nombre { get; set; }
        public decimal Saldo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Origen"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        public Origen(int id, string nombre, decimal saldo = 0)
        {
            this.id = id;
            this.nombre = nombre;
            this.Saldo = saldo;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ResultadoLogin
    {
        /// <summary>
        /// Gets or sets the usuario identifier.
        /// </summary>
        /// <value>
        /// The usuario identifier.
        /// </value>
        public int usuario_id { get; set; } = 0;
        /// <summary>
        /// Gets or sets the rol identifier.
        /// </summary>
        /// <value>
        /// The rol identifier.
        /// </value>
        public int rol_id { get; set; } = 0;
        /// <summary>
        /// Gets the identifier parroquia.
        /// </summary>
        /// <value>
        /// The identifier parroquia.
        /// </value>
        public int id_parroquia { get; internal set; } = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class ClsAccionesDB : Clsconexion
    {
        /// <summary>
        /// Validars the credenciales.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <param name="contraseña">The contraseña.</param>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al validar usuario: " + ex.Message</exception>
        public ResultadoLogin ValidarCredenciales(string usuario, string contraseña, int parroquia_id)
        {
            // Inicialización simplificada
            ResultadoLogin resultado = new ResultadoLogin();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("IngresoLogin", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Password", contraseña);

                    using (SqlDataReader dr = EjecutarReaderYEnviar(cmd))
                    {
                        if (dr.Read())
                        {
                            // Asegúrate que el SP devuelva estas columnas.
                            resultado.usuario_id = dr.GetInt32(dr.GetOrdinal("UsuarioID"));
                            resultado.rol_id = dr.GetInt32(dr.GetOrdinal("RolID"));

                            int parroquia_ordinal = dr.GetOrdinal("Parroquia_ID");
                            resultado.id_parroquia = dr.IsDBNull(parroquia_ordinal) ? 0 : dr.GetInt32(parroquia_ordinal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                return resultado;
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }

        public void RegistrarInicioSesionBiometrico(int userId)
        {

            try
            {
                Abrir();
                using (SqlCommand command = new SqlCommand("RegistrarInicioSesionBiometrico", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro del usuario
                    command.Parameters.AddWithValue("@UsuarioID", userId);
                    EjecutarYEnviar(command);
                }


            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                throw new Exception("Error en la operación biométrica: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Cambiars the contraseña.
        /// </summary>
        /// <param name="correo">The correo.</param>
        /// <param name="nuevaa_contraseña">The nuevaa contraseña.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cambiar la contraseña: " + ex.Message</exception>
        public bool CambiarContraseña(string usuario, string correo, string nuevaa_contraseña)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CambiarContraseña", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@NuevaContraseña", nuevaa_contraseña);


                    EjecutarYEnviar(cmd);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Obteners the usuario identifier por correo.
        /// </summary>
        /// <param name="correo">The correo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener ID de usuario por correo: " + ex.Message</exception>
        public int ObtenerUsuarioIdPorCorreo(string usuario, string correo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerUsuarioIdPorCorreo", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioNombre", usuario);
                    cmd.Parameters.AddWithValue("@CorreoParroquia", correo);

                    // Usamos el método que retorna el valor único (ID)
                    // y dispara la sincronización en segundo plano.
                    return EjecutarScalarYEnviar(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ID de usuario: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Guardars the codigo recuperacion.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="codigo">The codigo.</param>
        /// <exception cref="System.Exception">Error al guardar código de recuperación: " + ex.Message</exception>
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
                    cmd.ExecuteNonQuery();
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
        /// Validars the codigo recuperacion.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="codigo">The codigo.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al validar el código: " + ex.Message</exception>
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

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            resultado = dr["Resultado"]?.ToString() ?? "SIN_RESULTADO";
                        }
                    } // dr.Close() y dr.Dispose() llamados automáticamente por 'using'
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
        /// Guardars the certificado.
        /// </summary>
        /// <param name="nombre_certificado">The nombre certificado.</param>
        /// <param name="deposito_inicial">The deposito inicial.</param>
        /// <param name="plazo">The plazo.</param>
        /// <param name="tasa">The tasa.</param>
        /// <param name="id_parroquia">The identifier parroquia.</param>
        /// <param name="fecha">The fecha.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al guardar certificado de depósito: " + ex.Message</exception>
        public int GuardarCertificado(string nombre_certificado, int id_parroquia, DateTime fecha)
        {
            int idgenerado = 0;

            try
            {
                // Abrir la conexión a la base de datos
                Abrir();

                // El 'sc' es tu objeto SqlConnection
                using (SqlCommand cmd = new SqlCommand("sp_guardar_certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 1. Parámetros Requeridos
                    cmd.Parameters.AddWithValue("@Nombre_certificado", nombre_certificado);
                    cmd.Parameters.AddWithValue("@IdParroquia", id_parroquia);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);

                    // Ejecutar el procedimiento. Se espera que devuelva el nuevo ID.
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idgenerado = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Es buena práctica lanzar una excepción más específica o registrar el error.
                throw new Exception("Error al guardar certificado de depósito: " + ex.Message, ex);
            }
            finally
            {
                // Cerrar la conexión
                Cerrar();
            }
            return idgenerado;
        }

        /// <summary>
        /// Cargars the certificados.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar los certificados: " + ex.Message</exception>
        public DataTable CargarCertificados()
        {
            DataTable dt = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_Mostrarcertificados", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los certificados: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        public DataTable MostrarCertificadosUsuario(int idParroquia)
        {
            DataTable dtCertificados = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_Mostrarcertificados_Usuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Parroquia_ID", idParroquia);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCertificados);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al mostrar certificados del usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return dtCertificados;
        }

        /// <summary>
        /// Cargars the cuentas bancarias.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar las cuentas bancarias: " + ex.Message</exception>
        public DataTable CargarCuentasBancarias()
        {
            DataTable dt = new DataTable();
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_mostrarCuentas", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                // Error de certificados en CargarCuentasBancarias. Se corrige el mensaje.
                throw new Exception("Error al cargar las cuentas bancarias: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Editarcertificadoes the specified codigo certificado.
        /// </summary>
        /// <param name="codigo_certificado">The codigo certificado.</param>
        /// <param name="nombre_certificado">The nombre certificado.</param>
        /// <param name="deposito_inicial">The deposito inicial.</param>
        /// <param name="plazo">The plazo.</param>
        /// <param name="tasa">The tasa.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al editar certificado de depósito: " + ex.Message</exception>
        public bool editarcertificado(int codigo_certificado, string nombre_certificado, int id_parroquia)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_editar_certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de edición
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigo_certificado);
                    cmd.Parameters.AddWithValue("@Nombre_certificado", nombre_certificado);
                    cmd.Parameters.AddWithValue("@IdParroquia", id_parroquia); // Nuevo: Incluir el ID de la Parroquia

                    // --- Parámetros eliminados: deposito_inicial, plazo, tasa ---

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
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
        /// Renovars the certificado.
        /// </summary>
        /// <param name="codigo_certificado">The codigo certificado.</param>
        /// <param name="deposito_inicial">The deposito inicial.</param>
        /// <param name="plazo">The plazo.</param>
        /// <param name="tasa">The tasa.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al renovar el certificado de depósito: " + ex.Message</exception>
        public bool renovarCertificado(int codigo_certificado)
        {
            try
            {
                // Abrir la conexión
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_renovar_Certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros obligatorios
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigo_certificado);


                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al renovar el certificado de depósito: " + ex.Message, ex);
            }
            finally
            {
                // Cerrar la conexión
                Cerrar();
            }
        }

        /// <summary>
        /// Cancelars the certificado.
        /// </summary>
        /// <param name="codigo_certificado">The codigo certificado.</param>
        /// <param name="detalle">The detalle.</param>
        /// <exception cref="System.Exception">Error al cancelar el certificado de depósito: " + ex.Message</exception>
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
                    cmd.ExecuteNonQuery();
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

        public int ObtenerIdParroquiaPorNombre(string nombreParroquia)
        {
            // Usamos 0 como valor de retorno si no se encuentra la parroquia.
            int idParroquia = 0;

            try
            {
                Abrir();

                // 2. Crear el comando y asignarle el SP
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerIdParroquiaPorNombre", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir el parámetro de entrada del nombre
                    // Se usa el nombre de columna de tu tabla: Parroquia_nombre (aunque el SP lo espera como @NombreParroquia)
                    cmd.Parameters.AddWithValue("@NombreParroquia", nombreParroquia);

                    // Ejecutar ExecuteScalar para obtener el IdParroquia (un solo valor)
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        // Convertir el resultado a entero
                        idParroquia = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores específicos de SQL (ej. timeout, problemas de conexión)
                // Aquí puedes logear el error para el administrador del sistema
                Console.WriteLine("Error SQL al obtener ID de parroquia: " + ex.Message);
                // Opcional: Relanzar una excepción
                throw new Exception("Error en la base de datos al buscar la parroquia: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Manejo de otros errores (ej. problemas de conversión)
                Console.WriteLine("Error general al obtener ID de parroquia: " + ex.Message);
                throw; // Relanzar la excepción
            }
            finally
            {
                Cerrar();
            }

            return idParroquia;
        }

        /// <summary>
        /// Obteners the lista origenes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar origen de fondos: " + ex.Message</exception>
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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Uso de GetInt32 y GetString para robustez
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
        /// Obteners the lista parroquias.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar parroquias: " + ex.Message</exception>
        public List<string> ObtenerListaParroquias()
        {
            List<string> listaparroquias = new List<string>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("nom_parroquia", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
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

        public DataTable ObtenerParroquias()
        {
            DataTable dtParroquia = new DataTable();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_autocompletar_parroquias", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtParroquia);
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
        /// Obteners the tipo reporte.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar los reportes: " + ex.Message</exception>
        public List<string> ObtenerTipoReporte()
        {
            List<string> reportes = new List<string>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("tipo_reporte", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
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
        /// Obteners the cuentas ingreso.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener las cuentas de ingresos: " + ex.Message</exception>
        public DataTable ObtenerCuentasIngreso()
        {
            DataTable dtcuentas = new DataTable();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_mostrar_cuentasingresos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtcuentas);
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
        /// Obteners the cuentas gastos.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener las cuentas de gastos: " + ex.Message</exception>
        public DataTable ObtenerCuentasGastos()
        {
            DataTable dtcuentas = new DataTable();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_mostrar_cuentasgastos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtcuentas);
                    }
                }
                return dtcuentas;
            }
            catch (Exception ex)
            {
                // Mensaje corregido
                throw new Exception("Error al obtener las cuentas de gastos: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }


        /// <summary>
        /// Obteners the usuario identifier por nombre usuario.
        /// </summary>
        /// <param name="nombre_usuario">The nombre usuario.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener ID de usuario por nombre de usuario: " + ex.Message</exception>
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

                    // Usamos ExecuteReader para obtener ambas columnas
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Si encuentra al menos una fila
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

            return Tuple.Create(id_usuario, parroquia_id);  // Tuple con ambos valores
        }
        /// <summary>
        /// Obteners the usuarios.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar los usuarios: " + ex.Message</exception>
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_CargarUsuarios", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Usuario_id"]);
                            string nombre = reader["NombreCompleto"].ToString();

                            usuarios.Add(new Usuario(id, nombre));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los usuarios: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return usuarios;
        }

        /// <summary>
        /// Guardars the foto rostro.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="rostro_data">The rostro data.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al guardar la foto del rostro: " + ex.Message</exception>
        public int GuardarFotoRostro(int usuario_id, byte[] rostro_data)
        {
            int nuevoRostroId = 0;

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("SP_GuardarFotos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@RostroData", rostro_data);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        nuevoRostroId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la foto del rostro: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return nuevoRostroId;
        }

        /// <summary>
        /// Contars the fotos usuario.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al contar fotos del usuario: " + ex.Message</exception>
        public int ContarFotosUsuario(int usuario_id)
        {
            int total = 0;

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_ContarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        total = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar fotos del usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return total;
        }

        /// <summary>
        /// Borrars the fotos usuario.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <exception cref="System.Exception">Error al borrar fotos del usuario: " + ex.Message</exception>
        public void BorrarFotosUsuario(int usuario_id)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_BorrarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar fotos del usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

        }

        /// <summary>
        /// Obteners the nombre usuario.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener nombre de usuario: " + ex.Message</exception>
        public string ObtenerNombreUsuario(int usuario_id)
        {
            string nombre = "Desconocido";
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerNombreUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    object result = cmd.ExecuteScalar();


                    if (result != null && result != DBNull.Value)
                        nombre = result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener nombre de usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return nombre;
        }

        /// <summary>
        /// Obteners the rostros por usuario.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener el rostro: " + ex.Message</exception>
        public List<byte[]> ObtenerRostrosPorUsuario(int usuario_id)
        {
            List<byte[]> lista = new List<byte[]>();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerRostrosPorUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        byte[] data = (byte[])dr["RostroData"];
                        lista.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el rostro: " + ex.Message, ex);

            }
            finally
            {
                Cerrar();
            }

            return lista;
        }
        /// <summary>
        /// Obteners the usuario reconocimiento.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        public (string nombre, int rol_id, int estado_cuenta, int parroquia_id) ObtenerUsuarioReconocimiento(int usuario_id)
        {
            string nombre = "";
            int rol_id = 0;
            int estado_cuenta = 0;
            int parroquia_id = 0;

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioReconocimiento", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        nombre = dr["NombreCompleto"].ToString();
                        rol_id = Convert.ToInt32(dr["Rol_id"]);
                        estado_cuenta = Convert.ToInt32(dr["Id_estado_cuenta"]);
                        parroquia_id = Convert.ToInt32(dr["parroquia_id"]);
                    }
                }
            }
            finally
            {
                Cerrar();
            }

            return (nombre, rol_id, estado_cuenta, parroquia_id);
        }
    }
}