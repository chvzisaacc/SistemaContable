using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    public class ReporteUIItem
    {
        public int TipoReporteId { get; set; }
        public string NombreVisible { get; set; } // Lo que se ve en el ListBox
        public string RutaPdf { get; set; }
        public int ParroquiaId { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }

        public override string ToString()
        {
            return NombreVisible;
        }
    }
}
