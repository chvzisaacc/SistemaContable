namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// 
    /// </summary>
    public class Validacion
    {
        /// <summary>
        /// The npassword
        /// </summary>
        public string npassword = "";
        /// <summary>
        /// The confipassword
        /// </summary>
        public string confipassword = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Validacion"/> class.
        /// </summary>
        /// <param name="np">The np.</param>
        /// <param name="cp">The cp.</param>
        public Validacion(string np, string cp)
        {

            npassword = np;
            confipassword = cp;
        }

        /// <summary>
        /// Camposes the iguales.
        /// </summary>
        /// <param name="nuevaContraseña">The nueva contraseña.</param>
        /// <returns></returns>
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
