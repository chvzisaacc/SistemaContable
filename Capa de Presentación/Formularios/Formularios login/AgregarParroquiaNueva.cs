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

        /// <summary>DataTable con todas las parroquias cargadas desde BD.</summary>
        private DataTable _dtParroquias;

        /// <summary>Bandera para evitar recursividad en el evento TextChanged.</summary>
        private bool _filtrando = false;

        /// <summary>Bandera para bloquear el evento TextChanged durante la carga inicial.</summary>
        private bool _cargando = false;

        /// <summary>Constructor del formulario.</summary>
        public AgregarParroquiaNueva()
        {
            InitializeComponent();
        }

        /// <summary>Evento Load: carga la lista de parroquias al abrir el formulario.</summary>
        private async void AgregarParroquiaNueva_Load(object sender, EventArgs e)
        {
            await CargarParroquiasAsync();
        }

        /// <summary>Carga las parroquias desde la capa de datos de forma asíncrona y las enlaza al combo.</summary>
        private async Task CargarParroquiasAsync()
        {
            try
            {
                _cargando = true;

                _dtParroquias = await Task.Run(() => _crud.ObtenerParroquias());

                comboBox1.MaxDropDownItems = 5;
                comboBox1.DropDownHeight = 21 * 5;
                comboBox1.DropDownWidth = 600;

                comboBox1.DataSource = _dtParroquias;
                comboBox1.DisplayMember = "Parroquia_nombre";
                comboBox1.ValueMember = "Parroquia_id";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar parroquias: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargando = false;
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
            if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedItem is DataRowView row)
            {
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
                }
                else
                {
                    _acciones.EditarParroquia(_parroquiaSeleccionadaId, nombre, correo);
                    MessageBox.Show("Parroquia actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
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
            _ = CargarParroquiasAsync();
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

        /// <summary>Filtra el combo de parroquias según el texto escrito, buscando en cualquier parte del nombre.</summary>
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (_filtrando || _cargando) return;
            _filtrando = true;

            try
            {
                string busqueda = comboBox1.Text;
                if (_dtParroquias == null) return;

                // Desconectar datasource para evitar que el combo pise el texto
                comboBox1.DataSource = null;

                if (string.IsNullOrWhiteSpace(busqueda))
                    _dtParroquias.DefaultView.RowFilter = "";
                else
                    _dtParroquias.DefaultView.RowFilter = "Parroquia_nombre LIKE '%"
                        + busqueda.Replace("'", "''") + "%'";

                // Reconectar con el filtro aplicado
                comboBox1.DataSource = _dtParroquias.DefaultView;
                comboBox1.DisplayMember = "Parroquia_nombre";
                comboBox1.ValueMember = "Parroquia_id";

                // Restaurar el texto escrito
                comboBox1.Text = busqueda;
                comboBox1.SelectionStart = busqueda.Length;
                comboBox1.DroppedDown = true;
            }
            finally
            {
                _filtrando = false;
            }
        }
    }
}