using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class CajaChicaMonto : Form
    {
        public delegate void ActualizarSaldoDelegate();

        // Lo que el form principal debe actualizar o delegar
        public event ActualizarSaldoDelegate SaldoActualizado;
        //Validaciones
        private ClsValidaciones Validaciones;

        public CajaChicaMonto()
        {
            InitializeComponent();
            Validaciones = new ClsValidaciones();
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

        private void FRM_CajaChicaMonto_Load(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("El campo detalle es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();

                if (!Validaciones.EsNumeroDecimal(txtMonto.Text))
                {
                    MessageBox.Show("El detalle solo puede contener letras.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonto.Focus();
                    return false;
                }
            }
            return true;
        }
    }
}
