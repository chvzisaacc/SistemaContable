using Capa_de_acceso_de_datos;
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
    public partial class Olvidaste_tu_contraseña : Form
    {
        ClsCerrar cerrar = new ClsCerrar();
        public Olvidaste_tu_contraseña()
        {
            InitializeComponent();
            this.FormClosing += cerrar.CerrarApp;
        }

        private void FRM_PG2_Load(object sender, EventArgs e)
        {

        }

        private void FRM_PG2_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            ClsValidaciones validar = new ClsValidaciones();
            string correo = txtCorreo.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor ingrese su correo electrónico.");
                return;
            }

            // Validar que el formato del correo sea válido
            if (!validar.EsCorreoValido(correo))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
                return;
            }


            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor ingrese su correo electrónico.");
                return;
            }

            ClsAccionesDB acciones = new ClsAccionesDB();
            int usuarioId = acciones.ObtenerUsuarioIdPorCorreo(correo);

            if (usuarioId == 0)
            {
                MessageBox.Show("Este correo no está registrado.");
                return;
            }

            var sistema = new Capa_de_acceso_de_datos.CORREO.Sistema();

            string codigo = sistema.GenerarCodigo();
            acciones.GuardarCodigoRecuperacion(usuarioId, codigo);
            sistema.EnviarCodigoVerificacion(correo, codigo);

            MessageBox.Show("Se ha enviado un código de verificación a su correo.");

            FRM_PG3 objingresar = new FRM_PG3(usuarioId, correo);
            objingresar.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FRM_PG1 fRM_PG1 = new();
            fRM_PG1.Show();
            this.Hide();
        }
    }
}
