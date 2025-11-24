using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.RECONOCIMIENTO_FACIAL;
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
            var main = this.Owner as Form; // este es el FRM_42

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new Reportería_Administrador())
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

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form;

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new Bitacora_Admin())
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pibBitacora_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 2. Creamos el nuevo formulario y lo manejamos con 'using' para asegurar su descarte.
            using (var reconocimiento = new Capa_de_Presentación.RECONOCIMIENTO_FACIAL.RECONOCIMIENTO_FACIAL())
            {
                // 3. Lo mostramos de forma MODAL. El código se detiene aquí hasta que se cierra 'reconocimiento'.
                reconocimiento.ShowDialog(this);
            }

            // 4. Una vez que 'reconocimiento' se cierra, cerramos el formulario actual 'this'.
            // Si este formulario (Servicios) fue abierto por otro (dueño/Owner), 
            // el código del dueñó se encargará de mostrarlo de nuevo o cerrarlo.
            this.Close();


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
