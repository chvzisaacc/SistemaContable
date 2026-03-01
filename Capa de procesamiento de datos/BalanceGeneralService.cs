using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// Servicio para generar reportes de Balance General
    /// </summary>
    public class BalanceGeneralService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public BalanceGeneralService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");
            _carpetaReportes = Path.Combine(baseReportsFolder, "BalanceGeneral");
            Directory.CreateDirectory(_carpetaReportes);
        }

        /// <summary>
        /// Genera el archivo PDF del Balance General y lo guarda en disco.
        /// </summary>
        public string GenerarBalanceGeneral(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id) // Recibido por consistencia con otros servicios, no se usa aquí
        {
            // 1. Traer datos del SP
            DataSet ds = _repo.ObtenerBalanceGeneral(parroquia_id, desde, hasta);

            // 2. Generar PDF
            byte[] pdfBytes = GenerarPdf(ds, nombre_parroquia, desde, hasta);

            // 3. Guardar archivo
            string nombre_archivo = $"BalanceGeneral_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);
            File.WriteAllBytes(ruta_completa, pdfBytes);

            return ruta_completa;
        }

        /// <summary>
        /// Genera el PDF del Balance General.
        /// El DataSet contiene 4 tablas:
        ///   [0] Resumen    (TotalActivos, TotalPasivos, TotalCapital, Diferencia)
        ///   [1] Activos    (Categoria, Cuenta, Saldo)
        ///   [2] Pasivos    (Cuenta, Saldo)
        ///   [3] Capital    (CodigoCuenta, Cuenta, Detalle, Saldo)
        /// </summary>
        public byte[] GenerarPdf(DataSet ds, string parroquia, DateTime desde, DateTime hasta)
        {
            string logoPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "logo_arqui.png");

            // Extraer tablas del DataSet
            DataRow resumen = ds.Tables[0].Rows[0];
            DataTable dtActivos = ds.Tables[1];
            DataTable dtPasivos = ds.Tables[2];
            DataTable dtCapital = ds.Tables[3];

            decimal totalActivos = Convert.ToDecimal(resumen["TotalActivos"]);
            decimal totalPasivos = Convert.ToDecimal(resumen["TotalPasivos"]);
            decimal totalCapital = Convert.ToDecimal(resumen["TotalCapital"]);
            decimal diferencia = Convert.ToDecimal(resumen["Diferencia"]);
            bool cuadra = Math.Abs(diferencia) < 0.01m;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.Letter);

                    // ==========================================
                    // ENCABEZADO
                    // ==========================================
                    page.Header().Column(header =>
                    {
                        header.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("BALANCE GENERAL")
                                    .FontSize(20).Bold().FontColor("#003399");
                                col.Item().Text(parroquia)
                                    .FontSize(12).FontColor("#444444");
                                col.Item().Text($"Período: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")
                                    .FontSize(10).FontColor("#666666");
                            });

                            if (File.Exists(logoPath))
                            {
                                row.ConstantItem(110).Height(90)
                                   .Image(logoPath, ImageScaling.FitArea);
                            }
                        });

                        header.Item().PaddingTop(4)
                            .LineHorizontal(1).LineColor("#D4AF37");
                    });

                    // ==========================================
                    // CONTENIDO
                    // ==========================================
                    page.Content().Column(col =>
                    {
                        // ----------------------------------------
                        // SECCIÓN 1: RESUMEN EJECUTIVO
                        // ----------------------------------------
                        col.Item().PaddingVertical(10)
                            .Text("RESUMEN EJECUTIVO")
                            .Bold().FontSize(15).FontColor("#003399");

                        col.Item()
                            .Background("#E8F1FF")
                            .Border(1).BorderColor("#003399")
                            .Padding(15)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn();
                                    c.ConstantColumn(150);
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Text("Concepto").Bold().FontColor("#003399");
                                    h.Cell().Text("Saldo").Bold().FontColor("#003399").AlignRight();
                                });

                                // ACTIVOS
                                table.Cell().Text("TOTAL ACTIVOS").Bold();
                                table.Cell().Text(string.Format("{0:N2}", totalActivos))
                                    .AlignRight().Bold();

                                // PASIVOS
                                table.Cell().Text("TOTAL PASIVOS").Bold();
                                table.Cell().Text(string.Format("{0:N2}", totalPasivos))
                                    .AlignRight().Bold();

                                // CAPITAL
                                table.Cell().Text("TOTAL CAPITAL").Bold();
                                table.Cell().Text(string.Format("{0:N2}", totalCapital))
                                    .AlignRight().Bold();

                                // Separador
                                table.Cell().ColumnSpan(2).PaddingVertical(5)
                                    .LineHorizontal(1).LineColor("#003399");

                                // DIFERENCIA
                                table.Cell()
                                    .Text(cuadra ? "✓ Balance Cuadrado" : $"⚠ Diferencia: {diferencia:N2}")
                                    .FontSize(10)
                                    .FontColor(cuadra ? "#00AA00" : "#FF6600");
                                table.Cell().Text("");
                            });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor("#D4AF37");

                        // ----------------------------------------
                        // SECCIÓN 2: ACTIVOS
                        // ----------------------------------------
                        col.Item().PaddingTop(5).PaddingBottom(5)
                            .Background("#003399").Padding(8)
                            .Text("ACTIVOS — Cuentas Bancarias")
                            .FontSize(13).Bold().FontColor("#FFFFFF");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(2);   // Tipo
                                cols.RelativeColumn(3);   // Cuenta
                                cols.ConstantColumn(130); // Saldo
                            });

                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Tipo").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Cuenta").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Saldo").Bold().FontColor("#FFFFFF").FontSize(10).AlignRight();
                            });

                            int i = 0;
                            string categoriaActual = "";
                            decimal subtotal = 0;
                            var listaActivos = dtActivos.AsEnumerable()
                                .OrderBy(r => r["Categoria"]?.ToString())
                                .ThenBy(r => r["Cuenta"]?.ToString())
                                .ToList();

                            for (int idx = 0; idx < listaActivos.Count; idx++)
                            {
                                DataRow row = listaActivos[idx];
                                string categoria = row["Categoria"]?.ToString() ?? "";
                                decimal saldo = Convert.ToDecimal(row["Saldo"]);

                                // Si cambia la categoría mostrar subtotal anterior
                                if (categoria != categoriaActual)
                                {
                                    if (!string.IsNullOrEmpty(categoriaActual))
                                    {
                                        table.Cell().ColumnSpan(2)
                                            .Background("#E8F1FF").Padding(5)
                                            .Text($"SUBTOTAL {categoriaActual}")
                                            .Bold().FontSize(9).FontColor("#003399");
                                        table.Cell()
                                            .Background("#E8F1FF").Padding(5)
                                            .Text(string.Format("{0:N2}", subtotal))
                                            .Bold().FontSize(9).FontColor("#003399").AlignRight();
                                    }

                                    categoriaActual = categoria;
                                    subtotal = 0;

                                    // Header de categoría
                                    table.Cell().ColumnSpan(3)
                                        .Background("#F0F0F0").Padding(6)
                                        .Text(categoria)
                                        .Bold().FontSize(10).FontColor("#003399");
                                }

                                string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F9F9F9";
                                subtotal += saldo;

                                table.Cell().Background(fondo).Padding(5).Text("").FontSize(9);
                                table.Cell().Background(fondo).Padding(5)
                                    .Text(row["Cuenta"]?.ToString()).FontSize(9);
                                table.Cell().Background(fondo).Padding(5)
                                    .Text(string.Format("{0:N2}", saldo)).FontSize(9).AlignRight();
                                i++;
                            }

                            // Último subtotal
                            if (!string.IsNullOrEmpty(categoriaActual))
                            {
                                table.Cell().ColumnSpan(2)
                                    .Background("#E8F1FF").Padding(5)
                                    .Text($"SUBTOTAL {categoriaActual}")
                                    .Bold().FontSize(9).FontColor("#003399");
                                table.Cell()
                                    .Background("#E8F1FF").Padding(5)
                                    .Text(string.Format("{0:N2}", subtotal))
                                    .Bold().FontSize(9).FontColor("#003399").AlignRight();
                            }

                            // Total Activos
                            table.Cell().ColumnSpan(2)
                                .Background("#003399").Padding(6)
                                .Text("TOTAL ACTIVOS")
                                .Bold().FontSize(10).FontColor("#FFFFFF");
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text(string.Format("{0:N2}", totalActivos))
                                .Bold().FontSize(10).FontColor("#FFFFFF").AlignRight();
                        });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor("#D4AF37");

                        // ----------------------------------------
                        // SECCIÓN 3: PASIVOS
                        // ----------------------------------------
                        col.Item().PaddingTop(5).PaddingBottom(5)
                            .Background("#003399").Padding(8)
                            .Text("PASIVOS — Gastos Acumulados")
                            .FontSize(13).Bold().FontColor("#FFFFFF");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(3);
                                cols.ConstantColumn(130);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Cuenta").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Saldo").Bold().FontColor("#FFFFFF").FontSize(10).AlignRight();
                            });

                            int i = 0;
                            foreach (DataRow row in dtPasivos.Rows)
                            {
                                string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F9F9F9";
                                table.Cell().Background(fondo).Padding(5)
                                    .Text(row["Cuenta"]?.ToString()).FontSize(9);
                                table.Cell().Background(fondo).Padding(5)
                                    .Text(string.Format("{0:N2}", row["Saldo"]))
                                    .FontSize(9).AlignRight();
                                i++;
                            }

                            // Total Pasivos
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text("TOTAL PASIVOS")
                                .Bold().FontSize(10).FontColor("#FFFFFF");
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text(string.Format("{0:N2}", totalPasivos))
                                .Bold().FontSize(10).FontColor("#FFFFFF").AlignRight();
                        });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor("#D4AF37");

                        // ----------------------------------------
                        // SECCIÓN 4: CAPITAL
                        // ----------------------------------------
                        col.Item().PaddingTop(5).PaddingBottom(5)
                            .Background("#003399").Padding(8)
                            .Text("CAPITAL — Patrimonio")
                            .FontSize(13).Bold().FontColor("#FFFFFF");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(3);   // Concepto
                                cols.ConstantColumn(130); // Monto
                            });

                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Concepto").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Monto").Bold().FontColor("#FFFFFF").FontSize(10).AlignRight();
                            });

                            if (dtCapital.Rows.Count == 0)
                            {
                                table.Cell().ColumnSpan(2)
                                    .Background("#F9F9F9").Padding(10)
                                    .Text("Sin movimientos de capital registrados")
                                    .FontSize(9).FontColor("#666666");
                            }
                            else
                            {
                                int i = 0;
                                foreach (DataRow row in dtCapital.Rows)
                                {
                                    string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F9F9F9";
                                    decimal monto = Convert.ToDecimal(row["Monto"]);

                                    // Resaltar "Gastos Acumulados" en rojo para claridad visual
                                    string colorTexto = row["Concepto"].ToString() == "Gastos Acumulados"
                                        ? "#CC0000" : "#000000";

                                    table.Cell().Background(fondo).Padding(5)
                                        .Text(row["Concepto"]?.ToString())
                                        .FontSize(9).FontColor(colorTexto);
                                    table.Cell().Background(fondo).Padding(5)
                                        .Text(string.Format("{0:N2}", monto))
                                        .FontSize(9).AlignRight().FontColor(colorTexto);
                                    i++;
                                }
                            }

                            // Total Capital
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text("TOTAL CAPITAL")
                                .Bold().FontSize(10).FontColor("#FFFFFF");
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text(string.Format("{0:N2}", totalCapital))
                                .Bold().FontSize(10).FontColor("#FFFFFF").AlignRight();
                        });
                    });

                    // ==========================================
                    // PIE DE PÁGINA
                    // ==========================================
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
    }
}