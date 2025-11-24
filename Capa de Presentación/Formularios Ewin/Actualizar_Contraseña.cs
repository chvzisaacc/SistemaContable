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
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class Actualizar_Contraseña : Form
    {
        private string correoUsuario;
        ClsCerrar cerrar = new ClsCerrar();
        public Actualizar_Contraseña(string correo)
        {
            InitializeComponent();
            this.FormClosing += cerrar.CerrarApp;
            correoUsuario = correo;
        }

        public Actualizar_Contraseña()
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FRM_PG1 fRM_PG1 = new();
            fRM_PG1.Show();
            this.Hide();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtNuevaContraseña.Text.Trim();
            string confirmarContraseña = txtConfirmarContraseña.Text.Trim();

            Validacion val = new Validacion(nuevaContraseña, confirmarContraseña);
            ClsValidaciones validar = new ClsValidaciones();

            // Validar longitud de la contraseña
            if (!validar.EsContraseñaValida(nuevaContraseña))
            {
                MessageBox.Show("La contraseña debe tener entre 4 y 25 caracteres.");
                return;
            }

            if (!val.CamposIguales(nuevaContraseña))
            {
                MessageBox.Show("Las contraseñas no coinciden o están vacías.");
                return;
            }
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();

                acciones.CambiarContraseña(correoUsuario, nuevaContraseña);

                MessageBox.Show("Contraseña actualizada correctamente.");

                FRM_PG1 fRM_PG1 = new FRM_PG1();
                fRM_PG1.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FRM_PG3 fRM_PG3 = new FRM_PG3();
            fRM_PG3.Show();
            this.Hide();
        }
    }
}
