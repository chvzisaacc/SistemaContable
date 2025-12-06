using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Diego;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_procesamiento_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Media;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_42 : Form, ICierreSesionHandler
    {
        /// <summary>
        /// The dt datos ingresos
        /// </summary>
        private DataTable dtDatosIngresos = new DataTable("Ingresos");
        /// <summary>
        /// The dt datos gastos
        /// </summary>
        private DataTable dtDatosGastos = new DataTable("Gastos");
        /// <summary>
        /// The crud catalo cuentas
        /// </summary>
        private clsCRUD_CatalogoCuentas crudCataloCuentas;

        /// <summary>
        /// The crud cuentas bancarias
        /// </summary>
        private ClsCRUD_CuentasBancarias crudCuentasBancarias;

        /// <summary>
        /// The modo edicion
        /// </summary>
        private bool modoEdicion = false;
        /// <summary>
        /// The cuenta bancoid seleccionado
        /// </summary>
        private int cuentaBancoid_seleccionado = 0;
        /// <summary>
        /// The subcuentas
        /// </summary>
        private AutoCompleteStringCollection Subcuentas = new AutoCompleteStringCollection();
        /// <summary>
        /// The object sub cuentas
        /// </summary>
        private ClsAccionesDB objSubCuentas = new ClsAccionesDB();

        /// <summary>
        /// The crud historial
        /// </summary>
        private clsCRUD_Historial crudHistorial;

        /// <summary>
        /// The objalerta
        /// </summary>
        private readonly Alerta _objalerta = new();
        /// <summary>
        /// The controlador alerta
        /// </summary>
        private readonly ControlarAlerta _controladorAlerta;
        /// <summary>
        /// The player
        /// </summary>
        private readonly SoundPlayer _player = new SoundPlayer();

        /// <summary>
        /// The animation timer
        /// </summary>
        private System.Windows.Forms.Timer _animationTimer;
        /// <summary>
        /// The target height
        /// </summary>
        private const int _TARGET_HEIGHT = 40; // Altura final deseada del panel (ajustar si es necesario)
        /// <summary>
        /// The slide speed
        /// </summary>
        private const int _SLIDE_SPEED = 5;    // Velocidad de la animación 
        /// <summary>
        /// The is opening
        /// </summary>
        private bool _isOpening = false;       // Indica si la alerta se está abriendo o cerrando

        /// <summary>
        /// Gets or sets the predicted identifier.
        /// </summary>
        /// <value>
        /// The predicted identifier.
        /// </value>
        private int PredictedId { get; set; }
        /// <summary>
        /// Gets or sets the parroquia identifier.
        /// </summary>
        /// <value>
        /// The parroquia identifier.
        /// </value>
        private int ParroquiaId { get; set; }

        /// <summary>
        /// The cerrar
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();
        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_42"/> class.
        /// </summary>
        /// <param name="predicted_id">The predicted identifier.</param>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        public FRM_42(int predicted_id, int parroquia_id)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            PredictedId = predicted_id;
            ParroquiaId = parroquia_id;

            InicializarDGVIngr();
            InicializarDGVgastos();
            CargarDatosAutocompletado();
            CargarDatosAutocompletadoGastos();
            Transacciones obj_transa = new();
            obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2);


            crudCataloCuentas = new clsCRUD_CatalogoCuentas();

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();

            crudHistorial = new clsCRUD_Historial();

            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            //panelContenedor.Controls.Add(panelMensaje);

            // muestra uno por defecto
            MostrarSoloEstePanel(panel1);

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

        public FRM_42()
        {
        }

        public void ManejarCierreSesion()
        {
            // Lógica NORMAL de cierre de sesión (solo se ejecuta si el usuario realmente cerró)
            this.Hide();
            using (var login = new FRM_PG1())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the FRM_42 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_42_Load(object sender, EventArgs e)
        {
            dgvGastos.AllowUserToAddRows = false;
            DateTime mes_actual = DateTime.Now;
            DateTime mes_actual1 = new DateTime(mes_actual.Year, mes_actual.Month, 1);
            dtpFecha.MinDate = mes_actual1;
            dtpFecha.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);
            dateTimePicker2.MinDate = mes_actual1;
            dateTimePicker2.MaxDate = DateTime.Today.AddDays(1).AddTicks(-1);

            if (dtpFecha.Value < dtpFecha.MinDate || dtpFecha.Value > dtpFecha.MaxDate)
                dtpFecha.Value = DateTime.Today;

            if (dateTimePicker2.Value < dateTimePicker2.MinDate || dateTimePicker2.Value > dateTimePicker2.MaxDate)
                dateTimePicker2.Value = DateTime.Today;

            ActualizarSaldo();
            CargarCuentasEnComboBox();
            _controladorAlerta.ForzarVerificacionInmediata();

            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 10;
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();



        }

        /// <summary>
        /// Manejars the cambio estado alerta.
        /// </summary>
        /// <param name="activo">if set to <c>true</c> [activo].</param>
        /// <param name="mensaje">The mensaje.</param>
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
                _animationTimer = new System.Windows.Forms.Timer();
                _animationTimer.Start();
            }
        }

        /// <summary>
        /// Handles the Tick event of the AnimationTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// <summary>
        /// Handles the FormClosing event of the Frm_42 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void Frm_42_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controladorAlerta.DetenerMonitoreo();
        }

        /// <summary>
        /// Cargars the cuentas en ComboBox.
        /// </summary>
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



        //Detener el Timer al cerrar el formulario para liberar recursos


        /// <summary>
        /// Cargars the origenes.
        /// </summary>
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

        /// <summary>
        /// Cargars the datos.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Cargars the combo boxes.
        /// </summary>
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




        /// <summary>
        /// Mostrars the solo este panel.
        /// </summary>
        /// <param name="panel_mostrar">The panel mostrar.</param>
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



        /// <summary>
        /// Handles the Click event of the btnGastos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGastos_Click(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelGastos2);
            RegistrarNavegacion("Gastos");
        }

        /// <summary>
        /// Handles the Click event of the btnCajaChica control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCajaChica2);
            RegistrarNavegacion("Caja Chica");

        }
        /// <summary>
        /// Handles the 1 event of the btnIngresos_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelIngresos);
            RegistrarNavegacion("Ingresos");
        }

        /// <summary>
        /// Handles the 1 event of the btnBancos_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            //panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelBancos2);
            RegistrarNavegacion("Bancos");
        }







        /// <summary>
        /// Handles the Click event of the pictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS();
            popup.StartPosition = FormStartPosition.Manual;
            var button_screen_position = pictureBox1.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(button_screen_position.X, button_screen_position.Y + pictureBox1.Height);
            popup.ShowDialog(this);
        }



        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Cerrar_Sesión popup = new Cerrar_Sesión();
            var btnPos = pictureBox2.PointToScreen(Point.Empty);

            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(btnPos.X, btnPos.Y + pictureBox2.Height);

            if (popup.ShowDialog() == DialogResult.OK)
            {
                ManejarCierreSesion();
            }
        }



        /// <summary>
        /// Handles the Click event of the btnDetalle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            using (var frm = new Partidas_Dobles())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbCuentas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {


            // Si no hay nada seleccionado, no hace nada
            if (cmbCuentas.SelectedIndex < 0 || cmbCuentas.SelectedValue == null)
            {
                return;
            }


            int id_seleccionado = Convert.ToInt32(cmbCuentas.SelectedValue);


            var frm = new BancosCuentaAhorro(id_seleccionado)
            {
                StartPosition = FormStartPosition.Manual,

                Location = new Point(414, 101)
            };

            frm.ShowDialog(this);
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbAcciones control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the btnDetalle_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDetalle_Click_1(object sender, EventArgs e)
        {
            //llamar form 69,
            Partidas_Dobles obj_frm69 = new Partidas_Dobles();
            obj_frm69.ShowDialog();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkSaldoInicial control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// <summary>
        /// Mostrars the saldo actual.
        /// </summary>
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
        /// <summary>
        /// Actualizars the saldo.
        /// </summary>
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
        /// <summary>
        /// Handles the TextChanged event of the txtSaldoActual control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtSaldoActual_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbOrigen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            cmbOrigen.Text = "Seleccionar";
            txtNoReferencia.Text = null;
            Transacciones transa = new();
            transa.Agregarfila(dtDatosIngresos, dataGridView1);


        }
        /// <summary>
        /// Handles the CellContentClick event of the dgvIngresos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvIngresos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handles the CellValidating event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }
        /// <summary>
        /// Inicializars the DGV ingr.
        /// </summary>
        private void InicializarDGVIngr()
        {
            dataGridView1.Columns.Clear();
            dtDatosIngresos = new DataTable("Ingresos");
            dtDatosIngresos.Columns.Add("Id_transaccion", typeof(int));
            dtDatosIngresos.Columns.Add("Id_Origen", typeof(int));
            dtDatosIngresos.Columns.Add("NombreCuenta", typeof(string));
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

        /// <summary>
        /// Inicializars the dg vgastos.
        /// </summary>
        private void InicializarDGVgastos()
        {
            dgvGastos.Columns.Clear();
            dtDatosGastos = new DataTable("Gastos");
            dtDatosGastos.Columns.Add("Id_transaccion", typeof(int));
            dtDatosGastos.Columns.Add("Id_Origen", typeof(int));
            dtDatosGastos.Columns.Add("NombreCuenta", typeof(string));
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


        /// <summary>
        /// Handles the CellClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Transacciones obj_transa = new();
            obj_transa.BloquearDesbloquearDataIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);


        }

        /// <summary>
        /// Handles the Paint event of the panelIngresos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panelIngresos_Paint(object sender, PaintEventArgs e)
        {

        }


        /// <summary>
        /// Cargars the datos autocompletado.
        /// </summary>
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
        /// <summary>
        /// Cargars the datos autocompletado gastos.
        /// </summary>
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




        /// <summary>
        /// Handles the EditingControlShowing event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "NombreCuenta")
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

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            using (var frm = new Certificados_De_Depósito_User
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(430, 450)
            })
            {
                DialogResult result = frm.ShowDialog();

            }
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbOrigen2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbOrigen2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia2.Text = null;
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        /// <summary>
        /// Handles the 1 event of the button4_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia2.Clear();
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the dgvGastos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void dgvGastos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvGastos.CurrentCell == null)
                return;

            // Verifica por nombre de columna, no por índice
            if (dgvGastos.CurrentCell.OwningColumn.Name == "NombreCuenta")
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


        private bool ValidarPanelGastos()
        {
            try
            {
                // 1. Validar que el ComboBox de Origen esté seleccionado
                if (cmbOrigen2.SelectedIndex == -1 || cmbOrigen2.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar una cuenta de origen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbOrigen2.Focus();
                    return false;
                }

                // 2. Validar que la fecha esté seleccionada
                if (dateTimePicker2.Value == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePicker2.Focus();
                    return false;
                }

                // 3. Validar que el número de referencia no esté vacío
                if (string.IsNullOrWhiteSpace(txtNoReferencia2.Text))
                {
                    MessageBox.Show("Debe ingresar un número de referencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNoReferencia2.Focus();
                    return false;
                }

                // 4. Validar que haya al menos una fila en el DataGridView
                if (dgvGastos.Rows.Count == 0 || (dgvGastos.Rows.Count == 1 && dgvGastos.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Debe agregar al menos una cuenta con su monto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // 5. Obtener el saldo disponible de la cuenta origen seleccionada
                decimal saldoDisponible = 0;
                if (cmbOrigen2.SelectedValue != null)
                {
                    int idOrigen = Convert.ToInt32(cmbOrigen2.SelectedValue);
                    DataTable dtCuenta = crudCuentasBancarias.ObtenerCuentasBancarias();
                    DataRow[] rows = dtCuenta.Select($"Id_Origen = {idOrigen}");

                    if (rows.Length > 0 && rows[0]["saldo"] != DBNull.Value)
                    {
                        saldoDisponible = Convert.ToDecimal(rows[0]["saldo"]);
                    }
                }

                // 6. Validar cada fila del DataGridView y sumar los montos
                decimal totalGastos = 0;
                for (int i = 0; i < dgvGastos.Rows.Count; i++)
                {
                    if (dgvGastos.Rows[i].IsNewRow) continue;

                    // Validar que la columna NombreCuenta no esté vacía
                    if (dgvGastos.Rows[i].Cells["NombreCuenta"].Value == null ||
                        string.IsNullOrWhiteSpace(dgvGastos.Rows[i].Cells["NombreCuenta"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un nombre de cuenta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CurrentCell = dgvGastos.Rows[i].Cells["NombreCuenta"];
                        dgvGastos.BeginEdit(true);
                        return false;
                    }

                    // Validar que la columna Detalle no esté vacía
                    if (dgvGastos.Rows[i].Cells["Detalle"].Value == null ||
                        string.IsNullOrWhiteSpace(dgvGastos.Rows[i].Cells["Detalle"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un detalle.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CurrentCell = dgvGastos.Rows[i].Cells["Detalle"];
                        dgvGastos.BeginEdit(true);
                        return false;
                    }

                    // Validar que la columna Saldo (monto) no esté vacía y sea válida
                    if (dgvGastos.Rows[i].Cells["Saldo"].Value == null ||
                        string.IsNullOrWhiteSpace(dgvGastos.Rows[i].Cells["Saldo"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CurrentCell = dgvGastos.Rows[i].Cells["Saldo"];
                        dgvGastos.BeginEdit(true);
                        return false;
                    }

                    decimal monto = 0;
                    if (!decimal.TryParse(dgvGastos.Rows[i].Cells["Saldo"].Value.ToString(), out monto))
                    {
                        MessageBox.Show($"El monto en la fila {i + 1} no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CurrentCell = dgvGastos.Rows[i].Cells["Saldo"];
                        dgvGastos.BeginEdit(true);
                        return false;
                    }

                    if (monto <= 0)
                    {
                        MessageBox.Show($"El monto en la fila {i + 1} debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CurrentCell = dgvGastos.Rows[i].Cells["Saldo"];
                        dgvGastos.BeginEdit(true);
                        return false;
                    }

                    totalGastos += monto;
                }

                // 7. Validar que el total de gastos no exceda el saldo disponible
                if (totalGastos > saldoDisponible)
                {
                    MessageBox.Show($"El total de gastos ({totalGastos:C2}) excede el saldo disponible ({saldoDisponible:C2}) en la cuenta de origen.\n\n" +
                                  $"Saldo insuficiente para realizar esta transacción.",
                                  "Saldo Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // VALIDACIÓN PARA EL PANEL DE INGRESOS
        private bool ValidarPanelIngresos()
        {
            try
            {
                // 1. Validar que el ComboBox de Origen esté seleccionado
                if (cmbOrigen.SelectedIndex == -1 || cmbOrigen.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar una cuenta de origen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbOrigen.Focus();
                    return false;
                }

                // 2. Validar que la fecha esté seleccionada
                if (dtpFecha.Value == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFecha.Focus();
                    return false;
                }

                // 3. Validar que el número de referencia no esté vacío
                if (string.IsNullOrWhiteSpace(txtNoReferencia.Text))
                {
                    MessageBox.Show("Debe ingresar un número de referencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNoReferencia.Focus();
                    return false;
                }

                // 4. Validar que haya al menos una fila en el DataGridView
                if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Debe agregar al menos una cuenta con su monto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // 5. Validar cada fila del DataGridView
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].IsNewRow) continue;

                    // Validar que la columna NombreCuenta no esté vacía
                    if (dataGridView1.Rows[i].Cells["NombreCuenta"].Value == null ||
                        string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["NombreCuenta"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un nombre de cuenta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["NombreCuenta"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    // Validar que la columna Detalle no esté vacía
                    if (dataGridView1.Rows[i].Cells["Detalle"].Value == null ||
                        string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["Detalle"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un detalle.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Detalle"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    // Validar que la columna Saldo (monto) no esté vacía y sea válida
                    if (dataGridView1.Rows[i].Cells["Saldo"].Value == null ||
                        string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["Saldo"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Saldo"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    decimal monto = 0;
                    if (!decimal.TryParse(dataGridView1.Rows[i].Cells["Saldo"].Value.ToString(), out monto))
                    {
                        MessageBox.Show($"El monto en la fila {i + 1} no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Saldo"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    if (monto <= 0)
                    {
                        MessageBox.Show($"El monto en la fila {i + 1} debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Saldo"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //-------------------------------------------------


        /// <summary>
        /// Handles the 1 event of the button1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!ValidarPanelIngresos())
            {
                return; // Si la validación falla, no continúa
            }

            try
            {
                // Asegura que el DataGridView esté editable al inicio, si no lo estaba.
                dataGridView1.ReadOnly = false;

                if (modoEdicion)
                {

                    if (dataGridView1.CurrentRow == null)
                    {
                        MessageBox.Show("Seleccione una fila para guardar la edición.", "Advertencia",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Corrige posible celda invisible
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
                    if (!decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out saldo) || saldo <= 0)
                    {
                        MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

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

                    // Llamar al método de edición
                    IngresosIn editar = new IngresosIn();
                    bool actualizado = editar.GuardarEdicion(dtDatosIngresos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                                             txtNoReferencia, cmbOrigen, dataGridView1, dtpFecha);

                    if (actualizado)
                    {
                        modoEdicion = false;

                        // Asegura que todas las columnas sean editables después del guardado
                        dataGridView1.ReadOnly = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            col.ReadOnly = false;

                        MessageBox.Show("Transacción editada correctamente.", "Éxito",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la transacción. Verifique los datos o la conexión.",
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

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
                                if (celda.OwningColumn.Name != "Id_transaccion" && celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
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

                    // GUARDAR NUEVO INGRESO
                    Ingresos ingresos = new();
                    bool error_guardado = false;
                    int filas_guardadas = 0;

                    try
                    {
                        ClsValidaciones validar = new();
                        DateTime fecha_transaccion = dtpFecha.Value;
                        string referencia_texto = txtNoReferencia.Text.Trim();
                        int referencia = 0;
                        string saldo_texto = fila_nueva.Cells["Saldo"].Value?.ToString() ?? "";
                        decimal saldo = 0;
                        int.TryParse(referencia_texto, out referencia);

                        string montoParaConversion = saldo_texto.Replace(',', '.');
                        decimal.TryParse(montoParaConversion,
                         System.Globalization.NumberStyles.Any,
                         System.Globalization.CultureInfo.InvariantCulture,
                         out saldo);

                        if (!validar.EsMontoPositivo(saldo_texto))
                        {
                            MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                             MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            dataGridView1.CancelEdit();
                            return;
                        }

                        if (!validar.EsMontoDentroDelRango(saldo_texto))
                        {
                            MessageBox.Show("El saldo no puede ser mayor a 100,000,000.", "Advertencia",
                                             MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            dataGridView1.ReadOnly = false;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                col.ReadOnly = false;
                            }

                            dataGridView1.CancelEdit();
                            return;
                        }

                        string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;
                        string nombre_cuenta = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? string.Empty;

                        // Guardar en la base de datos
                        int nuevo_id = ingresos.IngresarIngresos(fecha_transaccion, descripcion, saldo, referencia,
                                                                 Sesion1.usuario_id, id_origenNuevo, nombre_cuenta);

                        if (nuevo_id <= 0)
                        {
                            MessageBox.Show("No se recibió un ID válido desde la base de datos. Verifique el SP.",
                                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        // Asegura que todas las columnas sean editables después del guardado
                        dataGridView1.ReadOnly = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            col.ReadOnly = false;

                        // código para limpiar y refrescar
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
            }
            finally
            {
                // El bloque finally se ejecuta SIEMPRE, asegurando la limpieza final
                _controladorAlerta.ForzarVerificacionInmediata();
            }
        }









        /// <summary>
        /// Handles the 1 event of the cmbOrigen2_SelectedIndexChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbOrigen2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Paint event of the panelGastos2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panelGastos2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the ValueChanged event of the dateTimePicker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the CellClick event of the dgvGastos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Transacciones obj_transa = new();
            obj_transa.BloquearDesbloquearDataGastos(dtDatosGastos, dgvGastos, e.RowIndex);

        }

        /// <summary>
        /// Handles the CellContentClick event of the dgvGastos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// The identifier transaccion a editar
        /// </summary>
        private int id_transaccionAEditar = 0;


        /// <summary>
        /// Handles the Click event of the pictureBox5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the CellEndEdit event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //IngresosIn ing = new();
            //ing.BloquearDesbloquearIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
        }

        /// <summary>
        /// Handles the Click event of the btnGuardar2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            if (!ValidarPanelGastos())
            {
                return; // Si la validación falla, no continúa
            }

            try
            {
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

                    string nombre_cuenta = fila.Cells["NombreCuenta"].Value?.ToString() ?? "";
                    string detalle = fila.Cells["Detalle"].Value?.ToString() ?? "";
                    decimal saldo = 0;
                    decimal.TryParse(fila.Cells["Saldo"].Value?.ToString(), out saldo);
                    DateTime fecha_tr = dateTimePicker2.Value;
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
                                        txtNoReferencia2, cmbOrigen2, dgvGastos, dateTimePicker2);

                    if (actualizado)
                    {
                        modoEdicion = false;
                        dgvGastos.ReadOnly = false;
                        foreach (DataGridViewColumn col in dgvGastos.Columns)
                            col.ReadOnly = true;

                        //Actualiza el Id_Origen en la fila actual (para que el lápiz lea el correcto)
                        if (dgvGastos.CurrentRow != null)
                            dgvGastos.CurrentRow.Cells["Id_Origen"].Value = id_origen;

                        txtNoReferencia.Clear();
                        dateTimePicker2.Value = DateTime.Now;

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
                        _controladorAlerta.ForzarVerificacionInmediata();
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
                    ClsValidaciones validar = new();
                    DateTime fecha_transaccion = dtpFecha.Value;
                    string referencia_texto = txtNoReferencia.Text.Trim();
                    int referencia = 0;
                    string saldo_texto = fila_nueva.Cells["Saldo"].Value?.ToString() ?? "";
                    decimal saldo = 0;
                    int.TryParse(referencia_texto, out referencia);

                    string montoParaConversion = saldo_texto.Replace(',', '.');
                    decimal.TryParse(montoParaConversion,
                     System.Globalization.NumberStyles.Any,
                     System.Globalization.CultureInfo.InvariantCulture,
                     out saldo);

                    int.TryParse(referencia_texto, out referencia);

                    int id_usuario = Sesion1.usuario_id;
                    string nombre_cuenta = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? string.Empty;
                    string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                    if (!validar.EsMontoPositivo(saldo_texto))
                    {
                        MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        dgvGastos.CancelEdit();
                        return;
                    }

                    if (!validar.EsMontoDentroDelRango(saldo_texto))
                    {
                        MessageBox.Show("El saldo no puede ser mayor a 100,000,000.", "Advertencia",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        dgvGastos.CancelEdit();
                        return;
                    }

                    int nuevo_id = gasto.IngresarGastos(fecha_transaccion, descripcion, saldo, referencia, id_usuario, id_origenNuevo, nombre_cuenta);
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
                    dateTimePicker2.Value = DateTime.Now;
                }
                else if (error_guardado)
                {
                    MessageBox.Show("No se guardó la transacción debido a un error.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                _controladorAlerta.ForzarVerificacionInmediata();
            }


        }

        /// <summary>
        /// Handles the ValueChanged event of the dtpFecha control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pictureBox7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            dateTimePicker2.Value = fecha_value != null && DateTime.TryParse(fecha_value.ToString(), out DateTime fecha)
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

        /// <summary>
        /// Handles the Click event of the pictureBox6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (dgvGastos.SelectedRows.Count > 0)
            {
                int id_transaccion = Convert.ToInt32(dgvGastos.SelectedRows[0].Cells["Id_Transaccion"].Value);



                Partidas_Dobles frm = new Partidas_Dobles(id_transaccion);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione una transacción antes de continuar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the pictureBox8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Registrars the navegacion.
        /// </summary>
        /// <param name="modulo">The modulo.</param>
        private void RegistrarNavegacion(string modulo)
        {
            try
            {

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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbInteresesBancarios control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbInteresesBancarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Paint event of the panelBancos2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panelBancos2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Paint event of the pnlAlertaDeslizante control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void pnlAlertaDeslizante_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }
    }

}