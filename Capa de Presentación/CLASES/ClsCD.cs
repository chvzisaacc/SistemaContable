using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Diego;
using System.Data;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Utilidades para operaciones sobre Certificados (CD) y su sincronización
    /// entre el origen de datos (DataTable) y la vista (DataGridView).
    /// </summary>
    public class ClsCD
    {
        /// <summary>
        /// Constructor. No realiza inicializaciones adicionales.
        /// </summary>
        public ClsCD()
        {
        }

        /// <summary>
        /// Agrega una fila vacía al DataTable y sitúa el foco en la nueva fila del DataGridView.
        /// Sincroniza temporalmente el modo de edición de la vista para permitir la entrada del usuario.
        /// </summary>
        public void Agregarfila(DataTable dtDatosCertificados, DataGridView dataGridView1)
        {
            if (dtDatosCertificados != null)
            {
                // 1. Desbloquear el DataGridView para permitir la entrada
                dataGridView1.ReadOnly = false;

                // 2. Crear la nueva fila en el origen de datos antes de enlazarla a la UI
                DataRow newRow = dtDatosCertificados.NewRow();

                // Inicializar columnas editables para evitar errores de esquema al agregar la fila
                if (dtDatosCertificados.Columns.Contains("Nombre_certificado"))
                {
                    newRow["Nombre_certificado"] = string.Empty;
                }

                if (dtDatosCertificados.Columns.Contains("Nombre_Parroquia"))
                {
                    newRow["Nombre_Parroquia"] = string.Empty;
                }

                // 3. Agregar la fila al DataTable (origen de datos)
                dtDatosCertificados.Rows.Add(newRow);

                // 4. Enfocar la nueva fila en la UI y comenzar edición para sincronizar la entrada
                int lastIndex = dataGridView1.Rows.Count - 1;
                if (lastIndex >= 0)
                {
                    DataGridViewColumn firstVisibleColumn = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible);

                    if (firstVisibleColumn != null)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[lastIndex].Cells[firstVisibleColumn.Index];
                        dataGridView1.BeginEdit(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se puede añadir la fila porque el origen de datos es nulo.",
                                "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Gestiona bloqueos de edición entre el DataTable y el DataGridView.
        /// Desbloquea columnas específicas en la última fila si existen campos vacíos para permitir edición.
        /// </summary>
        public void BloquearDesbloquearData(DataTable dtDatosCertificados, DataGridView dataGridView1, int RowIndex)
        {
            if (RowIndex < 0 || dtDatosCertificados == null) return;

            // Asegurar que el DataTable permita escritura antes de modificar el DGV
            foreach (DataColumn dc in dtDatosCertificados.Columns)
            {
                dc.ReadOnly = false;
            }

            // Por defecto dejamos el DGV en solo lectura y solo habilitamos edición si corresponde
            dataGridView1.ReadOnly = true;

            int lastDataRowIndex = dtDatosCertificados.Rows.Count - 1;

            if (RowIndex == lastDataRowIndex)
            {
                DataGridViewRow currentRow = dataGridView1.Rows[RowIndex];
                bool algunCampoVacio = false;

                string[] columnasAComprobar = new string[] { "Nombre_certificado", "Nombre_Parroquia" };

                foreach (string nombreColumna in columnasAComprobar)
                {
                    if (dataGridView1.Columns.Contains(nombreColumna))
                    {
                        object cellValue = currentRow.Cells[nombreColumna].Value;
                        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                        {
                            algunCampoVacio = true;
                            break;
                        }
                    }
                }

                if (algunCampoVacio)
                {
                    // Habilitar edición solo para las columnas permitidas, manteniendo IDs bloqueadas
                    dataGridView1.ReadOnly = false;

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.Name == "Id_certificado" || column.Name == "Id_Parroquia")
                        {
                            column.ReadOnly = true;
                        }
                        else
                        {
                            column.ReadOnly = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Guarda la última fila válida del DataGridView en la base de datos.
        /// Realiza validaciones sobre campos obligatorios y convierte el nombre de parroquia a su ID.
        /// Tras guardar, actualiza y bloquea la fila para mantener la sincronización con el origen persistente.
        /// </summary>
        public void GuardarCD(DataTable dtDatosCertificados, DataGridView dataGridView1, bool datos_guardados)
        {
            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay filas con datos válidos para guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila = dataGridView1.Rows.Count > 1 && dataGridView1.Rows[dataGridView1.Rows.Count - 1].IsNewRow
                ? dataGridView1.Rows[dataGridView1.Rows.Count - 2]
                : dataGridView1.Rows[dataGridView1.Rows.Count - 1];

            string[] columnas_obligatorias = new string[] { "Nombre_certificado", "Nombre_Parroquia" };

            foreach (string nombre_columna in columnas_obligatorias)
            {
                object cellValue = fila.Cells[nombre_columna]?.Value;
                if (cellValue == null || cellValue == DBNull.Value || string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    MessageBox.Show($"El campo '{nombre_columna}' (Nombre y/o Parroquia) está vacío. Llenalos todos antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DesbloquearFila(fila);
                    return;
                }
            }

            try
            {
                string nombre_certificado = fila.Cells["Nombre_certificado"].Value.ToString();
                string nombre_parroquia_escrito = fila.Cells["Nombre_Parroquia"].Value.ToString();

                DateTime fecha_transaccion_actual = DateTime.Now;

                // Conversión Nombre -> ID y persistencia
                ClsAccionesDB acciones = new ClsAccionesDB();
                int id_parroquia = acciones.ObtenerIdParroquiaPorNombre(nombre_parroquia_escrito);

                if (id_parroquia <= 0)
                {
                    MessageBox.Show($"La Parroquia '{nombre_parroquia_escrito}' no fue encontrada o no existe. Verifique la escritura.", "Error de Parroquia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DesbloquearFila(fila);
                    return;
                }

                int idCertificadoGenerado = acciones.GuardarCertificado(nombre_certificado, id_parroquia, fecha_transaccion_actual);

                // Actualizar UI con valores persistentes y bloquear la fila
                fila.Cells["Id_Certificado"].Value = idCertificadoGenerado;
                fila.Cells["FechaTransaccion"].Value = fecha_transaccion_actual;

                MessageBox.Show("Última fila guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        /// Desbloquea las celdas de una fila para edición salvo las columnas clave (ID y Fecha).
        /// Asegura además que el DataGridView asociado no esté en modo ReadOnly global.
        /// </summary>
        public static void DesbloquearFila(DataGridViewRow fila)
        {
            if (fila == null)
            {
                return;
            }

            if (fila.DataGridView != null && fila.DataGridView.ReadOnly == true)
            {
                fila.DataGridView.ReadOnly = false;
            }

            foreach (DataGridViewCell cell in fila.Cells)
            {
                bool esCampoClave = cell.OwningColumn.Name.Equals("Id_Certificado", StringComparison.OrdinalIgnoreCase) ||
                                    cell.OwningColumn.Name.Equals("FechaTransaccion", StringComparison.OrdinalIgnoreCase);

                if (esCampoClave)
                {
                    //Mantener campos clave como solo lectura para evitar modificaciones que rompan la integridad de los datos
                    cell.ReadOnly = true;
                }
                else
                {
                    // Permitir edición en campos no clave
                    cell.ReadOnly = false;
                    cell.ReadOnly = esCampoClave;
                }
               
                
            }
        }

        /// <summary>
        /// Activa la edición de la fila seleccionada y coloca el foco en la primera columna visible.
        /// Mantiene sincronía entre la UI (modo edición) y el origen de datos mientras el usuario modifica valores.
        /// </summary>
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
        /// Guarda cambios de una fila en modo edición. Realiza validaciones, conversión de parroquia y llama
        /// al método de actualización en la capa de acceso a datos.
        /// </summary>
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

                    DataGridViewCell pkCell = fila.Cells["Id_Certificado"];
                    if (pkCell == null || pkCell.Value == DBNull.Value || pkCell.Value == null ||
                        !int.TryParse(pkCell.Value.ToString(), out codigo_certificado))
                    {
                        MessageBox.Show("No se puede obtener un ID de certificado válido para editar.", "Error de ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string nombre_certificado = fila.Cells["Nombre_certificado"].Value?.ToString() ?? string.Empty;
                    string nombre_parroquia_escrito = fila.Cells["Nombre_Parroquia"].Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(nombre_certificado) || string.IsNullOrWhiteSpace(nombre_parroquia_escrito))
                    {
                        MessageBox.Show("El Nombre del Certificado y la Parroquia no pueden estar vacíos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    int id_parroquia = accionesDB.ObtenerIdParroquiaPorNombre(nombre_parroquia_escrito);

                    if (id_parroquia <= 0)
                    {
                        MessageBox.Show($"La Parroquia '{nombre_parroquia_escrito}' no fue encontrada. Verifique la escritura.", "Error de Parroquia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    accionesDB.editarcertificado(codigo_certificado, nombre_certificado, id_parroquia);

                    dataGridView1.ReadOnly = true;
                    modo_edicion_activo = false;

                    MessageBox.Show("Cambios guardados exitosamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la edición: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                editarCD(dtDatosCertificados, dataGridView1);
                modo_edicion_activo = true;
            }
        }

        /// <summary>
        /// Maneja el flujo de renovación: confirma la acción y llama al método que marca el certificado como renovado.
        /// Mantiene la UI sincronizada bloqueando la fila y desactivando el modo de edición al completar.
        /// </summary>
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

                    DataGridViewRow fila = dataGridView1.SelectedRows[0];
                    int codigo_certificado = Convert.ToInt32(fila.Cells["Id_certificado"].Value);

                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    accionesDB.renovarCertificado(codigo_certificado);

                    foreach (DataGridViewCell cell in fila.Cells)
                    {
                        cell.ReadOnly = true;
                    }

                    modo_edicion_activo = false;

                    MessageBox.Show("¡El certificado ha sido marcado como renovado y los cambios guardados!", "Renovación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Presione el botón nuevamente para confirmar y registrar la renovación del certificado.",
                                "Confirmar Renovación", MessageBoxButtons.OK, MessageBoxIcon.Question);

                modo_edicion_activo = true;
            }
        }

        /// <summary>
        /// Cancela el certificado seleccionado solicitando motivo y eliminando la fila de la vista tras persistir el cambio.
        /// </summary>
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
        /// Sobrecarga no implementada; mantiene compatibilidad de firma si es requerida en otro lugar.
        /// </summary>
        public void cancelarCertificado(object dataGridView1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Carga certificados y calcula intereses/total acumulado para presentación en la vista.
        /// Formatea columnas y oculta campos internos para mantener la sincronía presentación-datos.
        /// </summary>
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
        /// Carga cuentas bancarias, calcula ganancia y total acumulado y prepara la vista para presentación.
        /// </summary>
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

