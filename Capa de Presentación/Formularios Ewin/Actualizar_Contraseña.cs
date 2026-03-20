using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;


namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Actualizar_Contraseña : Form
    {
        /// <summary>
        /// The correo usuario
        /// </summary>
        private string nombreUsuario;
        private string correoUsuario;
        private int usuarioId;
        /// <summary>
        /// The cerrar
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();
        /// <summary>
        /// Initializes a new instance of the <see cref="Actualizar_Contraseña"/> class.
        /// </summary>
        /// <param name="correo">The correo.</param>
        public Actualizar_Contraseña(int id, string correo, string nombreUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.FormClosing += cerrar.CerrarApp;
            this.usuarioId = id;
            this.correoUsuario = correo;
            this.nombreUsuario = nombreUsuario;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actualizar_Contraseña"/> class.
        /// </summary>
        public Actualizar_Contraseña()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG4_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            FRM_PG1 fRM_PG1 = new();
            fRM_PG1.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnConfirmar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string nueva_contraseña = txt_nueva_contrasena.Text.Trim();
            string confirmar_contraseña = txt_confirmar_contrasena.Text.Trim();

            Validacion val = new Validacion(nueva_contraseña, confirmar_contraseña);
            ClsValidaciones validar = new ClsValidaciones();

            // Validar longitud de la contraseña
            if (!validar.EsContraseñaValida(nueva_contraseña))
            {
                MessageBox.Show("La contraseña debe tener entre 6 y 30 caracteres.");
                return;
            }

            if (!val.CamposIguales(nueva_contraseña))
            {
                MessageBox.Show("Las contraseñas no coinciden o están vacías.");
                return;
            }
            try
            {

                //Metodo para cambio
                ClsAccionesDB acciones = new ClsAccionesDB();

                acciones.CambiarContraseña(nombreUsuario, correoUsuario, nueva_contraseña);

                MessageBox.Show("Contraseña actualizada correctamente.");

                FRM_PG1 fRM_PG1 = new FRM_PG1();
                fRM_PG1.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the label4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label4_Click(object sender, EventArgs e)
        {
            //Movimineto de frm
            FRM_PG3 fRM_PG3 = new FRM_PG3(this.usuarioId, this.correoUsuario, this.nombreUsuario);
            fRM_PG3.Show();
            this.Hide();
        }
    }
}
