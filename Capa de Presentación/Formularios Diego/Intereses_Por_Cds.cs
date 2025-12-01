using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Diego
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Intereses_Por_Cds : Form
    {
        /// <summary>
        /// The dt datos certificados
        /// </summary>
        private DataTable dtDatosCertificados = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="Intereses_Por_Cds"/> class.
        /// </summary>
        public Intereses_Por_Cds()
        {
            InitializeComponent();
            CargarDatos();

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG114 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG114_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Cargars the datos.
        /// </summary>
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

        /// <summary>
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Paint event of the panel2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the CellContentClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        /// Handles the Click event of the textBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox4_Click(object sender, EventArgs e)
        {
            Certificados_De_Depósito fRM_PG103 = new();
            fRM_PG103.Show();
            this.Hide();
        }
    }
}
