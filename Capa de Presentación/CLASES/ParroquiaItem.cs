namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Representa un elemento de parroquia para su uso en controles de UI (por ejemplo ComboBox).
    /// Contiene el identificador y el nombre legible que se mostrará en la interfaz.
    /// </summary>
    public class ParroquiaItem
    {
        /// <summary>
        /// Identificador único de la parroquia.
        /// Se usa para operaciones internas y para mapear en la base de datos.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nombre de la parroquia que se mostrará en la UI.
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// Devuelve la representación en texto del elemento.
        /// Se sobreescribe para que controles como ComboBox muestren directamente el nombre.
        /// </summary>
        /// <returns>El <see cref="string"/> con el nombre de la parroquia.</returns>
        public override string ToString()
        {
            return nombre; // esto hace que el combo muestre solo el nombre
        }
    }
}
