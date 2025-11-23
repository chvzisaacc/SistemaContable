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

namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class FRM_PG77 : Form
    {
        public FRM_PG77()
        {
            InitializeComponent();
        }

        private void PnlLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG77_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ClsValidaciones v = new ClsValidaciones();

            // 1. Combo no vacío
            if (!v.ComboSeleccionado(cmbCuentas))
            {
                MessageBox.Show("Debe seleccionar una cuenta.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return;
            }

            // 2. Monto vacío
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("Debe ingresar un monto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            // 3. Monto no válido
            if (!v.EsNumeroDecimal(txtMonto.Text))
            {
                MessageBox.Show("El monto debe ser un número válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            // 4. No negativo ni cero
            if (decimal.Parse(txtMonto.Text) <= 0)
            {
                MessageBox.Show("El monto debe ser mayor que 0.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }
        }
    }
}
