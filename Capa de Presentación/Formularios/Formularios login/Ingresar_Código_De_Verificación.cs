using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG3 : Form
    {
        private int _usuarioId;
        private string _correoUsuario;
        private string _nombreUsuario;

        public FRM_PG3(int usuarioId, string correoUsuario, string nombreUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _usuarioId = usuarioId;
            _correoUsuario = correoUsuario;
            _nombreUsuario = nombreUsuario;
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }

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

       
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FRM_PG3_Load(object sender, EventArgs e) { }

        private void txt1_TextChanged(object sender, EventArgs e)
        { if (txt_1.Text.Length == 1) txt_2.Focus(); }

        private void txt2_TextChanged(object sender, EventArgs e)
        { if (txt_2.Text.Length == 1) txt_3.Focus(); }

        private void txt3_TextChanged(object sender, EventArgs e)
        { if (txt_3.Text.Length == 1) txt_4.Focus(); }

        private void txt4_TextChanged(object sender, EventArgs e)
        { if (txt_4.Text.Length == 1) txt_5.Focus(); }

        private void txt5_TextChanged(object sender, EventArgs e)
        { if (txt_5.Text.Length == 1) txt_6.Focus(); }

        private void txt6_TextChanged(object sender, EventArgs e)
        { if (txt_6.Text.Length == 1) txt_7.Focus(); }

        private void txt7_TextChanged(object sender, EventArgs e)
        { if (txt_7.Text.Length == 1) txt_8.Focus(); }

        private void txt8_TextChanged(object sender, EventArgs e)
        { if (txt_8.Text.Length == 1) btn_restablecer_contrasena.Focus(); }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt6_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt7_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }

        private void txt7_KeyUp(object sender, KeyEventArgs e) { }

        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }
    }
}