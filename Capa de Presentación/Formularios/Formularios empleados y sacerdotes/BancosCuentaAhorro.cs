using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class BancosCuentaAhorro : Form
    {
        private int _parroquiaId;
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;
        private readonly int cuentaId;

        public BancosCuentaAhorro(int parroquiaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            CRUD_CuentasBancarias = new ClsCRUD_CuentasBancarias();
            this._parroquiaId = parroquiaId;
        }

        private void FRM_PG42BancosCuentaCheque_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void FRM_PG6_Load(object sender, EventArgs e)
        {
            HabilitarControles(false);
        }

        private void HabilitarControles(bool habilitar)
        {
            txtMonto.Enabled = habilitar;
        }

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

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void panel2_Paint_1(object sender, PaintEventArgs e) { }

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

                    // Ocultar columnas de ID
                    if (dataGridView1.Columns.Contains("Id_Origen"))
                        dataGridView1.Columns["Id_Origen"].Visible = false;

                    // ✅ Ocultar IdOrigenTipo (se usa internamente para editar)
                    if (dataGridView1.Columns.Contains("IdOrigenTipo"))
                        dataGridView1.Columns["IdOrigenTipo"].Visible = false;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;

                    // ✅ Necesario para que el lápiz detecte la fila seleccionada
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

        private void FRM_PG42BancosCuentaAhorro_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarCuentasDataGridView();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            // Validar que haya una fila seleccionada
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cuenta antes de editar.",
                                "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dataGridView1.SelectedRows[0];

            // Obtener ID de la cuenta
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
            // Obtener nombre
            string nombre = fila.Cells["Nombre"].Value?.ToString() ?? string.Empty;

            // Limpiar formato "L. 1,650.00" para obtener el decimal puro
            decimal saldo = Convert.ToDecimal(fila.Cells["Saldo"].Value);

            // Obtener ID del tipo de cuenta
            if (!int.TryParse(fila.Cells["IdOrigenTipo"].Value?.ToString(), out int idTipoCuenta))
            {
                MessageBox.Show("No se pudo obtener el tipo de cuenta.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Abrir formulario en modo edición
            using (BancosAgregarCuentaBancaria frmEditar = new BancosAgregarCuentaBancaria(
                        _parroquiaId, idOrigen, nombre, saldo, idTipoCuenta))
            {
                if (frmEditar.ShowDialog() == DialogResult.OK)
                {
                    CargarCuentasDataGridView(); // Recargar la tabla tras editar
                }
            }
        }
    }
}