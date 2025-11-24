using Capa_de_acceso_de_datos;
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
    public partial class FRM_PG46 : Form
    {

        private clsCRUD_CatalogoCuentas crudCatalogoCuentas;
        public FRM_PG46()
        {
            InitializeComponent();
            crudCatalogoCuentas = new clsCRUD_CatalogoCuentas();
        }

        private void dgvBitacora_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FRM_PG46_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvBitacora.DataSource = crudCatalogoCuentas.ObtenerCatalogoCuentas();

                if (dgvBitacora.Columns["CuentaID"] != null)
                    dgvBitacora.Columns["CuentaID"].Visible = false;

                /*
                if (dgvCatalogoCuentas.Columns["Detalle"] != null)
                    dgvCatalogoCuentas.Columns["Detalle"].DefaultCellStyle.Format = "N2";
                */

                if (dgvBitacora.Columns["Saldo"] != null)
                    dgvBitacora.Columns["Saldo"].DefaultCellStyle.Format = "N2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
