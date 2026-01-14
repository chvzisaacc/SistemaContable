using Capa_de_Presentación.CLASES;


namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_PG3 : Form
    {
        /// <summary>
        /// The cerrar
        /// </summary>
        ClsCerrar cerrar = new ClsCerrar();
        /// <summary>
        /// The usuario identifier
        /// </summary>
        private int _usuarioId;
        /// <summary>
        /// The correo usuario
        /// </summary>
        private string _correoUsuario;
        private string _nombreUsuario;
        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_PG3"/> class.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        /// <param name="correoUsuario">The correo usuario.</param>
        public FRM_PG3(int usuarioId, string correoUsuario, string nombreUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormClosing += cerrar.CerrarApp;
            _usuarioId = usuarioId;
            _correoUsuario = correoUsuario;
            _nombreUsuario = nombreUsuario;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_PG3"/> class.
        /// </summary>
        public FRM_PG3()
        {
        }

        /// <summary>
        /// Handles the Paint event of the panel2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG3_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

            //Metodo para verificar 

            ClsVerificarCod verificador = new ClsVerificarCod();
            verificador.ProcesarCodigoRecuperacion(_usuarioId, codigo, _correoUsuario, _nombreUsuario, this);
        }

        /// <summary>
        /// Handles the TextChanged event of the txt1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt1_TextChanged(object sender, EventArgs e)
        {
            if (txt_1.Text.Length == 1)
                txt_2.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt2_TextChanged(object sender, EventArgs e)
        {
            if (txt_2.Text.Length == 1)
                txt_3.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt3_TextChanged(object sender, EventArgs e)
        {
            if (txt_3.Text.Length == 1)
                txt_4.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt4_TextChanged(object sender, EventArgs e)
        {
            if (txt_4.Text.Length == 1)
                txt_5.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt5_TextChanged(object sender, EventArgs e)
        {
            if (txt_5.Text.Length == 1)
                txt_6.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt6_TextChanged(object sender, EventArgs e)
        {
            if (txt_6.Text.Length == 1)
                txt_7.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt7_TextChanged(object sender, EventArgs e)
        {
            if (txt_7.Text.Length == 1)
                txt_8.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txt8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txt8_TextChanged(object sender, EventArgs e)
        {
            if (txt_8.Text.Length == 1)
                btn_restablecer_contrasena.Focus();
        }

        /// <summary>
        /// Handles the KeyPress event of the txt1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txt2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txt3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txt4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txt5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txt6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the txt7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txt7_KeyUp(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// Handles the KeyPress event of the txt7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txt8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the label2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label2_Click(object sender, EventArgs e)
        {

            //Movimiento de frm
            Olvidaste_tu_contraseña fRM_PG2 = new();
            fRM_PG2.Show();
            this.Hide();
        }
    }
}
