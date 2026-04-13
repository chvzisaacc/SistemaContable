namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Clase estática que gestiona el contexto de sesión del usuario autenticado.
    /// Almacena información del usuario logueado: ID, rol, parroquia, correo, nombre.
    /// Mantiene estado de sincronización: último certificado guardado y fecha.
    /// Se utiliza en toda la aplicación para acceder a datos del usuario actual sin parámetros.
    /// </summary>
    public static class Sesion1
    {
        /// <summary>
        /// Identificador único del usuario autenticado actualmente.
        /// Valor por defecto: 0 (sin sesión activa).
        /// Se utiliza para identificar al usuario en todas las operaciones de la aplicación.
        /// Establecido al iniciar sesión, restablecido a 0 al cerrar sesión.
        /// </summary>
        public static int usuario_id { get; set; } = 0;

        /// <summary>
        /// Identificador del rol asignado al usuario autenticado.
        /// Valores típicos: 2=Administrador, 3=Empleado, 4=Sacerdote.
        /// Valor por defecto: 0 (sin rol asignado).
        /// Se utiliza para controlar permisos y mostrar/ocultar funcionalidades según rol.
        /// Establecido al iniciar sesión, restablecido a 0 al cerrar sesión.
        /// </summary>
        public static int rol_id { get; set; } = 0;

        /// <summary>
        /// Identificador de la parroquia a la que pertenece el usuario autenticado.
        /// Valor por defecto: 0 (sin parroquia asignada).
        /// Se utiliza para filtrar datos y operaciones al contexto de la parroquia actual.
        /// Establecido al iniciar sesión, restablecido a 0 al cerrar sesión.
        /// Crítico para sincronización: incluido en eventos remotos (_ParroquiaId).
        /// </summary>
        public static int id_parroquia { get; set; } = 0;

        /// <summary>
        /// Correo electrónico del usuario autenticado.
        /// Valor por defecto: null (sin asignar).
        /// Solo lectura: se puede asignar mediante método SetCorreo().
        /// Se utiliza para notificaciones, recuperación de contraseña y comunicaciones.
        /// Inicialmente null, se carga bajo demanda cuando es necesario.
        /// </summary>
        public static string correo { get; private set; } = null;

        /// <summary>
        /// Nombre completo del usuario autenticado (nombre + apellido).
        /// Valor por defecto: null (sin asignar).
        /// Solo lectura: se asigna automáticamente al iniciar sesión.
        /// Se utiliza para mostrar información de usuario en interfaz y logs.
        /// Establecido al iniciar sesión, restablecido a null al cerrar sesión.
        /// </summary>
        public static string nombre { get; private set; } = null;

        /// <summary>
        /// Fecha y hora del último certificado de depósito guardado por el usuario.
        /// Valor por defecto: DateTime.MinValue (sin certificado guardado).
        /// Se utiliza para validar periodicidad de guardados y controlar operaciones de certificados.
        /// Actualizado automáticamente al crear/guardar un certificado.
        /// </summary>
        public static DateTime utimo_guardado_certificado { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Identificador del último certificado de depósito guardado por el usuario.
        /// Valor por defecto: 0 (sin certificado guardado).
        /// Se utiliza para referenciar el certificado más recientemente creado.
        /// Actualizado automáticamente al crear/guardar un certificado.
        /// Facilita validaciones y referencias a certificados recientes.
        /// </summary>
        public static int ultimo_id_certificado_guardado { get; set; } = 0;

        /// <summary>
        /// Inicializa la sesión del usuario autenticado con su información de identificación.
        /// Parámetros: id (usuario_id), rol (rol_id), idparroquiaa (id_parroquia),
        /// nombreUsuario (nombre, opcional).
        /// 
        /// Flujo:
        /// 1. Asigna ID, rol, parroquia e información del usuario
        /// 2. Reinicia correo a null (se carga bajo demanda)
        /// 3. Prepara contexto para todas las operaciones posteriores
        /// 
        /// Se invoca después de autenticación exitosa en ClsMetodos.IniciarSesion().
        /// Hace que el usuario esté disponible globalmente en toda la aplicación.
        /// </summary>
        public static void IniciarSesion(int id, int rol, int idparroquiaa, string nombreUsuario = null)
        {
            usuario_id = id;
            rol_id = rol;
            id_parroquia = idparroquiaa;
            nombre = nombreUsuario;
            correo = null;
        }

        /// <summary>
        /// Cierra la sesión actual del usuario limpiando todos los datos de contexto.
        /// Reinicia todas las propiedades a valores por defecto (0, null).
        /// 
        /// Flujo:
        /// 1. Reinicia usuario_id a 0
        /// 2. Reinicia rol_id a 0
        /// 3. Reinicia id_parroquia a 0
        /// 4. Reinicia correo a null
        /// 5. Reinicia nombre a null
        /// 
        /// Se invoca al hacer logout: cierra formulario y limpia estado de sesión.
        /// Crucial: evita filtraciones de datos del usuario anterior si alguien comparte equipo.
        /// NO reinicia datos de sincronización (último certificado se mantiene para estadísticas).
        /// </summary>
        public static void CerrarSesion()
        {
            usuario_id = 0;
            rol_id = 0;
            id_parroquia = 0;
            correo = null;
            nombre = null;
        }

        /// <summary>
        /// Asigna el correo electrónico del usuario actual en la sesión.
        /// Parámetro correo: dirección de correo electrónico a asignar.
        /// Se utiliza para cargar el correo bajo demanda cuando es necesario.
        /// Generalmente invocado después de consultar correo a la base de datos.
        /// 
        /// NOTA: Hay un bug en esta implementación - el parámetro y la propiedad tienen el mismo nombre,
        /// por lo que la asignación no funciona correctamente. La línea debería ser: Sesion1.correo = correo;
        /// </summary>
        public static void SetCorreo(string correo) => correo = correo;
    }
}