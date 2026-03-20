using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosAgregarSaldo : Form
    {
        private readonly int _parroquiaId;
        private readonly int _usuarioId;
        public delegate void ActualizarSaldoHandler();
        public event ActualizarSaldoHandler SaldoActualizado;
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
        public BancosAgregarSaldo(int parroquiaId, int usuarioId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();

            this._parroquiaId = parroquiaId;
            this._usuarioId = usuarioId;
        }

        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;  // Usar el resultado de ValidarCampos

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
                bool exito = crudCuentasBancarias.AgregarSaldo(id_origen, monto, _usuarioId);

                if (exito)
                {
                    MessageBox.Show("Saldo agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Disparar evento para actualizar el formulario principal
                    SaldoActualizado?.Invoke();

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
                MessageBox.Show(ex.Message, "Alarma de Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show("Debe seleccionar una cuenta.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsMontoPositivo(monto))
            {
                MessageBox.Show("El monto es requerido, solo puede contener números y debe ser mayor a 0.",
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
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias(_parroquiaId);
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
                var cuentas = crudCuentasBancarias.ObtenerCuentasBancarias(_parroquiaId);

                cuentas.Columns.Add("NombreConTipo", typeof(string));
                foreach (DataRow row in cuentas.Rows)
                {
                    row["NombreConTipo"] = $"{row["Nombre"]} ({row["Tipo de Cuenta"]})";
                }

                cmbCuentas.DataSource = cuentas;
                cmbCuentas.DisplayMember = "NombreConTipo";   
                cmbCuentas.ValueMember = "Id_Origen"; 
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
            //otorgar color al panel
        }
    }
}
