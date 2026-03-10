using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class CajaChicaMonto : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public delegate void ActualizarSaldoDelegate();

        // Lo que el form principal debe actualizar o delegar
        /// <summary>
        /// Occurs when [saldo actualizado].
        /// </summary>
        public event ActualizarSaldoDelegate SaldoActualizado;
        //Validaciones
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones;
        private int _parroquiaId;
        private int _usuarioId;


        /// <summary>
        /// Initializes a new instance of the <see cref="CajaChicaMonto"/> class.
        /// </summary>
        public CajaChicaMonto(int parroquiaId, int usuarioId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
            _parroquiaId = parroquiaId;
            _usuarioId = usuarioId;   // ← NUEVO
    
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;
            Clsconexion objcone = new Clsconexion();
            SqlCommand comando = new SqlCommand("sp_Insertarsaldo", objcone.sc);
            comando.CommandType = CommandType.StoredProcedure;

            decimal saldo;
            // Si ValidarCampos funciona bien, esto siempre será true, 
            // pero es bueno dejar el return por seguridad.
            if (!decimal.TryParse(txtMonto.Text, out saldo))
            {
                return;
            }


            comando.Parameters.AddWithValue("@monto", saldo);
            comando.Parameters.AddWithValue("@Parroquia_ID", _parroquiaId);
            comando.Parameters.AddWithValue("@Usuario_id", _usuarioId);  // ← NUEVO


            try
            {
                objcone.Abrir();
                comando.ExecuteNonQuery();
                MessageBox.Show("Monto insertado correctamente");
                this.Close();
                SaldoActualizado?.Invoke();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objcone.Cerrar();

            }

        }

        /// <summary>
        /// Handles the Load event of the FRM_CajaChicaMonto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_CajaChicaMonto_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Validars the campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            string textoMonto = txtMonto.Text.Trim();

            // 1. Validar que no esté vacío ni tenga el texto de ayuda (placeholder)
            if (string.IsNullOrWhiteSpace(textoMonto) || textoMonto == "Ingrese un monto")
            {
                MessageBox.Show("El campo monto es requerido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            // 2. Validar que sea un número válido y MAYOR a 0
            // (Asumiendo que tu método EsMontoPositivo ya valida que sea > 0)
            if (!Validaciones.EsMontoPositivo(textoMonto))
            {
                MessageBox.Show("El monto debe ser un número mayor a 0.", "Formato Incorrecto",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            // 3. Validar que no exceda el límite (Opcional, pero recomendado por tu consulta anterior)
            if (!Validaciones.EsMontoDentroDelRango(textoMonto))
            {
                MessageBox.Show("El saldo no puede ser mayor a 100,000,000.", "Advertencia",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }


        /// <summary>
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "Ingrese monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }
    }
}
