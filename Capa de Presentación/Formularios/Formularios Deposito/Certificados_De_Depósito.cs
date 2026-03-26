using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System;
using System.Data;
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
        private AutoCompleteStringCollection Parroquia = new AutoCompleteStringCollection();
        private ClsAccionesDB objParroquias = new ClsAccionesDB();
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
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            CargarDatosAutocompletadoParroquias();

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
                dtDatosCertificados = await Task.Run(() => acciones.CargarCertificados());

                // 1. Limpiar columnas previas para evitar duplicados o basura visual
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // 2. Asignar el origen de datos
                dataGridView1.DataSource = dtDatosCertificados;

                // 3. Configurar el mapeo de datos (DataPropertyName)
                // Esto es lo que hace que los datos APAREZCAN en las celdas
                if (dataGridView1.Columns.Contains("Nombre_certificado"))
                {
                    dataGridView1.Columns["Nombre_certificado"].DataPropertyName = "Nombre_certificado";
                    dataGridView1.Columns["Nombre_certificado"].HeaderText = "Certificado";
                    dataGridView1.Columns["Nombre_certificado"].Visible = true;
                }

                if (dataGridView1.Columns.Contains("FechaTransaccion"))
                {
                    dataGridView1.Columns["FechaTransaccion"].DataPropertyName = "FechaTransaccion";
                    dataGridView1.Columns["FechaTransaccion"].HeaderText = "Fecha";
                    dataGridView1.Columns["FechaTransaccion"].Visible = true;
                }

                if (dataGridView1.Columns.Contains("Nombre_Parroquia"))
                {
                    dataGridView1.Columns["Nombre_Parroquia"].DataPropertyName = "Nombre_Parroquia";
                    dataGridView1.Columns["Nombre_Parroquia"].HeaderText = "Parroquia";
                    dataGridView1.Columns["Nombre_Parroquia"].Visible = true;
                }

                // 4. Ocultar el resto de columnas que traiga el DataTable (ID, etc.)
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name != "Nombre_certificado" &&
                        col.Name != "FechaTransaccion" &&
                        col.Name != "Nombre_Parroquia")
                    {
                        col.Visible = false;
                    }
                }

                // 5. Ajustes finales
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false; // Evita la fila vacía al final si no la necesitas
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the 1 event of the dataGridView1_CellContentClick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the Load event of the FRM_PG103 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG103_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
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


        /// <summary>
        /// Handles the Click event of the pictureBox6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

        /// <summary>
        /// The datos guardados
        /// </summary>
        private bool datosGuardados = false;
        /// <summary>
        /// The predicted identifier
        /// </summary>


        /// <summary>
        /// Handles the Click event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_Click(object sender, EventArgs e)
        {
            //ClsCD objCD = new();
            //objCD.GuardarCD(dtDatosCertificados, dataGridView1, datosGuardados);
        }

        /// <summary>
        /// Handles the Click event of the pictureBox8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


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
            //ClsCD objCD = new();
            //objCD.renovarCD(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the Click event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox1_Click(object sender, EventArgs e)
        {
            /*ClsCD objCD = new ClsCD();
            objCD.cancelarCertificado(dataGridView1);
            CargarDatos();*/
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>



        private void CargarDatosAutocompletadoParroquias()
        {
            Parroquia.Clear();

            try
            {
                DataTable dt = objParroquias.ObtenerParroquias();

                foreach (DataRow row in dt.Rows)
                {
                    Parroquia.Add(row["Nombre_Parroquia"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        /// <summary>
        /// Handles the Paint event of the panel2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the Click event of the pictureBox9 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the pictureBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the TextChanged event of the textBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>




        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //No se hagan clicks en los encabezados
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
            DataGridViewCell celdaActual = fila.Cells[e.ColumnIndex];

            if (celdaActual.ReadOnly == true)
            {
                ClsCD.DesbloquearFila(fila);


                if (dataGridView1.Columns.Contains("Nombre_Parroquia"))
                {
                    DataGridViewCell celdaParroquia = fila.Cells["Nombre_Parroquia"];
                    dataGridView1.CurrentCell = celdaParroquia;
                    dataGridView1.BeginEdit(true);

                    MessageBox.Show("Fila desbloqueada. El cursor está listo para corregir el Nombre de la Parroquia.",
                                    "Edición Rápida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Si la columna de Parroquia no existe, solo iniciamos la edición normal en la celda del clic
                    dataGridView1.CurrentCell = celdaActual;
                    dataGridView1.BeginEdit(true);
                }
            }
            else
            {
                //  Si la fila ya estaba desbloqueada, solo inicia la edición
                dataGridView1.CurrentCell = celdaActual;
                dataGridView1.BeginEdit(true);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
                return;

            // Verifica por nombre de columna, no por índice
            if (dataGridView1.CurrentCell.OwningColumn.Name == "Nombre_Parroquia")
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    auto_text.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    // Usa la colección ya cargada desde CargarDatosAutocompletadoGastos()
                    auto_text.AutoCompleteCustomSource = Parroquia;
                }
            }
            else
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.None;
                    auto_text.AutoCompleteSource = AutoCompleteSource.None;
                    auto_text.AutoCompleteCustomSource = null;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.GuardarCD(dtDatosCertificados, dataGridView1, datosGuardados);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.cancelarCertificado(dataGridView1);
            CargarDatos();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.renovarCD(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);
        }
    }

}
