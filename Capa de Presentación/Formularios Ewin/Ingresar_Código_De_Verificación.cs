using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG3 : Form
    {
        ClsCerrar cerrar = new ClsCerrar();
        private int _usuarioId;
        private string _correoUsuario;
        public FRM_PG3(int usuarioId, string correoUsuario)
        {
            InitializeComponent();
            this.FormClosing += cerrar.CerrarApp;
            _usuarioId = usuarioId;
            _correoUsuario = correoUsuario;

        }

        public FRM_PG3()
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FRM_PG3_Load(object sender, EventArgs e)
        {

        }

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
            verificador.ProcesarCodigoRecuperacion(_usuarioId, codigo, _correoUsuario, this);
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            if (txt_1.Text.Length == 1)
                txt_2.Focus();
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
            if (txt_2.Text.Length == 1)
                txt_3.Focus();
        }

        private void txt3_TextChanged(object sender, EventArgs e)
        {
            if (txt_3.Text.Length == 1)
                txt_4.Focus();
        }

        private void txt4_TextChanged(object sender, EventArgs e)
        {
            if (txt_4.Text.Length == 1)
                txt_5.Focus();
        }

        private void txt5_TextChanged(object sender, EventArgs e)
        {
            if (txt_5.Text.Length == 1)
                txt_6.Focus();
        }

        private void txt6_TextChanged(object sender, EventArgs e)
        {
            if (txt_6.Text.Length == 1)
                txt_7.Focus();
        }

        private void txt7_TextChanged(object sender, EventArgs e)
        {
            if (txt_7.Text.Length == 1)
                txt_8.Focus();
        }

        private void txt8_TextChanged(object sender, EventArgs e)
        {
            if (txt_8.Text.Length == 1)
                btn_restablecer_contrasena.Focus();
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt7_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Olvidaste_tu_contraseña fRM_PG2 = new();
            fRM_PG2.Show();
            this.Hide();
        }
    }
}
