using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class Certificados_De_Depósito_User : Form
    {
        private object dataGridView1;
       

        public Certificados_De_Depósito_User()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


        }
        private void Certificados_De_Depósito_User_Load(object sender, EventArgs e)
        {
            CargarCertificados();
            this.CenterToScreen();
        }

        private void CargarCertificados()
        {
            try
            {
                // 1. Obtener ID de la Parroquia del Usuario
                int id_parroquia_usuario = Capa_de_acceso_de_datos.Sesion1.id_parroquia;

                if (id_parroquia_usuario <= 0)
                {
                    MessageBox.Show("No se pudo determinar la Parroquia del usuario. No se cargarán certificados.",
                                    "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Acceder a la Capa de Datos
                ClsAccionesDB accionesDB = new ClsAccionesDB();

                // 3. Llamar a la función con el ID de la Parroquia (asumiendo que está corregida)
                DataTable dtDatosCertificados = accionesDB.MostrarCertificadosUsuario(id_parroquia_usuario);

                // 4. Asignar al DataGridView
                // 
                dataGridView2.DataSource = dtDatosCertificados;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los certificados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
    
    
}
    

    