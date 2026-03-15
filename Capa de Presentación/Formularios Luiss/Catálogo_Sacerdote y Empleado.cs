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
                dgvCatalogoUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
                dgvCatalogoUsuarios.DataSource = crudCatalogoCuentas.ObtenerCatalogoCuentas();

                // 1. Ocultar columnas de IDs (Ya lo tenías, mantenlo)
                if (dgvCatalogoUsuarios.Columns["CuentaID"] != null) dgvCatalogoUsuarios.Columns["CuentaID"].Visible = false;
                if (dgvCatalogoUsuarios.Columns["id_cuenta"] != null) dgvCatalogoUsuarios.Columns["id_cuenta"].Visible = false;
                if (dgvCatalogoUsuarios.Columns["EstadoID"] != null) dgvCatalogoUsuarios.Columns["EstadoID"].Visible = false;

                // 2. Configurar el salto de línea (WrapMode) y ajuste de filas
                dgvCatalogoUsuarios.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Permite múltiples renglones
                dgvCatalogoUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Ajusta la altura según el texto

                
              
                dgvCatalogoUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                //Ajustamos los anchos manualmente 
                if (dgvCatalogoUsuarios.Columns["Nombre"] != null) dgvCatalogoUsuarios.Columns["Nombre"].Width = 150;

                if (dgvCatalogoUsuarios.Columns["Detalle"] != null)
                {
                    dgvCatalogoUsuarios.Columns["Detalle"].Width = 250; // Al ser estrecho, se repartirá en más renglones
                }

                dgvCatalogoUsuarios.Columns["Nombre"].MinimumWidth = 200;
                dgvCatalogoUsuarios.Columns["Detalle"].MinimumWidth = 350;
                dgvCatalogoUsuarios.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);

                dgvCatalogoUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

        private void lblConsulte_Click(object sender, EventArgs e)
        {

        }
    }
}
