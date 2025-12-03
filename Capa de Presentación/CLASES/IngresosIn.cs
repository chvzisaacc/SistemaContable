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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class IngresosIn : Clsconexion
    {
        /// <summary>
        /// Editars the ingreso.
        /// </summary>
        /// <param name="dtIngresos">The dt ingresos.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
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

        /// <summary>
        /// Guardars the edicion.
        /// </summary>
        /// <param name="dtIngresos">The dt ingresos.</param>
        /// <param name="nombre_cuenta">The nombre cuenta.</param>
        /// <param name="detalle">The detalle.</param>
        /// <param name="saldo">The saldo.</param>
        /// <param name="fecha_transaccion">The fecha transaccion.</param>
        /// <param name="referencia">The referencia.</param>
        /// <param name="id_origen">The identifier origen.</param>
        /// <param name="txtNoReferencia">The text no referencia.</param>
        /// <param name="cmbOrigen">The CMB origen.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <param name="dtpFecha">The DTP fecha.</param>
        /// <returns></returns>
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

                // Si no hay CurrentRow pero hay filas seleccionadas
                if (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.SelectedRows[0].Cells[0];
                }

                DataGridViewRow fila = dataGridView1.CurrentRow;

                // Validar ID
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

                    // Limpiar controles
                    txtNoReferencia?.Clear();
                    cmbOrigen?.ResetText();
                    if (cmbOrigen != null) cmbOrigen.SelectedIndex = -1;
                    if (dtpFecha != null) dtpFecha.Value = DateTime.Now;

                    // Dejar el DataGridView bloqueado para edición,
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

        /*public void BloquearDesbloquearIngresos(DataTable dtDatosIngresos, DataGridView dataGridView1, int rowIndex)
        {
            
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            
            object idTransaccionValue = row.Cells["Id_transaccion"].Value;

            int idTransaccion = 0;

           
            bool yaEstaGuardada = idTransaccionValue != null &&
                                  idTransaccionValue != DBNull.Value &&
                                  int.TryParse(idTransaccionValue.ToString(), out idTransaccion) &&
                                  idTransaccion > 0;

           
            if (yaEstaGuardada)
            { 
               
                row.Cells["NombreCuenta"].ReadOnly = true;
                row.Cells["Detalle"].ReadOnly = true;
                row.Cells["Saldo"].ReadOnly = true;
               
            }
            else 
            {
                
                row.Cells["NombreCuenta"].ReadOnly = false;
                row.Cells["Detalle"].ReadOnly = false;
                row.Cells["Saldo"].ReadOnly = false;

            }
        }
        */


    }
}



