namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public static class Sesion1
    {
        public static int rol;
        public static int parroquia_id;

        /// <summary>
        /// Gets the usuario identifier.
        /// </summary>
        /// <value>
        /// The usuario identifier.
        /// </value>
        public static int usuario_id { get; private set; } = 0;

        /// <summary>
        /// Gets the rol identifier.
        /// </summary>
        /// <value>
        /// The rol identifier.
        /// </value>
        public static int rol_id { get; private set; } = 0;
        /// <summary>
        /// Gets or sets the identifier parroquia.
        /// </summary>
        /// <value>
        /// The identifier parroquia.
        /// </value>
        public static int id_parroquia { get; set; } = 0;

        /// <summary>
        /// Gets the correo.
        /// </summary>
        /// <value>
        /// The correo.
        /// </value>
        public static string correo { get; private set; } = null;

        /// <summary>
        /// Gets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public static string nombre { get; private set; } = null;

        /// <summary>
        /// Gets or sets the utimo guardado certificado.
        /// </summary>
        /// <value>
        /// The utimo guardado certificado.
        /// </value>
        public static DateTime utimo_guardado_certificado { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Gets or sets the ultimo identifier certificado guardado.
        /// </summary>
        /// <value>
        /// The ultimo identifier certificado guardado.
        /// </value>
        public static int ultimo_id_certificado_guardado { get; set; } = 0;

        /// <summary>
        /// Iniciars the sesion.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rol">The rol.</param>
        /// <param name="idparroquiaa">The idparroquiaa.</param>
        /// <param name="nombreUsuario">The nombre usuario.</param>
        public static void IniciarSesion(int id, int rol, int idparroquiaa, string nombreUsuario = null)
        {
            usuario_id = id;
            rol_id = rol;
            id_parroquia = idparroquiaa;
            nombre = nombreUsuario;
            correo = null;
        }

        /// <summary>
        /// Cerrars the sesion.
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
        /// Sets the correo.
        /// </summary>
        /// <param name="correo">The correo.</param>
        public static void SetCorreo(string correo) => correo = correo;
    }
}
