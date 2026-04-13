using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using System.Data;

namespace Capa_de_Presentación.ALERTA
{
    public partial class ALERTA_SISTEMA : Form
    {
        /// <summary>
        /// Instancia que maneja operaciones de alerta (lectura y modificación).
        /// Se utiliza para sincronizar la configuración persistente con la interfaz de usuario.
        /// </summary>
        private readonly Alerta objalerta = new();

        /// <summary>
        /// Constructor del formulario. Inicializa componentes y fija el estilo de borde.
        /// </summary>
        public ALERTA_SISTEMA()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Manejador del evento Load del formulario.
        /// Carga y sincroniza los datos de alerta en la UI y centra la ventana en pantalla.
        /// </summary>
        private void ALERTA_SISTEMA_Load(object sender, EventArgs e)
        {
            CargarDatosAlerta();
            this.CenterToScreen();
        }

        /// <summary>
        /// Recupera los datos de alerta desde la capa de procesamiento y los enlaza al DataGridView.
        /// También ajusta columnas, encabezados y la altura del control para mantener la vista sincronizada con los datos.
        /// </summary>
        private void CargarDatosAlerta()
        {
            try
            {
                DataTable datosalerta = objalerta.CargarAlerta();
                this.dataGridView1.DataSource = datosalerta;

                // AJUSTE DE COLUMNAS SEGÚN EL TEXTO
                this.dataGridView1.Columns["Tarea"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns["Estado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns["Limite(Días)"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dataGridView1.Columns["Descripción"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // CENTRAR ENCABEZADOS
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // LIMPIEZA VISUAL
                this.dataGridView1.AllowUserToAddRows = false; // Quita la fila con el asterisco (*)
                this.dataGridView1.RowHeadersVisible = false; // Quita el borde gris izquierdo

                // AJUSTE DE LA ALTURA DEL CUADRO
                AjustarAlturaDGV(this.dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alerta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento asociado al botón de guardar/modificar configuración de alerta.
        /// Valida el valor del límite de días, persiste la configuración mediante <see cref="objalerta"/>
        /// y actualiza la vista para mantener sincronizados los datos y la UI.
        /// </summary>
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
        /// Controlador de evento generado por el diseñador. Actualmente sin implementación.
        /// Reservado para acciones adicionales si se require sincronización extra.
        /// </summary>
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Controlador para cambios en el control `LimiteDay`.
        /// Actualmente sin lógica; aquí se podría añadir sincronización inmediata con la UI si se desea.
        /// </summary>
        private void LimiteDay_ValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Ajusta la altura del DataGridView para que coincida con la suma de los encabezados y las filas.
        /// Evita barras de desplazamiento innecesarias y mantiene la presentación sincronizada con el contenido.
        /// </summary>
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
