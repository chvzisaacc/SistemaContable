using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_de_acceso_de_datos;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_BancosTransferenciaEntreCuentas : Form
    {
        private clsTransferenciaEntreCuentas crudTransferencia = new clsTransferenciaEntreCuentas();

        public FRM_BancosTransferenciaEntreCuentas()
        {
            InitializeComponent();
            CargarCuentas();

        }

        private void CargarCuentas()
        {
            try
            {
                DataTable dtCuentas = crudTransferencia.ObtenerCuentasBanco();

                cmbOrigen.DataSource = dtCuentas.Copy();
                cmbOrigen.DisplayMember = "Descripcion";
                cmbOrigen.ValueMember = "Id_cuentabanco";
                cmbOrigen.SelectedIndex = -1;

                cmbDestino.DataSource = dtCuentas.Copy();
                cmbDestino.DisplayMember = "Descripcion";
                cmbDestino.ValueMember = "Id_cuentabanco";
                cmbDestino.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RealizarTransferencia()
        {
            try
            {
                int cuentaOrigen = Convert.ToInt32(cmbOrigen.SelectedValue);
                int cuentaDestino = Convert.ToInt32(cmbDestino.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text);

                bool exito = crudTransferencia.TransferirEntreCuentas(cuentaOrigen, cuentaDestino, monto);

                if (exito)
                {
                    MessageBox.Show("Transferencia realizada exitosamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la transferencia: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (cmbOrigen.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de origen",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOrigen.Focus();
                return;
            }

            if (cmbDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de destino",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDestino.Focus();
                return;
            }

            if (cmbOrigen.SelectedValue.ToString() == cmbDestino.SelectedValue.ToString())
            {
                MessageBox.Show("Las cuentas de origen y destino deben ser diferentes",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("Debe ingresar un monto válido mayor a cero",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de transferir ${monto:N2} de la cuenta {cmbOrigen.Text} a la cuenta {cmbDestino.Text}?",
                "Confirmar transferencia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RealizarTransferencia();
            }
        }

        private void FRM_BancosTransferenciaEntreCuentas_Load(object sender, EventArgs e)
        {

        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
