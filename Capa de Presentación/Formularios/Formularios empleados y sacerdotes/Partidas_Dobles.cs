using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Ventana que muestra las partidas dobles asociadas a una transacción.
    /// Presenta los registros en un DataGridView y aplica formato monetario en tiempo de render.
    /// </summary>
    public partial class Partidas_Dobles : Form
    {
        /// <summary>
        /// Identificador de la transacción cuyas partidas se mostrarán.
        /// </summary>
        private int id_transaccion;

        /// <summary>
        /// Constructor que recibe el id de transacción y prepara la ventana.
        /// Las inicializaciones afectan controles UI y se ejecutan en el hilo de interfaz (UI thread).
        /// </summary>
        /// <param name="id_transaccion">Id de la transacción a visualizar.</param>
        public Partidas_Dobles(int id_transaccion)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.id_transaccion = id_transaccion;
        }

        /// <summary>
        /// Constructor por defecto (sin id). Mantener para compatibilidad con diseñador si procede.
        /// </summary>
        public Partidas_Dobles()
        {
        }

        /// <summary>
        /// Evento Load del formulario.
        /// - Carga las partidas mediante <see cref="CargarPartidas"/>.
        /// - Registra el manejador de formateo de celdas para mostrar valores monetarios.
        /// - Configura el ajuste de texto y modo de autoajuste del DataGridView.
        /// Nota de sincronización: si la carga de datos se realizara en un hilo background, las asignaciones a <see cref="dgvPartidas"/> deben marshalearse al hilo UI con Invoke/BeginInvoke.
        /// </summary>
        private void FRM_PG69_Load(object sender, EventArgs e)
        {
            CargarPartidas();

            // Formato monetario
            dgvPartidas.CellFormatting += dgvPartidas_CellFormatting;

            // Configurar el ajuste de texto
            dgvPartidas.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvPartidas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvPartidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.CenterToScreen();
        }

        /// <summary>
        /// Manejador de CellFormatting del DataGridView.
        /// Aplica formato currency (por ejemplo "L. 1,650.00") a las columnas "Debe" y "Haber".
        /// Ejecutado en el hilo UI durante el pintado de celdas.
        /// </summary>
        private void dgvPartidas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var columnasMoneto = new[] { "Debe", "Haber" };

            if (e.ColumnIndex >= 0 && e.Value != null &&
                columnasMoneto.Contains(dgvPartidas.Columns[e.ColumnIndex].Name))
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal monto))
                {
                    var formatoHnd = new System.Globalization.CultureInfo("en-US");
                    e.Value = $"L.{monto.ToString("N2", formatoHnd)}";
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// Consulta la capa de procesamiento para obtener las partidas de la transacción y asigna el DataSource del grid.
        /// Si la obtención se hace en background, devolver el DataTable al hilo UI antes de asignarlo a <see cref="dgvPartidas"/>.
        /// </summary>
        private void CargarPartidas()
        {
            PatidasDobles pa = new();
            DataTable dt = pa.CargarPartidas(id_transaccion);
            dgvPartidas.DataSource = dt;
        }

        /// <summary>
        /// Cierra la ventana al pulsar el botón cerrar.
        /// Acción realizada en el hilo UI.
        /// </summary>
        private void Btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}