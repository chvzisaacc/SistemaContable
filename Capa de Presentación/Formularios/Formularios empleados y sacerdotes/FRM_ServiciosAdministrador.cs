csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\FRM_ServiciosAdministrador.cs
using Capa_de_Presentación.Formularios_Diego;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Ventana de servicios para el administrador que actúa como menú/portal a módulos:
    /// reportería, bitácora, alertas y certificados. Contiene handlers que abren formularios
    /// modales y escenarios de navegación entre ventanas.
    /// </summary>
    public partial class FRM_ServiciosAdministrador : Form
    {
        /// <summary>
        /// Constructor: inicializa componentes y configura estilo de ventana.
        /// Las inicializaciones afectan al UI y deben ejecutarse en el hilo de interfaz (UI thread).
        /// </summary>
        public FRM_ServiciosAdministrador()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Manejador MouseClick para abrir el módulo de reportes del administrador.
        /// Oculta el formulario propietario, muestra el modal centrado y restaura el propietario al cerrar.
        /// Nota de sincronización: todas las llamadas a Hide/Show/ShowDialog se realizan en el hilo UI.
        /// </summary>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form; // este es el FRM_42

            try
            {
                main?.Hide();

                this.Hide();
                using (var frm = new Reportería_Administrador())
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
        /// Manejador MouseClick para abrir la bitácora del administrador.
        /// Comportamiento: ocultar propietario, mostrar modal centrado y restaurar propietario al cerrar.
        /// Ejecutarse en el hilo UI; si se carga información pesada en el modal, hacerlo en background y sincronizar UI.
        /// </summary>
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form;

            try
            {
                main?.Hide();

                this.Hide();
                using (var frm = new Bitacora_Admin())
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
        /// Controlador de Click vacío generado por el diseñador.
        /// Mantener o eliminar según el diseñador; no realiza acciones.
        /// </summary>
        private void textBox2_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Manejador MouseClick que muestra una alerta del sistema como diálogo modal.
        /// Oculta el formulario actual mientras se muestra la alerta y cierra al finalizar.
        /// Nota: ShowDialog se ejecuta en el hilo UI; si la alerta requiere datos externos, cargarlos en background.
        /// </summary>
        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();

            using (var alerta = new Capa_de_Presentación.ALERTA.ALERTA_SISTEMA())
            {
                alerta.ShowDialog(this);
            }

            this.Close();
        }

        /// <summary>
        /// Manejador MouseClick para abrir el formulario de Certificados de Depósito en modal.
        /// Oculta el propietario, muestra el modal y restaura el propietario al cerrar.
        /// Sincronización: llamadas a ShowDialog/Hide/Show en hilo UI.
        /// </summary>
        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            var main = this.Owner as Form;

            try
            {
                main?.Hide();
                this.Hide();

                using (var frm = new Certificados_De_Depósito())
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
        /// Handler vacío para eventos generados por el diseñador (placeholder).
        /// Mantener si el diseñador lo requiere; no ejecuta lógica.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handler vacío para cambios de texto (placeholder).
        /// Mantener si el diseñador lo requiere; no ejecuta lógica.
        /// </summary>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Manejador Click alterno para abrir la bitácora (similar a textBox3_MouseClick).
        /// Mantiene el mismo patrón: ocultar propietario, abrir modal y restaurar.
        /// </summary>
        private void textBox3_Click(object sender, EventArgs e)
        {
            var main = this.Owner as Form;

            try
            {
                main?.Hide();

                this.Hide();
                using (var frm = new Bitacora_Admin())
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
        /// Manejador Load del formulario de servicios del administrador.
        /// Actualmente no contiene lógica adicional; se ejecuta en el hilo UI.
        /// </summary>
        private void FRM_ServiciosAdministrador_Load(object sender, EventArgs e)
        {
        }
    }
}