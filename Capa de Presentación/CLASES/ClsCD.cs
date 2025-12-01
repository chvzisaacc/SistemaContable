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
    /// <summary>
    /// 
    /// </summary>
    public class ClsCD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClsCD"/> class.
        /// </summary>
        public ClsCD()
        {
        }

        /// <summary>
        /// Agregarfilas the specified dt datos certificados.
        /// </summary>
        /// <param name="dtDatosCertificados">The dt datos certificados.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
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

        /// <summary>
        /// Bloquears the desbloquear data.
        /// </summary>
        /// <param name="dtDatosCertificados">The dt datos certificados.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <param name="RowIndex">Index of the row.</param>
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

        /// <summary>
        /// Guardars the cd.
        /// </summary>
        /// <param name="dtDatosCertificados">The dt datos certificados.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <param name="datos_guardados">if set to <c>true</c> [datos guardados].</param>
        /// <exception cref="System.FormatException">
        /// El Depósito Inicial no es un número válido.
        /// or
        /// El Plazo no es un número entero válido.
        /// or
        /// La Tasa no es un número válido.
        /// </exception>
        public void GuardarCD(DataTable dtDatosCertificados, DataGridView dataGridView1, bool datos_guardados)
        {
            int id_parroquia = Capa_de_acceso_de_datos.Sesion1.id_parroquia;

            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay filas con datos válidos para guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dataGridView1.Rows.Count > 1 && dataGridView1.Rows[dataGridView1.Rows.Count - 1].IsNewRow
                ? dataGridView1.Rows[dataGridView1.Rows.Count - 2]
                : dataGridView1.Rows[dataGridView1.Rows.Count - 1];

            // Validación de Nulos y Celdas Vacías
            string[] columnas_obligatorias = new string[] { "Nombre_certificado", "deposito_inicial", "Plazo", "Tasa" };

            foreach (string nombre_columna in columnas_obligatorias)
            {
                object cellValue = fila.Cells[nombre_columna]?.Value;
                if (cellValue == null || cellValue == DBNull.Value || string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    MessageBox.Show($"El campo '{nombre_columna}' está vacío. Llenalos todos antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (id_parroquia <= 0)
            {
                MessageBox.Show("Error: No se pudo obtener el ID de Parroquia del usuario logeado. Reinicie la sesión.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string nombre_certificado = fila.Cells["Nombre_certificado"].Value.ToString();

                decimal deposito_inicial;
                if (!decimal.TryParse(fila.Cells["deposito_inicial"].Value.ToString(), out deposito_inicial))
                {
                    throw new FormatException("El Depósito Inicial no es un número válido.");
                }

                int plazo;
                if (!int.TryParse(fila.Cells["Plazo"].Value.ToString(), out plazo))
                {
                    throw new FormatException("El Plazo no es un número entero válido.");
                }

                decimal tasa;
                if (!decimal.TryParse(fila.Cells["Tasa"].Value.ToString(), out tasa))
                {
                    throw new FormatException("La Tasa no es un número válido.");
                }

                DateTime fecha_transaccion_actual = DateTime.Now;

                // Aquí se guarda el certificado y recupera el ID generado
                ClsAccionesDB acciones = new ClsAccionesDB();
                int idCertificadoGenerado = acciones.GuardarCertificado(nombre_certificado, deposito_inicial, plazo, tasa, id_parroquia, fecha_transaccion_actual);

                // Asignar el Id generado a la fila
                fila.Cells["Id_Certificado"].Value = idCertificadoGenerado;
                fila.Cells["FechaTransaccion"].Value = fecha_transaccion_actual;

                MessageBox.Show("Última fila guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Bloquear la fila después de guardar
                foreach (DataGridViewCell cell in fila.Cells)
                {
                    cell.ReadOnly = true;
                }

                datos_guardados = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la última fila: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Editars the cd.
        /// </summary>
        /// <param name="dtDatosCertificados">The dt datos certificados.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
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

        /// <summary>
        /// Guardaredics the specified dt datos certificados.
        /// </summary>
        /// <param name="dtDatosCertificados">The dt datos certificados.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <param name="modo_edicion_activo">if set to <c>true</c> [modo edicion activo].</param>
        public void guardaredic(DataTable dtDatosCertificados, DataGridView dataGridView1, ref bool modo_edicion_activo)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modo_edicion_activo)
            {
                try
                {
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    DataGridViewRow fila = dataGridView1.SelectedRows[0];
                    int codigo_certificado = 0;

                    // Verificar si el campo "Id_Certificado" tiene un valor válido
                    DataGridViewCell pkCell = fila.Cells["Id_Certificado"];

                    if (pkCell == null || pkCell.Value == DBNull.Value || pkCell.Value == null)
                    {
                        MessageBox.Show("No se puede obtener el ID para editar. Asegúrese de que la fila esté guardada.", "Error de ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar si el valor del "Id_Certificado" es un número válido
                    if (!int.TryParse(pkCell.Value.ToString(), out codigo_certificado))
                    {
                        MessageBox.Show("El código del certificado no tiene un formato numérico válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string nombre_certificado = fila.Cells["Nombre_certificado"].Value?.ToString() ?? string.Empty;
                    decimal deposito_inicial = Convert.ToDecimal(fila.Cells["deposito_inicial"].Value);
                    int plazo = Convert.ToInt32(fila.Cells["Plazo"].Value);
                    decimal tasa = Convert.ToDecimal(fila.Cells["Tasa"].Value);

                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    accionesDB.editarcertificado(codigo_certificado, nombre_certificado, deposito_inicial, plazo, tasa);

                    dataGridView1.ReadOnly = true;
                    modo_edicion_activo = false;

                    MessageBox.Show("Cambios guardados exitosamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no está en modo de edición, se llama al método para activar la edición
                editarCD(dtDatosCertificados, dataGridView1);
                modo_edicion_activo = true;
            }
        }

        /// <summary>
        /// Renovars the cd.
        /// </summary>
        /// <param name="dtDatosCertificados">The dt datos certificados.</param>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <param name="modo_edicion_activo">if set to <c>true</c> [modo edicion activo].</param>
        public void renovarCD(DataTable dtDatosCertificados, DataGridView dataGridView1, ref bool modo_edicion_activo)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modo_edicion_activo)
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

                    int codigo_certificado = Convert.ToInt32(fila.Cells["Id_certificado"].Value);
                    decimal deposito_inicial = Convert.ToDecimal(fila.Cells["deposito_inicial"].Value);
                    int plazo = Convert.ToInt32(fila.Cells["plazo"].Value);
                    decimal tasa = Convert.ToDecimal(fila.Cells["tasa"].Value);

                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    accionesDB.renovarCertificado(codigo_certificado, deposito_inicial, plazo, tasa);

                    dataGridView1.ReadOnly = true;
                    modo_edicion_activo = false;

                    MessageBox.Show("Cambios guardados exitosamente. El certificado ha sido renovado.", "Renovación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dataGridView1.ReadOnly = true;
                    modo_edicion_activo = false;
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

                    DataGridViewRow fila_seleccionada = dataGridView1.SelectedRows[0];
                    dataGridView1.CurrentCell = fila_seleccionada.Cells["deposito_inicial"];
                    dataGridView1.BeginEdit(true);

                    modo_edicion_activo = true;
                    MessageBox.Show("Modo de edición activado. Modifique los datos y vuelva a presionar el botón para guardar.", "Edición Activada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dataGridView1.ReadOnly = true;
                    MessageBox.Show("Error al renovar el certificado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Cancelars the certificado.
        /// </summary>
        /// <param name="dataGridView1">The data grid view1.</param>
        public void cancelarCertificado(DataGridView dataGridView1)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un certificado para cancelar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila_seleccionada = dataGridView1.SelectedRows[0];
            int codigo_certificado = Convert.ToInt32(fila_seleccionada.Cells["Id_Certificado"].Value);
            string motivo_cancelacion = string.Empty;

           
            using (Cancelar_Certificados frmCancel = new Cancelar_Certificados())
            {
                if (frmCancel.ShowDialog() == DialogResult.OK)
                {

                    motivo_cancelacion = frmCancel.ObtenerMotivo();
                }
                else
                {
                   
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(motivo_cancelacion))
            {
                MessageBox.Show("Debe ingresar un motivo. Cancelación abortada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ClsAccionesDB objAcciones = new ClsAccionesDB();

                objAcciones.cancelarCertificado(codigo_certificado, motivo_cancelacion);

                
                dataGridView1.Rows.Remove(fila_seleccionada);

                MessageBox.Show("Certificado cancelado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar el certificado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cancelars the certificado.
        /// </summary>
        /// <param name="dataGridView1">The data grid view1.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void cancelarCertificado(object dataGridView1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cargars the certificados intereses.
        /// </summary>
        /// <param name="dgv">The DGV.</param>
        /// <exception cref="System.Exception">Error en la carga y cálculo de certificados: " + ex.Message</exception>
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
                    //dtDatosCertificados.Columns.Add("FechaTransaccion", typeof(DateTime));

                    decimal ganancia_sin_redondear = deposito * (tasa / 100) * plazo;
                    decimal total_sin_redondear = deposito + ganancia_sin_redondear;

                    decimal ganancia = Math.Round(ganancia_sin_redondear, 2);
                    decimal total_acumulado = Math.Round(total_sin_redondear, 2);

                    row["Ganancia_Generado"] = ganancia;
                    row["Total_Acumulado"] = total_acumulado;
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
                if (dgv.Columns.Contains("Fecha"))
                {
                    dgv.Columns["Fecha"].Visible = false;
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

        /// <summary>
        /// Cargars the cuentas bancarias.
        /// </summary>
        /// <param name="dgv">The DGV.</param>
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

                decimal ganancia_sin_redondear = saldo * (tasa / 100);
                decimal total_acumulado_sin_redondear = ganancia_sin_redondear + saldo;


                decimal ganancia = Math.Round(ganancia_sin_redondear, 2);
                decimal total = Math.Round(total_acumulado_sin_redondear, 2);

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

