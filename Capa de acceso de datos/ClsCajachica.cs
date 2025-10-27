using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_acceso_de_datos
{
    public class ClsCajachica
    {
        public int Id_cajachica { get; set; }
        public SqlMoney saldo { get; set; }
    }
}
