using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación
{
    public partial class FRM_PG93 : Form
    {
        public FRM_PG93()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void FRM_PG93_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ClsValidaciones v = new ClsValidaciones();

            // Validar que no esté vacío y que solo tenga letras
            if (!ClsValidaciones.EsSoloLetras(txtCuenta.Text.Trim()))
            {
                MessageBox.Show("Ingrese una cuenta válida (solo letras, sin números).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si pasa la validación, aquí sigue el proceso de guardado
            MessageBox.Show("Cuenta registrada correctamente.");
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

