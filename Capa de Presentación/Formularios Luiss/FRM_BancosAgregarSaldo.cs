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

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_BancosAgregarSaldo : Form
    {
        private ClsCRUD_CuentasBancarias crudCuentasBancarias;
        public FRM_BancosAgregarSaldo()
        {
            InitializeComponent();
            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // 1) Validar selección de cuenta
            if (cmbCuentas.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una cuenta.");
                cmbCuentas.DroppedDown = true;
                return;
            }

            // 2) Validar monto
            if (!decimal.TryParse(txtMonto.Text.Trim(), out var monto) || monto <= 0m)
            {
                MessageBox.Show("Ingrese un monto válido mayor a 0.");
                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            int idCuentaBanco = Convert.ToInt32(cmbCuentas.SelectedValue);

            try
            {
                var crud = new ClsCRUD_CuentasBancarias();
                bool exito = crud.AgregarSaldo(idCuentaBanco, monto);

                if (exito)
                {
                    MessageBox.Show("Saldo agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se actualizó ninguna fila. Verifique la cuenta.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void FRM_BancosAgregarSaldo_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarComboBoxes();
            // LimpiarCampos();
            //HabilitarControles(false);
        }

        private void CargarDatos()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();

                //aqui es para ocultar algunos campos (los ids y las contraseñas)
                /*
                if (dgvUsuarios.Columns["Contraseña"] != null)
                    dgvUsuarios.Columns["Contraseña"].Visible = false;

                if (dgvUsuarios.Columns["RolID"] != null)
                    dgvUsuarios.Columns["RolID"].Visible = false;
                if (dgvUsuarios.Columns["ParroquiaID"] != null)
                    dgvUsuarios.Columns["ParroquiaID"].Visible = false;
                if (dgvUsuarios.Columns["EstadoID"] != null)
                    dgvUsuarios.Columns["EstadoID"].Visible = false;
                */
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
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_Origen";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
