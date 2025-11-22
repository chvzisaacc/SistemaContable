namespace Capa_de_acceso_de_datos
{
    public class Usuario
    {
        public int Usuario_id { get; set; }
        public string usuario_nombre { get; set; }
        public int Rol_id { get; set; }
        public int Id_estado_cuenta { get; set; }

        public Usuario()
        {

        }
        public Usuario(int id, string nombre)
        {
            Usuario_id = id;
            usuario_nombre = nombre;
        }
    }
}
