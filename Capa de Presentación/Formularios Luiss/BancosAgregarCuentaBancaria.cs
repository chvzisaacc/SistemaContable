using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosAgregarCuentaBancaria : Form
    {
        private int _parroquiaId;
        /// <summary>
        /// The crud
        /// </summary>
        private ClsCRUD_CuentasBancarias crud;  // campo
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones; //Validaciones
        /// <summary>
        /// Initializes a new instance of the <see cref="BancosAgregarCuentaBancaria"/> class.
        /// </summary>
        public BancosAgregarCuentaBancaria(int parroquiaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._parroquiaId = parroquiaId;
            crud = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();
        }


        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            var nombre = txtCuenta.Text.Trim();

            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal saldo))
            {
                return;
            }

            // CAPTURA DEL CÓDIGO (Ahorro = 2, Cheque = 3)
            if (cmbCuenta.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un tipo de cuenta válido.");
                return;
            }

            int idTipoCuenta = Convert.ToInt32(cmbCuenta.SelectedValue);

            bool ok = crud.CrearCuentaBanco(nombre, saldo, idTipoCuenta,
                                this._parroquiaId ,out int nuevo_Id);


            if (ok)
            {
                MessageBox.Show(this, $"Cuenta creada: {nombre}\nTipo: {cmbCuenta.Text}",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "Error al guardar la cuenta en la base de datos.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the FRM_BancosAgregarCuentaBancaria control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_BancosAgregarCuentaBancaria_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            LlenarComboTipos();

        }
        /// <summary>
        /// Validars the campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string cuenta = txtCuenta.Text.Trim();
            string monto = txtMonto.Text.Trim();




            if (string.IsNullOrWhiteSpace(cuenta) || !val.EsTextoValido(cuenta))
            {
                MessageBox.Show("El nombre de la cuenta es requerido y solo puede contener letras y espacios.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsMontoPositivo(monto))
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            if (!val.EsMontoDentroDelRango(monto))
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }


            return true;
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
        /// Handles the TextChanged event of the txtCuenta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtCuenta_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the txtCuenta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtCuenta_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text == "Ingrese una cuenta")
            {
                txtCuenta.Text = "";
                txtCuenta.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the Leave event of the txtUsuario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtCuenta.Text = "Ingrese una cuenta";
                txtCuenta.ForeColor = Color.Gray;
            }
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
        /// Handles the 1 event of the txtMonto_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMonto_Click_1(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un Monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the 1 event of the txtMonto_Leave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMonto_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }

        private void cmbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LlenarComboTipos()
        {
            clsEnviarACajaChica db = new clsEnviarACajaChica();
            DataTable dt = db.ObtenerTiposCuenta(); // El método que creamos antes
            cmbCuenta.DataSource = dt;
            cmbCuenta.DisplayMember = "TipoOrigen";
            cmbCuenta.ValueMember = "IdOrigenTipo";
        }

        
    }
}
