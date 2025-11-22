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
    public partial class FRM_BancosRetirarDinero : Form
    {
        private ClsValidaciones Validaciones;
        public FRM_BancosRetirarDinero()
        {
            InitializeComponent();
            Validaciones = new ClsValidaciones();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private void FRM_BancosRetirarDinero_Load(object sender, EventArgs e)
        {
            
        }
        private bool ValidarCampos()
        {
            if (!string.IsNullOrWhiteSpace(txtMonto.Text) && !Validaciones.EsNumeroDecimal(txtMonto.Text))
            {
                MessageBox.Show("El saldo debe ser un número válido (Ej: 100.00).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
