using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Diego;
using Capa_de_procesamiento_de_datos;
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
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_42 : Form
    {

        private DataTable dtDatosIngresos = new DataTable("Ingresos");
        private DataTable dtDatosGastos = new DataTable("Gastos");
        private clsCRUD_CatalogoCuentas crudCataloCuentas;
        

        private ClsCRUD_CuentasBancarias crudCuentasBancarias;


        private bool modoEdicion = false;
        private int cuentaBancoIDseleccionado = 0;
        private AutoCompleteStringCollection Subcuentas = new AutoCompleteStringCollection();
        private ClsAccionesDB objSubCuentas = new ClsAccionesDB();


        private clsCRUD_Historial crudHistorial = new clsCRUD_Historial();
        private int Id_UsuarioLogin;

        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42(int Id_Usuario)
        {
            InitializeComponent();


            InicializarDGVIngr();
            InicializarDGVgastos();
            CargarDatosAutocompletado();
            CargarDatosAutocompletadoGastos();
            Transacciones objtransa = new();
            objtransa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


            crudCataloCuentas = new clsCRUD_CatalogoCuentas();

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();

            crudHistorial = new clsCRUD_Historial();

            Id_UsuarioLogin = Id_Usuario; 

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
            DateTime mesactual = DateTime.Now;
            DateTime mesactual1 = new DateTime(mesactual.Year, mesactual.Month, 1);
            dtpFecha.MinDate = mesactual1;
            dtpFecha.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dateTimePicker1.MinDate = mesactual1;
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);

            if (dtpFecha.Value < dtpFecha.MinDate || dtpFecha.Value > dtpFecha.MaxDate)
                dtpFecha.Value = DateTime.Today;

            if (dateTimePicker1.Value < dateTimePicker1.MinDate || dateTimePicker1.Value > dateTimePicker1.MaxDate)
                dateTimePicker1.Value = DateTime.Today;


            // MostrarSaldoActual();

            ActualizarSaldo();
            CargarCuentasEnComboBox();

            // CargarDatos();
            // CargarComboBoxes();


        }
        //=======


       

        private void CargarCuentasEnComboBox()
        {
            try
            {
                DataTable dtCuentas = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DataSource = dtCuentas;
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_Origen";

                // Esto previene que el evento se dispare al cargar,
                // porque ningún ítem está seleccionado inicialmente.
                cmbCuentas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message);
            }
        }

        private void CargarOrigenes()
        {
            try
            {
                ClsAccionesDB db = new ClsAccionesDB();
                List<Origen> lista = db.ObtenerListaOrigenes();
                List<Origen> lista2 = db.ObtenerListaOrigenes();

                cmbOrigen.DataSource = lista;
                cmbOrigen.DisplayMember = "Nombre";
                cmbOrigen.ValueMember = "ID";

                cmbOrigen2.DataSource = lista2;
                cmbOrigen2.DisplayMember = "Nombre";
                cmbOrigen2.ValueMember = "ID";
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
            try
            {
                crudHistorial.RegistrarActividad(
                    Sesion1.UsuarioID,
                    3, // ID del Módulo de Gastos
                    "Navegación",
                    "El usuario entró al módulo de Gastos."
                );
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }

            try
            {
                crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Navegación", "Entrada a Gastos", null, "El usuario entró al módulo de Gastos.");
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }


            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelGastos2);
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            try
            {
                crudHistorial.RegistrarActividad(
                    Sesion1.UsuarioID,
                    4, // ID del Módulo de Caja Chica
                    "Navegación",
                    "El usuario entró al módulo de Caja Chica."
                );
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }

            try
            {
                crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Navegación", "Entrada a Caja Chica", null, "El usuario entró al módulo de Caja Chica.");
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }

            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCajaChica2);
        }
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            try
            {
                crudHistorial.RegistrarActividad(
                    Sesion1.UsuarioID, 
                    2, // ID del Módulo de Ingresos
                    "Navegación",
                    "El usuario entró al módulo de Ingresos."
                );
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }
           

            try
            {
                crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Navegación", "Entrada a Ingresos", null, "El usuario entró al módulo de Ingresos.");
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }
            
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelIngresos);
        }

        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            // --- LÍNEAS A AGREGAR/DESCOMENTAR ---
            try
            {
                crudHistorial.RegistrarActividad(
                    Sesion1.UsuarioID,
                    5, // ID del Módulo de Bancos
                    "Navegación",
                    "El usuario entró al módulo de Bancos."
                );
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }

            try
            {
                crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Navegación", "Entrada a Bancos", null, "El usuario entró al módulo de Bancos.");
            }
            catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }

            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelBancos2);
        }







        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS(Id_UsuarioLogin);
            popup.StartPosition = FormStartPosition.Manual;
            var buttonScreenPosition = pictureBox1.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox1.Height);
            popup.ShowDialog(this);
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
            if (cmbCuentas.SelectedValue == null)
            {
                return;
            }

            try
            {
                int idSeleccionado = Convert.ToInt32(cmbCuentas.SelectedValue);
                var frm = new FRM_PG42BancosCuentaAhorro(idSeleccionado);

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar abrir el formulario: \n" + ex.Message, "Error");
            }
        }







        private void cmbAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcciones.SelectedIndex < 0) return;
            Transacciones transaccionesObj = new();

            switch (cmbAcciones.SelectedIndex)
            {
                case 0: // Agregar Saldo
                    {
                        using (var frm = new FRM_BancosAgregarSaldo
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                try
                                {
                                    crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Bancos", "Agregar Saldo", null, "Se registró una adición de saldo a una cuenta.");
                                }
                                catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }
                                // --- FIN ---
                                transaccionesObj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                            }
                        }
                    }
                    break;

                case 1: // Transferencia entre cuentas
                    {
                        using (var frm = new FRM_BancosTransferenciaEntreCuentas
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                                try
                                {
                                    crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Bancos", "Transferencia", null, "Se realizó una transferencia entre cuentas bancarias.");
                                }
                                catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }
                            // --- FIN ---
                            transaccionesObj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                        }
                    }
                    break;
                case 2: // Agregar cuenta bancaria
                    {
                        using (var frm = new FRM_BancosAgregarCuentaBancaria
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                                try
                                {
                                    crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Bancos", "Nueva Cuenta", null, "Se agregó una nueva cuenta bancaria.");
                                }
                                catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }
                            // --- FIN ---
                            transaccionesObj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                            CargarCuentasEnComboBox();
                        }
                    }
                    break;
                case 3: // Retirar dinero
                    {
                        using (var frm = new FRM_BancosRetirarDinero
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                                try
                                {
                                    crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Bancos", "Retiro de Dinero", null, "Se realizó un retiro de dinero de una cuenta.");
                                }
                                catch (Exception ex) { Console.WriteLine("Error de Bitácora: " + ex.Message); }
                            // --- FIN ---
                            transaccionesObj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                        }

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
            FRM_CajaChicaMonto obcaja = new FRM_CajaChicaMonto();
            if (chkSaldoInicial.Checked)
            {

                obcaja.SaldoActualizado += this.ActualizarSaldo;

                obcaja.ShowDialog();

                obcaja.SaldoActualizado -= this.ActualizarSaldo;
            }
            ActualizarSaldo();
            Transacciones transacciones = new();
            transacciones.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);

            if (txtSaldoActual.Text == "0.00")
            {
                chkSaldoInicial.Visible = true;
                obcaja.ShowDialog();
            }
            else
            {
                chkSaldoInicial.Visible = false;
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
            dtDatosIngresos.Columns.Add("Id_transaccion", typeof(int));
            dtDatosIngresos.Columns.Add("Id_Origen", typeof(int));
            dtDatosIngresos.Columns.Add("NombreCuenta", typeof(string));
            dtDatosIngresos.Columns.Add("Detalle", typeof(string));
            dtDatosIngresos.Columns.Add("Saldo", typeof(string));
            dtDatosIngresos.Columns.Add("FechaTransaccion", typeof(DateTime));
            dtDatosIngresos.Columns.Add("NoReferencia", typeof(int));
            dataGridView1.DataSource = dtDatosIngresos;

            dataGridView1.AutoGenerateColumns = false;

            if (dataGridView1.Columns.Contains("Id_transaccion"))
                dataGridView1.Columns["Id_transaccion"].Visible = false;
            if (dataGridView1.Columns.Contains("Id_Origen"))
                dataGridView1.Columns["Id_Origen"].Visible = false;
            if (dataGridView1.Columns.Contains("FechaTransaccion"))
                dataGridView1.Columns["FechaTransaccion"].Visible = false;
            if (dataGridView1.Columns.Contains("NoReferencia"))
                dataGridView1.Columns["NoReferencia"].Visible = false;
        }

        private void InicializarDGVgastos()
        {
            dgvGastos.Columns.Clear();
            dtDatosGastos = new DataTable("Gastos");
            dtDatosGastos.Columns.Add("Id_transaccion", typeof(int));
            dtDatosGastos.Columns.Add("Id_Origen", typeof(int));
            dtDatosGastos.Columns.Add("NombreCuenta", typeof(string));
            dtDatosGastos.Columns.Add("Detalle", typeof(string));
            dtDatosGastos.Columns.Add("Saldo", typeof(string));
            dtDatosGastos.Columns.Add("FechaTransaccion", typeof(DateTime));
            dtDatosGastos.Columns.Add("NoReferencia", typeof(int));
            dgvGastos.DataSource = dtDatosGastos;
            dgvGastos.AutoGenerateColumns = true;


            if (dgvGastos.Columns.Contains("Id_transaccion"))
                dgvGastos.Columns["Id_transaccion"].Visible = false;
            if (dgvGastos.Columns.Contains("Id_Origen"))
                dgvGastos.Columns["Id_Origen"].Visible = false;
            if (dgvGastos.Columns.Contains("FechaTransaccion"))
                dgvGastos.Columns["FechaTransaccion"].Visible = false;
            if (dgvGastos.Columns.Contains("NoReferencia"))
                dgvGastos.Columns["NoReferencia"].Visible = false;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows.Count == 0)
                return;

            // Si estás en modo edición, no bloquea nada
            if (modoEdicion)
                return;

            // Si NO estás en modo edición, bloquea todo 
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.ReadOnly = true;

            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Enabled = true;

            // Solo aplica desbloqueo si la fila clickeada es la última (nueva sin guardar)
            int lastIndex = dataGridView1.Rows.Count - 1;
            if (e.RowIndex == lastIndex)
            {
                Transacciones objtransa = new();
                objtransa.BloquearDesbloquearDataIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
            }

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

        }

        private void cmbOrigen2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia2.Text = null;
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia2.Clear();
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        private void dgvGastos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvGastos.CurrentCell == null)
                return;

            // Verifica por nombre de columna, no por índice
            if (dgvGastos.CurrentCell.OwningColumn.Name == "NombreCuenta")
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    // Usa la colección ya cargada desde CargarDatosAutocompletadoGastos()
                    autoText.AutoCompleteCustomSource = Subcuentas;
                }
            }
            else
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.None;
                    autoText.AutoCompleteSource = AutoCompleteSource.None;
                    autoText.AutoCompleteCustomSource = null;
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
                int referenciaInt = 0;
                int.TryParse(txtNoReferencia.Text.Trim(), out referenciaInt);
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
                    int nuevoID = ingresos.IngresarIngresos(fechaTransaccion, descripcion,monto, referenciaInt, idUsuario, idOrigen, nombreCuenta);
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
                MessageBox.Show("Error al guardar la transaccion: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (!errorGuardado)
            {
                if (filasGuardadas > 0)
                {
                    MessageBox.Show("Se guardo correctamente el ingreso.", "Transaccion guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- CÓDIGO DE BITÁCORA INSERTADO ---
                    try
                    {
                        decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out decimal montoGuardado);
                        string nombreCuentaGuardada = fila.Cells["nombrecuenta"].Value?.ToString() ?? "";
                        string descripcionHistorial = $"Ingreso en '{nombreCuentaGuardada}' por {montoGuardado:C2}. Ref: {txtNoReferencia.Text}";

                        crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Ingresos", "Nuevo Ingreso", montoGuardado, descripcionHistorial);
                    }
                    catch (Exception exHistorial) { Console.WriteLine("Error de Bitácora: " + exHistorial.Message); }
                    // --- FIN DEL CÓDIGO DE BITÁCORA ---

                    Transacciones objtransa = new Transacciones();
                    objtransa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                    ActualizarSaldo();
                }
                else
                {
                    MessageBox.Show("No se guardo la transaccion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }



        private void cmbOrigen2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void panelGastos2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Transacciones objtransa = new();
            objtransa.BloquearDesbloquearDataGastos(dtDatosGastos, dgvGastos, e.RowIndex);

        }

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private int idTransaccionAEditar = 0;


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (modoEdicion)
            {
                MessageBox.Show("Ya estás en modo edición. Realiza los cambios y presiona GUARDAR.",
                                "Modo Edición Activo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para editar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
            if (filaSeleccionada.IsNewRow)
            {
                MessageBox.Show("No puede editar una fila vacía.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cargar datos de la fila
            object idOrigenValue = filaSeleccionada.Cells["Id_Origen"]?.Value;
            object referenciaValue = filaSeleccionada.Cells["NoReferencia"]?.Value;
            object fechaValue = filaSeleccionada.Cells["FechaTransaccion"]?.Value;

            if (idOrigenValue != null && idOrigenValue != DBNull.Value)
                cmbOrigen.SelectedValue = Convert.ToInt32(idOrigenValue);
            else
                cmbOrigen.SelectedIndex = -1;

            txtNoReferencia.Text = referenciaValue?.ToString() ?? "";
            dtpFecha.Value = fechaValue != null && DateTime.TryParse(fechaValue.ToString(), out DateTime fecha)
                ? fecha : DateTime.Now;

            //Desbloquear todas las columnas para edición libre
            dataGridView1.ReadOnly = false;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.ReadOnly = false;

            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;

            modoEdicion = true;

            MessageBox.Show("Modo edición activado. Puedes editar libremente las celdas.",
                            "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //IngresosIn ing = new();
            //ing.BloquearDesbloquearIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            dgvGastos.EndEdit();
            this.Validate();

            if (dgvGastos.DataSource != null)
            {
                this.BindingContext[dgvGastos.DataSource]?.EndCurrentEdit();
            }

            int idOrigen = Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);

            if (idOrigen == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            Gastos gastos = new();
            bool errorGuardado = false;
            int filasGuardadas = 0;

            try
            {
                DateTime fechaTransaccion = dtpFecha.Value; // Debería ser dateTimePicker1 del panel de gastos
                int referenciaInt = 0;
                int.TryParse(txtNoReferencia.Text.Trim(), out referenciaInt);
                int idUsuario = Sesion1.UsuarioID;

                string nombreCuenta = fila.Cells["dataGridViewTextBoxColumn1"].Value?.ToString() ?? string.Empty;
                string descripcion = fila.Cells["dataGridViewTextBoxColumn2"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila.Cells["dataGridViewTextBoxColumn3"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("Monto invalido para la cuenta: {nombreCuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    int nuevoID = gastos.IngresarGastos(fechaTransaccion, descripcion, monto, referenciaInt, idUsuario, idOrigen, nombreCuenta);
                    filasGuardadas++;
                    fila.Cells["dataGridViewTextBoxColumn3"].Style.ForeColor = Color.Red;
                }
                catch (Exception exGuardado)
                {
                    errorGuardado = true;
                    MessageBox.Show("Error al guardar la fila para la cuenta {nombreCuenta}: {exGuardado.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // --- CÓDIGO DE BITÁCORA INSERTADO ---
                    try
                    {
                        decimal.TryParse(fila.Cells["dataGridViewTextBoxColumn3"].Value?.ToString(), out decimal montoGuardado);
                        string nombreCuentaGuardada = fila.Cells["dataGridViewTextBoxColumn1"].Value?.ToString() ?? "";
                        string descripcionHistorial = "Gasto en '{nombreCuentaGuardada}' por {montoGuardado:C2}. Ref: {txtNoReferencia2.Text}";

                        crudHistorial.RegistrarAccionUsuario(Id_UsuarioLogin, "Gastos", "Nuevo Gasto", montoGuardado, descripcionHistorial);
                    }
                    catch (Exception exHistorial) { Console.WriteLine("Error de Bitácora: " + exHistorial.Message); }
                    // --- FIN DEL CÓDIGO DE BITÁCORA ---

                    Transacciones objtransa = new Transacciones();
                    objtransa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                    ActualizarSaldo();
                }
                else
                {
                    MessageBox.Show("No se guardó la transacción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (modoEdicion)
            {
                MessageBox.Show("Ya estás en modo edición. Realiza los cambios y presiona GUARDAR.",
                                "Modo Edición Activo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvGastos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para editar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow filaSeleccionada = dgvGastos.SelectedRows[0];
            if (filaSeleccionada.IsNewRow)
            {
                MessageBox.Show("No puede editar una fila vacía.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cargar datos de la fila
            object idOrigenValue = filaSeleccionada.Cells["Id_Origen"]?.Value;
            object referenciaValue = filaSeleccionada.Cells["NoReferencia"]?.Value;
            object fechaValue = filaSeleccionada.Cells["FechaTransaccion"]?.Value;

            if (idOrigenValue != null && idOrigenValue != DBNull.Value)
                cmbOrigen2.SelectedValue = Convert.ToInt32(idOrigenValue);
            else
                cmbOrigen2.SelectedIndex = -1;

            txtNoReferencia2.Text = referenciaValue?.ToString() ?? "";
            dateTimePicker1.Value = fechaValue != null && DateTime.TryParse(fechaValue.ToString(), out DateTime fecha)
                ? fecha : DateTime.Now;

            //Desbloquear todas las columnas para edición libre
            dgvGastos.ReadOnly = false;
            foreach (DataGridViewColumn col in dgvGastos.Columns)
                col.ReadOnly = false;

            dgvGastos.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvGastos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvGastos.MultiSelect = false;

            modoEdicion = true;

            MessageBox.Show("Modo edición activado. Puedes editar libremente las celdas.",
                            "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    
}

