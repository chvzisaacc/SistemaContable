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
            // Bloqueo Inicial (se asume que el DGV debe ser de solo lectura por defecto)
            if (RowIndex >= 0)
            {
                // Esto pone todo el DataGridView en modo ReadOnly si se hace clic en una fila de datos.
                // Esto es una configuración de alto nivel y puede anular la configuración de columnas individuales.
                dataGridView1.ReadOnly = true;
            }

            // Lógica para Desbloquear la ÚLTIMA fila de datos (antes de la fila de nueva entrada) si está vacía.
            if (RowIndex >= 0 && dtDatosCertificados != null)
            {
                // Se asume que el DataTable refleja el DataGridView (excluyendo la fila de nueva entrada si ShowNewRow es true)
                int lastDataRowIndex = dtDatosCertificados.Rows.Count - 1;

                // Solo se aplica la lógica a la última fila de datos ingresada (que aún no ha sido guardada)
                if (RowIndex == lastDataRowIndex)
                {
                    // La validación debe hacerse sobre el DGV, que incluye la fila que se acaba de hacer clic.
                    DataGridViewRow currentRow = dataGridView1.Rows[RowIndex];
                    bool algunCampoVacio = false;

                    // Lista de columnas obligatorias simplificada ---
                    string[] columnasAComprobar = new string[]
                    {
                        "Nombre_certificado",
                        "Nombre_Parroquia"

                    };

                    //Comprobar si los campos obligatorios están vacíos
                    foreach (string nombreColumna in columnasAComprobar)
                    {
                        // Es importante comprobar si la columna existe antes de acceder a la celda
                        if (dataGridView1.Columns.Contains(nombreColumna))
                        {
                            object cellValue = currentRow.Cells[nombreColumna].Value;

                            if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                            {
                                algunCampoVacio = true;
                                break;
                            }
                        }
                    }

                    // Si hay campos vacíos, desbloquear la edición
                    if (algunCampoVacio)
                    {
                        // Desbloquear el DGV
                        dataGridView1.ReadOnly = false;

                        // Desbloquear todas las columnas para que el usuario pueda ingresar datos
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            // Se pueden añadir exclusiones aquí si hay campos de solo lectura (ej. Id_Certificado)
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
            // NO se usa la ID de Parroquia de la sesión, se capturará del DataGridView.
            // int id_parroquia = Capa_de_acceso_de_datos.Sesion1.id_parroquia; 

            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay filas con datos válidos para guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Identifica la última fila de datos (justo antes de la fila nueva, si existe)
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
                string nombre_parroquia_escrito = fila.Cells["Nombre_Parroquia"].Value.ToString(); // Capturamos el nombre

                DateTime fecha_transaccion_actual = DateTime.Now;

                // 3. OBTENER EL ID DE LA PARROQUIA (Conversión de Nombre a ID)
                ClsAccionesDB acciones = new ClsAccionesDB();

                // Llamar a la función que busca el ID en la base de datos
                int id_parroquia = acciones.ObtenerIdParroquiaPorNombre(nombre_parroquia_escrito);

                if (id_parroquia <= 0)
                {
                    MessageBox.Show($"La Parroquia '{nombre_parroquia_escrito}' no fue encontrada o no existe. Verifique la escritura.", "Error de Parroquia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DesbloquearFila(fila);
                    return;
                }

                // 4. GUARDAR EL CERTIFICADO 
                // Se pasan: nombre_certificado, id_parroquia, y la fecha
                int idCertificadoGenerado = acciones.GuardarCertificado(nombre_certificado, id_parroquia, fecha_transaccion_actual);

                // 5. Actualizar la Fila
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

        public static void DesbloquearFila(DataGridViewRow fila)
        {
            // Verificación de nulidad
            if (fila == null)
            {
                return;
            }

            // 1. Asegurar que el DataGridView asociado NO esté bloqueado globalmente
            if (fila.DataGridView != null && fila.DataGridView.ReadOnly == true)
            {
                fila.DataGridView.ReadOnly = false;
            }

            // 2. Iterar y modificar el ReadOnly de CADA celda
            foreach (DataGridViewCell cell in fila.Cells)
            {
                // Usamos StringComparison.OrdinalIgnoreCase para una coincidencia segura de nombres.
                bool esCampoClave = cell.OwningColumn.Name.Equals("Id_Certificado", StringComparison.OrdinalIgnoreCase) ||
                                    cell.OwningColumn.Name.Equals("FechaTransaccion", StringComparison.OrdinalIgnoreCase);

                if (esCampoClave)
                {
                    // Mantiene los campos clave SIEMPRE como de solo lectura (Bloqueo)
                    cell.ReadOnly = true;
                }
                else
                {
                    // Desbloquea todos los demás campos (como Nombre_certificado y Nombre_Parroquia)
                    cell.ReadOnly = false;
                }
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
            // Verificar si hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modo_edicion_activo)
            {
                try
                {
                    // Forzar la validación de la celda actual para asegurar que el valor se capture
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    DataGridViewRow fila = dataGridView1.SelectedRows[0];
                    int codigo_certificado = 0;

                    // --- 1. Obtener y validar el ID del Certificado ---
                    DataGridViewCell pkCell = fila.Cells["Id_Certificado"];
                    if (pkCell == null || pkCell.Value == DBNull.Value || pkCell.Value == null ||
                        !int.TryParse(pkCell.Value.ToString(), out codigo_certificado))
                    {
                        MessageBox.Show("No se puede obtener un ID de certificado válido para editar.", "Error de ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // --- 2. Capturar los valores necesarios ---
                    string nombre_certificado = fila.Cells["Nombre_certificado"].Value?.ToString() ?? string.Empty;
                    string nombre_parroquia_escrito = fila.Cells["Nombre_Parroquia"].Value?.ToString() ?? string.Empty;

                    // Validación de campos obligatorios (opcionalmente)
                    if (string.IsNullOrWhiteSpace(nombre_certificado) || string.IsNullOrWhiteSpace(nombre_parroquia_escrito))
                    {
                        MessageBox.Show("El Nombre del Certificado y la Parroquia no pueden estar vacíos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // --- 3. Convertir Nombre de Parroquia a ID ---
                    ClsAccionesDB accionesDB = new ClsAccionesDB();
                    int id_parroquia = accionesDB.ObtenerIdParroquiaPorNombre(nombre_parroquia_escrito);

                    if (id_parroquia <= 0)
                    {
                        MessageBox.Show($"La Parroquia '{nombre_parroquia_escrito}' no fue encontrada. Verifique la escritura.", "Error de Parroquia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // --- 4. Llamada al método de edición simplificado ---
                    // La nueva firma es: (int codigo_certificado, string nombre_certificado, int id_parroquia)
                    accionesDB.editarcertificado(codigo_certificado, nombre_certificado, id_parroquia);

                    // --- 5. Finalizar edición y actualizar UI ---
                    dataGridView1.ReadOnly = true; // Hace el DGV de sólo lectura (o debes recorrer las celdas de la fila)
                    modo_edicion_activo = false; // Desactiva el modo de edición

                    // Opcional: Recargar la fila o la tabla para mostrar el Nombre_Parroquia actualizado si aplica

                    MessageBox.Show("Cambios guardados exitosamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la edición: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no está en modo de edición, llama al método para activar la edición
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

            // --- Lógica del Botón: GUARDAR RENOVACIÓN (Si modo_edicion_activo es TRUE) ---
            if (modo_edicion_activo)
            {
                try
                {
                    // Forzamos la captura del valor de la celda en edición
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    dataGridView1.EndEdit();

                    DataGridViewRow fila = dataGridView1.SelectedRows[0];

                    // 1. Obtener solo el ID del certificado
                    int codigo_certificado = Convert.ToInt32(fila.Cells["Id_certificado"].Value);


                    ClsAccionesDB accionesDB = new ClsAccionesDB();

                    // 2. Llamada a la función renovarCertificado simplificada (solo ID)
                    // Esta función llama al SP que actualiza el estado y la fecha.
                    accionesDB.renovarCertificado(codigo_certificado);

                    // 3. Finalizar edición y actualizar UI
                    // Recorre las celdas de la fila para hacerlas de solo lectura
                    foreach (DataGridViewCell cell in fila.Cells)
                    {
                        cell.ReadOnly = true;
                    }

                    modo_edicion_activo = false;

       

                    MessageBox.Show("¡El certificado ha sido marcado como renovado y los cambios guardados!", "Renovación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Aseguramos que el modo edición se desactive en caso de fallo
                    dataGridView1.ReadOnly = true;
                    modo_edicion_activo = false;
                    MessageBox.Show("Error al guardar la renovación: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // En el esquema simplificado, la renovación NO requiere edición de datos en el DGV.
                // La renovación es una ACCIÓN de cambiar el estado, no una edición de campos.

                MessageBox.Show("Presione el botón nuevamente para confirmar y registrar la renovación del certificado.",
                                "Confirmar Renovación", MessageBoxButtons.OK, MessageBoxIcon.Question);

                modo_edicion_activo = true;
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

