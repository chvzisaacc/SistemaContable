using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class BancosAgregarCuentaBancaria : Form
    {
        private int _parroquiaId;
        private ClsCRUD_CuentasBancarias crud;
        private ClsValidaciones Validaciones;


        private bool _modoEdicion = false;
        private int _idOrigenEdicion = 0;
        private string _nombreInicial = "";
        private decimal _saldoInicial = 0;
        private int _idTipoInicial = 0;

        /// <summary>
        /// Constructor para crear el formulario en modo "Agregar".
        /// Inicializa componentes visuales y dependencias locales (CRUD, validaciones).
        /// </summary>
        /// <param name="parroquiaId">Identificador de la parroquia asociada a la cuenta.</param>
        public BancosAgregarCuentaBancaria(int parroquiaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._parroquiaId = parroquiaId;
            crud = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();
        }

        /// <summary>
        /// Constructor para crear el formulario en modo "Edición".
        /// Precarga valores iniciales y marca el formulario para comportamiento de edición.
        /// </summary>
        /// <param name="parroquiaId">Identificador de la parroquia asociada a la cuenta.</param>
        /// <param name="idOrigen">Id de la cuenta que será editada.</param>
        /// <param name="nombre">Nombre inicial de la cuenta (para precarga).</param>
        /// <param name="saldo">Saldo inicial de la cuenta (para precarga).</param>
        /// <param name="idTipoCuenta">Tipo de cuenta (Id) para preseleccionar en el combo.</param>
        public BancosAgregarCuentaBancaria(int parroquiaId, int idOrigen, string nombre,
                                           decimal saldo, int idTipoCuenta)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._parroquiaId = parroquiaId;
            crud = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();

            _modoEdicion = true;
            _idOrigenEdicion = idOrigen;
            _nombreInicial = nombre;
            _saldoInicial = saldo;
            _idTipoInicial = idTipoCuenta;

            this.Text = "Edición de Cuenta Bancaria";
        }

        /// <summary>
        /// Manejador del evento Load del formulario.
        /// Centra la ventana, llena el combo de tipos y, si está en modo edición,
        /// precarga los valores de nombre, saldo y tipo en los controles correspondientes.
        /// Nota de sincronización: las asignaciones a controles se realizan en el hilo de UI;
        /// si este método fuera invocado desde otro hilo, usar Invoke/BeginInvoke.
        /// </summary>
        private void FRM_BancosAgregarCuentaBancaria_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            LlenarComboTipos();

            if (_modoEdicion)
            {
                label6.Text = "Edición de cuenta bancaria";

                txtCuenta.Text = _nombreInicial;
                txtCuenta.ForeColor = Color.Black;

                txtMonto.Text = _saldoInicial.ToString("0.00");
                txtMonto.ForeColor = Color.Black;

                cmbCuenta.SelectedValue = _idTipoInicial;
            }
        }

        /// <summary>
        /// Manejador del evento click del botón Guardar (pictureBox2).
        /// Valida campos, parsea monto y determina si debe crear o modificar la cuenta.
        /// Llama a los métodos CRUD apropiados y cierra el formulario con DialogResult.OK si tiene éxito.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            var nombre = txtCuenta.Text.Trim();

            string montoLimpio = txtMonto.Text.Replace("L.", "").Replace(",", "").Trim();

            if (!decimal.TryParse(montoLimpio, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.GetCultureInfo("en-US"), out decimal saldo))
            {
                MessageBox.Show("El monto ingresado no es válido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
  
            if (cmbCuenta.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un tipo de cuenta válido.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idTipoCuenta = Convert.ToInt32(cmbCuenta.SelectedValue);

            if (_modoEdicion)
            {
                try
                {
                    bool ok = crud.ModificarCuentaBanco(_idOrigenEdicion, nombre, saldo,
                                                        idTipoCuenta, _parroquiaId);
                    if (ok)
                    {
                        MessageBox.Show(this, $"Cuenta actualizada: {nombre}",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "No se pudo actualizar la cuenta.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ok = crud.CrearCuentaBanco(nombre, saldo, idTipoCuenta,
                                                _parroquiaId, out int nuevo_Id);
                if (ok)
                {
                    MessageBox.Show(this, $"Cuenta creada: {nombre}\nTipo: {cmbCuenta.Text}",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Error al guardar la cuenta en la base de datos.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Valida los campos del formulario antes de guardar.
        /// Usa la clase ClsValidaciones para reglas de negocio (texto válido, monto positivo y rango).
        /// Devuelve true si todos los campos cumplen las reglas; en caso contrario muestra mensajes y establece el foco.
        /// </summary>
        /// <returns>True si los campos son válidos; false en caso contrario.</returns>
        private bool ValidarCampos()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string cuenta = txtCuenta.Text.Trim();

            string montoParaValidar = txtMonto.Text.Replace("L.", "").Replace(",", "").Trim();

            if (string.IsNullOrWhiteSpace(cuenta) || !val.EsTextoValidoCuentas(cuenta))
            {
                MessageBox.Show("El nombre de la cuenta es requerido y solo puede contener letras y espacios.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
                return false;
            }

            // Usamos la variable limpia 'montoParaValidar'
            if (string.IsNullOrWhiteSpace(montoParaValidar) || !val.EsMontoPositivo(montoParaValidar))
            {
                MessageBox.Show("El monto es requerido, solo puede contener números y debe ser mayor a 0.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            if (!val.EsMontoDentroDelRango(montoParaValidar))
            {
                MessageBox.Show("El monto está fuera del rango permitido.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Carga el combo de tipos de cuenta desde la capa de datos.
        /// Asigna DataSource, DisplayMember y ValueMember.
        /// </summary>
        private void LlenarComboTipos()
        {
            clsEnviarACajaChica db = new clsEnviarACajaChica();
            DataTable dt = db.ObtenerTiposCuenta();
            cmbCuenta.DataSource = dt;
            cmbCuenta.DisplayMember = "TipoOrigen";
            cmbCuenta.ValueMember = "IdOrigenTipo";
        }

        /// <summary>
        /// Paint vacío del panel (se deja como placeholder).
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e) { }

        /// <summary>
        /// Manejador Click del textbox de cuenta.
        /// Limpia el placeholder visual si corresponde.
        /// </summary>
        private void txtCuenta_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text == "Ingrese una cuenta")
            {
                txtCuenta.Text = "";
                txtCuenta.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Manejador Leave del textbox de cuenta.
        /// Restaura el placeholder visual si el texto queda vacío.
        /// </summary>
        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtCuenta.Text = "Ingrese una cuenta";
                txtCuenta.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Manejador Click del textbox de monto.
        /// Limpia el placeholder visual si corresponde.
        /// </summary>
        private void txtMonto_Click_1(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un Monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Manejador Leave del textbox de monto.
        /// Restaura el placeholder visual si el texto queda vacío.
        /// </summary>
        private void txtMonto_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Manejador del evento TextChanged del campo de monto.
        /// Formatea automáticamente el valor ingresado a formato monetario con símbolo "L." y 2 decimales.
        /// 
        /// </summary>
        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            txtMonto.TextChanged -= txtMonto_TextChanged;

            try
            {
               
                string numeros = new string(txtMonto.Text.Where(char.IsDigit).ToArray());

                if (string.IsNullOrEmpty(numeros))
                {
                    txtMonto.Text = "L.0.00";
                }
                else
                {
                    if (ulong.TryParse(numeros, out ulong valorNumerico))
                    {
                        decimal resultado = valorNumerico / 100m;

                       
                        txtMonto.Text = "L." + resultado.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    }
                }
            }
            catch { }
            txtMonto.SelectionStart = txtMonto.Text.Length;
            txtMonto.TextChanged += txtMonto_TextChanged;
        }
    }
}
