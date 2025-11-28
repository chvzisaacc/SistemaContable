using System;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using Capa_de_procesamiento_de_datos;

namespace Capa_de_Presentación.CLASES
{
    public class ControlarAlerta
    {
        // Instancia de tu capa de negocio/DAL para acceder a la base de datos
        private readonly Alerta objalerta = new();

        // Objeto Timer para la ejecución periódica
        private readonly System.Windows.Forms.Timer _timerVerificacion;

        // Delegado/Evento para notificar al formulario (UI) sobre el cambio de estado
        // (bool: true=mostrar, false=ocultar; string: mensaje a mostrar)
        public event Action<bool, string> OnAlertaStateChanged;

        // Constructor: Recibe la instancia de la clase que maneja las operaciones de DB
        public ControlarAlerta(Alerta alertaBLInstance)
        {
            objalerta = alertaBLInstance;
            _timerVerificacion = new System.Windows.Forms.Timer();
        }

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

        public void DetenerMonitoreo()
        {
            _timerVerificacion.Stop();
        }

        // Método que se llama cuando el Timer se dispara
        private void TimerVerificacion_Tick(object sender, EventArgs e)
        {
            VerificarYNotificarEstado();
        }

        // Lógica principal: Llama al SP y determina si hay inactividad
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

        public void ForzarVerificacionInmediata()
        {
            VerificarYNotificarEstado();
        }
    }
}