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
        public int IniciarSesion(string usuario, string contraseña,int IdParroquia)
        {

            ResultadoLogin resultado = null;

            try
            {
                Abrir();

   
                resultado = ValidarCredenciales(usuario, contraseña,IdParroquia);

                if (resultado != null && resultado.UsuarioID > 0)
                {
   
                    Sesion1.IniciarSesion(resultado.UsuarioID, resultado.RolID,resultado.IdParroquia);

             
                    return resultado.RolID;
                    return resultado.IdParroquia;
                }

                return 0;


            }
            catch (Exception ex)
            {
 
                throw new Exception("Error al iniciar sesión: " + ex.Message);
            }
            finally
            {
                // Asegurar el cierre de la conexión
                Cerrar();
            }
        }



    }
}
