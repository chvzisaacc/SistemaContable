using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using QuestPDF.Infrastructure;
using System.Timers;
using Capa_de_procesamiento_de_datos;

namespace Capa_de_Presentación
{
    internal static class Program
    {
        private static System.Timers.Timer syncTimer;

        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            ApplicationConfiguration.Initialize();
            ConfigurarSincronizador(30000); // cada 30 segundos

            using (FRM_PG1 login = new FRM_PG1())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Form principal;

                    if (login.Rol == 1)
                        principal = new Ventana_Principal_Administrador(login.UsuarioId, login.ParroquiaId);
                    else
                        principal = new FRM_42(login.UsuarioId, login.ParroquiaId);

                    Application.Run(principal);
                }
            }

            // opcional: detener timer al salir
            if (syncTimer != null)
            {
                syncTimer.Stop();
                syncTimer.Dispose();
            }
        }

        private static void ConfigurarSincronizador(int intervalo)
        {
            syncTimer = new System.Timers.Timer(intervalo);
            syncTimer.Elapsed += async (sender, e) => await EjecutarSincronizacion();
            syncTimer.AutoReset = true;
            syncTimer.Enabled = true;
        }

        private static async Task EjecutarSincronizacion()
        {
            try
            {
                LocalDbOff motor = new LocalDbOff();
                await motor.ProcesarColaSincronizacion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de sincronización: " + ex.Message);
            }
        }
    }
}
