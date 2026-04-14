csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\Cerrar_Sesión.cs
using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario que permite visualizar el correo asociado a la sesión y cerrar sesión.
    /// </summary>
    public partial class Cerrar_Sesión : Form
    {
        /// <summary>
        /// Repositorio para operaciones relacionadas con usuarios.
        /// </summary>
        private readonly clsCRUD_Usuarios _repo = new clsCRUD_Usuarios();

        /// <summary>
        /// Constructor.
        /// Inicializa componentes y configura el evento Shown para cargar el correo del usuario.
        /// Nota de sincronización: la carga se realiza de forma síncrona en el hilo UI para asegurar
        /// coherencia con las operaciones CRUD que usan el mismo hilo.
        /// </summary>
        public Cerrar_Sesión()
        {
            InitializeComponent();
            this.Shown += (_, __) => CargarCorreo();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Manejador Load del formulario.
        /// Actualmente no contiene lógica adicional.
        /// </summary>
        private void Cerrar_Sesión_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obtiene el correo del usuario actual desde la capa de datos y lo muestra en la etiqueta.
        /// Si ocurre un error la etiqueta mostrará un mensaje genérico.
        /// Nota de sincronización: actualiza controles UI; debe ejecutarse en el hilo de interfaz (UI thread).
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
            catch (Exception)
            {
                label1.Text = "Error obteniendo correo";
            }
        }

        /// <summary>
        /// Cierra el formulario al hacer click en el control correspondiente.
        /// Acción realizada en el hilo UI.
        /// </summary>
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Manejador del botón de cierre de sesión.
        /// - Finaliza la sesión actual.
        /// - Pide confirmación al usuario.
        /// - Reinicia la aplicación si el usuario confirma.
        /// Nota de sincronización: las llamadas a Application.Restart y al cierre de sesión deben ejecutarse en el hilo UI.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Sesion1.CerrarSesion();

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de cerrar sesión?",
                "Confirmar Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}