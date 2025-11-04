using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    public class Transacciones : Clsconexion
    {
        public void CargarComboBoxOrigen(ComboBox cmbOrigen)
        {
            ClsAccionesDB clsAccionesDB = new ClsAccionesDB();

            try
            {
                List<Origen> lista = clsAccionesDB.ObtenerListaOrigenes();

                cmbOrigen.Items.Clear();
                cmbOrigen.DisplayMember = "Nombre";

                foreach (Origen item in lista)
                {
                    cmbOrigen.Items.Add(item);
                }

                if (cmbOrigen.Items.Count > 0)
                {
                    cmbOrigen.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los orígenes: " + ex.Message, "Error");
            }
        }

        public void Agregarfila(DataTable dtDatosIngresos, DataGridView dataGridView1)
        {
            if (dtDatosIngresos != null)
            {
                dataGridView1.ReadOnly = false;

                DataRow newRow = dtDatosIngresos.NewRow();
                dtDatosIngresos.Rows.Add(newRow);

                int lastIndex = dtDatosIngresos.Rows.Count - 1;

                if (lastIndex >= 0)
                {
                    DataGridViewColumn firstVisibleColumn = dataGridView1.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Visible);

                    if (firstVisibleColumn != null)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[lastIndex].Cells[firstVisibleColumn.Index];
                    }
                }
            }
            else
            {
                MessageBox.Show("No se puede añadir la fila.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void BloquearDesbloquearDataIngresos(DataTable dtDatosIngresos, DataGridView dataGridView1, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                dataGridView1.ReadOnly = true;
            }

            if (RowIndex >= 0 && dtDatosIngresos != null)
            {
                int lastDataRowIndex = dtDatosIngresos.Rows.Count - 1;

                if (RowIndex == lastDataRowIndex)
                {
                    DataGridViewRow currentRow = dataGridView1.Rows[RowIndex];
                    bool algunCampoVacio = false;

                    string[] columnasAComprobar = new string[]
                    {
                        "NombreCuenta",
                        "Detalle",
                        "Saldo",

                    };

                    foreach (string nombreColumna in columnasAComprobar)
                    {
                        object cellValue = currentRow.Cells[nombreColumna].Value;

                        if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                        {
                            algunCampoVacio = true;
                            break;
                        }
                    }

                    if (algunCampoVacio)
                    {
                        dataGridView1.ReadOnly = false;

                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            column.ReadOnly = false;
                        }
                    }

                }
            }
        }
        
    }
}
