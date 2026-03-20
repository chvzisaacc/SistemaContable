using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Partidas_Dobles : Form
    {
        /// <summary>
        /// The cn
        /// </summary>
        Clsconexion cn = new Clsconexion();
        /// <summary>
        /// The identifier transaccion
        /// </summary>
        private int id_transaccion;


        /// <summary>
        /// Initializes a new instance of the <see cref="Partidas_Dobles"/> class.
        /// </summary>
        /// <param name="id_transaccion">The identifier transaccion.</param>
        public Partidas_Dobles(int id_transaccion)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.id_transaccion = id_transaccion;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Partidas_Dobles"/> class.
        /// </summary>
        public Partidas_Dobles()
        {
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG69 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Cargars the partidas.
        /// </summary>
        private void CargarPartidas()
        {
            PatidasDobles pa = new();
            DataTable dt = pa.CargarPartidas(id_transaccion);
            dgvPartidas.DataSource = dt;
        }


        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        

        /// <summary>
        /// Handles the Click event of the Btncerrar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
