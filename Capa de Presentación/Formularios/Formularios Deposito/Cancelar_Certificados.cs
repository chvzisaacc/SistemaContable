namespace Capa_de_Presentación.Formularios_Diego
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Cancelar_Certificados : Form
    {
        /// <summary>
        /// The identifier certificado
        /// </summary>
        private DataGridViewTextBoxColumn id_Certificado;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cancelar_Certificados"/> class.
        /// </summary>
        public Cancelar_Certificados()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cancelar_Certificados"/> class.
        /// </summary>
        /// <param name="id_Certificado">The identifier certificado.</param>
        public Cancelar_Certificados(DataGridViewTextBoxColumn id_Certificado)
        {
            this.id_Certificado = id_Certificado;
        }
        /// <summary>
        /// Obteners the motivo.
        /// </summary>
        /// <returns></returns>
        public string ObtenerMotivo()
        {
            if (textBox1 != null)
            {
                return textBox1.Text.Trim();
            }
            return string.Empty;
        }
        /// <summary>
        /// Handles the Load event of the FRM_PG108 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG108_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the Click event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_Click(object sender, EventArgs e)
        {

            /*if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Ingresar el motivo de la cancelación para continuar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;


            this.Close();*/

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Ingresar el motivo de la cancelación para continuar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;


            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

    }
}
