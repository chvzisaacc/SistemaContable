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
    public partial class FRM_BancosAgregarCuentaBancaria : Form
    {
        private ClsCRUD_CuentasBancarias crud;  // campo
        private ClsValidaciones Validaciones; //Validaciones
        public FRM_BancosAgregarCuentaBancaria()
        {
            InitializeComponent();
            crud = new ClsCRUD_CuentasBancarias();
           
            Validaciones = new ClsValidaciones();
        }






        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ValidarCampos();
            var nombre = txtCuenta.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre de cuenta. ");
                return;

            }
            decimal saldo = 0m;

            bool ok = crud.CrearCuentaBanco(nombre, saldo,  out int nuevo_Id);
            /* if (ok)
             {
                 MessageBox.Show($"Cuenta creada. Id: {nuevo_Id}");
                 this.Close();
             }
             else
             {
                 MessageBox.Show("No se pudo crear la cuenta.");
             }
            */
            if (ok)
            {
                MessageBox.Show(this, $"Cuenta creada. Id: {nuevo_Id}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "No se pudo crear la cuenta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FRM_BancosAgregarCuentaBancaria_Load(object sender, EventArgs e)
        {
            
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                MessageBox.Show("El campo detalle es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();

                if (!Validaciones.EsTextoValido(txtCuenta.Text))
                {
                    MessageBox.Show("El detalle solo puede contener letras.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCuenta.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtMonto.Text) && !Validaciones.EsNumeroDecimal(txtMonto.Text))
            {
                MessageBox.Show("El saldo debe ser un número válido (Ej: 100.00).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCuenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCuenta_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text == "Ingrese una cuenta")
            {
                txtCuenta.Text = "";
                txtCuenta.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtCuenta.Text = "Ingrese una cuenta";
                txtCuenta.ForeColor = Color.Gray;
            }
        }

        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrse un Monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }
    }
}
