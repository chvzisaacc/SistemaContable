using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Luiss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class Ventana_Principal_Administrador : Form
    {
        //usuarios
        private clsCRUD_Usuarios crudUsuarios;
        private bool modoEdicionUsuario = false;
        private int usuarioIdSeleccionado = 0;
        private BindingSource bindingSource;
        private int idParroquia;
        private int _usuarioID;

        //Catalogo
        private clsCRUD_CatalogoCuentas crudCatalogoCuentas;
        private bool modoEdicionCatalogo = false;
        private int codigoCuentaSeleccionado = 0;
        //BITACORA
        private clsCRUD_Historial crudHistorial;
        //Validaciones
       private ClsValidaciones Validaciones;

        ClsCerrar cerrar = new ClsCerrar();

     

        public Ventana_Principal_Administrador(int usuarioID, int idParroquia)
        {
            InitializeComponent();
            this._usuarioID = usuarioID;
            this.idParroquia = idParroquia;
            UsuarioLogueado.UsuarioId = usuarioID;
            UsuarioLogueado.ParroquiaId = idParroquia;

            this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelUsuario);
            panelContenedor.Controls.Add(panelCatalogoCuentas);


            // Opcional: muestra uno por defecto
            MostrarSoloEstePanel(panel1);

            //usuarios
            crudUsuarios = new clsCRUD_Usuarios();

            //catalogo
            crudCatalogoCuentas = new clsCRUD_CatalogoCuentas();

            //bitacora
            crudHistorial = new clsCRUD_Historial();
            //validaciones
                        Validaciones = new ClsValidaciones();
            //Para busqueda de usuarios
            bindingSource = new BindingSource();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

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
            HabilitarControlesCatalogo(false);
            CargarComboBoxCuentas();
            //ValidarCamposCatalogo();
        }

        //usuarios
        private void CargarDatosUsuarioDGV()
        {
            try
            {
                DataTable dt = crudUsuarios.ObtenerUsuarios();
                bindingSource.DataSource = dt;
                dgvUsuarios.DataSource = bindingSource;

                //aqui es para ocultar algunos campos (los ids y las contraseñas)

                if (dgvUsuarios.Columns["Contraseña"] != null)
                    dgvUsuarios.Columns["Contraseña"].Visible = false;

                if (dgvUsuarios.Columns["RolID"] != null)
                    dgvUsuarios.Columns["RolID"].Visible = false;
                if (dgvUsuarios.Columns["ParroquiaID"] != null)
                    dgvUsuarios.Columns["ParroquiaID"].Visible = false;
                if (dgvUsuarios.Columns["EstadoID"] != null)
                    dgvUsuarios.Columns["EstadoID"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboBoxesUsuario()
        {
            try
            {
                cmbRol.DataSource = crudUsuarios.ObtenerRoles();
                cmbRol.DisplayMember = "Rol_descripcion";
                cmbRol.ValueMember = "Rol_Id";

                cmbParroquia.DataSource = crudUsuarios.ObtenerParroquias();
                cmbParroquia.DisplayMember = "Parroquia_nombre";
                cmbParroquia.ValueMember = "Parroquia_id";

                cmbEstado.DataSource = crudUsuarios.ObtenerEstados();
                cmbEstado.DisplayMember = "descripcion";
                cmbEstado.ValueMember = "Id_estado_cuenta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosUsuario()
        {
            try
            {
                var usuario = crudUsuarios.BuscarUsuarioPorId(usuarioIdSeleccionado);

                if (usuario != null)
                {
                    txtId.Text = usuario["Usuario_id"].ToString();
                    txtNombre.Text = usuario["usuario_nombre"].ToString();
                    txtApellido.Text = usuario["usuario_apellido"].ToString();
                    txtCorreo.Text = usuario["usuario_correo"] != DBNull.Value
                        ? usuario["usuario_correo"].ToString()
                        : "";
                    txtUsuario.Text = usuario["usuario"].ToString();
                    txtContraseña.Text = usuario["usuario_password"].ToString();
                    cmbRol.SelectedValue = usuario["Rol_Id"];
                    cmbParroquia.SelectedValue = usuario["Parroquia_Id"];
                    cmbEstado.SelectedValue = usuario["Id_estado_cuenta"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuario: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCamposUsuario()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            // Nombre
            if (string.IsNullOrWhiteSpace(nombre) || !val.EsTextoValido(nombre))
            {
                MessageBox.Show("El nombre es requerido y solo puede contener letras.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Apellido
            if (string.IsNullOrWhiteSpace(apellido) || !val.EsTextoValido(apellido))
            {
                MessageBox.Show("El apellido es requerido y solo puede contener letras.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            // Correo
            if (string.IsNullOrWhiteSpace(correo) || !val.EsCorreoValido(correo))
            {
                MessageBox.Show("El correo es requerido y debe tener un formato válido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            // Usuario
            if (string.IsNullOrWhiteSpace(usuario) || !val.EsUsuarioValido(usuario))
            {
                MessageBox.Show("El nombre de usuario es requerido y solo puede tener letras, números, puntos o guiones bajos.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            // Contraseña
            if (string.IsNullOrWhiteSpace(contraseña) || !val.EsContraseñaValida(contraseña))
            {
                MessageBox.Show("La contraseña es requerida y debe tener entre 4 y 25 caracteres.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return false;
            }

            // Combo Rol
            if (cmbRol.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un rol.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRol.Focus();
                return false;
            }

            // Combo Parroquia
            if (cmbParroquia.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una parroquia.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbParroquia.Focus();
                return false;
            }

            // Combo Estado
            if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un estado de cuenta.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEstado.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCamposUsuario()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCorreo.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();

            if (cmbRol.Items.Count > 0)
                cmbRol.SelectedIndex = 0;
            if (cmbParroquia.Items.Count > 0)
                cmbParroquia.SelectedIndex = 0;
            if (cmbEstado.Items.Count > 0)
                cmbEstado.SelectedIndex = 0;

            usuarioIdSeleccionado = 0;
            modoEdicionUsuario = false;
        }

        private void HabilitarControlesUsuario(bool habilitar)
        {
            txtId.Enabled = false;
            txtNombre.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtCorreo.Enabled = habilitar;
            txtUsuario.Enabled = habilitar;
            txtContraseña.Enabled = habilitar;
            cmbRol.Enabled = habilitar;
            cmbParroquia.Enabled = habilitar;
            cmbEstado.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelUsuario);
        }

        private void btnCatalagoCuenta_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCatalogoCuentas);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposCatalogo())
                return;

            try
            {
                int idCuenta = Convert.ToInt32(cmbCuenta.SelectedValue);
                string nombreCuenta = txtNombreCuenta.Text.Trim();
                string detalle = txtDetalle.Text.Trim();

                if (modoEdicionCatalogo)
                {
                    // MODIFICAR cuenta existente
                    bool resultado = crudCatalogoCuentas.ModificarCatalogoCuenta(
                        codigoCuentaSeleccionado,
                        idCuenta,
                        nombreCuenta,
                        detalle,
                        null // saldo siempre null
                    );

                    if (resultado)
                    {
                        MessageBox.Show("Cuenta modificada exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            crudHistorial.RegistrarActividad(
                                1,
                                8,
                                "Modificación de Cuenta",
                                $"Se modificó la cuenta: '{nombreCuenta}' (Código: {codigoCuentaSeleccionado})."
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
                    if (crudCatalogoCuentas.CatalogoCuentaExiste(nombreCuenta))
                    {
                        MessageBox.Show("El nombre de la cuenta ya existe", "Advertencia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nuevoCodigo = crudCatalogoCuentas.AgregarCatalogoCuenta(
                        idCuenta,
                        nombreCuenta,
                        detalle,
                        null // saldo siempre null
                    );

                    if (nuevoCodigo > 0)
                    {
                        MessageBox.Show($"Cuenta agregada exitosamente con código: {nuevoCodigo}",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            crudHistorial.RegistrarActividad(
                                1,
                                8,
                                "Creación de Cuenta",
                                $"Se creó la nueva cuenta: '{nombreCuenta}' (Código: {nuevoCodigo})."
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

        private void CargarComboBoxCuentas()
        {
            try
            {
                cmbCuenta.DataSource = crudCatalogoCuentas.ObtenerCuentas();
                cmbCuenta.DisplayMember = "descripcion";
                cmbCuenta.ValueMember = "id_cuenta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuentas: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosCatalogoCuenta()
        {
            try
            {
                var cuenta = crudCatalogoCuentas.BuscarCatalogoCuentaPorId(codigoCuentaSeleccionado);

                if (cuenta != null)
                {
                    txtIdCuenta.Text = cuenta["Cod_cuenta"].ToString();
                    txtNombreCuenta.Text = cuenta["nombre_cuenta"].ToString();
                    txtDetalle.Text = cuenta["detalle"] != DBNull.Value
                        ? cuenta["detalle"].ToString()
                        : "";
                    cmbCuenta.SelectedValue = cuenta["id_cuenta"];
                    cmbTipoCuenta.SelectedValue = cuenta["cod_tipo"]; // Ajusta según tu stored procedure
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuenta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevaCuenta_Click(object sender, EventArgs e)
        {
            LimpiarCamposCatalogo();
            HabilitarControlesCatalogo(true);
            modoEdicionCatalogo = false; // Debes agregar esta variable global

            try
            {
                int proximoCodigo = crudCatalogoCuentas.ObtenerProximoCodigo();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCamposUsuario();
            HabilitarControlesUsuario(true);
            modoEdicionUsuario = false;

            try
            {
                int proximoId = crudUsuarios.ObtenerProximoId();
                txtId.Text = proximoId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ID: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtNombre.Focus();
        }



        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposUsuario())
                return;

            try
            {
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string usuario = txtUsuario.Text.Trim();
                string password = txtContraseña.Text.Trim();
                int idRol = Convert.ToInt32(cmbRol.SelectedValue);
                int idParroquia = Convert.ToInt32(cmbParroquia.SelectedValue);
                int idEstado = Convert.ToInt32(cmbEstado.SelectedValue);

                if (modoEdicionUsuario)
                {
                    bool resultado = crudUsuarios.ModificarUsuario(
                        usuarioIdSeleccionado, nombre, apellido,
                        correo, usuario, password, idRol, idParroquia, idEstado);

                    if (resultado)
                    {
                        MessageBox.Show("Usuario modificado exitosamente", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            crudHistorial.RegistrarActividad(
                                1, // CAMBIAR por Sesion.UsuarioID
                                7, // Módulo de Usuarios
                                "Modificación de Usuario",
                                $"Se modificaron los datos del usuario: '{usuario}' (ID: {usuarioIdSeleccionado})."
                            );
                        }
                        catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }
                        // --- FIN DEL NUEVO CÓDIGO ---

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
                    if (crudUsuarios.UsuarioExiste(usuario))
                    {
                        MessageBox.Show("El nombre de usuario ya existe", "Advertencia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nuevoId = crudUsuarios.AgregarUsuario(nombre, apellido, correo,
                        usuario, password, idRol, idParroquia, idEstado);

                    if (nuevoId > 0)
                    {
                        MessageBox.Show($"Usuario agregado exitosamente con ID: {nuevoId}", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // --- **NUEVO** REGISTRO DE BITÁCORA (AGREGAR) ---
                        try
                        {
                            crudHistorial.RegistrarActividad(
                                1, // CAMBIAR por Sesion.UsuarioID
                                7, // Módulo de Usuarios
                                "Creación de Usuario",
                                $"Se creó el nuevo usuario: '{usuario}' (ID: {nuevoId})."
                            );
                        }
                        catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }
                        // --- FIN DEL NUEVO CÓDIGO ---

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

        private void panelUsuario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnModificarCuentaUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para modificar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar el ID del usuario seleccionado
            usuarioIdSeleccionado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["ID"].Value);

            HabilitarControlesUsuario(true);
            modoEdicionUsuario = true;
            CargarDatosUsuario();
            txtNombre.Focus();
        }

        private void btnInhabilitarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para inhabilitar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["ID"].Value);
                int estadoInactivo = 2; // Ajustar según tu BD

                bool resultado = crudUsuarios.InhabilitarUsuario(id, estadoInactivo);

                if (resultado)
                {
                    MessageBox.Show("Usuario inhabilitado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // --- **NUEVO** REGISTRO DE BITÁCORA (INHABILITAR) ---
                    try
                    {
                        crudHistorial.RegistrarActividad(
                            1, // CAMBIAR por Sesion.UsuarioID
                            7, // Módulo de Usuarios
                            "Inhabilitación de Usuario",
                            $"Se inhabilitó al usuario con ID: {id}."
                        );
                    }
                    catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }
                    // --- FIN DEL NUEVO CÓDIGO ---

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

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para habilitar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["ID"].Value);
                int estadoInactivo = 1; // Ajustar según tu BD

                bool resultado = crudUsuarios.InhabilitarUsuario(id, estadoInactivo);

                if (resultado)
                {
                    MessageBox.Show("Usuario habilitado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- **NUEVO** REGISTRO DE BITÁCORA (HABILITAR) ---
                    try
                    {
                        crudHistorial.RegistrarActividad(
                            1, // CAMBIAR por Sesion.UsuarioID
                            7, // Módulo de Usuarios
                            "Habilitación de Usuario",
                            $"Se habilitó al usuario con ID: {id}."
                        );
                    }
                    catch (Exception exBitacora) { Console.WriteLine("Error de Bitácora: " + exBitacora.Message); }
                    // --- FIN DEL NUEVO CÓDIGO ---

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

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            LimpiarCamposUsuario();
            HabilitarControlesUsuario(true);
            modoEdicionUsuario = false;

            try
            {
                int proximoId = crudUsuarios.ObtenerProximoId();
                txtId.Text = proximoId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ID: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtNombre.Focus();
        }

        //catalogo
        private void CargarDatosCatalogoDGV()
        {
            try
            {
                dgvCatalogoCuentas.DataSource = crudCatalogoCuentas.ObtenerCatalogoCuentas();

                if (dgvCatalogoCuentas.Columns["CuentaID"] != null)
                    dgvCatalogoCuentas.Columns["CuentaID"].Visible = false;

                /*
                if (dgvCatalogoCuentas.Columns["Detalle"] != null)
                    dgvCatalogoCuentas.Columns["Detalle"].DefaultCellStyle.Format = "N2";
                */

                if (dgvCatalogoCuentas.Columns["Saldo"] != null)
                    dgvCatalogoCuentas.Columns["Saldo"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string nombreColumnaAOcultar = "Saldo";

            if (dgvCatalogoCuentas.Columns.Contains(nombreColumnaAOcultar))
            {
                dgvCatalogoCuentas.Columns[nombreColumnaAOcultar].Visible = false;
            }
        }

        private void CargarComboBoxTipoTransaccion()
        {
            try
            {
                cmbTipoCuenta.DataSource = crudCatalogoCuentas.ObtenerTipoTransaccion();
                cmbTipoCuenta.DisplayMember = "descripcion";
                cmbTipoCuenta.ValueMember = "Cod_tipo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de transaccion: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void LimpiarCamposCatalogo()
        {
            txtIdCuenta.Clear();
            txtNombreCuenta.Clear();
            txtDetalle.Clear();

            if (cmbCuenta.Items.Count > 0)
                cmbCuenta.SelectedIndex = 0;

            if (cmbTipoCuenta.Items.Count > 0)
                cmbTipoCuenta.SelectedIndex = 0;
        }

        private void HabilitarControlesCatalogo(bool habilitar)
        {
            txtIdCuenta.Enabled = false;
            cmbCuenta.Enabled = habilitar;
            txtNombreCuenta.Enabled = habilitar;
            txtDetalle.Enabled = habilitar;
            cmbTipoCuenta.Enabled = habilitar;
            btnGuardarCuenta.Enabled = habilitar;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textoBusqueda = txtBuscar.Text.Trim();

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

        private void dgvCatalogoCuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FRM_ServiciosAdministrador popup = new FRM_ServiciosAdministrador();
            popup.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Cerrar_Sesión popup = new Cerrar_Sesión();
            var buttonScreenPosition = pictureBox4.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox4.Height);
            popup.ShowDialog();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
