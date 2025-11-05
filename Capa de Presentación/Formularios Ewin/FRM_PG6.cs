using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG6 : Form
    {
        private clsCRUD_Usuarios crudUsuarios;
        private bool modoEdicion = false;
        private int usuarioIdSeleccionado = 0;
        private BindingSource bindingSource;


        public FRM_PG6()
        {
            InitializeComponent();
            crudUsuarios = new clsCRUD_Usuarios();
            bindingSource = new BindingSource();

        }

        private void FRM_PG6_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboBoxes();
            LimpiarCampos();
            HabilitarControles(false);
        }



        private void CargarDatos()
        {
            try
            {
                dgvUsuarios.DataSource = crudUsuarios.ObtenerUsuarios();

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

        private void CargarComboBoxes()
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

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("El usuario es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("La contraseña es requerida", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return false;
            }

            return true;
        }

        // Limpiar campos
        private void LimpiarCampos()
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
            modoEdicion = false;
        }

        private void HabilitarControles(bool habilitar)
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

        // ===== BOTONES =====

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarControles(true);
            modoEdicion = false;

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


        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para modificar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar el ID del usuario seleccionado
            usuarioIdSeleccionado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["ID"].Value);

            HabilitarControles(true);
            modoEdicion = true;
            CargarDatosUsuario();
            txtNombre.Focus();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos())
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

                if (modoEdicion)
                {
                    bool resultado = crudUsuarios.ModificarUsuario(
                        usuarioIdSeleccionado, nombre, apellido,
                        correo, usuario, password, idRol, idParroquia, idEstado);

                    if (resultado)
                    {
                        MessageBox.Show("Usuario modificado exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos();
                        LimpiarCampos();
                        HabilitarControles(false);
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
                        CargarDatos();
                        LimpiarCampos();
                        HabilitarControles(false);
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

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar este usuario? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["ID"].Value);
                    bool resultado = crudUsuarios.EliminarUsuario(id);

                    if (resultado)
                    {
                        MessageBox.Show("Usuario eliminado exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnInhabilitar_Click_1(object sender, EventArgs e)
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
                    CargarDatos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inhabilitar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRol_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
                    CargarDatos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
