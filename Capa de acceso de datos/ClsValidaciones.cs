using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsValidaciones
    {
        // 1. Solo números enteros
        /// <summary>
        /// Eses the numero entero.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        /// 


        public bool EsNumeroEntero(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^\d+$");
        }

        // 2. Números decimales (acepta punto y comas como separador)
        /// <summary>
        /// Eses the numero decimal.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public bool EsNumeroDecimal(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^[0-9]+([.,][0-9]{1,2})?$");
        }


        // 3. Solo letras (Español)
        /// <summary>
        /// Eses the texto valido.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public bool EsTextoValido(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚüÜ\s]+$");
        }
        // 4. Valida formato de Correo Electrónico
        /// <summary>
        /// Eses the correo valido.
        /// </summary>
        /// <param name="correo">The correo.</param>
        /// <returns></returns>
        public bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;

            //  texto + @ + texto + . + extensión 
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }


        // 5. Valida que el usuario no esté vacío y que sea un texto válido
        /// <summary>
        /// Eses the usuario valido.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        public bool EsUsuarioValidoRango(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario)) return false;

            // Solo letras, números, punto, guion bajo
            if (!Regex.IsMatch(usuario, @"^[a-zA-Z0-9._]+$"))
                return false;

            if (usuario.Contains(" "))
                return false;

            if (usuario.Contains("   ")) // tres espacios
                return false;

            // Rango de caracteres 3 a 20
            return usuario.Length >= 3 && usuario.Length <= 20;
        }

        // 6. Valida que la contraseña tenga entre 4 y 25 caracteres
        /// <summary>
        /// Eses the contraseña valida.
        /// </summary>
        /// <param name="contraseña">The contraseña.</param>
        /// <returns></returns>
        public bool EsContraseñaValida(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña)) return false;

            if (contraseña.Contains(" ")) return false; // sin espacios

            if (contraseña.Contains("   ")) // tres espacios
                return false;

            return contraseña.Length >= 6 && contraseña.Length <= 30;
        }

        // 7.Valida longitud mínima y máxima para cualquier texto
        /// <summary>
        /// Eses the longitud valida.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public bool EsLongitudValida(string texto, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            return texto.Length >= min && texto.Length <= max;
        }

        //8. Valida el nombre
        /// <summary>
        /// Eses the nombre persona valido.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public bool EsNombrePersonaValido(string texto)
        {
            if (!EsTextoValido(texto)) return false;
            return EsLongitudValida(texto, 2, 50);
        }

        //9. Valida el nombre del catalogo

        /// <summary>
        /// Eses the nombre cuenta valido.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public bool EsNombreCuentaValido(string texto)
        {
            if (!EsTextoValido(texto)) return false;
            return EsLongitudValida(texto, 3, 60);
        }

        //10.Detalle del catalogo
        /// <summary>
        /// Eses the detalle catalogo valido.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public bool EsDetalleCatalogoValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;

            return Regex.IsMatch(texto, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ.,\s]+$")
                   && EsLongitudValida(texto, 5, 200);
        }
        //11. solo letras
        /// <summary>
        /// Eses the solo letras.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public static bool EsSoloLetras(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            // Acepta letras, espacios y acentos
            return System.Text.RegularExpressions.Regex.IsMatch(
                texto,
                @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"
            );
        }


        // 12.Validar que una fecha no sea mayor que otra
        /// <summary>
        /// Fechas the rango valido.
        /// </summary>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public bool FechaRangoValido(DateTime desde, DateTime hasta)
        {
            return desde.Date <= hasta.Date;
        }

        // 13.Validar que un ComboBox tenga un valor seleccionado
        /// <summary>
        /// Comboes the seleccionado.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <returns></returns>
        public bool ComboSeleccionado(ComboBox combo)
        {
            return combo.SelectedIndex >= 0;
        }

        // 14.Validar que un ListBox tenga al menos un item seleccionado
        /// <summary>
        /// ListBoxes the seleccionado.
        /// </summary>
        /// <param name="lista">The lista.</param>
        /// <returns></returns>
        public bool ListBoxSeleccionado(ListBox lista)
        {
            return lista.SelectedItem != null;
        }

        // 15. Valida que el monto sea decimal válido y mayor que 0
        /// <summary>
        /// Eses the monto positivo.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        public bool EsMontoPositivo(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            if (!EsNumeroDecimal(texto))
                return false;

            // Si llegó aquí, es número decimal, ahora veo si es > 0
            decimal monto = decimal.Parse(texto);
            return monto > 0;
        }

        public bool EsMontoDentroDelRango(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            if (!EsNumeroDecimal(texto))
                return false;

            // Si llegó aquí, es número decimal, ahora veo si es <= 100,000,000
            decimal monto = decimal.Parse(texto);
            return monto <= 100000000;
        }

        // Método para validar que no haya más de 3 espacios en un campo de texto
        public bool ValidarEspacios(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;

            // Contamos la cantidad de espacios en el texto
            int espacios = texto.Split(' ').Length - 1;

            if (espacios > 3)
            {
                MessageBox.Show("El texto no puede contener más de 3 espacios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        
        //espacios
        public bool NoPermitirEspacioInicial(string texto, char key)
        {
            return !(texto.Length == 0 && key == ' ');
        }

        public void MaxlenghtDGV(DataGridView dgv)
        {
            // 1. Configurar Longitud MÁXIMA (Lo que ya tenías)
            if (dgv.Columns.Contains("Saldo") && dgv.Columns["Saldo"] is DataGridViewTextBoxColumn colSaldo)
            {
                colSaldo.MaxInputLength = 8;
            }

            if (dgv.Columns.Contains("Detalle") && dgv.Columns["Detalle"] is DataGridViewTextBoxColumn colDetalle)
            {
                colDetalle.MaxInputLength = 100;
            }

            if (dgv.Columns.Contains("NombreCuenta") && dgv.Columns["NombreCuenta"] is DataGridViewTextBoxColumn colNombreCuenta)
            {
                colNombreCuenta.MaxInputLength = 100;
            }

            // 2. Suscribir evento para Espacios (Tu código anterior)
            dgv.EditingControlShowing -= Dgv_EditingControlShowing;
            dgv.EditingControlShowing += Dgv_EditingControlShowing;

            // 3. NUEVO: Suscribir evento para validar MÍNIMO DE CARACTERES al salir de la celda
            dgv.CellValidating -= Dgv_CellValidating;
            dgv.CellValidating += Dgv_CellValidating;
        }

        // Evento que impide salir de la celda si no cumple los requisitos
        private void Dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            string columnaNombre = dgv.Columns[e.ColumnIndex].Name;

            // Solo validamos Detalle y NombreCuenta
            if (columnaNombre == "Detalle" || columnaNombre == "NombreCuenta")
            {
                // Obtenemos el valor que el usuario intentó ingresar
                string valorNuevo = e.FormattedValue.ToString().Trim();

                // Si la celda está vacía, permitimos salir (o bloqueamos, depende de tu gusto). 
                // Si quieres que sea OBLIGATORIO escribir algo, quita el "!string.IsNullOrEmpty(valorNuevo) &&"
                if (!string.IsNullOrEmpty(valorNuevo) && valorNuevo.Length < 3)
                {
                    dgv.Rows[e.RowIndex].ErrorText = "Debe ingresar al menos 3 caracteres.";
                    MessageBox.Show("El texto es muy corto. Ingrese al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    e.Cancel = true; // ESTO ES LO IMPORTANTE: Impide que el usuario salga de la celda
                }
                else
                {
                    // Limpiar el mensaje de error si ya lo corrigió
                    dgv.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        private void Dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            
            string columnaActual = dgv.Columns[dgv.CurrentCell.ColumnIndex].Name;

            if (columnaActual == "Detalle" || columnaActual == "NombreCuenta")
            {
                if (e.Control is TextBox tb)
                {
                    // Limpiamos eventos previos y asignamos la validación de tecla
                    tb.KeyPress -= Tb_KeyPress_Espacios;
                    tb.KeyPress += Tb_KeyPress_Espacios;
                }
            }
        }

       
        private void Tb_KeyPress_Espacios(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            
            if (e.KeyChar != ' ') return;

            // No permitir espacio al inicio
            if (tb.SelectionStart == 0)
            {
                e.Handled = true; // Bloquea la tecla
                return;
            }

            
            int cantidadEspacios = tb.Text.Count(c => c == ' ');

            
            if (cantidadEspacios >= 3)
            {
                
                if (tb.SelectionLength == 0)
                {
                    e.Handled = true; // Bloquea la tecla
                                      
                }
            }
        }

        public bool EsTextoPuntuacionValido(string texto)
        {
            // Esta expresión permite: Letras (a-z, A-Z), acentos, espacios, puntos (.) y comas (,)
            // ^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\.,]+$
            return System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\.,]+$");
        }

    }
}

