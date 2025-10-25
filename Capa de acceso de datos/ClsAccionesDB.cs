using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Capa_de_acceso_de_datos
{
    public class ClsAccionesDB : Clsconexion
    {
        public int ValidarCredenciales(string usuario, string contraseña)
        {
            int rol = 0;
            try
            {
                Abrir();
                SqlCommand cmd = new SqlCommand("IngresoLogin", sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Password", contraseña);

                object resultado = cmd.ExecuteScalar();

                if (resultado != null)
                {
                    rol = Convert.ToInt32(resultado);
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

            return rol;
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

    }
}
