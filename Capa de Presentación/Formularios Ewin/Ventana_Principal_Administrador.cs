using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Luiss;
using System.Data;


namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// 
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
        /// <summary>
        /// The codigo cuenta seleccionado
        /// </summary>
        private int codigo_cuenta_seleccionado = 0;
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



        /// <summary>
        /// Initializes a new instance of the <see cref="Ventana_Principal_Administrador"/> class.
        /// </summary>
        /// <param name="usuarioID">The usuario identifier.</param>
        /// <param name="idParroquia">The identifier parroquia.</param>
        public Ventana_Principal_Administrador(int usuarioID, int idParroquia)
        {
            InitializeComponent();
            this._usuario_id = usuarioID;
            this.id_parroquia = idParroquia;
            UsuarioLogueado.usuario_id = usuarioID;
            UsuarioLogueado.parroquia_id = idParroquia;

            this.FormClosing += cerrar.CerrarApp;
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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
        /// Handles the Click event of the label2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label2_Click(object sender, EventArgs e)
        {

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
            CargarComboBoxTipoTransaccion();
            LimpiarCamposCatalogo();
            CargarComboBoxEstadoCuenta();
            HabilitarControlesCatalogo(false);
            CargarComboBoxCuentas();
            //ValidarCamposCatalogo();
        }

        //usuarios
        /// <summary>
        /// Cargars the datos usuario DGV.
        /// </summary>
        private void CargarDatosUsuarioDGV()
        {
            try
            {
                DataTable dt = crud_usuarios.ObtenerUsuarios();
                bindingSource.DataSource = dt;
                dgv_usuarios.DataSource = bindingSource;

                //aqui es para ocultar algunos campos (los ids y las contraseñas)

                if (dgv_usuarios.Columns["Contraseña"] != null)
                    dgv_usuarios.Columns["Contraseña"].Visible = false;

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
                cmbEstadoCuenta.DataSource = crud_usuarios.ObtenerEstados();
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

                cmb_parroquia.DataSource = crud_usuarios.ObtenerParroquias();
                cmb_parroquia.DisplayMember = "Parroquia_nombre";
                cmb_parroquia.ValueMember = "Parroquia_id";

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
            if (string.IsNullOrWhiteSpace(usuario) || !val.EsUsuarioValido(usuario))
            {
                MessageBox.Show("El nombre de usuario es requerido y solo puede tener letras, números, puntos o guiones bajos.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_usuario.Focus();
                return false;
            }

            // Contraseña
            if (string.IsNullOrWhiteSpace(contraseña) || !val.EsContraseñaValida(contraseña))
            {
                MessageBox.Show("La contraseña es requerida y debe tener entre 4 y 25 caracteres.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_contraseña.Focus();
                return false;
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

            ClsValidaciones validaciones = new ClsValidaciones();

            // Validamos el campo "Nombre"
            if (!validaciones.ValidarEspacios(txtNombreCuenta.Text)) // Se asume que txtNombre es el campo de texto para el nombre
            {
                return; // Si no es válido, detiene la ejecución
            }

            if (!ValidarCamposCatalogo())
                return;

            try
            {
                int id_cuenta = Convert.ToInt32(cmbCuenta.SelectedValue);
                string nombre_cuenta = txtNombreCuenta.Text.Trim();
                string detalle = txtDetalle.Text.Trim();
                int id_estado = Convert.ToInt32(cmbEstadoCuenta.SelectedValue);


                if (modo_edicion_catalogo)
                {
                    // MODIFICAR cuenta existente
                    bool resultado = crud_catalogo_cuentas.ModificarCatalogoCuenta(
                        codigo_cuenta_seleccionado,
                        id_cuenta,
                        nombre_cuenta,
                        detalle,
                        null // saldo siempre null
                    );

                    if (resultado)
                    {
                        MessageBox.Show("Cuenta modificada exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            crud_historial.RegistrarActividad(
                                1,
                                8,
                                "Modificación de Cuenta",
                                $"Se modificó la cuenta: '{nombre_cuenta}' (Código: {codigo_cuenta_seleccionado})."
                            );
                        }
                        catch (Exception exBitacora)
                        {
                            Console.WriteLine("Error de Bitácora: " + exBitacora.Message);
                        }

                        CargarDatosCatalogoDGV();
                        LimpiarCamposCatalogo();
                        HabilitarControlesCatalogo(false);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar la cuenta", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // AGREGAR nueva cuenta
                    if (crud_catalogo_cuentas.CatalogoCuentaExiste(nombre_cuenta))
                    {
                        MessageBox.Show("El nombre de la cuenta ya existe", "Advertencia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nuevoCodigo = crud_catalogo_cuentas.AgregarCatalogoCuenta(
                        id_cuenta,
                        nombre_cuenta,
                        detalle,
                        null, // saldo siempre null
                        id_estado
                    );

                    if (nuevoCodigo > 0)
                    {
                        MessageBox.Show($"Cuenta agregada exitosamente con código: {nuevoCodigo}",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            crud_historial.RegistrarActividad(
                                1,
                                8,
                                "Creación de Cuenta",
                                $"Se creó la nueva cuenta: '{nombre_cuenta}' (Código: {nuevoCodigo})."
                            );
                        }
                        catch (Exception exBitacora)
                        {
                            Console.WriteLine("Error de Bitácora: " + exBitacora.Message);
                        }

                        CargarDatosCatalogoDGV();
                        LimpiarCamposCatalogo();
                        HabilitarControlesCatalogo(false);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar la cuenta", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la cuenta: " + ex.Message + "\n\nDetalle: " + ex.InnerException?.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cargars the ComboBox cuentas.
        /// </summary>
        private void CargarComboBoxCuentas()
        {
            try
            {
                cmbCuenta.DataSource = crud_catalogo_cuentas.ObtenerCuentas();
                cmbCuenta.DisplayMember = "descripcion";
                cmbCuenta.ValueMember = "id_cuenta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuentas: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cargars the datos catalogo cuenta.
        /// </summary>
        private void CargarDatosCatalogoCuenta()
        {
            try
            {
                var cuenta = crud_catalogo_cuentas.BuscarCatalogoCuentaPorId(codigo_cuenta_seleccionado);

                if (cuenta != null)
                {
                    txtIdCuenta.Text = cuenta["Cod_cuenta"].ToString();
                    txtNombreCuenta.Text = cuenta["nombre_cuenta"].ToString();
                    txtDetalle.Text = cuenta["detalle"] != DBNull.Value
                        ? cuenta["detalle"].ToString()
                        : "";
                    cmbCuenta.SelectedValue = cuenta["id_cuenta"];
                    cmbTipoCuenta.SelectedValue = cuenta["cod_tipo"];
                    cmbEstadoCuenta.SelectedValue = cuenta["Id_estado_cuenta"]; // AGREGAR ESTA LÍNEA
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuenta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnNuevaCuenta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNuevaCuenta_Click(object sender, EventArgs e)
        {
            LimpiarCamposCatalogo();
            HabilitarControlesCatalogo(true);
            modo_edicion_catalogo = false; // Debes agregar esta variable global

            try
            {
                int proximoCodigo = crud_catalogo_cuentas.ObtenerProximoCodigo();
                txtIdCuenta.Text = proximoCodigo.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener código: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtNombreCuenta.Focus();
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

            // Validar los espacios en los campos de texto
            if (!validaciones.ValidarEspacios(txt_nombreCuenta.Text)
                || !validaciones.ValidarEspacios(txt_apellido.Text)
                || !validaciones.ValidarEspacios(txt_contraseña.Text))
            {
                return; // Si algún campo tiene más de tres espacios, se detiene la ejecución
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
                                Sesion1.usuario_id,
                                7,
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
                                Sesion1.usuario_id,
                                7,
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
                dgvCatalogoCuentas.DataSource = crud_catalogo_cuentas.ObtenerCatalogoCuentas();

                // Ocultar columna EstadoID
                if (dgvCatalogoCuentas.Columns["EstadoID"] != null)
                    dgvCatalogoCuentas.Columns["EstadoID"].Visible = false;

                // Formato para saldo
                if (dgvCatalogoCuentas.Columns["Saldo"] != null)
                    dgvCatalogoCuentas.Columns["Saldo"].DefaultCellStyle.Format = "N2";

                // Ocultar columna Saldo
                string nombreColumnaAOcultar = "Saldo";
                if (dgvCatalogoCuentas.Columns.Contains(nombreColumnaAOcultar))
                {
                    dgvCatalogoCuentas.Columns[nombreColumnaAOcultar].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cargars the ComboBox tipo transaccion.
        /// </summary>
        private void CargarComboBoxTipoTransaccion()
        {
            try
            {
                cmbTipoCuenta.DataSource = crud_catalogo_cuentas.ObtenerTipoTransaccion();
                cmbTipoCuenta.DisplayMember = "descripcion";
                cmbTipoCuenta.ValueMember = "Cod_tipo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de transaccion: " + ex.Message, "Error",
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

            string codigo = txtIdCuenta.Text.Trim();
            string nombreCuenta = txtNombreCuenta.Text.Trim();
            string detalle = txtDetalle.Text.Trim();

            // Código: verificamos que venga generado
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("No se generó el código de la cuenta. Vuelva a intentar.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Nombre de cuenta
            if (string.IsNullOrWhiteSpace(nombreCuenta) || !val.EsTextoValido(nombreCuenta))
            {
                MessageBox.Show("El nombre de la cuenta es requerido y solo puede contener letras y espacios.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCuenta.Focus();
                return false;
            }

            // Cuenta padre (ComboBox)
            if (cmbCuenta.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una cuenta padre.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuenta.Focus();
                return false;
            }

            // Detalle
            if (string.IsNullOrWhiteSpace(detalle) || !val.EsTextoValido(detalle))
            {
                MessageBox.Show("El detalle es requerido y solo puede contener letras y espacios.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDetalle.Focus();
                return false;
            }

            // Tipo de cuenta
            if (cmbTipoCuenta.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de cuenta.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoCuenta.Focus();
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

            if (cmbCuenta.Items.Count > 0)
                cmbCuenta.SelectedIndex = 0;

            if (cmbTipoCuenta.Items.Count > 0)
                cmbTipoCuenta.SelectedIndex = 0;

            if (cmbEstadoCuenta.Items.Count > 0)
                cmbEstadoCuenta.SelectedIndex = 0;

            codigo_cuenta_seleccionado = 0;
            modo_edicion_catalogo = false;
        }

        /// <summary>
        /// Habilitars the controles catalogo.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void HabilitarControlesCatalogo(bool habilitar)
        {
            txtIdCuenta.Enabled = false;
            cmbCuenta.Enabled = habilitar;
            txtNombreCuenta.Enabled = habilitar;
            txtDetalle.Enabled = habilitar;
            cmbTipoCuenta.Enabled = habilitar;
            cmbEstadoCuenta.Enabled = habilitar;
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
        /// Handles the CellContentClick event of the dgvCatalogoCuentas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvCatalogoCuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handles the DataBindingComplete event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbParroquia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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
            // Ocultar el formulario principal
            this.Hide(); // Ocultar, NO cerrar

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
                MessageBox.Show("Seleccione una cuenta para habilitar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int codigo = Convert.ToInt32(dgvCatalogoCuentas.CurrentRow.Cells["Codigo"].Value);
                int estadoActivo = 1; // Estado Activo

                bool resultado = crud_catalogo_cuentas.CambiarEstadoCuenta(codigo, estadoActivo);

                if (resultado)
                {
                    MessageBox.Show("Cuenta habilitada exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        crud_historial.RegistrarActividad(
                            UsuarioLogueado.usuario_id,
                            8,
                            "Habilitación de Cuenta",
                            $"Se habilitó la cuenta con código: {codigo}."
                        );
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
                MessageBox.Show("Seleccione una cuenta para inhabilitar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int codigo = Convert.ToInt32(dgvCatalogoCuentas.CurrentRow.Cells["Codigo"].Value);
                int estadoInactivo = 2; // Estado Inactivo

                bool resultado = crud_catalogo_cuentas.CambiarEstadoCuenta(codigo, estadoInactivo);

                if (resultado)
                {
                    MessageBox.Show("Cuenta inhabilitada exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        crud_historial.RegistrarActividad(
                            UsuarioLogueado.usuario_id,
                            8,
                            "Inhabilitación de Cuenta",
                            $"Se inhabilitó la cuenta con código: {codigo}."
                        );
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

        private void label8_Click(object sender, EventArgs e)
        {

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
    }
}
