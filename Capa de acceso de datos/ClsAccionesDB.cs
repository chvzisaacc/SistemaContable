using Emgu.CV; // Necesario para Mat, VideoCapture, CascadeClassifier
using Emgu.CV.CvEnum; // Necesario para ImreadModes y otros Enums
using Emgu.CV.Structure; // Necesario para Image<TColor, TDepth>
using Microsoft.Data.SqlClient;
using System.Data;

// Nota: Asumo que Clsconexion es la clase base que contiene sc (SqlConnection), Abrir() y Cerrar().

namespace Capa_de_acceso_de_datos
{

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

        public List<(int Id, Image<Gray, byte> Rostro)> CargarImagenesBD(CascadeClassifier detector)
        {
            List<(int, Image<Gray, byte>)> lista = new List<(int, Image<Gray, byte>)>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ObtenerPersonas", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                int id = dr.GetInt32(dr.GetOrdinal("Usuario_id"));
                                int rostroDataOrdinal = dr.GetOrdinal("RostroData");

                                if (dr.IsDBNull(rostroDataOrdinal))
                                {
                                    Console.WriteLine($"Usuario {id}: RostroData es NULL");
                                    continue;
                                }

                                byte[] fotoBytes = (byte[])dr["RostroData"];

                                if (fotoBytes.Length == 0)
                                {
                                    Console.WriteLine($"Usuario {id}: RostroData vacío");
                                    continue;
                                }

                                // Convertimos directamente el byte[] en Mat
                                using (Mat m = new Mat())
                                {
                                    CvInvoke.Imdecode(fotoBytes, ImreadModes.Grayscale, m);

                                    if (m.IsEmpty)
                                    {
                                        Console.WriteLine($"Usuario {id}: RostroData vacío");
                                        continue;
                                    }

                                    Image<Gray, byte> rostro = m.ToImage<Gray, byte>();

                                    // Asegurar tamaño correcto
                                    if (rostro.Width != 100 || rostro.Height != 100)
                                    {
                                        rostro = rostro.Resize(100, 100, Inter.Linear);
                                    }

                                    lista.Add((id, rostro.Clone()));
                                }
                            

                            }
                                
                            catch (Exception exInner)
                            {
                                Console.WriteLine($"Error procesando fila: {exInner.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar imágenes desde la BD: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return lista;
        }

        public ResultadoLogin ValidarUsuarioPorID(int usuarioID)
        {
            ResultadoLogin resultado = new ResultadoLogin();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("IngresoLoginPorIDReconocimientoFacial", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
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
                throw new Exception("Error al validar usuario por biometria: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }
    }
}