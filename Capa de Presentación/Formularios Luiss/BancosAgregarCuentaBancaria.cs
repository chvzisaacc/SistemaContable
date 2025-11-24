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
    public partial class BancosAgregarCuentaBancaria : Form
    {
        private ClsCRUD_CuentasBancarias crud;  // campo
        private ClsValidaciones Validaciones; //Validaciones
        public BancosAgregarCuentaBancaria()
        {
            InitializeComponent();
            crud = new ClsCRUD_CuentasBancarias();

            Validaciones = new ClsValidaciones();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            var nombre = txtCuenta.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre de cuenta.");
                return;
            }


            decimal saldo = 0m;


            if (!decimal.TryParse(txtMonto.Text.Trim(), out saldo))
            {
                MessageBox.Show("El saldo ingresado no es válido.");
                return;
            }
           

            bool ok = crud.CrearCuentaBanco(nombre, saldo, out int nuevo_Id);

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
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string cuenta = txtCuenta.Text.Trim();
            string monto = txtMonto.Text.Trim();




            if (string.IsNullOrWhiteSpace(cuenta) || !val.EsTextoValido(cuenta))
            {
                MessageBox.Show("El nombre de la cuenta es requerido y solo puede contener letras y espacios.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsMontoPositivo(monto))
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMonto_Click_1(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un Monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        private void txtMonto_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }
    }
}
