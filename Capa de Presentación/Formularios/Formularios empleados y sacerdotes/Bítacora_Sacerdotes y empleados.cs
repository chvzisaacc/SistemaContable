using Capa_de_acceso_de_datos;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_PG51 : Form
    {
        /// <summary>
        /// The identifier usuario login
        /// </summary>
        private int id_usuario_login;
        /// <summary>
        /// The crud historial
        /// </summary>
        private clsCRUD_Historial crudHistorial;

        private bool isLoading = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_PG51"/> class.
        /// </summary>
        /// <param name="id_usuario">The identifier usuario.</param>
        public FRM_PG51(int id_usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            id_usuario_login = id_usuario;
            crudHistorial = new clsCRUD_Historial();
        }


        /// <summary>
        /// Handles the Load event of the FRM_PG51 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG51_Load(object sender, EventArgs e)
        {
            isLoading = true;
            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today;
            this.CenterToScreen();
            CargarMiHistorial();
            isLoading = false;
            dtpFechaDesde.MaxDate = DateTime.Today;
            dtpFechaHasta.MaxDate = DateTime.Today;
        }

        /// <summary>
        /// Cargars the mi historial.
        /// </summary>
        private void CargarMiHistorial()
        {
            try
            {
                dgvBitacora.DataSource = crudHistorial.ObtenerHistorialUsuario(
                    id_usuario_login,
                    dtpFechaDesde.Value.Date,
                    dtpFechaHasta.Value.Date
                );

                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                if (dgvBitacora.Columns["Monto"] != null)
                {
                    dgvBitacora.Columns["Monto"].Visible = false;
                }

                if (dgvBitacora.Columns["Fecha Y Hora"] != null)
                {
                    dgvBitacora.Columns["Fecha Y Hora"].Width = 230;
                    dgvBitacora.Columns["Fecha Y Hora"].DefaultCellStyle.Format = "g";
                }

                if (dgvBitacora.Columns.Contains("Módulo"))
                    dgvBitacora.Columns["Módulo"].Width = 230;

                if (dgvBitacora.Columns.Contains("Acción"))
                    dgvBitacora.Columns["Acción"].Width = 230;

                if (dgvBitacora.Columns.Contains("Descripción"))
                    dgvBitacora.Columns["Descripción"].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;

                dgvBitacora.ColumnHeadersDefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tu historial: " + ex.Message, "Error");
            }
        }

        /// <summary>
        /// Cargars the datos.
        /// </summary>


        /// <summary>
        /// Handles the Click event of the btnVolver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
                dtpFechaHasta.Value = dtpFechaDesde.Value;
            CargarMiHistorial();
        }

        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtpFechaHasta.Value.Date < dtpFechaDesde.Value.Date)
                dtpFechaDesde.Value = dtpFechaHasta.Value;
            CargarMiHistorial();
        }
    }
}
