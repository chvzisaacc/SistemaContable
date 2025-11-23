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
    public partial class FRM_PG5 : Form
    {
        //usuarios
        private clsCRUD_Usuarios crudUsuarios;
        private bool modoEdicionUsuario = false;
        private int usuarioIdSeleccionado = 0;
        private BindingSource bindingSource;

        //Catalogo
        private clsCRUD_CatalogoCuentas crudCatalogoCuentas;
        //BITACORA
        private clsCRUD_Historial crudHistorial;
        //Validaciones
       private ClsValidaciones Validaciones;

        ClsCerrar cerrar = new ClsCerrar();
        private int idParroquia;

     

        public FRM_PG5(int usuarioID)
        {
            InitializeComponent();
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

        public FRM_PG5(int usuarioID, int idParroquia) : this(usuarioID)
        {
            this.idParroquia = idParroquia;
        }

        public FRM_PG5(string userName, int userId) : this(0, 0)
        {

        }

        public FRM_PG5()
        {
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
            ValidarCamposCatalogo();
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
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();

                if (!Validaciones.EsTextoValido(txtNombre.Text))
                {
                    MessageBox.Show("El nombre solo puede contener letras.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return false;
                }

            }

           

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                if (!Validaciones.EsTextoValido(txtApellido.Text))
                {
                    MessageBox.Show("El apellido solo puede contener letras.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return false;
                }

            }

           

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                if (!Validaciones.EsCorreoValido(txtCorreo.Text))
                {
                    MessageBox.Show("El formato del correo no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return false;
                }

            }

            

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El usuario es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                if (string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    MessageBox.Show("La contraseña es requerida", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Focus();
                    return false;
                }
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

        }

        private void btnNuevaCuenta_Click(object sender, EventArgs e)
        {
            LimpiarCamposCatalogo();
            HabilitarControlesCatalogo(true);

            try
            {
                int proximoCodigo = crudCatalogoCuentas.ObtenerProximoCodigo();
                txtId.Text = proximoCodigo.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener código: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtNombre.Focus();
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
            if (string.IsNullOrWhiteSpace(txtIdCuenta.Text))
            {
                MessageBox.Show("El codigo de la cuenta es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCuenta.Focus();
                if (!Validaciones.EsNumeroDecimal(txtNombreCuenta.Text))
                {
                    MessageBox.Show("El codigo de la cuenta tiene caracteres inválidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreCuenta.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtNombreCuenta.Text))
            {
                MessageBox.Show("El nombre de la cuenta es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCuenta.Focus();
                
                if (!Validaciones.EsTextoValido(txtNombreCuenta.Text))
                {
                    MessageBox.Show("El nombre de la cuenta tiene caracteres inválidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreCuenta.Focus();

                    return false;
                }

            }
            

            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                MessageBox.Show("El campo Cuenta es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();

                if (!Validaciones.EsTextoValido(txtCuenta.Text))
                {
                    MessageBox.Show("La cuenta solo puede contener letras.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtDetalle.Text))
            {
                MessageBox.Show("El campo detalle es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();

                if (!Validaciones.EsTextoValido(txtCuenta.Text))
                {
                    MessageBox.Show("El detalle solo puede contener letras.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtSaldo.Text) && !Validaciones.EsNumeroDecimal(txtSaldo.Text))
            {
                MessageBox.Show("El saldo debe ser un número válido (Ej: 100.00).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSaldo.Focus();
                return false;
            }

            if (cmbTipoCuenta.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de cuenta", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoCuenta.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCamposCatalogo()
        {
            txtId.Clear();
            txtCuenta.Clear();
            txtNombreCuenta.Clear();
            txtDetalle.Clear();
            txtSaldo.Clear();

            if (cmbTipoCuenta.Items.Count > 0)
                cmbTipoCuenta.SelectedIndex = 0;
        }

        private void HabilitarControlesCatalogo(bool habilitar)
        {
            txtId.Enabled = false;
            txtCuenta.Enabled = habilitar;
            txtNombreCuenta.Enabled = habilitar;
            txtDetalle.Enabled = habilitar;
            txtSaldo.Enabled = habilitar;
            cmbTipoCuenta.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
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
            FRM_CERRARSESION popup = new FRM_CERRARSESION();
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
