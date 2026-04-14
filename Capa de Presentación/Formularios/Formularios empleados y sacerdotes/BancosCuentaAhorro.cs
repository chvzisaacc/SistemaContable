csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\BancosCuentaAhorro.cs
using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class BancosCuentaAhorro : Form
    {
        /// <summary>
        /// Identificador de la parroquia asociado a las consultas y operaciones.
        /// </summary>
        private int _parroquiaId;

        /// <summary>
        /// Instancia que encapsula operaciones CRUD sobre cuentas bancarias.
        /// </summary>
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;

        /// <summary>
        /// Identificador de la cuenta actual (solo lectura).
        /// </summary>
        private readonly int cuentaId;

        /// <summary>
        /// Constructor del formulario para listar y administrar cuentas de ahorro.
        /// Inicializa componentes y dependencias. Las inicializaciones afectan el UI y
        /// deben ejecutarse en el hilo de la interfaz (UI thread).
        /// </summary>
        /// <param name="parroquiaId">Id de la parroquia cuyos registros se mostrarán.</param>
        public BancosCuentaAhorro(int parroquiaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            CRUD_CuentasBancarias = new ClsCRUD_CuentasBancarias();
            this._parroquiaId = parroquiaId;
        }

        /// <summary>
        /// Manejador del evento Load (versión FRM_PG42BancosCuentaCheque).
        /// Centra el formulario en pantalla.
        /// </summary>
        private void FRM_PG42BancosCuentaCheque_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Manejador del evento Load (versión FRM_PG6).
        /// Controla el estado inicial de los controles del formulario.
        /// </summary>
        private void FRM_PG6_Load(object sender, EventArgs e)
        {
            HabilitarControles(false);
        }

        /// <summary>
        /// Habilita o deshabilita controles relevantes del formulario.
        /// Uso: centralizar la lógica de activación de UI para facilitar sincronización si se llama desde hilos.
        /// </summary>
        /// <param name="habilitar">True para activar controles; false para desactivarlos.</param>
        private void HabilitarControles(bool habilitar)
        {
            txtMonto.Enabled = habilitar;
        }

        /// <summary>
        /// Manejador del click sobre el control de guardar/actualizar saldo.
        /// Parsea el monto ingresado y llama a la capa de datos para modificar el saldo.
        /// Mensajes al usuario y cierre del formulario se realizan en el hilo de UI.
        /// </summary>
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtMonto.Text, out decimal nuevo_saldo))
                {
                    MessageBox.Show("El saldo ingresado no es un número válido.");
                    return;
                }

                bool exito = CRUD_CuentasBancarias.ModificarSaldo(cuentaId, nuevo_saldo);

                if (exito)
                {
                    MessageBox.Show("Saldo actualizado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el saldo.", "Fallo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el saldo: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Paint vacío del panel (placeholder visual).
        /// Mantener vacío si no se requiere dibujo personalizado.
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e) { }

        /// <summary>
        /// Segundo manejador Paint del panel (placeholder).
        /// Conservado para el diseñador; sin lógica adicional.
        /// </summary>
        private void panel2_Paint_1(object sender, PaintEventArgs e) { }

        /// <summary>
        /// Carga las cuentas en el DataGridView.
        /// Formatea la columna Saldo y configura opciones de visualización y selección.
        /// Nota de sincronización: los resultados provenientes de la capa de datos deben aplicarse al UI mediante Invoke
        /// si la carga se hace desde un hilo background.
        /// </summary>
        private void CargarCuentasDataGridView()
        {
            try
            {
                DataTable dtCuentas = CRUD_CuentasBancarias.ObtenerCuentasBancarias(_parroquiaId);

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                if (dtCuentas != null && dtCuentas.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtCuentas;

                    if (dataGridView1.Columns.Contains("Saldo"))
                    {
                        var culturaEEUU = new System.Globalization.CultureInfo("en-US");
                        dataGridView1.Columns["Saldo"].DefaultCellStyle.FormatProvider = culturaEEUU;
                        dataGridView1.Columns["Saldo"].DefaultCellStyle.Format = "'L. ' #,##0.00";
                        dataGridView1.Columns["Saldo"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleRight;
                    }

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.Columns["Saldo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns["Saldo"].Width = 150;

                    if (dataGridView1.Columns.Contains("Id_Origen"))
                        dataGridView1.Columns["Id_Origen"].Visible = false;

                    if (dataGridView1.Columns.Contains("IdOrigenTipo"))
                        dataGridView1.Columns["IdOrigenTipo"].Visible = false;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;

                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                }
                else
                {
                    MessageBox.Show("No se encontraron cuentas.", "Información",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message, "Error de Carga",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Manejador Load principal del formulario.
        /// Centra la ventana y carga las cuentas en el DataGridView.
        /// </summary>
        private void FRM_PG42BancosCuentaAhorro_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarCuentasDataGridView();
        }

        /// <summary>
        /// Manejador del click para editar la fila seleccionada.
        /// Valida selección, comprueba ventana de edición permitida según tiempo desde creación,
        /// extrae valores necesarios (id, nombre, saldo, tipo) y abre el formulario de edición.
        /// Al retornar con DialogResult.OK recarga la vista para sincronizar cambios.
        /// </summary>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cuenta antes de editar.",
                                "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dataGridView1.SelectedRows[0];

            if (!int.TryParse(fila.Cells["Id_Origen"].Value?.ToString(), out int idOrigen))
            {
                MessageBox.Show("No se pudo obtener el ID de la cuenta.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object fechaValor = fila.Cells["Fecha de Creación"].Value;

            if (fechaValor != null && fechaValor != DBNull.Value)
            {
                DateTime fechaCreacion = Convert.ToDateTime(fechaValor);
                double minutosTranscurridos = (DateTime.Now - fechaCreacion).TotalMinutes;

                if (minutosTranscurridos > 15)
                {
                    MessageBox.Show(
                        "No se puede editar este registro.\nHan pasado más de 15 minutos desde su creación.",
                        "Edición no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            string nombre = fila.Cells["Nombre"].Value?.ToString() ?? string.Empty;
            decimal saldo = Convert.ToDecimal(fila.Cells["Saldo"].Value);

            if (!int.TryParse(fila.Cells["IdOrigenTipo"].Value?.ToString(), out int idTipoCuenta))
            {
                MessageBox.Show("No se pudo obtener el tipo de cuenta.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (BancosAgregarCuentaBancaria frmEditar = new BancosAgregarCuentaBancaria(
                        _parroquiaId, idOrigen, nombre, saldo, idTipoCuenta))
            {
                if (frmEditar.ShowDialog() == DialogResult.OK)
                {
                    CargarCuentasDataGridView();
                }
            }
        }
    }
}