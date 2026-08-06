using Capa_de_acceso_de_datos;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Ventana de servicios que permite navegar a módulos como catálogo, reportes y bitácora.
    /// Contiene handlers que abren formularios modales/ventanas y registra navegación en el historial.
    /// </summary>
    public partial class FRM_SERVICIOS : Form
    {
        //private readonly int Id_Usuariologin;

        /// <summary>
        /// Constructor que recibe Id de usuario.
        /// Inicializa componentes y configura el estilo de la ventana.
        /// Nota de sincronización: inicializaciones afectan al UI y se ejecutan en el hilo de interfaz.
        /// </summary>
        public FRM_SERVICIOS(int Id_Usuario)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Constructor por defecto.
        /// Inicializa componentes visuales.
        /// </summary>
        public FRM_SERVICIOS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Abre el formulario del catálogo de cuentas.
        /// Ejecutado en el hilo UI; cierra el formulario actual después de mostrar el nuevo.
        /// </summary>
        private void pibCataloCuentas_Click(object sender, EventArgs e)
        {
            FRM_PG46 frm = new FRM_PG46();

            frm.Show();
            this.Close();
        }

        /// <summary>
        /// Abre el formulario de generación de reportes.
        /// Ejecutado en el hilo UI; cierra el formulario actual.
        /// </summary>
        private void pibGenerarReportes_Click(object sender, EventArgs e)
        {
            FRM_PG49 frm = new FRM_PG49();
            frm.Show();
            this.Close();
        }

        /// <summary>
        /// Abre la bitácora como diálogo modal centrado respecto al padre.
        /// Oculta la ventana propietaria durante la visualización y la restaura al finalizar.
        /// Nota de sincronización: las llamadas a ShowDialog y las operaciones de Hide/Show deben ejecutarse en el hilo UI.
        /// </summary>
        private void pibBitacora_Click(object sender, EventArgs e)
        {
            var main = this.Owner as Form;

            try
            {
                main?.Hide();
                this.Hide();

                using (var frm = new FRM_PG51(Sesion1.usuario_id))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                this.Close();
                main?.Show();
            }
        }

        /// <summary>
        /// Abre el catálogo desde un control tipo textbox (MouseClick).
        /// Registra la navegación y muestra el formulario FRM_PG46 de forma modal.
        /// Mantiene al formulario propietario oculto mientras se muestra el modal y lo restaura luego.
        /// </summary>
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrarNavegacion("Catálogo de Cuentas");
            var main = this.Owner as Form; // esto es el FRM_42

            try
            {
                // 1) oculta el 42 para que no se vea atrás
                main?.Hide();

                // 2) abre el 46 de forma modal
                this.Hide(); // oculta el popup mientras estoy en 46
                using (var frm = new FRM_PG46())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                // 3) cierra el popup y vuelvo a mostrar el 42
                this.Close();
                main?.Show();
            }
        }

        /// <summary>
        /// Abre el módulo de reportes desde un control tipo textbox (MouseClick).
        /// Registra navegación y muestra FRM_PG49 de forma modal; restaura el propietario al cerrar.
        /// </summary>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrarNavegacion("Reportes");
            var main = this.Owner as Form; // este es el FRM_42

            try
            {
                main?.Hide();

                this.Hide();
                using (var frm = new FRM_PG49())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                this.Close();
                main?.Show();
            }
        }

        /// <summary>
        /// Abre la bitácora desde un control tipo textbox (MouseClick).
        /// Comportamiento similar a <see cref="pibBitacora_Click"/>: modal, propietario oculto y restaurado.
        /// </summary>
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrarNavegacion("Bitacora");
            var main = this.Owner as Form; // este es el FRM_42

            try
            {
                main?.Hide();
                this.Hide();

                using (var frm = new FRM_PG51(Sesion1.usuario_id))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                this.Close();
                main?.Show();
            }
        }

        /// <summary>
        /// Registra la acción de navegación del usuario en la bitácora.
        /// Llama a la capa de datos; se ejecuta en el hilo que invoca este método (normalmente UI).
        /// Si se necesita evitar bloqueos, llamar desde un hilo background y manejar errores/logging adecuadamente.
        /// </summary>
        /// <param name="modulo">Nombre del módulo accedido.</param>
        private void RegistrarNavegacion(string modulo)
        {
            try
            {
                clsCRUD_Historial historial = new clsCRUD_Historial();

                historial.RegistrarAccionUsuario(
                    Sesion1.usuario_id,
                    modulo,
                    "Navegación",
                    $"Ingresó al módulo de {modulo}"
                );
            }
            catch { }
        }

        /// <summary>
        /// Load del formulario: limpia el foco activo y establece el enfoque.
        /// Ejecutado en el hilo UI.
        /// </summary>
        private void FRM_SERVICIOS_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            this.Focus();
        }

        /// <summary>
        /// Handler vacío para clicks en label (placeholder generado por el diseñador).
        /// Mantener o eliminar según uso.
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}