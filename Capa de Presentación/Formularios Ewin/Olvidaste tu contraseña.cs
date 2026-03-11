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
            this.FormClosing += cerrar.CerrarApp;
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG2_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the FRM_PG2_Load control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG2_Load_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the label2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the button1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click_1(object sender, EventArgs e)
        {

            ClsValidaciones validar = new ClsValidaciones();
            string correo = txt_correo_electronico.Text.Trim();
            string usuario = txtUsuario.Text.Trim();

            //Validar ambos campos al mismo tiempo

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor complete todos los campos (Usuario y Correo).");
                return;
            }

            // Validar que no esté vacío
            /*if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor ingrese su correo electrónico.");
                return;
            }*/

            // Validar que el formato del correo sea válido
            if (!validar.EsCorreoValido(correo))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
                return;
            }

            //Validar que no este vacio
            /*if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor ingrese su correo electrónico.");
                return;
            }*/

            /*if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Por favor ingrese su usuario.");
                return;
            }*/
            //instacnia 
            ClsAccionesDB acciones = new ClsAccionesDB();
            int usuario_id = acciones.ObtenerUsuarioIdPorCorreo(usuario,correo);

            if (usuario_id == 0)
            {
                MessageBox.Show("Los datos ingresados no coinciden con ningún registro.");
                return;
            }
            //metodo de generar codigo login
            var sistema = new Capa_de_acceso_de_datos.CORREO.Sistema();

            string codigo = sistema.GenerarCodigo();
            acciones.GuardarCodigoRecuperacion(usuario_id, codigo);
            sistema.EnviarCodigoVerificacion(correo, codigo);

            MessageBox.Show("Se ha enviado un código de verificación a su correo.");

            FRM_PG3 objingresar = new FRM_PG3(usuario_id, correo, usuario);
            objingresar.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the label4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label4_Click(object sender, EventArgs e)
        {
            FRM_PG1 fRM_PG1 = new();
            fRM_PG1.Show();
            this.Hide();
        }
    }
}
