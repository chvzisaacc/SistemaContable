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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Capa_de_acceso_de_datos.Clsconexion" />
    public class Transacciones : Clsconexion
    {
        /// <summary>
        /// Cargars the ComboBox origen.
        /// </summary>
        /// <param name="cmbOrigen">The CMB origen.</param>
        /// <param name="cmbOrigen2">The CMB origen2.</param>
        public void CargarComboBoxOrigen(ComboBox cmbOrigen, ComboBox cmbOrigen2)
        {
            ClsAccionesDB clsAccionesDB = new ClsAccionesDB();

            try
            {
                ClsAccionesDB db = new ClsAccionesDB();
                List<Origen> lista = db.ObtenerListaOrigenes();
                List<Origen> lista2 = db.ObtenerListaOrigenes();

                lista.Insert(0, new Origen(0, "Seleccionar"));
                lista2.Insert(0, new Origen(0, "Seleccionar"));

                cmbOrigen.DataSource = lista;
                cmbOrigen.DisplayMember = "Nombre";
                cmbOrigen.ValueMember = "ID";
                cmbOrigen.SelectedIndex = 0;

                cmbOrigen2.DataSource = lista2;
                cmbOrigen2.DisplayMember = "Nombre";
                cmbOrigen2.ValueMember = "ID";
                cmbOrigen2.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los orígenes: " + ex.Message, "Error");
            }
        }

        /// <summary>
        /// Cargars the ComboBox cuentas.
        /// </summary>
        /// <param name="cmbCuentas">The CMB cuentas.</param>
        public void CargarComboBoxCuentas(ComboBox cmbCuentas)
        {
                cmbCuentas.Items.Clear();
                cmbCuentas.Items.Add("Cuentas Bancarias");
                cmbCuentas.SelectedIndex = 0;
        }

        /// <summary>
        /// Agregarfilas the specified dt datos ingresos.
        /// </summary>
        /// <param name="dtDatosIngresos">The dt datos ingresos.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
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
        /// Agregarfila2s the specified dt datos gastos.
        /// </summary>
        /// <param name="dtDatosGastos">The dt datos gastos.</param>
        /// <param name="dgvGastos">The DGV gastos.</param>
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
        /// Bloquears the desbloquear data ingresos.
        /// </summary>
        /// <param name="dtDatosIngresos">The dt datos ingresos.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <param name="RowIndex">Index of the row.</param>
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

        /// <summary>
        /// Bloquears the desbloquear data gastos.
        /// </summary>
        /// <param name="dtDatosGastos">The dt datos gastos.</param>
        /// <param name="dgvgastos">The dgvgastos.</param>
        /// <param name="RowIndex">Index of the row.</param>
        public void BloquearDesbloquearDataGastos(DataTable dtDatosGastos, DataGridView dgvgastos, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                dgvgastos.ReadOnly = false;
            }

            if (RowIndex >= 0 && dtDatosGastos != null)
            {
                int lastDataRowIndex = dtDatosGastos.Rows.Count - 1;

                if (RowIndex == lastDataRowIndex)
                {
                    DataGridViewRow currentRow = dgvgastos.Rows[RowIndex];
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
                        dgvgastos.ReadOnly = false;

                        foreach (DataGridViewColumn column in dgvgastos.Columns)
                        {
                            column.ReadOnly = false;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Agregarfila2s the specified dt datos gastos.
        /// </summary>
        /// <param name="dtDatosGastos">The dt datos gastos.</param>
        /// <param name="dgvGastos">The DGV gastos.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal void Agregarfila2(object dtDatosGastos, DataGridView dgvGastos)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cargars the ComboBox origen.
        /// </summary>
        /// <param name="cmbOrigen">The CMB origen.</param>
        /// <param name="cmbOrigen2">The CMB origen2.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal void CargarComboBoxOrigen(object cmbOrigen, object cmbOrigen2)
        {
            throw new NotImplementedException();
        }
    }
}
