using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class FRM_PG114 : Form
    {
        private DataTable dtDatosCertificados = null;
        public FRM_PG114()
        {
            InitializeComponent();
            CargarDatos();
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

        private void FRM_PG114_Load(object sender, EventArgs e)
        {

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
            FRM_PG103 fRM_PG103 = new();
            fRM_PG103.Show();
            this.Hide();
        }
    }
}
