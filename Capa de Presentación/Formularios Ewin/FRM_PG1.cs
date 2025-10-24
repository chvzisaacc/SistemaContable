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
using Capa_de_Presentación.CAPAS;


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG1 : Form
    {
        public FRM_PG1()
        {
            InitializeComponent();
            //this.FormClosing += FRM_PG1_FormClosing;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           ClsRecuperacion objrecu = new ClsRecuperacion();
           objrecu.IniciarSesion(txtUsuario.Text, txtContraseña.Text, this, label1);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FRM_PG2 objrecu = new FRM_PG2();
            objrecu.Show();
            this.Hide();
        }

        private void txtUsuario_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.ForeColor = Color.Gray;
            }
        }

        private void txtContraseña_Click(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "Contraseña")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.Black;
            }
        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                txtContraseña.Text = "Contraseña";
                txtContraseña.ForeColor = Color.Gray;
            }
        }
    }
}
