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
    public partial class FRM_PG69 : Form
    {
        Clsconexion cn = new Clsconexion();
        private int idTransaccion;

        public FRM_PG69(int idTransaccion)
        {
            InitializeComponent();
            this.idTransaccion = idTransaccion;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FRM_PG69_Load(object sender, EventArgs e)
        {
            CargarPartidas();

        }

        private void CargarPartidas()
        {
            PatidasDobles pa = new();
            DataTable dt = pa.CargarPartidas(idTransaccion);
            dgvPartidas.DataSource = dt;
        }

  
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
