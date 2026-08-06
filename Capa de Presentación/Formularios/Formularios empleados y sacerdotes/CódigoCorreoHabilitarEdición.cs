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

namespace Capa_de_Presentación.Formularios.Formularios_empleados_y_sacerdotes
{
    public partial class CódigoCorreoHabilitarEdición : Form
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
        private int _modulo;

        /// <summary>
        /// Constructor que recibe los datos de usuario necesarios para procesar el código.
        /// </summary>
        public CódigoCorreoHabilitarEdición(int usuarioId, string correoUsuario, string nombreUsuario, int modulo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _usuarioId = usuarioId;
            _correoUsuario = correoUsuario;
            _nombreUsuario = nombreUsuario;
            _modulo = modulo;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }


        private void habilitarEdicion_Click(object sender, EventArgs e)
        {

        }

        private void btn_habilitaredicion_Click(object sender, EventArgs e)
        {
            string codigo = (txt_1.Text + txt_2.Text + txt_3.Text + txt_4.Text +
                             txt_5.Text + txt_6.Text + txt_7.Text + txt_8.Text)
                             .Replace(" ", "");

            if (codigo.Length != 8)
            {
                MessageBox.Show("Por favor ingrese el código completo.");
                return;
            }

            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();

                string resultado = acciones.ValidarCodigoEdicionEspecial(_usuarioId, codigo, _modulo);

                switch (resultado)
                {
                    case "CODIGO_VALIDO":
                        MessageBox.Show("Código verificado. Ya puede editar el registro.",
                            "Acceso concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;

                    case "CODIGO_INCORRECTO":
                        MessageBox.Show("Código incorrecto. Intente nuevamente.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case "CODIGO_EXPIRADO":
                        MessageBox.Show("El código ha expirado. Solicite uno nuevo.",
                            "Expirado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        break;

                    case "CODIGO_AGOTADO":
                        MessageBox.Show("Ha agotado los intentos para este código. Solicite uno nuevo.",
                            "Agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        break;

                    case "SIN_CODIGO":
                        MessageBox.Show("No hay ningún código activo. Solicite uno nuevo.",
                            "Sin código", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        break;

                    default:
                        MessageBox.Show("Error inesperado. Intente nuevamente.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar el código: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
        { if (txt_8.Text.Length == 1) btn_habilitaredicion.Focus(); }

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

        private void txt8_KeyPress(object sender, KeyPressEventArgs e)
        { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; }
    }
}
