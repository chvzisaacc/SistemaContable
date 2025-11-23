using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Capa_de_Presentación.CLASES
{
    public class ClsValidaciones
    {
        // 1. Solo números enteros
        public bool EsNumeroEntero(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return Regex.IsMatch(texto, @"^\d+$");
        }

        // 2. Números decimales (acepta punto como separador)
        public bool EsNumeroDecimal(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            // Acepta "100" o "100.50"
            return Regex.IsMatch(texto, @"^[0-9]+(\.[0-9]{1,2})?$");
        }

        // 3. Solo letras (Español)
        public bool EsTextoValido(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            // Nota: He quitado los espacios que tenías dentro del string @" ^ [...]"
            return Regex.IsMatch(texto, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚüÜ\s]+$");
        }
        // 4. Valida formato de Correo Electrónico
        public bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;

            // Este patrón verifica: texto + @ + texto + . + extensión (2+ letras)
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }


        // 5. Valida que el usuario no esté vacío y que sea un texto válido
        public bool EsUsuarioValido(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario)) return false;
            // Validar solo letras y números, y permitir guiones bajos o puntos
            return Regex.IsMatch(usuario, @"^[a-zA-Z0-9._]+$");
        }

        // 6. Valida que la contraseña tenga entre 4 y 25 caracteres
        public bool EsContraseñaValida(string contraseña)
        {
            if (string.IsNullOrWhiteSpace(contraseña)) return false;
            return contraseña.Length >= 4 && contraseña.Length <= 25;
        }

        // 7.Valida longitud mínima y máxima para cualquier texto
        public bool EsLongitudValida(string texto, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;
            return texto.Length >= min && texto.Length <= max;
        }

        //8. Valida el nombre
        public bool EsNombrePersonaValido(string texto)
        {
            if (!EsTextoValido(texto)) return false;
            return EsLongitudValida(texto, 2, 50);
        }

        //9. Valida el nombre del catalogo

        public bool EsNombreCuentaValido(string texto)
        {
            if (!EsTextoValido(texto)) return false;
            return EsLongitudValida(texto, 3, 60);
        }

        //10.Detalle del catalogo
        public bool EsDetalleCatalogoValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;

            return Regex.IsMatch(texto, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ.,\s]+$")
                   && EsLongitudValida(texto, 5, 200);
        }
        //11. solo letras
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
        public bool FechaRangoValido(DateTime desde, DateTime hasta)
        {
            return desde <= hasta;
        }

        // 13.Validar que un ComboBox tenga un valor seleccionado
        public bool ComboSeleccionado(ComboBox combo)
        {
            return combo.SelectedIndex >= 0;
        }

        // 14.Validar que un ListBox tenga al menos un item seleccionado
        public bool ListBoxSeleccionado(ListBox lista)
        {
            return lista.SelectedItem != null;
        }



    }
}

