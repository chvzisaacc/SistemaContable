using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class Intereses_Por_Cds : Form
    {
        private DataTable dtDatosCertificados = null;
        public Intereses_Por_Cds()
        {
            InitializeComponent();
            CargarDatos();

        }

        private void FRM_PG114_Load(object sender, EventArgs e)
        {
            
        }

        public void CargarDatos()
        {
            try
            {
                ClsCD objCd = new();

                objCd.CargarCertificadosIntereses(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            Certificados_De_Depósito fRM_PG103 = new();
            fRM_PG103.Show();
            this.Hide();
        }
    }
}
