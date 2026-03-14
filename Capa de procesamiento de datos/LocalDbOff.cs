using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capa_de_procesamiento_de_datos
{
    public class LocalDbOff
    {
        // Guardar un nuevo proceso
        public void RegistrarProcesoLocal(string tipo, string json)
        {
            using (var db = new LocalDbContext())
            {
                var nuevo = new ProcesosLocales
                {
                    TipoObjeto = tipo,
                    DatosJson = json,
                    Sincronizado = false,
                    Fecha = DateTime.Now
                };

                db.ColaSincronizacion.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Obtener solo lo que no se ha sincronizado
        public List<ProcesosLocales> ObtenerPendientes()
        {
            using (var db = new LocalDbContext())
            {
                return db.ColaSincronizacion
                         .Where(p => !p.Sincronizado)
                         .ToList();
            }
        }

        //Marcar como sincronizado
        public void MarcarComoSincronizado(int id)
        {
            using (var db = new LocalDbContext())
            {
                var registro = db.ColaSincronizacion.Find(id);
                if (registro != null)
                {
                    registro.Sincronizado = true;
                    db.SaveChanges();
                }
            }
        }

        public async Task ProcesarColaSincronizacion()
        {
            var pendientes = ObtenerPendientes(); // Ya tienes este método
            if (pendientes.Count == 0) return;

            using (var client = new HttpClient())
            {
                foreach (var registro in pendientes)
                {
                    try
                    {
                        // Preparar el contenido
                        var contenido = new StringContent(registro.DatosJson, Encoding.UTF8, "application/json");

                        // Enviar a XAMPP
                        var url = $"http://localhost/api_sincronizar/receptor.php?tipo={registro.TipoObjeto}";
                        var respuesta = await client.PostAsync(url, contenido);

                        // 3. Si el servidor respondió OK, marcar como sincronizado
                        if (respuesta.IsSuccessStatusCode)
                        {
                            MarcarComoSincronizado(registro.Id);
                            Console.WriteLine($"ID {registro.Id} sincronizado con éxito.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Si no hay conexión, salimos del bucle para reintentar después
                        Console.WriteLine("Error de conexión: " + ex.Message);
                        break;
                    }
                }
            }
        }
    }
}