using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Capa_de_Presentación.CLASES
{
    public class IngresosIn : Clsconexion
    {
        public void editarIngreso(DataTable dtIngresos, DataGridView dataGridView1)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay filas para editar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Desbloquear todo el DataGridView
            dataGridView1.ReadOnly = false;

            //Permitir editar con clic y teclado
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;

            //Hacer todas las celdas editables y visualmente activas
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ReadOnly = false;
                    cell.Style.BackColor = Color.White; // opcional: color editable
                }
            }

            //Enfocar la primera celda visible editable
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

        public bool GuardarEdicion(DataTable dtIngresos, string nombreCuenta, string detalle, decimal saldo,
        DateTime fechaTransaccion, string referencia, int idOrigen,
        TextBox txtNoReferencia = null, ComboBox cmbOrigen = null,
        DataGridView dataGridView1 = null, DateTimePicker dtpFecha = null)
        {
            try
            {
                // Validar selección de fila
                if (dataGridView1 != null && dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.SelectedRows[0].Cells[0];
                    MessageBox.Show("No se seleccionó ninguna fila válida.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DataGridViewRow fila = dataGridView1.CurrentRow;

                // Validar ID
                if (fila.Cells["Id_transaccion"].Value == null || fila.Cells["Id_transaccion"].Value == DBNull.Value)
                {
                    MessageBox.Show("La fila seleccionada no tiene un ID válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int transaccionId = Convert.ToInt32(fila.Cells["Id_transaccion"].Value);

                Ingresos ingresos = new Ingresos();

                int rowsAffected = ingresos.ModificarIngreso(
                    transaccionId, fechaTransaccion, detalle, saldo,
                    Convert.ToInt32(referencia), Sesion1.UsuarioId, idOrigen, nombreCuenta
                );

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registro modificado con éxito.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar controles
                    txtNoReferencia?.Clear();
                    cmbOrigen?.ResetText();
                    if (cmbOrigen != null) cmbOrigen.SelectedIndex = -1;
                    if (dtpFecha != null) dtpFecha.Value = DateTime.Now;

                    // 🔹 Dejar el DataGridView bloqueado para edición,
                    // pero habilitado para selección y navegación.
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



