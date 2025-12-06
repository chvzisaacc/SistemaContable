using System.Text.RegularExpressions;

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
        public bool EsUsuarioValido(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario)) return false;
            // Validar solo letras y números, y permitir guiones bajos o puntos
            return Regex.IsMatch(usuario, @"^[a-zA-Z0-9._]+$");
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
            return desde <= hasta;
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



    }
}

