using Capa_de_procesamiento_de_datos;
using System.Data;
using System.Diagnostics;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlarAlerta
    {
        // Instancia de tu capa de negocio/DAL para acceder a la base de datos
        /// <summary>
        /// The objalerta
        /// </summary>
        private readonly Alerta objalerta = new();

        // Objeto Timer para la ejecución periódica
        /// <summary>
        /// The timer verificacion
        /// </summary>
        private readonly System.Windows.Forms.Timer _timerVerificacion;

        // Delegado/Evento para notificar al formulario (UI) sobre el cambio de estado
        // (bool: true=mostrar, false=ocultar; string: mensaje a mostrar)
        /// <summary>
        /// Occurs when [on alerta state changed].
        /// </summary>
        public event Action<bool, string> OnAlertaStateChanged;

        // Constructor: Recibe la instancia de la clase que maneja las operaciones de DB
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlarAlerta"/> class.
        /// </summary>
        /// <param name="alertaBLInstance">The alerta bl instance.</param>
        public ControlarAlerta(Alerta alertaBLInstance)
        {
            objalerta = alertaBLInstance;
            _timerVerificacion = new System.Windows.Forms.Timer();
        }

        /// <summary>
        /// Iniciars the monitoreo.
        /// </summary>
        /// <param name="intervaloMinutos">The intervalo minutos.</param>
        public void IniciarMonitoreo(int intervaloMinutos = 5)
        {
            if (intervaloMinutos <= 0) intervaloMinutos = 5;

            // 1. Configurar el intervalo (Minutos a Milisegundos)
            _timerVerificacion.Interval = intervaloMinutos * 60 * 1000;

            // 2. Asignar el método que se ejecutará periódicamente
            _timerVerificacion.Tick += TimerVerificacion_Tick;

            // 3. Iniciar el Timer
            _timerVerificacion.Start();

            // Ejecutar una verificación inicial inmediata al iniciar
            VerificarYNotificarEstado();

            Debug.WriteLine($"Monitoreo de alerta iniciado. Intervalo: {intervaloMinutos} minutos.");
        }

        /// <summary>
        /// Deteners the monitoreo.
        /// </summary>
        public void DetenerMonitoreo()
        {
            _timerVerificacion.Stop();
        }

        // Método que se llama cuando el Timer se dispara
        /// <summary>
        /// Handles the Tick event of the TimerVerificacion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TimerVerificacion_Tick(object sender, EventArgs e)
        {
            VerificarYNotificarEstado();
        }

        // Lógica principal: Llama al SP y determina si hay inactividad
        /// <summary>
        /// Verificars the y notificar estado.
        /// </summary>
        private void VerificarYNotificarEstado()
        {
            try
            {
                // El SP devuelve un DataTable con una fila si la alerta está activa
                DataTable dtEstado = objalerta.MensajeAlerta();

                if (dtEstado.Rows.Count > 0)
                {
                    // Alerta activa: Notificar al formulario que muestre el mensaje
                    string mensaje = dtEstado.Rows[0]["MensajeAlerta"].ToString();

                    // Dispara el evento, indicando TRUE (mostrar) y el mensaje
                    OnAlertaStateChanged?.Invoke(true, mensaje);
                }
                else
                {
                    // Alerta inactiva: Notificar al formulario que oculte la alerta
                    OnAlertaStateChanged?.Invoke(false, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en ControlarAlerta: {ex.Message}");
            }
        }

        /// <summary>
        /// Forzars the verificacion inmediata.
        /// </summary>
        public void ForzarVerificacionInmediata()
        {
            VerificarYNotificarEstado();
        }
    }
}