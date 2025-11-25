using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    public class ReporteUIItem
    {
        public int tipo_reporte_id { get; set; }
        public string nombre_visible { get; set; } // Lo que se ve en el ListBox
        public string ruta_pdf { get; set; }
        public int parroquia_id { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }

        public override string ToString()
        {
            return nombre_visible;
        }
    }
}
