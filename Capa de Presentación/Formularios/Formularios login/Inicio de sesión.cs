using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Formulario de inicio de sesión. Valida credenciales y establece la sesión en <c>Sesion1</c>.
    /// Contiene lógica de validación de campos y delega la autenticación a <see cref="ClsRecuperacion"/>.
    /// </summary>
    public partial class FRM_PG1 : Form
    {
        /// <summary>
        /// Identificador del usuario autenticado. Se establece tras iniciar sesión correctamente.
        /// </summary>
        public int UsuarioId { get; private set; }

        /// <summary>
        /// Rol del usuario autenticado. Se obtiene de la lógica de inicio de sesión.
        /// </summary>
        public int Rol { get; private set; }

        /// <summary>
        /// Identificador de la parroquia asociada al usuario autenticado.
        /// </summary>
        public int ParroquiaId { get; private set; }

        /// <summary>
        /// Constructor: inicializa componentes y configura la ventana.
        /// </summary>
        public FRM_PG1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Evento del botón de inicio de sesión. Valida campos, llama a la lógica de autenticación y
        /// en caso de éxito inicializa la sesión y cierra el formulario con DialogResult.OK.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            ClsValidaciones validaciones = new ClsValidaciones();

            if (string.IsNullOrWhiteSpace(txt_usuario.Text) || txt_usuario.Text == "Usuario")
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario.");
                return;
            }


            if (string.IsNullOrWhiteSpace(txt_contraseña.Text) || txt_contraseña.Text == "Contraseña")
            {
                MessageBox.Show("Por favor, ingrese una contraseña.");
                return;
            }



            if (txt_contraseña.Text.Contains(" "))
            {
                MessageBox.Show("La contraseña no puede contener espacios.");
                return;
            }

            // Lógica de autenticación: delega a ClsRecuperacion
            ClsRecuperacion login = new ClsRecuperacion();
            int rol = login.IniciarSesion(txt_usuario.Text, txt_contraseña.Text, 0, this, label1);

            if (rol <= 0)
                return;

            // Obtener identificador y parroquia del usuario autenticado
            ClsAccionesDB acciones = new ClsAccionesDB();
            var resultado = acciones.ValidarCredenciales(txt_usuario.Text, txt_contraseña.Text, 0);

            UsuarioId = resultado.usuario_id;
            ParroquiaId = resultado.id_parroquia;
            Rol = resultado.rol_id;

            // Iniciar sesión global
            Sesion1.IniciarSesion(UsuarioId, Rol, ParroquiaId);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Muestra el formulario de recuperación de contraseña.
        /// </summary>
        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var frm = new Olvidaste_tu_contraseña())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
            this.Show();
        }

        /// <summary>
        /// Controlador para el clic en el placeholder del usuario: limpia el texto y ajusta el color.
        /// </summary>
        private void txtUsuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario.Text == "Usuario")
            {
                txt_usuario.Text = "";
                txt_usuario.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Restaurar placeholder si el campo usuario queda vacío al salir del control.
        /// </summary>
        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_usuario.Text))
            {
                txt_usuario.Text = "Usuario";
                txt_usuario.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Limpia el placeholder de contraseña al hacer foco.
        /// </summary>
        private void txtContraseña_Click(object sender, EventArgs e)
        {
            if (txt_contraseña.Text == "Contraseña")
            {
                txt_contraseña.Text = "";
                txt_contraseña.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Restaura el placeholder de contraseña si queda vacío.
        /// </summary>
        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_contraseña.Text))
            {
                txt_contraseña.Text = "Contraseña";
                txt_contraseña.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Controlador reservado para el PictureBox principal (sin lógica actual).
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Restringe la entrada en el campo usuario: evita espacio inicial y espacios en general.
        /// </summary>
        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones v = new ClsValidaciones();

            if (!v.NoPermitirEspacioInicial(txt_usuario.Text, e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == ' ')
                e.Handled = true;
        }

        /// <summary>
        /// Restringe la entrada en el campo contraseña: evita espacio inicial y espacios en general.
        /// </summary>
        private void txt_contraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones val = new ClsValidaciones();

            if (!val.NoPermitirEspacioInicial(txt_contraseña.Text, e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == ' ')
                e.Handled = true;
        }

        /// <summary>
        /// Muestra la contraseña (quita el enmascarado).
        /// </summary>
        private void pbMostrar_Click(object sender, EventArgs e)
        {
            pbOcultar.BringToFront();
            txt_contraseña.PasswordChar = '\0';
        }

        /// <summary>
        /// Oculta la contraseña (activa el enmascarado).
        /// </summary>
        private void pbOcultar_Click(object sender, EventArgs e)
        {
            pbMostrar.BringToFront();
            txt_contraseña.PasswordChar = '*';
        }

        /// <summary>
        /// Evento Load del formulario. Reservado para inicializaciones futuras.
        /// </summary>
        private void FRM_PG1_Load(object sender, EventArgs e)
        {

        }
    }
}
