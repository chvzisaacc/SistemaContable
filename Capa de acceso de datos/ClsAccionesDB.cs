using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

// Nota: Asumo que Clsconexion es la clase base que contiene sc (SqlConnection), Abrir() y Cerrar().

namespace Capa_de_acceso_de_datos
{

    public class Parroquia
    {
        public string Nombre { get; set; }

        public Parroquia(int id, string nombre)
        {

            this.Nombre = nombre;
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
        public int ID { get; set; }
        public string Nombre { get; set; }

        public Origen(int id, string nombre)
        {
            this.ID = id;
            this.Nombre = nombre;
        }
    }

    public class ResultadoLogin
    {
        public int UsuarioID { get; set; } = 0;
        public int RolID { get; set; } = 0;
        public int IdParroquia { get; internal set; } = 0;
    }

    public class ClsAccionesDB : Clsconexion
    {
        public ResultadoLogin ValidarCredenciales(string usuario, string contraseña, int parroquiaId)
        {
            // Inicialización simplificada
            ResultadoLogin resultado = new ResultadoLogin();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("IngresoLogin", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // El parámetro parroquiaId en el método pero no en el SP:
                    // Si el SP 'IngresoLogin' no usa @ParroquiaID, elimínalo de la firma del método.
                    // cmd.Parameters.AddWithValue("@ParroquiaID", parroquiaId); 
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Password", contraseña);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // Asegúrate que el SP devuelva estas columnas.
                            resultado.UsuarioID = dr.GetInt32(dr.GetOrdinal("UsuarioID"));
                            resultado.RolID = dr.GetInt32(dr.GetOrdinal("RolID"));

                            int parroquiaOrdinal = dr.GetOrdinal("Parroquia_ID");
                            resultado.IdParroquia = dr.IsDBNull(parroquiaOrdinal) ? 0 : dr.GetInt32(parroquiaOrdinal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Es mejor registrar o manejar la excepción en la capa de negocio, no solo lanzarla.
                throw new Exception("Error al validar usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }

        public bool CambiarContraseña(string correo, string nuevaContraseña)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("CambiarContraseña", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@NuevaContraseña", nuevaContraseña);
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                // throw; es mejor que throw new Exception(ex.Message); si quieres preservar el stack trace
                throw new Exception("Error al cambiar la contraseña: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        public int ObtenerUsuarioIdPorCorreo(string correo)
        {
            int id = 0;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("ObtenerUsuarioIdPorCorreo", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CorreoParroquia", correo);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        id = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ID de usuario por correo: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return id;
        }

        public void GuardarCodigoRecuperacion(int usuarioId, string codigo)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("GuardarCodigoRecuperacion", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
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


        public string ValidarCodigoRecuperacion(int usuarioId, string codigo)
        {
            string resultado = string.Empty;

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("ValidarCodigoRecuperacion", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
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

        public int GuardarCertificado(string nombreCertificado, decimal depositoInicial, int plazo, decimal tasa, int idParroquia, DateTime fecha)
        {
            int idGenerado = 0;

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_guardar_certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre_certificado", nombreCertificado);
                    cmd.Parameters.AddWithValue("@deposito_inicial", depositoInicial);
                    cmd.Parameters.AddWithValue("@plazo", plazo);
                    cmd.Parameters.AddWithValue("@tasa", tasa);
                    cmd.Parameters.AddWithValue("@IdParroquia", idParroquia);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);

                    // ExecuteScalar devuelve un objeto; se debe manejar DBNull si aplica, aunque para un ID serial es improbable.
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idGenerado = Convert.ToInt32(result);
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
            return idGenerado;
        }

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

        public bool editarcertificado(int codigocertificado, string nombreCertificado, decimal depositoInicial, int plazo, decimal tasa)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_editar_certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigocertificado);
                    cmd.Parameters.AddWithValue("@Nombre_certificado", nombreCertificado);
                    cmd.Parameters.AddWithValue("@deposito_inicial", depositoInicial);
                    cmd.Parameters.AddWithValue("@plazo", plazo);
                    cmd.Parameters.AddWithValue("@tasa", tasa);
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

        public bool renovarCertificado(int codigocertificado, decimal depositoInicial, int plazo, decimal tasa)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_renovar_Certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigocertificado);
                    cmd.Parameters.AddWithValue("@deposito_inicial", depositoInicial);
                    cmd.Parameters.AddWithValue("@plazo", plazo);
                    cmd.Parameters.AddWithValue("@tasa", tasa);
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
                Cerrar();
            }
        }

        public void cancelarCertificado(int codigocertificado, string detalle)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("sp_cancelar_Certificado", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Certificado", codigocertificado);
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

        public List<Origen> ObtenerListaOrigenes()
        {
            List<Origen> listaOrigenes = new List<Origen>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_ObtenerFuentesDeFondos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Uso de GetInt32 y GetString para robustez
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string nombre = reader.GetString(reader.GetOrdinal("NombreOrigen"));

                            listaOrigenes.Add(new Origen(id, nombre));
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

            return listaOrigenes;
        }


        public List<string> ObtenerListaParroquias()
        {
            List<string> listaParroquias = new List<string>();

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
                            listaParroquias.Add(nombre);
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

            return listaParroquias;
        }

        public List<string> ObtenerTipoReporte()
        {
            List<string> Reportes = new List<string>();

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
                            Reportes.Add(nombre);
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

            return Reportes;
        }
        public DataTable ObtenerCuentasIngreso()
        {
            DataTable dtCuentas = new DataTable();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_mostrar_cuentasingresos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCuentas);
                    }
                }
                return dtCuentas;
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

        public DataTable ObtenerCuentasGastos()
        {
            DataTable dtCuentas = new DataTable();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_mostrar_cuentasgastos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCuentas);
                    }
                }
                return dtCuentas;
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


        public int ObtenerUsuarioIdPorNombreUsuario(string nombreUsuario)
        {
            int idUsuario = 0;
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioId", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        idUsuario = Convert.ToInt32(result);
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
            return idUsuario;
        }

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
                            string nombre = reader["usuario_nombre"].ToString();

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

        public int GuardarFotoRostro(int usuarioId, byte[] rostroData)
        {
            int nuevoRostroId = 0;

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("SP_GuardarFotos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Usuario_id", usuarioId);
                    command.Parameters.AddWithValue("@RostroData", rostroData);

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

        public int ContarFotosUsuario(int usuarioId)
        {
            int total = 0;

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_ContarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);

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

        public void BorrarFotosUsuario(int usuarioId)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_BorrarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);
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

        public string ObtenerNombreUsuario(int usuarioId)
        {
            string nombre = "Desconocido";
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerNombreUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);
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

        public List<byte[]> ObtenerRostrosPorUsuario(int usuarioId)
        {
            List<byte[]> lista = new List<byte[]>();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerRostrosPorUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);
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

        public Usuario ObtenerUsuarioCompleto(int usuarioId)
        {
            Usuario usuario = null;

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioCompleto", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuarioId);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Usuario_id = Convert.ToInt32(dr["Usuario_id"]),
                            usuario_nombre = dr["usuario_nombre"].ToString(),
                            Rol_id = Convert.ToInt32(dr["Rol_id"]),
                            Id_estado_cuenta = Convert.ToInt32(dr["Id_estado_cuenta"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario completo: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return usuario;
        }

    }
}