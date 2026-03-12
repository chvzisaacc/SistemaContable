using Capa_de_Presentación.Formularios_Ewin;
using QuestPDF.Infrastructure;
using System.Timers; // Necesario para el Timer
using Capa_de_procesamiento_de_datos; // Para acceder a LocalDbOff

namespace Capa_de_Presentación
{
    internal static class Program
    {
        // Definimos el Timer como estático para que no sea destruido por el recolector de basura
        private static System.Timers.Timer syncTimer;

        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            ConfigurarSincronizador(30000); // Se ejecutará cada 30 segundos

            FRM_PG1 login = new FRM_PG1();
            login.Show();
            Application.Run();
        }

        private static void ConfigurarSincronizador(int intervalo)
        {
            // Creamos el timer con el intervalo
            syncTimer = new System.Timers.Timer(intervalo);

            // Asignamos el evento que ocurrirá cada vez que pase el tiempo
            syncTimer.Elapsed += async (sender, e) => await EjecutarSincronizacion();

            syncTimer.AutoReset = true;
            syncTimer.Enabled = true;
        }

        private static async Task EjecutarSincronizacion()
        {
            try
            {
                //clase de lógica offline
                LocalDbOff motor = new LocalDbOff();

                // Ejecutamos el envío de datos pendientes
                await motor.ProcesarColaSincronizacion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de sincronización: " + ex.Message);
            }
        }
    }
}