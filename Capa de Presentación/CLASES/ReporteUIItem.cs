namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// 
    /// </summary>
    public class ReporteUIItem
    {
        /// <summary>
        /// Gets or sets the tipo reporte identifier.
        /// </summary>
        /// <value>
        /// The tipo reporte identifier.
        /// </value>
        public int tipo_reporte_id { get; set; }
        /// <summary>
        /// Gets or sets the nombre visible.
        /// </summary>
        /// <value>
        /// The nombre visible.
        /// </value>
        public string nombre_visible { get; set; } // Lo que se ve en el ListBox
        /// <summary>
        /// Gets or sets the ruta PDF.
        /// </summary>
        /// <value>
        /// The ruta PDF.
        /// </value>
        public string ruta_pdf { get; set; }
        /// <summary>
        /// Gets or sets the parroquia identifier.
        /// </summary>
        /// <value>
        /// The parroquia identifier.
        /// </value>
        public int parroquia_id { get; set; }
        public string parroquia_nombre { get; set; }
        /// <summary>
        /// Gets or sets the desde.
        /// </summary>
        /// <value>
        /// The desde.
        /// </value>
        public DateTime desde { get; set; }
        /// <summary>
        /// Gets or sets the hasta.
        /// </summary>
        /// <value>
        /// The hasta.
        /// </value>
        public DateTime hasta { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return nombre_visible;
        }
    }
}
