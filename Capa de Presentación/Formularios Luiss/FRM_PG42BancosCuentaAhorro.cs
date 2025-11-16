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
<<<<<<< HEAD
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;
        private bool modoEdicion = false;
        //private int CuentaBancoID = 1;
        private readonly int cuentaId;
        private DataRow _cuentaActual; // Para guardar los datos de la cuenta cargada

        public FRM_PG42BancosCuentaAhorro(int idOrigen)
        {
            InitializeComponent();
            cuentaId = idOrigen;
            CRUD_CuentasBancarias = new ClsCRUD_CuentasBancarias();
            
=======
        private readonly int _cuentaId;
        private readonly ClsCRUD_CuentasBancarias _crud;

        public FRM_PG42BancosCuentaAhorro(int idCuenta)
        {
            InitializeComponent();
            _cuentaId = idCuenta; // Guarda el ID de la cuenta que se va a mostrar.
            _crud = new ClsCRUD_CuentasBancarias();

>>>>>>> 39807de266bd94fe8493277493778386845411f1
        }

        private void FRM_PG42BancosCuentaCheque_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            //Si _cuentaId tiene un valor, carga los datos
            if (cuentaId > 0)
            {
                CargarDatosDeLaCuenta();
            }
=======
            CargarYMostrarDatosDeCuenta();
>>>>>>> 39807de266bd94fe8493277493778386845411f1
        }

        private void CargarYMostrarDatosDeCuenta()
        {
            try
            {
                // Llama al método que ya tienes para obtener TODAS las cuentas.
                DataTable dtCuentas = _crud.ObtenerCuentasBancarias();

                // Usa el método .Select() de DataTable para encontrar la fila exacta que coincide con el ID.
                DataRow[] rows = dtCuentas.Select($"Id_Origen = {_cuentaId}");

                // Verifica si se encontró la fila.
                if (rows.Length > 0)
                {
                    // Si se encontró, toma la primera fila (solo debería haber una).
                    DataRow cuentaActual = rows[0];

                    // Extrae el nombre y el saldo de la fila.
                    string nombreCuenta = cuentaActual["Nombre"].ToString();
                    decimal saldoActual = Convert.ToDecimal(cuentaActual["saldo"]);

                    // ¡Aquí está la parte clave! Actualiza tus controles.
                    // Asegúrate de que tus controles se llamen 'lblTitulo' y 'txtMonto'.
                    lblTitulo.Text = $"Saldo Actual - {nombreCuenta}";
                    txtMonto.Text = saldoActual.ToString("N2"); // "N2" formatea el número con 2 decimales.
                }
                else
                {
                    // Si no se encuentra el ID, muestra un error y cierra el formulario.
                    MessageBox.Show("No se encontraron los datos para la cuenta seleccionada.", "Error de Datos");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al cargar los datos de la cuenta: \n" + ex.Message, "Error");
                this.Close();
            }
        }





        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRM_PG42BancosCuentaAhorro_Load(object sender, EventArgs e)
        {

        }
    }



}

