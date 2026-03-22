using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Data;
using Document = QuestPDF.Fluent.Document;

namespace Capa_de_procesamiento_de_datos
{
    public class LibroMayorService
    {
        private readonly LibroMayor _repo = new LibroMayor();
        private readonly string _carpetaReportes;

        public LibroMayorService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");
            _carpetaReportes = Path.Combine(baseReportsFolder, "LibroMayor");
            Directory.CreateDirectory(_carpetaReportes);
        }

        public string GenerarInformeLibroMayor(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta)
        {
            DataTable datos = _repo.ObtenerLibroMayor(parroquia_id, desde, hasta);
            byte[] pdfBytes = GenerarPdf(datos, nombre_parroquia, desde, hasta);

            string nombreArchivo = $"LibroMayor_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);
            return rutaCompleta;
        }

        public byte[] GenerarPdf(
            DataTable datos,
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

                    // ================= HEADER =================
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            // Solo texto, logo quitado del row
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("LIBRO MAYOR")
                                    .FontSize(20).Bold().FontColor("#003399");

                                col.Item().Text(nombre_parroquia)
                                    .FontSize(12).FontColor("#444444");

                                col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")
                                    .FontSize(10).FontColor("#666666");
                            });
                        });

                        header.Item()
                            .PaddingTop(4)
                            .LineHorizontal(1)
                            .LineColor("#D4AF37");
                    });

                    // ================= CONTENT =================
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(90);
                            cols.ConstantColumn(110);
                            cols.RelativeColumn(2);
                            cols.ConstantColumn(85);
                            cols.ConstantColumn(85);
                            cols.ConstantColumn(95);
                        });

                        table.Header(h =>
                        {
                            string fondo = "#D4AF37";
                            h.Cell().Background(fondo).Padding(5).Text("Fecha").Bold().FontColor("#FFFFFF").FontSize(10);
                            h.Cell().Background(fondo).Padding(5).Text("Código").Bold().FontColor("#FFFFFF").FontSize(10);
                            h.Cell().Background(fondo).Padding(5).Text("Cuenta").Bold().FontColor("#FFFFFF").FontSize(10);
                            h.Cell().Background(fondo).Padding(5).Text("Debe").Bold().FontColor("#FFFFFF").AlignRight().FontSize(10);
                            h.Cell().Background(fondo).Padding(5).Text("Haber").Bold().FontColor("#FFFFFF").AlignRight().FontSize(10);
                            h.Cell().Background(fondo).Padding(5).Text("Saldo").Bold().FontColor("#FFFFFF").AlignRight().FontSize(10);
                        });

                        int i = 0;
                        decimal totalDebe = 0;
                        decimal totalHaber = 0;

                        foreach (DataRow row in datos.Rows)
                        {
                            decimal debe = row["Debe"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Debe"]);
                            decimal haber = row["Haber"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Haber"]);

                            totalDebe += debe;
                            totalHaber += haber;

                            string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5";

                            table.Cell().Background(fondo).Padding(4)
                                .Text(row["Fecha"] == DBNull.Value ? "" :
                                    Convert.ToDateTime(row["Fecha"]).ToString("dd/MM/yyyy"))
                                .FontSize(9);

                            table.Cell().Background(fondo).Padding(4)
                                .Text(row["codigo"]?.ToString() ?? "")
                                .FontSize(9);

                            table.Cell().Background(fondo).Padding(4)
                                .Text(row["cuenta"].ToString())
                                .FontSize(9);

                            table.Cell().Background(fondo).Padding(4)
                                .Text($"L.{debe.ToString("N2", formatoHnd)}")
                                .AlignRight().FontSize(9);

                            table.Cell().Background(fondo).Padding(4)
                                .Text($"L.{haber.ToString("N2", formatoHnd)}")
                                .AlignRight().FontSize(9);

                            table.Cell().Background(fondo).Padding(4)
                                .Text(row["Saldo"] == DBNull.Value ? "" :
                                    $"L.{Convert.ToDecimal(row["Saldo"]).ToString("N2", formatoHnd)}")
                                .AlignRight().FontSize(9);

                            i++;
                        }

                        decimal totalGeneral = Math.Abs(totalDebe - totalHaber);
                        string fondoTotal = "#E8E8E8";

                        table.Cell().Background(fondoTotal).Padding(5).Text("");
                        table.Cell().Background(fondoTotal).Padding(5).Text("");
                        table.Cell().Background(fondoTotal).Padding(5).Text("TOTAL GENERAL").Bold().FontSize(10);
                        table.Cell().Background(fondoTotal).Padding(5).Text($"L.{totalDebe.ToString("N2", formatoHnd)}").AlignRight().Bold().FontSize(10);
                        table.Cell().Background(fondoTotal).Padding(5).Text($"L.{totalHaber.ToString("N2", formatoHnd)}").AlignRight().Bold().FontSize(10);
                        table.Cell().Background(fondoTotal).Padding(5).Text($"L.{totalGeneral.ToString("N2", formatoHnd)}").AlignRight().Bold().FontSize(10);
                    });

                    // ================= FOOTER =================
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