using Capa_de_acceso_de_datos;
using Capa_de_Presentación.Formularios_Ewin;

namespace Capa_de_Presentación.CLASES
{
    public class ClsVerificarCod
    {
        public void ProcesarCodigoRecuperacion(int usuarioId, string codigo, string correoUsuario, Form formularioActual)
        {
            try
            {
                ClsAccionesDB acciones = new ClsAccionesDB();
                string resultado = acciones.ValidarCodigoRecuperacion(usuarioId, codigo);

                switch (resultado)
                {
                    case "CODIGO_VALIDO":
                        MessageBox.Show("Código verificado correctamente.");
                        FRM_PG4 frm = new(correoUsuario);
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
                MessageBox.Show("Error al verificar el código: " + ex.Message);
            }
        }
    }
}
