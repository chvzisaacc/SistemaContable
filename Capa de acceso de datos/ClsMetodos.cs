namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.ClsAccionesDB" />
    public class ClsMetodos : ClsAccionesDB
    {
        /// <summary>
        /// Iniciars the sesion.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <param name="contraseña">The contraseña.</param>
        /// <param name="id_parroquia">The identifier parroquia.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al iniciar sesión: " + ex.Message</exception>
        public int IniciarSesion(string usuario, string contraseña, int id_parroquia)
        {

            ResultadoLogin resultado = null;

            try
            {
                Abrir();


                resultado = ValidarCredenciales(usuario, contraseña, id_parroquia);

                if (resultado != null && resultado.usuario_id > 0)
                {

                    Sesion1.IniciarSesion(resultado.usuario_id, resultado.rol_id, resultado.id_parroquia);


                    return resultado.rol_id;
                    return resultado.id_parroquia;
                }

                return 0;


            }
            catch (Exception ex)
            {

                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
            finally
            {
                // Asegurar el cierre de la conexión
                Cerrar();
            }
        }



    }
}
