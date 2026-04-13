using Capa_de_acceso_de_datos;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Provee lógica de cierre de la aplicación y liberación de recursos.
    /// Documenta las operaciones de cierre y asegura que la conexión con la
    /// capa de acceso a datos se cierre antes de terminar el proceso.
    /// </summary>
    public class ClsCerrar
    {
        /// <summary>
        /// Maneja el cierre de la aplicación. Se encarga de cerrar la conexión a la base de datos,
        /// terminar el proceso actual y llamar a <see cref="Application.Exit"/> para solicitar
        /// la salida ordenada. Se usa típicamente como manejador de <c>FormClosing</c>.
        /// </summary>
        /// <param name="sender">El origen del evento de cierre (puede ser null).</param>
        /// <param name="e">Argumentos del evento de cierre del formulario.</param>
        public void CerrarApp(object? sender, FormClosingEventArgs e)
        {
            try
            {
                // Crear instancia de conexión para asegurar el cierre de recursos.
                Clsconexion conexion = new Clsconexion();

                // Cerrar la conexión a la base de datos antes de terminar el proceso.
                conexion.Cerrar();

                // Forzar terminación del proceso actual. Esto asegura que no queden hilos en ejecución.
                System.Diagnostics.Process.GetCurrentProcess().Kill();

                // Solicitar salida ordenada de la aplicación (por redundancia tras Kill()).
                Application.Exit();
            }
            catch (Exception ex)
            {
                // Mostrar error si ocurre alguna excepción durante el cierre.
                MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}
