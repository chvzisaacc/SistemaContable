using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG8 : Form
    {
        private int usuarioID;
        private int idParroquia;

        public FRM_PG8()
        {
            InitializeComponent();
        }

        public FRM_PG8(int usuarioID, int idParroquia)
        {
            this.usuarioID = usuarioID;
            this.idParroquia = idParroquia;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FRM_PG8_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
