namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Utilidad de validación simple para comparar contraseñas.
    /// Se usa en formularios de la UI para verificar que los campos
    /// "nueva contraseña" y "confirmar contraseña" coincidan antes de
    /// persistir cambios en la capa de datos.
    /// </summary>
    public class Validacion
    {
        /// <summary>
        /// Nueva contraseña proporcionada por el usuario.
        /// </summary>
        public string npassword = "";

        /// <summary>
        /// Confirmación de la nueva contraseña proporcionada por el usuario.
        /// </summary>
        public string confipassword = "";

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Validacion"/> con
        /// los valores de contraseña y confirmación recibidos.
        /// </summary>
        /// <param name="np">Nueva contraseña.</param>
        /// <param name="cp">Confirmación de la nueva contraseña.</param>
        public Validacion(string np, string cp)
        {
            npassword = np;
            confipassword = cp;
        }

        /// <summary>
        /// Comprueba si las contraseñas proporcionadas son iguales.
        /// - Retorna false si alguno de los campos está vacío.
        /// - Realiza comparación tras aplicar <see cref="string.Trim"/> para evitar diferencias por espacios.
        /// </summary>
        /// <param name="nuevaContraseña">Parámetro adicional (no usado en la implementación actual) reservado para compatibilidad.</param>
        /// <returns>True si las contraseñas coinciden; de lo contrario false.</returns>
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
