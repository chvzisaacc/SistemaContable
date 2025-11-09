using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_PG42BancosCuentaAhorro : Form
    {
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;
        private bool modoEdicion = false;
        //private int CuentaBancoID = 1;
       
        private DataRow _cuentaActual; // Para guardar los datos de la cuenta cargada
        public FRM_PG42BancosCuentaAhorro(int IdOrigen)
        {
            InitializeComponent();
            
        }

        private void FRM_PG42BancosCuentaCheque_Load(object sender, EventArgs e)
        {
            
        }


        // EN FRM_PG42BancosCuentaAhorro.cs

        private void CargarYMostrarDatos()
        {
            
        }



        private void FRM_PG6_Load(object sender, EventArgs e)
        {
            // CargarDatos();
            //CargarComboBoxes();
            //LimpiarCampos();
            HabilitarControles(false);
            //CargarDatosCuenta();
        }



        /* private void CargarDatosCuenta()
         {
             try
             {
                 // Obtiene todas las cuentas
                 DataTable dt = CRUD_CuentasBancarias.ObtenerCuentasBancarias();

                 // Busca la fila específica que corresponde a nuestro ID
                 // Nota: Lo ideal sería tener un método en tu CRUD que obtenga una sola cuenta por ID.
                 DataRow[] rows = dt.Select($"Id_Origen = {cuentaId}");

                 if (rows.Length > 0)
                 {
                     _cuentaActual = rows[0];

                     // Llena los controles del formulario con los datos
                     this.Text = $"Editando Cuenta: {_cuentaActual["Nombre"]}";
                     txtNombre.Text = _cuentaActual["Nombre"].ToString();
                     txtSaldo.Text = _cuentaActual["saldo"].ToString();
                     txtTasaInteres.Text = _cuentaActual["tasa_interes"].ToString();
                     // ...y así con los demás controles que tengas.
                 }
                 else
                 {
                     MessageBox.Show("No se encontraron los datos para la cuenta seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     this.Close();
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error al cargar los datos de la cuenta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }
        */
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("El monto es requerido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }
        private void HabilitarControles(bool habilitar)
        {
            txtMonto.Enabled = habilitar;

        }
        /*private void ModificarSaldo()
        {
            try
            {
                decimal saldo = decimal.Parse(txtMonto.Text.Trim());
                bool exito = CRUD_CuentasBancarias.ModificarSaldo(saldo);

                if (exito)
                {
                    MessageBox.Show("Saldo modificado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo modificar el saldo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
          this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FRM_PG42BancosCuentaAhorro_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }



}

