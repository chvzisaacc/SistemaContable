namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Clase estática que almacena información del usuario actualmente logueado en la sesión.
    /// Proporciona acceso global a datos de identificación del usuario sin parámetros.
    /// Se utiliza en toda la aplicación para obtener contexto del usuario actual.
    /// Complementa a Sesion1 con información adicional de usuario.
    /// Crítico para sincronización remota: proporciona usuario_id y parroquia_id para eventos remotos.
    /// </summary>
    public static class UsuarioLogueado
    {
        /// <summary>
        /// Identificador único del usuario actualmente logueado.
        /// Se utiliza para identificar al usuario en todas las operaciones de la aplicación.
        /// Requerido en eventos de sincronización remota para auditoría y trazabilidad.
        /// Se obtiene durante la autenticación y se mantiene durante toda la sesión.
        /// </summary>
        public static int usuario_id { get; set; }

        /// <summary>
        /// Nombre completo del usuario actualmente logueado.
        /// Se utiliza para mostrar información del usuario en la interfaz de usuario.
        /// Se incluye en logs, reportes y auditoría de operaciones.
        /// Facilita la identificación visual del usuario en la aplicación.
        /// </summary>
        public static string nombre { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario actualmente logueado.
        /// Valores típicos: 2=Administrador, 3=Empleado, 4=Sacerdote.
        /// Se utiliza para control de acceso a funcionalidades y formularios específicos.
        /// Determina qué operaciones y vistas están disponibles para el usuario.
        /// </summary>
        public static int rol_id { get; set; }

        /// <summary>
        /// Identificador de la parroquia a la que pertenece el usuario actualmente logueado.
        /// Se utiliza para filtrar datos y operaciones al contexto de la parroquia actual.
        /// Crítico para sincronización remota: se incluye en eventos remotos (_ParroquiaId).
        /// Asegura que el usuario solo tenga acceso a datos de su parroquia.
        /// </summary>
        public static int parroquia_id { get; set; }
    }
}