using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_PG42BancosCuentaAhorro : Form
    {
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;
        private bool modoEdicion = false;
        //private int CuentaBancoID = 1;
        public FRM_PG42BancosCuentaAhorro()
        {
            InitializeComponent();
            CRUD_CuentasBancarias = new ClsCRUD_CuentasBancarias();
        }



        private void FRM_PG6_Load(object sender, EventArgs e)
        {
            // CargarDatos();
            //CargarComboBoxes();
            //LimpiarCampos();
            HabilitarControles(false);
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
        private void HabilitarControles(bool habilitar)
        {
            txtMonto.Enabled = habilitar;

        }
        /*private void ModificarSaldo()
        {
            try
            {
                decimal saldo = decimal.Parse(txtMonto.Text.Trim());
                bool exito = CRUD_CuentasBancarias.ModificarSaldo(saldo);

                if (exito)
                {
                    MessageBox.Show("Saldo modificado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el saldo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text.Trim(), out var saldo))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            int idCuentaBanco = 1 ;/* el ID real de la cuenta a actualizar */
            

            bool exito = CRUD_CuentasBancarias.ModificarSaldo(idCuentaBanco, saldo);
            if (exito)
            {
                MessageBox.Show("Saldo modificado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se actualizó ninguna fila. Verifica el Id de cuenta.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }    

   
           
    }

