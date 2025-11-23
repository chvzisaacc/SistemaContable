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
    public partial class FRM_PG82 : Form
    {

        private ClsValidaciones _validaciones = new ClsValidaciones();

        public FRM_PG82()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void FRM_PG82_Load(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Validar que los combos tengan algo seleccionado
            /*if (!_validaciones.ComboSeleccionado(cmbCuentas))
            {
                MessageBox.Show("Seleccione una opción en 'Cuentas'.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return;
            }

            if (!_validaciones.ComboSeleccionado(cmbIntereses))
            {
                MessageBox.Show("Seleccione una opción en 'Intereses Bancarios'.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbIntereses.Focus();
                return;
            }

            if (!_validaciones.ComboSeleccionado(cmbAcciones))
            {
                MessageBox.Show("Seleccione una opción en 'Acciones'.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAcciones.Focus();
                return;
            }*/
        }
    }
}
