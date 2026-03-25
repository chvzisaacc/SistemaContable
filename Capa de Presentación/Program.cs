using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using Capa_de_procesamiento_de_datos;
using QuestPDF.Infrastructure;

namespace Capa_de_Presentación
{
    internal static class Program
    {
        private static System.Timers.Timer syncTimer;
        private static System.Diagnostics.Process _procesoApi;

        [STAThread]
        static void Main()
        {
            
            QuestPDF.Settings.License = LicenseType.Community;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            ConfigurarSincronizador(30000);

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

            if (syncTimer != null)
            {
                syncTimer.Stop();
                syncTimer.Dispose();
            }

            if (_procesoApi != null && !_procesoApi.HasExited)
                _procesoApi.Kill();
        }

        private static void IniciarAPI()
        {
            try
            {
                // Verificar si la API ya está corriendo
                var procesosExistentes = System.Diagnostics.Process.GetProcessesByName("BASEDEDATOS.API");
                if (procesosExistentes.Length > 0)
                {
                    _procesoApi = procesosExistentes[0];
                    return; // Ya está corriendo
                }

                string rutaApi = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "BASEDEDATOS.API.exe"
                );

                if (!File.Exists(rutaApi))
                {
                    rutaApi = Path.GetFullPath(Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        @"..\..\..\..\BASEDEDATOS.API\bin\Debug\net8.0\BASEDEDATOS.API.exe"
                    ));
                }

                if (!File.Exists(rutaApi))
                {
                    MessageBox.Show("No se encontró la API.\nRuta buscada: " + rutaApi, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _procesoApi = new System.Diagnostics.Process();
                _procesoApi.StartInfo.FileName = rutaApi;
                _procesoApi.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                _procesoApi.StartInfo.CreateNoWindow = true;
                _procesoApi.Start();

                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la API: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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