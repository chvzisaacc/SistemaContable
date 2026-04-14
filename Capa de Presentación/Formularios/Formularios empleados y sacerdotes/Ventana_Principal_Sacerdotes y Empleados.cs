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
        /// Manejador para acciones del combo de acciones (abrir formularios relacionados).
        /// Describe cada caso y su efecto (abrir modal, recargar combos, actualizar saldo).
        /// </summary>
        private void cmbAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcciones.SelectedIndex < 0) return;
            Transacciones transacciones_obj = new();

            switch (cmbAcciones.SelectedIndex)
            {
                case 0: // Agregar Saldo
                    {
                        using (var frm = new BancosAgregarSaldo(ParroquiaId, UsuarioId)
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
                            }
                        }
                    }
                    break;
                case 1: // Transferencia entre cuentas
                    {
                        using (var frm = new BancosTransferenciaEntreCuentas(ParroquiaId, 0, PredictedId)
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
                            }
                        }
                    }
                    break;
                case 2: // Agregar cuenta bancaria
                    {
                        using (var frm = new BancosAgregarCuentaBancaria(ParroquiaId)
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
                                CargarCuentasEnComboBox();
                            }
                        }
                    }
                    break;
                case 3: // Retirar dinero
                    {
                        using (var frm = new BancosRetirarDinero(ParroquiaId, PredictedId)
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        })
                        {
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
                            }
                        }
                    }
                    break;
                case 4: // Envío caja chica a banco
                    {
                        clsTransferenciaEntreCuentas crud = new clsTransferenciaEntreCuentas();
                        DataTable dt_caja = crud.ObtenerCajaChica(ParroquiaId);

                        if (dt_caja.Rows.Count == 0)
                        {
                            MessageBox.Show("No existe una Caja Chica.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int idCajaChica = System.Convert.ToInt32(dt_caja.Rows[0]["Id_Origen"]);

                        ActualizarSaldo();

                        using (var frm = new BancosTransferenciaEntreCuentas(ParroquiaId, idCajaChica, PredictedId))
                        {
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.Location = new Point(430, 450);
                            frm.SaldoTexto = this.txtSaldoActual.Text;
                            DialogResult result = frm.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                ActualizarSaldo();
                                transacciones_obj.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Placeholder Paint handler (sin lógica adicional).
        /// </summary>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Abre el formulario 69 de partidas dobles.
        /// </summary>
        private void btnDetalle_Click_1(object sender, EventArgs e)
        {
            Partidas_Dobles obj_frm69 = new Partidas_Dobles();
            obj_frm69.ShowDialog();
        }

        /// <summary>
        /// Actualiza mostradores de saldo, refresca combos y determina visibilidad de controles.
        /// Usa conversiones seguras y se ejecuta en hilo UI.
        /// </summary>
        private void ActualizarSaldo()
        {
            try
            {
                MostrarSaldoActual();

                txtSaldoActual.Invalidate();
                txtSaldoActual.Update();

                string textoSaldo = txtSaldoActual.Text;
                string soloNumeros = textoSaldo.Replace("L.", "").Replace("L", "").Replace(",", "").Trim();

                if (decimal.TryParse(soloNumeros, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal saldo))
                {
                    chkSaldoInicial.Visible = (saldo == 0);
                }

                Transacciones transacciones = new();
                transacciones.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el saldo: " + ex.Message);
            }
        }

        /// <summary>
        /// Manejador del checkbox que abre el diálogo para ingresar saldo inicial.
        /// Suscribe eventos para sincronizar la vista tras el cierre del diálogo.
        /// </summary>
        private void chkSaldoInicial_CheckedChanged(object sender, EventArgs e)
        {
            CajaChicaMonto obj_caja = new CajaChicaMonto(ParroquiaId, PredictedId);

            if (chkSaldoInicial.Checked)
            {
                obj_caja.SaldoActualizado += this.ActualizarSaldo;
                obj_caja.SaldoActualizado += () => MostrarCapitalInicial();

                obj_caja.CapitalIngresado += () =>
                {
                    panel4.Visible = false;
                    chkIngresarCapital.Visible = false;
                    chkSaldoInicial.Visible = false;
                    lblCapitalInicial.Visible = false;
                    MostrarCapitalInicial();

                    lblCapitalInicial.Visible = false;
                    label9.Visible = false;
                };

                obj_caja.ShowDialog();
                obj_caja.SaldoActualizado -= this.ActualizarSaldo;
            }

            ActualizarSaldo();

            string soloNumeros = txtSaldoActual.Text
                .Replace("L.", "")
                .Replace("L", "")
                .Replace(",", "")
                .Trim();

            if (decimal.TryParse(soloNumeros, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal saldo))
            {
                var culturaEEUU = new System.Globalization.CultureInfo("en-US");
                txtSaldoCaja.Text = saldo.ToString("'L. ' #,##0.00", culturaEEUU);
                chkSaldoInicial.Visible = (saldo == 0);

                Transacciones transacciones = new();
                transacciones.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);
            }
        }

        /// <summary>
        /// Muestra el saldo actual consultando el procedimiento almacenado 'saldo_actual'.
        /// Llama a la BD de forma síncrona; si se desea no bloquear UI, ejecutar en background y luego sincronizar.
        /// </summary>
        private void MostrarSaldoActual()
        {
            Clsconexion obj_con = new Clsconexion();

            try
            {
                obj_con.Abrir();

                string query = "exec saldo_actual @Parroquia_ID";
                SqlCommand comando = new SqlCommand(query, obj_con.sc);
                comando.Parameters.AddWithValue("@Parroquia_ID", this.ParroquiaId);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    decimal saldo = System.Convert.ToDecimal(lector["saldo"]);
                    var culturaEEUU = new System.Globalization.CultureInfo("en-US");
                    txtSaldoCaja.Text = saldo.ToString("'L. ' #,##0.00", culturaEEUU);
                }
                else
                {
                    txtSaldoCaja.Text = "L. 0.00";
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar saldo: " + ex.Message);
            }
            finally
            {
                obj_con.Cerrar();
            }
        }

        /// <summary>
        /// Limpia y agrega una fila de ingreso al grid.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            cmbOrigen.Text = "Seleccionar";
            txtNoReferencia.Text = null;
            Transacciones transa = new();
            transa.Agregarfila(dtDatosIngresos, dataGridView1);
        }

        /// <summary>
        /// Verifica si una cuenta existe en la BD (comparación estricta).
        /// </summary>
        private bool NombreCuentaExisteEnBD(string nombreIngresado, bool esIngreso)
        {
            ClsAccionesDB clsAcciones = new ClsAccionesDB();
            DataTable dt = esIngreso
                ? clsAcciones.ObtenerCuentasIngreso()
                : clsAcciones.ObtenerCuentasGastos();

            foreach (DataRow row in dt.Rows)
            {
                string valorBD = row["Subcuentas"].ToString();
                if (string.Equals(valorBD, nombreIngresado, StringComparison.Ordinal))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Validación de celdas al editar nombre de cuenta en el grid de ingresos.
        /// Cancela edición si el nombre no existe en catálogo.
        /// </summary>
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridView1.Rows[e.RowIndex].IsNewRow) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "NombreCuenta")
            {
                string valorIngresado = e.FormattedValue?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(valorIngresado)) return;

                if (!NombreCuentaExisteEnBD(valorIngresado, esIngreso: true))
                {
                    MessageBox.Show(
                        $"La cuenta \"{valorIngresado}\" no existe en el catálogo.\n" +
                        "Debe seleccionar un valor exacto de la lista (respetando mayúsculas y minúsculas).",
                        "Cuenta no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    e.Cancel = true;
                    dataGridView1.Rows[e.RowIndex].Cells["NombreCuenta"].Value = "";
                }
            }
        }

        /// <summary>
        /// Validación de celdas al editar nombre de cuenta en el grid de gastos.
        /// Cancela edición si el nombre no existe en catálogo.
        /// </summary>
        private void dgvGastos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvGastos.Rows[e.RowIndex].IsNewRow) return;

            if (dgvGastos.Columns[e.ColumnIndex].Name == "NombreCuenta")
            {
                string valorIngresado = e.FormattedValue?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(valorIngresado)) return;

                if (!NombreCuentaExisteEnBD(valorIngresado, esIngreso: false))
                {
                    MessageBox.Show(
                        $"La cuenta \"{valorIngresado}\" no existe en el catálogo.\n" +
                        "Debe seleccionar un valor exacto de la lista (respetando mayúsculas y minúsculas).",
                        "Cuenta no válida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    e.Cancel = true;
                    dgvGastos.Rows[e.RowIndex].Cells["NombreCuenta"].Value = "";
                }
            }
        }

        /// <summary>
        /// Inicializa estructura y columnas del DataGridView de ingresos.
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
            dtDatosIngresos.Columns.Add("id_cuenta_destino", typeof(int));

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
            if (dataGridView1.Columns.Contains("id_cuenta_destino"))
                dataGridView1.Columns["id_cuenta_destino"].Visible = false;
        }

        /// <summary>
        /// Inicializa estructura y columnas del DataGridView de gastos.
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
            dtDatosGastos.Columns.Add("id_cuenta_destino", typeof(int));

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
            if (dgvGastos.Columns.Contains("id_cuenta_destino"))
                dgvGastos.Columns["id_cuenta_destino"].Visible = false;
        }

        /// <summary>
        /// Manejo de clic en celdas de ingresos: bloquea/desbloquea fila según lógica.
        /// </summary>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (_modoEdicionManual)
                return;

            Transacciones obj_transa = new();
            obj_transa.BloquearDesbloquearDataIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
        }

        /// <summary>
        /// Carga datos para autocompletar nombres de subcuentas de ingresos.
        /// </summary>
        private void CargarDatosAutocompletado()
        {
            SubcuentasIngresos.Clear();
            try
            {
                ClsAccionesDB objTemp = new ClsAccionesDB();
                DataTable dt = objTemp.ObtenerCuentasIngreso();

                foreach (DataRow row in dt.Rows)
                    SubcuentasIngresos.Add(row["Subcuentas"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR REAL: " + ex.Message, "Error de Carga",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga datos para autocompletar nombres de subcuentas de gastos.
        /// </summary>
        private void CargarDatosAutocompletadoGastos()
        {
            SubcuentasGastos.Clear();
            try
            {
                DataTable dt = objSubCuentas.ObtenerCuentasGastos();
                foreach (DataRow row in dt.Rows)
                    SubcuentasGastos.Add(row["Subcuentas"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Configura autocompletado del control editor cuando se está editando el campo NombreCuenta (ingresos).
        /// </summary>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell?.OwningColumn.Name == "NombreCuenta")
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    auto_text.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    auto_text.AutoCompleteCustomSource = SubcuentasIngresos;
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
        /// Abre modal de Certificados de Depósito para usuario.
        /// </summary>
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
        /// Agrega fila en panel de gastos.
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia2.Text = null;
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        /// <summary>
        /// Variante para agregar fila de gastos (otro handler).
        /// </summary>
        private void button4_Click_1(object sender, EventArgs e)
        {
            cmbOrigen2.Text = "Seleccionar";
            txtNoReferencia2.Clear();
            Transacciones transa = new();
            transa.Agregarfila2(dtDatosGastos, dgvGastos);
        }

        /// <summary>
        /// Configura autocompletado para el editor del grid de gastos.
        /// </summary>
        private void dgvGastos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvGastos.CurrentCell == null) return;

            if (dgvGastos.CurrentCell.OwningColumn.Name == "NombreCuenta")
            {
                TextBox auto_text = e.Control as TextBox;
                if (auto_text != null)
                {
                    auto_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    auto_text.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    auto_text.AutoCompleteCustomSource = SubcuentasGastos;
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

        /// <summary>
        /// Valida el contenido del panel de gastos antes de guardar.
        /// </summary>
        private bool ValidarPanelGastos()
        {
            try
            {
                if (cmbOrigen2.SelectedIndex == -1 || cmbOrigen2.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar una cuenta de origen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbOrigen2.Focus();
                    return false;
                }

                if (dateTimePicker2.Value == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePicker2.Focus();
                    return false;
                }

                if (dgvGastos.Rows.Count == 0 || (dgvGastos.Rows.Count == 1 && dgvGastos.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Debe agregar al menos una cuenta con su monto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                decimal saldoDisponible = 0;
                if (cmbOrigen2.SelectedValue != null)
                {
                    int idOrigen = System.Convert.ToInt32(cmbOrigen2.SelectedValue);
                    DataTable dtCuenta = crudCuentasBancarias.ObtenerCuentasBancarias(ParroquiaId);
                    DataRow[] rows = dtCuenta.Select($"Id_Origen = {idOrigen}");

                    if (rows.Length > 0 && rows[0]["saldo"] != DBNull.Value)
                    {
                        saldoDisponible = System.Convert.ToDecimal(rows[0]["saldo"]);
                    }
                }

                dgvGastos.EndEdit();
                dgvGastos.CommitEdit(DataGridViewDataErrorContexts.Commit);

                DataGridViewRow filaActual = null;
                for (int i = dgvGastos.Rows.Count - 1; i >= 0; i--)
                {
                    if (!dgvGastos.Rows[i].IsNewRow)
                    {
                        filaActual = dgvGastos.Rows[i];
                        break;
                    }
                }

                if (filaActual == null)
                {
                    MessageBox.Show("Debe ingresar una fila válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                string nombreCuenta = filaActual.Cells["NombreCuenta"].Value?.ToString();
                string detalle = filaActual.Cells["Detalle"].Value?.ToString();
                string textoMonto = filaActual.Cells["Saldo"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(nombreCuenta))
                {
                    MessageBox.Show("Debe ingresar un nombre de cuenta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvGastos.CurrentCell = filaActual.Cells["NombreCuenta"];
                    return false;
                }

                if (string.IsNullOrWhiteSpace(detalle))
                {
                    MessageBox.Show("Debe ingresar un detalle.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvGastos.CurrentCell = filaActual.Cells["Detalle"];
                    return false;
                }

                decimal monto = 0;
                if (!decimal.TryParse(textoMonto, out monto))
                {
                    MessageBox.Show("Debe ingresar un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvGastos.CurrentCell = filaActual.Cells["Saldo"];
                    return false;
                }

                if (monto <= 0)
                {
                    MessageBox.Show("El monto debe ser mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvGastos.CurrentCell = filaActual.Cells["Saldo"];
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

        /// <summary>
        /// Valida el panel de ingresos antes de guardar.
        /// </summary>
        private bool ValidarPanelIngresos()
        {
            try
            {
                if (cmbOrigen.SelectedIndex == -1 || cmbOrigen.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar una cuenta de origen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbOrigen.Focus();
                    return false;
                }

                if (dtpFecha.Value == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFecha.Focus();
                    return false;
                }

                if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
                {
                    MessageBox.Show("Debe agregar al menos una cuenta con su monto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].IsNewRow) continue;

                    if (dataGridView1.Rows[i].Cells["NombreCuenta"].Value == null ||
                        string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["NombreCuenta"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un nombre de cuenta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["NombreCuenta"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    if (dataGridView1.Rows[i].Cells["Detalle"].Value == null ||
                        string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["Detalle"].Value.ToString()))
                    {
                        MessageBox.Show($"La fila {i + 1} debe tener un detalle.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Detalle"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    if (dataGridView1.Rows[i].Cells["Saldo"].Value == null ||
                        string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells["Saldo"].Value.ToString()))
                    {
                        MessageBox.Show($"El monto es requerido, solo puede contener numeros y debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["Saldo"];
                        dataGridView1.BeginEdit(true);
                        return false;
                    }

                    decimal monto = 0;
                    if (!decimal.TryParse(dataGridView1.Rows[i].Cells["Saldo"].Value.ToString(), out monto))
                    {
                        MessageBox.Show($"El monto  es requerido, solo puede contener numeros y debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        /// <summary>
        /// Guarda o edita ingresos (handler del botón guardar de ingresos).
        /// El método contiene lógica para edición y creación de registros; mantener la sincronización con UI.
        /// </summary>
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!ValidarPanelIngresos())
            {
                return;
            }

            dataGridView1.ReadOnly = false;

            try
            {
                dataGridView1.ReadOnly = false;

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
                    int id_origen = System.Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);

                    if (string.IsNullOrEmpty(nombre_cuenta))
                    {
                        MessageBox.Show("Debe ingresar el nombre de la cuenta.", "Advertencia",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    IngresosIn editar = new IngresosIn();
                    bool actualizado = editar.GuardarEdicion(dtDatosIngresos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                                             txtNoReferencia, cmbOrigen, dataGridView1, dtpFecha);

                    if (actualizado)
                    {
                        modoEdicion = false;

                        dataGridView1.ReadOnly = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            col.ReadOnly = false;
                        ActualizarSaldo();
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

                    int id_origenNuevo = System.Convert.ToInt32(cmbOrigen.SelectedValue ?? 0);
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

                    Ingresos ingresos = new();
                    bool error_guardado = false;
                    int filas_guardadas = 0;

                    try
                    {
                        ClsValidaciones validar = new();
                        DateTime fecha_transaccion = dtpFecha.Value;
                        string referencia_texto = txtNoReferencia.Text.Trim();
                        int referencia = 0;

                        this.Validate();

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

                        string nombre_cuenta_buscar_i = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? "";
                        int? id_cuenta_destino_ingreso = null;
                        if (!string.IsNullOrEmpty(nombre_cuenta_buscar_i))
                        {
                            int id_encontrado = crudCataloCuentas.BuscarIdCuentaPorNombre(nombre_cuenta_buscar_i);
                            if (id_encontrado > 0) id_cuenta_destino_ingreso = id_encontrado;
                        }

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
                        obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);

                        ActualizarSaldo();
                        _controladorAlerta.ForzarVerificacionInmediata();
                    }
                    catch (Exception ex)
                    {
                        error_guardado = true;
                        MessageBox.Show("Observación: " + ex.Message,
                                         "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (!error_guardado && filas_guardadas > 0)
                    {
                        modoEdicion = false;
                        dataGridView1.ReadOnly = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                            col.ReadOnly = false;

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
                _controladorAlerta.ForzarVerificacionInmediata();
            }
        }

        /// <summary>
        /// Paint placeholder para panel de gastos.
        /// </summary>
        private void panelGastos2_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Placeholder ValueChanged handler.
        /// </summary>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Maneja doble clic en filas de gastos para entrar en edición manual.
        /// </summary>
        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (_modoEdicionManual)
                return;

            Transacciones obj_transa = new();
            obj_transa.BloquearDesbloquearDataGastos(dtDatosGastos, dgvGastos, e.RowIndex);
        }

        /// <summary>
        /// Habilita modo edición para una fila seleccionada en ingresos.
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (modoEdicion)
            {
                MessageBox.Show("Ya estás en modo edición. Realiza los cambios y presiona GUARDAR.",
                                "Modo Edición Activo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione una fila válida para editar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object fechaValor = dataGridView1.CurrentRow.Cells["fecha_transaccion"].Value;
            if (fechaValor != null && fechaValor != DBNull.Value)
            {
                DateTime fechaCreacion = System.Convert.ToDateTime(fechaValor);
                double minutosTranscurridos = (DateTime.Now - fechaCreacion).TotalMinutes;

                if (minutosTranscurridos > 15)
                {
                    MessageBox.Show(
                        "No se puede editar este registro.\nHan pasado más de 15 minutos desde su creación.",
                        "Edición no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                DataGridViewRow fila = dataGridView1.CurrentRow;

                txtNoReferencia.Text = fila.Cells["NoReferencia"].Value?.ToString() ?? "";

                dataGridView1.ReadOnly = false;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.ReadOnly = false;
                    if (col.Name == "Id_transaccion" || col.Name == "Id_Origen")
                    {
                        col.ReadOnly = true;
                    }
                }

                dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                dataGridView1.MultiSelect = false;

                modoEdicion = true;

                dataGridView1.Focus();

                var primeraCelda = fila.Cells.Cast<DataGridViewCell>()
                                             .FirstOrDefault(c => c.Visible && !c.ReadOnly);

                if (primeraCelda != null)
                {
                    dataGridView1.CurrentCell = primeraCelda;
                    dataGridView1.BeginEdit(true);
                }

                MessageBox.Show("Modo edición activado en Ingresos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en Ingresos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Placeholder CellEndEdit (sin implementación adicional).
        /// </summary>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// Guarda o edita la fila seleccionada de gastos.
        /// Contiene lógica similar a la de ingresos y realiza sincronizaciones de UI cuando es necesario.
        /// </summary>
        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            _modoEdicionManual = false;
            if (!ValidarPanelGastos())
            {
                return;
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
                    int id_origen = System.Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);

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

                    GastosGa editar = new();
                    bool actualizado = editar.GuardarEdicion2(dtDatosGastos, nombre_cuenta, detalle, saldo, fecha_tr, referencia_texto, id_origen,
                                        txtNoReferencia2, cmbOrigen2, dgvGastos, dateTimePicker2);

                    if (actualizado)
                    {
                        modoEdicion = false;
                        BloquearFilasGuardadas(fila);
                        foreach (DataGridViewColumn col in dgvGastos.Columns)
                            col.ReadOnly = true;

                        txtNoReferencia.Clear();
                        dateTimePicker2.Value = DateTime.Now;

                        Transacciones obj_transa = new Transacciones();
                        obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);

                        this.BeginInvoke(new Action(() =>
                        {
                            cmbOrigen2.SelectedValue = id_origen;
                            cmbOrigen2.Refresh();
                        }));

                        ActualizarSaldo();

                        MessageBox.Show("Transacción editada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                int id_origenNuevo = System.Convert.ToInt32(cmbOrigen2.SelectedValue ?? 0);
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
                    int parroquia_id = Sesion1.id_parroquia;
                    string nombre_cuenta = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? string.Empty;
                    string descripcion = fila_nueva.Cells["Detalle"].Value?.ToString() ?? string.Empty;

                    if (!validar.EsMontoPositivo(saldo_texto))
                    {
                        MessageBox.Show("El monto debe ser mayor que cero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CancelEdit();
                        return;
                    }

                    if (!validar.EsMontoDentroDelRango(saldo_texto))
                    {
                        MessageBox.Show("El saldo no puede ser mayor a 100,000,000.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvGastos.CancelEdit();
                        return;
                    }

                    string nombre_cuenta_buscar_g = fila_nueva.Cells["NombreCuenta"].Value?.ToString() ?? "";
                    int? id_cuenta_destino_gasto = null;
                    if (!string.IsNullOrEmpty(nombre_cuenta_buscar_g))
                    {
                        int id_encontrado = crudCataloCuentas.BuscarIdCuentaPorNombre(nombre_cuenta_buscar_g);
                        if (id_encontrado > 0) id_cuenta_destino_gasto = id_encontrado;
                    }

                    int nuevo_id = gasto.IngresarGastos(fecha_transaccion, descripcion, saldo, referencia,
                                                        id_usuario, id_origenNuevo, nombre_cuenta, parroquia_id
                                                        );
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

                    BloquearFilasGuardadas(fila_nueva);

                    dgvGastos.Refresh();
                    dgvGastos.ClearSelection();

                    MessageBox.Show("Gasto registrado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Transacciones obj_transa = new Transacciones();
                    obj_transa.CargarComboBoxOrigen(cmbOrigen, cmbOrigen2, ParroquiaId);

                    ActualizarSaldo();
                    _controladorAlerta.ForzarVerificacionInmediata();
                }
                catch (Exception ex)
                {
                    error_guardado = true;
                    MessageBox.Show("Observación: " + ex.Message,
                                    "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!error_guardado && filas_guardadas > 0)
                {
                    modoEdicion = false;

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
        /// Bloquea todas las filas hasta la fila indicada para impedir edición posterior.
        /// </summary>
        private void BloquearFilasGuardadas(DataGridViewRow filaActual)
        {
            int limite = filaActual.Index;

            for (int i = 0; i <= limite; i++)
            {
                DataGridViewRow fila = dgvGastos.Rows[i];

                if (!fila.IsNewRow)
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        celda.ReadOnly = true;
                    }
                }
            }
        }

        /// <summary>
        /// Habilita edición de la fila seleccionada en gastos (modo libre).
        /// </summary>
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

            object fechaValor = fila_seleccionada.Cells["fecha_transaccion"].Value;
            if (fechaValor != null && fechaValor != DBNull.Value)
            {
                DateTime fechaCreacion = System.Convert.ToDateTime(fechaValor);
                double minutosTranscurridos = (DateTime.Now - fechaCreacion).TotalMinutes;

                if (minutosTranscurridos > 15)
                {
                    MessageBox.Show(
                        "No se puede editar este registro.\nHan pasado más de 15 minutos desde su creación.",
                        "Edición no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            object id_origenValue = fila_seleccionada.Cells["Id_Origen"]?.Value;
            object referencia_value = fila_seleccionada.Cells["NoReferencia"]?.Value;
            object fecha_value = fila_seleccionada.Cells["fecha_transaccion"]?.Value;

            if (id_origenValue != null && id_origenValue != DBNull.Value)
                cmbOrigen2.SelectedValue = System.Convert.ToInt32(id_origenValue);
            else
                cmbOrigen2.SelectedIndex = -1;

            txtNoReferencia2.Text = referencia_value?.ToString() ?? "";
            dateTimePicker2.Value = fecha_value != null && DateTime.TryParse(fecha_value.ToString(), out DateTime fecha)
                ? fecha : DateTime.Now;

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
        /// Abre partidas dobles para la transacción seleccionada en gastos.
        /// </summary>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (dgvGastos.SelectedRows.Count > 0)
            {
                int id_transaccion = System.Convert.ToInt32(dgvGastos.SelectedRows[0].Cells["Id_Transaccion"].Value);
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
        /// Abre partidas dobles para la transacción seleccionada en ingresos.
        /// </summary>
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id_transaccion = System.Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id_Transaccion"].Value);
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
        /// Registra navegación del usuario en historial (llamada a capa de datos).
        /// Ejecutado en hilo invocador (UI); si se desea evitar bloqueo, ejecutar en background.
        /// </summary>
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
        /// Activa edición manual por doble clic en celda de ingresos.
        /// </summary>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (modoEdicion)
                return;

            _modoEdicionManual = true;

            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

            dataGridView1.ReadOnly = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name != "Id_transaccion")
                    col.ReadOnly = false;
            }

            foreach (DataGridViewCell celda in fila.Cells)
            {
                if (celda.OwningColumn.Name != "Id_transaccion")
                    celda.ReadOnly = false;
            }

            dataGridView1.CurrentCell = fila.Cells[e.ColumnIndex];
            dataGridView1.BeginEdit(true);
        }

        /// <summary>
        /// Paint placeholder para panel bancos.
        /// </summary>
        private void panelBancos2_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Paint placeholder para panel de alerta deslizante.
        /// </summary>
        private void pnlAlertaDeslizante_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Modo edición manual por doble clic en gastos.
        /// </summary>
        private void dgvGastos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            _modoEdicionManual = true;

            DataGridViewRow fila = dgvGastos.Rows[e.RowIndex];

            dgvGastos.ReadOnly = false;

            foreach (DataGridViewColumn col in dgvGastos.Columns)
                col.ReadOnly = false;

            foreach (DataGridViewCell celda in fila.Cells)
                celda.ReadOnly = false;

            dgvGastos.CurrentCell = fila.Cells[e.ColumnIndex];
            dgvGastos.BeginEdit(true);
        }

        /// <summary>
        /// Abre formulario para agregar/visualizar cuentas de ahorro.
        /// </summary>
        private void button5_Click_1(object sender, EventArgs e)
        {
            var frm = new BancosCuentaAhorro(ParroquiaId);
            frm.ShowDialog();
        }

        /// <summary>
        /// Paint placeholder para panel de caja chica.
        /// </summary>
        private void panelCajaChica2_Paint(object sender, PaintEventArgs e)
        {
        }

        private readonly ClsCapitalInicial _crudCapital = new ClsCapitalInicial();

        /// <summary>
        /// Configura la visibilidad y texto relacionado con el capital inicial.
        /// </summary>
        private void ConfigurarCapitalInicial()
        {
            bool tieneCapital = _crudCapital.TieneCapitalInicial(ParroquiaId);

            if (tieneCapital)
            {
                chkIngresarCapital.Visible = false;
                panelIngresarCapital.Visible = false;

                decimal capitalActual = _crudCapital.ObtenerCapitalInicial(PredictedId, ParroquiaId);

                lblCapitalInicial.Text = $"Capital Inicial: L. {capitalActual:N2}";
                lblCapitalInicial.Visible = true;
            }
            else
            {
                lblCapitalInicial.Visible = false;
                chkIngresarCapital.Visible = true;
                chkIngresarCapital.Checked = false;
                panelIngresarCapital.Visible = false;
            }
        }

        /// <summary>
        /// Valida el monto ingresado para capital inicial.
        /// </summary>
        private bool ValidarCapitalInicial()
        {
            if (string.IsNullOrWhiteSpace(txtCapitalInicial.Text))
            {
                MessageBox.Show("Debe ingresar un monto.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapitalInicial.Focus();
                return false;
            }

            decimal monto;
            if (!decimal.TryParse(txtCapitalInicial.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("El monto debe ser un número válido mayor a cero.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapitalInicial.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Muestra el capital inicial con formato seguro; captura y reporta excepciones relacionadas con permisos o errores.
        /// </summary>
        private void MostrarCapitalInicial()
        {
            try
            {
                decimal capital = _crudCapital.ObtenerCapitalInicial(PredictedId, ParroquiaId);
                var culturaEEUU = new System.Globalization.CultureInfo("en-US");
                lblCapitalInicial.Text = capital.ToString("'L. ' #,##0.00", culturaEEUU);
            }
            catch (UnauthorizedAccessException ex)
            {
                lblCapitalInicial.Text = "L. 0.00";
                MessageBox.Show(ex.Message, "Error de permisos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                lblCapitalInicial.Text = "L. 0.00";
                Console.WriteLine("Error al obtener capital: " + ex.Message);
            }
        }

        /// <summary>
        /// Guarda o modifica capital inicial según estado y actualiza UI.
        /// </summary>
        private void btnGuardarCapital_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCapitalInicial()) return;

            try
            {
                string textoLimpio = txtCapitalInicial.Text
                    .Replace("L.", "")
                    .Replace("L", "")
                    .Replace(",", "")
                    .Trim();

                if (!decimal.TryParse(textoLimpio,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal monto) || monto <= 0)
                {
                    MessageBox.Show("El monto no es válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool exito = false;

                if (EsModificacionCapital)
                    exito = _crudCapital.ModificarCapitalInicial(monto, ParroquiaId, PredictedId);
                else
                    exito = _crudCapital.IngresarCapitalInicial(PredictedId, ParroquiaId, monto);

                if (exito)
                {
                    MessageBox.Show("Operación realizada con éxito.", "Sistema",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    panel4.Visible = false;
                    panelIngresarCapital.Visible = false;
                    checkBox2.Checked = false;
                    chkIngresarCapital.Checked = false;
                    chkIngresarCapital.Visible = false;
                    txtCapitalInicial.Clear();

                    EsModificacionCapital = false;

                    MostrarCapitalInicial();
                    lblCapitalInicial.Visible = true;

                    ActualizarSaldo();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el capital.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Cancela la entrada de capital inicial.
        /// </summary>
        private void btnCancelarCapital_Click(object sender, EventArgs e)
        {
            chkIngresarCapital.Checked = false;
            panelIngresarCapital.Visible = false;
            txtCapitalInicial.Clear();
        }

        /// <summary>
        /// Muestra/oculta panel de ingreso de capital según checkbox.
        /// </summary>
        private void chkIngresarCapital_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkIngresarCapital.Checked)
            {
                panelIngresarCapital.Visible = true;
                txtCapitalInicial.Focus();
            }
            else
            {
                panelIngresarCapital.Visible = false;
            }
        }

        private void btnCancelarCapital_Click_1(object sender, EventArgs e)
        {
            panelIngresarCapital.Visible = false;
            chkIngresarCapital.Checked = false;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Doble clic para activar edición manual completa en ingresos (similar a otro handler).
        /// </summary>
        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            _modoEdicionManual = true;

            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

            dataGridView1.ReadOnly = false;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.ReadOnly = false;

            foreach (DataGridViewCell celda in fila.Cells)
                celda.ReadOnly = false;

            dataGridView1.CurrentCell = fila.Cells[e.ColumnIndex];
            dataGridView1.BeginEdit(true);
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                panel3.Visible = true;
                lblCapitalInicial.Visible = false;
                chkSaldoInicial.Visible = true;
            }
            else
            {
                panel3.Visible = false;
                chkSaldoInicial.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                panel4.Visible = true;
                lblCapitalInicial.Visible = true;
                label9.Visible = true;

                MostrarCapitalInicial();
            }
            else
            {
                panel4.Visible = false;
                lblCapitalInicial.Visible = false;
                label9.Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Intenta abrir el formulario de modificación de Caja Chica si el último movimiento fue reciente.
        /// </summary>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            try
            {
                ClsCRUD_CuentasBancarias objetoCuentas = new ClsCRUD_CuentasBancarias();

                DateTime fechaIngreso = objetoCuentas.ObtenerFechaUltimoIngreso(ParroquiaId, "Caja Chica");

                if (fechaIngreso != DateTime.MinValue)
                {
                    TimeSpan diferencia = DateTime.Now - fechaIngreso;

                    if (diferencia.TotalMinutes > 16)
                    {
                        MessageBox.Show(
                            "No se puede editar este registro.\nHan pasado más de 15 minutos desde su creación.",
                            "Edición no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        CajaChicaMonto frm = new CajaChicaMonto(ParroquiaId, UsuarioId);
                        frm.EsModificacion = true;
                        frm.SaldoActualizado += this.ActualizarSaldo;
                        frm.SaldoActualizado += () => MostrarCapitalInicial();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No existe registro previo de Caja Chica en esta parroquia.", "Validación");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Abre el formulario para modificar capital inicial (con verificación de tiempo).
        /// </summary>
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox2.Checked || !lblCapitalInicial.Visible)
                {
                    MessageBox.Show("Primero debe activar la sección de Capital Inicial marcando la casilla 'CAPITAL INICIAL'.",
                                    "Acción requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ClsCRUD_CuentasBancarias objetoCuentas = new ClsCRUD_CuentasBancarias();
                DateTime fechaUltimoMovimiento = objetoCuentas.ObtenerFechaUltimoIngreso(ParroquiaId, "Capital Inicial");

                if (fechaUltimoMovimiento == DateTime.MinValue)
                {
                    MessageBox.Show("No se encontró un registro de capital inicial para modificar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                TimeSpan diferencia = DateTime.Now - fechaUltimoMovimiento;
                double minutosPasados = Math.Floor(diferencia.TotalMinutes);

                if (minutosPasados > 15)
                {
                    MessageBox.Show(
                        "No se puede editar este registro.\nHan pasado más de 15 minutos desde su creación.",
                        "Edición no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal capitalActual = _crudCapital.ObtenerCapitalInicial(PredictedId, ParroquiaId);

                if (capitalActual <= 0)
                {
                    MessageBox.Show("No hay capital inicial registrado para modificar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                EsModificacionCapital = true;
                panel4.Visible = true;
                panelIngresarCapital.Visible = true;

                var culturaEEUU = new System.Globalization.CultureInfo("en-US");
                txtCapitalInicial.Text = capitalActual.ToString("'L. ' #,##0.00", culturaEEUU);
                txtCapitalInicial.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox10_DoubleClick(object sender, EventArgs e)
        {
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private Label lblConexion;
        private System.Windows.Forms.Timer timerConexion;

        /// <summary>
        /// Inicializa un indicador visual de estado de conexión y un timer que actualiza su estado periódicamente.
        /// </summary>
        private void InicializarIndicadorConexion()
        {
            lblConexion = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Padding = new Padding(8, 4, 8, 4),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "● Verificando...",
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            this.Controls.Add(lblConexion);
            lblConexion.BringToFront();

            lblConexion.Location = new Point(this.ClientSize.Width - lblConexion.Width - 10, 10);

            this.Resize += (s, e) =>
            {
                lblConexion.Location = new Point(this.ClientSize.Width - lblConexion.Width - 10, 10);
            };

            timerConexion = new System.Windows.Forms.Timer { Interval = 5000 };
            timerConexion.Tick += async (s, e) => await ActualizarEstadoConexion();
            timerConexion.Start();

            _ = ActualizarEstadoConexion();
        }

        /// <summary>
        /// Consulta asincrónicamente la disponibilidad del servidor y pendientes locales, y actualiza el indicador.
        /// Devuelve resultados al UI con Invoke si es necesario.
        /// </summary>
        private async Task ActualizarEstadoConexion()
        {
            bool hayServidor = await Capa_de_procesamiento_de_datos.LocalDbOff
                                    .ServidorDisponibleAsync();

            var todos = new Capa_de_procesamiento_de_datos.LocalDbOff()
                                .ObtenerPendientes();

            int pendientes = todos.Count(r =>
            {
                try
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(r.DatosJson);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("_ParroquiaId", out var p))
                        return p.GetInt32() == this.ParroquiaId;

                    if (root.TryGetProperty("Parametros", out var parametros))
                    {
                        if (parametros.TryGetProperty("Parroquia_ID", out var p2))
                            return p2.GetInt32() == this.ParroquiaId;

                        if (parametros.TryGetProperty("parroquia_id", out var p3))
                            return p3.GetInt32() == this.ParroquiaId;

                        if (parametros.TryGetProperty("id_parroquia", out var p4))
                            return p4.GetInt32() == this.ParroquiaId;
                    }

                    return false;
                }
                catch { return false; }
            });

            if (lblConexion.InvokeRequired)
                lblConexion.Invoke(() => MostrarEstado(hayServidor, pendientes));
            else
                MostrarEstado(hayServidor, pendientes);
        }

        /// <summary>
        /// Actualiza el texto y colores del indicador de conexión según estado.
        /// Ejecutado en hilo UI.
        /// </summary>
        private void MostrarEstado(bool conectado, int pendientes)
        {
            if (!conectado)
            {
                lblConexion.Text = "● Sin conexión — guardando local";
                lblConexion.ForeColor = Color.FromArgb(127, 29, 29);
                lblConexion.BackColor = Color.FromArgb(254, 226, 226);
            }
            else if (pendientes > 0)
            {
                lblConexion.Text = $"● {pendientes} registro(s) pendiente(s) — esperando servidor";
                lblConexion.ForeColor = Color.FromArgb(113, 63, 18);
                lblConexion.BackColor = Color.FromArgb(254, 249, 195);
            }
            else
            {
                lblConexion.Text = "● Conectado — sincronizado";
                lblConexion.ForeColor = Color.FromArgb(22, 101, 52);
                lblConexion.BackColor = Color.FromArgb(220, 252, 231);
            }
        }
    }
}