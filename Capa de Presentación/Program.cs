using Capa_de_Presentación.Formularios_Ewin;
using QuestPDF.Infrastructure;


namespace Capa_de_Presentación
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            QuestPDF.Settings.License = LicenseType.Community;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            FRM_PG1 login = new FRM_PG1();
            login.Show();
            Application.Run();

        }
    }
}