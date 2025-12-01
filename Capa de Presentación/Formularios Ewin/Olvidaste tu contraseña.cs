using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

            // Validar que no esté vacío
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor ingrese su correo electrónico.");
                return;
            }

            // Validar que el formato del correo sea válido
            if (!validar.EsCorreoValido(correo))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
                return;
            }


            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor ingrese su correo electrónico.");
                return;
            }

            ClsAccionesDB acciones = new ClsAccionesDB();
            int usuario_id = acciones.ObtenerUsuarioIdPorCorreo(correo);

            if (usuario_id == 0)
            {
                MessageBox.Show("Este correo no está registrado.");
                return;
            }

            var sistema = new Capa_de_acceso_de_datos.CORREO.Sistema();

            string codigo = sistema.GenerarCodigo();
            acciones.GuardarCodigoRecuperacion(usuario_id, codigo);
            sistema.EnviarCodigoVerificacion(correo, codigo);

            MessageBox.Show("Se ha enviado un código de verificación a su correo.");

            FRM_PG3 objingresar = new FRM_PG3(usuario_id, correo);
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
