using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Capa_de_acceso_de_datos
{
    public class ClsMetodos:ClsAccionesDB
    {
        public int IniciarSesion(string usuario, string contraseña)
        {
            try
            {
                Abrir();
                int rol = ValidarCredenciales(usuario, contraseña);
                return rol;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
