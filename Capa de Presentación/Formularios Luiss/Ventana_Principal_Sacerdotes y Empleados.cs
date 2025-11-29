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
<<<<<<< HEAD
using System.Media;
=======
>>>>>>> v
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
        private int cuentaBancoid_seleccionado = 0;
        private AutoCompleteStringCollection Subcuentas = new AutoCompleteStringCollection();
        private ClsAccionesDB objSubCuentas = new ClsAccionesDB();

        private clsCRUD_Historial crudHistorial;

<<<<<<< HEAD
        private readonly Alerta _objalerta = new();
        private readonly ControlarAlerta _controladorAlerta;
        private readonly SoundPlayer _player = new SoundPlayer();

        private System.Windows.Forms.Timer _animationTimer;
        private const int _TARGET_HEIGHT = 40; // Altura final deseada del panel (ajustar si es necesario)
        private const int _SLIDE_SPEED = 5;    // Velocidad de la animación 
        private bool _isOpening = false;       // Indica si la alerta se está abriendo o cerrando

        private int PredictedId { get; set; }
        private int ParroquiaId { get; set; }

        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42(int predicted_id, int parroquia_id)
        {
            InitializeComponent();
            PredictedId = predicted_id;
            ParroquiaId = parroquia_id;
=======
        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42(int predicted_id)
        {
            InitializeComponent();
>>>>>>> v

            InicializarDGVIngr();
            InicializarDGVgastos();
            CargarDatosAutocompletado();
            CargarDatosAutocompletadoGastos();
            Transacciones obj_transa = new();
            obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


            crudCataloCuentas = new clsCRUD_CatalogoCuentas();

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();

            crudHistorial = new clsCRUD_Historial();

            this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            //panelContenedor.Controls.Add(panelMensaje);

            // muestra uno por defecto
            MostrarSoloEstePanel(panel1);
<<<<<<< HEAD

            _controladorAlerta = new ControlarAlerta(_objalerta);

            _controladorAlerta.OnAlertaStateChanged += ManejarCambioEstadoAlerta;

            _controladorAlerta.IniciarMonitoreo(intervaloMinutos: 5);

            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 10; // Rápido (10ms) para movimiento suave
            _animationTimer.Tick += AnimationTimer_Tick;

            // Inicializar el panel de alerta oculto
            pnlAlertaDeslizante.Height = 0;
            pnlAlertaDeslizante.Visible = true; // Lo dejamos Visible, pero con Altura 0
        }

<<<<<<< HEAD
=======
        }

        public FRM_42()
        {
        }

        public FRM_42(int predicted_id, int parroquia_id) : this(predicted_id)
        {
        }
>>>>>>> v
=======
        public FRM_42() : this(0, 0)
        {
        }
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)

        private void FRM_42_Load(object sender, EventArgs e)
        {
            dgvGastos.AllowUserToAddRows = false;

            DateTime mes_actual = DateTime.Now;
            DateTime mes_actual1 = new DateTime(mes_actual.Year, mes_actual.Month, 1);
            DateTime hoy_sin_hora = DateTime.Today;

            dtpFecha.Value = hoy_sin_hora;
            dateTimePicker1.Value = hoy_sin_hora;
            // ------------------------------------------

            dtpFecha.MinDate = mes_actual1;
            dateTimePicker1.MinDate = mes_actual1;
            dtpFecha.MaxDate = hoy_sin_hora;
            dateTimePicker1.MaxDate = hoy_sin_hora;

            if (dtpFecha.Value < dtpFecha.MinDate || dtpFecha.Value > dtpFecha.MaxDate)
                dtpFecha.Value = hoy_sin_hora;

            if (dateTimePicker1.Value < dateTimePicker1.MinDate || dateTimePicker1.Value > dateTimePicker1.MaxDate)
                dateTimePicker1.Value = hoy_sin_hora;

<<<<<<< HEAD
            ActualizarSaldo();
            CargarCuentasEnComboBox();
            _controladorAlerta.ForzarVerificacionInmediata();

        }

        private void CargarCuentasEnComboBox()
        {
            try
            {
                // 1. Desvincula el evento para que no se dispare
                cmbCuentas.SelectedIndexChanged -= cmbCuentas_SelectedIndexChanged;

                // Carga los datos como ya lo haces
                DataTable dt_cuentas = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DataSource = dt_cuentas;
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_Origen";

                // Asegúrate de que no haya nada seleccionado al inicio
                cmbCuentas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 3. Vuelve a vincular el evento para que funcione cuando el usuario haga clic
                cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            }
        }

        private void ManejarCambioEstadoAlerta(bool activo, string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ManejarCambioEstadoAlerta(activo, mensaje)));
                return;
            }

            if (activo && pnlAlertaDeslizante.Height == 0) // Si está activo y actualmente cerrado
            {
                // 3.1. CONFIGURACIÓN AL MOSTRAR
                lblAlertaMensaje.Text = mensaje;
                _isOpening = true;
                _animationTimer.Start();

                // Reproducir sonido
                _player.Play();
            }
            else if (!activo && pnlAlertaDeslizante.Height > 0) // Si está inactivo y actualmente abierto
            {
                // 3.2. CONFIGURACIÓN AL OCULTAR
                _isOpening = false;
                _animationTimer.Start();
            }
        }

        // 4. El corazón de la animación: mueve el panel en cada "tick"
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_isOpening) // Deslizar hacia abajo (Mostrar)
            {
                if (pnlAlertaDeslizante.Height < _TARGET_HEIGHT)
                {
                    pnlAlertaDeslizante.Height += _SLIDE_SPEED;
                    if (pnlAlertaDeslizante.Height >= _TARGET_HEIGHT)
                    {
                        pnlAlertaDeslizante.Height = _TARGET_HEIGHT; // Asegurar el tope
                        _animationTimer.Stop();
                    }
                }
            }
            else // Deslizar hacia arriba (Ocultar)
            {
                if (pnlAlertaDeslizante.Height > 0)
                {
                    pnlAlertaDeslizante.Height -= _SLIDE_SPEED;
                    if (pnlAlertaDeslizante.Height <= 0)
                    {
                        pnlAlertaDeslizante.Height = 0; // Asegurar que quede en cero
                        _animationTimer.Stop();
                    }
                }
            }
        }

        //Detener el Timer al cerrar el formulario para liberar recursos
        private void Frm_42_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controladorAlerta.DetenerMonitoreo();
=======



            ActualizarSaldo();
            CargarCuentasEnComboBox();




>>>>>>> v
        }

<<<<<<< HEAD
        private void CargarCuentasEnComboBox()
        {
            try
            {
                // 1. Desvincula el evento para que no se dispare
                cmbCuentas.SelectedIndexChanged -= cmbCuentas_SelectedIndexChanged;

                // Carga los datos como ya lo haces
                DataTable dt_cuentas = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DataSource = dt_cuentas;
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_Origen";

                // Asegúrate de que no haya nada seleccionado al inicio
                cmbCuentas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 3. Vuelve a vincular el evento para que funcione cuando el usuario haga clic
                cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            }
        }

<<<<<<< HEAD
        

        //Detener el Timer al cerrar el formulario para liberar recursos
      

=======
>>>>>>> v
=======
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
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
<<<<<<< HEAD
=======

                //aqui es para ocultar algunos campos (los ids y las contraseñas)
                /*
                if (dgvUsuarios.Columns["Contraseña"] != null)
                    dgvUsuarios.Columns["Contraseña"].Visible = false;

                if (dgvUsuarios.Columns["RolID"] != null)
                    dgvUsuarios.Columns["RolID"].Visible = false;
                if (dgvUsuarios.Columns["parroquia_id"] != null)
                    dgvUsuarios.Columns["parroquia_id"].Visible = false;
                if (dgvUsuarios.Columns["EstadoID"] != null)
                    dgvUsuarios.Columns["EstadoID"].Visible = false;
                */
>>>>>>> v
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




        private void MostrarSoloEstePanel(Panel panel_mostrar)
        {
            foreach (Control ctrl in panelContenedor.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }
            panel_mostrar.Visible = true;
            panel_mostrar.BringToFront();
        }



        private void btnGastos_Click(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelGastos2);
            RegistrarNavegacion("Gastos");
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCajaChica2);
            RegistrarNavegacion("Caja Chica");

        }
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelIngresos);
            RegistrarNavegacion("Ingresos");
        }

        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelBancos2);
            RegistrarNavegacion("Bancos");
        }







        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS();
            popup.StartPosition = FormStartPosition.Manual;
            var button_screen_position = pictureBox1.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(button_screen_position.X, button_screen_position.Y + pictureBox1.Height);
            popup.ShowDialog(this);
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Cerrar_Sesión popup = new Cerrar_Sesión();
            var button_screen_position = pictureBox2.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(button_screen_position.X, button_screen_position.Y + pictureBox2.Height);
            popup.ShowDialog();
        }



        private void btnDetalle_Click(object sender, EventArgs e)
        {
            using (var frm = new Partidas_Dobles())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void cmbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {


            // Si no hay nada seleccionado, no hace nada
            if (cmbCuentas.SelectedIndex < 0 || cmbCuentas.SelectedValue == null)
            {
                return;
            }

<<<<<<< HEAD

            int id_seleccionado = Convert.ToInt32(cmbCuentas.SelectedValue);


            var frm = new BancosCuentaAhorro(id_seleccionado)
            {
                StartPosition = FormStartPosition.Manual,

=======
            
            int id_seleccionado = Convert.ToInt32(cmbCuentas.SelectedValue);

            
            var frm = new BancosCuentaAhorro(id_seleccionado) 
            {
                StartPosition = FormStartPosition.Manual,
                
>>>>>>> v
                Location = new Point(414, 101)
            };

            frm.ShowDialog(this);
        }


        private void cmbAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcciones.SelectedIndex < 0) return;
            Transacciones transacciones_obj = new();

            switch (cmbAcciones.SelectedIndex)
            {
                case 0: // Agregar Saldo
                    {
                        using (var frm = new BancosAgregarSaldo
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                            }
                        }
                    }
                    break;

                case 1: // Transferencia entre cuentas
                    {
                        using (var frm = new BancosTransferenciaEntreCuentas
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                            }
                        }
                    }
                    break;
                case 2: // Agregar cuenta bancaria
                    {
                        using (var frm = new BancosAgregarCuentaBancaria
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                                CargarCuentasEnComboBox();
                            }
                        }
                    }
                    break;
                case 3: // Retirar dinero
                    {
                        using (var frm = new BancosRetirarDinero
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);
                            }
                        }
                    }
                    break;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDetalle_Click_1(object sender, EventArgs e)
        {
            //llamar form 69,
            Partidas_Dobles obj_frm69 = new Partidas_Dobles();
            obj_frm69.ShowDialog();
        }

        private void chkSaldoInicial_CheckedChanged(object sender, EventArgs e)
        {
            CajaChicaMonto obj_caja = new CajaChicaMonto();
            if (chkSaldoInicial.Checked)
            {

                obj_caja.SaldoActualizado += this.ActualizarSaldo;

                obj_caja.ShowDialog();

                obj_caja.SaldoActualizado -= this.ActualizarSaldo;
            }
            ActualizarSaldo();
            Transacciones transacciones = new();
            transacciones.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);

            if (txtSaldoActual.Text == "0.00")
            {
                chkSaldoInicial.Visible = true;
                obj_caja.ShowDialog();
            }
            else
            {
                chkSaldoInicial.Visible = false;
            }



        }
        private void MostrarSaldoActual()
        {
            Clsconexion obj_con = new Clsconexion();

            try
            {
                obj_con.Abrir();

                string query = "exec saldo_actual";
                SqlCommand comando = new SqlCommand(query, obj_con.sc);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
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
                obj_con.Cerrar();
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
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> v
=======

>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
        private void txtSaldoActual_TextChanged(object sender, EventArgs e)
        {

        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> v
=======

>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> v
=======

>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
        private void button2_Click(object sender, EventArgs e)
        {
            cmbOrigen.Text = "Seleccionar";
            txtNoReferencia.Text = null;
            Transacciones transa = new();
            transa.Agregarfila(dtDatosIngresos, dataGridView1);


        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> v
=======

>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
        private void dgvIngresos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

<<<<<<< HEAD
<<<<<<< HEAD
=======


>>>>>>> v
=======


>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> v
=======

>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
        private void InicializarDGVIngr()
        {
            dataGridView1.Columns.Clear();
            dtDatosIngresos = new DataTable("Ingresos");
            dtDatosIngresos.Columns.Add("Id_transaccion", typeof(int));
            dtDatosIngresos.Columns.Add("Id_Origen", typeof(int));
<<<<<<< HEAD
            dtDatosIngresos.Columns.Add("NombreCuenta", typeof(string));
=======
            dtDatosIngresos.Columns.Add("nombre_cuenta", typeof(string));
>>>>>>> v
            dtDatosIngresos.Columns.Add("Detalle", typeof(string));
            dtDatosIngresos.Columns.Add("Saldo", typeof(string));
            dtDatosIngresos.Columns.Add("fecha_transaccion", typeof(DateTime));
            dtDatosIngresos.Columns.Add("NoReferencia", typeof(int));
            dataGridView1.DataSource = dtDatosIngresos;

            dataGridView1.AutoGenerateColumns = false;

            if (dataGridView1.Columns.Contains("Id_transaccion"))
                dataGridView1.Columns["Id_transaccion"].Visible = false;
            if (dataGridView1.Columns.Contains("Id_Origen"))
                dataGridView1.Columns["Id_Origen"].Visible = false;
            if (dataGridView1.Columns.Contains("fecha_transaccion"))
                dataGridView1.Columns["fecha_transaccion"].Visible = false;
            if (dataGridView1.Columns.Contains("NoReferencia"))
                dataGridView1.Columns["NoReferencia"].Visible = false;
        }

        private void InicializarDGVgastos()
        {
            dgvGastos.Columns.Clear();
            dtDatosGastos = new DataTable("Gastos");
            dtDatosGastos.Columns.Add("Id_transaccion", typeof(int));
            dtDatosGastos.Columns.Add("Id_Origen", typeof(int));
<<<<<<< HEAD
            dtDatosGastos.Columns.Add("NombreCuenta", typeof(string));
=======
            dtDatosGastos.Columns.Add("nombre_cuenta", typeof(string));
>>>>>>> v
            dtDatosGastos.Columns.Add("Detalle", typeof(string));
            dtDatosGastos.Columns.Add("Saldo", typeof(string));
            dtDatosGastos.Columns.Add("fecha_transaccion", typeof(DateTime));
            dtDatosGastos.Columns.Add("NoReferencia", typeof(int));
            dgvGastos.DataSource = dtDatosGastos;
            dgvGastos.AutoGenerateColumns = true;


            if (dgvGastos.Columns.Contains("Id_transaccion"))
                dgvGastos.Columns["Id_transaccion"].Visible = false;
            if (dgvGastos.Columns.Contains("Id_Origen"))
                dgvGastos.Columns["Id_Origen"].Visible = false;
            if (dgvGastos.Columns.Contains("fecha_transaccion"))
                dgvGastos.Columns["fecha_transaccion"].Visible = false;
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
            int last_index = dataGridView1.Rows.Count - 1;
            if (e.RowIndex == last_index)
            {
                Transacciones obj_transa = new();
                obj_transa.BloquearDesbloquearDataIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
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
<<<<<<< HEAD
            if (dataGridView1.CurrentCell.OwningColumn.Name == "NombreCuenta")
=======
            if (dataGridView1.CurrentCell.OwningColumn.Name == "nombre_cuenta")
>>>>>>> v
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    auto_text.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    ClsAccionesDB clsAccionesDB = new();
                    DataTable dt_cuentas = clsAccionesDB.ObtenerCuentasIngreso();
                    AutoCompleteStringCollection nombres_cuentas = new AutoCompleteStringCollection();

                    foreach (DataRow row in dt_cuentas.Rows)
                    {
                        nombres_cuentas.Add(row["Subcuentas"].ToString());
                    }

                    auto_text.AutoCompleteCustomSource = nombres_cuentas;
                }
            }
            else
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.None;
                    auto_text.AutoCompleteSource = AutoCompleteSource.None;
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Certificados_De_Depósito fRM_PG103 = new Certificados_De_Depósito();
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
<<<<<<< HEAD
            if (dgvGastos.CurrentCell.OwningColumn.Name == "NombreCuenta")
=======
            if (dgvGastos.CurrentCell.OwningColumn.Name == "nombre_cuenta")
>>>>>>> v
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    auto_text.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    // Usa la colección ya cargada desde CargarDatosAutocompletadoGastos()
                    auto_text.AutoCompleteCustomSource = Subcuentas;
                }
            }
            else
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.None;
                    auto_text.AutoCompleteSource = AutoCompleteSource.None;
                    auto_text.AutoCompleteCustomSource = null;
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            try
=======

            if (modoEdicion)
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una fila para guardar la edición.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewColumn primera_visible = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible);

                    if (primera_visible != null)
                        dataGridView1.CurrentCell = dataGridView1.SelectedRows[0].Cells[primera_visible.Index];
                }

                DataGridViewRow fila = dataGridView1.CurrentRow;

                string nombre_cuenta = fila.Cells["NombreCuenta"].Value?.ToString() ?? "";
                string detalle = fila.Cells["Detalle"].Value?.ToString() ?? "";
                decimal saldo = 0;
                decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out saldo);
                DateTime fecha_tr = dtpFecha.Value;
                string referencia_texto = txtNoReferencia.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);
                int id_origen = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);

                if (string.IsNullOrEmpty(nombre_cuenta))
                {
                    MessageBox.Show("Debe ingresar el nombre de la cuenta.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (saldo <= 0)
                {
                    MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Llamar al método de edición
                IngresosIn editar = new IngresosIn();
                bool actualizado = editar.GuardarEdicion(dtDatosIngresos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                    txtNoReferencia, cmbOrigen, dataGridView1, dtpFecha);

                if (actualizado)
                {
                    modoEdicion = false;
                    dataGridView1.ReadOnly = true;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                        col.ReadOnly = true;

                    //Actualiza el Id_Origen en la fila actual 
                    if (dataGridView1.CurrentRow != null)
                        dataGridView1.CurrentRow.Cells["Id_Origen"].Value = id_origen;

                    txtNoReferencia.Clear();
                    dtpFecha.Value = DateTime.Now;

                    //Recargar los combos SIN perder la selección
                    Transacciones obj_transa = new Transacciones();
                    obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);

                    //Forzar actualización visual segura del ComboBox
                    this.BeginInvoke(new Action(() =>
                    {
                        cmbOrigen.SelectedValue = id_origen;  // Selecciona el origen actualizado
                        cmbOrigen.Refresh();                 // Refresca el control en pantalla
                    }));

                    //Actualizar saldo después de recargar origen
                    ActualizarSaldo();

                    MessageBox.Show("Transacción editada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _controladorAlerta.ForzarVerificacionInmediata();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la transacción. Verifique los datos o la conexión.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            dataGridView1.EndEdit();
            this.Validate();
            this.BindingContext[dataGridView1.DataSource]?.EndCurrentEdit();

            int id_origenNuevo = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);
            if (id_origenNuevo == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila_nueva = null;
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                var row = dataGridView1.Rows[i];
                if (!row.IsNewRow)
                {
                    bool tiene_datos = false;
                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            tiene_datos = true;
                            break;
                        }
                    }
                    if (tiene_datos)
                    {
                        fila_nueva = row;
                        break;
                    }
                }
            }

            if (fila_nueva == null)
            {
                MessageBox.Show("No hay ninguna fila válida para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //GUARDAR NUEVO INGRESO

            Ingresos ingresos = new();
            bool error_guardado = false;
            int filas_guardadas = 0;

            try
            {
                DateTime fecha_transaccion = dtpFecha.Value;
                string referencia_texto = txtNoReferencia.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);

                int id_usuario = Sesion1.usuario_id;
                string nombre_cuenta = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? string.Empty;
                string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila_nueva.Cells["Saldo"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show($"Monto inválido para la cuenta: {NombreCuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nuevo_id = ingresos.IngresarIngresos(fecha_transaccion, descripcion, monto, referencia, id_usuario, id_origenNuevo, nombre_cuenta);
                if (nuevo_id <= 0)
                {
                    MessageBox.Show("No se recibió un ID válido desde la base de datos. Verifique el SP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                filas_guardadas++;

                if (!dtDatosIngresos.Columns.Contains("Id_transaccion"))
                    dtDatosIngresos.Columns.Add("Id_transaccion", typeof(int));

                if (dataGridView1.Columns.Contains("Id_transaccion"))
                    fila_nueva.Cells["Id_transaccion"].Value = nuevo_id;

                fila_nueva.Cells["fecha_transaccion"].Value = fecha_transaccion;
                fila_nueva.Cells["NoReferencia"].Value = referencia;
                fila_nueva.Cells["Id_Origen"].Value = id_origenNuevo;
                fila_nueva.Cells["Saldo"].Style.ForeColor = Color.Green;

                dataGridView1.Refresh();
                dataGridView1.ClearSelection();

                MessageBox.Show("Ingreso registrado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Transacciones obj_transa = new Transacciones();
                obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


                ActualizarSaldo();
                _controladorAlerta.ForzarVerificacionInmediata();
            }
            catch (Exception ex)
            {
                error_guardado = true;
                MessageBox.Show("Error al guardar la transacción: " + ex.Message,
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (!error_guardado && filas_guardadas > 0)
            {
                modoEdicion = false;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    col.ReadOnly = true;

                dataGridView1.ReadOnly = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.ClearSelection();

                txtNoReferencia.Clear();
                cmbOrigen.SelectedIndex = -1;
                dtpFecha.Value = DateTime.Now;

            }
            else if (error_guardado)
            {
                MessageBox.Show("No se guardó la transacción debido a un error.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
<<<<<<< HEAD
        

=======

            if (modoEdicion)
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una fila para guardar la edición.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                if (dataGridView1.CurrentRow == null && dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewColumn primera_visible = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible);

                    if (primera_visible != null)
                        dataGridView1.CurrentCell = dataGridView1.SelectedRows[0].Cells[primera_visible.Index];
                }

                DataGridViewRow fila = dataGridView1.CurrentRow;

                string nombre_cuenta = fila.Cells["nombre_cuenta"].Value?.ToString() ?? "";
                string detalle = fila.Cells["Detalle"].Value?.ToString() ?? "";
                decimal saldo = 0;
                decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out saldo);
                DateTime fecha_tr = dtpFecha.Value;
                string referencia_texto = txtNoReferencia.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);
                int id_origen = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);

                if (string.IsNullOrEmpty(nombre_cuenta))
                {
                    MessageBox.Show("Debe ingresar el nombre de la cuenta.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (saldo <= 0)
                {
                    MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Llamar al método de edición
                IngresosIn editar = new IngresosIn();
                bool actualizado = editar.GuardarEdicion(dtDatosIngresos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                    txtNoReferencia, cmbOrigen, dataGridView1, dtpFecha);

                if (actualizado)
                {
                    modoEdicion = false;
                    dataGridView1.ReadOnly = true;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                        col.ReadOnly = true;

                    //Actualiza el Id_Origen en la fila actual 
                    if (dataGridView1.CurrentRow != null)
                        dataGridView1.CurrentRow.Cells["Id_Origen"].Value = id_origen;

                    txtNoReferencia.Clear();
                    dtpFecha.Value = DateTime.Now;

                    //Recargar los combos SIN perder la selección
                    Transacciones obj_transa = new Transacciones();
                    obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);

                    //Forzar actualización visual segura del ComboBox
                    this.BeginInvoke(new Action(() =>
                    {
                        cmbOrigen.SelectedValue = id_origen;  // Selecciona el origen actualizado
                        cmbOrigen.Refresh();                 // Refresca el control en pantalla
                    }));

                    //Actualizar saldo después de recargar origen
                    ActualizarSaldo();

                    MessageBox.Show("Transacción editada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la transacción. Verifique los datos o la conexión.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            dataGridView1.EndEdit();
            this.Validate();
            this.BindingContext[dataGridView1.DataSource]?.EndCurrentEdit();

            int id_origenNuevo = Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);
            if (id_origenNuevo == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila_nueva = null;
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                var row = dataGridView1.Rows[i];
                if (!row.IsNewRow)
                {
                    bool tiene_datos = false;
                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            tiene_datos = true;
                            break;
                        }
                    }
                    if (tiene_datos)
                    {
                        fila_nueva = row;
                        break;
                    }
                }
            }

            if (fila_nueva == null)
            {
                MessageBox.Show("No hay ninguna fila válida para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //GUARDAR NUEVO INGRESO

            Ingresos ingresos = new();
            bool error_guardado = false;
            int filas_guardadas = 0;

            try
            {
                DateTime fecha_transaccion = dtpFecha.Value;
                string referencia_texto = txtNoReferencia.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);

                int id_usuario = Sesion1.usuario_id;
                string nombre_cuenta = fila_nueva.Cells["nombre_cuenta"].Value?.ToString() ?? string.Empty;
                string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila_nueva.Cells["Saldo"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show($"Monto inválido para la cuenta: {nombre_cuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nuevo_id = ingresos.IngresarIngresos(fecha_transaccion, descripcion, monto, referencia, id_usuario, id_origenNuevo, nombre_cuenta);
                if (nuevo_id <= 0)
                {
                    MessageBox.Show("No se recibió un ID válido desde la base de datos. Verifique el SP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                filas_guardadas++;

                if (!dtDatosIngresos.Columns.Contains("Id_transaccion"))
                    dtDatosIngresos.Columns.Add("Id_transaccion", typeof(int));

                if (dataGridView1.Columns.Contains("Id_transaccion"))
                    fila_nueva.Cells["Id_transaccion"].Value = nuevo_id;

                fila_nueva.Cells["fecha_transaccion"].Value = fecha_transaccion;
                fila_nueva.Cells["NoReferencia"].Value = referencia;
                fila_nueva.Cells["Id_Origen"].Value = id_origenNuevo;
                fila_nueva.Cells["Saldo"].Style.ForeColor = Color.Green;

                dataGridView1.Refresh();
                dataGridView1.ClearSelection();

                MessageBox.Show("Ingreso registrado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Transacciones obj_transa = new Transacciones();
                obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


                ActualizarSaldo();
            }
            catch (Exception ex)
            {
                error_guardado = true;
                MessageBox.Show("Error al guardar la transacción: " + ex.Message,
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (!error_guardado && filas_guardadas > 0)
            {
                modoEdicion = false;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    col.ReadOnly = true;

                dataGridView1.ReadOnly = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.ClearSelection();

                txtNoReferencia.Clear();
                cmbOrigen.SelectedIndex = -1;
                dtpFecha.Value = DateTime.Now;

            }
            else if (error_guardado)
            {
                MessageBox.Show("No se guardó la transacción debido a un error.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
>>>>>>> v
=======
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)



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
            Transacciones obj_transa = new();
            obj_transa.BloquearDesbloquearDataGastos(dtDatosGastos, dgvGastos, e.RowIndex);

        }

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private int id_transaccionAEditar = 0;


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

            DataGridViewRow fila_seleccionada = dataGridView1.SelectedRows[0];
            if (fila_seleccionada.IsNewRow)
            {
                MessageBox.Show("No puede editar una fila vacía.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cargar datos de la fila
            object id_origenValue = fila_seleccionada.Cells["Id_Origen"]?.Value;
            object referencia_value = fila_seleccionada.Cells["NoReferencia"]?.Value;
            object fecha_value = fila_seleccionada.Cells["fecha_transaccion"]?.Value;

            if (id_origenValue != null && id_origenValue != DBNull.Value)
                cmbOrigen.SelectedValue = Convert.ToInt32(id_origenValue);
            else
                cmbOrigen.SelectedIndex = -1;

            txtNoReferencia.Text = referencia_value?.ToString() ?? "";
            dtpFecha.Value = fecha_value != null && DateTime.TryParse(fecha_value.ToString(), out DateTime fecha)
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
<<<<<<< HEAD
<<<<<<< HEAD
            try
=======

            if (modoEdicion)
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
            {
                if (dgvGastos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una fila para guardar la edición.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Corrige posible celda invisible
                if (dgvGastos.CurrentRow == null && dgvGastos.SelectedRows.Count > 0)
                {
                    DataGridViewColumn primera_visible = dgvGastos.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible);

                    if (primera_visible != null)
                        dgvGastos.CurrentCell = dgvGastos.SelectedRows[0].Cells[primera_visible.Index];
                }

                DataGridViewRow fila = dgvGastos.CurrentRow;

                string nombre_cuenta = fila.Cells["NombreCuenta"].Value?.ToString() ?? "";
                string detalle = fila.Cells["Detalle"].Value?.ToString() ?? "";
                decimal saldo = 0;
                decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out saldo);
                DateTime fecha_tr = dateTimePicker1.Value;
                string referencia_texto = txtNoReferencia2.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);
                int id_origen = Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);

                if (string.IsNullOrEmpty(nombre_cuenta))
                {
                    MessageBox.Show("Debe ingresar el nombre de la cuenta.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (saldo <= 0)
                {
                    MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Llamar al método de edición
                GastosGa editar = new();
                bool actualizado = editar.GuardarEdicion2(dtDatosGastos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                    txtNoReferencia2, cmbOrigen2, dgvGastos, dateTimePicker1);

                if (actualizado)
                {
                    modoEdicion = false;
                    dgvGastos.ReadOnly = true;
                    foreach (DataGridViewColumn col in dgvGastos.Columns)
                        col.ReadOnly = true;

                    //Actualiza el Id_Origen en la fila actual (para que el lápiz lea el correcto)
                    if (dgvGastos.CurrentRow != null)
                        dgvGastos.CurrentRow.Cells["Id_Origen"].Value = id_origen;

                    txtNoReferencia.Clear();
                    dateTimePicker1.Value = DateTime.Now;

                    //Recargar los combos SIN perder la selección
                    Transacciones obj_transa = new Transacciones();
                    obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);

                    //Forzar actualización visual segura del ComboBox
                    this.BeginInvoke(new Action(() =>
                    {
                        cmbOrigen2.SelectedValue = id_origen;  // Selecciona el origen actualizado
                        cmbOrigen2.Refresh();                 // Refresca el control en pantalla
                    }));

                    // Actualizar saldo después de recargar origen
                    ActualizarSaldo();

                    MessageBox.Show("Transacción editada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _controladorAlerta.ForzarVerificacionInmediata();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la transacción. Verifique los datos o la conexión.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            dgvGastos.EndEdit();
            this.Validate();
            this.BindingContext[dgvGastos.DataSource]?.EndCurrentEdit();

            int id_origenNuevo = Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);
            if (id_origenNuevo == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila_nueva = null;
            for (int i = dgvGastos.Rows.Count - 1; i >= 0; i--)
            {
                var row = dgvGastos.Rows[i];
                if (!row.IsNewRow)
                {
                    bool tiene_datos = false;
                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            tiene_datos = true;
                            break;
                        }
                    }
                    if (tiene_datos)
                    {
                        fila_nueva = row;
                        break;
                    }
                }
            }

            if (fila_nueva == null)
            {
                MessageBox.Show("No hay ninguna fila válida para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //GUARDAR NUEVO GASTO

            Gastos gasto = new();
            bool error_guardado = false;
            int filas_guardadas = 0;

            try
            {
                DateTime fecha_transaccion = dtpFecha.Value;
                string referencia_texto = txtNoReferencia.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);

                int id_usuario = Sesion1.usuario_id;
                string nombre_cuenta = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? string.Empty;
                string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila_nueva.Cells["Saldo"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show($"Monto inválido para la cuenta: {NombreCuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nuevo_id = gasto.IngresarGastos(fecha_transaccion, descripcion, monto, referencia, id_usuario, id_origenNuevo, nombre_cuenta);
                if (nuevo_id <= 0)
                {
                    MessageBox.Show("No se recibió un ID válido desde la base de datos. Verifique el SP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                filas_guardadas++;

                if (!dtDatosGastos.Columns.Contains("Id_transaccion"))
                    dtDatosGastos.Columns.Add("Id_transaccion", typeof(int));

                if (dgvGastos.Columns.Contains("Id_transaccion"))
                    fila_nueva.Cells["Id_transaccion"].Value = nuevo_id;

                fila_nueva.Cells["fecha_transaccion"].Value = fecha_transaccion;
                fila_nueva.Cells["NoReferencia"].Value = referencia;
                fila_nueva.Cells["Id_Origen"].Value = id_origenNuevo;
                fila_nueva.Cells["Saldo"].Style.ForeColor = Color.Green;

                dgvGastos.Refresh();
                dgvGastos.ClearSelection();

                MessageBox.Show("Gasto registrado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Transacciones obj_transa = new Transacciones();
                obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


                ActualizarSaldo();
                _controladorAlerta.ForzarVerificacionInmediata();
            }
            catch (Exception ex)
            {
                error_guardado = true;
                MessageBox.Show("Error al guardar la transacción: " + ex.Message,
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

<<<<<<< HEAD
           
=======

            if (modoEdicion)
            {
                if (dgvGastos.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una fila para guardar la edición.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Corrige posible celda invisible
                if (dgvGastos.CurrentRow == null && dgvGastos.SelectedRows.Count > 0)
                {
                    DataGridViewColumn primera_visible = dgvGastos.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible);

                    if (primera_visible != null)
                        dgvGastos.CurrentCell = dgvGastos.SelectedRows[0].Cells[primera_visible.Index];
                }

                DataGridViewRow fila = dgvGastos.CurrentRow;

                string nombre_cuenta = fila.Cells["nombre_cuenta"].Value?.ToString() ?? "";
                string detalle = fila.Cells["Detalle"].Value?.ToString() ?? "";
                decimal saldo = 0;
                decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out saldo);
                DateTime fecha_tr = dateTimePicker1.Value;
                string referencia_texto = txtNoReferencia2.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);
                int id_origen = Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);

                if (string.IsNullOrEmpty(nombre_cuenta))
                {
                    MessageBox.Show("Debe ingresar el nombre de la cuenta.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (saldo <= 0)
                {
                    MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Llamar al método de edición
                GastosGa editar = new();
                bool actualizado = editar.GuardarEdicion2(dtDatosGastos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                    txtNoReferencia2, cmbOrigen2, dgvGastos, dateTimePicker1);

                if (actualizado)
                {
                    modoEdicion = false;
                    dgvGastos.ReadOnly = true;
                    foreach (DataGridViewColumn col in dgvGastos.Columns)
                        col.ReadOnly = true;

                    //Actualiza el Id_Origen en la fila actual (para que el lápiz lea el correcto)
                    if (dgvGastos.CurrentRow != null)
                        dgvGastos.CurrentRow.Cells["Id_Origen"].Value = id_origen;

                    txtNoReferencia.Clear();
                    dateTimePicker1.Value = DateTime.Now;

                    //Recargar los combos SIN perder la selección
                    Transacciones obj_transa = new Transacciones();
                    obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);

                    //Forzar actualización visual segura del ComboBox
                    this.BeginInvoke(new Action(() =>
                    {
                        cmbOrigen2.SelectedValue = id_origen;  // Selecciona el origen actualizado
                        cmbOrigen2.Refresh();                 // Refresca el control en pantalla
                    }));

                    // Actualizar saldo después de recargar origen
                    ActualizarSaldo();

                    MessageBox.Show("Transacción editada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la transacción. Verifique los datos o la conexión.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            dgvGastos.EndEdit();
            this.Validate();
            this.BindingContext[dgvGastos.DataSource]?.EndCurrentEdit();

            int id_origenNuevo = Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);
            if (id_origenNuevo == 0)
            {
                MessageBox.Show("Debe seleccionar un Origen de fondos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow fila_nueva = null;
            for (int i = dgvGastos.Rows.Count - 1; i >= 0; i--)
            {
                var row = dgvGastos.Rows[i];
                if (!row.IsNewRow)
                {
                    bool tiene_datos = false;
                    foreach (DataGridViewCell celda in row.Cells)
                    {
                        if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                        {
                            tiene_datos = true;
                            break;
                        }
                    }
                    if (tiene_datos)
                    {
                        fila_nueva = row;
                        break;
                    }
                }
            }

            if (fila_nueva == null)
            {
                MessageBox.Show("No hay ninguna fila válida para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //GUARDAR NUEVO GASTO

            Gastos gasto = new();
            bool error_guardado = false;
            int filas_guardadas = 0;

            try
            {
                DateTime fecha_transaccion = dtpFecha.Value;
                string referencia_texto = txtNoReferencia.Text.Trim();
                int referencia = 0;
                int.TryParse(referencia_texto, out referencia);

                int id_usuario = Sesion1.usuario_id;
                string nombre_cuenta = fila_nueva.Cells["nombre_cuenta"].Value?.ToString() ?? string.Empty;
                string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                if (!decimal.TryParse(fila_nueva.Cells["Saldo"].Value?.ToString(), out decimal monto) || monto <= 0)
                {
                    MessageBox.Show($"Monto inválido para la cuenta: {nombre_cuenta}", "Error de Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nuevo_id = gasto.IngresarGastos(fecha_transaccion, descripcion, monto, referencia, id_usuario, id_origenNuevo, nombre_cuenta);
                if (nuevo_id <= 0)
                {
                    MessageBox.Show("No se recibió un ID válido desde la base de datos. Verifique el SP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                filas_guardadas++;

                if (!dtDatosGastos.Columns.Contains("Id_transaccion"))
                    dtDatosGastos.Columns.Add("Id_transaccion", typeof(int));

                if (dgvGastos.Columns.Contains("Id_transaccion"))
                    fila_nueva.Cells["Id_transaccion"].Value = nuevo_id;

                fila_nueva.Cells["fecha_transaccion"].Value = fecha_transaccion;
                fila_nueva.Cells["NoReferencia"].Value = referencia;
                fila_nueva.Cells["Id_Origen"].Value = id_origenNuevo;
                fila_nueva.Cells["Saldo"].Style.ForeColor = Color.Green;

                dgvGastos.Refresh();
                dgvGastos.ClearSelection();

                MessageBox.Show("Gasto registrado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Transacciones obj_transa = new Transacciones();
                obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


                ActualizarSaldo();
            }
            catch (Exception ex)
            {
                error_guardado = true;
                MessageBox.Show("Error al guardar la transacción: " + ex.Message,
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

=======
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)

            if (!error_guardado && filas_guardadas > 0)
            {
                modoEdicion = false;

                foreach (DataGridViewColumn col in dgvGastos.Columns)
                    col.ReadOnly = true;

                dgvGastos.ReadOnly = false;
                dgvGastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvGastos.MultiSelect = false;
                dgvGastos.ClearSelection();

                txtNoReferencia2.Clear();
                cmbOrigen2.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
            }
            else if (error_guardado)
            {
                MessageBox.Show("No se guardó la transacción debido a un error.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
<<<<<<< HEAD
>>>>>>> v
=======
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
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

            DataGridViewRow fila_seleccionada = dgvGastos.SelectedRows[0];
            if (fila_seleccionada.IsNewRow)
            {
                MessageBox.Show("No puede editar una fila vacía.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cargar datos de la fila
            object id_origenValue = fila_seleccionada.Cells["Id_Origen"]?.Value;
            object referencia_value = fila_seleccionada.Cells["NoReferencia"]?.Value;
            object fecha_value = fila_seleccionada.Cells["fecha_transaccion"]?.Value;

            if (id_origenValue != null && id_origenValue != DBNull.Value)
                cmbOrigen2.SelectedValue = Convert.ToInt32(id_origenValue);
            else
                cmbOrigen2.SelectedIndex = -1;

            txtNoReferencia2.Text = referencia_value?.ToString() ?? "";
            dateTimePicker1.Value = fecha_value != null && DateTime.TryParse(fecha_value.ToString(), out DateTime fecha)
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id_transaccion = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id_Transaccion"].Value);


                Partidas_Dobles frm = new Partidas_Dobles(id_transaccion);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione una transacción antes de continuar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id_transaccion = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id_Transaccion"].Value);


                Partidas_Dobles frm = new Partidas_Dobles(id_transaccion);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione una transacción antes de continuar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrarNavegacion(string modulo)
        {
            try
            {
<<<<<<< HEAD

=======
                
>>>>>>> v
                clsCRUD_Historial historial = new clsCRUD_Historial();

                historial.RegistrarAccionUsuario(
                    Sesion1.usuario_id,
                    modulo,
                    "Navegación",
                    null,
                    $"Ingresó al módulo de {modulo}"
                );
            }
            catch { }
        }
<<<<<<< HEAD

        private void cmbInteresesBancarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInteresesBancarios.SelectedIndex == 0)
            {
                Intereses_Por_Cds intereses_Por_Cds = new();
                intereses_Por_Cds.Show();
                this.Hide();
            }
        }

        private void panelBancos2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlAlertaDeslizante_Paint(object sender, PaintEventArgs e)
        {

        }
<<<<<<< HEAD

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
=======
>>>>>>> v
=======
>>>>>>> parent of eebe7b3 (ALERTA FINALIZADA)
    }

}