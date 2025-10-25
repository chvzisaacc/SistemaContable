using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    public class Validacion
    {
        public string npassword = "";
        public string confipassword = "";

        public Validacion(string np, string cp) {

            npassword = np;
            confipassword = cp;
        }

        public bool CamposIguales(string nuevaContraseña)
        {
            if (string.IsNullOrEmpty(npassword) || string.IsNullOrEmpty(confipassword))
            {
                return false;
            }
            return npassword.Trim() == confipassword.Trim();
        }


    }
}
