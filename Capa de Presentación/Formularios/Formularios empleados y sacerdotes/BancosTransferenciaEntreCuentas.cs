using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario para transferencias entre cuentas de la parroquia.
    /// Contiene lógica de carga de cuentas, validación de campos y ejecución de la transferencia.
    /// </summary>
    public partial class BancosTransferenciaEntreCuentas : Form
    {
        /// <summary>
        /// Texto de saldo opcional (no usado directamente en la lógica).
        /// </summary>
        public string SaldoTexto { get; set; }

        /// <summary>
        /// Identificador del usuario que realiza la operación (usado para auditoría en la capa de datos).
        /// </summary>
        private int _usuarioId;

        /// <summary>
        /// Identificador de la parroquia cuyo conjunto de cuentas se manipula.
        /// </summary>
        private int _parroquiaId;

        /// <summary>
        /// Instancia que encapsula las operaciones de transferencia entre cuentas.
        /// </summary>
        private clsTransferenciaEntreCuentas crudTransferencia = new clsTransferenciaEntreCuentas();

        /// <summary>
        /// Utilidades de validación para formatos y reglas de negocio (montos, etc.).
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// Si es mayor que 0, fuerza un origen fijo (ej. caja chica) y deshabilita selección de origen.
        /// </summary>
        private int _idOrigenFijo = 0;

        /// <summary>
        /// Constructor del formulario.
        /// Inicializa componentes, guarda identificadores, carga cuentas y prepara validaciones.
        /// Nota de sincronización: las inicializaciones de UI deben ejecutarse en el hilo de UI.
        /// </summary>
        /// <param name="parroquiaId">Id de la parroquia cuyos registros se mostrarán.</param>
        /// <param name="idOrigenFijo">Id de origen fijo opcional (0 = no fijo).</param>
        /// <param name="usuarioId">Id del usuario que realiza la operación (opcional).</param>
        public BancosTransferenciaEntreCuentas(int parroquiaId, int idOrigenFijo = 0, int usuarioId = 0)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._usuarioId = usuarioId;
            this._parroquiaId = parroquiaId;
            this._idOrigenFijo = idOrigenFijo;
            CargarCuentas();
            Validaciones = new ClsValidaciones();
        }

        /// <summary>
        /// Carga las listas de cuentas para origen y destino desde la capa de datos.
        /// Configura DisplayMember/ValueMember y el estado del control origen cuando existe un origen fijo.
        /// Nota: si la llamada se hiciera desde un hilo background, aplicar los resultados al UI usando Invoke/BeginInvoke.
        /// </summary>
        private void CargarCuentas()
        {
            try
            {
                DataTable dt_cuentas = crudTransferencia.ObtenerCuentasBanco(this._parroquiaId);

                cmbDestino.DataSource = dt_cuentas.Copy();
                cmbDestino.DisplayMember = "NombreCompleto";
                cmbDestino.ValueMember = "Id_Origen";
                cmbDestino.SelectedIndex = -1;

                if (_idOrigenFijo > 0)
                {
                    DataTable dt_caja = crudTransferencia.ObtenerCajaChica(this._parroquiaId);
                    cmbOrigen.DataSource = dt_caja;
                    cmbOrigen.DisplayMember = "NombreCompleto";
                    cmbOrigen.ValueMember = "Id_Origen";
                    cmbOrigen.Enabled = false;
                }
                else
                {
                    cmbOrigen.DataSource = dt_cuentas.Copy();
                    cmbOrigen.DisplayMember = "NombreCompleto";
                    cmbOrigen.ValueMember = "Id_Origen";
                    cmbOrigen.SelectedIndex = -1;
                    cmbOrigen.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ejecuta la transferencia entre cuentas llamando a la capa de datos.
        /// Cierra el formulario con DialogResult.OK si la transferencia fue exitosa.
        /// Nota de sincronización: las llamadas a la capa de datos pueden bloquear; si se ejecutan en hilos background,
        /// los mensajes y el cierre del formulario deben invocarse en el hilo UI.
        /// </summary>
        /// <param name="monto">El monto decimal ya validado y limpio.</param>
        private void RealizarTransferencia(decimal monto)
        {
            try
            {
                int cuenta_origen = Convert.ToInt32(cmbOrigen.SelectedValue);
                int cuenta_destino = Convert.ToInt32(cmbDestino.SelectedValue);

                bool exito = crudTransferencia.TransferirEntreCuentas(
                    cuenta_origen, cuenta_destino, monto, _parroquiaId, _usuarioId);

                if (exito)
                {
                    MessageBox.Show("Transferencia realizada exitosamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message
                    .Replace("Error al realizar la transferencia: ", "")
                    .Replace("Error al procesar: ", "");

                if (mensaje.Contains("ya fue procesada previamente"))
                {
                    MessageBox.Show(mensaje, "Transacción Duplicada",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(mensaje, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Valida los campos requeridos antes de realizar cualquier operación.
        /// Usa <see cref="ClsValidaciones"/> para comprobar formato numérico del monto.
        /// </summary>
        /// <returns>True si los campos son válidos; false en caso contrario.</returns>
        private bool ValidarCampos()
        {
            if (cmbDestino.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Destino.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDestino.Focus();
                return false;
            }
            if (cmbOrigen.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Origen.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbOrigen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMonto.Text) || txtMonto.Text == "L.0.00")
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Manejador del evento click del botón confirmar transferencia.
        /// Valida campos, verifica que origen y destino sean diferentes,
        /// solicita confirmación al usuario y llama a <see cref="RealizarTransferencia"/> si confirma.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            if (cmbOrigen.SelectedValue.ToString() == cmbDestino.SelectedValue.ToString())
            {
                MessageBox.Show("Las cuentas de origen y destino deben ser diferentes",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Limpieza profunda de formato visual para obtener el decimal puro
            string soloDigitos = new string(txtMonto.Text.Where(char.IsDigit).ToArray());
            if (ulong.TryParse(soloDigitos, out ulong valorNumerico))
            {
                decimal monto = valorNumerico / 100m;

                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de transferir L.{monto:N2} de la cuenta {cmbOrigen.Text} a la cuenta {cmbDestino.Text}?",
                    "Confirmar transferencia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    RealizarTransferencia(monto);
                }
            }
        }

        /// <summary>
        /// Manejador Load del formulario; centra la ventana.
        /// </summary>
        private void FRM_BancosTransferenciaEntreCuentas_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            txtMonto.Text = "L.0.00";
        }

        /// <summary>
        /// Click en el textbox de monto: posiciona el cursor al final para facilitar edición.
        /// </summary>
        private void txtMonto_Click(object sender, EventArgs e)
        {
            txtMonto.SelectionStart = txtMonto.Text.Length;
        }

        /// <summary>
        /// Leave del textbox de monto: restaura placeholder visual si el campo queda vacío.
        /// </summary>
        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "L.0.00";
            }
        }

        /// <summary>
        /// Paint del panel (placeholder para personalización visual).
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //otorga color  
        }

        /// <summary>
        /// Manejador del evento TextChanged del campo de monto.
        /// Formatea automáticamente el valor ingresado a formato monetario con símbolo "L." y 2 decimales.
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
                        txtMonto.Text = "L." + resultado.ToString("N2",
                            System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    }
                }
            }
            catch { }

            txtMonto.SelectionStart = txtMonto.Text.Length;
            txtMonto.TextChanged += txtMonto_TextChanged;
        }
    }
}