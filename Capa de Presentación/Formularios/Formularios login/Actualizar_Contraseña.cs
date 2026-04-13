using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Formulario para actualizar la contraseña del usuario.
    /// Contiene validaciones de formato y delega la actualización a <see cref="ClsAccionesDB"/>.
    /// </summary>
    public partial class Actualizar_Contraseña : Form
    {
        /// <summary>
        /// Nombre del usuario cuyo password se actualizará.
        /// </summary>
        private string nombreUsuario;

        /// <summary>
        /// Correo del usuario, usado para identificar/confirmar la cuenta en la actualización.
        /// </summary>
        private string correoUsuario;

        /// <summary>
        /// Identificador interno del usuario (si aplica) para navegación o referencia.
        /// </summary>
        private int usuarioId;

        /// <summary>
        /// Utilidad para el cierre/control de la aplicación (instancia local).
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();

        /// <summary>
        /// Constructor que inicializa el formulario con datos de usuario para la actualización.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <param name="nombreUsuario">Nombre del usuario.</param>
        public Actualizar_Contraseña(int id, string correo, string nombreUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // this.FormClosing += cerrar.CerrarApp;
            this.usuarioId = id;
            this.correoUsuario = correo;
            this.nombreUsuario = nombreUsuario;
        }

        /// <summary>
        /// Constructor por defecto. Inicializa los componentes del formulario.
        /// </summary>
        public Actualizar_Contraseña()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento Load del formulario. Actualmente sin lógica adicional; reservado para inicializaciones futuras.
        /// </summary>
        private void Actualizar_Contraseña_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Maneja el clic en el control de volver atrás (label4): abre el formulario anterior pasando datos de usuario.
        /// </summary>
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var frm = new FRM_PG3(this.usuarioId, this.correoUsuario, this.nombreUsuario))
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
            this.Close();
        }

        /// <summary>
        /// Maneja el clic en el botón principal (button1): vuelve a la pantalla de inicio de sesión (FRM_PG1).
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var frm = new FRM_PG1())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
            }
            this.Close();
        }

        /// <summary>
        /// Confirma y aplica el cambio de contraseña:
        /// - Valida formato mediante <see cref="ClsValidaciones"/>.
        /// - Verifica igualdad de campos mediante <see cref="Validacion"/>.
        /// - Si es válido, delega la actualización a <see cref="ClsAccionesDB.CambiarContraseña"/>.
        /// Nota de sincronización: este método interactúa con controles de UI y realiza llamadas a la capa de datos;
        /// si se traslada la operación a un hilo en background, asegurar que las actualizaciones de UI se realicen en el hilo correcto.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string nueva_contraseña = txt_nueva_contrasena.Text.Trim();
            string confirmar_contraseña = txt_confirmar_contrasena.Text.Trim();

            Validacion val = new Validacion(nueva_contraseña, confirmar_contraseña);
            ClsValidaciones validar = new ClsValidaciones();

            if (!validar.EsContraseñaValida(nueva_contraseña))
            {
                MessageBox.Show(
                    "La contraseña debe tener al menos 8 caracteres, una mayuscula, una minuscula, un numero y un caracter especial.",
                    "Contraseña invalida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (!val.CamposIguales(nueva_contraseña))
            {
                MessageBox.Show(
                    "Las contraseñas no coinciden o estan vacias.",
                    "Error de confirmacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                acciones.CambiarContraseña(nombreUsuario, correoUsuario, nueva_contraseña);

                MessageBox.Show(
                    "Contraseña actualizada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Hide();
                using (var frm = new FRM_PG1())
                {
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog(this);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
