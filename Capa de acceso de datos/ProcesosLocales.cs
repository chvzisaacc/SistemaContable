using Microsoft.EntityFrameworkCore;
using System;

namespace Capa_de_acceso_de_datos
{
    public class ProcesosLocales
    {
        public int Id { get; set; }
        public string TipoObjeto { get; set; }
        public string DatosJson { get; set; }
        public bool Sincronizado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}