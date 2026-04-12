using Capa_de_acceso_de_datos;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_PG51 : Form
    {
        private int id_usuario_login;
        private clsCRUD_Historial crudHistorial;
        private bool isLoading = false;

        public FRM_PG51(int id_usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

          
            id_usuario_login = id_usuario;
            crudHistorial = new clsCRUD_Historial();
        }

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

        private void CargarMiHistorial()
        {
            try
            {
                dgvBitacora.DataSource = crudHistorial.ObtenerHistorialUsuario(
                    id_usuario_login,
                    dtpFechaDesde.Value.Date,
                    dtpFechaHasta.Value.Date
                );

                // Ocultar columnas no deseadas
                if (dgvBitacora.Columns["Monto"] != null)
                    dgvBitacora.Columns["Monto"].Visible = false;

                // Configuración de anchos y formatos
                if (dgvBitacora.Columns["Fecha Y Hora"] != null)
                {
                    dgvBitacora.Columns["Fecha Y Hora"].Width = 200;
                    dgvBitacora.Columns["Fecha Y Hora"].DefaultCellStyle.Format = "g";
                }

                // Ajuste dinámico de columnas por nombre exacto (según imagen)
                if (dgvBitacora.Columns.Contains("Módulo"))
                    dgvBitacora.Columns["Módulo"].Width = 150;

                if (dgvBitacora.Columns.Contains("Acción"))
                    dgvBitacora.Columns["Acción"].Width = 150;

                if (dgvBitacora.Columns.Contains("Descripción"))
                {
                    // Esto hará que la descripción use el resto del espacio del DGV
                    dgvBitacora.Columns["Descripción"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                // Estilos generales
                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvBitacora.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tu historial: " + ex.Message, "Error");
            }
        }

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