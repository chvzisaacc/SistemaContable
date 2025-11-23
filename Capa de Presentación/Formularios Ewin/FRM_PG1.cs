using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
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


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG1 : Form
    {
        ClsCerrar cerrar = new ClsCerrar();
        private int usuarioID;

        public FRM_PG1()
        {
            InitializeComponent();
            this.FormClosing += cerrar.CerrarApp;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsRecuperacion login = new ClsRecuperacion();
            int rol = login.IniciarSesion(txtUsuario.Text, txtContraseña.Text, 0, label1);

            if (rol <= 0) return;

            ClsAccionesDB acciones = new ClsAccionesDB();
            int idUsuario = acciones.ObtenerUsuarioIdPorNombreUsuario(txtUsuario.Text);

            Sesion1.IniciarSesion(idUsuario, rol, 0);

            if (rol == 1)
            {
                FRM_PG5 admin = new FRM_PG5(idUsuario);
                admin.Show();
                this.Hide();
            }
            else if (rol == 2 || rol == 3)
            {
                FRM_42 empleado = new FRM_42(idUsuario);
                empleado.Show();
                this.Hide();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Olvidaste_tu_contraseña objrecu = new Olvidaste_tu_contraseña();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RECONOCIMIENTO_FACIAL.RECONOCER rECONOCER = new();
            rECONOCER.Show();
            this.Hide();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
