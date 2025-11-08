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
    public partial class FRM_PG51 : Form
    {

        private clsCRUD_Historial crudHistorial;
        public FRM_PG51()
        {
            InitializeComponent();
            crudHistorial = new clsCRUD_Historial();
        }

        private void FRM_PG51_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvBitacora.DataSource = crudHistorial.ObtenerHistorialSacerdote();

                if (dgvBitacora.Columns["numero_actividad"] != null)
                    dgvBitacora.Columns["nuemro_actividad"].Visible = false;

                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
