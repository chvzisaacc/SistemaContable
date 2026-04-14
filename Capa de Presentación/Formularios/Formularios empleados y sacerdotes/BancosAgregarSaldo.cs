csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\BancosAgregarSaldo.cs
using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario para agregar saldo a una cuenta bancaria de la parroquia.
    /// Contiene lógica de validación, carga de datos y llamadas a la capa de datos.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BancosAgregarSaldo : Form
    {
        /// <summary>
        /// Identificador de la parroquia asociada al formulario.
        /// Lectura única; usado en consultas y operaciones de saldo.
        /// </summary>
        private readonly int _parroquiaId;

        /// <summary>
        /// Identificador del usuario que realiza la operación.
        /// Lectura única; pasado a la capa de datos para auditoría.
        /// </summary>
        private readonly int _usuarioId;

        /// <summary>
        /// Evento que notifica a suscriptores que el saldo fue actualizado.
        /// Se recomienda invocarlo en el hilo de UI o sincronizar desde el invocador.
        /// </summary>
        public delegate void ActualizarSaldoHandler();
        public event ActualizarSaldoHandler SaldoActualizado;

        /// <summary>
        /// Instancia de la clase que encapsula operaciones CRUD sobre cuentas bancarias.
        /// </summary>
        private ClsCRUD_CuentasBancarias crudCuentasBancarias;

        /// <summary>
        /// Instancia de utilidades de validación (reglas de negocio para campos).
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// Constructor principal.
        /// Inicializa componentes visuales, dependencias y guarda identificadores.
        /// </summary>
        /// <param name="parroquiaId">Id de la parroquia cuyo saldo será modificado.</param>
        /// <param name="usuarioId">Id del usuario que realiza la operación (para auditoría).</param>
        public BancosAgregarSaldo(int parroquiaId, int usuarioId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();
            Validaciones = new ClsValidaciones();

            this._parroquiaId = parroquiaId;
            this._usuarioId = usuarioId;
        }

        /// <summary>
        /// Manejador del click del botón guardar.
        /// Valida los campos, prepara parámetros y llama a la operación de agregar saldo.
        /// Si la operación es exitosa dispara <see cref="SaldoActualizado"/> para sincronizar la vista principal.
        /// Nota de sincronización: el evento se dispara en el hilo actual (UI); si hay suscriptores que usan hilos
        /// distintos deben sincronizar su acceso a controles o usar BeginInvoke/Invoke según corresponda.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;  // Usar el resultado de ValidarCampos

            if (cmbCuentas.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una cuenta.");
                cmbCuentas.DroppedDown = true;
                return;
            }

            if (!decimal.TryParse(txtMonto.Text.Trim(), out var monto) || monto <= 0m)
            {
                MessageBox.Show("Ingrese un monto válido mayor a 0.");
                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            int id_origen = Convert.ToInt32(cmbCuentas.SelectedValue);

            try
            {
                bool exito = crudCuentasBancarias.AgregarSaldo(id_origen, monto, _usuarioId);

                if (exito)
                {
                    MessageBox.Show("Saldo agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Disparar evento para que formularios suscritos actualicen su vista.
                    // El invocador está en el hilo de UI; los suscriptores deben sincronizar si manipulan controles.
                    SaldoActualizado?.Invoke();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se actualizó ninguna fila. Verifique la cuenta.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alarma de Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        /// <summary>
        /// Manejador del evento Load del formulario.
        /// Centra la ventana y carga los datos necesarios y los combo boxes.
        /// Si se llamara desde un hilo distinto al UI se debe usar Invoke/BeginInvoke.
        /// </summary>
        private void FRM_BancosAgregarSaldo_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarDatos();
            CargarComboBoxes();

        }

        /// <summary>
        /// Valida los campos del formulario antes de ejecutar la operación de agregar saldo.
        /// Emplea reglas de la clase <see cref="ClsValidaciones"/>.
        /// </summary>
        /// <returns>True si los campos son válidos; false en caso contrario.</returns>
        private bool ValidarCampos()
        {
            ClsValidaciones val = Validaciones ?? new ClsValidaciones();

            string monto = txtMonto.Text.Trim();

            if (cmbCuentas.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una cuenta.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(monto) || !val.EsMontoPositivo(monto))
            {
                MessageBox.Show("El monto es requerido, solo puede contener números y debe ser mayor a 0.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Carga datos básicos para el formulario (fuente inicial del combo).
        /// Realiza la llamada a la capa de datos; si se ejecuta desde un hilo background
        /// los resultados deben aplicarse al UI usando Invoke.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias(_parroquiaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Llena los ComboBoxes con la información de cuentas formateada.
        /// Añade una columna calculada "NombreConTipo" para mostrar Nombre (Tipo de Cuenta).
        /// </summary>
        private void CargarComboBoxes()
        {
            try
            {
                var cuentas = crudCuentasBancarias.ObtenerCuentasBancarias(_parroquiaId);

                cuentas.Columns.Add("NombreConTipo", typeof(string));
                foreach (DataRow row in cuentas.Rows)
                {
                    row["NombreConTipo"] = $"{row["Nombre"]} ({row["Tipo de Cuenta"]})";
                }

                cmbCuentas.DataSource = cuentas;
                cmbCuentas.DisplayMember = "NombreConTipo";
                cmbCuentas.ValueMember = "Id_Origen";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Click sobre el textbox de monto: limpia el placeholder si corresponde.
        /// </summary>
        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese un monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Leave del textbox de monto: restaura placeholder si queda vacío.
        /// </summary>
        private void txtMonto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                txtMonto.Text = "Ingrese un monto";
                txtMonto.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Paint del panel (placeholder para personalización visual).
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //otorgar color al panel
        }

        /// <summary>
        /// Evento TextChanged del textbox de monto (actualmente sin implementación).
        /// </summary>
        private void txtMonto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}