using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Data;
using Document = QuestPDF.Fluent.Document;


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
        public byte[] GenerarPdf(DataTable datos, string parroquia, DateTime desde, DateTime hasta)
        {
            // Ruta del logo (opcional si deseas incluirlo)
            string logoPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "logo_arqui.png"
            );

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    // ==========================================================
                    // ENCABEZADO (ajustado al formato)
                    // ==========================================================
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            // IZQUIERDA
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("INFORME DE GASTOS")
                                    .FontSize(20).Bold().FontColor("#003399");

                                col.Item().Text(parroquia)
                                    .FontSize(12).FontColor("#444444");

                                col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")
                                    .FontSize(10).FontColor("#666666");
                            });

                            // DERECHA: LOGO
                            if (File.Exists(logoPath))
                            {
                                row.ConstantItem(110)
                                   .Height(90)
                                   .Image(logoPath, ImageScaling.FitArea);
                            }
                        });

                        // LÍNEA DORADA DEBAJO DEL ENCABEZADO
                        header.Item()
                            .PaddingTop(4)
                            .LineHorizontal(1)
                            .LineColor("#D4AF37");
                    });

                    // ==========================================================
                    // CONTENIDO DEL REPORTE
                    // ==========================================================
                    page.Content().Column(col =>
                    {
                        // ----------------------------
                        // RESUMEN
                        // ----------------------------
                        col.Item().PaddingVertical(10)
                            .Text("RESUMEN DEL INFORME DE GASTOS")
                            .Bold().FontSize(15)
                            .FontColor("#003399");

                        col.Item()
                            .Background("#E8F1FF")
                            .Border(1).BorderColor("#003399")
                            .Padding(15)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn();
                                    c.ConstantColumn(120);
                                });

                                // Header
                                table.Header(h =>
                                {
                                    h.Cell().Text("Descripción").Bold().FontColor("#003399");
                                    h.Cell().Text("Monto").Bold().FontColor("#003399").AlignRight();
                                });

                                // Variables para calcular el total
                                decimal total_gastos = 0;

                                // Filas
                                foreach (DataRow row in datos.Rows)
                                {
                                    table.Cell().Text(row["descripcion"]?.ToString());
                                    decimal monto = Convert.ToDecimal(row["Monto"]);
                                    table.Cell().Text(string.Format("{0:N2}", monto)).AlignRight();
                                    total_gastos += monto;
                                }

                                // Fila de total
                                table.Cell().Text("TOTAL").Bold();
                                table.Cell().Text(string.Format("{0:N2}", total_gastos)).Bold().AlignRight();
                            });

                        col.Item().PaddingVertical(15)
                            .LineHorizontal(1)
                            .LineColor("#D4AF37");

                        // ----------------------------
                        // DETALLE DE GASTOS
                        // ----------------------------
                        col.Item().Text("DETALLE DE GASTOS")
                            .Bold().FontSize(14)
                            .FontColor("#003399");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(70);   // Fecha
                                cols.RelativeColumn(2);    // Cuenta
                                cols.RelativeColumn(3);    // Descripción
                                cols.ConstantColumn(100);  // Monto
                            });

                            // CABECERA
                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Fecha").Bold().FontColor("#FFFFFF");

                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Cuenta").Bold().FontColor("#FFFFFF");

                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Descripción").Bold().FontColor("#FFFFFF");

                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Monto").Bold().FontColor("#FFFFFF")
                                    .AlignRight();
                            });

                            // FILAS
                            int i = 0;
                            foreach (DataRow row in datos.Rows)
                            {
                                string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5";

                                table.Cell().Background(fondo).Padding(4).Text(Convert.ToDateTime(row["fecha_transaccion"]).ToString("dd/MM/yyyy"));
                                table.Cell().Background(fondo).Padding(4).Text(row["NombreCuenta"]?.ToString());
                                table.Cell().Background(fondo).Padding(4).Text(row["descripcion"]?.ToString());
                                table.Cell().Background(fondo).Padding(4).Text(string.Format("{0:N2}", Convert.ToDecimal(row["Monto"]))).AlignRight();

                                i++;
                            }
                        });
                    });

                    // ==========================================================
                    // FOOTER
                    // ==========================================================
                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(9).FontColor("#666666");
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

