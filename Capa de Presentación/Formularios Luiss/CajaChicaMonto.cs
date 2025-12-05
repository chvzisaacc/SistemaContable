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

        /// <summary>
        /// Initializes a new instance of the <see cref="CajaChicaMonto"/> class.
        /// </summary>
        public CajaChicaMonto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
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
            if (!decimal.TryParse(txtMonto.Text, out saldo))
            {
                MessageBox.Show("Ingrese un valor numerico");
                return;
            }

            comando.Parameters.AddWithValue("@monto", saldo);

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
                MessageBox.Show("Error al insertar: " + ex.Message);
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
            ValidarCampos();
            this.CenterToScreen();
        }

        /// <summary>
        /// Validars the campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("El campo detalle es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();

                if (!Validaciones.EsNumeroDecimal(txtMonto.Text))
                {
                    MessageBox.Show("El monto solo puede contener numeros.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonto.Focus();
                    return false;
                }
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
    }
}
