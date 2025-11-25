using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    public class ParroquiaItem
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public override string ToString()
        {
            return nombre; // esto hace que el combo muestre solo el nombre
        }
    }
}
