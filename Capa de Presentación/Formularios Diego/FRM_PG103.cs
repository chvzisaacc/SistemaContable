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

namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class FRM_PG103 : Form
    {
        public FRM_PG103()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FRM_PG103_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.ReadOnly = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null)
                    {
                        string nombreCertificado = row.Cells["Nombre"].Value.ToString();
                        decimal depositoInicial = Convert.ToDecimal(row.Cells["DepositoInicial"].Value);
                        int plazo = Convert.ToInt32(row.Cells["Plazo"].Value);
                        decimal tasa = Convert.ToDecimal(row.Cells["Tasa"].Value);

                        ClsAccionesDB acciones = new ClsAccionesDB();
                        acciones.GuardarCertificado(nombreCertificado, depositoInicial, plazo, tasa);
                    }
                }

                MessageBox.Show("Datos guardados con éxito.");
            }
            else
            {
                MessageBox.Show("No hay datos para guardar.");
            }
        }
    }
}
