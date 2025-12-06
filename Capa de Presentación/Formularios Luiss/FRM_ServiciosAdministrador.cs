
using Capa_de_Presentación.Formularios_Diego;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_ServiciosAdministrador : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_ServiciosAdministrador"/> class.
        /// </summary>
        public FRM_ServiciosAdministrador()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_TextChanged(object sender, EventArgs e)
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
        /// Handles the MouseClick event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; // este es el FRM_42

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new Reportería_Administrador())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {

                this.Close();
                main?.Show();
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the MouseClick event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form;

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new Bitacora_Admin())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {

                this.Close();
                main?.Show();
            }

        }

        /// <summary>
        /// Handles the Click event of the pictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pibBitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pibBitacora_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 2. Creamos el nuevo formulario y lo manejamos con 'using' para asegurar su descarte.
            using (var reconocimiento = new Capa_de_Presentación.RECONOCIMIENTO_FACIAL.RECONOCIMIENTO_FACIAL())
            {
                // 3. Lo mostramos de forma MODAL. El código se detiene aquí hasta que se cierra 'reconocimiento'.
                reconocimiento.ShowDialog(this);
            }

            // 4. Una vez que 'reconocimiento' se cierra, cerramos el formulario actual 'this'.
            // Si este formulario (Servicios) fue abierto por otro (dueño/Owner), 
            // el código del dueñó se encargará de mostrarlo de nuevo o cerrarlo.
            this.Close();


        }

        /// <summary>
        /// Handles the Click event of the pictureBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pibGenerarReportes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pibGenerarReportes_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the MouseClick event of the textBox5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {

            this.Hide();


            using (var alerta = new Capa_de_Presentación.ALERTA.ALERTA_SISTEMA())
            {

                alerta.ShowDialog(this);
            }


            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the pictureBox6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the pictureBox5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pictureBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the textBox2_TextChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_ServiciosAdministrador control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_ServiciosAdministrador_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {

            var main = this.Owner as Form;

            try
            {
                // Oculta el formulario principal (main) y el formulario actual (this)
                main?.Hide();
                this.Hide();

                // Abre el formulario Certificados_De_Depósito de forma modal.
                // Usamos 'using' para asegurar que el formulario se deseche correctamente al cerrarse.
                using (var frm = new Certificados_De_Depósito()) //
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                // Cierra el formulario actual.
                this.Close();

                // Muestra el formulario principal que se había ocultado.
                main?.Show();
            }
        }
    }
}
