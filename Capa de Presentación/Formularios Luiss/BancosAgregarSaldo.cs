using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosAgregarSaldo : Form
    {
        /// <summary>
        /// The crud cuentas bancarias
        /// </summary>
        private ClsCRUD_CuentasBancarias crudCuentasBancarias;
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones;
        /// <summary>
        /// Initializes a new instance of the <see cref="BancosAgregarSaldo"/> class.
        /// </summary>
        public BancosAgregarSaldo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();
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
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ValidarCampos();
            
            if (cmbCuentas.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una cuenta.");
                cmbCuentas.DroppedDown = true;
                return;
            }
            if (!decimal.TryParse(txtMonto.Text.Trim(), out var monto) || monto <= 0m)
            {
                MessageBox.Show("Ingrese un monto válido mayor a 0.");
                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            int id_origen = Convert.ToInt32(cmbCuentas.SelectedValue);

            try
            {
                var crud = new ClsCRUD_CuentasBancarias();
                bool exito = crud.AgregarSaldo(id_origen, monto);

                if (exito)
                {
                    MessageBox.Show("Saldo agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se actualizó ninguna fila. Verifique la cuenta.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Handles the Load event of the FRM_BancosAgregarSaldo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_BancosAgregarSaldo_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarDatos();
            CargarComboBoxes();

        }

        /// <summary>
        /// Validars the campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();


            string monto = txtMonto.Text.Trim();



            if (cmbCuentas.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un rol.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsMontoPositivo(monto))
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// Cargars the datos.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();

                //aqui es para ocultar algunos campos (los ids y las contraseñas)
                /*
                if (dgvUsuarios.Columns["Contraseña"] != null)
                    dgvUsuarios.Columns["Contraseña"].Visible = false;

                if (dgvUsuarios.Columns["RolID"] != null)
                    dgvUsuarios.Columns["RolID"].Visible = false;
                if (dgvUsuarios.Columns["ParroquiaID"] != null)
                    dgvUsuarios.Columns["ParroquiaID"].Visible = false;
                if (dgvUsuarios.Columns["EstadoID"] != null)
                    dgvUsuarios.Columns["EstadoID"].Visible = false;
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Cargars the combo boxes.
        /// </summary>
        private void CargarComboBoxes()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "id_origen";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Handles the Click event of the txtMonto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the Leave event of the txtMonto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
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
    }
}
