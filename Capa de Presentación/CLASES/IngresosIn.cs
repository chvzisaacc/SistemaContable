using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;
using System.Data;


namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Funciones auxiliares para gestionar la edición y guardado de ingresos
    /// desde la capa de presentación. Mantiene sincronía entre la UI (DataGridView)
    /// y la capa de procesamiento de datos.
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class IngresosIn : Clsconexion
    {
        /// <summary>
        /// Habilita el modo edición del DataGridView para permitir que el usuario
        /// modifique los registros de ingresos directamente en la vista.
        /// - Desbloquea el control y sus celdas.
        /// - Ajusta el modo de edición y enfoque en la primera celda editable.
        /// </summary>
        /// <param name="dtIngresos">DataTable origen de datos (no se modifica directamente aquí).</param>
        /// <param name="dataGridView1">Control DataGridView que mostrará los ingresos.</param>
        public void editarIngreso(DataTable dtIngresos, DataGridView dataGridView1)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay filas para editar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Desbloquear todo el DataGridView para permitir edición
            dataGridView1.ReadOnly = false;

            // Permitir editar con clic y teclado; sincronizar comportamiento de selección
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;

            // Hacer todas las celdas editables y marcar visualmente para el usuario
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ReadOnly = false;
                    cell.Style.BackColor = Color.White; // opcional: indicar visualmente que es editable
                }
            }

            // Enfocar la primera celda visible y editable para comenzar la edición
            DataGridViewColumn firstVisibleColumn = dataGridView1.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(c => c.Visible && !c.ReadOnly);

            if (firstVisibleColumn != null && dataGridView1.Rows.Count > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[firstVisibleColumn.Index];
                dataGridView1.BeginEdit(true);
            }

            MessageBox.Show("Modo edición activado.",
                "Modo Edición Activado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Guarda la edición realizada en la fila seleccionada del DataGridView.
        /// - Valida la selección y el identificador de la transacción.
        /// - Llama a la capa de procesamiento para persistir los cambios.
        /// - Actualiza la UI (limpia controles y restablece el DGV a modo solo lectura) para mantener la sincronía.
        /// </summary>
        /// <param name="dtIngresos">Origen de datos (DataTable) asociado al DGV.</param>
        /// <param name="nombre_cuenta">Nombre de la cuenta para la transacción.</param>
        /// <param name="detalle">Detalle de la transacción.</param>
        /// <param name="saldo">Monto de la transacción.</param>
        /// <param name="fecha_transaccion">Fecha de la transacción.</param>
        /// <param name="referencia">Referencia asociada (convertible a entero).</param>
        /// <param name="id_origen">Identificador del origen de la transacción.</param>
        /// <param name="txtNoReferencia">TextBox con la referencia (opcional, se limpiará si procede).</param>
        /// <param name="cmbOrigen">ComboBox del origen (opcional, se restablecerá si procede).</param>
        /// <param name="dataGridView1">DataGridView que contiene la fila editada (opcional).</param>
        /// <param name="dtpFecha">DateTimePicker del formulario (opcional, se restablecerá si procede).</param>
        /// <returns>True si la actualización se realizó correctamente; de lo contrario false.</returns>
        public bool GuardarEdicion(DataTable dtIngresos, string nombre_cuenta, string detalle, decimal saldo,
        DateTime fecha_transaccion, string referencia, int id_origen,
        TextBox txtNoReferencia = null, ComboBox cmbOrigen = null,
        DataGridView dataGridView1 = null, DateTimePicker dtpFecha = null)
        {
            try
            {
                if (dataGridView1 == null || (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count == 0))
                {
                    dataGridView1?.EndEdit();
                    MessageBox.Show("No se seleccionó ninguna fila válida.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Si no hay CurrentRow pero hay filas seleccionadas, establecer la actual
                if (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.SelectedRows[0].Cells[0];
                }

                DataGridViewRow fila = dataGridView1.CurrentRow;

                // Validar ID antes de persistir en la capa de datos
                if (fila.Cells["Id_transaccion"].Value == null || fila.Cells["Id_transaccion"].Value == DBNull.Value)
                {
                    dataGridView1?.EndEdit();
                    MessageBox.Show("La fila seleccionada no tiene un ID válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int transaccion_id = Convert.ToInt32(fila.Cells["Id_transaccion"].Value);

                Ingresos ingresos = new Ingresos();

                int rowsAffected = ingresos.ModificarIngreso(
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
                    if (dataGridView1 != null)
                    {
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            col.ReadOnly = true;

                        dataGridView1.ReadOnly = true;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView1.MultiSelect = false;
                        dataGridView1.Enabled = true;
                        dataGridView1.ClearSelection();

                        // Seleccionar primera fila para evitar bloqueo visual
                        if (dataGridView1.Rows.Count > 0)
                            dataGridView1.Rows[0].Selected = true;
                    }

                    return true;
                }
                else
                {
                    dataGridView1?.EndEdit();
                    MessageBox.Show("No se pudo actualizar la transacción.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                dataGridView1?.EndEdit();
                MessageBox.Show("Error al guardar la edición: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Guarda la edición especial (sin restricción de tiempo) de un ingreso.
        /// Solo se invoca tras validación exitosa del código enviado por correo.
        /// Llama a ModificarIngresoEspecial en la capa de procesamiento.
        /// </summary>
        /// <param name="dtIngresos">Origen de datos (DataTable) asociado al DGV.</param>
        /// <param name="nombre_cuenta">Nombre de la cuenta para la transacción.</param>
        /// <param name="detalle">Detalle de la transacción.</param>
        /// <param name="saldo">Monto de la transacción.</param>
        /// <param name="fecha_transaccion">Fecha de la transacción.</param>
        /// <param name="referencia">Referencia asociada (convertible a entero).</param>
        /// <param name="id_origen">Identificador del origen de la transacción.</param>
        /// <param name="txtNoReferencia">TextBox con la referencia (opcional, se limpiará si procede).</param>
        /// <param name="cmbOrigen">ComboBox del origen (opcional, se restablecerá si procede).</param>
        /// <param name="dataGridView1">DataGridView que contiene la fila editada (opcional).</param>
        /// <param name="dtpFecha">DateTimePicker del formulario (opcional, se restablecerá si procede).</param>
        /// <returns>True si la actualización se realizó correctamente; de lo contrario false.</returns>
        public bool GuardarEdicionEspecial(DataTable dtIngresos, string nombre_cuenta, string detalle, decimal saldo,
            DateTime fecha_transaccion, string referencia, int id_origen,
            TextBox txtNoReferencia = null, ComboBox cmbOrigen = null,
            DataGridView dataGridView1 = null, DateTimePicker dtpFecha = null)
        {
            try
            {
                if (dataGridView1 == null || (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count == 0))
                {
                    dataGridView1?.EndEdit();
                    MessageBox.Show("No se seleccionó ninguna fila válida.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Si no hay CurrentRow pero hay filas seleccionadas, establecer la actual
                if (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.SelectedRows[0].Cells[0];
                }

                DataGridViewRow fila = dataGridView1.CurrentRow;

                // Validar ID antes de persistir en la capa de datos
                if (fila.Cells["Id_transaccion"].Value == null || fila.Cells["Id_transaccion"].Value == DBNull.Value)
                {
                    dataGridView1?.EndEdit();
                    MessageBox.Show("La fila seleccionada no tiene un ID válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int transaccion_id = Convert.ToInt32(fila.Cells["Id_transaccion"].Value);

                Ingresos ingresos = new Ingresos();

                // Única diferencia con GuardarEdicion: llama a ModificarIngresoEspecial
                int rowsAffected = ingresos.ModificarIngresoEspecial(
                    transaccion_id, fecha_transaccion, detalle, saldo,
                    Convert.ToInt32(referencia), Sesion1.usuario_id, id_origen, nombre_cuenta
                );

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registro modificado con éxito (Edición Especial).", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar controles del formulario para reflejar el nuevo estado
                    txtNoReferencia?.Clear();
                    cmbOrigen?.ResetText();
                    if (cmbOrigen != null) cmbOrigen.SelectedIndex = -1;
                    if (dtpFecha != null) dtpFecha.Value = DateTime.Now;

                    // Restablecer el DataGridView a modo solo lectura
                    if (dataGridView1 != null)
                    {
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            col.ReadOnly = true;

                        dataGridView1.ReadOnly = true;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView1.MultiSelect = false;
                        dataGridView1.Enabled = true;
                        dataGridView1.ClearSelection();

                        if (dataGridView1.Rows.Count > 0)
                            dataGridView1.Rows[0].Selected = true;
                    }

                    return true;
                }
                else
                {
                    dataGridView1?.EndEdit();
                    MessageBox.Show("No se pudo actualizar la transacción.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                dataGridView1?.EndEdit();
                MessageBox.Show("Error al guardar la edición especial: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}



