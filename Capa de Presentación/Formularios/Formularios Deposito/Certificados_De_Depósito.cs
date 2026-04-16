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
    /// Formulario para la gestión de Certificados de Depósito.
    /// Contiene carga asíncrona de datos, edición en grid y soporte de autocompletado para parroquias.
    /// </summary>
    public partial class Certificados_De_Depósito : Form
    {
        /// <summary>
        /// Colección usada para autocompletado de la columna "Nombre_Parroquia" en el DataGridView.
        /// </summary>
        private AutoCompleteStringCollection Parroquia = new AutoCompleteStringCollection();

        /// <summary>
        /// Acceso a acciones de base de datos (obtener parroquias, certificados, etc.).
        /// </summary>
        private ClsAccionesDB objParroquias = new ClsAccionesDB();

        /// <summary>
        /// Indica si el modo edición está activo para controlar comportamiento de guardado/renovación.
        /// </summary>
        private bool modoEdicionActivo = false;

        /// <summary>
        /// DataTable que contiene los datos cargados desde la base de datos. Usado como DataSource del DataGridView.
        /// Mantener sincronizado con las operaciones en el grid (edición, agregar, eliminar).
        /// </summary>
        private DataTable dtDatosCertificados = null;

        /// <summary>
        /// Indica si los datos han sido guardados en la sesión actual.
        /// </summary>
        private bool datosGuardados = false;

        /// <summary>
        /// Constructor por defecto: inicializa componentes, carga autocompletado y datos.
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
        /// Constructor alternativo que permite establecer el texto del formulario antes de cargar datos.
        /// </summary>
        /// <param name="text">Texto del título del formulario.</param>
        public Certificados_De_Depósito(string text)
        {
            InitializeComponent();
            Text = text;
            CargarDatos();

        }

        /// <summary>
        /// Carga los certificados desde la base de datos de forma asíncrona y vincula el resultado al DataGridView.
        /// Ejecuta tareas de UI después de la carga; asegurarse de no bloquear el hilo de interfaz.
        /// </summary>
        public async void CargarDatos()
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                // Carga en background para no bloquear la UI
                dtDatosCertificados = await Task.Run(() => acciones.CargarCertificados());

                // 1. Limpiar columnas previas para evitar duplicados o artefactos visuales
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                // 2. Asignar el origen de datos
                dataGridView1.DataSource = dtDatosCertificados;

                if (dtDatosCertificados.Columns.Contains("Id_certificado"))
                {
                    dtDatosCertificados.Columns["Id_certificado"].ReadOnly = false;
                }

                // 3. Mapear columnas visibles y sus cabeceras
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

                // 4. Ocultar el resto de columnas retornadas por el DataTable
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.Name != "Nombre_certificado" &&
                        col.Name != "FechaTransaccion" &&
                        col.Name != "Nombre_Parroquia")
                    {
                        col.Visible = false;
                    }
                }

                // 5. Ajustes finales de comportamiento del grid
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false; // Evita fila vacía al final
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Evento Load del formulario: centra la ventana en pantalla.
        /// </summary>
        private void FRM_PG103_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Controlador de CellClick: delega en la clase de lógica (ClsCD) para bloquear/desbloquear fila.
        /// </summary>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClsCD clsCD = new();
            clsCD.BloquearDesbloquearData(dtDatosCertificados, dataGridView1, e.RowIndex);
        }

        /// <summary>
        /// Añade una nueva fila al DataTable y actualiza el DataGridView usando la lógica de ClsCD.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            ClsCD objCd = new();
            objCd.Agregarfila(dtDatosCertificados, dataGridView1);

        }

        /// <summary>
        /// Controlador sobre click en textBox3 (reserva para acciones futuras). Actualmente sin implementación.
        /// </summary>
        private void textBox3_Click(object sender, EventArgs e)
        {
            // Reserva: guardar o procesar datos al requerirlo
            //ClsCD objCD = new();
            //objCD.GuardarCD(dtDatosCertificados, dataGridView1, datosGuardados);
        }

        /// <summary>
        /// Aplica cambios de edición (guardado rápido) delegando en la clase de lógica ClsCD.
        /// </summary>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.guardaredic(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);

        }

        /// <summary>
        /// Reserva para renovaciones. Actualmente comentado; delegaría en ClsCD. Mantener referencia a modoEdicionActivo.
        /// </summary>
        private void textBox2_Click(object sender, EventArgs e)
        {
            //ClsCD objCD = new();
            //objCD.renovarCD(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);
        }

        /// <summary>
        /// Controlador de click en textBox1 (acción reservada). Actualmente sin implementación activa.
        /// </summary>
        private void textBox1_Click(object sender, EventArgs e)
        {
            /*ClsCD objCD = new ClsCD();
            objCD.cancelarCertificado(dataGridView1);
            CargarDatos();*/
        }

        /// <summary>
        /// Carga la lista de parroquias desde la base de datos y la almacena en la colección de autocompletado.
        /// Esta colección se utiliza en el evento EditingControlShowing para sugerencias al editar celdas.
        /// </summary>
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
        /// Cierra el formulario. Asociado al control de cierre (pictureBox9).
        /// </summary>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Doble clic en celda: desbloquea fila para edición o inicia edición en la celda seleccionada.
        /// Si existe la columna "Nombre_Parroquia", posiciona el cursor allí y activa autocompletado.
        /// </summary>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // No procesar clicks en encabezados
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
                    // Si la columna de Parroquia no existe, iniciar edición normal
                    dataGridView1.CurrentCell = celdaActual;
                    dataGridView1.BeginEdit(true);
                }
            }
            else
            {
                // Si ya estaba editable, iniciar edición directamente
                dataGridView1.CurrentCell = celdaActual;
                dataGridView1.BeginEdit(true);
            }
        }

        /// <summary>
        /// Evento que configura el control de edición para columnas específicas (autocompletado para parroquias).
        /// Se ejecuta cuando se muestra el control de edición en el DataGridView.
        /// </summary>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
                return;

            // Activar autocompletado sólo para la columna Nombre_Parroquia
            if (dataGridView1.CurrentCell.OwningColumn.Name == "Nombre_Parroquia")
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    auto_text.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    // Usa la colección cargada por CargarDatosAutocompletadoParroquias()
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

        /// <summary>
        /// Controlador para cambios en el textBox3. Reservado.
        /// </summary>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Guarda los cambios usando la lógica de ClsCD.
        /// </summary>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.GuardarCD(dtDatosCertificados, dataGridView1, datosGuardados);
        }

        /// <summary>
        /// Cancela el certificado seleccionado delegando en ClsCD y recarga los datos.
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new ClsCD();
            objCD.cancelarCertificado(dataGridView1);
            CargarDatos();
        }

        /// <summary>
        /// Renovación de certificado: delega en ClsCD y mantiene el estado de modoEdicionActivo.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClsCD objCD = new();
            objCD.renovarCD(dtDatosCertificados, dataGridView1, ref modoEdicionActivo);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
