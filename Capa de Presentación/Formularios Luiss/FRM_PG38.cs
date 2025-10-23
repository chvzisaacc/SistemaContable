using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentación
{
    public partial class FRM_PG38 : Form
    {
        public FRM_PG38()
        {
            InitializeComponent();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbParroquia.Items.AddRange(new string[] { "Seleccionar", "SCJ", "El Calvario" });
            cmbParroquia.SelectedIndex = 0;
            cmbParroquia.BackColor = Color.Beige;
            cmbParroquia.Font = new Font("Segoe UI", 10);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvBitacora.Rows.Add("Inicio de sesión", "Ingreso al sistema", "Isaac C.", "06 Jun 2025", "7:00", "Se ha iniciado sesión exitosamente");
            dgvBitacora.Rows.Add("Registro de ingresos", "Ingresos", "Diego M.", "15 Jun 2025", "20:30", "Ingreso registrado");

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void lblConsulte_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void FRM_PG38_Load(object sender, EventArgs e)
        {

        }
    }
}
