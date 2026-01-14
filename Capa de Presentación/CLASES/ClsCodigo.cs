using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class ClsCodigo : Clsconexion
    {

        /// <summary>
        /// The acciones
        /// </summary>
        private ClsAccionesDB acciones;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClsCodigo"/> class.
        /// </summary>
        public ClsCodigo()
        {
            acciones = new ClsAccionesDB();
        }

        /// <summary>
        /// Procesars the codigo recuperacion.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="codigo">The codigo.</param>
        /// <param name="correo">The correo.</param>
        /// <param name="formulario_actual">The formulario actual.</param>
        public void ProcesarCodigoRecuperacion(int usuario_id, string codigo, string correo, string nombreUsurio, Form formulario_actual)
        {
            string resultado = acciones.ValidarCodigoRecuperacion(usuario_id, codigo);

            switch (resultado)
            {
                case "CODIGO_VALIDO":
                    MessageBox.Show("Código verificado correctamente.");
                    Actualizar_Contraseña frm = new Actualizar_Contraseña(usuario_id, correo, nombreUsurio);
                    frm.Show();
                    formulario_actual.Hide();
                    break;

                case "CODIGO_INCORRECTO":
                    MessageBox.Show("Código incorrecto. Intente nuevamente.");
                    break;

                case "CODIGO_EXPIRADO":
                    MessageBox.Show("El código ha expirado. Solicite uno nuevo.");
                    break;

                case "CUENTA_INHABILITADA":
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
    }
}
