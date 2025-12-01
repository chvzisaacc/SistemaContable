using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;
using System.Text;
using System.Threading.Tasks;


namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class GastosService
    {
        /// <summary>
        /// The repo
        /// </summary>
        private readonly ClsReportes _repo = new ClsReportes();
        /// <summary>
        /// The carpeta reportes
        /// </summary>
        private readonly string _carpetaReportes;

        /// <summary>
        /// Initializes a new instance of the <see cref="GastosService"/> class.
        /// </summary>
        public GastosService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesGastos");

            Directory.CreateDirectory(_carpetaReportes);
        }

        /// <summary>
        /// Generars the informe gastos.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="nombre_parroquia">The nombre parroquia.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        public string GenerarInformeGastos(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id)
        {
            // 1. Traer datos
            DataTable datos = _repo.ObtenerGastosPorParroquia(parroquia_id, desde, hasta);

            // 2. Crear PDF en memoria
            byte[] pdfBytes = GenerarPdf(datos, nombre_parroquia, desde, hasta);

            // 3. Guardar archivo
            string nombre_archivo = $"Gastos_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);

            File.WriteAllBytes(ruta_completa, pdfBytes);

            // 4. Aquí podrías registrar en reportes_generados si luego lo necesitas

            return ruta_completa;
        }

        /// <summary>
        /// Generars the PDF.
        /// </summary>
        /// <param name="datos">The datos.</param>
        /// <param name="parroquia">The parroquia.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        private byte[] GenerarPdf(DataTable datos, string parroquia, DateTime desde, DateTime hasta)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("INFORME DE GASTOS").FontSize(16).Bold();
                        col.Item().Text(parroquia);
                        col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}");
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(70);  // Fecha
                            columns.RelativeColumn(2);   // Cuenta
                            columns.RelativeColumn(3);   // Descripción
                            columns.ConstantColumn(80);  // Monto
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Fecha").SemiBold();
                            header.Cell().Text("Cuenta").SemiBold();
                            header.Cell().Text("Descripción").SemiBold();
                            header.Cell().Text("Monto").SemiBold();
                        });

                        foreach (DataRow row in datos.Rows)
                        {
                            var fecha = (DateTime)row["fecha_transaccion"];
                            string cuenta = row["NombreCuenta"].ToString();
                            string desc = row["descripcion"].ToString();
                            decimal monto = Convert.ToDecimal(row["Monto"]);   // 👈 viene del SP

                            table.Cell().Text(fecha.ToString("dd/MM/yyyy"));
                            table.Cell().Text(cuenta);
                            table.Cell().Text(desc);
                            table.Cell().Text(string.Format("{0:N2}", monto));
                        }
                    });

                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            return document.GeneratePdf();
        }

        /// <summary>
        /// Obteners the nombre parroquia.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <returns></returns>
        public string ObtenerNombreParroquia(int parroquia_id)
        {
            return _repo.ObtenerNombreParroquia(parroquia_id);
        }
        /// <summary>
        /// Obteners the tipos reporte.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerTiposReporte()
        {
            return _repo.ObtenerTiposReporte();

        }
        /// <summary>
        /// Obteners the parroquias.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerParroquias()
        {
            return _repo.ObtenerParroquias();
        }


    }

}

