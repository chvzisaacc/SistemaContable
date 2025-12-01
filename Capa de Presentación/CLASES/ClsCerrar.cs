using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsCerrar
    {
        /// <summary>
        /// Cerrars the application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        public void CerrarApp(object? sender, FormClosingEventArgs e)
        {
            try
            {
                Clsconexion conexion = new Clsconexion();
                conexion.Cerrar();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}
