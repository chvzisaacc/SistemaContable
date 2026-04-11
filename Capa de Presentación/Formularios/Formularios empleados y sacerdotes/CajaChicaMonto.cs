using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario para agregar monto a la Caja Chica
    /// </summary>
    public partial class CajaChicaMonto : Form
    {
        public delegate void ActualizarSaldoDelegate();
        public event ActualizarSaldoDelegate SaldoActualizado;

       
        private int _parroquiaId;
        private int _usuarioId;
        public bool EsModificacion { get; set; } = false;

        public CajaChicaMonto(int parroquiaId, int usuarioId)
        {
            InitializeComponent();
            _parroquiaId = parroquiaId;
            _usuarioId = usuarioId;

            // Cambiar el título del formulario o label según el modo
            this.Load += (s, e) => {
                lblTitulo.AutoSize = false; 
                lblTitulo.Width = this.ClientSize.Width; 
                lblTitulo.TextAlign = ContentAlignment.MiddleCenter; 
                lblTitulo.Location = new Point(0, lblTitulo.Location.Y); 
                if (EsModificacion)
                {
                    // Suponiendo que el label de arriba se llama lblTitulo
                    // Si no tienes el nombre, búscalo en el diseñador
                    lblTitulo.Text = "MODIFICAR SALDO DE CAJA CHICA";
                    this.Text = "Modificar Saldo";
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Validar campos
            if (!ValidarCampos())
                return;

            // 2. Extraer el valor limpio
            string textoLimpio = txtMonto.Text.Replace("L.", "").Replace(",", "").Trim();
            decimal saldo = decimal.Parse(textoLimpio, CultureInfo.InvariantCulture);

            Clsconexion objcone = new Clsconexion();

            // Creamos el comando indicando que usaremos un Store Procedure
            using (SqlCommand comando = new SqlCommand())
            {
                comando.Connection = objcone.sc;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear(); 

                // 3. Configuración dinámica según el modo
                if (EsModificacion)
                {
                    comando.CommandText = "sp_Modificarsaldo";
                    comando.Parameters.AddWithValue("@monto_nuevo", saldo);
                }
                else
                {
                    comando.CommandText = "sp_Insertarsaldo";
                    comando.Parameters.AddWithValue("@monto", saldo);
                }

                // Parámetros comunes que ambos SPs requieren
                comando.Parameters.AddWithValue("@Parroquia_ID", _parroquiaId);
                comando.Parameters.AddWithValue("@Usuario_id", _usuarioId);

                try
                {
                    objcone.Abrir();
                    comando.ExecuteNonQuery();

                    string mensajeExito = EsModificacion ? "Saldo modificado correctamente" : "Monto insertado correctamente";
                    MessageBox.Show(mensajeExito, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SaldoActualizado?.Invoke();
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Captura errores de SQL (como los RAISERROR que definimos)
                    MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    objcone.Cerrar();
                }
            }
        }

        private bool ValidarCampos()
        {
            // Limpieza total para que decimal.TryParse no falle
            string textoLimpio = txtMonto.Text.Replace("L.", "").Replace(",", "").Trim();

            // 1. Validar vacío o placeholder
            if (string.IsNullOrWhiteSpace(textoLimpio) || txtMonto.Text == "Ingrese monto")
            {
                MessageBox.Show("El campo monto es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            // 2. Validar numéricamente aquí directamente para evitar errores en ClsValidaciones
            if (decimal.TryParse(textoLimpio, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                if (valor <= 0)
                {
                    MessageBox.Show("El monto debe ser un número mayor a 0.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonto.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("El formato del número no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void FRM_CajaChicaMonto_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (this.EsModificacion)
            {
                lblTitulo.Text = "MODIFICAR SALDO DE CAJA CHICA";
            }
        }

        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text) || txtMonto.Text == "Ingrese monto")
            {
                txtMonto.Text = "Ingrese monto";
                txtMonto.ForeColor = Color.Gray;
            }
            else
            {
                
                string soloNumeros = txtMonto.Text.Replace("L.", "").Replace(",", "").Trim();

                if (decimal.TryParse(soloNumeros, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                {
                    var cultura = new CultureInfo("en-US");
                    txtMonto.Text = valor.ToString("'L. ' #,##0.00", cultura);
                    txtMonto.ForeColor = Color.Black;
                }
            }
        }

        // Métodos vacíos si no se utilizan, se pueden dejar o borrar según el diseñador
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}