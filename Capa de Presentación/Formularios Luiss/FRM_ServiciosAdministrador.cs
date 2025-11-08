using Capa_de_Presentación.Formularios_Ewin;
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
    public partial class FRM_ServiciosAdministrador : Form
    {
        public FRM_ServiciosAdministrador()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; // esto es el FRM_42

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new FRM_PG10())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {

                this.Close();
                main?.Show();
            }
        }

        private void textBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; 

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new FRM_PG38())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {

                this.Close();
                main?.Show();
            }
        }
    }
}
