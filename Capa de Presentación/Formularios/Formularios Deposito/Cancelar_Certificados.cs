namespace Capa_de_Presentación.Formularios_Diego
{
    /// <summary>
    /// Clase de formulario utilizada para cancelar certificados desde la interfaz.
    /// Contiene validación de motivo y comunicación con el formulario llamador mediante <see cref="DialogResult"/>.
    /// </summary>
    public partial class Cancelar_Certificados : Form
    {
        /// <summary>
        /// Referencia a la columna del DataGridView que contiene el identificador del certificado.
        /// Se usa para sincronizar la selección entre el formulario de lista y este diálogo.
        /// </summary>
        private DataGridViewTextBoxColumn id_Certificado;

        /// <summary>
        /// Constructor por defecto. Inicializa componentes y configura propiedades visuales del formulario.
        /// </summary>
        public Cancelar_Certificados()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Constructor que recibe la columna de certificado para mantener referencia/sincronización con el grid externo.
        /// </summary>
        /// <param name="id_Certificado">Columna del DataGridView que contiene el identificador del certificado.</param>
        public Cancelar_Certificados(DataGridViewTextBoxColumn id_Certificado)
        {
            this.id_Certificado = id_Certificado;
        }

        /// <summary>
        /// Devuelve el motivo de cancelación ingresado por el usuario.
        /// Retorna cadena vacía si el control no está disponible.
        /// </summary>
        /// <returns>Motivo de cancelación (trimmed) o cadena vacía.</returns>
        public string ObtenerMotivo()
        {
            if (textBox1 != null)
            {
                return textBox1.Text.Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// Evento Load del formulario: centra la ventana en pantalla.
        /// Asegúrese de ejecutar acciones de UI en el hilo de interfaz cuando sincronice datos.
        /// </summary>
        private void FRM_PG108_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Controlador de click para el control 'textBox2'. Actualmente sin implementación.
        /// Se deja reservado para comportamiento futuro si se requiere interacción directa.
        /// </summary>
        private void textBox2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Controlador del clic en el botón/imagen de confirmar (pictureBox2).
        /// Valida que exista un motivo, asigna <see cref="DialogResult.OK"/> y cierra el diálogo para devolver control al formulario llamador.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Ingresar el motivo de la cancelación para continuar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
