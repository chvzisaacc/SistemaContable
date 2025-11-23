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

namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG97 : Form
    {

        private ClsValidaciones _validaciones = new ClsValidaciones();
        public FRM_PG97()
        {
            InitializeComponent();
        }

        private void FRM_PG97_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // 1. Combo no vacío
            if (!_validaciones.ComboSeleccionado(cmbCuentas))
            {
                MessageBox.Show("Seleccione una cuenta.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return;
            }

            // 2. Monto no vacío, numérico y > 0
            if (!_validaciones.EsMontoPositivo(txtMonto.Text))
            {
                MessageBox.Show("Ingrese un monto válido mayor que 0.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            
            MessageBox.Show("Envío a caja chica exitoso.", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            
            this.Close();
        }
    }
    
}
