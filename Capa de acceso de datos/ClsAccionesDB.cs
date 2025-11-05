using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        public int UsuarioID { get; set; }
        public int RolID { get; set; }
    }
    public class ClsAccionesDB : Clsconexion
    {
        public ResultadoLogin ValidarCredenciales(string usuario, string contraseña)
        {
            ResultadoLogin resultado = new ResultadoLogin { UsuarioID = 0, RolID = 0 };

            int rol = 0;
            try
            {
                Abrir();
                SqlCommand cmd = new SqlCommand("IngresoLogin", sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Password", contraseña);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // Asegúrate que el SP devuelva estas columnas.
                        resultado.UsuarioID = Convert.ToInt32(dr["UsuarioID"]);
                        resultado.RolID = Convert.ToInt32(dr["RolID"]);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar usuario: " + ex.Message);
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
                throw;
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
                    if (result != null)
                        id = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ID de usuario: " + ex.Message);
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
                throw new Exception("Error al guardar código de recuperación: " + ex.Message);
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
                SqlCommand cmd = new SqlCommand("ValidarCodigoRecuperacion", sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    resultado = dr["Resultado"]?.ToString() ?? "SIN_RESULTADO";
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el código: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return resultado;
        }

        public void GuardarCertificado(string nombreCertificado, decimal depositoInicial, int plazo, decimal tasa)
        {
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
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar certificado de depósito: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public DataTable CargarCertificados()
        {
            try
            {
                DataTable dt = new DataTable();

                Abrir();

                SqlCommand cmd = new SqlCommand("sp_Mostrarcertificados", sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los certificados: " + ex.Message);
            }
            finally
            {

                Cerrar();
            }

        }

        public DataTable CargarCuentasBancarias()
        {
            try
            {
                DataTable dt = new DataTable();
                Abrir();
                SqlCommand cmd = new SqlCommand("sp_mostrarCuentas", sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los certificados: " + ex.Message);
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
                throw new Exception("Error al editar certificado de depósito: " + ex.Message);
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
                throw new Exception("Error al renovar el certificado de depósito: " + ex.Message);
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
                    int filas = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cancelar el certificado de depósito: " + ex.Message);
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
                            int id = Convert.ToInt32(reader["ID"]);
                            string nombre = reader["NombreOrigen"].ToString();

                            listaOrigenes.Add(new Origen(id, nombre));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar origen: " + ex.Message, ex);
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

                    return dtCuentas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las sugerencias para autocompletar: " + ex.Message);
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

                    return dtCuentas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las cuentas de gastos: " + ex.Message);
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
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerUsuarioIdPorNombre", sc))
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
                throw new Exception("Error al obtener ID de usuario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return idUsuario;
        }
    }
}
