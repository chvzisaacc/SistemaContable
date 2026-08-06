using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Ewin;
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
    public partial class EdiciónEspecialAcceso : Form
    {
        private int _modulo;
        public EdiciónEspecialAcceso(int modulo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _modulo = modulo;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EdiciónEspecialAcceso_Load(object sender, EventArgs e)
        {

        }

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

            try
            {
                var sistema = new Capa_de_acceso_de_datos.CORREO.Sistema();
                string codigo = sistema.GenerarCodigo();

                //Usa el nuevo SP con límite de 3 diarios por módulo
                acciones.GuardarCodigoEdicionEspecial(usuario_id, codigo, _modulo);

                sistema.EnviarCodigoVerificacion2(correo, codigo);
                MessageBox.Show("Se ha enviado un código de verificación a su correo.");

                this.Hide();
                using (var frm = new CódigoCorreoHabilitarEdición(usuario_id, correo, usuario, _modulo))
                {
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    //Si el código fue válido, retorna OK al formulario que lo llamó
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                //Aquí llega el mensaje de límite diario
                string mensaje = ex.Message.Replace("Error al procesar: ", "");
                MessageBox.Show(mensaje,
                    mensaje.Contains("límite") ? "Límite alcanzado" : "Error",
                    MessageBoxButtons.OK,
                    mensaje.Contains("límite") ? MessageBoxIcon.Warning : MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }
    }
}
