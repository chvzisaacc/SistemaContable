namespace Capa_de_acceso_de_datos
{
    public class Usuario
    {

        public int usuario_id { get; set; }
        public string usuario_nombre { get; set; }
        public int rol_id { get; set; }
        public int id_estado_cuenta { get; set; }

        

        public Usuario()
        {

        }
        public Usuario(int id, string nombre)
        {
            usuario_id = id;
            usuario_nombre = nombre;
        }
    }
}
