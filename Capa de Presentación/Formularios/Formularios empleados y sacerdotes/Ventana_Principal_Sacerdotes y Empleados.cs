csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\Ventana_Principal_Sacerdotes y Empleados.cs
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
    /// Formulario principal para sacerdotes y empleados.
    /// Contiene control de transacciones (ingresos/gastos), navegación a módulos y mecanismos de sincronización/alertas.
    /// Nota: las operaciones que modifican controles UI deben ejecutarse en el hilo de interfaz; si se realiza trabajo en background,
    /// marshalear resultados al UI usando Invoke/BeginInvoke.
    /// </summary>
    public partial class FRM_42 : Form, ICierreSesionHandler
    {
        /// <summary>
        /// DataTable para acumulado de ingresos mostrado en grid.
        /// </summary>
        private DataTable dtDatosIngresos = new DataTable("Ingresos");

        /// <summary>
        /// DataTable para acumulado de gastos mostrado en grid.
        /// </summary>
        private DataTable dtDatosGastos = new DataTable("Gastos");

        /// <summary>
        /// Repositorio para catálogo de cuentas.
        /// </summary>
        private clsCRUD_CatalogoCuentas crudCataloCuentas;

        /// <summary>
        /// Repositorio para cuentas bancarias.
        /// </summary>
        private ClsCRUD_CuentasBancarias crudCuentasBancarias;

        /// <summary>
        /// Indicador de modo edición general.
        /// </summary>
        private bool modoEdicion = false;
        private bool _modoEdicionManual = false;

        /// <summary>
        /// Colección de autocompletado para subcuentas de ingresos.
        /// </summary>
        private AutoCompleteStringCollection SubcuentasIngresos = new AutoCompleteStringCollection();

        /// <summary>
        /// Colección de autocompletado para subcuentas de gastos.
        /// </summary>
        private AutoCompleteStringCollection SubcuentasGastos = new AutoCompleteStringCollection();

        /// <summary>
        /// Utilidad para acciones relacionadas con subcuentas en BD.
        /// </summary>
        private ClsAccionesDB objSubCuentas = new ClsAccionesDB();

        /// <summary>
        /// Repositorio para historial de acciones.
        /// </summary>
        private clsCRUD_Historial crudHistorial;

        /// <summary>
        /// Objeto de alerta visual y controlador asociado.
        /// </summary>
        private readonly Alerta _objalerta = new();
        private readonly ControlarAlerta _controladorAlerta;

        /// <summary>
        /// Reproductor de sonido para alertas.
        /// </summary>
        private readonly SoundPlayer _player = new SoundPlayer();

        /// <summary>
        /// Timer usado para animación del panel de alerta deslizante.
        /// </summary>
        private System.Windows.Forms.Timer _animationTimer;

        /// <summary>
        /// Altura objetivo del panel deslizante (px).
        /// </summary>
        private const int _TARGET_HEIGHT = 40;

        /// <summary>
        /// Velocidad de deslizamiento del panel (px por tick).
        /// </summary>
        private const int _SLIDE_SPEED = 5;

        /// <summary>
        /// Indica si la alerta se está abriendo (true) o cerrando (false).
        /// </summary>
        private bool _isOpening = false;

        /// <summary>
        /// Id previsto / usuario actual.
        /// </summary>
        private int PredictedId { get; set; }
        private int ParroquiaId { get; set; }
        public int UsuarioId { get; private set; }

        private readonly clsCRUD_Usuarios _repo = new clsCRUD_Usuarios();
        bool EsModificacionCapital;

        ClsCerrar cerrar = new ClsCerrar();

        /// <summary>
        /// Constructor del formulario principal.
        /// Inicializa UI, controladores, grids y arranca el monitoreo de alertas.
        /// Nota: muchas inicializaciones tocan controles UI; deben ejecutarse en el hilo de interfaz.
        /// </summary>
        /// <param name="predicted_id">Identificador de usuario.</param>
        /// <param name="parroquia_id">Identificador de parroquia.</param>
        public FRM_42(int predicted_id, int parroquia_id)
        {
            InitializeComponent();
            this.Shown += (_, __) => CargarNombre();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            PredictedId = predicted_id;
            ParroquiaId = parroquia_id;
            UsuarioId = predicted_id;
            EsModificacionCapital = false;

            ConfigurarCapitalInicial();

            InicializarDGVIngr();
            InicializarDGVgastos();
            CargarDatosAutocompletado();
            CargarDatosAutocompletadoGastos();
            Transacciones obj_transa = new();
            obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);

            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dgvGastos.CellValidating += dgvGastos_CellValidating;

            crudCataloCuentas = new clsCRUD_CatalogoCuentas();
            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();
            crudHistorial = new clsCRUD_Historial();

            // Agrega paneles al contenedor y muestra el por defecto
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            MostrarSoloEstePanel(panel1);

            _controladorAlerta = new ControlarAlerta(_objalerta);
            _controladorAlerta.OnAlertaStateChanged += ManejarCambioEstadoAlerta;
            _controladorAlerta.IniciarMonitoreo(intervaloMinutos: 5);

            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 10;
            _animationTimer.Tick += AnimationTimer_Tick;

            pnlAlertaDeslizante.Height = 0;
            pnlAlertaDeslizante.Visible = true;

            InicializarIndicadorConexion();
        }

        /// <summary>
        /// Manejador del cierre de sesión (invocado al confirmar logout).
        /// Presenta el formulario de login y determina si regresar al principal o salir de la aplicación.
        /// </summary>
        public void ManejarCierreSesion()
        {
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
        /// Load del formulario: configura rangos de fechas, timers y actualiza saldo inicial.
        /// Si algunas cargas son costosas deben ejecutarse en background y luego sincronizar UI.
        /// </summary>
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

            ClsValidaciones config = new ClsValidaciones();
            config.MaxlenghtDGV(dgvGastos);
            config.MaxlenghtDGV(dataGridView1);
            ClsCapitalInicial clsCapital = new ClsCapitalInicial();

            // Verificar capital inicial
            decimal capital = clsCapital.ObtenerCapitalInicial(PredictedId, ParroquiaId);

            if (capital > 0)
            {
                chkIngresarCapital.Visible = false;
                panelIngresarCapital.Visible = false;
                lblCapitalInicial.Visible = true;

                var culturaEEUU = new System.Globalization.CultureInfo("en-US");
                lblCapitalInicial.Text = capital.ToString("'L. ' #,##0.00", culturaEEUU);
            }
        }

        /// <summary>
        /// Maneja los cambios de estado de la alerta (activo/inactivo).
        /// Actualiza el panel deslizante y reproduce sonido si corresponde.
        /// Este método es seguro con respecto al hilo UI (usa Invoke si es necesario).
        /// </summary>
        private void ManejarCambioEstadoAlerta(bool activo, string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ManejarCambioEstadoAlerta(activo, mensaje)));
                return;
            }

            if (activo && pnlAlertaDeslizante.Height == 0)
            {
                lblAlertaMensaje.Text = mensaje;
                _isOpening = true;
                _animationTimer.Start();
                _player.Play();
            }
            else if (!activo && pnlAlertaDeslizante.Height > 0)
            {
                _isOpening = false;
                _animationTimer = new System.Windows.Forms.Timer();
                _animationTimer.Start();
            }
        }

        /// <summary>
        /// Carga y muestra el nombre del usuario en la etiqueta del formulario.
        /// Actualiza controles UI; deben ejecutarse en hilo UI.
        /// </summary>
        private void CargarNombre()
        {
            try
            {
                string nombre = _repo.ObtenerNombrePorUsuario(Sesion1.usuario_id);
                label1.Text = string.IsNullOrWhiteSpace(nombre)
                ? "Nombre no disponible / cuenta inactiva"
                : $"Bienvenido, {nombre}";
            }
            catch (Exception)
            {
                label1.Text = "Error obteniendo nombre";
            }
        }

        /// <summary>
        /// Tick del timer de animación: desplaza el panel de alerta (mostrar/ocultar).
        /// Ejecutado en hilo UI.
        /// </summary>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_isOpening)
            {
                if (pnlAlertaDeslizante.Height < _TARGET_HEIGHT)
                {
                    pnlAlertaDeslizante.Height += _SLIDE_SPEED;
                    if (pnlAlertaDeslizante.Height >= _TARGET_HEIGHT)
                    {
                        pnlAlertaDeslizante.Height = _TARGET_HEIGHT;
                        _animationTimer.Stop();
                    }
                }
            }
            else
            {
                if (pnlAlertaDeslizante.Height > 0)
                {
                    pnlAlertaDeslizante.Height -= _SLIDE_SPEED;
                    if (pnlAlertaDeslizante.Height <= 0)
                    {
                        pnlAlertaDeslizante.Height = 0;
                        _animationTimer.Stop();
                    }
                }
            }
        }

        /// <summary>
        /// FormClosing: detiene monitoreo y timers para liberar recursos.
        /// </summary>
        private void Frm_42_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controladorAlerta.DetenerMonitoreo();
            timerConexion?.Stop();
            timerConexion?.Dispose();
        }

        /// <summary>
        /// Carga las cuentas en el ComboBox de forma segura (desvincula y vuelve a vincular el evento).
        /// Indica cómo evitar disparos no deseados de SelectedIndexChanged.
        /// </summary>
        private void CargarCuentasEnComboBox()
        {
            try
            {
                cmbCuentas.SelectedIndexChanged -= cmbCuentas_SelectedIndexChanged;

                DataTable dt_cuentas = crudCuentasBancarias.ObtenerCuentasBancarias(ParroquiaId);
                cmbCuentas.DataSource = dt_cuentas;
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_Origen";
                cmbCuentas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            }
        }

        /// <summary>
        /// Muestra únicamente el panel indicado dentro del contenedor.
        /// </summary>
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
        /// Muestra el panel de gastos y registra la navegación.
        /// </summary>
        private void btnGastos_Click(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelGastos2);
            RegistrarNavegacion("Gastos");
        }

        /// <summary>
        /// Muestra el panel de caja chica y actualiza saldos.
        /// </summary>
        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelCajaChica2);
            RegistrarNavegacion("Caja Chica");
            ActualizarSaldo();
            lblCapitalInicial.Visible = false;
            label9.Visible = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
        }

        /// <summary>
        /// Muestra el panel de ingresos y registra la navegación.
        /// </summary>
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelIngresos);
            RegistrarNavegacion("Ingresos");
        }

        /// <summary>
        /// Muestra el panel de bancos y registra la navegación.
        /// </summary>
        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelBancos2);
            RegistrarNavegacion("Bancos");
        }

        /// <summary>
        /// Abre un popup de servicios posicionado cerca del icono.
        /// Ejecutado en hilo UI.
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS();
            var btnPos = pictureBox1.PointToScreen(Point.Empty);
            int desplazamientoIzquierda = 450;

            popup.StartPosition = FormStartPosition.Manual;

            popup.Location = new Point(
                btnPos.X - desplazamientoIzquierda,
                btnPos.Y + pictureBox1.Height
            );

            popup.Load += (s, ev) => { popup.ActiveControl = null; };
            popup.ShowDialog(this);
        }

        /// <summary>
        /// Abre popup de cierre de sesión posicionado; si confirma, maneja cierre de sesión.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Cerrar_Sesión popup = new Cerrar_Sesión();
            var btnPos = pictureBox2.PointToScreen(Point.Empty);
            int desplazamientoIzquierda = 450;
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(
                btnPos.X - desplazamientoIzquierda,
                btnPos.Y + pictureBox2.Height
            );

            if (popup.ShowDialog() == DialogResult.OK)
            {
                ManejarCierreSesion();
            }
        }

        /// <summary>
        /// Abre ventana de detalle de partidas dobles.
        /// </summary>
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            using (var frm = new Partidas_Dobles())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Se dispara al cambiar la cuenta seleccionada; abre formulario relacionado.
        /// </summary>
        private void cmbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCuentas.SelectedIndex < 0 || cmbCuentas.SelectedValue == null)
                return;

            int id_seleccionado = System.Convert.ToInt32(cmbCuentas.SelectedValue);

            var frm = new BancosCuentaAhorro(ParroquiaId)
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(414, 101)
            };

            frm.ShowDialog(this);
        }

        /// <summary>
        /// Manejador para acciones del combo de acciones (abrir