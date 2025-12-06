using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosCuentaAhorro : Form
    {
        /// <summary>
        /// The crud cuentas bancarias
        /// </summary>
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;
        /// <summary>
        /// The modo edicion
        /// </summary>
        private bool modoEdicion = false;
        //private int CuentaBancoID = 1;
        /// <summary>
        /// The cuenta identifier
        /// </summary>
        private readonly int cuentaId;
        /// <summary>
        /// The cuenta actual
        /// </summary>
        private DataRow _cuentaActual; // Para guardar los datos de la cuenta cargada

        /// <summary>
        /// Initializes a new instance of the <see cref="BancosCuentaAhorro"/> class.
        /// </summary>
        /// <param name="id_origen">The identifier origen.</param>
        public BancosCuentaAhorro(int id_origen)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cuentaId = id_origen;
            CRUD_CuentasBancarias = new ClsCRUD_CuentasBancarias();

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG42BancosCuentaCheque control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG42BancosCuentaCheque_Load(object sender, EventArgs e)
        {
            //Si _cuentaId tiene un valor, carga los datos
            if (cuentaId > 0)
            {
                CargarDatosDeLaCuenta();
            }
        }

        /// <summary>
        /// Cargars the datos de la cuenta.
        /// </summary>
        private void CargarDatosDeLaCuenta()
        {
            try
            {
                DataTable dt = CRUD_CuentasBancarias.ObtenerCuentasBancarias();
                DataRow[] rows = dt.Select($"Id_Origen = {cuentaId}");

                if (rows.Length > 0)
                {
                    var cuenta = rows[0];
                    // Asigna los valores a tus TextBoxes y otros controles
                    //txtNombreCuenta.Text = cuenta["Nombre"].ToString();
                    txtMonto.Text = cuenta["saldo"].ToString();
                    // ...etc.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error");
            }
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG6_Load(object sender, EventArgs e)
        {
            // CargarDatos();
            //CargarComboBoxes();
            //LimpiarCampos();
            HabilitarControles(false);
            //CargarDatosCuenta();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("El monto es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }
        /// <summary>
        /// Habilitars the controles.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void HabilitarControles(bool habilitar)
        {
            txtMonto.Enabled = habilitar;

        }

        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the pictureBox2_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            // Ejemplo de cómo usarías tu CRUD para guardar cambios.
            try
            {
                if (!decimal.TryParse(txtMonto.Text, out decimal nuevo_saldo))
                {
                    MessageBox.Show("El saldo ingresado no es un número válido.");
                    return;
                }

                // Llama al método para modificar el saldo
                bool exito = CRUD_CuentasBancarias.ModificarSaldo(cuentaId, nuevo_saldo);

                if (exito)
                {
                    MessageBox.Show("Saldo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el saldo.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el saldo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        /// Handles the Load event of the FRM_PG42BancosCuentaAhorro control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG42BancosCuentaAhorro_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Handles the 1 event of the panel2_Paint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }



}

