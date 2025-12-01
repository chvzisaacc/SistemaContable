using System.Data.SqlTypes;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsCajachica
    {
        /// <summary>
        /// Gets or sets the identifier cajachica.
        /// </summary>
        /// <value>
        /// The identifier cajachica.
        /// </value>
        public int id_cajachica { get; set; }
        /// <summary>
        /// Gets or sets the saldo.
        /// </summary>
        /// <value>
        /// The saldo.
        /// </value>
        public SqlMoney saldo { get; set; }
    }
}
