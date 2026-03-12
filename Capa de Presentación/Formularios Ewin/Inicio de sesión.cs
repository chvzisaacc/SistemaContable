using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Luiss;
using Capa_de_Presentación.RECONOCIMIENTO_FACIAL;

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
        private async void button1_Click(object sender, EventArgs e)
        {
            //Instancia
            ClsValidaciones validaciones = new ClsValidaciones();


            //Validaciones
            if (string.IsNullOrWhiteSpace(txt_usuario.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario.");
                return;
            }

            if (txt_usuario.Text == "Usuario")
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
            if (txt_contraseña.Text == "Contraseña")
            {
                MessageBox.Show("Por favor, ingrese una contraseña.");
                return;
            }

            else if (!validaciones.EsContraseñaValida(txt_contraseña.Text))
            {
                MessageBox.Show("La contraseña debe tener entre 6 y 30 caracteres.");
                return;
            }

            if (txt_contraseña.Text.Contains(" "))
            {
                MessageBox.Show("La contraseña no puede contener espacios.");
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

            bool hayInternet = await AccesoRemoto.VerificarConexion();

            if (!hayInternet)
            {
                var result = MessageBox.Show(
                    "No se detectó conexión con el servidor remoto de la parroquia.\n\n" +
                    "¿Desea entrar en MODO LOCAL?",
                    "Servidor Desconectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;
            }

            Form f;
            //REDIRECCIONAR AL FORM SEGÚN Roles 
            if (rol == 1)
            {
                f = new Ventana_Principal_Administrador(id_usuario, parroquia_id);
            }
            else
            {
                f = new FRM_42(id_usuario, parroquia_id);
            }

            // 2. MOSTRAMOS LA VENTANA de forma independiente
            f.Show();

            // 3. Cerramos el login de forma segura
            // Nota: Para que esto no cierre TODA la app, asegúrate de que en Program.cs 
            // tu Application.Run() esté configurado correctamente o usa this.Hide() 
            // si el Login es el formulario principal.
            this.Hide();
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
            try
            {
                using (RECONOCIMIENTO_FACIAL.RECONOCER reconocer = new())
                {
                    this.Hide();
                    var result = reconocer.ShowDialog();

                    if (result == DialogResult.OK)
                    {

                        // 1. Recuperamos los datos que guardó la Sesion1
                        int rol = Capa_de_acceso_de_datos.Sesion1.rol_id;
                        int p_id = Capa_de_acceso_de_datos.Sesion1.usuario_id;
                        int par_id = Capa_de_acceso_de_datos.Sesion1.id_parroquia;

                        Form f;
                        // 2. Decidimos qué ventana abrir
                        if (rol == 1)
                        {
                            f = new Ventana_Principal_Administrador(p_id, par_id);
                        }
                        else
                        {
                            f = new FRM_42(p_id, par_id);
                        }

                        // 3. MOSTRAMOS LA VENTANA
                        f.Show();

                        // 4. Cerramos el login de forma segura
                        this.Close();
                    }
                    else
                    {
                        this.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir biometría: " + ex.Message);
                this.Show();
            }
        }

        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones v = new ClsValidaciones();
            if (!v.NoPermitirEspacioInicial(txt_usuario.Text, e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == ' ')
                e.Handled = true;
        }

        private void txt_contraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones val = new ClsValidaciones();
            if (!val.NoPermitirEspacioInicial(txt_contraseña.Text, e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == ' ')
                e.Handled = true;
        }

        private void pbMostrar_Click(object sender, EventArgs e)
        {
            //Imagen ocultar la mandamos al frente
            pbOcultar.BringToFront();
            txt_contraseña.PasswordChar = '\0'; // Mostrar contraseña
        }

        private void pbOcultar_Click(object sender, EventArgs e)
        {
            //Imagen mostrar la mandamos al frente
            pbMostrar.BringToFront();
            txt_contraseña.PasswordChar = '*'; // Ocultar contraseña
        }
    }
}
