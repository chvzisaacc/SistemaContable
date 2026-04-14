using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios.Formularios_inicio_de_sesion_y_ventana_administrador
{
    /// <summary>
    /// Formulario para agregar o editar parroquias.
    /// Permite seleccionar una parroquia existente para editarla o crear una nueva.
    /// </summary>
    public partial class AgregarParroquiaNueva : Form
    {
        /// <summary>Acceso a operaciones relacionadas con usuarios y parroquias.</summary>
        private clsCRUD_Usuarios _crud = new clsCRUD_Usuarios();

        /// <summary>Acciones de base de datos auxiliares (insert/update) para parroquias.</summary>
        private ClsAccionesDB _acciones = new ClsAccionesDB();

        /// <summary>ID de la parroquia actualmente seleccionada; 0 indica nueva parroquia.</summary>
        private int _parroquiaSeleccionadaId = 0;

        /// <summary>Constructor del formulario.</summary>
        public AgregarParroquiaNueva()
        {
            InitializeComponent();
        }

        /// <summary>Evento Load: carga la lista de parroquias al abrir el formulario.</summary>
        private void AgregarParroquiaNueva_Load(object sender, EventArgs e)
        {
            CargarParroquias();
        }

        /// <summary>Carga las parroquias desde la capa de datos y las enlaza al combo.</summary>
        private void CargarParroquias()
        {
            try
            {
                comboBox1.DataSource = _crud.ObtenerParroquias();
                comboBox1.DisplayMember = "Parroquia_nombre";
                comboBox1.ValueMember = "Parroquia_id";
                comboBox1.SelectedIndex = -1; // sin selección por defecto
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar parroquias: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Prepara el formulario para la creación de una nueva parroquia.
        /// Limpia campos y habilita los controles correspondientes.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            comboBox1.Enabled = false;
            comboBox1.SelectedIndex = -1;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            _parroquiaSeleccionadaId = 0; // Indica que es un registro nuevo
        }

        /// <summary>
        /// Carga los datos de la parroquia seleccionada en los campos para edición.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                DataRowView row = (DataRowView)comboBox1.SelectedItem;

                _parroquiaSeleccionadaId = Convert.ToInt32(row["Parroquia_id"]);
                textBox1.Text = row["Parroquia_nombre"].ToString();
                textBox2.Text = row["Parroquia_correo"].ToString();

                textBox1.Enabled = true;
                textBox2.Enabled = true;
                comboBox1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una parroquia para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Guarda los cambios: agrega una parroquia nueva o actualiza la existente.
        /// Actualiza el combo y cierra el diálogo con DialogResult.OK en caso de éxito.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = textBox1.Text.Trim();
                string correo = textBox2.Text.Trim();

                if (_parroquiaSeleccionadaId == 0)
                {
                    int nuevoId = _acciones.AgregarParroquia(nombre, correo);
                    MessageBox.Show($"Parroquia agregada exitosamente. ID: {nuevoId}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.DataSource = _crud.ObtenerParroquias();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    _acciones.EditarParroquia(_parroquiaSeleccionadaId, nombre, correo);
                    MessageBox.Show("Parroquia actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.DataSource = _crud.ObtenerParroquias();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                FinalizarOperacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>Restablece la UI después de una operación (limpia campos y recarga el combo).</summary>
        private void FinalizarOperacion()
        {
            LimpiarCampos();
            CargarParroquias();
            comboBox1.Enabled = true;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            _parroquiaSeleccionadaId = 0;
        }

        /// <summary>Limpia los campos de entrada del formulario.</summary>
        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
