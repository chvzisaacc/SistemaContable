using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Capa_de_Presentación.CLASES
{
    public class IngresosIn:Clsconexion
    {
        public void editarIngreso(DataTable dtIngresos, DataGridView dataGridView1)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                dataGridView1.ReadOnly = false;

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {

                    cell.ReadOnly = false;
                }

                DataGridViewColumn firstVisibleColumn = dataGridView1.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Visible);

                if (firstVisibleColumn != null)
                {
                    dataGridView1.CurrentCell = selectedRow.Cells[firstVisibleColumn.Index];
                    dataGridView1.BeginEdit(true);
                }

                MessageBox.Show("Fila habilitada para edición", "Modo Edición Activado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila antes de editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        

    }
}



