using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Capa_de_acceso_de_datos
{
    /// <summary>
    /// Contexto de Entity Framework Core para base de datos SQLite local.
    /// Gestiona la cola de sincronización de procesos pendientes para replicar en servidor remoto.
    /// Almacena localmente operaciones que fallan o se ejecutan offline para sincronizar posteriormente.
    /// </summary>
    public class LocalDbContext : DbContext
    {
        /// <summary>
        /// Colección de procesos locales pendientes de sincronización.
        /// Almacena: tipo de objeto, datos en JSON, estado de sincronización, fecha de registro.
        /// Se utiliza como cola para replicar cambios al servidor remoto cuando conexión se restablece.
        /// </summary>
        public DbSet<ProcesosLocales> ColaSincronizacion { get; set; }

        /// <summary>
        /// Configura la conexión a base de datos SQLite local.
        /// Crea automáticamente archivo "respaldo_sistema.db" en directorio de aplicación.
        /// La ruta es: AppDomain.CurrentDomain.BaseDirectory + "respaldo_sistema.db"
        /// 
        /// Función:
        /// - Almacenamiento offline de transacciones y cambios
        /// - Cola de sincronización para operaciones pendientes
        /// - Respaldo automático de datos locales durante desconexiones
        /// 
        /// Se ejecuta una única vez al instanciar el contexto.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string ruta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "respaldo_sistema.db"
            );
            options.UseSqlite($"Data Source={ruta}");
        }
        public DbSet<HashAplicado> HashesAplicados { get; set; }
        public class HashAplicado
        {
            public int Id { get; set; }
            public string Hash { get; set; }
            public DateTime Fecha { get; set; } = DateTime.Now;
        }
    }
}