using Capa_de_procesamiento_de_datos;
using System.Data;
using System.Diagnostics;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Controla la verificación periódica de alertas mediante un timer y
    /// notifica a la capa de presentación cuando cambia el estado de la alerta.
    /// Mantiene sincronizados los resultados devueltos por la capa de negocio (Alerta)
    /// con la UI a través del evento <see cref="OnAlertaStateChanged"/>.
    /// </summary>
    public class ControlarAlerta
    {
        /// <summary>
        /// Instancia de la capa de procesamiento que expone operaciones relacionadas con alertas.
        /// Se utiliza para consultar el estado actual de la alerta en la base de datos.
        /// </summary>
        private readonly Alerta objalerta = new();

        /// <summary>
        /// Timer de Windows Forms usado para ejecutar verificaciones periódicas en el hilo de la UI.
        /// </summary>
        private readonly System.Windows.Forms.Timer _timerVerificacion;

        /// <summary>
        /// Evento que notifica a la UI cuando cambia el estado de la alerta.
        /// Parámetros: (bool mostrar, string mensaje).
        /// </summary>
        public event Action<bool, string> OnAlertaStateChanged;

        /// <summary>
        /// Constructor que recibe la instancia de <see cref="Alerta"/> a usar y prepara el timer.
        /// </summary>
        /// <param name="alertaBLInstance">Instancia de la lógica de alertas (capa de negocio).</param>
        public ControlarAlerta(Alerta alertaBLInstance)
        {
            objalerta = alertaBLInstance;
            _timerVerificacion = new System.Windows.Forms.Timer();
        }

        /// <summary>
        /// Inicia el monitoreo periódico de alertas. Ejecuta también una verificación inmediata
        /// para sincronizar el estado actual con la UI al comenzar.
        /// </summary>
        /// <param name="intervaloMinutos">Intervalo en minutos entre verificaciones (por defecto 5).</param>
        public void IniciarMonitoreo(int intervaloMinutos = 5)
        {
            if (intervaloMinutos <= 0) intervaloMinutos = 5;

            // Convertir minutos a milisegundos y configurar el timer
            _timerVerificacion.Interval = intervaloMinutos * 60 * 1000;

            // Asociar el manejador que se ejecutará en cada tick
            _timerVerificacion.Tick += TimerVerificacion_Tick;

            // Iniciar el timer en el hilo de UI
            _timerVerificacion.Start();

            // Verificación inicial para sincronizar la UI inmediatamente
            VerificarYNotificarEstado();

            Debug.WriteLine($"Monitoreo de alerta iniciado. Intervalo: {intervaloMinutos} minutos.");
        }

        /// <summary>
        /// Detiene el monitoreo periódico sin alterar el estado almacenado.
        /// </summary>
        public void DetenerMonitoreo()
        {
            _timerVerificacion.Stop();
        }

        /// <summary>
        /// Manejador del evento <c>Tick</c> del timer que dispara la verificación.
        /// </summary>
        private void TimerVerificacion_Tick(object sender, EventArgs e)
        {
            VerificarYNotificarEstado();
        }

        /// <summary>
        /// Consulta la capa de datos para determinar el estado de la alerta y
        /// dispara <see cref="OnAlertaStateChanged"/> para que la UI muestre u oculte
        /// la notificación con el mensaje correspondiente.
        /// </summary>
        private void VerificarYNotificarEstado()
        {
            try
            {
                // Obtener estado desde la capa de procesamiento (SP o lógica de negocio)
                DataTable dtEstado = objalerta.MensajeAlerta();

                if (dtEstado.Rows.Count > 0)
                {
                    // Hay alerta activa: obtener mensaje y notificar a la UI
                    string mensaje = dtEstado.Rows[0]["MensajeAlerta"].ToString();
                    OnAlertaStateChanged?.Invoke(true, mensaje);
                }
                else
                {
                    // No hay alerta: notificar para ocultar cualquier notificación visible
                    OnAlertaStateChanged?.Invoke(false, string.Empty);
                }
            }
            catch (Exception ex)
            {
                // Registrar internamente; la UI no se altera en este flujo
                Debug.WriteLine($"Error en ControlarAlerta: {ex.Message}");
            }
        }

        /// <summary>
        /// Fuerza una verificación inmediata del estado de la alerta y notifica a la UI.
        /// Útil para sincronizar manualmente tras cambios de configuración.
        /// </summary>
        public void ForzarVerificacionInmediata()
        {
            VerificarYNotificarEstado();
        }
    }
}
