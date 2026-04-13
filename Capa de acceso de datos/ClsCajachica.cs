using System.Data.SqlTypes;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Modelo de datos que representa la caja chica de una parroquia.
    /// Contiene identificador único y saldo monetario con precisión financiera.
    /// Se utiliza para transferencias, consultas y operaciones de la caja chica.
    /// </summary>
    public class ClsCajachica
    {
        /// <summary>
        /// Identificador único de la caja chica asignado por la base de datos.
        /// Valor inmutable generado automáticamente y utilizado como clave primaria.
        /// Se obtiene al crear una nueva caja chica en el sistema.
        /// </summary>
        public int id_cajachica { get; set; }

        /// <summary>
        /// Saldo monetario actual de la caja chica expresado en SqlMoney.
        /// Se actualiza automáticamente cuando se realizan ingresos, egresos o transferencias.
        /// Utiliza SqlMoney para garantizar precisión de 4 decimales en cálculos financieros.
        /// Rango soportado: -922,337,203,685,477.5808 a +922,337,203,685,477.5807
        /// 
        /// Operaciones que afectan este saldo:
        /// - Ingreso: suma cantidad disponible
        /// - Egreso: resta cantidad disponible
        /// - Transferencia: resta de cuenta origen, suma en cuenta destino
        /// - Sincronización remota: se propaga automáticamente al servidor
        /// </summary>
        public SqlMoney saldo { get; set; }
    }
}