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
        private readonly int Id_Usuariologin;
        public FRM_SERVICIOS(int Id_Usuario)
        {
            InitializeComponent();
            Id_Usuariologin = Id_Usuario;

        }

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
            var main = this.Owner as Form;

            try
            {
                main?.Hide();
                this.Hide();

                // --- LÍNEA CORREGIDA ---
                // Creamos el formulario de la bitácora pasándole el ID que guardamos.
                // Asumo que FRM_PG51 es tu bitácora personal.
                using (var frm = new FRM_PG51(Id_Usuariologin))
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; // esto es el FRM_42

            try
            {
                // 1) oculto el 42 para que no se vea atrás
                main?.Hide();

                // 2) abro el 46 de forma modal
                this.Hide(); // oculto el popup mientras estoy en 46
                using (var frm = new FRM_PG46())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this); // dueño = este popup (que está oculto)
                }
            }
            finally
            {
                // 3) cierro el popup y vuelvo a mostrar el 42
                this.Close();
                main?.Show();
            }

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; // esto es el FRM_42

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new FRM_PG49())
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

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; // esto es el FRM_42

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new FRM_PG51(Id_Usuariologin))
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

        private void FRM_SERVICIOS_Load(object sender, EventArgs e)
        {

        }
    }
}

