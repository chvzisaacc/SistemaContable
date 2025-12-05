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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_PG51 : Form
    {
        /// <summary>
        /// The identifier usuario login
        /// </summary>
        private int id_usuario_login;
        /// <summary>
        /// The crud historial
        /// </summary>
        private clsCRUD_Historial crudHistorial;

        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_PG51"/> class.
        /// </summary>
        /// <param name="id_usuario">The identifier usuario.</param>
        public FRM_PG51(int id_usuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            id_usuario_login =id_usuario;
            crudHistorial = new clsCRUD_Historial();
        }


        /// <summary>
        /// Handles the Load event of the FRM_PG51 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG51_Load(object sender, EventArgs e)
        {
            CargarMiHistorial();
            this.CenterToScreen();
        }

        /// <summary>
        /// Cargars the mi historial.
        /// </summary>
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

        /// <summary>
        /// Cargars the datos.
        /// </summary>
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

        /// <summary>
        /// Handles the Click event of the btnVolver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
