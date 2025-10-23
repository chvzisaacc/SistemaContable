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
    public partial class FRM_SERVICIOS : Form
    {
        public FRM_SERVICIOS()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pibCataloCuentas_Click(object sender, EventArgs e)
        {
            FRM_PG46 frm = new FRM_PG46();
            frm.Show();
            this.Close();
        }

        private void pibGenerarReportes_Click(object sender, EventArgs e)
        {
            FRM_PG49 frm = new FRM_PG49();
            frm.Show();
            this.Close();
        }

        private void pibBitacora_Click(object sender, EventArgs e)
        {
            FRM_PG51 frm = new FRM_PG51();
            frm.Show();
            this.Close();
        }
    }
}

