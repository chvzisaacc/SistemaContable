using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentación.CLASES
{
    public class ClsCodigo:Clsconexion
    {

        private ClsAccionesDB acciones;

        public ClsCodigo()
        {
            acciones = new ClsAccionesDB();
        }

        public void ProcesarCodigoRecuperacion(int usuarioId, string codigo,string correo, Form formularioActual)
        {
            string resultado = acciones.ValidarCodigoRecuperacion(usuarioId, codigo);

            switch (resultado)
            {
                case "CODIGO_VALIDO":
                    MessageBox.Show("Código verificado correctamente.");
                    FRM_PG4 frm = new FRM_PG4(correo);
                    frm.Show();
                    formularioActual.Hide();
                    break;

                case "CODIGO_INCORRECTO":
                    MessageBox.Show("Código incorrecto. Intente nuevamente.");
                    break;

                case "CODIGO_EXPIRADO":
                    MessageBox.Show("El código ha expirado. Solicite uno nuevo.");
                    break;

                case "CUENTA_INHABILITADA":
                    MessageBox.Show("Su cuenta ha sido bloqueada por seguridad.");
                    Application.Exit();
                    break;

                case "SIN_CODIGO":
                    MessageBox.Show("No hay ningún código activo para este usuario.");
                    break;

                default:
                    MessageBox.Show("Error: código no válido.");
                    break;
            }
        }
    }
}
