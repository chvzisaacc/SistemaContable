using Capa_de_acceso_de_datos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_CajaChicaMonto : Form
    {
        public delegate void ActualizarSaldoDelegate();

        // Lo que el form principal debe actualizar o delegar
        public event ActualizarSaldoDelegate SaldoActualizado;


        public FRM_CajaChicaMonto()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Clsconexion objcone = new Clsconexion();
            SqlCommand comando = new SqlCommand("sp_Insertarsaldo", objcone.sc);
            comando.CommandType = CommandType.StoredProcedure;

            decimal saldo;
            if (!decimal.TryParse(txtMonto.Text, out saldo))
            {
                MessageBox.Show("Ingrese un valor numerico");
                return;
            }

            comando.Parameters.AddWithValue("@monto", saldo);

            try
            {
                objcone.Abrir();
                comando.ExecuteNonQuery();
                MessageBox.Show("Monto insertado correctamente");
                this.Close();
                SaldoActualizado?.Invoke();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message);
            }
            finally
            {
                objcone.Cerrar();

            }

        }
    }
}
