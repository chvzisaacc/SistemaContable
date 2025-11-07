using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Diego;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_42 : Form
    {

        private DataTable dtDatosIngresos = null;
        private DataTable dtDatosGastos = null;
        private clsCRUD_CatalogoCuentas crudCataloCuentas;

        private ClsCRUD_CuentasBancarias crudCuentasBancarias;

        private bool modoEdicion = false;
        private int cuentaBancoIDseleccionado = 0;
        private AutoCompleteStringCollection Subcuentas = new AutoCompleteStringCollection();
        private ClsAccionesDB objSubCuentas = new ClsAccionesDB();

        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42()
        {
            InitializeComponent();

            InicializarDGVIngr();
            CargarDatosAutocompletado();
            CargarDatosAutocompletadoGastos();
            Transacciones objtransa = new();
            objtransa.CargarComboBoxOrigen(cmbOrigen);
            objtransa.CargarComboBoxOrigen(cmbOrigen2);
            crudCataloCuentas = new clsCRUD_CatalogoCuentas();

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();

            this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            //panelContenedor.Controls.Add(panelMensaje);

            // Opcional: muestra uno por defecto
            MostrarSoloEstePanel(panel1);
        }

        private void FRM_42_Load(object sender, EventArgs e)
        {
            dgvGastos.AllowUserToAddRows = false;
            CargarOrigenes();
            DateTime mesactual = DateTime.Now;
            DateTime mesactual1 = new DateTime(mesactual.Year, mesactual.Month, 1);
            dtpFecha.MinDate = mesactual1;
            dtpFecha.MaxDate = mesactual;
            // MostrarSaldoActual();

            ActualizarSaldo();


        }
        //=======
        private void FRM_42_LOAD(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboBoxes();
            // LimpiarCampos();
            //HabilitarControles(false);
        }

        private void CargarOrigenes()
        {
            try
            {
                ClsAccionesDB db = new ClsAccionesDB(); 
                List<Origen> lista = db.ObtenerListaOrigenes(); 

                cmbOrigen.DataSource = lista;
                cmbOrigen.DisplayMember = "Nombre"; 
                cmbOrigen.ValueMember = "ID";       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los orígenes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();

                //aqui es para ocultar algunos campos (los ids y las contraseñas)
                /*
                if (dgvUsuarios.Columns["Contraseña"] != null)
                    dgvUsuarios.Columns["Contraseña"].Visible = false;

                if (dgvUsuarios.Columns["RolID"] != null)
                    dgvUsuarios.Columns["RolID"].Visible = false;
                if (dgvUsuarios.Columns["ParroquiaID"] != null)
                    dgvUsuarios.Columns["ParroquiaID"].Visible = false;
                if (dgvUsuarios.Columns["EstadoID"] != null)
                    dgvUsuarios.Columns["EstadoID"].Visible = false;
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarComboBoxes()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_cuentaBanco";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void MostrarSoloEstePanel(Panel panelAMostrar)
        {
            foreach (Control ctrl in panelContenedor.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }
            panelAMostrar.Visible = true;
            panelAMostrar.BringToFront();
        }



        private void btnGastos_Click(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelGastos2);
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCajaChica2);
        }
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelIngresos);
        }

        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelBancos2);
        }







        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS();
            var buttonScreenPosition = pictureBox1.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox1.Height);
            popup.ShowDialog();
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FRM_CERRARSESION popup = new FRM_CERRARSESION();
            var buttonScreenPosition = pictureBox2.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox2.Height);
            popup.ShowDialog();
        }



        private void btnDetalle_Click(object sender, EventArgs e)
        {
            using (var frm = new FRM_PG69())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void cmbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cmbCuentas.SelectedIndex < 0) return;

            switch (cmbCuentas.SelectedIndex)
            {
                case 0: // Cuenta ahorro
                    {
                        var frm = new FRM_PG42BancosCuentaAhorro
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };
                        frm.ShowDialog();
                        break;
                    }
                case 1: // Cuenta cheques
                    {
                        var frm = new FRM_PG42BancosCuentaCheque
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
            }
        }

        private void cmbAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcciones.SelectedIndex < 0) return;

            switch (cmbAcciones.SelectedIndex)
            {
                case 0: // Agregar Saldo
                    {
                        var frm = new FRM_BancosAgregarSaldo
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };
                        frm.ShowDialog();
                        break;
                    }
                case 1: // Transferencia entre cuentas
                    {
                        var frm = new FRM_BancosTransferenciaEntreCuentas
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
                case 2: // Agregar cuenta bancaria
                    {
                        var frm = new FRM_BancosAgregarCuentaBancaria
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
                case 3: // Retirar dinero
                    {
                        var frm = new FRM_BancosRetirarDinero
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDetalle_Click_1(object sender, EventArgs e)
        {
            //llamar form 69,
            FRM_PG69 obj_frm69 = new FRM_PG69();
            obj_frm69.ShowDialog();
        }

        private void chkSaldoInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (txtSaldoActual.Text == "0.00")
            {
                chkSaldoInicial.Visible = true;
            }
            FRM_CajaChicaMonto obcaja = new FRM_CajaChicaMonto();
            obcaja.ShowDialog();

            chkSaldoInicial.Visible = false;

            if (txtSaldoActual.Text == "0.00")
            {
                chkSaldoInicial.Visible = true;
            }
        }
        private void MostrarSaldoActual()
        {
            Clsconexion objCon = new Clsconexion();

            try
            {
                objCon.Abrir();

                string query = "exec saldo_actual";
                SqlCommand comando = new SqlCommand(query, objCon.sc);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    //convertir el valor string a decimal
                    decimal saldo = Convert.ToDecimal(lector["saldo"]);
                    //n2 formatea a dos digitos despues del "."
                    txtSaldoActual.Text = saldo.ToString("N2");
                }
                else
                {
                    txtSaldoActual.Text = "0.00";
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                objCon.Cerrar();
            }
        }
        //Actualiza el saldo que se muestra en pantalla
        private void ActualizarSaldo()
        {
            try
            {
                MostrarSaldoActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el saldo: " + ex.Message);
            }
        }

        private void txtSaldoActual_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbOrigen.Text = "Seleccionar";
            txtNoReferencia.Text = null;
            Transacciones transa = new();
            transa.Agregarfila(dtDatosIngresos, dataGridView1);


        }

        private void dgvIngresos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void InicializarDGVIngr()
        {
            dataGridView1.Columns.Clear();
            dtDatosIngresos = new DataTable("Ingresos");
            dtDatosIngresos.Columns.Add("NombreCuenta", typeof(string));
            dtDatosIngresos.Columns.Add("Detalle", typeof(string));
            dtDatosIngresos.Columns.Add("Saldo", typeof(string));
            dataGridView1.DataSource = dtDatosIngresos;
            dataGridView1.AutoGenerateColumns = true;
        }

        private void InicializarDGVgastos()
        {
            dgvGastos.Columns.Clear();
            dtDatosIngresos = new DataTable("Ingresos");
            dtDatosIngresos.Columns.Add("NombreCuenta", typeof(string));
            dtDatosIngresos.Columns.Add("Detalle", typeof(string));
            dtDatosIngresos.Columns.Add("Saldo", typeof(string));
            dgvGastos.DataSource = dtDatosIngresos;
            dgvGastos.AutoGenerateColumns = true;
            dgvGastos.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Transacciones objtransa = new();
            objtransa.BloquearDesbloquearDataIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
        }

        private void panelIngresos_Paint(object sender, PaintEventArgs e)
        {

        }


        private void CargarDatosAutocompletado()
        {
            Subcuentas.Clear();

            try
            {
                DataTable dt = objSubCuentas.ObtenerCuentasIngreso();

                foreach (DataRow row in dt.Rows)
                {
                    Subcuentas.Add(row["Subcuentas"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void CargarDatosAutocompletadoGastos()
        {
            Subcuentas.Clear();

            try
            {
                DataTable dt = objSubCuentas.ObtenerCuentasGastos();

                foreach (DataRow row in dt.Rows)
                {
                    Subcuentas.Add(row["Subcuentas"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "NombreCuenta")
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    ClsAccionesDB clsAccionesDB = new();
                    DataTable dtCuentas = clsAccionesDB.ObtenerCuentasIngreso();
                    AutoCompleteStringCollection nombresCuentas = new AutoCompleteStringCollection();

                    foreach (DataRow row in dtCuentas.Rows)
                    {
                        nombresCuentas.Add(row["Subcuentas"].ToString());
                    }

                    autoText.AutoCompleteCustomSource = nombresCuentas;
                }
            }
            else
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.None;
                    autoText.AutoCompleteSource = AutoCompleteSource.None;
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            FRM_PG103 fRM_PG103 = new FRM_PG103();
            fRM_PG103.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idOrigen = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);

            if (idOrigen == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ingresos ingresos = new();
            int filasGuardadas = 0;
            bool errorGuardado = false;
            try
            {
                DateTime fechaTransaccion = dtpFecha.Value;
                string referencia = txtNoReferencia.Text.Trim();
                int idUsuario = Sesion1.UsuarioID;
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (fila.IsNewRow) continue;
                    string nombreCuenta = fila.Cells["NombreCuenta"].Value?.ToString() ?? string.Empty;
                    string descripcion = fila.Cells["Detalle"].Value?.ToString() ?? string.Empty;


                    if (!decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out decimal monto) || monto <= 0)
                    {
                        MessageBox.Show($"Monto inválido", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    int nuevoID = ingresos.IngresarIngresos(fechaTransaccion, descripcion, monto, referencia, idUsuario, idOrigen, nombreCuenta);

                    filasGuardadas++;
                }

            }
            catch (Exception ex)
            {
                errorGuardado = true;
                string mensajeError = "Error al guardar la transacción" + ex.Message;
            }

            if (!errorGuardado && filasGuardadas > 0)
            {
                if (filasGuardadas > 0)
                {
                    MessageBox.Show(
                        $"Se guardaron {filasGuardadas} fila(s) correctamente.",
                        "Transacción guardada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "No se guardó ninguna fila válida.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

            }
        }

        private void cmbOrigen2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia.Text = null;
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //Adicionar una fila al datagridview de gastos
            int lastIndex = dgvGastos.Rows.Add();
            dgvGastos.CurrentCell = dgvGastos.Rows[lastIndex].Cells[0];
        }

        private void dgvGastos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                if (dgvGastos.CurrentCell.ColumnIndex == 0)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    // Cargar sugerencias solo una vez
                    if (txt.AutoCompleteCustomSource == null || txt.AutoCompleteCustomSource.Count == 0)
                    {
                        AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                        ClsAccionesDB objac = new ClsAccionesDB();
                        DataTable dtNombres = objac.ObtenerCuentasGastos();
                        foreach (DataRow row in dtNombres.Rows)
                        {
                            coleccion.Add(row["Subcuentas"].ToString());
                        }
                        txt.AutoCompleteCustomSource = coleccion;
                    }
                }
                else
                {
                    txt.AutoCompleteMode = AutoCompleteMode.None;
                    txt.AutoCompleteSource = AutoCompleteSource.None;
                    txt.AutoCompleteCustomSource = null;
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //ENTRA EN MODO EDICION
            dataGridView1.EndEdit();
            this.Validate();
            this.BindingContext[dataGridView1.DataSource]?.EndCurrentEdit();

            int idOrigen = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);
            if (idOrigen == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // BUSCA LA ULTIMA FILA
            DataGridViewRow fila = null;
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                var row = dataGridView1.Rows[i];
                if (!row.IsNewRow)
                {
                    bool tieneDatos = false;
                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            tieneDatos = true;
                            break;
                        }
                    }

                    if (tieneDatos)
                    {
                        fila = row;
                        break;
                    }
                }
            }

            if (fila == null)
            {
                MessageBox.Show("No hay ninguna fila válida para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Ingresos ingresos = new();
            bool errorGuardado = false;
            int filasGuardadas = 0;

            try
            {
                DateTime fechaTransaccion = dtpFecha.Value;
                string referencia = txtNoReferencia.Text.Trim();
                int idUsuario = Sesion1.UsuarioID;

                string nombreCuenta = fila.Cells["nombrecuenta"].Value?.ToString() ?? string.Empty;
                string descripcion = fila.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show($"Monto inválido para la cuenta: {nombreCuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    int nuevoID = ingresos.IngresarIngresos(fechaTransaccion, descripcion, monto, referencia, idUsuario, idOrigen, nombreCuenta);
                    filasGuardadas++;

                    fila.Cells["Saldo"].Style.ForeColor = Color.Green;
                }
                catch (Exception exGuardado)
                {
                    errorGuardado = true;
                    MessageBox.Show($"Error al guardar la fila para la cuenta {nombreCuenta}: {exGuardado.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                errorGuardado = true;
                MessageBox.Show("Error al guardar la transacciom: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (!errorGuardado)
            {
                if (filasGuardadas > 0)
                {
                    MessageBox.Show("Se guardo correctamente el ingreso.", "Transaccion guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se guardo la transaccion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {

            dgvGastos.EndEdit();
            this.Validate();

            if (dgvGastos.DataSource != null)
            {
                this.BindingContext[dgvGastos.DataSource]?.EndCurrentEdit();
            }

           int idOrigen = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);
            
            if (idOrigen == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // BUSCAR ÚLTIMA FILA VÁLIDA
            DataGridViewRow fila = null;
            for (int i = dgvGastos.Rows.Count - 1; i >= 0; i--)
            {
                var row = dgvGastos.Rows[i];
                if (!row.IsNewRow)
                {
                    bool tieneDatos = false;
                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            tieneDatos = true;
                            break;
                        }
                    }

                    if (tieneDatos)
                    {
                        fila = row;
                        break;
                    }
                }
            }

            if (fila == null)
            {
                MessageBox.Show("No hay ninguna fila valida para guardar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // LLAMAR CLASE DE GASTOS
            Gastos gastos = new();
            bool errorGuardado = false;
            int filasGuardadas = 0;

            try
            {
                DateTime fechaTransaccion = dtpFecha.Value;
                string referencia = txtNoReferencia.Text.Trim();
                int idUsuario = Sesion1.UsuarioID;

                string nombreCuenta = fila.Cells["dataGridViewTextBoxColumn1"].Value?.ToString() ?? string.Empty;
                string descripcion = fila.Cells["dataGridViewTextBoxColumn2"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila.Cells["dataGridViewTextBoxColumn3"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show($"Monto invalido para la cuenta: {nombreCuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // LLAMAR PROCEDIMIENTO ALMACENADO IngresarGastos
                    int nuevoID = gastos.IngresarGastos(fechaTransaccion, descripcion, monto, referencia, idUsuario, idOrigen, nombreCuenta);
                    filasGuardadas++;

                    fila.Cells["dataGridViewTextBoxColumn3"].Style.ForeColor = Color.Red;
                }
                catch (Exception exGuardado)
                {
                    errorGuardado = true;
                    MessageBox.Show($"Error al guardar la fila para la cuenta {nombreCuenta}: {exGuardado.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                errorGuardado = true;
                MessageBox.Show("Error al guardar la transaccion: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!errorGuardado)
            {
                if (filasGuardadas > 0)
                {
                    MessageBox.Show("Se guardó correctamente el gasto.", "Transacción guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se guardó la transacción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
 




      

      


