namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Clase de utilidad que encapsula métodos de autenticación y sesión.
    /// Hereda de <see cref="ClsAccionesDB"/> para reutilizar operaciones de base de datos.
    /// Proporciona punto de entrada centralizado para validación y gestión de sesiones de usuario.
    /// </summary>
    public class ClsMetodos : ClsAccionesDB
    {
        /// <summary>
        /// Inicia sesión de usuario validando credenciales e inicializando contexto de sesión.
        /// Parámetros: usuario (nombre login), contraseña (credencial), id_parroquia (contexto de parroquia).
        /// Retorna tupla (rol_id, id_parroquia): rol_id del usuario y su parroquia asociada.
        /// Retorna (0, 0) si credenciales son inválidas.
        /// 
        /// Flujo:
        /// 1. Valida credenciales llamando a ValidarCredenciales() de clase base
        /// 2. Si usuario_id > 0: inicializa sesión global llamando a Sesion1.IniciarSesion()
        /// 3. Retorna tupla con rol e id_parroquia para contexto de aplicación
        /// 4. Si falla: retorna (0, 0) indicando fallo de autenticación
        /// 
        /// Sincronización: ValidarCredenciales dispara sincronización remota automática.
        /// </summary>
        public (int rol_id, int id_parroquia) IniciarSesion(string usuario, string contraseña, int id_parroquia)
        {
            ResultadoLogin resultado = null;
            try
            {
                resultado = ValidarCredenciales(usuario, contraseña, id_parroquia);

                if (resultado != null)
                {

                    if (resultado.rol_id == -1)
                    {
                        return (-1, 0);
                    }
                    // ----------------------------------------------

                    if (resultado.usuario_id > 0)
                    {
                        Sesion1.IniciarSesion(resultado.usuario_id, resultado.rol_id, resultado.id_parroquia);
                        return (resultado.rol_id, resultado.id_parroquia);
                    }
                }
                return (0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}