using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Encapsula la lógica para verificar códigos de recuperación y coordinar
    /// la transición entre formularios cuando el código es válido.
    /// </summary>
    public class ClsVerificarCod
    {
        /// <summary>
        /// Valida un código de recuperación mediante la capa de datos y actúa según
        /// el resultado: abre el formulario de actualización de contraseña si es válido,
        /// informa al usuario en caso de error o cierra la aplicación si la cuenta está inhabilitada.
        /// Mantiene sincronizada la UI ocultando/mostrando formularios según corresponda.
        /// </summary>
        /// <param name="usuario_id">Identificador del usuario asociado al código.</param>
        /// <param name="codigo">Código de recuperación proporcionado por el usuario.</param>
        /// <param name="correo_usuario">Correo del usuario (pasado al formulario de actualización).</param>
        /// <param name="nombreUsuario">Nombre de usuario (pasado al formulario de actualización).</param>
        /// <param name="formulario_actual">Formulario que invoca la verificación; se oculta y cierra cuando procede.</param>
        public void ProcesarCodigoRecuperacion(int usuario_id, string codigo, string correo_usuario, string nombreUsuario, Form formulario_actual)
        {
            try
            {
                // Delegar la validación a la capa de acceso a datos
                ClsAccionesDB acciones = new ClsAccionesDB();
                string resultado = acciones.ValidarCodigoRecuperacion(usuario_id, codigo);

                switch (resultado)
                {
                    case "CODIGO_VALIDO":
                        // Código válido: abrir formulario modal de actualización y cerrar el actual
                        MessageBox.Show("Código verificado correctamente.");
                        formulario_actual.Hide();
                        using (var frm = new Actualizar_Contraseña(usuario_id, correo_usuario, nombreUsuario))
                        {
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            // Mostrar modal para asegurar sincronía antes de continuar
                            frm.ShowDialog(formulario_actual);
                        }
                        formulario_actual.Close();
                        break;

                    case "CODIGO_INCORRECTO":
                        // Permitir reintento del usuario sin cambiar estados persistentes
                        MessageBox.Show("Código incorrecto. Intente nuevamente.");
                        break;

                    case "CODIGO_EXPIRADO":
                        // Informar caducidad para solicitar nuevo código
                        MessageBox.Show("El código ha expirado. Solicite uno nuevo.");
                        break;

                    case "CUENTA_INHABILITADA":
                        // Cuenta bloqueada: informar y solicitar salida de la aplicación
                        MessageBox.Show("Su cuenta ha sido bloqueada por seguridad.");
                        Application.Exit();
                        break;

                    case "SIN_CODIGO":
                        // No hay código activo para el usuario
                        MessageBox.Show("No hay ningún código activo para este usuario.");
                        break;

                    default:
                        // Resultado inesperado: mensaje genérico
                        MessageBox.Show("Error: código no válido.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Mostrar error sin exponer detalles internos adicionales
                MessageBox.Show("Error al verificar el código: " + ex.Message);
            }
        }
    }
}
