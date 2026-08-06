using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios.Formularios_inicio_de_sesion_y_ventana_administrador;
using Capa_de_Presentación.Formularios_Luiss;
using System.Data;


namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Ventana principal del administrador. Contiene gestión de usuarios, catálogo de cuentas y bitácora.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Ventana_Principal_Administrador : Form
    {
        //usuarios
        /// <summary>
        /// The crud usuarios
        /// </summary>
        private clsCRUD_Usuarios crud_usuarios;
        /// <summary>
        /// The modo edicion usuario
        /// </summary>
        private bool modo_edicion_usuario = false;
        /// <summary>
        /// The usuario identifier seleccionado
        /// </summary>
        private int usuario_id_seleccionado = 0;
        /// <summary>
        /// The binding source
        /// </summary>
        private BindingSource bindingSource;
        /// <summary>
        /// The identifier parroquia
        /// </summary>
        private int id_parroquia;
        /// <summary>
        /// The usuario identifier
        /// </summary>
        private int _usuario_id;

        //Catalogo
        /// <summary>
        /// The crud catalogo cuentas
        /// </summary>
        private clsCRUD_CatalogoCuentas crud_catalogo_cuentas;
        /// <summary>
        /// The modo edicion catalogo
        /// </summary>
        private bool modo_edicion_catalogo = false;

        private int _idPadreOriginal = 0;

        private int id_cuenta_seleccionada = 0;

        private string _codigoOriginal = "";

        private BindingSource bindingSourceCatalogo;
        private readonly clsCRUD_Usuarios _repo = new clsCRUD_Usuarios();
        private string _passwordHashOriginal = "";


        //BITACORA
        /// <summary>
        /// The crud historial
        /// </summary>
        private clsCRUD_Historial crud_historial;
        //Validaciones
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// The cerrar
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();
        private Label lblConexion;
        private System.Windows.Forms.Timer timerConexion;

        private DataTable _dtParroquiasAdmin;
        private bool _filtrandoAdmin = false;
        private bool _cargandoAdmin = false;



        /// <summary>
        /// Initializes a new instance of the <see cref="Ventana_Principal_Administrador"/> class.
        /// </summary>
        /// <param name="usuarioID">The usuario identifier.</param>
        /// <param name="idParroquia">The identifier parroquia.</param>
        public Ventana_Principal_Administrador(int usuarioID, int idParroquia)
        {
            InitializeComponent();
            this.Shown += (_, __) => CargarNombre();
            this._usuario_id = usuarioID;
            this.id_parroquia = idParroquia;
            UsuarioLogueado.usuario_id = usuarioID;
            UsuarioLogueado.parroquia_id = idParroquia;

            //this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelUsuario);
            panelContenedor.Controls.Add(panelCatalogoCuentas);


            // Opcional: muestra uno por defecto
            MostrarSoloEstePanel(panel1);

            //usuarios
            crud_usuarios = new clsCRUD_Usuarios();

            //catalogo
            crud_catalogo_cuentas = new clsCRUD_CatalogoCuentas();

            //bitacora
            crud_historial = new clsCRUD_Historial();
            //validaciones
            Validaciones = new ClsValidaciones();
            //Para busqueda de usuarios
            bindingSource = new BindingSource();
            bindingSourceCatalogo = new BindingSource();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormClosed += (s, e) => Application.Exit();

            this.FormClosing += (s, e) =>
            {
                timerConexion?.Stop();
                timerConexion?.Dispose();
            };

            InicializarIndicadorConexion();


        }

        /// <summary>
        /// Mostrars the solo este panel.
        /// </summary>
        /// <param name="panelAMostrar">The panel a mostrar.</param>
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

        /// <summary>
        /// Handles the Paint event of the panel5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG5_Load(object sender, EventArgs e)
        {
            //usuarios
            CargarDatosUsuarioDGV();
            CargarComboBoxesUsuario();
            LimpiarCamposUsuario();
            HabilitarControlesUsuario(false);

            //catalogo
            CargarDatosCatalogoDGV();
            LimpiarCamposCatalogo();
            CargarComboBoxEstadoCuenta();
            HabilitarControlesCatalogo(false);

            _ = ActualizarEstadoConexionAdmin();
        }


        private void CargarNivel1()
        {
            try
            {
                DataTable dt = crud_catalogo_cuentas.ObtenerNivel1();

                // Permitir nulos en id_cuenta para la fila vacía
                dt.Columns["id_cuenta"].AllowDBNull = true;

                // Agregar fila vacía al inicio para que no haya selección por defecto
                DataRow fila = dt.NewRow();
                fila["id_cuenta"] = DBNull.Value;
                fila["codigo"] = "";
                fila["nombre"] = "-- Seleccione --";
                dt.Rows.InsertAt(fila, 0);
                cmbNivel1.DataSource = dt;
                cmbNivel1.DisplayMember = "nombre";
                cmbNivel1.ValueMember = "id_cuenta";
                cmbNivel1.SelectedIndex = 0;
                // Limpiar y deshabilitar niveles inferiores
                LimpiarComboBox(cmbNivel2);
                LimpiarComboBox(cmbNivel3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar nivel 1: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarNivel2(int id_padre)
        {
            try
            {
                DataTable dt = crud_catalogo_cuentas.ObtenerHijosPorPadre(id_padre);

                LimpiarComboBox(cmbNivel2);
                LimpiarComboBox(cmbNivel3);

                if (dt.Rows.Count == 0)
                {
                    if (!modo_edicion_catalogo)
                        SugerirCodigo(id_padre);
                    else if (id_padre != _idPadreOriginal)
                        SugerirCodigo(id_padre);
                    return;
                }

                // Permitir nulos antes de insertar la fila vacía
                dt.Columns["id_cuenta"].AllowDBNull = true;

                DataRow fila = dt.NewRow();
                fila["id_cuenta"] = DBNull.Value;
                fila["codigo"] = "";
                fila["nombre"] = "-- Seleccione --";
                dt.Rows.InsertAt(fila, 0);

                cmbNivel2.DataSource = dt;
                cmbNivel2.DisplayMember = "nombre";
                cmbNivel2.ValueMember = "id_cuenta";
                cmbNivel2.SelectedIndex = 0;
                cmbNivel2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar nivel 2: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarNivel3(int id_padre)
        {
            try
            {
                DataTable dt = crud_catalogo_cuentas.ObtenerHijosPorPadre(id_padre);

                LimpiarComboBox(cmbNivel3);

                if (dt.Rows.Count == 0)
                {
                    if (!modo_edicion_catalogo)
                        SugerirCodigo(id_padre);
                    else if (id_padre != _idPadreOriginal)
                        SugerirCodigo(id_padre);
                    return;
                }

                // Permitir nulos antes de insertar la fila vacía
                dt.Columns["id_cuenta"].AllowDBNull = true;

                DataRow fila = dt.NewRow();
                fila["id_cuenta"] = DBNull.Value;
                fila["codigo"] = "";
                fila["nombre"] = "-- Seleccione --";
                dt.Rows.InsertAt(fila, 0);

                cmbNivel3.DataSource = dt;
                cmbNivel3.DisplayMember = "nombre";
                cmbNivel3.ValueMember = "id_cuenta";
                cmbNivel3.SelectedIndex = 0;
                cmbNivel3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar nivel 3: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarNombre()
        {
            try
            {
                string nombre = _repo.ObtenerNombrePorUsuario(Sesion1.usuario_id);
                label1.Text = string.IsNullOrWhiteSpace(nombre)
                ? "Nombre no disponible / cuenta inactiva"
                : $"Bienvenido, {nombre}";
            }
            catch (Exception ex)
            {
                label1.Text = "Error obteniendo nombre";
            }
        }

        private void LimpiarComboBox(ComboBox cmb)
        {
            cmb.DataSource = null;
            cmb.Items.Clear();
            cmb.Enabled = false;
            //txtIdCuenta.Clear();
        }

        private int ObtenerIdPadreSeleccionado()
        {
            // Nivel 3 tiene prioridad si tiene selección válida
            if (cmbNivel3.Enabled && cmbNivel3.SelectedValue != null
                && cmbNivel3.SelectedValue != DBNull.Value
                && (int)cmbNivel3.SelectedValue > 0)
                return (int)cmbNivel3.SelectedValue;

            // Si no, nivel 2
            if (cmbNivel2.Enabled && cmbNivel2.SelectedValue != null
                && cmbNivel2.SelectedValue != DBNull.Value
                && (int)cmbNivel2.SelectedValue > 0)
                return (int)cmbNivel2.SelectedValue;

            // Si no, nivel 1
            if (cmbNivel1.SelectedValue != null
                && cmbNivel1.SelectedValue != DBNull.Value
                && (int)cmbNivel1.SelectedValue > 0)
                return (int)cmbNivel1.SelectedValue;

            return -1;
        }

        private void SugerirCodigo(int id_padre)
        {
            try
            {
                if (id_padre <= 0) return;
                string proximo = crud_catalogo_cuentas.ObtenerProximoCodigo(id_padre);
                txtIdCuenta.Text = proximo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al sugerir código: " + ex.Message);
            }
        }


        private void CargarDatosUsuarioDGV()
        {
            try
            {
                DataTable dt = crud_usuarios.ObtenerUsuarios();
                bindingSource.DataSource = dt;
                dgv_usuarios.DataSource = bindingSource;

                dgv_usuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgv_usuarios.AllowUserToResizeRows = false;


                dgv_usuarios.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgv_usuarios.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv_usuarios.Columns["ID"].Width = 70;

                dgv_usuarios.Columns["Correo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv_usuarios.Columns["Correo"].Width = 250;

                dgv_usuarios.Columns["Usuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv_usuarios.Columns["Usuario"].Width = 220;

                // Centrar encabezados
                foreach (DataGridViewColumn col in dgv_usuarios.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Ocultar columnas internas
                if (dgv_usuarios.Columns["Contrasena"] != null)
                    dgv_usuarios.Columns["Contrasena"].Visible = false;
                if (dgv_usuarios.Columns["RolID"] != null)
                    dgv_usuarios.Columns["RolID"].Visible = false;
                if (dgv_usuarios.Columns["ParroquiaID"] != null)
                    dgv_usuarios.Columns["ParroquiaID"].Visible = false;
                if (dgv_usuarios.Columns["EstadoID"] != null)
                    dgv_usuarios.Columns["EstadoID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarComboBoxEstadoCuenta()
        {
            try
            {
                cmbEstadoCuenta.DataSource = crud_catalogo_cuentas.ObtenerEstados();
                cmbEstadoCuenta.DisplayMember = "descripcion";
                cmbEstadoCuenta.ValueMember = "Id_estado_cuenta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estados: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cargars the combo boxes usuario.
        /// </summary>
        private void CargarComboBoxesUsuario()
        {
            try
            {
                cmb_rol.DataSource = crud_usuarios.ObtenerRoles();
                cmb_rol.DisplayMember = "Rol_descripcion";
                cmb_rol.ValueMember = "Rol_Id";

                _cargandoAdmin = true;
                _dtParroquiasAdmin = crud_usuarios.ObtenerParroquias();
                cmb_parroquia.DataSource = _dtParroquiasAdmin.DefaultView;
                cmb_parroquia.DisplayMember = "Parroquia_nombre";
                cmb_parroquia.ValueMember = "Parroquia_id";
                _cargandoAdmin = false;
                cmb_parroquia.TextChanged -= cmb_parroquia_TextChanged;
                cmb_parroquia.TextChanged += cmb_parroquia_TextChanged;

                cmb_estado.DataSource = crud_usuarios.ObtenerEstados();
                cmb_estado.DisplayMember = "descripcion";
                cmb_estado.ValueMember = "Id_estado_cuenta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Cargars the datos usuario.
        /// </summary>
        private void CargarDatosUsuario()
        {
            try
            {
                var usuario = crud_usuarios.BuscarUsuarioPorId(usuario_id_seleccionado);

                if (usuario != null)
                {
                    txt_id.Text = usuario["Usuario_id"].ToString();
                    txt_nombreCuenta.Text = usuario["usuario_nombre"].ToString();
                    txt_apellido.Text = usuario["usuario_apellido"].ToString();
                    txt_correo.Text = usuario["usuario_correo"] != DBNull.Value
                        ? usuario["usuario_correo"].ToString()
                        : "";
                    txt_usuario.Text = usuario["usuario"].ToString();
                    txt_contraseña.Text = usuario["usuario_password"].ToString();

                    _passwordHashOriginal = usuario["usuario_password"].ToString();

                    cmb_rol.SelectedValue = usuario["Rol_Id"];
                    cmb_parroquia.SelectedValue = usuario["Parroquia_Id"];
                    cmb_estado.SelectedValue = usuario["Id_estado_cuenta"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuario: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validars the campos usuario.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCamposUsuario()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();
            string nombre = txt_nombreCuenta.Text.Trim();
            string apellido = txt_apellido.Text.Trim();
            string correo = txt_correo.Text.Trim();
            string usuario = txt_usuario.Text.Trim();
            string contraseña = txt_contraseña.Text.Trim();

            // Nombre
            if (string.IsNullOrWhiteSpace(nombre) || !val.EsTextoValido(nombre))
            {
                MessageBox.Show("El nombre es requerido y solo puede contener letras.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombreCuenta.Focus();
                return false;
            }

            // Apellido
            if (string.IsNullOrWhiteSpace(apellido) || !val.EsTextoValido(apellido))
            {
                MessageBox.Show("El apellido es requerido y solo puede contener letras.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_apellido.Focus();
                return false;
            }

            // Correo
            if (string.IsNullOrWhiteSpace(correo) || !val.EsCorreoValido(correo))
            {
                MessageBox.Show("El correo es requerido y debe tener un formato válido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_correo.Focus();
                return false;
            }

            // Usuario
            if (string.IsNullOrWhiteSpace(usuario) || !val.EsUsuarioValidoRango(usuario))
            {
                MessageBox.Show("El nombre de usuario es requerido y solo puede tener letras, números, puntos o guiones bajos.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_usuario.Focus();
                return false;
            }

            bool passwordCambio = contraseña != _passwordHashOriginal;
            if (passwordCambio)
            {
                if (string.IsNullOrWhiteSpace(contraseña) || !val.EsContraseñaValida(contraseña))
                {
                    MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayuscula, un numero y un caracter especial.",
                        "Contraseña invalida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_contraseña.Focus();
                    return false;
                }
            }

            // Combo Rol
            if (cmb_rol.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un rol.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_rol.Focus();
                return false;
            }

            // Combo Parroquia
            if (cmb_parroquia.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una parroquia.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_parroquia.Focus();
                return false;
            }

            // Combo Estado
            if (cmb_estado.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un estado de cuenta.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_estado.Focus();
                return false;
            }

            return true;
        }
        /// <summary>
        /// Limpiars the campos usuario.
        /// </summary>
        private void LimpiarCamposUsuario()
        {
            txt_id.Clear();
            txt_nombreCuenta.Clear();
            txt_apellido.Clear();
            txt_correo.Clear();
            txt_usuario.Clear();
            txt_contraseña.Clear();

            _passwordHashOriginal = "";

            if (cmb_rol.Items.Count > 0)
                cmb_rol.SelectedIndex = 0;
            if (cmb_parroquia.Items.Count > 0)
                cmb_parroquia.SelectedIndex = 0;
            if (cmb_estado.Items.Count > 0)
                cmb_estado.SelectedIndex = 0;

            usuario_id_seleccionado = 0;
            modo_edicion_usuario = false;
        }

        /// <summary>
        /// Habilitars the controles usuario.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void HabilitarControlesUsuario(bool habilitar)
        {
            txt_id.Enabled = false;
            txt_nombreCuenta.Enabled = habilitar;
            txt_apellido.Enabled = habilitar;
            txt_correo.Enabled = habilitar;
            txt_usuario.Enabled = habilitar;
            txt_contraseña.Enabled = habilitar;
            cmb_rol.Enabled = habilitar;
            cmb_parroquia.Enabled = habilitar;
            cmb_estado.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
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
        /// Handles the 1 event of the panel5_Paint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUsuario_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelUsuario);
        }

        /// <summary>
        /// Handles the Click event of the btnCatalagoCuenta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCatalagoCuenta_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCatalogoCuentas);
        }

        /// <summary>
        /// Handles the Click event of the button6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

        private void button6_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposCatalogo()) return;

            try
            {
                // 1. Captura de datos de la interfaz
                string codigo = txtIdCuenta.Text.Trim();
                string nombre = txtNombreCuenta.Text.Trim();
                string detalle = txtDetalle.Text.Trim();
                int id_padre = ObtenerIdPadreSeleccionado();
                int id_estado = (cmbEstadoCuenta.SelectedValue != null) ? Convert.ToInt32(cmbEstadoCuenta.SelectedValue) : 1;

                // 2. Validación de nivel jerárquico
                if (id_padre < 0) // Permitimos 0 si es una cuenta raíz, según tu lógica de BD
                {
                    MessageBox.Show("Seleccione una jerarquía válida o cuenta padre.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool resultado = false;

                if (modo_edicion_catalogo)
                {
                    // MODIFICAR
                    resultado = crud_catalogo_cuentas.ModificarCatalogoCuenta(
                        id_cuenta_seleccionada, codigo, nombre, id_padre, detalle);

                    // Verificamos si el resultado fue exitoso
                    if (resultado)
                    {
                        // Esto ya ejecuta CargarDatosCatalogoDGV() y LimpiarCamposCatalogo()
                        FinalizarOperacion("Modificación", nombre, codigo);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo confirmar la modificación en la base de datos.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        // Forzamos el refresco de todos modos por si hubo un falso negativo
                        CargarDatosCatalogoDGV();
                        LimpiarCamposCatalogo();
                    }
                }
                else
                {
                    // AGREGAR
                    if (crud_catalogo_cuentas.CatalogoCuentaExiste(nombre))
                    {
                        MessageBox.Show("El nombre de la cuenta ya existe.", "Advertencia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    resultado = crud_catalogo_cuentas.AgregarCatalogoCuenta(
                        codigo, nombre, id_padre, detalle, es_detalle: true, id_estado: id_estado);

                    if (resultado)
                    {
                        FinalizarOperacion("Creación", nombre, codigo);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar la cuenta.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // English: Show the actual SQL Error message (from RAISERROR).
                // Español: Muestra el mensaje de error real de SQL (del RAISERROR).
                string mensajeError = ex.Message;
                if (ex.InnerException != null)
                {
                    mensajeError += "\nDetalle técnico: " + ex.InnerException.Message;
                }

                MessageBox.Show("Error al guardar: " + mensajeError,
                    "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para no repetir el refresco de la UI y la bitácora
        private void FinalizarOperacion(string tipoAccion, string nombre, string codigo)
        {
            MessageBox.Show($"{tipoAccion} de cuenta exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                crud_historial.RegistrarActividad(UsuarioLogueado.usuario_id, 8, tipoAccion,
                    $"Acción: {tipoAccion} - Cuenta: '{nombre}' (Código: {codigo}).");
            }
            catch { }

            CargarDatosCatalogoDGV();
            LimpiarCamposCatalogo();
            HabilitarControlesCatalogo(false);
        }

        /// <summary>
        /// Cargars the datos catalogo cuenta.
        /// </summary>
        private void CargarDatosCatalogoCuenta()
        {
            try
            {
                // 1. Buscar la cuenta en la BD usando el id_cuenta de tu tabla
                var cuenta = crud_catalogo_cuentas.BuscarCatalogoCuentaPorId(id_cuenta_seleccionada);
                if (cuenta == null) return;

                // 2. Desconectar eventos para evitar que se sugiera un código nuevo
                cmbNivel1.SelectedIndexChanged -= cmbCuenta_SelectedIndexChanged;
                cmbNivel2.SelectedIndexChanged -= cmbTipoCuenta_SelectedIndexChanged;
                cmbNivel3.SelectedIndexChanged -= cmbNivel3_SelectedIndexChanged;

                // 3. Asignar valores básicos de la BD
                txtIdCuenta.Text = cuenta["codigo"].ToString();
                txtIdCuenta.ReadOnly = true; // Bloqueamos el código para que no cambie
                txtIdCuenta.BackColor = Color.LightGray;

                txtNombreCuenta.Text = cuenta["nombre"].ToString();
                txtDetalle.Text = cuenta["detalle"] != DBNull.Value ? cuenta["detalle"].ToString() : "";

                // 4. Cargar Estado (Id_estado_cuenta en tu BD)
                if (cuenta["Id_estado_cuenta"] != DBNull.Value)
                    cmbEstadoCuenta.SelectedValue = Convert.ToInt32(cuenta["Id_estado_cuenta"]);

                // 5. Reconstruir la Cascada de Niveles
                // Para el Nivel 1, necesitamos el id_padre que sea NULL o nivel raíz
                // Nota: Asegúrate que tu SP o Función devuelva estos IDs calculados
                int idNivel1 = cuenta["id_padre_nivel0"] != DBNull.Value ? Convert.ToInt32(cuenta["id_padre_nivel0"]) : 0;
                int idNivel2 = cuenta["id_padre_nivel1"] != DBNull.Value ? Convert.ToInt32(cuenta["id_padre_nivel1"]) : 0;
                int idPadreDirecto = cuenta["id_padre"] != DBNull.Value ? Convert.ToInt32(cuenta["id_padre"]) : 0;

                // Lógica de selección para que cmbNivel1 recupere su info
                if (idNivel1 > 0)
                {
                    cmbNivel1.SelectedValue = idNivel1;
                    CargarNivel2(idNivel1); // Carga las opciones del siguiente combo
                }

                if (idNivel2 > 0)
                {
                    cmbNivel2.SelectedValue = idNivel2;
                    CargarNivel3(idNivel2);
                }

                if (idPadreDirecto > 0)
                {
                    cmbNivel3.SelectedValue = idPadreDirecto;
                }

                // 6. Restaurar eventos
                cmbNivel1.SelectedIndexChanged += cmbCuenta_SelectedIndexChanged;
                cmbNivel2.SelectedIndexChanged += cmbTipoCuenta_SelectedIndexChanged;
                cmbNivel3.SelectedIndexChanged += cmbNivel3_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la BD: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCamposUsuario();
            HabilitarControlesUsuario(true);
            modo_edicion_usuario = false;

            try
            {
                int proximoId = crud_usuarios.ObtenerProximoId();
                txt_id.Text = proximoId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ID: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txt_nombreCuenta.Focus();
        }



        /// <summary>
        /// Handles the Click event of the btnGuardarUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            ClsValidaciones validaciones = new ClsValidaciones();

            if (!ValidarCamposUsuario())
                return;

            if (!validaciones.ValidarEspacios(txt_nombreCuenta.Text)
                || !validaciones.ValidarEspacios(txt_apellido.Text)
                || !validaciones.ValidarEspacios(txt_contraseña.Text))
                return;

            bool passwordCambio = txt_contraseña.Text.Trim() != _passwordHashOriginal;
            if (passwordCambio && !validaciones.EsContraseñaValida(txt_contraseña.Text.Trim()))
            {
                MessageBox.Show(
                    "La contraseña debe tener al menos 8 caracteres, una mayuscula, un numero y un caracter especial.",
                    "Contraseña invalida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string nombre = txt_nombreCuenta.Text.Trim();
                string apellido = txt_apellido.Text.Trim();
                string correo = txt_correo.Text.Trim();
                string usuario = txt_usuario.Text.Trim();
                string password = txt_contraseña.Text.Trim();
                int idRol = Convert.ToInt32(cmb_rol.SelectedValue);
                int idParroquia = Convert.ToInt32(cmb_parroquia.SelectedValue);
                int idEstado = Convert.ToInt32(cmb_estado.SelectedValue);

                if (modo_edicion_usuario)
                {
                    bool resultado = crud_usuarios.ModificarUsuario(
                        usuario_id_seleccionado, nombre, apellido,
                        correo, usuario, password, idRol, idParroquia, idEstado);

                    if (resultado)
                    {
                        MessageBox.Show("Usuario modificado exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            crud_historial.RegistrarActividad(
                                Sesion1.usuario_id, 7,
                                "Modificación de Usuario",
                                $"Se modificaron los datos del usuario: '{usuario}' (ID: {usuario_id_seleccionado})."
                            );
                        }
                        catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }

                        CargarDatosUsuarioDGV();
                        LimpiarCamposUsuario();
                        HabilitarControlesUsuario(false);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el usuario", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (crud_usuarios.UsuarioExiste(usuario))
                    {
                        MessageBox.Show("El nombre de usuario ya existe", "Advertencia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nuevoId = crud_usuarios.AgregarUsuario(nombre, apellido, correo,
                        usuario, password, idRol, idParroquia, idEstado);

                    if (nuevoId > 0)
                    {
                        MessageBox.Show($"Usuario agregado exitosamente con ID: {nuevoId}", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            crud_historial.RegistrarActividad(
                                Sesion1.usuario_id, 7,
                                "Creación de Usuario",
                                $"Se creó el nuevo usuario: '{usuario}' (ID: {nuevoId})."
                            );
                        }
                        catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }

                        CargarDatosUsuarioDGV();
                        LimpiarCamposUsuario();
                        HabilitarControlesUsuario(false);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el usuario", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Paint event of the panelUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panelUsuario_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnModificarCuentaUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnModificarCuentaUsuario_Click(object sender, EventArgs e)
        {
            if (dgv_usuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para modificar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar el ID del usuario seleccionado
            usuario_id_seleccionado = Convert.ToInt32(dgv_usuarios.CurrentRow.Cells["ID"].Value);

            HabilitarControlesUsuario(true);
            modo_edicion_usuario = true;
            CargarDatosUsuario();
            txt_nombreCuenta.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnInhabilitarUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInhabilitarUsuario_Click(object sender, EventArgs e)
        {
            if (dgv_usuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para inhabilitar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgv_usuarios.CurrentRow.Cells["ID"].Value);
                int estadoInactivo = 2;

                bool resultado = crud_usuarios.InhabilitarUsuario(id, estadoInactivo);

                if (resultado)
                {
                    MessageBox.Show("Usuario inhabilitado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // --- **NUEVO** REGISTRO DE BITÁCORA (INHABILITAR) ---
                    try
                    {
                        crud_historial.RegistrarActividad(
                            Sesion1.usuario_id,
                            7, // Módulo de Usuarios
                            "Inhabilitación de Usuario",
                            $"Se inhabilitó al usuario con ID: {id}."
                        );
                    }
                    catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }


                    CargarDatosUsuarioDGV();
                    LimpiarCamposUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inhabilitar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        /// <summary>
        /// Handles the Click event of the btnHabilitar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dgv_usuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para habilitar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgv_usuarios.CurrentRow.Cells["ID"].Value);
                int estadoInactivo = 1;

                bool resultado = crud_usuarios.InhabilitarUsuario(id, estadoInactivo);

                if (resultado)
                {
                    MessageBox.Show("Usuario habilitado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                    try
                    {
                        crud_historial.RegistrarActividad(
                            Sesion1.usuario_id,
                            7, // Módulo de Usuarios
                            "Habilitación de Usuario",
                            $"Se habilitó al usuario con ID: {id}."
                        );
                    }
                    catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }


                    CargarDatosUsuarioDGV();
                    LimpiarCamposUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the 1 event of the btnAgregar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            LimpiarCamposUsuario();
            HabilitarControlesUsuario(true);
            modo_edicion_usuario = false;

            try
            {
                int proximoId = crud_usuarios.ObtenerProximoId();
                txt_id.Text = proximoId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ID: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txt_nombreCuenta.Focus();
        }

        //catalogo
        /// <summary>
        /// Cargars the datos catalogo DGV.
        /// </summary>
        private void CargarDatosCatalogoDGV()
        {
            try
            {
                DataTable dt = crud_catalogo_cuentas.ObtenerCatalogoCuentas();
                bindingSourceCatalogo.DataSource = dt;

                dgvCatalogoCuentas.DataSource = bindingSourceCatalogo;

                // Ocultar columnas internas
                string[] ocultar = { "EstadoID", "EsDetalle", "id_cuenta",
                            "CodigoPadre" };
                foreach (string col in ocultar)
                    if (dgvCatalogoCuentas.Columns[col] != null)
                        dgvCatalogoCuentas.Columns[col].Visible = false;


                // Centrar encabezados
                foreach (DataGridViewColumn col in dgvCatalogoCuentas.Columns)
                {
                    if (col?.HeaderCell != null)
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogo: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        /// <summary>
        /// Validars the campos catalogo.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCamposCatalogo()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            // 1. Validar que exista un código generado
            if (string.IsNullOrWhiteSpace(txtIdCuenta.Text))
            {
                MessageBox.Show("Seleccione un padre para generar el código.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Validar Nombre (Permite letras, espacios, puntos y comas)
            if (string.IsNullOrWhiteSpace(txtNombreCuenta.Text) ||
                !val.EsTextoPuntuacionValido(txtNombreCuenta.Text.Trim()))
            {
                MessageBox.Show("El nombre es requerido y solo puede contener letras, espacios, puntos o comas.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCuenta.Focus();
                return false;
            }

            // 3. Validar Detalle (Permite letras, espacios, puntos y comas)
            if (string.IsNullOrWhiteSpace(txtDetalle.Text) ||
                !val.EsTextoPuntuacionValido(txtDetalle.Text.Trim()))
            {
                MessageBox.Show("El detalle es requerido y solo puede contener letras, espacios, puntos o comas.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDetalle.Focus();
                return false;
            }

            // 4. Validar jerarquía
            if (ObtenerIdPadreSeleccionado() <= 0)
            {
                MessageBox.Show("Debe seleccionar al menos el tipo de cuenta (Nivel 1).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbNivel1.Focus();
                return false;
            }

            return true;
        }
        /// <summary>
        /// Limpiars the campos catalogo.
        /// </summary>
        private void LimpiarCamposCatalogo()
        {
            txtIdCuenta.Clear();
            txtNombreCuenta.Clear();
            txtDetalle.Clear();

            CargarNivel1();  // Resetea toda la cascada

            id_cuenta_seleccionada = 0;
            modo_edicion_catalogo = false;
        }

        /// <summary>
        /// Habilitars the controles catalogo.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void HabilitarControlesCatalogo(bool habilitar)
        {
            txtIdCuenta.Enabled = habilitar;
            txtNombreCuenta.Enabled = habilitar;
            txtDetalle.Enabled = habilitar;
            cmbEstadoCuenta.Enabled = habilitar;
            cmbNivel1.Enabled = habilitar;
            cmbNivel2.Enabled = false;
            cmbNivel3.Enabled = false;
            btnGuardarCuenta.Enabled = habilitar;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBuscar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textoBusqueda = txt_buscar.Text.Trim();

                if (string.IsNullOrEmpty(textoBusqueda))
                {
                    bindingSource.RemoveFilter();
                }
                else
                {
                    // Busca en Nombre, Apellido y Usuario
                    bindingSource.Filter = string.Format(
                        "Nombre LIKE '%{0}%' OR Apellido LIKE '%{0}%' OR Usuario LIKE '%{0}%'",
                        textoBusqueda.Replace("'", "''")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbParroquia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>


        /// <summary>
        /// Handles the Click event of the pictureBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FRM_ServiciosAdministrador popup = new FRM_ServiciosAdministrador();

            // Obtener la posición del PictureBox en coordenadas de pantalla
            var btnPos = pictureBox3.PointToScreen(Point.Empty);

            // Desplazamiento horizontal (a la izquierda)
            int desplazamientoIzquierda = 450;

            // Desplazamiento vertical (hacia arriba)
            int desplazamientoArriba = 30;

            popup.StartPosition = FormStartPosition.Manual;

            // Calculamos la nueva ubicación:
            // X: Restamos el desplazamiento a la izquierda (450).
            // Y: Restamos la altura del PictureBox (para subirlo a su nivel)
            //    y restamos el desplazamiento hacia arriba (150).
            popup.Location = new Point(
                btnPos.X - desplazamientoIzquierda,
                btnPos.Y - desplazamientoArriba
            );
            popup.ShowDialog(this);
        }

        /// <summary>
        /// Handles the Click event of the pictureBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Cerrar_Sesión popup = new Cerrar_Sesión();
            var btnPos = pictureBox4.PointToScreen(Point.Empty);

            // Cantidad de desplazamiento a la izquierda (en píxeles) 
            int desplazamientoIzquierda = 450; // Ajusta este valor según tu necesidad

            popup.StartPosition = FormStartPosition.Manual;

            popup.Location = new Point(
                btnPos.X - desplazamientoIzquierda,
                btnPos.Y + pictureBox4.Height
            );

            if (popup.ShowDialog() == DialogResult.OK)
            {
                ManejarCierreSesion();
            }
        }

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
        /// Handles the KeyPress event of the txtNombre control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Validaciones.NoPermitirEspacioInicial(txt_nombreCuenta.Text, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnHabilitarCuenta_Click(object sender, EventArgs e)
        {
            if (dgvCatalogoCuentas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una cuenta para habilitar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvCatalogoCuentas.CurrentRow.Cells["id_cuenta"].Value);


                string codigo = dgvCatalogoCuentas.CurrentRow.Cells["Código"].Value?.ToString() ?? "";

                // 1 = Habilitada
                if (crud_catalogo_cuentas.CambiarEstadoCuenta(id, id_estado: 1))
                {
                    dgvCatalogoCuentas.Refresh();
                    MessageBox.Show("Cuenta habilitada exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        crud_historial.RegistrarActividad(UsuarioLogueado.usuario_id, 8,
                            "Habilitación de Cuenta",
                            $"Se habilitó la cuenta con código: {codigo}.");
                    }
                    catch (Exception exBitacora)
                    {
                        Console.WriteLine("Error de Bitácora: " + exBitacora.Message);
                    }
                    CargarDatosCatalogoDGV();
                    LimpiarCamposCatalogo();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelCatalogoCuentas_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInhabilitarCuenta_Click(object sender, EventArgs e)
        {
            if (dgvCatalogoCuentas.CurrentRow == null)
            {

                MessageBox.Show("Seleccione una cuenta para inhabilitar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvCatalogoCuentas.CurrentRow.Cells["id_cuenta"].Value);
                string codigo = dgvCatalogoCuentas.CurrentRow.Cells["Código"].Value?.ToString() ?? "";

                // 2 = Inhabilitada
                if (crud_catalogo_cuentas.CambiarEstadoCuenta(id, id_estado: 2))
                {
                    CargarDatosCatalogoDGV();
                    dgvCatalogoCuentas.Refresh(); // ← agregar esto
                    LimpiarCamposCatalogo();
                    MessageBox.Show("Cuenta inhabilitada exitosamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        crud_historial.RegistrarActividad(UsuarioLogueado.usuario_id, 8,
                            "Inhabilitación de Cuenta",
                            $"Se inhabilitó la cuenta con código: {codigo}.");
                    }
                    catch (Exception exBitacora)
                    {
                        Console.WriteLine("Error de Bitácora: " + exBitacora.Message);
                    }
                    CargarDatosCatalogoDGV();
                    LimpiarCamposCatalogo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inhabilitar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void txt_apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validaciones.NoPermitirEspacioInicial(txt_apellido.Text, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validaciones.NoPermitirEspacioInicial(txt_usuario.Text, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_correo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validaciones.NoPermitirEspacioInicial(txt_correo.Text, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombreCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validaciones.NoPermitirEspacioInicial(txtNombreCuenta.Text, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
                e.Handled = true; // bloquea cualquier espacis 
        }

        private void txtDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validaciones.NoPermitirEspacioInicial(txtDetalle.Text, e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void btnModificarCuenta_Click(object sender, EventArgs e)
        {
            if (dgvCatalogoCuentas.CurrentRow == null) return;

            modo_edicion_catalogo = true;
            HabilitarControlesCatalogo(true);
            txtIdCuenta.ReadOnly = true;

            try
            {
                id_cuenta_seleccionada = Convert.ToInt32(dgvCatalogoCuentas.CurrentRow.Cells["id_cuenta"].Value);
                _codigoOriginal = dgvCatalogoCuentas.CurrentRow.Cells["Código"].Value?.ToString() ?? "";
                string nombrePadre = dgvCatalogoCuentas.CurrentRow.Cells["Nombre Padre"].Value?.ToString() ?? "";

                // Obtener el padre ANTES de cargar, consultando la BD directamente
                var cuenta = crud_catalogo_cuentas.BuscarCatalogoCuentaPorId(id_cuenta_seleccionada);
                if (cuenta != null)
                {
                    int idPadreDirecto = cuenta["id_padre"] != DBNull.Value ? Convert.ToInt32(cuenta["id_padre"]) : 0;
                    _idPadreOriginal = idPadreDirecto; // Guardarlo ANTES de cargar cascada
                }

                // Ahora cargar sin que SugerirCodigo afecte
                CargarDatosCatalogoCuenta();

                // Restaurar código siempre
                txtIdCuenta.Text = _codigoOriginal;
                txtIdCuenta.ReadOnly = true;
                txtIdCuenta.BackColor = Color.LightGray;

                if (cmbNivel1.Items.Count > 0)
                {
                    int index = cmbNivel1.FindStringExact(nombrePadre);
                    if (index != -1)
                        cmbNivel1.SelectedIndex = index;

                    // Restaurar código de nuevo por si el SelectedIndex lo cambió
                    txtIdCuenta.Text = _codigoOriginal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al devolver jerarquía: " + ex.Message);
            }

            txtNombreCuenta.Focus();
        }

        private void cmbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNivel1.SelectedValue == null || cmbNivel1.SelectedValue == DBNull.Value) return;
            if (!int.TryParse(cmbNivel1.SelectedValue.ToString(), out int id)) return;
            if (id <= 0) return;

            CargarNivel2(id);

            if (modo_edicion_catalogo)
            {
                int padreActual = ObtenerIdPadreSeleccionado();
                if (padreActual == _idPadreOriginal)
                {
                    txtIdCuenta.Text = _codigoOriginal;
                    txtIdCuenta.ReadOnly = true;
                    txtIdCuenta.BackColor = Color.LightGray;
                }
                else if (padreActual > 0)
                {
                    SugerirCodigo(padreActual);
                    txtIdCuenta.ReadOnly = true;
                    txtIdCuenta.BackColor = Color.LightGray;
                }
            }
        }

        private void cmbTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNivel2.SelectedValue == null || cmbNivel2.SelectedValue == DBNull.Value) return;
            if (!int.TryParse(cmbNivel2.SelectedValue.ToString(), out int id)) return;
            if (id <= 0) return;

            CargarNivel3(id);

            if (modo_edicion_catalogo)
            {
                int padreActual = ObtenerIdPadreSeleccionado();
                if (padreActual == _idPadreOriginal)
                {
                    txtIdCuenta.Text = _codigoOriginal;
                    txtIdCuenta.ReadOnly = true;
                    txtIdCuenta.BackColor = Color.LightGray;
                }
                else if (padreActual > 0)
                {
                    SugerirCodigo(padreActual);
                    txtIdCuenta.ReadOnly = true;
                    txtIdCuenta.BackColor = Color.LightGray;
                }
            }
        }

        private void cmbNivel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(cmbNivel3.SelectedValue?.ToString(), out int idSeleccionado)) return;
            if (idSeleccionado <= 0) return;

            if (modo_edicion_catalogo)
            {
                if (idSeleccionado == _idPadreOriginal)
                {
                    txtIdCuenta.Text = _codigoOriginal;
                    txtIdCuenta.ReadOnly = true;
                    txtIdCuenta.BackColor = Color.LightGray;
                }
                else
                {
                    SugerirCodigo(idSeleccionado);
                    txtIdCuenta.ReadOnly = true;
                    txtIdCuenta.BackColor = Color.LightGray;
                }
                return;
            }

            SugerirCodigo(idSeleccionado);
        }

        private void btnNuevaCuenta_Click_1(object sender, EventArgs e)
        {
            LimpiarCamposCatalogo();
            HabilitarControlesCatalogo(true);
            modo_edicion_catalogo = false;
            id_cuenta_seleccionada = 0;
            txtNombreCuenta.Focus();
        }

        private void txtBuscarCuenta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textoBusqueda = txtBuscarCuenta.Text.Trim();

                if (string.IsNullOrEmpty(textoBusqueda))
                {
                    bindingSourceCatalogo.RemoveFilter();
                }
                else
                {
                    bindingSourceCatalogo.Filter = string.Format(
                       "[Código] LIKE '%{0}%' OR [Nombre] LIKE '%{0}%'",
                        textoBusqueda.Replace("'", "''")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmb_parroquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var frm = new AgregarParroquiaNueva())
            {
                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    CargarComboBoxesUsuario();
                }
            }
        }


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
            timerConexion.Tick += async (s, e) => await ActualizarEstadoConexionAdmin();
            timerConexion.Start();

            _ = ActualizarEstadoConexionAdmin();
        }

        private async Task ActualizarEstadoConexionAdmin()
        {
            bool hayServidor = await Capa_de_procesamiento_de_datos.LocalDbOff
                                    .ServidorDisponibleAsync();

            int pendientes = new Capa_de_procesamiento_de_datos.LocalDbOff()
                                    .ContarPendientes();

            if (lblConexion.InvokeRequired)
                lblConexion.Invoke(() => MostrarEstadoAdmin(hayServidor, pendientes));
            else
                MostrarEstadoAdmin(hayServidor, pendientes);
        }

        private void MostrarEstadoAdmin(bool conectado, int pendientes)
        {
            if (!conectado && pendientes > 0)
            {
                lblConexion.Text = $"● Sin servidor — {pendientes} pendiente(s)";
                lblConexion.ForeColor = Color.FromArgb(113, 63, 18);
                lblConexion.BackColor = Color.FromArgb(254, 249, 195); // amarillo
            }
            else if (!conectado)
            {
                lblConexion.Text = "● Sin conexión al servidor";
                lblConexion.ForeColor = Color.FromArgb(127, 29, 29);
                lblConexion.BackColor = Color.FromArgb(254, 226, 226); // rojo
            }
            else if (pendientes > 0)
            {
                lblConexion.Text = $"● {pendientes} registro(s) pendiente(s)";
                lblConexion.ForeColor = Color.FromArgb(113, 63, 18);
                lblConexion.BackColor = Color.FromArgb(254, 249, 195); // amarillo
            }
            else
            {
                lblConexion.Text = "● Servidor en linea";
                lblConexion.ForeColor = Color.FromArgb(22, 101, 52);
                lblConexion.BackColor = Color.FromArgb(220, 252, 231); // verde
            }
        }

        private void cmb_parroquia_TextChanged(object sender, EventArgs e)
        {
            if (_filtrandoAdmin || _cargandoAdmin) return;
            _filtrandoAdmin = true;
            try
            {
                string busqueda = cmb_parroquia.Text;
                if (_dtParroquiasAdmin == null) return;

                if (string.IsNullOrWhiteSpace(busqueda))
                    _dtParroquiasAdmin.DefaultView.RowFilter = "";
                else
                    _dtParroquiasAdmin.DefaultView.RowFilter = "Parroquia_nombre LIKE '%"
                        + busqueda.Replace("'", "''") + "%'";

                cmb_parroquia.Text = busqueda;
                cmb_parroquia.SelectionStart = busqueda.Length;
                cmb_parroquia.DroppedDown = true; // Volver al original sin BeginInvoke
            }
            finally
            {
                _filtrandoAdmin = false;
            }
        }
    }
}
