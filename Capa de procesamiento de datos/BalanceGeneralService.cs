using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// 
    /// </summary>
    public class BalanceGeneralService
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
        /// Initializes a new instance of the <see cref="BalanceGeneralService"/> class.
        /// </summary>
        public BalanceGeneralService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesBalanceGeneral");

            Directory.CreateDirectory(_carpetaReportes);
        }

        /// <summary>
        /// Generars the balance general.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        /// <param name="nombre_parroquia">The nombre parroquia.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <param name="usuario_id">The usuario identifier.</param>
        /// <returns></returns>
        public string GenerarBalanceGeneral(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id)
        {
            // 1. Traer datos del SP
            DataTable datos = _repo.ObtenerBalanceGeneral(parroquia_id, desde, hasta);

            // 2. Generar PDF
            byte[] pdfBytes = GenerarPdf(datos, nombre_parroquia, desde, hasta);

            // 3. Guardar archivo
            string nombre_archivo = $"BalanceGeneral_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);

            File.WriteAllBytes(ruta_completa, pdfBytes);

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
                    // ENCABEZADO (ajustado a tu formato)
                    // ==========================================================
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            // IZQUIERDA
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("BALANCE GENERAL")
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
                            .Text("RESUMEN DEL BALANCE GENERAL")
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
                                    h.Cell().Text("Cuenta").Bold().FontColor("#003399");
                                    h.Cell().Text("Saldo").Bold().FontColor("#003399").AlignRight();
                                });

                                // Filas
                                foreach (DataRow row in datos.Rows)
                                {
                                    table.Cell().Text(row["Cuenta"]?.ToString());
                                    table.Cell().Text(string.Format("{0:N2}", row["Saldo"])).AlignRight();
                                }
                            });

                        col.Item().PaddingVertical(15)
                            .LineHorizontal(1)
                            .LineColor("#D4AF37");

                        // ----------------------------
                        // DETALLE DE CUENTAS
                        // ----------------------------
                        col.Item().Text("DETALLE DE CUENTAS")
                            .Bold().FontSize(14)
                            .FontColor("#003399");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(70);   // Cuenta
                                cols.ConstantColumn(80);   // Detalle
                                cols.ConstantColumn(100);  // Saldo
                            });

                            // CABECERA
                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Cuenta").Bold().FontColor("#FFFFFF");

                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Detalle").Bold().FontColor("#FFFFFF");

                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Saldo").Bold().FontColor("#FFFFFF")
                                    .AlignRight();
                            });

                            // FILAS
                            int i = 0;
                            foreach (DataRow row in datos.Rows)
                            {
                                string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5";

                                table.Cell().Background(fondo).Padding(4).Text(row["Cuenta"]?.ToString());
                                table.Cell().Background(fondo).Padding(4).Text(row["Detalle"]?.ToString());
                                table.Cell().Background(fondo).Padding(4).Text(string.Format("{0:N2}", row["Saldo"])).AlignRight();

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


    }
}
