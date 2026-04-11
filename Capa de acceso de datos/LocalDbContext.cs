using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Capa_de_acceso_de_datos
{
    public class LocalDbContext : DbContext
    {
        public DbSet<ProcesosLocales> ColaSincronizacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "respaldo_sistema.db"
            );
            options.UseSqlite($"Data Source={ruta}");
        }
    }
}