using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Formulario para ingresar el código de verificación de 8 dígitos.
    /// Permite capturar cada dígito en controles separados y procesar el código para recuperación.
    /// </summary>
    public partial class FRM_PG3 : Form
    {
        /// <summary>
        /// Identificador del usuario para el cual se valida el código.
        /// </summary>
        private int _usuarioId;

        /// <summary>
        /// Correo del usuario asociado al proceso de verificación.
        /// </summary>
        private string _correoUsuario;

        /// <summary>
        /// Nombre del usuario usado para mensajes o registros.
        /// </summary>
        private string _nombreUsuario;

        /// <summary>
        /// Constructor que recibe los datos de usuario necesarios para procesar el código.
        /// </summary>
        public FRM_PG3(int usuarioId, string correoUsuario, string nombreUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _usuarioId = usuarioId;
            _correoUsuario = correoUsuario;
            _nombreUsuario = nombreUsuario;
        }

        /// <summary>
        /// Evento Paint del panel2. Reservado para personalización visual si se requiere.
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e) { }

        /// <summary>
        /// Lee los 8 dígitos de los TextBox, valida longitud y delega el procesamiento a <see cref="ClsVerificarCod"/>.
        /// Nota de sincronización: <see cref="ProcesarCodigoRecuperacion"/> puede interactuar con la UI o la capa de datos;
        /// si implementa trabajo en background asegúrese de marshaling al hilo de UI para actualizaciones de controles.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            string codigo = (txt_1.Text + txt_2.Text + txt_3.Text + txt_4.Text +
                            txt_5.Text + txt_6.Text + txt_7.Text + txt_8.Text)
                            .Replace(" ", "");

            if (codigo.Length != 8)
            {
                MessageBox.Show("Por favor ingrese el código completo.");
                return;
            }

            ClsVerificarCod verificador = new ClsVerificarCod();
            verificador.ProcesarCodigoRecuperacion(_usuarioId, codigo, _correoUsuario, _nombreUsuario, this);
        }

        /// <summary>
        /// Cierra el formulario cuando se pulsa el control de cierre (label2).
        /// </summary>
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento Load del formulario. Reservado para inicializaciones futuras.
        /// </summary>
        private void FRM_PG3_Load(object sender, EventArgs e) { }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt1_TextChanged(object sender, EventArgs e)
        { if (txt_1.Text.Length == 1) txt_2.Focus(); }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt2_TextChanged(object sender, EventArgs e)
        { if (txt_2.Text.Length == 1) txt_3.Focus(); }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt3_TextChanged(object sender, EventArgs e)
        { if (txt_3.Text.Length == 1) txt_4.Focus(); }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt4_TextChanged(object sender, EventArgs e)
        { if (txt_4.Text.Length == 1) txt_5.Focus(); }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt5_TextChanged(object sender, EventArgs e)
        { if (txt_5.Text.Length == 1) txt_6.Focus(); }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt6_TextChanged(object sender, EventArgs e)
        { if (txt_6.Text.Length == 1) txt_7.Focus(); }

        /// <summary>
        /// Avanza el foco al siguiente TextBox cuando se ingresa un dígito.
        /// </summary>
        private void txt7_TextChanged(object sender, EventArgs e)
        { if (txt_7.Text.Length == 1) txt_8.Focus(); }

        /// <summary>
        /// Al completar el último dígito, mueve el foco al botón de restablecer contraseña.
        /// </summary>
        private void txt8_TextChanged(object sender, EventArgs e)
        { if (txt_8.Text.Length == 1) btn_restablecer_contrasena.Focus(); }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el primer TextBox.
        /// </summary>
        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el segundo TextBox.
        /// </summary>
        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el tercer TextBox.
        /// </summary>
        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el cuarto TextBox.
        /// </summary>
        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el quinto TextBox.
        /// </summary>
        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el sexto TextBox.
        /// </summary>
        private void txt6_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el séptimo TextBox.
        /// </summary>
        private void txt7_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        /// <summary>
        /// Evento KeyUp reservado (sin implementación actual).
        /// </summary>
        private void txt7_KeyUp(object sender, KeyEventArgs e) { }

        /// <summary>
        /// Restringe la entrada a dígitos y teclas de control para el octavo TextBox.
        /// </summary>
        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }
    }
}
