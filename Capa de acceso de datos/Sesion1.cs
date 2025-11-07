using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public static class Sesion1
    {
        public static int UsuarioID { get; private set; } = 0;

        public static int RolID { get; private set; } = 0;

        public static string Correo { get; private set; } = null;

        public static void IniciarSesion(int id, int rol)
        {
            UsuarioID = id;
            RolID = rol;
            Correo = null;  // limpia cache al loguear
        }

        public static void CerrarSesion()
        {
            UsuarioID = 0;
            RolID = 0;
            Correo = null;
        }

        public static void SetCorreo(string correo) => Correo = correo;
    }
}
