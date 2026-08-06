using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Utilidades para manejar operaciones comunes en transacciones (ingresos/gastos)
    /// y sincronizar las acciones entre la UI (DataGridView/ComboBox) y la capa de datos.
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class Transacciones : Clsconexion
    {
        /// <summary>
        /// Carga los orígenes en dos ComboBox a partir de la parroquia dada.
        /// Inserta una opción "Seleccionar" al inicio y sincroniza Display/Value members.
        /// </summary>
        /// <param name="cmbOrigen">ComboBox principal para orígenes.</param>
        /// <param name="cmbOrigen2">ComboBox secundario para orígenes (si aplica).</param>
        /// <param name="parroquiaId">Identificador de la parroquia para filtrar orígenes.</param>
        public void CargarComboBoxOrigen(ComboBox cmbOrigen, ComboBox cmbOrigen2, int parroquiaId)
        {
            ClsAccionesDB clsAccionesDB = new ClsAccionesDB();

            try
            {
                ClsAccionesDB db = new ClsAccionesDB();
                List<Origen> lista = db.ObtenerListaOrigenes(parroquiaId);
                List<Origen> lista2 = db.ObtenerListaOrigenes(parroquiaId);

                // Insertar opción por defecto
                lista.Insert(0, new Origen(0, "Seleccionar"));
                lista2.Insert(0, new Origen(0, "Seleccionar"));

                cmbOrigen.DataSource = lista;
                cmbOrigen.DisplayMember = "nombre";
                cmbOrigen.ValueMember = "id";
                cmbOrigen.SelectedIndex = 0;

                cmbOrigen2.DataSource = lista2;
                cmbOrigen2.DisplayMember = "nombre";
                cmbOrigen2.ValueMember = "id";
                cmbOrigen2.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los orígenes: " + ex.Message, "Error");
            }
        }

        /// <summary>
        /// Inicializa el ComboBox de cuentas con opciones por defecto.
        /// </summary>
        /// <param name="cmbCuentas">ComboBox que mostrará las cuentas.</param>
        public void CargarComboBoxCuentas(ComboBox cmbCuentas)
        {
            cmbCuentas.Items.Clear();
            cmbCuentas.Items.Add("Cuentas Bancarias");
            cmbCuentas.SelectedIndex = 0;
        }

        /// <summary>
        /// Agrega una fila vacía al DataTable de ingresos, enlaza al DataGridView y posiciona el foco
        /// en la nueva fila para comenzar la edición. Oculta columnas internas como "HoraRegistro".
        /// </summary>
        /// <param name="dtDatosIngresos">DataTable origen de los ingresos.</param>
        /// <param name="dataGridView1">DataGridView asociado a la vista.</param>
        public void Agregarfila(DataTable dtDatosIngresos, DataGridView dataGridView1)
        {
            if (dtDatosIngresos != null)
            {
                dataGridView1.ReadOnly = false;

                DataRow newRow = dtDatosIngresos.NewRow();
                dtDatosIngresos.Rows.Add(newRow);

                dataGridView1.DataSource = dtDatosIngresos;

                if (dataGridView1.Columns.Contains("HoraRegistro"))
                {
                    dataGridView1.Columns["HoraRegistro"].Visible = false;
                }

                int lastIndex = dtDatosIngresos.Rows.Count - 1;

                if (lastIndex >= 0)
                {
                    dataGridView1.CurrentCell = null;
                    dataGridView1.ClearSelection();

                    dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;

                    DataGridViewColumn firstVisibleColumn = dataGridView1.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Visible);

                    if (firstVisibleColumn != null)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[lastIndex].Cells[firstVisibleColumn.Index];
                        dataGridView1.BeginEdit(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se puede añadir la fila.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Agrega una fila vacía al DataTable de gastos y pone el foco en ella para edición.
        /// </summary>
        /// <param name="dtDatosGastos">DataTable origen de gastos.</param>
        /// <param name="dgvGastos">DataGridView asociado a gastos.</param>
        public void Agregarfila2(DataTable dtDatosGastos, DataGridView dgvGastos)
        {
            if (dtDatosGastos != null)
            {
                dgvGastos.ReadOnly = false;

                DataRow newRow = dtDatosGastos.NewRow();
                dtDatosGastos.Rows.Add(newRow);

                int lastIndex = dtDatosGastos.Rows.Count - 1;

                if (lastIndex >= 0)
                {
                    DataGridViewColumn firstVisibleColumn = dgvGastos.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Visible);
                    if (firstVisibleColumn != null)
                    {
                        dgvGastos.ClearSelection();
                        dgvGastos.CurrentCell = dgvGastos.Rows[lastIndex].Cells[firstVisibleColumn.Index];
                        dgvGastos.BeginEdit(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se puede añadir la fila", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Controla el bloqueo/desbloqueo automático de la última fila de ingresos.
        /// - Verifica campos obligatorios y rango de saldo antes de bloquear.
        /// - Mantiene abierta la edición si faltan datos o el saldo es inválido.
        /// </summary>
        /// <param name="dtDatosIngresos">DataTable de origen de ingresos.</param>
        /// <param name="dataGridView1">DataGridView asociado.</param>
        /// <param name="RowIndex">Índice de la fila a comprobar.</param>
        public void BloquearDesbloquearDataIngresos(DataTable dtDatosIngresos, DataGridView dataGridView1, int RowIndex)
        {
            try
            {
                if (dataGridView1.IsCurrentCellInEditMode)
                {
                    dataGridView1.CancelEdit(); // cancelar en lugar de confirmar
                }
            }
            catch { return; }

            // 1. Resetear siempre a editable por defecto
            try
            {
                dataGridView1.ReadOnly = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    column.ReadOnly = false;
            }
            catch { return; }

            // 2. Validaciones iniciales
            if (RowIndex < 0 || dtDatosIngresos == null) return;

            int lastDataRowIndex = dtDatosIngresos.Rows.Count - 1;

            // 3. Solo actuar sobre la última fila
            if (RowIndex != lastDataRowIndex) return;

            DataGridViewRow currentRow = dataGridView1.Rows[RowIndex];

            // 4. Verificar campos vacíos
            string[] columnasAComprobar = { "NombreCuenta", "Detalle", "Saldo" };
            foreach (string nombreColumna in columnasAComprobar)
            {
                object cellValue = currentRow.Cells[nombreColumna].Value;
                if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    return; // Salir sin bloquear
                }
            }

            // 5. Validar Saldo
            string saldoTexto = currentRow.Cells["Saldo"].Value?.ToString() ?? "";
            if (!decimal.TryParse(saldoTexto, out decimal saldo)) return;

            if (saldo <= 0 || saldo > 100000000)
            {
                string mensaje = saldo <= 0 ? "El saldo no puede ser negativo o cero." : "El saldo no puede ser mayor a 100,000,000.";
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Salir sin bloquear
            }

            // 6. Si llegó aquí, todo está bien: BLOQUEAR
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.ReadOnly = true;
            }
        }

        /// <summary>
        /// Controla el bloqueo/desbloqueo automático de la última fila de gastos.
        /// Realiza validaciones similares a las de ingresos y mantiene la UI sincronizada.
        /// </summary>
        /// <param name="dtDatosGastos">DataTable de origen de gastos.</param>
        /// <param name="dgvgastos">DataGridView asociado a gastos.</param>
        /// <param name="RowIndex">Índice de la fila a comprobar.</param>
        public void BloquearDesbloquearDataGastos(DataTable dtDatosGastos, DataGridView dgvgastos, int RowIndex)
        {
            // Siempre permitir editar salvo que se decida bloquear
            dgvgastos.ReadOnly = false;

            if (RowIndex < 0 || dtDatosGastos == null)
                return;

            int lastDataRowIndex = dtDatosGastos.Rows.Count - 1;

            // Solo aplicar bloqueo automático en la ÚLTIMA fila
            if (RowIndex != lastDataRowIndex)
                return;

            DataGridViewRow currentRow = dgvgastos.Rows[RowIndex];
            bool algunCampoVacio = false;

            string[] columnasAComprobar = new string[]
            {
                "NombreCuenta",
                "Detalle",
                "Saldo",
            };

            // Verificar si falta algún dato
            foreach (string nombreColumna in columnasAComprobar)
            {
                object cellValue = currentRow.Cells[nombreColumna].Value;

                if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                {
                    algunCampoVacio = true;
                    break;
                }
            }

            // Si falta un campo → dejar todo editable
            if (algunCampoVacio)
            {
                foreach (DataGridViewColumn column in dgvgastos.Columns)
                    column.ReadOnly = false;

                return;
            }

            // Validar saldo
            string saldoTexto = currentRow.Cells["Saldo"].Value?.ToString() ?? "";
            decimal saldo = 0;

            if (!decimal.TryParse(saldoTexto, out saldo))
            {
                foreach (DataGridViewColumn column in dgvgastos.Columns)
                    column.ReadOnly = false;
                return;
            }

            if (saldo <= 0)
            {
                MessageBox.Show("El saldo no puede ser negativo o cero.", "Advertencia",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);

                foreach (DataGridViewColumn column in dgvgastos.Columns)
                    column.ReadOnly = false;

                return;
            }

            if (saldo > 100000000)
            {
                MessageBox.Show("El saldo no puede ser mayor a 100,000,000.", "Advertencia",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);

                foreach (DataGridViewColumn column in dgvgastos.Columns)
                    column.ReadOnly = false;

                return;
            }

            // SOLO SI TODO ES CORRECTO,BLOQUEAR FILA
            foreach (DataGridViewColumn column in dgvgastos.Columns)
                column.ReadOnly = true;
        }

    }
}
