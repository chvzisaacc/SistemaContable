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
        private clsCRUD_Usuarios crud_usuarios;
        private bool modo_edicion_usuario = false;
        private int usuario_id_seleccionado = 0;
        private BindingSource bindingSource;
        private int id_parroquia;
        private int _usuario_id;

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

        private void CargarDatosUsuario()
        {
            try
            {
                var usuario = crud_usuarios.BuscarUsuarioPorId(usuario_id_seleccionado);

                if (usuario != null)
                {
                    txt_id.Text = usuario["Usuario_id"].ToString();
                    txt_nombre.Text = usuario["usuario_nombre"].ToString();
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

        private bool ValidarCamposUsuario()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string nombre = txt_nombre.Text.Trim();
            string apellido = txt_apellido.Text.Trim();
            string correo = txt_correo.Text.Trim();
            string usuario = txt_usuario.Text.Trim();
            string contraseña = txt_contraseña.Text.Trim();

            // Nombre
            if (string.IsNullOrWhiteSpace(nombre) || !val.EsTextoValido(nombre))
            {
                MessageBox.Show("El nombre es requerido y solo puede contener letras.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre.Focus();
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

        private void LimpiarCamposUsuario()
        {
            txt_id.Clear();
            txt_nombre.Clear();
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

        private void HabilitarControlesUsuario(bool habilitar)
        {
            txt_id.Enabled = false;
            txt_nombre.Enabled = habilitar;
            txt_apellido.Enabled = habilitar;
            txt_correo.Enabled = habilitar;
            txt_usuario.Enabled = habilitar;
            txt_contraseña.Enabled = habilitar;
            cmb_rol.Enabled = habilitar;
            cmb_parroquia.Enabled = habilitar;
            cmb_estado.Enabled = habilitar;
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

            txt_nombre.Focus();
        }



        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposUsuario())
                return;

            try
            {
                string nombre = txt_nombre.Text.Trim();
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
                            crudHistorial.RegistrarActividad(
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
                            crudHistorial.RegistrarActividad(
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

        private void panelUsuario_Paint(object sender, PaintEventArgs e)
        {

        }

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
            txt_nombre.Focus();
        }

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
                        crudHistorial.RegistrarActividad(
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
                        crudHistorial.RegistrarActividad(
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

            txt_nombre.Focus();
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
