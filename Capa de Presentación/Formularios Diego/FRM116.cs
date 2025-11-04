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
    public partial class FRM116 : Form
    {
        public FRM116()
        {
            InitializeComponent();
            CargarBancos();
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
    }
}
