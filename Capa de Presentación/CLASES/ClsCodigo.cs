using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Provee funcionalidad relacionada con el procesamiento de códigos
    /// de recuperación de contraseña y acciones asociadas a la verificación.
    /// Hereda de <see cref="Capa_de_acceso_de_datos.Clsconexion"/> para disponer
    /// de utilidades de conexión si fueran necesarias.
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class ClsCodigo : Clsconexion
    {
        /// <summary>
        /// Instancia para acceder a las acciones de la capa de datos.
        /// Se usa para validar códigos y realizar operaciones relacionadas.
        /// </summary>
        private ClsAccionesDB acciones;

        /// <summary>
        /// Constructor que inicializa las dependencias necesarias.
        /// Mantiene la sincronía entre la capa de presentación y la de datos
        /// al delegar validaciones y búsquedas a <see cref="ClsAccionesDB"/>.
        /// </summary>
        public ClsCodigo()
        {
            acciones = new ClsAccionesDB();
        }

        /// <summary>
        /// Procesa el código de recuperación introducido por el usuario.
        /// Llama a la capa de datos para validar el código y actúa según el resultado:
        /// - Si es válido, abre el formulario de actualización de contraseña y oculta el actual.
        /// - Si es incorrecto, expirado o no existe, informa al usuario.
        /// - Si la cuenta está inhabilitada, solicita la salida de la aplicación.
        /// </summary>
        /// <param name="usuario_id">Identificador del usuario que solicita recuperación.</param>
        /// <param name="codigo">Código de recuperación proporcionado.</param>
        /// <param name="correo">Correo asociado al usuario (pasado al formulario de actualización).</param>
        /// <param name="nombreUsurio">Nombre de usuario (pasado al formulario de actualización).</param>
        /// <param name="formulario_actual">Formulario desde el cual se invoca; se ocultará si el código es válido.</param>
        public void ProcesarCodigoRecuperacion(int usuario_id, string codigo, string correo, string nombreUsurio, Form formulario_actual)
        {
            // Delegar la validación a la capa de datos para mantener separación de responsabilidades
            string resultado = acciones.ValidarCodigoRecuperacion(usuario_id, codigo);

            switch (resultado)
            {
                case "CODIGO_VALIDO":
                    // Código válido: abrir formulario de cambio de contraseña y ocultar el actual
                    MessageBox.Show("Código verificado correctamente.");
                    Actualizar_Contraseña frm = new Actualizar_Contraseña(usuario_id, correo, nombreUsurio);
                    frm.Show();
                    formulario_actual.Hide();
                    break;

                case "CODIGO_INCORRECTO":
                    // Informe al usuario y permita reintentar (sin cambios de estado)
                    MessageBox.Show("Código incorrecto. Intente nuevamente.");
                    break;

                case "CODIGO_EXPIRADO":
                    // Código caducado: solicitar generación de uno nuevo
                    MessageBox.Show("El código ha expirado. Solicite uno nuevo.");
                    break;

                case "CUENTA_INHABILITADA":
                    // Cuenta bloqueada por seguridad: informar y cerrar la aplicación
                    MessageBox.Show("Su cuenta ha sido bloqueada por seguridad.");
                    Application.Exit();
                    break;

                case "SIN_CODIGO":
                    // No existe código activo para el usuario
                    MessageBox.Show("No hay ningún código activo para este usuario.");
                    break;

                default:
                    // Resultado no esperado: aviso genérico
                    MessageBox.Show("Error: código no válido.");
                    break;
            }
        }
    }
}
