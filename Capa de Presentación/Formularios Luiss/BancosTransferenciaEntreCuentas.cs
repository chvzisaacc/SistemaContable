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

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosTransferenciaEntreCuentas : Form
    {
        /// <summary>
        /// The crud transferencia
        /// </summary>
        private clsTransferenciaEntreCuentas crudTransferencia = new clsTransferenciaEntreCuentas();
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// Initializes a new instance of the <see cref="BancosTransferenciaEntreCuentas"/> class.
        /// </summary>
        public BancosTransferenciaEntreCuentas()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarCuentas();
            Validaciones = new ClsValidaciones();
        }

        /// <summary>
        /// Cargars the cuentas.
        /// </summary>
        private void CargarCuentas()
        {
            try
            {
                DataTable dt_cuentas = crudTransferencia.ObtenerCuentasBanco();

                cmbOrigen.DataSource = dt_cuentas.Copy();
                cmbOrigen.DisplayMember = "NombreCompleto";
                cmbOrigen.ValueMember = "Id_Origen";
                cmbOrigen.SelectedIndex = -1;

                cmbDestino.DataSource = dt_cuentas.Copy();
                cmbDestino.DisplayMember = "NombreCompleto";
                cmbDestino.ValueMember = "Id_Origen";
                cmbDestino.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Realizars the transferencia.
        /// </summary>
        private void RealizarTransferencia()
        {
            try
            {
                int cuenta_origen = Convert.ToInt32(cmbOrigen.SelectedValue);
                int cuenta_destino = Convert.ToInt32(cmbDestino.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text);

                bool exito = crudTransferencia.TransferirEntreCuentas(cuenta_origen, cuenta_destino, monto);

                if (exito)
                {
                    MessageBox.Show("Transferencia realizada exitosamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la transferencia: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtMonto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
        /// Validars the campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();


            string monto = txtMonto.Text.Trim();



            if (cmbDestino.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Destino.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDestino.Focus();
                return false;
            }
            if (cmbOrigen.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Origen.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOrigen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsNumeroDecimal(monto))
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
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
            

            if (cmbOrigen.SelectedValue.ToString() == cmbDestino.SelectedValue.ToString())
            {
                MessageBox.Show("Las cuentas de origen y destino deben ser diferentes",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal monto ;
            if (!decimal.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("Debe ingresar un monto válido mayor a cero",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de transferir ${monto:N2} de la cuenta {cmbOrigen.Text} a la cuenta {cmbDestino.Text}?",
                "Confirmar transferencia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            this.DialogResult = DialogResult.OK;

            if (result == DialogResult.Yes)
            {
                RealizarTransferencia();
            }
        }

        /// <summary>
        /// Handles the Load event of the FRM_BancosTransferenciaEntreCuentas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_BancosTransferenciaEntreCuentas_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }


        /// <summary>
        /// Handles the TextChanged event of the txtMonto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMonto_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the txtMonto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Usuario")
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
                txtMonto.Text = "Usuario";
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
