using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;
using Capa_de_Presentación.Formularios_Luiss;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentación.CAPAS
{
    public class ClsRecuperacion
    {

        public int IniciarSesion(string usuario, string contraseña, int idparroquia, FRM_PG1 fRM_PG1, Label lblMensaje)
        {
            ClsMetodos metodos = new ClsMetodos();
            int rol = metodos.IniciarSesion(usuario, contraseña, idparroquia);

            var ids = metodos.ObtenerUsuarioIdPorNombreUsuario(usuario);
            if (rol == -1)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Su cuenta está inhabilitada.";
            }
            else if (rol == 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Credenciales incorrectas";
            }

            return rol;
        }


        public void ProcesarCodigoRecuperacion(int usuarioId, string codigo, Form formularioActual)
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                string resultado = acciones.ValidarCodigoRecuperacion(usuarioId, codigo);

                switch (resultado)
                {
                    case "CODIGO_VALIDO":
                        MessageBox.Show("Código verificado correctamente.");
                        FRM_PG4 frm = new FRM_PG4();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal int IniciarSesion(string text1, string text2, int v, Label label1)
        {
            throw new NotImplementedException();
        }
    }

}

