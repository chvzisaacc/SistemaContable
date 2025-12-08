using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Luiss;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_PG1 : Form
    {
        /// <summary>
        /// The cerrar
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();
        /// <summary>
        /// The usuario identifier
        /// </summary>
        private int usuarioID;

        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_PG1"/> class.
        /// </summary>
        public FRM_PG1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormClosing += cerrar.CerrarApp;
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG1_Load(object sender, EventArgs e)
        {

        }

        private void FRM_PG1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si la aplicación se está cerrando completamente (por el usuario en X o Alt+F4),
            // debemos permitirlo. Si es por el botón de Login/Logout, solo queremos ocultarlo.

            // Comprueba si no hay otros formularios abiertos (solo el Login)
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] == this)
            {
                // Si el usuario presiona X y es el último formulario, permite el cierre de la aplicación
                // o pide confirmación.
            }
            // Si el Login se está cerrando después de un login exitoso, 
            // solo ocúltalo si no hay otra razón explícita para cerrarlo.
        }

        // Opcional: Define explícitamente el comportamiento de cierre del Login
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Ocultar en lugar de cerrar si no se está cerrando la aplicación por completo
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            // En un login, si presiona X, podemos querer cerrar la app. 
            // Pero si se cierra al hacer Login exitoso, se maneja con this.Hide() desde el botón.
            //}
            //base.OnFormClosing(e);
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Instancia
            ClsValidaciones validaciones = new ClsValidaciones();


            //Validaciones
            if (string.IsNullOrWhiteSpace(txt_usuario.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario.");
                return;
            }
            if (!validaciones.EsUsuarioValidoRango(txt_usuario.Text))
            {
                MessageBox.Show("El usuario debe tener entre 3 y 20 caracteres y usar solo letras, números, punto o guion bajo.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_contraseña.Text))
            {
                MessageBox.Show("Por favor, ingrese una contraseña.");
                return;
            }
            else if (!validaciones.EsContraseñaValida(txt_contraseña.Text)) 
            {
                MessageBox.Show("La contraseña debe tener entre 6 y 30 caracteres.");
                return;
            }

            // VALIDAR LOGIN
            ClsRecuperacion login = new ClsRecuperacion();
            int rol = login.IniciarSesion(txt_usuario.Text, txt_contraseña.Text, 0, this, label1);

            if (rol <= 0)
                return;

            // OBTENER ID DE USUARIO Y PARROQUIA
            ClsAccionesDB acciones = new ClsAccionesDB();
            var resultado_tuple = acciones.ObtenerUsuarioIdPorNombreUsuario(txt_usuario.Text);

            int id_usuario = resultado_tuple.Item1;
            int parroquia_id = resultado_tuple.Item2;

            // Guardar sesión con ambos valores
            Sesion1.IniciarSesion(id_usuario, rol, parroquia_id);

            //REDIRECCIONAR AL FORM SEGÚN Roles 
            if (rol == 1)
            {
                this.Hide();
                using (var admin = new Ventana_Principal_Administrador(id_usuario, parroquia_id))
                {
                    admin.ShowDialog();
                }
                this.Show();
            }
            else if (rol == 2 || rol == 3)
            {
                this.Hide();
                using (var emp = new FRM_42(id_usuario, parroquia_id))
                {
                    emp.ShowDialog();
                }
                this.Show();
            }
        }




        /// <summary>
        /// Handles the Click event of the label3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label3_Click(object sender, EventArgs e)
        {
            //moviminento de frm
            Olvidaste_tu_contraseña objrecu = new Olvidaste_tu_contraseña();
            objrecu.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the txtUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtUsuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario.Text == "Usuario")
            {
                txt_usuario.Text = "";
                txt_usuario.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the Leave event of the txtUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_usuario.Text))
            {
                txt_usuario.Text = "Usuario";
                txt_usuario.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Handles the Click event of the txtContraseña control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtContraseña_Click(object sender, EventArgs e)
        {
            if (txt_contraseña.Text == "Contraseña")
            {
                txt_contraseña.Text = "";
                txt_contraseña.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the Leave event of the txtContraseña control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_contraseña.Text))
            {
                txt_contraseña.Text = "Contraseña";
                txt_contraseña.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Handles the Click event of the pictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Instancia del reconocimineto facial
            RECONOCIMIENTO_FACIAL.RECONOCER rECONOCER = new();
            rECONOCER.Show();
            this.Hide();
        }
    }
}
