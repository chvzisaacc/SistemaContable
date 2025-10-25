using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class clsCRUD_CatalogoCuentas
    {
        private Clsconexion conexion;

        public clsCRUD_CatalogoCuentas()
        {
            conexion = new Clsconexion();
        }

        public int AgregarCatalogoCuenta(int idCuenta, string nombre, decimal? detalle, decimal? saldo)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_AgregarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada
                cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@detalle", detalle.HasValue ? (object)detalle.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@saldo", saldo.HasValue ? (object)saldo.Value : DBNull.Value);

                // Parámetro de salida para obtener el Código generado
                SqlParameter nuevoId = new SqlParameter("@nuevoId", SqlDbType.Int);
                nuevoId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nuevoId);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(nuevoId.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cuenta al catálogo: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // OBTENER todas las cuentas del catálogo
        public DataTable ObtenerCatalogoCuentas()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCatalogoCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener catálogo de cuentas: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // BUSCAR cuenta por Código
        public DataRow BuscarCatalogoCuentaPorId(int codCuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_BuscarCatalogoCuentaPorId", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", codCuenta);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // MODIFICAR cuenta
        public bool ModificarCatalogoCuenta(int codCuenta, int idCuenta, string nombre, decimal? detalle, decimal? saldo)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ModificarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codCuenta", codCuenta);
                cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@detalle", detalle.HasValue ? (object)detalle.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@saldo", saldo.HasValue ? (object)saldo.Value : DBNull.Value);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // ELIMINAR cuenta
        public bool EliminarCatalogoCuenta(int codCuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_EliminarCatalogoCuenta", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", codCuenta);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // VALIDAR si nombre_cuenta existe
        public bool CatalogoCuentaExiste(string nombreCuenta)
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_CatalogoCuentaExiste", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreCuenta", nombreCuenta);

                SqlParameter existe = new SqlParameter("@existe", SqlDbType.Bit);
                existe.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(existe);

                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(existe.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar cuenta: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // OBTENER próximo Código
        public int ObtenerProximoCodigo()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerProximoCodigoCatalogo", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                int proximoCodigo = (int)cmd.ExecuteScalar();
                return proximoCodigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo código: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // OBTENER cuentas para ComboBox (Ingresos y Egresos)
        public DataTable ObtenerCuentas()
        {
            try
            {
                conexion.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCuentas", conexion.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cuentas: " + ex.Message, ex);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
