using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Diego;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentación.CLASES
{
    public class ClsCD
    {
        public ClsCD()
        {
        }

        public void Agregarfila(DataTable dtDatosCertificados, DataGridView dataGridView1)
        {
            if (dtDatosCertificados != null)
            {
                dataGridView1.ReadOnly = false;

                DataRow newRow = dtDatosCertificados.NewRow();
                dtDatosCertificados.Rows.Add(newRow);

                int lastIndex = dataGridView1.Rows.Count - 1;

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

        public void BloquearDesbloquearData(DataTable dtDatosCertificados, DataGridView dataGridView1, int RowIndex)
        {
            if (RowIndex >= 0)
            {
                dataGridView1.ReadOnly = true;
            }

            if (RowIndex >= 0 && dtDatosCertificados != null)
            {
                int lastDataRowIndex = dtDatosCertificados.Rows.Count - 1;

                if (RowIndex == lastDataRowIndex)
                {
                    DataGridViewRow currentRow = dataGridView1.Rows[RowIndex];
                    bool algunCampoVacio = false;

                    string[] columnasAComprobar = new string[]
                    {
                        "Nombre_certificado",
                        "deposito_inicial",
                        "plazo",
                        "tasa"
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

        public void GuardarCD(DataTable dtDatosCertificados, DataGridView dataGridView1, bool datosGuardados)
        {

            if (dataGridView1.Rows.Count > 0)
            {

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    if (
                        selectedRow.Cells["Nombre_Certificado"].Value != null &&
                        selectedRow.Cells["deposito_inicial"].Value != null &&
                        selectedRow.Cells["Plazo"].Value != null &&
                        selectedRow.Cells["Tasa"].Value != null)
                    {

                        string nombreCertificado = selectedRow.Cells["Nombre_certificado"].Value.ToString();
                        decimal depositoInicial = Convert.ToDecimal(selectedRow.Cells["deposito_inicial"].Value);
                        int plazo = Convert.ToInt32(selectedRow.Cells["Plazo"].Value);
                        decimal tasa = Convert.ToDecimal(selectedRow.Cells["Tasa"].Value);

                        ClsAccionesDB acciones = new ClsAccionesDB();
                        acciones.GuardarCertificado(nombreCertificado, depositoInicial, plazo, tasa);

                        MessageBox.Show("Datos guardados con éxito.");

                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            cell.ReadOnly = true;
                        }
                        datosGuardados = true;
                    }
                    else
                    {
                        MessageBox.Show("Hay campos vacíos en la fila seleccionada.");
                    }
                }
                else
                {
                    MessageBox.Show("No hay filas seleccionadas.");
                }

            }
        }

        public void editarCD(DataTable dtDatosCertificados, DataGridView dataGridView1)
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

        public void guardaredic(DataTable dtDatosCertificados, DataGridView dataGridView1, ref bool modoEdicionActivo)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modoEdicionActivo)
            {
                try
                {
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    DataGridViewRow fila = dataGridView1.SelectedRows[0];

                    int codigocertificado = Convert.ToInt32(fila.Cells["Id_Certificado"].Value);
                    string nombreCertificado = fila.Cells["Nombre_certificado"].Value?.ToString() ?? string.Empty;
                    decimal depositoInicial = Convert.ToDecimal(fila.Cells["deposito_inicial"].Value);
                    int plazo = Convert.ToInt32(fila.Cells["Plazo"].Value);
                    decimal tasa = Convert.ToDecimal(fila.Cells["Tasa"].Value);

                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    accionesDB.editarcertificado(codigocertificado, nombreCertificado, depositoInicial, plazo, tasa);

                    dataGridView1.ReadOnly = true;
                    modoEdicionActivo = false;

                    MessageBox.Show("Cambios guardados exitosamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                editarCD(dtDatosCertificados, dataGridView1);
                modoEdicionActivo = true;
            }
        }

        public void renovarCD(DataTable dtDatosCertificados, DataGridView dataGridView1, ref bool modoEdicionActivo)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modoEdicionActivo)
            {
                try
                {
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    dataGridView1.EndEdit();

                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    DataGridViewRow fila = dataGridView1.SelectedRows[0];

                    int codigocertificado = Convert.ToInt32(fila.Cells["Id_certificado"].Value);
                    decimal depositoInicial = Convert.ToDecimal(fila.Cells["deposito_inicial"].Value);
                    int plazo = Convert.ToInt32(fila.Cells["plazo"].Value);
                    decimal tasa = Convert.ToDecimal(fila.Cells["tasa"].Value);

                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    accionesDB.renovarCertificado(codigocertificado, depositoInicial, plazo, tasa);

                    dataGridView1.ReadOnly = true;
                    modoEdicionActivo = false;

                    MessageBox.Show("Cambios guardados exitosamente. El certificado ha sido renovado.", "Renovación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dataGridView1.ReadOnly = true;
                    modoEdicionActivo = false;
                    MessageBox.Show("Error al guardar la renovación: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    dataGridView1.ReadOnly = false;

                    if (dataGridView1.Columns.Contains("Nombre_certificado"))
                    {
                        dataGridView1.Columns["Nombre_certificado"].ReadOnly = true;
                    }
                    if (dataGridView1.Columns.Contains("Id_certificado"))
                    {
                        dataGridView1.Columns["Id_certificado"].ReadOnly = true;
                    }

                    DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                    dataGridView1.CurrentCell = filaSeleccionada.Cells["deposito_inicial"];
                    dataGridView1.BeginEdit(true); 

                    modoEdicionActivo = true;
                    MessageBox.Show("Modo de edición activado. Modifique los datos y vuelva a presionar el botón para guardar.", "Edición Activada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dataGridView1.ReadOnly = true;
                    MessageBox.Show("Error al renovar el certificado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void cancelarCertificado(DataGridView dataGridView1)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un certificado para cancelar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
            int codigoCertificado = Convert.ToInt32(filaSeleccionada.Cells["Id_Certificado"].Value);
            string motivoCancelacion = string.Empty;

           
            using (FRM_PG108 frmCancel = new FRM_PG108())
            {
                if (frmCancel.ShowDialog() == DialogResult.OK)
                {
         
                    motivoCancelacion = frmCancel.ObtenerMotivo();
                }
                else
                {
                   
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(motivoCancelacion))
            {
                MessageBox.Show("Debe ingresar un motivo. Cancelación abortada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ClsAccionesDB objAcciones = new ClsAccionesDB();

                objAcciones.cancelarCertificado(codigoCertificado, motivoCancelacion);

                
                dataGridView1.Rows.Remove(filaSeleccionada);

                MessageBox.Show("Certificado cancelado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar el certificado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void cancelarCertificado(object dataGridView1)
        {
            throw new NotImplementedException();
        }

        public void CargarCertificadosIntereses(DataGridView dgv)
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                DataTable dtDatosCertificados = acciones.CargarCertificados();

                dtDatosCertificados.Columns.Add("Ganancia_Generado", typeof(decimal));
                dtDatosCertificados.Columns.Add("Total_Acumulado", typeof(decimal));

                foreach (DataRow row in dtDatosCertificados.Rows)
                {
                    decimal deposito = Convert.ToDecimal(row["deposito_inicial"]);
                    decimal tasa = Convert.ToDecimal(row["tasa"]);
                    int plazo = Convert.ToInt32(row["plazo"]);

                    decimal gananciaSinRedondear = deposito * (tasa / 100) * plazo;
                    decimal totalSinRedondear = deposito + gananciaSinRedondear;

                    decimal ganancia = Math.Round(gananciaSinRedondear, 2);
                    decimal totalAcumulado = Math.Round(totalSinRedondear, 2);

                    row["Ganancia_Generado"] = ganancia;
                    row["Total_Acumulado"] = totalAcumulado;
                }

                dgv.Columns.Clear();
                dgv.DataSource = dtDatosCertificados;

                dgv.AllowUserToAddRows = false;
                dgv.AutoResizeColumns();
                dgv.ReadOnly = true;

                if (dgv.Columns.Contains("Id_certificado"))
                {
                    dgv.Columns["Id_certificado"].Visible = false;
                }
                if (dgv.Columns.Contains("plazo"))
                {
                    dgv.Columns["plazo"].Visible = false;
                }
                if (dgv.Columns.Contains("tasa"))
                {
                    dgv.Columns["tasa"].Visible = false;
                }

                if (dgv.Columns.Contains("Ganancia_Generado"))
                {
                    dgv.Columns["Ganancia_Generado"].DefaultCellStyle.Format = "N2";
                    dgv.Columns["Ganancia_Generado"].HeaderText = "Ganancia Generada";
                }
                if (dgv.Columns.Contains("Total_Acumulado"))
                {
                    dgv.Columns["Total_Acumulado"].DefaultCellStyle.Format = "N2";
                    dgv.Columns["Total_Acumulado"].HeaderText = "Total Acumulado";
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la carga y cálculo de certificados: " + ex.Message, ex);
            }
        }

        public void CargarCuentasBancarias(DataGridView dgv)
        {
            ClsAccionesDB acciones = new ClsAccionesDB();
            DataTable dtDatosCertificados = acciones.CargarCuentasBancarias();
            dtDatosCertificados.Columns.Add("GananciaGenerada", typeof(decimal));
            dtDatosCertificados.Columns.Add("TotalAcumulado", typeof(decimal));

            foreach (DataRow row in dtDatosCertificados.Rows)
            {
                decimal saldo = Convert.ToDecimal(row["Saldo"]);
                decimal tasa = Convert.ToDecimal(row["tasa_interes"]);

                decimal gananciaSinRedondear = saldo * (tasa / 100);
                decimal totalacumuladoSinRedondear = gananciaSinRedondear + saldo;


                decimal ganancia = Math.Round(gananciaSinRedondear, 2);
                decimal total = Math.Round(totalacumuladoSinRedondear, 2);

                row["GananciaGenerada"] = ganancia;
                row["TotalAcumulado"] = total;
            }
            dgv.Columns.Clear();
            dgv.DataSource = dtDatosCertificados;

            dgv.AllowUserToAddRows = false;
            dgv.AutoResizeColumns();
            dgv.ReadOnly = true;
            if (dgv.Columns.Contains("Id_cuentaBanco"))
            {
                dgv.Columns["Id_cuentaBanco"].Visible = false;
            }


            dgv.DataSource = dtDatosCertificados;


        }
    }


}

