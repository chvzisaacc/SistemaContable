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
    public class GastosGa: Clsconexion
    {

        public void editarGasto(DataTable dtGasto, DataGridView dgvGastos)
        {
            if (dgvGastos.Rows.Count == 0)
            {
                MessageBox.Show("No hay filas para editar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            dgvGastos.ReadOnly = false;

            //Permitir editar con clic y teclado
            dgvGastos.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvGastos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvGastos.MultiSelect = false;

            //Hacer todas las celdas editables y visualmente activas
            foreach (DataGridViewRow row in dgvGastos.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ReadOnly = false;
                    cell.Style.BackColor = Color.White; // opcional: color editable
                }
            }
        }

        public bool GuardarEdicion2(DataTable dtGasto, string nombreCuenta, string detalle, decimal saldo,
        DateTime fechaTransaccion, string referencia, int idOrigen,
        TextBox txtNoReferencia = null, ComboBox cmbOrigen = null,
        DataGridView dgvGastos = null, DateTimePicker dtpFecha = null)
        {
            try
            {
                // Validar selección de fila
                if (dgvGastos != null && dgvGastos.CurrentRow == null && dgvGastos.SelectedRows.Count > 0)
                {
                    dgvGastos.CurrentCell = dgvGastos.SelectedRows[0].Cells[0];
                    MessageBox.Show("No se seleccionó ninguna fila válida.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DataGridViewRow fila = dgvGastos.CurrentRow;

                // Validar ID
                if (fila.Cells["Id_transaccion"].Value == null || fila.Cells["Id_transaccion"].Value == DBNull.Value)
                {
                    MessageBox.Show("La fila seleccionada no tiene un ID válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int transaccionId = Convert.ToInt32(fila.Cells["Id_transaccion"].Value);

                Capa_de_procesamiento_de_datos.Gastos gastos = new Capa_de_procesamiento_de_datos.Gastos();

                int rowsAffected = gastos.ModificarGastos(
                    transaccionId, fechaTransaccion, detalle, saldo,
                    Convert.ToInt32(referencia), Sesion1.UsuarioID, idOrigen, nombreCuenta
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

                    //Dejar el DataGridView bloqueado para edición,
                    // pero habilitado para selección y navegación.
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
