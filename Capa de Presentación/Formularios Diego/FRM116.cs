using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Luiss;
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
    public partial class FRM116 : Form
    {
        public FRM116()
        {
            InitializeComponent();
            CargarBancos();
        }

        private void FRM116_Load(object sender, EventArgs e)
        {

        }

        public void CargarBancos()
        {
            try
            {
                ClsCD objCd = new();

                objCd.CargarCuentasBancarias(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            FRM_42 fRM_42 = new();
            fRM_42.ShowDialog();
            this.Hide();
        }
    }
}
