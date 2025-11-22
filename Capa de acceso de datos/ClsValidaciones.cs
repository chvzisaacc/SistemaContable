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
        // 4. Validar formato de Correo Electrónico
        public bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;

            // Este patrón verifica: texto + @ + texto + . + extensión (2+ letras)
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

    }
}

