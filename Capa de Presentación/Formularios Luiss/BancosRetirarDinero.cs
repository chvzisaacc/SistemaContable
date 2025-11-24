using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
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
    public partial class BancosRetirarDinero : Form
    {

        private clsEnviarACajaChica crudCajaChica = new clsEnviarACajaChica();
        private ClsValidaciones Validaciones;

        public BancosRetirarDinero()
        {
            InitializeComponent();
            Validaciones = new ClsValidaciones();
            CargarCuentas();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ValidarCampos();

            if (cmbCuentas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de origen",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
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
                $"¿Está seguro de enviar L.{monto:N2} a caja chica desde la cuenta {cmbCuentas.Text}?",
                "Confirmar envío",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RealizarTransferencia();
            }
        }

        private void FRM_BancosRetirarDinero_Load(object sender, EventArgs e)
        {

        }
        private bool ValidarCampos()
        {
            if (!string.IsNullOrWhiteSpace(txtMonto.Text) && !Validaciones.EsNumeroDecimal(txtMonto.Text))
            {
                MessageBox.Show("El monto debe ser un número válido (Ej: 100.00).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
        }


        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }

        private void CargarCuentas()
        {
            try
            {
                DataTable dtCuentas = crudCajaChica.ObtenerCuentasDisponibles();
                cmbCuentas.DataSource = dtCuentas.Copy();
                cmbCuentas.DisplayMember = "NombreCompleto";
                cmbCuentas.ValueMember = "Id_Origen";
                cmbCuentas.SelectedIndex = -1;
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
                int idOrigen = Convert.ToInt32(cmbCuentas.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text);

                bool exito = crudCajaChica.EnviarDineroCajaChica(idOrigen, monto);

                if (exito)
                {
                    MessageBox.Show("Dinero enviado a caja chica exitosamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar dinero a caja chica: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
