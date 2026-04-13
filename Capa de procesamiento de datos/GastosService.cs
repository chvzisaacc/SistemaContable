using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Data;
using Document = QuestPDF.Fluent.Document;

namespace Capa_de_procesamiento_de_datos
{
    // Genera el PDF del informe de gastos y expone helpers de catálogo para la UI
    public class GastosService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        // Carpeta destino dentro de Mis Documentos; se crea si no existe
        public GastosService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");
            _carpetaReportes = Path.Combine(baseReportsFolder, "Gastos");
            Directory.CreateDirectory(_carpetaReportes);
        }

        /// <summary>Genera el PDF del informe de gastos, lo guarda en disco y retorna la ruta.</summary>
        public string GenerarInformeGastos(
            int parroquia_id, string nombre_parroquia,
            DateTime desde, DateTime hasta, int usuario_id)
        {
            DataTable datos = _repo.ObtenerGastosPorParroquia(parroquia_id, desde, hasta);
            byte[] pdfBytes = GenerarPdf(datos, nombre_parroquia, desde, hasta);

            string nombre_archivo = $"Gastos_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);

            File.WriteAllBytes(ruta_completa, pdfBytes);
            return ruta_completa;
        }

        /// <summary>Construye el documento PDF con QuestPDF y retorna sus bytes.</summary>
        public byte[] GenerarPdf(DataTable datos, string parroquia, DateTime desde, DateTime hasta)
        {
            var formatoHnd = new System.Globalization.CultureInfo("en-US");
            string logoPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo_arqui.png"
            );

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    // Foreground superpone el logo sin desplazar el contenido del encabezado
                    if (File.Exists(logoPath))
                    {
                        page.Foreground()
                            .AlignTop().AlignRight()
                            .Width(70).Height(55)
                            .PaddingTop(5).PaddingRight(30)
                            .Image(logoPath, ImageScaling.FitArea);
                    }

                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("INFORME DE GASTOS")
                                    .FontSize(20).Bold().FontColor("#003399");
                                col.Item().Text(parroquia)
                                    .FontSize(12).FontColor("#444444");
                                col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")
                                    .FontSize(10).FontColor("#666666");
                            });
                        });

                        header.Item().PaddingTop(4).LineHorizontal(1).LineColor("#D4AF37");
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingVertical(10)
                            .Text("RESUMEN DEL INFORME DE GASTOS").Bold().FontSize(15).FontColor("#003399");

                        col.Item()
                            .Background("#E8F1FF").Border(1).BorderColor("#003399").Padding(15)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn();
                                    c.ConstantColumn(120);
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Text("Descripción").Bold().FontColor("#003399");
                                    h.Cell().Text("Monto").Bold().FontColor("#003399").AlignRight();
                                });

                                decimal total_gastos = 0;

                                // El total se acumula en el mismo recorrido para evitar un segundo loop
                                foreach (DataRow row in datos.Rows)
                                {
                                    table.Cell().Text(row["descripcion"]?.ToString());
                                    decimal monto = Convert.ToDecimal(row["Monto"]);
                                    table.Cell().Text($"L.{monto.ToString("N2", formatoHnd)}").AlignRight();
                                    total_gastos += monto;
                                }

                                table.Cell().Text("TOTAL").Bold();
                                table.Cell().Text($"L.{total_gastos.ToString("N2", formatoHnd)}").Bold().AlignRight();
                            });

                        col.Item().PaddingVertical(15).LineHorizontal(1).LineColor("#D4AF37");

                        col.Item().Text("DETALLE DE GASTOS").Bold().FontSize(14).FontColor("#003399");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(70);
                                cols.RelativeColumn(2);
                                cols.RelativeColumn(3);
                                cols.ConstantColumn(100);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5).Text("Fecha").Bold().FontColor("#FFFFFF");
                                h.Cell().Background("#D4AF37").Padding(5).Text("Cuenta").Bold().FontColor("#FFFFFF");
                                h.Cell().Background("#D4AF37").Padding(5).Text("Descripción").Bold().FontColor("#FFFFFF");
                                h.Cell().Background("#D4AF37").Padding(5).Text("Monto").Bold().FontColor("#FFFFFF").AlignRight();
                            });

                            int i = 0;
                            foreach (DataRow row in datos.Rows)
                            {
                                // Filas alternas para mejorar legibilidad
                                string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5";
                                decimal montoFila = Convert.ToDecimal(row["Monto"]);

                                table.Cell().Background(fondo).Padding(4).Text(Convert.ToDateTime(row["fecha_transaccion"]).ToString("dd/MM/yyyy"));
                                table.Cell().Background(fondo).Padding(4).Text(row["NombreCuenta"]?.ToString());
                                table.Cell().Background(fondo).Padding(4).Text(row["descripcion"]?.ToString());
                                table.Cell().Background(fondo).Padding(4).Text($"L.{montoFila.ToString("N2", formatoHnd)}").AlignRight();
                                i++;
                            }
                        });
                    });

                    page.Footer().Height(30).AlignCenter().Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor("#D4AF37");
                        col.Item().Text(text =>
                        {
                            text.Span("Generado el ").FontSize(9).FontColor("#666666");
                            text.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor("#666666");
                            text.Span("  |  Página ").FontSize(9).FontColor("#666666");
                            text.CurrentPageNumber().FontSize(9).FontColor("#666666");
                            text.Span(" de ").FontSize(9).FontColor("#666666");
                            text.TotalPages().FontSize(9).FontColor("#666666");
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        // Pass-through al repositorio para que la UI no dependa de ClsReportes directamente
        public string ObtenerNombreParroquia(int parroquia_id) => _repo.ObtenerNombreParroquia(parroquia_id);
        public DataTable ObtenerTiposReporte() => _repo.ObtenerTiposReporte();
        public DataTable ObtenerParroquias() => _repo.ObtenerParroquias();
    }
}