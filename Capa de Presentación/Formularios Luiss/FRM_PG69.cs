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

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_PG69 : Form
    {
        Clsconexion cn = new Clsconexion();

        public FRM_PG69()
        {
            InitializeComponent();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FRM_PG69_Load(object sender, EventArgs e)
        {
            MostrarCajachica();

        }

        private void MostrarCajachica()
        {
            try
            {
                cn.Abrir();

                SqlCommand cmd = new SqlCommand("sp_ObtenerCajachica", cn.sc);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvCajaChica.DataSource = dt;

                cn.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los datos: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btncerrar_Click(object sender, EventArgs e)
        {

        }
    }
}
