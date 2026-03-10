using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosCuentaAhorro : Form
    {
        private int _parroquiaId;
        /// <summary>
        /// The crud cuentas bancarias
        /// </summary>
        private ClsCRUD_CuentasBancarias CRUD_CuentasBancarias;
        /// <summary>
        /// The modo edicion
        /// </summary>
        private bool modoEdicion = false;
        //private int CuentaBancoID = 1;
        /// <summary>
        /// The cuenta identifier
        /// </summary>
        private readonly int cuentaId;
        /// <summary>
        /// The cuenta actual
        /// </summary>
        private DataRow _cuentaActual; // Para guardar los datos de la cuenta cargada

        /// <summary>
        /// Initializes a new instance of the <see cref="BancosCuentaAhorro"/> class.
        /// </summary>
        /// <param name="id_origen">The identifier origen.</param>
        public BancosCuentaAhorro(int parroquiaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            CRUD_CuentasBancarias = new ClsCRUD_CuentasBancarias();
            this._parroquiaId = parroquiaId;
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG42BancosCuentaCheque control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG42BancosCuentaCheque_Load(object sender, EventArgs e)
        {

            this.CenterToScreen();
        }


        /// <summary>
        /// Cargars the datos de la cuenta.
        /// </summary>
        /// 
        private void CargarDatosDeLaCuenta()
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG6_Load(object sender, EventArgs e)
        {

            HabilitarControles(false);
        }

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
        /// <summary>
        /// Habilitars the controles.
        /// </summary>
        /// <param name="habilitar">if set to <c>true</c> [habilitar].</param>
        private void HabilitarControles(bool habilitar)
        {
            txtMonto.Enabled = habilitar;

        }

        /// <summary>
        /// Handles the Click event of the pictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the pictureBox2_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            // Ejemplo de cómo usarías tu CRUD para guardar cambios.
            try
            {
                if (!decimal.TryParse(txtMonto.Text, out decimal nuevo_saldo))
                {
                    MessageBox.Show("El saldo ingresado no es un número válido.");
                    return;
                }

                // Llama al método para modificar el saldo
                bool exito = CRUD_CuentasBancarias.ModificarSaldo(cuentaId, nuevo_saldo);

                if (exito)
                {
                    MessageBox.Show("Saldo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el saldo.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el saldo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Paint event of the panel2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CargarCuentasDataGridView()
        {
            try
            {
                // 1. Obtener los datos primero
                DataTable dtCuentas = CRUD_CuentasBancarias.ObtenerCuentasBancarias(_parroquiaId);

                // Limpiar el DataSource y las columnas antes de re-asignar
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                if (dtCuentas != null && dtCuentas.Rows.Count > 0)
                {
                    //Asignar los nuevos datos
                    dataGridView1.DataSource = dtCuentas;

                    // establecer el modo general para que las demás se adapten
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    //Cambiar el modo de la columna específica a 'None' 
                    //para permitirle un ancho manual, de lo contrario el grid la obligará a auto-ajustarse.
                    dataGridView1.Columns["Saldo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    // 3. Definir el ancho manual
                    dataGridView1.Columns["Saldo"].Width = 150;

                    //OCULTAR COLUMNAS QUE NO DEBEN MOSTRARSE
                    if (dataGridView1.Columns.Contains("Id_Origen"))
                    {
                        dataGridView1.Columns["Id_Origen"].Visible = false;
                    }

                    // Configuración visual
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;

                    
                }
                else
                {
                    MessageBox.Show("No se encontraron cuentas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Este catch capturará el error de 'newDisplayMember'
                MessageBox.Show("Error al cargar: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG42BancosCuentaAhorro control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG42BancosCuentaAhorro_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarCuentasDataGridView();
        }

        /// <summary>
        /// Handles the 1 event of the panel2_Paint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }



}

