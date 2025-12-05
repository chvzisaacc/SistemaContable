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
    public partial class BancosRetirarDinero : Form
    {

        /// <summary>
        /// The crud caja chica
        /// </summary>
        private clsEnviarACajaChica crudCajaChica = new clsEnviarACajaChica();
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// Initializes a new instance of the <see cref="BancosRetirarDinero"/> class.
        /// </summary>
        public BancosRetirarDinero()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
            CargarCuentas();

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

            if (cmbCuentas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de origen",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("Debe ingresar un monto válido mayor a cero",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de enviar L.{monto:N2} a caja chica desde la cuenta {cmbCuentas.Text}?",
                "Confirmar envío",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RealizarTransferencia();
            }

            
        }

        /// <summary>
        /// Handles the Load event of the FRM_BancosRetirarDinero control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_BancosRetirarDinero_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        /// <summary>
        /// Validars the campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            if (!string.IsNullOrWhiteSpace(txtMonto.Text) && !Validaciones.EsNumeroDecimal(txtMonto.Text))
            {
                MessageBox.Show("El monto debe ser un número válido (Ej: 100.00).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
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
        /// Cargars the cuentas.
        /// </summary>
        private void CargarCuentas()
        {
            try
            {
                DataTable dt_cuentas = crudCajaChica.ObtenerCuentasDisponibles();
                cmbCuentas.DataSource = dt_cuentas.Copy();
                cmbCuentas.DisplayMember = "NombreCompleto";
                cmbCuentas.ValueMember = "Id_Origen";
                cmbCuentas.SelectedIndex = -1;
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
                int id_origen = Convert.ToInt32(cmbCuentas.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text);

                bool exito = crudCajaChica.EnviarDineroCajaChica(id_origen, monto);

                if (exito)
                {
                    MessageBox.Show("Dinero enviado a caja chica exitosamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar dinero a caja chica: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
