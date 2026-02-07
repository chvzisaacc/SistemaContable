using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using System.Data;

namespace Capa_de_Presentación.ALERTA
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ALERTA_SISTEMA : Form
    {
        /// <summary>
        /// The objalerta
        /// </summary>
        private readonly Alerta objalerta = new();
        /// <summary>
        /// Initializes a new instance of the <see cref="ALERTA_SISTEMA"/> class.
        /// </summary>
        public ALERTA_SISTEMA()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


        }

        /// <summary>
        /// Handles the Load event of the ALERTA_SISTEMA control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ALERTA_SISTEMA_Load(object sender, EventArgs e)
        {
            CargarDatosAlerta();
            this.CenterToScreen();

        }

        /// <summary>
        /// Cargars the datos alerta.
        /// </summary>
        private void CargarDatosAlerta()
        {
            try
            {
                DataTable datosalerta = objalerta.CargarAlerta();
                this.dataGridView1.DataSource = datosalerta;

                //AJUSTE DE COLUMNAS SEGÚN EL TEXTO
                this.dataGridView1.Columns["Tarea"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns["Limite(Días)"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns["Descripción"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //CENTRAR ENCABEZADOS
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // LIMPIEZA VISUAL
                this.dataGridView1.AllowUserToAddRows = false; // Quita la fila con el asterisco (*)
                this.dataGridView1.RowHeadersVisible = false; // Quita el borde gris izquierdo
                

                //AJUSTE DE LA ALTURA DEL CUADRO
                AjustarAlturaDGV(this.dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alerta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SOLO ADMITE DIAS - ES EL METODO FINAL
        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            string diasLimiteTexto = LimiteDay.Value.ToString();
            bool alarmaActiva = Estado.Checked;

            ClsValidaciones validar = new ClsValidaciones();

            if (!validar.EsMontoPositivo(diasLimiteTexto))
            {
                MessageBox.Show("El límite de días debe ser un número válido y positivo.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución si falla la validación
            }
            int diasLimite = (int)LimiteDay.Value;

            try
            {
                objalerta.ModificarConfiguracionAlerta(diasLimite, alarmaActiva);
                CargarDatosAlerta();

                MessageBox.Show(" alerta modificada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the 1 event of the button1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void LimiteDay_ValueChanged(object sender, EventArgs e)
        {

        }
        private void AjustarAlturaDGV(DataGridView dgv)
        {
            int alturaTotal = dgv.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                alturaTotal += row.Height;
            }
            dgv.Height = alturaTotal + 2;
        }
    }
}
