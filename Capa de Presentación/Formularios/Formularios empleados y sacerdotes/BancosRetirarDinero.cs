csharp DESARROLLO DE SOFTWARE - PROYECTO PARROQUIAS2\Capa de Presentación\Formularios\Formularios empleados y sacerdotes\BancosRetirarDinero.cs
using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using System.Data;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class BancosRetirarDinero : Form
    {
        /// <summary>
        /// Identificador del usuario que realiza la operación (usado para auditoría en la capa de datos).
        /// </summary>
        private int _usuarioId;

        /// <summary>
        /// Identificador de la parroquia cuyas cuentas se manipulan.
        /// </summary>
        private int _parroquiaId;

        /// <summary>
        /// Evento utilizado para notificar a los suscriptores que el saldo fue actualizado.
        /// Nota de sincronización: el evento se invoca en el hilo que ejecuta el llamador (normalmente UI).
        /// Los suscriptores que manipulen controles deben sincronizar (Invoke/BeginInvoke) si escuchan desde otro hilo.
        /// </summary>
        public delegate void ActualizarSaldoDelegate();
        public event ActualizarSaldoDelegate SaldoActualizado;

        /// <summary>
        /// Instancia de acceso a datos para operaciones relacionadas con caja chica y tipos de cuenta.
        /// </summary>
        private clsEnviarACajaChica crudCajaChica = new clsEnviarACajaChica();

        /// <summary>
        /// Utilidades de validación para campos de formulario (números, montos, etc.).
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// Constructor del formulario.
        /// Inicializa componentes visuales, utilidades y carga las cuentas disponibles.
        /// Las inicializaciones de UI deben ejecutarse en el hilo de la interfaz (UI thread).
        /// </summary>
        /// <param name="parroquiaId">Id de la parroquia para filtrar cuentas.</param>
        /// <param name="usuarioId">Id del usuario que realiza la operación.</param>
        public BancosRetirarDinero(int parroquiaId, int usuarioId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
            this._parroquiaId = parroquiaId;
            this._usuarioId = usuarioId;
            CargarCuentas();
        }

        /// <summary>
        /// Manejador del click en el control de confirmar envío.
        /// Valida campos, solicita confirmación y en caso afirmativo delega la operación a <see cref="RealizarTransferencia"/>.
        /// Nota: los diálogos y el cierre del formulario ocurren en el hilo UI.
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ValidarCampos();

            if (cmbCuentas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de origen",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCuentas.Focus();
                return;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de enviar L.{monto:N2} a caja chica desde la cuenta {cmbCuentas.Text}?",
                "Confirmar envío",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RealizarTransferencia();
            }
        }

        /// <summary>
        /// Manejador del evento Load del formulario.
        /// Centra la ventana en pantalla. Si la carga de datos se realiza fuera del hilo UI,
        /// las actualizaciones de controles deben aplicarse con Invoke/BeginInvoke.
        /// </summary>
        private void FRM_BancosRetirarDinero_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Valida los campos relevantes del formulario antes de procesar la transferencia.
        /// Usa <see cref="ClsValidaciones"/> para comprobar formatos numéricos.
        /// </summary>
        /// <returns>True si los campos son válidos; False en caso contrario.</returns>
        private bool ValidarCampos()
        {
            if (!string.IsNullOrWhiteSpace(txtMonto.Text) && !Validaciones.EsNumeroDecimal(txtMonto.Text))
            {
                MessageBox.Show("El monto de la cuenta es requerido, solo puede contener numeros y debe ser mayor a 0.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Click en el textbox de monto: limpia el placeholder visual si corresponde.
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
        /// Leave del textbox de monto: restaura el placeholder visual si el campo quedó vacío.
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
        /// Carga las cuentas disponibles en el combo.
        /// Obtiene los datos desde la capa de acceso y asigna DataSource, DisplayMember y ValueMember.
        /// Si esta llamada se ejecuta desde un hilo background, aplicar el resultado al UI con Invoke.
        /// </summary>
        private void CargarCuentas()
        {
            try
            {
                DataTable dt_cuentas = crudCajaChica.ObtenerCuentasDisponibles(_parroquiaId);
                cmbCuentas.DataSource = dt_cuentas.Copy();
                cmbCuentas.DisplayMember = "NombreCompleto";
                cmbCuentas.ValueMember = "Id_Origen";
                cmbCuentas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ejecuta la transferencia hacia caja chica usando la capa de datos.
        /// Al completarse correctamente invoca el evento <see cref="SaldoActualizado"/> para sincronizar otras vistas.
        /// Nota de sincronización: el evento se dispara en el hilo actual (UI); los suscriptores deben sincronizar si actualizan controles.
        /// </summary>
        private void RealizarTransferencia()
        {
            try
            {
                int id_origen = Convert.ToInt32(cmbCuentas.SelectedValue);
                decimal monto = Convert.ToDecimal(txtMonto.Text);

                bool exito = crudCajaChica.EnviarDineroCajaChica(id_origen, monto, _parroquiaId, _usuarioId);

                if (exito)
                {
                    MessageBox.Show("Dinero enviado a caja chica exitosamente",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SaldoActualizado?.Invoke();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar dinero a caja chica: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Paint del panel (placeholder para personalización visual).
        /// Mantener vacío si no se requiere dibujo personalizado.
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //otorga color  
        }

        /// <summary>
        /// Maneja cambios en el textbox auxiliar (actualmente sin implementación).
        /// </summary>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}