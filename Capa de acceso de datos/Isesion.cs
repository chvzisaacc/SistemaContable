using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public static class Isesion
    {
        public static int IdUsuario { get; private set; }
        public static string NombreUsuario { get; private set; }

        public static void IniciarSesion(int id, string nombre)
        {
            IdUsuario = id;
            NombreUsuario = nombre;
        }
    }
}
