using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_de_procesamiento_de_datos;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Partidas_Dobles : Form
    {
        /// <summary>
        /// The cn
        /// </summary>
        Clsconexion cn = new Clsconexion();
        /// <summary>
        /// The identifier transaccion
        /// </summary>
        private int id_transaccion;

        /// <summary>
        /// Initializes a new instance of the <see cref="Partidas_Dobles"/> class.
        /// </summary>
        /// <param name="id_transaccion">The identifier transaccion.</param>
        public Partidas_Dobles(int id_transaccion)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.id_transaccion = id_transaccion;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Partidas_Dobles"/> class.
        /// </summary>
        public Partidas_Dobles()
        {
        }

        /// <summary>
        /// Handles the CellContentClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG69 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG69_Load(object sender, EventArgs e)
        {
            CargarPartidas();
            this.CenterToScreen();

        }

        /// <summary>
        /// Cargars the partidas.
        /// </summary>
        private void CargarPartidas()
        {
            PatidasDobles pa = new();
            DataTable dt = pa.CargarPartidas(id_transaccion);
            dgvPartidas.DataSource = dt;
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
        /// Handles the Click event of the Btncerrar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
