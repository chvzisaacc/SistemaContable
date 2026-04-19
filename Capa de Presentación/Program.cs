using Capa_de_acceso_de_datos;
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
            using (var db = new Capa_de_acceso_de_datos.LocalDbContext())
                db.Database.EnsureCreated();

            QuestPDF.Settings.License = LicenseType.Community;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            Application.ApplicationExit += (s, e) =>
            {
                foreach (var p in System.Diagnostics.Process.GetProcessesByName("BASEDEDATOS.API"))
                    p.Kill();
                foreach (var p in System.Diagnostics.Process.GetProcessesByName("ngrok"))
                    p.Kill();
            };

            IniciarAPI();
            Capa_de_acceso_de_datos.Clsconexion.OnSpEjecutado += (nombreSp, json) =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var motor = new Capa_de_procesamiento_de_datos.LocalDbOff();
                        bool hayServidor = await Capa_de_procesamiento_de_datos.LocalDbOff.ServidorDisponibleAsync();

                        if (hayServidor)
                        {
                            var (exitoso, esErrorNegocio) = await motor.EnviarAlServidorAsync(nombreSp, json);

                            if (!exitoso && !esErrorNegocio)
                            {
                                // Solo guardar pendiente si fue error de RED (timeout, sin conexión)
                                motor.RegistrarProcesoLocal(nombreSp, json);
                            }
                            // Si fue 500/400 = error de negocio, se descarta silenciosamente
                        }
                        else
                        {
                            // Sin servidor = guardar para después
                            motor.RegistrarProcesoLocal(nombreSp, json);
                        }
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText("sync_errors.log",
                            $"{DateTime.Now} - ERROR: {ex.Message}\n{ex.StackTrace}\n");
                    }
                });
            };

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
                var procesosExistentes = System.Diagnostics.Process.GetProcessesByName("BASEDEDATOS.API");
                if (procesosExistentes.Length > 0)
                {
                    _procesoApi = procesosExistentes[0];
                    return;
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
                _procesoApi.StartInfo.UseShellExecute = false;
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
                bool hayServidor = await LocalDbOff.ServidorDisponibleAsync();
                if (!hayServidor) return;

                LocalDbOff motor = new LocalDbOff();

                // Push: enviar pendientes locales al servidor
                await motor.ProcesarColaSincronizacion();

                // Pull: recibir cambios del servidor (nuevo)
                await motor.PullDesdeServidorAsync(Sesion1.id_parroquia);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de sincronización: " + ex.Message);
            }
        }
    }
}