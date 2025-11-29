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
    public partial class Certificados_De_Depósito : Form
    {
        private bool modoEdicionActivo = false;
        private DataTable dtDatosCertificados = null;
        public Certificados_De_Depósito()
        {
            InitializeComponent();
            CargarDatos();
        }

        public Certificados_De_Depósito(string text)
        {
            InitializeComponent();
            Text = text;
            CargarDatos();
        }

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FRM_PG103_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClsCD clsCD = new();
            clsCD.BloquearDesbloquearData(dtDatosCertificados, dataGridView1, e.RowIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsCD objCd = new();
            objCd.Agregarfila(dtDatosCertificados, dataGridView1);

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        private bool datosGuardados = false;
        private int predicted_id;
        private int parroquia_id;

        private void textBox3_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.GuardarCD(dtDatosCertificados, dataGridView1, datosGuardados);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Intereses_Por_Cds objICD = new();
            objICD.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.guardaredic(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.renovarCD(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.cancelarCertificado(dataGridView1);
            CargarDatos();
        }

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FRM_42 obj42 = new(predicted_id, parroquia_id);
            obj42.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
