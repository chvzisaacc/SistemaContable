using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;


namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Formulario que gestiona la recuperación de contraseña: valida usuario/correo,
    /// genera y envía un código de verificación y redirige al formulario de ingreso del código.
    /// </summary>
    public partial class Olvidaste_tu_contraseña : Form
    {
        /// <summary>
        /// Utilidad para el manejo del cierre de la aplicación (reservada para uso en FormClosing si se requiere).
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();

        /// <summary>
        /// Constructor: inicializa componentes y propiedades visuales del formulario.
        /// </summary>
        public Olvidaste_tu_contraseña()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.FormClosing += cerrar.CerrarApp;
        }

        /// <summary>
        /// Controlador reservado (sin implementación actual). Puede usarse para acciones adicionales del botón si se requiere.
        /// </summary>
        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Cierra el formulario cuando se pulsa el control de regresar (label4).
        /// </summary>
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento Load del formulario. Reservado para inicializaciones adicionales futuras.
        /// </summary>
        private void Olvidaste_tu_contraseña_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Valida usuario y correo, genera un código de recuperación, lo guarda en BD y lo envía por correo.
        /// - Valida formato de correo.
        /// - Verifica existencia del usuario mediante <see cref="ClsAccionesDB"/>.
        /// - Genera código usando la capa de correo y lo persiste antes de enviarlo.
        /// Nota de sincronización: aquí se realizan llamadas a la capa de datos y envío de correo;
        /// si se traslada el envío a segundo plano, asegurar que cualquier actualización de UI se haga en el hilo de interfaz.
        /// </summary>
        private void btn_restablecer_contrasena_Click(object sender, EventArgs e)
        {
            ClsValidaciones validar = new ClsValidaciones();
            string correo = txt_correo_electronico.Text.Trim();
            string usuario = txtUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor complete todos los campos (Usuario y Correo).");
                return;
            }

            if (!validar.EsCorreoValido(correo))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
                return;
            }

            ClsAccionesDB acciones = new ClsAccionesDB();
            int usuario_id = acciones.ObtenerUsuarioIdPorCorreo(usuario, correo);

            if (usuario_id == 0)
            {
                MessageBox.Show("Los datos ingresados no coinciden con ningún registro.");
                return;
            }

            var sistema = new Capa_de_acceso_de_datos.CORREO.Sistema();
            string codigo = sistema.GenerarCodigo();
            acciones.GuardarCodigoRecuperacion(usuario_id, codigo);
            sistema.EnviarCodigoVerificacion(correo, codigo);
            MessageBox.Show("Se ha enviado un código de verificación a su correo.");

            this.Hide();
            using (var frm = new FRM_PG3(usuario_id, correo, usuario))
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
            this.Close();
        }
    }
}
