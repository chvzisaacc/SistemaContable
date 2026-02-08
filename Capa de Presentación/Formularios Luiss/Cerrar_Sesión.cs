using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Cerrar_Sesión : Form
    {
        /// <summary>
        /// The repo
        /// </summary>
        private readonly clsCRUD_Usuarios _repo = new clsCRUD_Usuarios();

        /// <summary>
        /// Initializes a new instance of the <see cref="Cerrar_Sesión"/> class.
        /// </summary>
        public Cerrar_Sesión()
        {
            InitializeComponent();
            this.Shown += (_, __) => CargarCorreo(); // síncrono para ir a juego con CRUD
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Cargars the correo.
        /// </summary>
        private void CargarCorreo()
        {
            try
            {
                string correo = _repo.ObtenerCorreoPorUsuario(Sesion1.usuario_id);
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

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the pictureBox2_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// Handles the Load event of the FRM_CERRARSESION control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_CERRARSESION_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. LIMPIAR DATOS DE SESIÓN
            Sesion1.CerrarSesion();

            // 2. CREAR UNA LISTA DE FORMULARIOS A CERRAR Y NO QUEDEN COMO HILOS 
            List<Form> formulariosAbiertos = new List<Form>(Application.OpenForms.Cast<Form>());

            // 3. ABRIR EL NUEVO LOGIN
            FRM_PG1 nuevoLogin = new FRM_PG1();
            nuevoLogin.Show();

            // 4. CERRAR TODO LO DEMÁS
            foreach (Form f in formulariosAbiertos)
            {
                // No cerramos el nuevo login que acabamos de abrir
                if (f != nuevoLogin)
                {
                    f.Hide(); // Lo ocultamos para que visualmente desaparezca ya
                    f.Dispose(); // Liberamos recursos
                    
                }
            }
        }


    }
}