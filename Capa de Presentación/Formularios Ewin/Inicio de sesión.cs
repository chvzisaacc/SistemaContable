using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Luiss;
using Capa_de_procesamiento_de_datos;
using System;
using System.Drawing;
using System.Windows.Forms;

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

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ClsValidaciones validaciones = new ClsValidaciones();

            if (!validaciones.EsUsuarioValido(txt_usuario.Text))
            {
                MessageBox.Show("Por favor, ingrese un usuario válido.");
                return;
            }

            if (!validaciones.EsContraseñaValida(txt_contraseña.Text))
            {
                MessageBox.Show("La contraseña debe tener entre 4 y 25 caracteres.");
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
                Ventana_Principal_Administrador admin = new Ventana_Principal_Administrador(id_usuario, parroquia_id);
                admin.Show();
                this.Hide();
            }
            else if (rol == 2 || rol == 3)
            {
                FRM_42 empleado = new FRM_42(id_usuario, parroquia_id);
                empleado.Show();
                this.Hide();
            }
        }




        /// <summary>
        /// Handles the Click event of the label3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label3_Click(object sender, EventArgs e)
        {
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
            RECONOCIMIENTO_FACIAL.RECONOCER rECONOCER = new();
            rECONOCER.Show();
            this.Hide();
        }
    }
}
