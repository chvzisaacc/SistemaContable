using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Diego
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Certificados_De_Depósito : Form
    {
        /// <summary>
        /// The modo edicion activo
        /// </summary>
        private bool modoEdicionActivo = false;
        /// <summary>
        /// The dt datos certificados
        /// </summary>
        private DataTable dtDatosCertificados = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="Certificados_De_Depósito"/> class.
        /// </summary>
        public Certificados_De_Depósito()
        {
            InitializeComponent();
            CargarDatos();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Certificados_De_Depósito"/> class.
        /// </summary>
        /// <param name="text">The text displayed by the control.</param>
        public Certificados_De_Depósito(string text)
        {
            InitializeComponent();
            Text = text;
            CargarDatos();
        }

        /// <summary>
        /// Cargars the datos.
        /// </summary>
        public async void CargarDatos()
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();

                // Ejecutar la carga de datos en un hilo de fondo
                dtDatosCertificados = await Task.Run(() => acciones.CargarCertificados());

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dtDatosCertificados;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoResizeColumns();
                dataGridView1.ReadOnly = true;

                // Ocultar columnas innecesarias
                if (dataGridView1.Columns.Contains("Id_certificado"))
                {
                    dataGridView1.Columns["Id_certificado"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("FechaTransaccion"))
                {
                    dataGridView1.Columns["FechaTransaccion"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
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
        /// Handles the TextChanged event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_TextChanged(object sender, EventArgs e)
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
        /// Handles the 1 event of the dataGridView1_CellContentClick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG103 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG103_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the CellClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClsCD clsCD = new();
            clsCD.BloquearDesbloquearData(dtDatosCertificados, dataGridView1, e.RowIndex);
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ClsCD objCd = new();
            objCd.Agregarfila(dtDatosCertificados, dataGridView1);

        }

        /// <summary>
        /// Handles the 1 event of the textBox3_TextChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pictureBox6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// The datos guardados
        /// </summary>
        private bool datosGuardados = false;
        /// <summary>
        /// The predicted identifier
        /// </summary>
        private int predicted_id;
        /// <summary>
        /// The parroquia identifier
        /// </summary>
        private int parroquia_id;

        /// <summary>
        /// Handles the Click event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.GuardarCD(dtDatosCertificados, dataGridView1, datosGuardados);
        }

        /// <summary>
        /// Handles the Click event of the pictureBox8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Intereses_Por_Cds objICD = new();
            objICD.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the pictureBox7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.guardaredic(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);

        }

        /// <summary>
        /// Handles the Click event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.renovarCD(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox1_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.cancelarCertificado(dataGridView1);
            CargarDatos();
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            const string COLUMNA_FECHA = "FechaTransaccion"; // Asegúrate de que este sea el nombre real de tu columna


            DataGridViewRow filaActual = dataGridView1.Rows[e.RowIndex];


            if (filaActual.IsNewRow) return;


            if (filaActual.Cells[COLUMNA_FECHA].Value != null &&
                DateTime.TryParse(filaActual.Cells[COLUMNA_FECHA].Value.ToString(), out DateTime fechaTransaccion))
            {
                DateTime limiteEdicion = fechaTransaccion.AddMinutes(10);

                if (DateTime.Now > limiteEdicion)
                {
                    e.Cancel = true;
                    MessageBox.Show("No puedes editar esta transacción. Solo se permite la modificación durante los primeros 15 minutos del registro.",
                                    "Edición Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

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
        /// Handles the Click event of the pictureBox9 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FRM_42 obj42 = new(predicted_id, parroquia_id);
            obj42.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the pictureBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox3_Click(object sender, EventArgs e)
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
    }

}
