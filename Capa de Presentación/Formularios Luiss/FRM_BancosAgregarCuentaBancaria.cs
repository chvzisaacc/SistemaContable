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
    public partial class FRM_BancosAgregarCuentaBancaria : Form
    {
        private ClsCRUD_CuentasBancarias crud;  // campo

        public FRM_BancosAgregarCuentaBancaria()
        {
            InitializeComponent();
            crud = new ClsCRUD_CuentasBancarias();
            txtTasaInteres.Enabled = false;
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtTasaInteres.Enabled = true;
            }
            else
            {
                txtTasaInteres.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var nombre = txtCuenta.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre de cuenta. ");
                return;

            }

            if (!decimal.TryParse(txtTasaInteres.Text.Trim(), out var tasa) || tasa < 0m)
            {
                MessageBox.Show("Ingrese una tasa válida.");
                return;
            }

            decimal saldo = 0m;

            bool ok = crud.CrearCuentaBanco(nombre, saldo, tasa, out int nuevo_Id);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
