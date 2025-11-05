using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CAPAS;
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


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG1 : Form
    {
        ClsCerrar cerrar = new ClsCerrar();
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
            string usuario = txtUsuario.Text;
            string password = txtContraseña.Text;
            ClsRecuperacion objrecu = new ClsRecuperacion();
           objrecu.IniciarSesion(txtUsuario.Text, txtContraseña.Text, this, label1);
           ClsAccionesDB clsAccionesDB = new();
            int rol = 0;

            if (rol > 0)
            {
                int idUsuario = clsAccionesDB.ObtenerUsuarioIdPorNombreUsuario(usuario);

                if (idUsuario > 0)
                {
                    Sesion1.IniciarSesion(idUsuario, rol);

                    MessageBox.Show("Inicio de sesión exitoso. ID de Usuario guardado.");
                }
                else
                {
                    MessageBox.Show("Error: El usuario es válido, pero no se pudo obtener su ID.", "Error Crítico");
                }
            }

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
