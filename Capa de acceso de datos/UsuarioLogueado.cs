using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public static class UsuarioLogueado
    {
        public static int UsuarioId { get; set; }
        public static string Nombre { get; set; }
        public static int RolId { get; set; }
        public static int ParroquiaId { get; set; }
    }
}
