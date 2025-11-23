namespace Capa_de_acceso_de_datos
{
    public static class Sesion1
    {



        public static int UsuarioId { get; private set; } = 0;

        public static int RolID { get; private set; } = 0;
        public static int IdParroquia { get; set; } = 0;

        public static string Correo { get; private set; } = null;

        public static DateTime UltimoGuardadoCertificado { get; set; } = DateTime.MinValue;
        public static int UltimoIdCertificadoGuardado { get; set; } = 0;

        public static void IniciarSesion(int id, int rol, int idParroquia)
        {
            UsuarioId = id;
            RolID = rol;
            IdParroquia = idParroquia; // Almacenamos el ID de Parroquia
            Correo = null;  // limpia cache al loguear
        }

        public static void CerrarSesion()
        {
            UsuarioId = 0;
            RolID = 0;
            IdParroquia = 0;
            Correo = null;
        }

        public static void SetCorreo(string correo) => Correo = correo;
    }
}
