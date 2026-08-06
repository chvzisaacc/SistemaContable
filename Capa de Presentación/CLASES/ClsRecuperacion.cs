using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.CAPAS
{
    /// <summary>
    /// Clase de ayuda para procesos de recuperación y control de sesión relacionados
    /// con la autenticación y validación de códigos de recuperación.
    /// </summary>
    public class ClsRecuperacion
    {
        /// <summary>
        /// Inicia sesión mediante la capa de métodos y actualiza la UI con el resultado.
        /// - Llama a <see cref="ClsMetodos.IniciarSesion(string,string,int)"/> para validar credenciales.
        /// - Modifica el <paramref name="lblMensaje"/> para reflejar el estado de la autenticación.
        /// Devuelve el identificador de rol (-1 o 0 indican credenciales inválidas según la lógica actual).
        /// </summary>
        /// <param name="usuario">Nombre de usuario proporcionado.</param>
        /// <param name="contraseña">Contraseña proporcionada.</param>
        /// <param name="id_parroquia">Identificador de la parroquia asociado al intento de inicio.</param>
        /// <param name="fRM_PG1">Formulario principal que puede necesitar acciones adicionales (no usado aquí).</param>
        /// <param name="lblMensaje">Etiqueta de la UI que se actualiza con mensajes de estado.</param>
        /// <returns>Identificador de rol obtenido de la validación de sesión.</returns>
        public int IniciarSesion(string usuario, string contraseña, int id_parroquia, FRM_PG1 fRM_PG1, Label lblMensaje)
        {
            ClsMetodos metodos = new ClsMetodos();
            var sesion = metodos.IniciarSesion(usuario, contraseña, id_parroquia);
            int rol = sesion.rol_id;
            var ids = metodos.ObtenerUsuarioIdPorNombreUsuario(usuario);

            if (rol == 0)
            {
                lblMensaje.ForeColor = Color.White;
                lblMensaje.BackColor = Color.Transparent;
                lblMensaje.Text = "⚠ Credenciales incorrectas";
                lblMensaje.Font = new Font(lblMensaje.Font, FontStyle.Bold);
            }

            else if (rol == -1)
            {
                lblMensaje.ForeColor = Color.White;
                lblMensaje.BackColor = Color.Transparent;
                lblMensaje.Text = "⚠ Cuenta deshabilitada";
                lblMensaje.Font = new Font(lblMensaje.Font, FontStyle.Bold);

                // Se muestra el MessageBox solo cuando el rol es -1
                MessageBox.Show("Esta cuenta se encuentra deshabilitada o suspendida.\nPor favor, contacte al administrador del sistema.",
                                "Acceso Denegado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
            }
            return rol;
        }


        /// <summary>
        /// Procesa el código de recuperación: valida el código mediante la capa de datos
        /// y actúa según el resultado (abrir formulario de actualización, informar errores, o cerrar la app si procede).
        /// </summary>
        /// <param name="usuario_id">Identificador del usuario que solicita la recuperación.</param>
        /// <param name="codigo">Código de recuperación introducido.</param>
        /// <param name="formulario_actual">Formulario desde el que se invoca la verificación (se oculta al validar correctamente).</param>
        public void ProcesarCodigoRecuperacion(int usuario_id, string codigo, Form formulario_actual)
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                string resultado = acciones.ValidarCodigoRecuperacion(usuario_id, codigo);

                switch (resultado)
                {
                    case "CODIGO_VALIDO":
                        // Código válido: abrir formulario de actualización de contraseña y ocultar el actual
                        MessageBox.Show("Código verificado correctamente.");
                        Actualizar_Contraseña frm = new Actualizar_Contraseña();
                        frm.Show();
                        formulario_actual.Hide();
                        break;

                    case "CODIGO_INCORRECTO":
                        // Permitir reintento
                        MessageBox.Show("Código incorrecto. Intente nuevamente.");
                        break;

                    case "CODIGO_EXPIRADO":
                        // Informar y solicitar nuevo código
                        MessageBox.Show("El código ha expirado. Solicite uno nuevo.");
                        break;

                    case "CUENTA_INHABILITADA":
                        // Cuenta bloqueada: informar y cerrar aplicación
                        MessageBox.Show("Su cuenta ha sido bloqueada por seguridad.");
                        Application.Exit();
                        break;

                    case "SIN_CODIGO":
                        MessageBox.Show("No hay ningún código activo para este usuario.");
                        break;

                    default:
                        MessageBox.Show("Error: código no válido.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error sin exponer detalles internos
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Firma reservada: método no implementado actualmente.
        /// Se mantiene para compatibilidad de firmas si es necesario implementarlo luego.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Siempre lanzada hasta su implementación.</exception>

    }

}
