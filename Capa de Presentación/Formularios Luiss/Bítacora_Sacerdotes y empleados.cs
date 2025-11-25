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
        private int id_usuario_login;
        private clsCRUD_Historial crudHistorial;

        public FRM_PG51(int id_usuario)
        {
            InitializeComponent();
            id_usuario_login =id_usuario;
            crudHistorial = new clsCRUD_Historial();
        }
       

        private void FRM_PG51_Load(object sender, EventArgs e)
        {
           // CargarDatos();
            CargarMiHistorial();
        }

        private void CargarMiHistorial()
        {
            try
            {
                // Llama al nuevo método para obtener solo el historial de este usuario
                dgvBitacora.DataSource = crudHistorial.ObtenerHistorialUsuario(id_usuario_login);

                // Formatear columnas 
                if (dgvBitacora.Columns["Monto"] != null)
                {
                    dgvBitacora.Columns["Monto"].Visible = false;
                }
                if (dgvBitacora.Columns["FechaHora"] != null)
                {
                    dgvBitacora.Columns["FechaHora"].DefaultCellStyle.Format = "g"; // Formato de fecha y hora corta
                }
                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tu historial: " + ex.Message, "Error");
            }
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
