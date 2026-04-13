using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

namespace Capa_de_Presentación.CLASES
{
    /// <summary>
    /// Clase de utilidad que proporciona métodos de validación para campos de entrada de usuario.
    /// Valida: números, textos, correos, contraseñas, rangos de fechas, montos y controles de UI.
    /// Incluye validación de DataGridView con restricciones de longitud y espacios.
    /// </summary>
    public class ClsValidaciones
    {
        /// <summary>
        /// Valida que el texto contenga solo números enteros positivos.
        /// Parámetro texto: cadena a validar.
        /// Retorna true si el texto es un número entero válido (sin decimales), false si está vacío o contiene caracteres no numéricos.
        /// Patrón: solo dígitos del 0-9.
        /// </summary>
        public bool EsNumeroEntero(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^\d+$");
        }

        /// <summary>
        /// Valida que el texto sea un número decimal válido.
        /// Parámetro texto: cadena a validar.
        /// Retorna true si es número decimal (acepta punto o coma como separador, 1-2 decimales), false si está vacío o formato inválido.
        /// Patrón: dígitos + separador (. o ,) + 1-2 dígitos decimales opcionales.
        /// </summary>
        public bool EsNumeroDecimal(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^[0-9]+([.,][0-9]{1,2})?$");
        }

        /// <summary>
        /// Valida que el texto contenga solo letras con soporte para caracteres españoles.
        /// Parámetro texto: cadena a validar.
        /// Retorna true si contiene solo letras (a-z, A-Z), acentos y espacios, false si está vacío o contiene números/caracteres especiales.
        /// Patrón: letras, ñ, acentos y espacios permitidos.
        /// </summary>
        public bool EsTextoValido(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚüÜ\s]+$");
        }

        /// <summary>
        /// Valida que el correo tenga formato correcto de dirección de email.
        /// Parámetro correo: dirección de correo a validar.
        /// Retorna true si el formato es válido (contiene @, punto y partes válidas), false si está vacío o formato inválido.
        /// Patrón: texto + @ + texto + . + extensión (sin espacios ni caracteres especiales inválidos).
        /// </summary>
        public bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Valida que el nombre de usuario cumpla requisitos de seguridad y formato.
        /// Parámetro usuario: nombre de usuario a validar.
        /// Retorna true si cumple: 3-20 caracteres, solo letras/números/punto/guion bajo, sin espacios.
        /// False si está vacío, fuera de rango o contiene caracteres inválidos.
        /// </summary>
        public bool EsUsuarioValidoRango(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario)) return false;

            if (!Regex.IsMatch(usuario, @"^[a-zA-Z0-9._]+$"))
                return false;

            if (usuario.Contains(" "))
                return false;

            if (usuario.Contains("   "))
                return false;

            return usuario.Length >= 3 && usuario.Length <= 20;
        }

        /// <summary>
        /// Valida que la contraseña cumpla requisitos de seguridad estrictos.
        /// Parámetro contraseña: contraseña a validar.
        /// Retorna true si: mínimo 8 caracteres, contiene mayúscula, minúscula, número y carácter especial, no es contraseña débil común.
        /// False si no cumple estos requisitos o está vacía.
        /// Bloquea contraseñas débiles comunes: "password", "12345678", "qwerty", etc.
        /// </summary>
        public bool EsContraseñaValida(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña)) return false;
            if (contraseña.Length < 8) return false;
            if (!contraseña.Any(char.IsUpper)) return false;
            if (!contraseña.Any(char.IsLower)) return false;
            if (!contraseña.Any(char.IsDigit)) return false;
            if (!contraseña.Any(c => !char.IsLetterOrDigit(c))) return false;

            string[] contraseñasDebiles = { "12345678", "123456789", "00000000", "password", "qwerty", "aaaaaa" };
            if (contraseñasDebiles.Contains(contraseña)) return false;

            return true;
        }

        /// <summary>
        /// Valida que la longitud del texto esté dentro de un rango permitido.
        /// Parámetros: texto (cadena a validar), min (longitud mínima), max (longitud máxima).
        /// Retorna true si longitud >= min y longitud <= max, false si está fuera de rango o vacío.
        /// Útil para validar campos con límites de caracteres específicos.
        /// </summary>
        public bool EsLongitudValida(string texto, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            return texto.Length >= min && texto.Length <= max;
        }

        /// <summary>
        /// Valida que el nombre de una persona sea válido (solo letras con acentos españoles, 2-50 caracteres).
        /// Parámetro texto: nombre a validar.
        /// Retorna true si es texto válido y tiene entre 2-50 caracteres, false si no cumple.
        /// Reutiliza EsTextoValido y EsLongitudValida para validar composición.
        /// </summary>
        public bool EsNombrePersonaValido(string texto)
        {
            if (!EsTextoValido(texto)) return false;
            return EsLongitudValida(texto, 2, 50);
        }

        /// <summary>
        /// Valida que el nombre de una cuenta contable sea válido (solo letras con acentos, 3-60 caracteres).
        /// Parámetro texto: nombre de cuenta a validar.
        /// Retorna true si es texto válido y tiene entre 3-60 caracteres, false si no cumple.
        /// Se utiliza en catálogo de cuentas para nombres de cuentas contables.
        /// </summary>
        public bool EsNombreCuentaValido(string texto)
        {
            if (!EsTextoValido(texto)) return false;
            return EsLongitudValida(texto, 3, 60);
        }

        /// <summary>
        /// Valida que el detalle/descripción de una cuenta sea válido.
        /// Parámetro texto: detalle a validar.
        /// Retorna true si contiene letras, números, acentos españoles, puntos, comas y espacios (5-200 caracteres).
        /// False si está vacío o fuera de rango permitido.
        /// Se utiliza para descripción de cuentas en el catálogo.
        /// </summary>
        public bool EsDetalleCatalogoValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;

            return Regex.IsMatch(texto, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ.,\s]+$")
                   && EsLongitudValida(texto, 5, 200);
        }

        /// <summary>
        /// Valida que el texto contenga solo letras con soporte para caracteres españoles (incluyendo acentos).
        /// Parámetro texto: cadena a validar.
        /// Retorna true si contiene solo letras, acentos y espacios, false si contiene números o caracteres especiales.
        /// Patrón: a-z, A-Z, ñ, acentos y espacios permitidos.
        /// </summary>
        public static bool EsSoloLetras(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            return System.Text.RegularExpressions.Regex.IsMatch(
                texto,
                @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"
            );
        }

        /// <summary>
        /// Valida que el rango de fechas sea válido (fecha inicio no sea mayor que fecha fin).
        /// Parámetros: desde (fecha inicial), hasta (fecha final).
        /// Retorna true si desde.Date <= hasta.Date, false si rango es inválido.
        /// Se utiliza en reportes y filtros de fecha para evitar rangos invertidos.
        /// </summary>
        public bool FechaRangoValido(DateTime desde, DateTime hasta)
        {
            return desde.Date <= hasta.Date;
        }

        /// <summary>
        /// Valida que un ComboBox tenga un elemento seleccionado.
        /// Parámetro combo: control ComboBox a validar.
        /// Retorna true si SelectedIndex >= 0 (hay selección), false si no hay selección.
        /// Se utiliza para validar que usuario ha seleccionado una opción obligatoria.
        /// </summary>
        public bool ComboSeleccionado(ComboBox combo)
        {
            return combo.SelectedIndex >= 0;
        }

        /// <summary>
        /// Valida que un ListBox tenga al menos un elemento seleccionado.
        /// Parámetro lista: control ListBox a validar.
        /// Retorna true si SelectedItem no es null, false si no hay selección.
        /// Se utiliza para validar que usuario ha seleccionado elementos de lista.
        /// </summary>
        public bool ListBoxSeleccionado(ListBox lista)
        {
            return lista.SelectedItem != null;
        }

        /// <summary>
        /// Valida que el monto sea un número decimal válido y sea mayor que cero.
        /// Parámetro texto: monto a validar (cadena con formato decimal).
        /// Retorna true si es decimal válido y monto > 0, false si está vacío, formato inválido o es cero/negativo.
        /// Se utiliza para validar montos en transacciones financieras.
        /// </summary>
        public bool EsMontoPositivo(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            if (!EsNumeroDecimal(texto))
                return false;

            decimal monto = decimal.Parse(texto);
            return monto > 0;
        }

        /// <summary>
        /// Valida que el monto sea un número decimal válido y no exceda 100,000,000.
        /// Parámetro texto: monto a validar (cadena con formato decimal).
        /// Retorna true si es decimal válido y monto <= 100,000,000, false si está fuera de rango.
        /// Se utiliza para limitar montos máximos en transacciones (control de límites operacionales).
        /// </summary>
        public bool EsMontoDentroDelRango(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            if (!EsNumeroDecimal(texto))
                return false;

            decimal monto = decimal.Parse(texto);
            return monto <= 100000000;
        }

        /// <summary>
        /// Valida que el texto no contenga más de 3 espacios en blanco.
        /// Parámetro texto: cadena a validar.
        /// Retorna true si espacios <= 3, false si contiene más de 3 espacios o está vacío.
        /// Muestra mensaje de error si no cumple validación.
        /// Se utiliza para limitar espacios en nombres y descripciones.
        /// </summary>
        public bool ValidarEspacios(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;

            int espacios = texto.Split(' ').Length - 1;

            if (espacios > 3)
            {
                MessageBox.Show("El texto no puede contener más de 3 espacios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que no se permita un espacio en blanco al inicio de una entrada.
        /// Parámetros: texto (contenido actual), key (tecla presionada).
        /// Retorna true si se permite la tecla, false si es espacio al inicio y debe bloquearse.
        /// Se utiliza en eventos KeyPress para validación en tiempo real.
        /// </summary>
        public bool NoPermitirEspacioInicial(string texto, char key)
        {
            return !(texto.Length == 0 && key == ' ');
        }

        /// <summary>
        /// Configura validaciones automáticas en un DataGridView para columnas específicas.
        /// Parámetro dgv: control DataGridView a configurar.
        /// 
        /// Configuraciones realizadas:
        /// - Saldo: máximo 8 caracteres
        /// - Detalle: máximo 100 caracteres
        /// - NombreCuenta: máximo 100 caracteres
        /// 
        /// Eventos suscritos:
        /// - EditingControlShowing: valida espacios al editar
        /// - CellValidating: valida longitud mínima (3 caracteres) al salir de celda
        /// 
        /// Se utiliza en formularios con grillas editables para garantizar datos válidos.
        /// </summary>
        public void MaxlenghtDGV(DataGridView dgv)
        {
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

            dgv.EditingControlShowing -= Dgv_EditingControlShowing;
            dgv.EditingControlShowing += Dgv_EditingControlShowing;

            dgv.CellValidating -= Dgv_CellValidating;
            dgv.CellValidating += Dgv_CellValidating;
        }

        /// <summary>
        /// Evento handler que valida que el valor ingresado en celda cumpla longitud mínima.
        /// Impide salir de la celda si contiene texto pero menos de 3 caracteres.
        /// Muestra mensaje de error y anula el evento si validación falla.
        /// Se dispara al intentar salir de celdas de Detalle o NombreCuenta.
        /// </summary>
        private void Dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            string columnaNombre = dgv.Columns[e.ColumnIndex].Name;

            if (columnaNombre == "Detalle" || columnaNombre == "NombreCuenta")
            {
                string valorNuevo = e.FormattedValue.ToString().Trim();

                if (!string.IsNullOrEmpty(valorNuevo) && valorNuevo.Length < 3)
                {
                    dgv.Rows[e.RowIndex].ErrorText = "Debe ingresar al menos 3 caracteres.";
                    MessageBox.Show("El texto es muy corto. Ingrese al menos 3 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    e.Cancel = true;
                }
                else
                {
                    dgv.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        /// <summary>
        /// Evento handler que se dispara cuando comienza la edición de una celda en DataGridView.
        /// Suscribe validación de espacios al TextBox de edición si es columna Detalle o NombreCuenta.
        /// Permite control en tiempo real de entrada mientras usuario escribe.
        /// </summary>
        private void Dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            string columnaActual = dgv.Columns[dgv.CurrentCell.ColumnIndex].Name;

            if (columnaActual == "Detalle" || columnaActual == "NombreCuenta")
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= Tb_KeyPress_Espacios;
                    tb.KeyPress += Tb_KeyPress_Espacios;
                }
            }
        }

        /// <summary>
        /// Evento handler que valida el presionamiento de tecla espaciadora en TextBox de DataGridView.
        /// Bloquea espacios al inicio del texto.
        /// Bloquea espacios si ya hay 3 o más espacios en el texto (máximo 3 espacios permitidos).
        /// Se utiliza para mantener formato limpio de datos ingresados.
        /// </summary>
        private void Tb_KeyPress_Espacios(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (e.KeyChar != ' ') return;

            if (tb.SelectionStart == 0)
            {
                e.Handled = true;
                return;
            }

            int cantidadEspacios = tb.Text.Count(c => c == ' ');

            if (cantidadEspacios >= 3)
            {
                if (tb.SelectionLength == 0)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Valida que el texto contenga solo letras con acentos españoles, espacios, puntos y comas.
        /// Parámetro texto: cadena a validar.
        /// Retorna true si cumple formato, false si contiene números o caracteres especiales no permitidos.
        /// Patrón: letras, ñ, acentos, espacios, puntos y comas permitidos.
        /// Se utiliza para descripciones que pueden incluir puntuación.
        /// </summary>
        public bool EsTextoPuntuacionValido(string texto)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s\.,]+$");
        }
    }
}