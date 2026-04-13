namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Modelo de datos que representa la información de un usuario del sistema.
    /// Contiene identificación, nombre completo, rol asignado y estado de la cuenta.
    /// Se utiliza para transferencia de datos de usuario entre capas y en operaciones de sincronización.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario en el sistema.
        /// Clave primaria generada por la base de datos.
        /// Se utiliza para identificar al usuario en todas las operaciones y transacciones.
        /// Requerido en eventos de sincronización remota para auditoría.
        /// </summary>
        public int usuario_id { get; set; }

        /// <summary>
        /// Nombre completo del usuario (nombre y apellido).
        /// Se utiliza para mostrar información del usuario en la interfaz y reportes.
        /// Se incluye en logs y auditoría de operaciones para trazabilidad.
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario.
        /// Valores típicos: 2=Administrador, 3=Empleado, 4=Sacerdote.
        /// Se utiliza para control de acceso y permisos a funcionalidades específicas.
        /// Requerido para determinar qué formularios y operaciones puede realizar.
        /// </summary>
        public int rol_id { get; set; }

        /// <summary>
        /// Identificador del estado actual de la cuenta del usuario.
        /// Valores típicos: 1=Activo, 0=Inactivo.
        /// Se utiliza para validar que el usuario tenga cuenta habilitada antes de permitir acceso.
        /// Controla el ciclo de vida de la cuenta (activación, inhabilitación, bloqueos).
        /// </summary>
        public int id_estado_cuenta { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Usuario sin parámetros.
        /// Constructor por defecto utilizado para desserialización y mapeo de datos.
        /// Se utiliza al cargar usuarios desde base de datos o deserializar JSON en sincronización.
        /// </summary>
        public Usuario()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Usuario con identificador y nombre completo.
        /// Parámetros: id (usuario_id), nombre (NombreCompleto).
        /// Constructor de conveniencia utilizado para crear instancias de usuario rápidamente.
        /// Se utiliza en operaciones de lectura y en sincronización de datos de usuario.
        /// </summary>
        public Usuario(int id, string nombre)
        {
            usuario_id = id;
            NombreCompleto = nombre;
        }
    }
}