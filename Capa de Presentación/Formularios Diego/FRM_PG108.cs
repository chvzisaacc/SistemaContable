namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class FRM_PG108 : Form
    {
        private DataGridViewTextBoxColumn id_Certificado;

        public FRM_PG108()
        {
            InitializeComponent();
        }

        public FRM_PG108(DataGridViewTextBoxColumn id_Certificado)
        {
            this.id_Certificado = id_Certificado;
        }
        public string ObtenerMotivo()
        {
            if (textBox1 != null)
            {
                return textBox1.Text.Trim();
            }
            return string.Empty;
        }
        private void FRM_PG108_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Ingresar el motivo de la cancelación para continuar.","Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;

            
            this.Close();

        }
        

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
