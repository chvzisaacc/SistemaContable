namespace Capa_de_acceso_de_datos
{
    public static class Sesion1
    {



        public static int usuario_id { get; private set; } = 0;

        public static int rol_id { get; private set; } = 0;
        public static int id_parroquia { get; set; } = 0;

        public static string correo { get; private set; } = null;

        public static DateTime UltimoGuardadoCertificado { get; set; } = DateTime.MinValue;
        public static int UltimoIdCertificadoGuardado { get; set; } = 0;

        public static void IniciarSesion(int id, int rol, int idparroquiaa)
        {
            usuario_id = id;
            rol_id = rol;
            id_parroquia = idparroquiaa; // Almacenamos el ID de Parroquia
            correo = null;  // limpia cache al loguear
        }

        public static void CerrarSesion()
        {
            usuario_id = 0;
            rol_id = 0;
            id_parroquia = 0;
            correo = null;
        }

        public static void SetCorreo(string correo) => correo = correo;
    }
}
