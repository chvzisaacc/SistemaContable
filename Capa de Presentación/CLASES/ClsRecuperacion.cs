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
        public void IniciarSesion(string usuario, string contraseña,int idparroquia, Form formularioActual, Label lblMensaje)
        {
            try
            {
                ClsMetodos metodos = new ClsMetodos();
                int rol = metodos.IniciarSesion(usuario, contraseña, idparroquia);

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
                else if (rol == 1)
                {
                    FRM_PG5 admin = new FRM_PG5();
                    admin.Show();
                    formularioActual.Hide();
                }
                else if (rol == 2 || rol == 3)
                {
                    FRM_42 empleado = new FRM_42();
                    empleado.Show();
                    formularioActual.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }

}

