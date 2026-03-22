using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Data;
using Document = QuestPDF.Fluent.Document;

namespace Capa_de_procesamiento_de_datos
{
    public class EstadoResultadosService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public EstadoResultadosService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");
            _carpetaReportes = Path.Combine(baseReportsFolder, "BalanceGeneral");
            Directory.CreateDirectory(_carpetaReportes);
        }

        public string ObtenerNombreParroquia(int parroquia_id)
        {
            return _repo.ObtenerNombreParroquia(parroquia_id);
        }

        public DataTable ObtenerTiposReporte()
        {
            return _repo.ObtenerTiposReporte();
        }

        public DataTable ObtenerParroquias()
        {
            return _repo.ObtenerParroquias();
        }

        public string GenerarInformeEstadoResultados(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id)
        {
            DataSet datos = _repo.ObtenerEstadoResultados(parroquia_id, desde, hasta);
            DataTable totales = datos.Tables[0];
            DataTable detalle = datos.Tables[1];

            byte[] pdfBytes = GenerarPdf(totales, detalle, nombre_parroquia, desde, hasta);

            string nombreArchivo = $"EstadoResultados_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);
            return rutaCompleta;
        }

        public byte[] GenerarPdf(
            DataTable totales,
            DataTable detalle,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta)
        {
            var formatoHnd = new System.Globalization.CultureInfo("en-US");
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
                    // LOGO con posición absoluta arriba a la derecha
                    // ==========================================================
                    if (File.Exists(logoPath))
                    {
                        page.Foreground()
                            .AlignTop()
                            .AlignRight()
                            .Width(70)
                            .Height(55)
                            .PaddingTop(5)
                            .PaddingRight(30)
                            .Image(logoPath, ImageScaling.FitArea);
                    }

                    // ==========================================================
                    // ENCABEZADO 
                    // ==========================================================
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            // IZQUIERDA — solo texto, sin logo
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("ESTADO DE RESULTADOS")
                                    .FontSize(20).Bold().FontColor("#003399");

                                col.Item().Text(nombre_parroquia)
                                    .FontSize(12).FontColor("#444444");

                                col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")
                                    .FontSize(10).FontColor("#666666");
                            });
                        });

                        // LÍNEA DORADA DEBAJO DEL ENCABEZADO
                        header.Item()
                            .PaddingTop(4)
                            .LineHorizontal(1)
                            .LineColor("#D4AF37");
                    });

                    // ==========================================================
                    // CONTENIDO
                    // ==========================================================
                    page.Content().Column(col =>
                    {
                        DataRow t = totales.Rows[0];

                        // RESUMEN
                        col.Item().PaddingVertical(10)
                            .Text("RESUMEN")
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

                                table.Header(h =>
                                {
                                    h.Cell().Text("Descripción").Bold().FontColor("#003399");
                                    h.Cell().Text("Monto").Bold().FontColor("#003399").AlignRight();
                                });

                                table.Cell().Text("Total Ingresos");
                                table.Cell().Text($"L.{Convert.ToDecimal(t["totalIngresos"]).ToString("N2", formatoHnd)}").AlignRight();

                                table.Cell().Text("Total Gastos");
                                table.Cell().Text($"L.{Convert.ToDecimal(t["totalGastos"]).ToString("N2", formatoHnd)}").AlignRight();

                                table.Cell().Text("Resultado Final").Bold().FontColor("#003399");
                                table.Cell().Text($"L.{Convert.ToDecimal(t["resultadoFinal"]).ToString("N2", formatoHnd)}").Bold().FontColor("#003399").AlignRight();
                            });

                        col.Item().PaddingVertical(15)
                            .LineHorizontal(1)
                            .LineColor("#D4AF37");

                        // DETALLE DE TRANSACCIONES
                        col.Item().Text("DETALLE DE TRANSACCIONES")
                            .Bold().FontSize(14)
                            .FontColor("#003399");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(70);
                                cols.ConstantColumn(80);
                                cols.RelativeColumn();
                                cols.ConstantColumn(100);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5).Text("Tipo").Bold().FontColor("#FFFFFF");
                                h.Cell().Background("#D4AF37").Padding(5).Text("Fecha").Bold().FontColor("#FFFFFF");
                                h.Cell().Background("#D4AF37").Padding(5).Text("Cuenta / Detalle").Bold().FontColor("#FFFFFF");
                                h.Cell().Background("#D4AF37").Padding(5).Text("Monto").Bold().FontColor("#FFFFFF").AlignRight();
                            });

                            int i = 0;
                            foreach (DataRow row in detalle.Rows)
                            {
                                string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5";
                                decimal montoFila = Convert.ToDecimal(row["monto"]);
                                table.Cell().Background(fondo).Padding(4).Text(row["tipo"]);
                                table.Cell().Background(fondo).Padding(4).Text(Convert.ToDateTime(row["fecha"]).ToString("dd/MM/yyyy"));
                                table.Cell().Background(fondo).Padding(4).Text($"{row["Cuenta"]}\n{row["Detalle"]}");
                                table.Cell().Background(fondo).Padding(4).Text($"L.{montoFila.ToString("N2", formatoHnd)}").AlignRight();
                                i++;
                            }
                        });
                    });

                    // ==========================================================
                    // PIE DE PÁGINA
                    // ==========================================================
                    page.Footer()
                        .Height(30)
                        .AlignCenter()
                        .Column(col =>
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
    }
}