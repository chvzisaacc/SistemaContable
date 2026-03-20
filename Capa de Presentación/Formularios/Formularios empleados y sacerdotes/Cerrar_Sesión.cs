using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class Cerrar_Sesión : Form
    {
        private readonly clsCRUD_Usuarios _repo = new clsCRUD_Usuarios();

        public Cerrar_Sesión()
        {
            InitializeComponent();
            this.Shown += (_, __) => CargarCorreo(); // síncrono para ir a juego con CRUD
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void CargarCorreo()
        {
            try
            {
                string correo = _repo.ObtenerCorreoPorUsuario(Sesion1.usuario_id);
                label1.Text = string.IsNullOrWhiteSpace(correo)
                    ? "Sin correo / cuenta inactiva"
                    : correo;
            }
            catch (Exception ex)
            {
                label1.Text = "Error obteniendo correo";
                // opcional: log ex.Message
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. LIMPIAR DATOS DE SESIÓN
            Sesion1.CerrarSesion();

            // 2. CONFIRMAR CON USUARIO
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de cerrar sesión?",
                "Confirmar Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // 3. REINICIAR APLICACIÓN DESDE LOGIN FRESCO
                Application.Restart();
            }
        }
    }
}
