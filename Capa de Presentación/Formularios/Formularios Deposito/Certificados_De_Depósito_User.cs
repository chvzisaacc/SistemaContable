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
                
                int id_parroquia_usuario = Capa_de_acceso_de_datos.Sesion1.id_parroquia;

                if (id_parroquia_usuario <= 0)
                {
                    MessageBox.Show("No se pudo determinar la Parroquia del usuario. No se cargarán certificados.",
                                    "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                ClsAccionesDB accionesDB = new ClsAccionesDB();

               
                DataTable dtDatosCertificados = accionesDB.MostrarCertificadosUsuario(id_parroquia_usuario);

             
                dataGridView2.DataSource = dtDatosCertificados;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los certificados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        
    }
    
    
}
    

    