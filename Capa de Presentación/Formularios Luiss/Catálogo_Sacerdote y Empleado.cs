using Capa_de_acceso_de_datos;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_PG46 : Form
    {

        /// <summary>
        /// The crud catalogo cuentas
        /// </summary>
        private clsCRUD_CatalogoCuentas crudCatalogoCuentas;
        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_PG46"/> class.
        /// </summary>
        public FRM_PG46()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            crudCatalogoCuentas = new clsCRUD_CatalogoCuentas();
        }

        /// <summary>
        /// Handles the CellContentClick event of the dgvBitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvBitacora_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG46 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG46_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.CenterToScreen();
        }

        /// <summary>
        /// Cargars the datos.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                dgvBitacora.DataSource = crudCatalogoCuentas.ObtenerCatalogoCuentas();

                if (dgvBitacora.Columns["CuentaID"] != null)
                    dgvBitacora.Columns["CuentaID"].Visible = false;

                if (dgvBitacora.Columns["EstadoID"] != null)
                    dgvBitacora.Columns["EstadoID"].Visible = false;


                /*
                if (dgvCatalogoCuentas.Columns["Detalle"] != null)
                    dgvCatalogoCuentas.Columns["Detalle"].DefaultCellStyle.Format = "N2";
                */

                if (dgvBitacora.Columns["Saldo"] != null)
                    dgvBitacora.Columns["Saldo"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnVolver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
