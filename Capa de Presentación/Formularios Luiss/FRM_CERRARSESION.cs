using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_CERRARSESION : Form
    {
        private readonly clsCRUD_Usuarios _repo = new clsCRUD_Usuarios();

        public FRM_CERRARSESION()
        {
            InitializeComponent();
            this.Shown += (_, __) => CargarCorreo(); // síncrono para ir a juego con  CRUD
        }

        private void CargarCorreo()
        {
            try
            {
                string correo = _repo.ObtenerCorreoPorUsuario(Sesion1.UsuarioId);
                label1.Text = string.IsNullOrWhiteSpace(correo)
                    ? "Sin correo / cuenta inactiva"
                    : correo;
            }
            catch (Exception ex)
            {
                label1.Text = "Error obteniendo correo";
                // opcional: log ex.Message
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FRM_CERRARSESION_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Sesion1.CerrarSesion();

            var login = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (login == null) login = new Form1(); // por si lo cerraron por error

            login.Show();
            login.BringToFront();

            // ahora sí, cierra todos los forms menos el login
            foreach (var f in Application.OpenForms.Cast<Form>().ToList())
                if (f != login) f.Close();
        }
    }
}
