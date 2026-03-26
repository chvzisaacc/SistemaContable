using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;


namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Olvidaste_tu_contraseña : Form
    {
        /// <summary>
        /// The cerrar
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();
        /// <summary>
        /// Initializes a new instance of the <see cref="Olvidaste_tu_contraseña"/> class.
        /// </summary>
        public Olvidaste_tu_contraseña()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.FormClosing += cerrar.CerrarApp;
        }

        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the 1 event of the button1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// Handles the Click event of the label4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Olvidaste_tu_contraseña_Load(object sender, EventArgs e)
        {

        }

        private void btn_restablecer_contrasena_Click(object sender, EventArgs e)
        {
            ClsValidaciones validar = new ClsValidaciones();
            string correo = txt_correo_electronico.Text.Trim();
            string usuario = txtUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor complete todos los campos (Usuario y Correo).");
                return;
            }

            if (!validar.EsCorreoValido(correo))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
                return;
            }

            ClsAccionesDB acciones = new ClsAccionesDB();
            int usuario_id = acciones.ObtenerUsuarioIdPorCorreo(usuario, correo);

            if (usuario_id == 0)
            {
                MessageBox.Show("Los datos ingresados no coinciden con ningún registro.");
                return;
            }

            var sistema = new Capa_de_acceso_de_datos.CORREO.Sistema();
            string codigo = sistema.GenerarCodigo();
            acciones.GuardarCodigoRecuperacion(usuario_id, codigo);
            sistema.EnviarCodigoVerificacion(correo, codigo);
            MessageBox.Show("Se ha enviado un código de verificación a su correo.");

            this.Hide();
            using (var frm = new FRM_PG3(usuario_id, correo, usuario))
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this); 
            }
            this.Close(); 
        }
    }
}
