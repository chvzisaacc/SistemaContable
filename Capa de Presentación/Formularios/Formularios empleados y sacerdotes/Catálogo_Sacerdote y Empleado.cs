using Capa_de_acceso_de_datos;
namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_PG46 : Form
    {
        private clsCRUD_CatalogoCuentas crudCatalogoCuentas;

        public FRM_PG46()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            crudCatalogoCuentas = new clsCRUD_CatalogoCuentas();
        }

        private void FRM_PG46_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.CenterToScreen();
        }

        private void CargarDatos()
        {
            try
            {
                dgvCatalogoUsuarios.DataSource = crudCatalogoCuentas.ObtenerCatalogoCuentas();

                // Ocultar columnas de IDs
                if (dgvCatalogoUsuarios.Columns["CuentaID"] != null) dgvCatalogoUsuarios.Columns["CuentaID"].Visible = false;
                if (dgvCatalogoUsuarios.Columns["id_cuenta"] != null) dgvCatalogoUsuarios.Columns["id_cuenta"].Visible = false;
                if (dgvCatalogoUsuarios.Columns["EstadoID"] != null) dgvCatalogoUsuarios.Columns["EstadoID"].Visible = false;

                // Fuente y tamaño de encabezados
                dgvCatalogoUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);

                // Centrar encabezados
                dgvCatalogoUsuarios.ColumnHeadersDefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                // Centrar contenido de celdas
                dgvCatalogoUsuarios.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                // Salto de línea y ajuste de filas
                dgvCatalogoUsuarios.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvCatalogoUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                // Padding de celdas
                dgvCatalogoUsuarios.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);

                // Anchos manuales
                dgvCatalogoUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                if (dgvCatalogoUsuarios.Columns["Nombre"] != null)
                {
                    dgvCatalogoUsuarios.Columns["Nombre"].Width = 150;
                    dgvCatalogoUsuarios.Columns["Nombre"].MinimumWidth = 200;
                }

                if (dgvCatalogoUsuarios.Columns["Detalle"] != null)
                {
                    dgvCatalogoUsuarios.Columns["Detalle"].Width = 250;
                    dgvCatalogoUsuarios.Columns["Detalle"].MinimumWidth = 350;
                    // Detalle alineado a la izquierda por ser texto largo
                    dgvCatalogoUsuarios.Columns["Detalle"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleLeft;
                }

                // Fill al final para que ocupe todo el ancho
                dgvCatalogoUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}