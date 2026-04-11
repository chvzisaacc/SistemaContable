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

        // ✅ Campos para modo edición
        private bool _modoEdicion = false;
        private int _idOrigenEdicion = 0;
        private string _nombreInicial = "";
        private decimal _saldoInicial = 0;
        private int _idTipoInicial = 0; // ✅ int, no string

        // ✅ Constructor original — modo AGREGAR
        public BancosAgregarCuentaBancaria(int parroquiaId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._parroquiaId = parroquiaId;
            crud = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();
        }

        // ✅ Constructor nuevo — modo EDICIÓN
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

        private void FRM_BancosAgregarCuentaBancaria_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            LlenarComboTipos();

            if (_modoEdicion)
            {
                // ✅ Cambiar el label del título (label6 confirmado)
                label6.Text = "Edición de cuenta bancaria";

                // ✅ Precargar nombre
                txtCuenta.Text = _nombreInicial;
                txtCuenta.ForeColor = Color.Black;

                // ✅ Precargar saldo
                txtMonto.Text = _saldoInicial.ToString("0.00");
                txtMonto.ForeColor = Color.Black;

                // ✅ Preseleccionar tipo de cuenta por ID
                cmbCuenta.SelectedValue = _idTipoInicial;
            }
        }

        // ✅ Botón Guardar y cerrar
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            var nombre = txtCuenta.Text.Trim();

            if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal saldo))
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
                // ✅ MODO EDICIÓN — llama al SP de modificar
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
                // ✅ MODO AGREGAR — lógica original
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

        private bool ValidarCampos()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string cuenta = txtCuenta.Text.Trim();
            string monto = txtMonto.Text.Trim();

            if (string.IsNullOrWhiteSpace(cuenta) || !val.EsTextoValido(cuenta))
            {
                MessageBox.Show("El nombre de la cuenta es requerido y solo puede contener letras y espacios.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsMontoPositivo(monto))
            {
                MessageBox.Show("El monto es requerido, solo puede contener números y debe ser mayor a 0.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            if (!val.EsMontoDentroDelRango(monto))
            {
                MessageBox.Show("El monto está fuera del rango permitido.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }

        private void LlenarComboTipos()
        {
            clsEnviarACajaChica db = new clsEnviarACajaChica();
            DataTable dt = db.ObtenerTiposCuenta();
            cmbCuenta.DataSource = dt;
            cmbCuenta.DisplayMember = "TipoOrigen";
            cmbCuenta.ValueMember = "IdOrigenTipo";
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void txtCuenta_Click(object sender, EventArgs e)
        {
            if (txtCuenta.Text == "Ingrese una cuenta")
            {
                txtCuenta.Text = "";
                txtCuenta.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                txtCuenta.Text = "Ingrese una cuenta";
                txtCuenta.ForeColor = Color.Gray;
            }
        }

        private void txtMonto_Click_1(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un Monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        private void txtMonto_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }
    }
}