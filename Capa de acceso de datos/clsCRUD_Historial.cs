using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_Historial
    {
        private Clsconexion conexion;

        public clsCRUD_Historial()
        {
            conexion = new Clsconexion();   
        }

        public DataTable ObtenerHistorial(int? parroquiaId = null, int? usuarioId = null)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorial", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@parroquiaId", parroquiaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerUsuariosPorParroquia(int? parroquiaId = null)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuariosPorParroquia", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parroquiaId", parroquiaId ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public void RegistrarInicioSesion(int usuarioId)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_RegistrarInicioSesion", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar inicio de sesión: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerHistorialSacerdote()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorialSacerdote", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public void RegistrarActividad(int usuarioId, int moduloId, string tarea, string descripcion)
        {
            try
            {
                conexion.Abrir();

                using (SqlCommand cmd = new SqlCommand("sp_RegistrarActividad", conexion.sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_usuario", usuarioId);
                    cmd.Parameters.AddWithValue("@id_modulo", moduloId);
                    cmd.Parameters.AddWithValue("@tarea_realizada", tarea);
                    cmd.Parameters.AddWithValue("@descripcion_tarea", descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el historial: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }


    }
}
