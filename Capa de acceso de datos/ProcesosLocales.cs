using Microsoft.EntityFrameworkCore;
using System;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Modelo de datos que representa un proceso local pendiente de sincronización con servidor remoto.
    /// Almacena información de operaciones ejecutadas localmente que deben replicarse en el servidor.
    /// Se utiliza en la cola de sincronización para garantizar consistencia de datos entre cliente y servidor.
    /// </summary>
    public class ProcesosLocales
    {
        /// <summary>
        /// Identificador único del proceso local en la base de datos SQLite.
        /// Clave primaria auto-incrementada generada automáticamente por la BD.
        /// Se utiliza para rastrear y sincronizar procesos específicos.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo de objeto/entidad que fue modificado localmente.
        /// Ejemplos: "Usuario", "Transferencia", "Ingreso", "Gasto", "Certificado", "ParroquiaTransferencia", etc.
        /// Identifica qué tipo de operación se debe sincronizar al servidor.
        /// Se utiliza para enrutar el proceso a la clase remota correspondiente mediante AccesoRemoto.
        /// </summary>
        public string TipoObjeto { get; set; }

        /// <summary>
        /// Datos completos del proceso serializado en formato JSON.
        /// Contiene todos los parámetros y valores necesarios para replicar la operación en servidor remoto.
        /// Ejemplo: {"SpName":"sp_AgregarUsuario","Parametros":{"nombre":"Juan","apellido":"Pérez",...},"_ParroquiaId":1}
        /// Se deserializa y se envía a AccesoRemoto.EjecutarSpRemoto() durante la sincronización.
        /// </summary>
        public string DatosJson { get; set; }

        /// <summary>
        /// Indicador de estado de sincronización del proceso.
        /// true: el proceso fue sincronizado exitosamente con servidor remoto (puede ser eliminado).
        /// false: el proceso está pendiente de sincronización (debe reintentarse).
        /// Se utiliza para filtrar procesos a sincronizar en la cola local.
        /// </summary>
        public bool Sincronizado { get; set; }

        /// <summary>
        /// Fecha y hora de registro del proceso en la base de datos local.
        /// Valor por defecto: DateTime.Now (momento de creación del registro).
        /// Se utiliza para ordenar la cola de sincronización (FIFO) y auditoría de procesos.
        /// Importante: mantiene el orden de ejecución para garantizar integridad de datos.
        /// </summary>
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}