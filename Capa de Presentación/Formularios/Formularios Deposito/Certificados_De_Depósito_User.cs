using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Diego
{
    /// <summary>
    /// Formulario para mostrar los certificados de depósito asociados al usuario actual.
    /// Realiza la obtención de datos por parroquia de la sesión y los enlaza a <c>dataGridView2</c>.
    /// </summary>
    public partial class Certificados_De_Depósito_User : Form
    {

        /// <summary>
        /// Constructor. Inicializa componentes y configura estilo del formulario.
        /// </summary>
        public Certificados_De_Depósito_User()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Evento Load del formulario: carga los certificados y centra la ventana en pantalla.
        /// </summary>
        private void Certificados_De_Depósito_User_Load(object sender, EventArgs e)
        {
            CargarCertificados();
            this.CenterToScreen();
        }

        /// <summary>
        /// Obtiene y muestra los certificados del usuario según la parroquia almacenada en la sesión.
        /// - Valida que exista una parroquia en <c>Sesion1.id_parroquia</c>.
        /// - Consulta la base de datos mediante <see cref="ClsAccionesDB"/>.
        /// - Asigna el resultado a <c>dataGridView2.DataSource</c>.
        /// Nota de sincronización: este método actualiza controles de la UI; si se hace la carga en segundo plano,
        /// asegurar la actualización del control en el hilo de interfaz (Invoke/BeginInvoke o usar async/await correctamente).
        /// </summary>
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
