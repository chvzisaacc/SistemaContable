using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG1 : Form
    {
        public int UsuarioId { get; private set; }
        public int Rol { get; private set; }
        public int ParroquiaId { get; private set; }

        public FRM_PG1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsValidaciones validaciones = new ClsValidaciones();

            if (string.IsNullOrWhiteSpace(txt_usuario.Text) || txt_usuario.Text == "Usuario")
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario.");
                return;
            }

            if (!validaciones.EsUsuarioValidoRango(txt_usuario.Text))
            {
                MessageBox.Show("El usuario debe tener entre 3 y 20 caracteres y usar solo letras, números, punto o guion bajo.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_contraseña.Text) || txt_contraseña.Text == "Contraseña")
            {
                MessageBox.Show("Por favor, ingrese una contraseña.");
                return;
            }

            if (!validaciones.EsContraseñaValida(txt_contraseña.Text))
            {
                MessageBox.Show("La contraseña debe tener entre 6 y 30 caracteres.");
                return;
            }

            if (txt_contraseña.Text.Contains(" "))
            {
                MessageBox.Show("La contraseña no puede contener espacios.");
                return;
            }

            ClsRecuperacion login = new ClsRecuperacion();
            int rol = login.IniciarSesion(txt_usuario.Text, txt_contraseña.Text, 0, this, label1);

            if (rol <= 0)
                return;

            ClsAccionesDB acciones = new ClsAccionesDB();
            var resultado_tuple = acciones.ObtenerUsuarioIdPorNombreUsuario(txt_usuario.Text);

            UsuarioId = resultado_tuple.Item1;
            ParroquiaId = resultado_tuple.Item2;
            Rol = rol;

            Sesion1.IniciarSesion(UsuarioId, Rol, ParroquiaId);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

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

        private void txtUsuario_Click(object sender, EventArgs e)
        {
            if (txt_usuario.Text == "Usuario")
            {
                txt_usuario.Text = "";
                txt_usuario.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_usuario.Text))
            {
                txt_usuario.Text = "Usuario";
                txt_usuario.ForeColor = Color.Gray;
            }
        }

        private void txtContraseña_Click(object sender, EventArgs e)
        {
            if (txt_contraseña.Text == "Contraseña")
            {
                txt_contraseña.Text = "";
                txt_contraseña.ForeColor = Color.Black;
            }
        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_contraseña.Text))
            {
                txt_contraseña.Text = "Contraseña";
                txt_contraseña.ForeColor = Color.Gray;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
            pbOcultar.BringToFront();
            txt_contraseña.PasswordChar = '\0';
        }

        private void pbOcultar_Click(object sender, EventArgs e)
        {
            pbMostrar.BringToFront();
            txt_contraseña.PasswordChar = '*';
        }

        private void FRM_PG1_Load(object sender, EventArgs e)
        {

        }
    }
}
