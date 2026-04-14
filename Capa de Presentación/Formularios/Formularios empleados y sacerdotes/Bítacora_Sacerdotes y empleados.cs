csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\Bítacora_Sacerdotes y empleados.cs
using Capa_de_acceso_de_datos;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario para mostrar la bitácora (historial) del usuario actual.
    /// Permite filtrar por rango de fechas y presenta los resultados en un DataGridView.
    /// Las operaciones de carga actualizan controles UI y deben ejecutarse en el hilo de interfaz.
    /// </summary>
    public partial class FRM_PG51 : Form
    {
        /// <summary>
        /// Id del usuario con sesión activa (usado para filtrar su historial).
        /// </summary>
        private int id_usuario_login;

        /// <summary>
        /// Acceso a la capa de datos para obtener historial.
        /// </summary>
        private clsCRUD_Historial crudHistorial;

        /// <summary>
        /// Indicador usado para evitar recargas mientras se inicializan controles.
        /// </summary>
        private bool isLoading = false;

        /// <summary>
        /// Constructor del formulario.
        /// Inicializa componentes y dependencias. Las inicializaciones del UI deben ocurrir en el hilo de interfaz.
        /// </summary>
        /// <param name="id_usuario">Identificador del usuario cuyo historial se mostrará.</param>
        public FRM_PG51(int id_usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            id_usuario_login = id_usuario;
            crudHistorial = new clsCRUD_Historial();
        }

        /// <summary>
        /// Manejador del evento Load del formulario.
        /// Configura valores iniciales de controles (fechas) y lanza la carga del historial.
        /// Nota de sincronización: las llamadas a <see cref="CargarMiHistorial"/> actualizan controles UI y deben ejecutarse en el hilo UI.
        /// </summary>
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
        /// Carga el historial del usuario logueado en el DataGridView.
        /// Realiza formateos de columnas y ajustes visuales. Si la obtención de datos se hace en background,
        /// aplicar los resultados al UI mediante Invoke/BeginInvoke.
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

                // Ocultar columnas no deseadas
                if (dgvBitacora.Columns["Monto"] != null)
                    dgvBitacora.Columns["Monto"].Visible = false;

                // Configuración de anchos y formatos
                if (dgvBitacora.Columns["Fecha Y Hora"] != null)
                {
                    dgvBitacora.Columns["Fecha Y Hora"].Width = 200;
                    dgvBitacora.Columns["Fecha Y Hora"].DefaultCellStyle.Format = "g";
                }

                // Ajuste dinámico de columnas por nombre exacto
                if (dgvBitacora.Columns.Contains("Módulo"))
                    dgvBitacora.Columns["Módulo"].Width = 150;

                if (dgvBitacora.Columns.Contains("Acción"))
                    dgvBitacora.Columns["Acción"].Width = 150;

                if (dgvBitacora.Columns.Contains("Descripción"))
                {
                    // Hace que la descripción use el resto del espacio del DGV
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

        /// <summary>
        /// Botón volver: cierra el formulario.
        /// Acción realizada en el hilo UI.
        /// </summary>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Manejador ValueChanged de dtpFechaDesde.
        /// Mantiene consistencia con dtpFechaHasta y recarga el historial si corresponde.
        /// Protegido por <see cref="isLoading"/> para evitar recargas durante inicialización.
        /// </summary>
        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
                dtpFechaHasta.Value = dtpFechaDesde.Value;
            CargarMiHistorial();
        }

        /// <summary>
        /// Manejador ValueChanged de dtpFechaHasta.
        /// Mantiene consistencia con dtpFechaDesde y recarga el historial si corresponde.
        /// Protegido por <see cref="isLoading"/> para evitar recargas durante inicialización.
        /// </summary>
        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtpFechaHasta.Value.Date < dtpFechaDesde.Value.Date)
                dtpFechaDesde.Value = dtpFechaHasta.Value;
            CargarMiHistorial();
        }
    }
}