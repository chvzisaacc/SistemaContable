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
    public partial class FRM_PG7 : Form
    {
        private clsCRUD_CatalogoCuentas crudCatalogoCuentas;
        private bool modoEdicion = false;
        private int codCuentaSeleccionado = 0;

        public FRM_PG7()
        {
            InitializeComponent();
            crudCatalogoCuentas = new clsCRUD_CatalogoCuentas();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG7_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboBoxTipoCuenta();
            LimpiarCampos();
            HabilitarControles(false);
        }

        private void CargarDatos()
        {
            try
            {
                dgvCatalogoCuentas.DataSource = crudCatalogoCuentas.ObtenerCatalogoCuentas();

                // Opcional: Ocultar la columna CuentaID si solo quieres mostrar el nombre
                if (dgvCatalogoCuentas.Columns["CuentaID"] != null)
                    dgvCatalogoCuentas.Columns["CuentaID"].Visible = false;

                // Formatear columnas de tipo decimal a 2 decimales
                if (dgvCatalogoCuentas.Columns["Detalle"] != null)
                    dgvCatalogoCuentas.Columns["Detalle"].DefaultCellStyle.Format = "N2";

                if (dgvCatalogoCuentas.Columns["Saldo"] != null)
                    dgvCatalogoCuentas.Columns["Saldo"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboBoxTipoCuenta()
        {
            try
            {
                // Cargar las cuentas desde la tabla "cuentas" (Ingresos/Egresos)
                cmbTipoCuenta.DataSource = crudCatalogoCuentas.ObtenerCuentas();
                cmbTipoCuenta.DisplayMember = "nombre_cuenta";
                cmbTipoCuenta.ValueMember = "id_cuenta";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de cuenta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosCuenta()
        {
            try
            {
                var cuenta = crudCatalogoCuentas.BuscarCatalogoCuentaPorId(codCuentaSeleccionado);

                if (cuenta != null)
                {
                    txtId.Text = cuenta["Cod_cuenta"].ToString();
                    txtNombre.Text = cuenta["nombre_cuenta"].ToString();
                    txtCuenta.Text = cuenta["nombre_cuenta"].ToString(); // O el campo que corresponda

                    // Seleccionar el tipo de cuenta en el ComboBox
                    cmbTipoCuenta.SelectedValue = cuenta["id_cuenta"];

                    txtDetalle.Text = cuenta["detalle"] != DBNull.Value
                        ? Convert.ToDecimal(cuenta["detalle"]).ToString("N2")
                        : "";

                    txtSaldo.Text = cuenta["saldo"] != DBNull.Value
                        ? Convert.ToDecimal(cuenta["saldo"]).ToString("N2")
                        : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuenta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la cuenta es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                MessageBox.Show("El campo Cuenta es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
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

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtCuenta.Clear();
            txtNombre.Clear();
            txtDetalle.Clear();
            txtSaldo.Clear();

            if (cmbTipoCuenta.Items.Count > 0)
                cmbTipoCuenta.SelectedIndex = 0;

            codCuentaSeleccionado = 0;
            modoEdicion = false;
        }

        private void HabilitarControles(bool habilitar)
        {
            txtId.Enabled = false; // El código siempre deshabilitado (es IDENTITY)
            txtCuenta.Enabled = habilitar; // Permitir editar
            txtNombre.Enabled = habilitar;
            txtDetalle.Enabled = habilitar;
            txtSaldo.Enabled = habilitar;
            cmbTipoCuenta.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
        }

        // ===== BOTONES =====

        private void btnNuevaCuenta_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarControles(true);
            modoEdicion = false;

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

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                int idCuenta = Convert.ToInt32(cmbTipoCuenta.SelectedValue);
                string nombre = txtNombre.Text.Trim();

                decimal? detalle = null;
                if (!string.IsNullOrWhiteSpace(txtDetalle.Text))
                {
                    if (decimal.TryParse(txtDetalle.Text, out decimal detalleValue))
                        detalle = detalleValue;
                }

                decimal? saldo = null;
                if (!string.IsNullOrWhiteSpace(txtSaldo.Text))
                {
                    if (decimal.TryParse(txtSaldo.Text, out decimal saldoValue))
                        saldo = saldoValue;
                }

                if (modoEdicion)
                {
                    bool resultado = crudCatalogoCuentas.ModificarCatalogoCuenta(
                        codCuentaSeleccionado, idCuenta, nombre, detalle, saldo);

                    if (resultado)
                    {
                        MessageBox.Show("Cuenta modificada exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos();
                        LimpiarCampos();
                        HabilitarControles(false);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar la cuenta", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (crudCatalogoCuentas.CatalogoCuentaExiste(nombre))
                    {
                        MessageBox.Show("El nombre de la cuenta ya existe", "Advertencia",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int nuevoCodigo = crudCatalogoCuentas.AgregarCatalogoCuenta(idCuenta, nombre, detalle, saldo);

                    if (nuevoCodigo > 0)
                    {
                        MessageBox.Show($"Cuenta agregada exitosamente con código: {nuevoCodigo}", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos();
                        LimpiarCampos();
                        HabilitarControles(false);
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
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para cuando se hace doble clic en el DataGridView para editar
        private void dgvCatalogoCuentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codCuentaSeleccionado = Convert.ToInt32(dgvCatalogoCuentas.Rows[e.RowIndex].Cells["Codigo"].Value);
                HabilitarControles(true);
                modoEdicion = true;
                CargarDatosCuenta();
                txtNombre.Focus();
            }
        }

        // Evento para actualizar txtCuenta cuando cambia el ComboBox (OPCIONAL - puedes comentarlo si no lo necesitas)
        private void cmbTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si quieres que txtCuenta se llene automáticamente con el id del tipo de cuenta:
            // if (cmbTipoCuenta.SelectedValue != null)
            // {
            //     txtCuenta.Text = cmbTipoCuenta.SelectedValue.ToString();
            // }

            // De lo contrario, deja este método vacío o elimínalo
        }

        // Opcional: Botón para eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCatalogoCuentas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una cuenta para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de eliminar esta cuenta? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int codigo = Convert.ToInt32(dgvCatalogoCuentas.CurrentRow.Cells["Codigo"].Value);
                    bool resultado = crudCatalogoCuentas.EliminarCatalogoCuenta(codigo);

                    if (resultado)
                    {
                        MessageBox.Show("Cuenta eliminada exitosamente", "Éxito",
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
        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
