
using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario para insertar o modificar montos en la Caja Chica.
    /// Contiene validaciones, llamadas a procedimientos almacenados y eventos para sincronizar vistas externas.
    /// </summary>
    public partial class CajaChicaMonto : Form
    {
        /// <summary>
        /// Evento que notifica a suscriptores que el saldo fue actualizado.
        /// Se invoca en el hilo que ejecuta la operación (normalmente UI); los suscriptores que actualicen controles deben sincronizarse si es necesario.
        /// </summary>
        public delegate void ActualizarSaldoDelegate();
        public event ActualizarSaldoDelegate SaldoActualizado;

        /// <summary>
        /// Evento adicional que notifica que se ingresó capital (uso libre para otros subsistemas).
        /// </summary>
        public event Action CapitalIngresado;

        /// <summary>
        /// Identificador de la parroquia asociado a la operación.
        /// </summary>
        private int _parroquiaId;

        /// <summary>
        /// Identificador del usuario que realiza la operación (auditoría).
        /// </summary>
        private int _usuarioId;

        /// <summary>
        /// Indica si el formulario está en modo modificación (true) o inserción (false).
        /// </summary>
        public bool EsModificacion { get; set; } = false;

        /// <summary>
        /// Constructor.
        /// Inicializa componentes y asigna identificadores; también adapta el título y layout en el evento Load.
        /// Notas de sincronización: las modificaciones al UI se realizan en el hilo de interfaz (UI thread).
        /// </summary>
        /// <param name="parroquiaId">Id de la parroquia donde se aplicará el monto.</param>
        /// <param name="usuarioId">Id del usuario que realiza la acción.</param>
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
                    lblTitulo.Text = "MODIFICAR SALDO DE CAJA CHICA";
                    this.Text = "Modificar Saldo";
                }
            };
        }

        /// <summary>
        /// Manejador del botón principal: valida, parsea el monto y llama al procedimiento almacenado correspondiente.
        /// Invoca eventos de sincronización (<see cref="SaldoActualizado"/> y <see cref="CapitalIngresado"/>) después del éxito.
        /// Observación: las llamadas a la base de datos se realizan de forma síncrona; si se desea evitar bloqueo del UI,
        /// ejecutar la operación en un hilo de fondo y aplicar los resultados al UI con Invoke/BeginInvoke.
        /// </summary>
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

                    // Notificar a los suscriptores para que sincronicen sus vistas (invocado en hilo UI).
                    SaldoActualizado?.Invoke();
                    CapitalIngresado?.Invoke();
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Captura errores de SQL (como RAISERROR definidos en los SP).
                    MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    objcone.Cerrar();
                }
            }
        }

        /// <summary>
        /// Valida el campo monto: verifica vacío, formato numérico y que sea mayor a cero.
        /// Devuelve true si la validación pasa; false y enfoca el control en caso contrario.
        /// </summary>
        /// <returns>True si el monto es válido; false en caso contrario.</returns>
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

        /// <summary>
        /// Manejador Load del formulario: centra la ventana y ajusta el título si está en modo modificación.
        /// Si la inicialización carga datos de forma asíncrona, las asignaciones al UI deben hacerse con Invoke/BeginInvoke.
        /// </summary>
        private void FRM_CajaChicaMonto_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (this.EsModificacion)
            {
                lblTitulo.Text = "MODIFICAR SALDO DE CAJA CHICA";
            }
        }

        /// <summary>
        /// Manejador Click del textbox de monto: limpia el placeholder visual si corresponde.
        /// </summary>
        private void txtMonto_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text == "Ingrese monto")
            {
                txtMonto.Text = "";
                txtMonto.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Manejador Leave del textbox de monto: si el campo está vacío restaura el placeholder;
        /// si contiene un número válido lo formatea para mostrarlo con moneda local.
        /// Las operaciones de formato se realizan en el hilo UI.
        /// </summary>
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

        /// <summary>
        /// Controladores vacíos generados por el diseñador.
        /// Se mantienen por compatibilidad con el diseñador; pueden eliminarse si no se usan.
        /// </summary>
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}