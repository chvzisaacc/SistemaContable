using Capa_de_acceso_de_datos;
using System.Data;


namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Operaciones relacionadas con gastos: habilitar edición en la UI y
    /// persistir cambios en la capa de datos manteniendo la sincronía entre
    /// el origen (DB) y la vista (<see cref="DataGridView"/>).
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class GastosGa : Clsconexion
    {
        /// <summary>
        /// Habilita la edición de gastos en el DataGridView.
        /// - Cambia el modo de edición y selección para permitir entrada por teclado o F2.
        /// - Marca todas las celdas como editables y actualiza su apariencia para indicar estado editable.
        /// </summary>
        /// <param name="dtGasto">Origen de datos (DataTable) asociado al DGV.</param>
        /// <param name="dgvGastos">Control DataGridView que mostrará/permitirá la edición.</param>
        public void editarGasto(DataTable dtGasto, DataGridView dgvGastos)
        {
            if (dgvGastos.Rows.Count == 0)
            {
                MessageBox.Show("No hay filas para editar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvGastos.ReadOnly = false;

            // Permitir editar con clic y teclado; sincronizar comportamiento de selección
            dgvGastos.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvGastos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvGastos.MultiSelect = false;

            // Hacer todas las celdas editables y marcar visualmente para el usuario
            foreach (DataGridViewRow row in dgvGastos.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ReadOnly = false;
                    cell.Style.BackColor = Color.White; // opcional: indicar visualmente que es editable
                }
            }
        }

        /// <summary>
        /// Guarda la edición realizada en la fila seleccionada del DataGridView.
        /// - Valida la selección y el identificador de la transacción.
        /// - Llama a la capa de procesamiento para persistir los cambios.
        /// - Actualiza la UI (limpia controles y restablece el DGV a modo solo lectura) para mantener la sincronía.
        /// </summary>
        /// <param name="dtGasto">Origen de datos (no modificado directamente aquí).</param>
        /// <param name="nombre_cuenta">Nombre de la cuenta asociada a la transacción.</param>
        /// <param name="detalle">Detalle de la transacción.</param>
        /// <param name="saldo">Monto de la transacción.</param>
        /// <param name="fecha_transaccion">Fecha de la transacción.</param>
        /// <param name="referencia">Referencia asociada (se convierte a entero).</param>
        /// <param name="id_origen">Identificador del origen de la transacción.</param>
        /// <param name="txtNoReferencia">Control TextBox que contiene la referencia (opcional, se limpia si procede).</param>
        /// <param name="cmbOrigen">ComboBox de origen (opcional, se restablece si procede).</param>
        /// <param name="dgvGastos">DataGridView que contiene la fila editada (opcional).</param>
        /// <param name="dtpFecha">DateTimePicker del formulario (opcional, se restablece si procede).</param>
        /// <returns>True si la actualización en BD fue exitosa; false en caso contrario.</returns>
        public bool GuardarEdicion2(DataTable dtGasto, string nombre_cuenta, string detalle, decimal saldo,
        DateTime fecha_transaccion, string referencia, int id_origen,
        TextBox txtNoReferencia = null, ComboBox cmbOrigen = null,
        DataGridView dgvGastos = null, DateTimePicker dtpFecha = null)
        {
            try
            {
                // Validar selección de fila en la UI
                if (dgvGastos != null && dgvGastos.CurrentRow == null && dgvGastos.SelectedRows.Count > 0)
                {
                    dgvGastos.CurrentCell = dgvGastos.SelectedRows[0].Cells[0];
                    MessageBox.Show("No se seleccionó ninguna fila válida.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DataGridViewRow fila = dgvGastos.CurrentRow;

                // Validar que la fila contiene un ID de transacción válido antes de persistir
                if (fila.Cells["Id_transaccion"].Value == null || fila.Cells["Id_transaccion"].Value == DBNull.Value)
                {
                    MessageBox.Show("La fila seleccionada no tiene un ID válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int transaccion_id = Convert.ToInt32(fila.Cells["Id_transaccion"].Value);

                // Llamada a la capa de procesamiento de datos para actualizar la transacción
                Capa_de_procesamiento_de_datos.Gastos gastos = new Capa_de_procesamiento_de_datos.Gastos();

                int rowsAffected = gastos.ModificarGastos(
                    transaccion_id, fecha_transaccion, detalle, saldo,
                    Convert.ToInt32(referencia), Sesion1.usuario_id, id_origen, nombre_cuenta
                );

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registro modificado con éxito.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar controles del formulario para reflejar el nuevo estado
                    txtNoReferencia?.Clear();
                    cmbOrigen?.ResetText();
                    if (cmbOrigen != null) cmbOrigen.SelectedIndex = -1;
                    if (dtpFecha != null) dtpFecha.Value = DateTime.Now;

                    // Restablecer el DataGridView a modo solo lectura para evitar ediciones accidentales
                    if (dgvGastos != null)
                    {
                        foreach (DataGridViewColumn col in dgvGastos.Columns)
                            col.ReadOnly = true;

                        dgvGastos.ReadOnly = true;
                        dgvGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvGastos.MultiSelect = false;
                        dgvGastos.Enabled = true;
                        dgvGastos.ClearSelection();

                        // Seleccionar primera fila para evitar bloqueo visual
                        if (dgvGastos.Rows.Count > 0)
                            dgvGastos.Rows[0].Selected = true;
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la transacción.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la edición: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
