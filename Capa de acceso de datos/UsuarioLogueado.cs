using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public static class UsuarioLogueado
    {
        public static int usuario_id { get; set; }
        public static string nombre { get; set; }
        public static int rol_id { get; set; }
        public static int parroquia_id { get; set; }
    }
}
