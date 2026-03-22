using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.CAPAS
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsRecuperacion
    {

        /// <summary>
        /// Iniciars the sesion.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <param name="contraseña">The contraseña.</param>
        /// <param name="id_parroquia">The identifier parroquia.</param>
        /// <param name="fRM_PG1">The f rm p g1.</param>
        /// <param name="lblMensaje">The label mensaje.</param>
        /// <returns></returns>
        public int IniciarSesion(string usuario, string contraseña, int id_parroquia, FRM_PG1 fRM_PG1, Label lblMensaje)
        {
            ClsMetodos metodos = new ClsMetodos();
            var sesion = metodos.IniciarSesion(usuario, contraseña, id_parroquia);
            int rol = sesion.rol_id;
            var ids = metodos.ObtenerUsuarioIdPorNombreUsuario(usuario);
            if (rol == -1)
            {
                lblMensaje.ForeColor = Color.White;
                lblMensaje.BackColor = Color.Transparent;
                lblMensaje.Text = "⚠ Credenciales incorrectas";
                lblMensaje.Font = new Font(lblMensaje.Font, FontStyle.Bold);
            }
            else if (rol == 0)
            {
                lblMensaje.ForeColor = Color.White;
                lblMensaje.BackColor = Color.Transparent;
                lblMensaje.Text = "⚠ Credenciales incorrectas";
                lblMensaje.Font = new Font(lblMensaje.Font, FontStyle.Bold);
            }
            return rol;
        }


        /// <summary>
        /// Procesars the codigo recuperacion.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <param name="codigo">The codigo.</param>
        /// <param name="formulario_actual">The formulario actual.</param>
        public void ProcesarCodigoRecuperacion(int usuario_id, string codigo, Form formulario_actual)
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                string resultado = acciones.ValidarCodigoRecuperacion(usuario_id, codigo);

                switch (resultado)
                {
                    case "CODIGO_VALIDO":
                        MessageBox.Show("Código verificado correctamente.");
                        Actualizar_Contraseña frm = new Actualizar_Contraseña();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Iniciars the sesion.
        /// </summary>
        /// <param name="text1">The text1.</param>
        /// <param name="text2">The text2.</param>
        /// <param name="v">The v.</param>
        /// <param name="rECONOCER">The r econocer.</param>
        /// <param name="label1">The label1.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        
    }

}
