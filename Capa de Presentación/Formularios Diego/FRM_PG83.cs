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

namespace Capa_de_Presentación.Formularios_Diego
{
    public partial class FRM_PG83 : Form
    {
        private clsTransferenciaEntreCuentas crudTransferencia = new clsTransferenciaEntreCuentas();

        public FRM_PG83()
        {
            InitializeComponent();
            CargarCuentas();
        }

        private void CargarCuentas()
        {
            try
            {
                DataTable dtCuentas = crudTransferencia.ObtenerCuentasBanco();

                cmbOrigen.DataSource = dtCuentas.Copy();
                cmbOrigen.DisplayMember = "Descripcion";
                cmbOrigen.ValueMember = "Id_cuentabanco";
                cmbOrigen.SelectedIndex = -1;

                cmbDestino.DataSource = dtCuentas.Copy();
                cmbDestino.DisplayMember = "Descripcion";
                cmbDestino.ValueMember = "Id_cuentabanco";
                cmbDestino.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCerrar_Click(object sender, EventArgs e)
        {

        }

        private void RealizarTransferencia()
        {
            try
            {
                int cuentaOrigen = Convert.ToInt32(cmbOrigen.SelectedValue);
                int cuentaDestino = Convert.ToInt32(cmbDestino.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text);

                bool exito = crudTransferencia.TransferirEntreCuentas(cuentaOrigen, cuentaDestino, monto);

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG83_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_TextChanged(object sender, EventArgs e)
        {
            ClsValidaciones v = new ClsValidaciones();

            // Validar Origen
            if (!v.ComboSeleccionado(cmbOrigen))
            {
                MessageBox.Show("Seleccione una cuenta de origen.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar Destino
            if (!v.ComboSeleccionado(cmbDestino))
            {
                MessageBox.Show("Seleccione una cuenta de destino.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string monto = txtMonto.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrWhiteSpace(monto))
            {
                MessageBox.Show("Ingrese un monto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que no tenga letras (solo números)
            if (!v.EsNumeroDecimal(monto) && !v.EsNumeroEntero(monto))
            {
                MessageBox.Show("El monto debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que sea mayor a 0
            if (decimal.Parse(monto) <= 0)
            {
                MessageBox.Show("El monto debe ser mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                            $"¿Está seguro de transferir ${monto:N2} de la cuenta {cmbOrigen.Text} a la cuenta {cmbDestino.Text}?",
                            "Confirmar transferencia",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RealizarTransferencia();
            }
        }


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
