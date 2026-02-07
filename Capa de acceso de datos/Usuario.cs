namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class Usuario
    {

        /// <summary>
        /// Gets or sets the usuario identifier.
        /// </summary>
        /// <value>
        /// The usuario identifier.
        /// </value>
        public int usuario_id { get; set; }
        /// <summary>
        /// Gets or sets the usuario nombre.
        /// </summary>
        /// <value>
        /// The usuario nombre.
        /// </value>
        public string NombreCompleto { get; set; }
        /// <summary>
        /// Gets or sets the rol identifier.
        /// </summary>
        /// <value>
        /// The rol identifier.
        /// </value>
        public int rol_id { get; set; }
        /// <summary>
        /// Gets or sets the identifier estado cuenta.
        /// </summary>
        /// <value>
        /// The identifier estado cuenta.
        /// </value>
        public int id_estado_cuenta { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="Usuario"/> class.
        /// </summary>
        public Usuario()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Usuario"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        public Usuario(int id, string nombre)
        {
            usuario_id = id;
            NombreCompleto = nombre;
        }
    }
}
