using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class LocalDbContext : DbContext
    {
        // Esta propiedad representa la tabla en SQLite
        public DbSet<ProcesosLocales> ColaSincronizacion { get; set; }

        // Aquí configuramos en qué parte de la PC se creará el archivo
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // "Data Source=respaldo_sistema.db" creará el archivo en la misma carpeta del .exe
            options.UseSqlite("Data Source=respaldo_sistema.db");
        }
    }
}
