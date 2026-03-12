using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// Servicio para generar reportes de Balance General
    /// Consume las 5 tablas de sp_GenerarBalanceGeneral:
    ///   [0] Resumen    (TotalActivos, TotalPasivos, TotalPatrimonio, Diferencia)
    ///   [1] Activos OrigenFuentes (Categoria, Cuenta, Saldo)
    ///   [2] Activos CatalogoCuentas nat=1 (Categoria, Cuenta, Saldo)
    ///   [3] Pasivos CatalogoCuentas nat=2 (Categoria, Cuenta, Saldo)
    ///   [4] Patrimonio (Concepto, Monto)
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

        public string GenerarBalanceGeneral(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id)
        {
            DataSet ds = _repo.ObtenerBalanceGeneral(parroquia_id, desde, hasta);
            byte[] pdfBytes = GenerarPdf(ds, nombre_parroquia, desde, hasta);

            string nombre_archivo = $"BalanceGeneral_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);
            File.WriteAllBytes(ruta_completa, pdfBytes);

            return ruta_completa;
        }

        public byte[] GenerarPdf(DataSet ds, string parroquia, DateTime desde, DateTime hasta)
        {
            string logoPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "logo_arqui.png");

            // ============================================================
            // EXTRAER TABLAS DEL DATASET
            // ============================================================
            DataRow resumen = ds.Tables[0].Rows[0];
            DataTable dtActivosOrigen = ds.Tables[1]; // OrigenFuentes
            DataTable dtActivosCatalogo = ds.Tables[2]; // CatalogoCuentas nat=1
            DataTable dtPasivos = ds.Tables[3]; // CatalogoCuentas nat=2
            DataTable dtPatrimonio = ds.Tables[4]; // Patrimonio

            decimal totalActivos = Convert.ToDecimal(resumen["TotalActivos"]);
            decimal totalPasivos = Convert.ToDecimal(resumen["TotalPasivos"]);
            decimal totalPatrimonio = Convert.ToDecimal(resumen["TotalPatrimonio"]);
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

                                table.Cell().Text("TOTAL ACTIVOS").Bold();
                                table.Cell().Text(string.Format("{0:N2}", totalActivos))
                                    .AlignRight().Bold();

                                table.Cell().Text("TOTAL PASIVOS").Bold();
                                table.Cell().Text(string.Format("{0:N2}", totalPasivos))
                                    .AlignRight().Bold();

                                table.Cell().Text("TOTAL PATRIMONIO").Bold();
                                table.Cell().Text(string.Format("{0:N2}", totalPatrimonio))
                                    .AlignRight().Bold();

                                table.Cell().ColumnSpan(2).PaddingVertical(5)
                                    .LineHorizontal(1).LineColor("#003399");

                                table.Cell()
                                    .Text(cuadra ? "✓ Balance Cuadrado" : $"⚠ Diferencia: {diferencia:N2}")
                                    .FontSize(10)
                                    .FontColor(cuadra ? "#00AA00" : "#FF6600");
                                table.Cell().Text("");
                            });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor("#D4AF37");

                        // ----------------------------------------
                        // SECCIÓN 2: ACTIVOS — OrigenFuentes
                        // ----------------------------------------
                        col.Item().PaddingTop(5).PaddingBottom(5)
                            .Background("#003399").Padding(8)
                            .Text("ACTIVOS — Efectivo y Equivalentes")
                            .FontSize(13).Bold().FontColor("#FFFFFF");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(2);
                                cols.RelativeColumn(3);
                                cols.ConstantColumn(130);
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

                            RenderTablaConSubtotales(table, dtActivosOrigen, totalActivos, false);
                        });

                        // ----------------------------------------
                        // SECCIÓN 3: ACTIVOS — CatalogoCuentas
                        // ----------------------------------------
                        if (dtActivosCatalogo.Rows.Count > 0)
                        {
                            col.Item().PaddingTop(10).PaddingBottom(5)
                                .Background("#003399").Padding(8)
                                .Text("ACTIVOS — Otros Activos")
                                .FontSize(13).Bold().FontColor("#FFFFFF");

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(2);
                                    cols.RelativeColumn(3);
                                    cols.ConstantColumn(130);
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Background("#D4AF37").Padding(5)
                                        .Text("Categoría").Bold().FontColor("#FFFFFF").FontSize(10);
                                    h.Cell().Background("#D4AF37").Padding(5)
                                        .Text("Cuenta").Bold().FontColor("#FFFFFF").FontSize(10);
                                    h.Cell().Background("#D4AF37").Padding(5)
                                        .Text("Saldo").Bold().FontColor("#FFFFFF").FontSize(10).AlignRight();
                                });

                                RenderTablaConSubtotales(table, dtActivosCatalogo, 0, true);
                            });
                        }

                        // Total Activos
                        col.Item().Background("#003399").Padding(8).Row(row =>
                        {
                            row.RelativeItem().Text("TOTAL ACTIVOS")
                                .Bold().FontSize(11).FontColor("#FFFFFF");
                            row.ConstantItem(130).Text(string.Format("{0:N2}", totalActivos))
                                .Bold().FontSize(11).FontColor("#FFFFFF").AlignRight();
                        });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor("#D4AF37");

                        // ----------------------------------------
                        // SECCIÓN 4: PASIVOS
                        // ----------------------------------------
                        col.Item().PaddingTop(5).PaddingBottom(5)
                            .Background("#003399").Padding(8)
                            .Text("PASIVOS")
                            .FontSize(13).Bold().FontColor("#FFFFFF");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(2);
                                cols.RelativeColumn(3);
                                cols.ConstantColumn(130);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Categoría").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Cuenta").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Saldo").Bold().FontColor("#FFFFFF").FontSize(10).AlignRight();
                            });

                            if (dtPasivos.Rows.Count == 0)
                            {
                                table.Cell().ColumnSpan(3)
                                    .Background("#F9F9F9").Padding(10)
                                    .Text("Sin pasivos registrados")
                                    .FontSize(9).FontColor("#666666");
                            }
                            else
                            {
                                RenderTablaConSubtotales(table, dtPasivos, totalPasivos, true);
                            }

                            table.Cell().ColumnSpan(2)
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
                        // SECCIÓN 5: PATRIMONIO
                        // ----------------------------------------
                        col.Item().PaddingTop(5).PaddingBottom(5)
                            .Background("#003399").Padding(8)
                            .Text("PATRIMONIO")
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
                                    .Text("Concepto").Bold().FontColor("#FFFFFF").FontSize(10);
                                h.Cell().Background("#D4AF37").Padding(5)
                                    .Text("Monto").Bold().FontColor("#FFFFFF").FontSize(10).AlignRight();
                            });

                            if (dtPatrimonio.Rows.Count == 0)
                            {
                                table.Cell().ColumnSpan(2)
                                    .Background("#F9F9F9").Padding(10)
                                    .Text("Sin movimientos de patrimonio registrados")
                                    .FontSize(9).FontColor("#666666");
                            }
                            else
                            {
                                int i = 0;
                                foreach (DataRow row in dtPatrimonio.Rows)
                                {
                                    // Omitir fila TOTAL PATRIMONIO del SP,
                                    // se muestra separado abajo
                                    if (row["Concepto"]?.ToString() == "TOTAL PATRIMONIO")
                                        continue;

                                    string fondo = (i % 2 == 0) ? "#FFFFFF" : "#F9F9F9";
                                    decimal monto = Convert.ToDecimal(row["Monto"]);

                                    // Gastos en rojo, ingresos en verde
                                    string colorTexto = monto < 0 ? "#CC0000" : "#000000";

                                    table.Cell().Background(fondo).Padding(5)
                                        .Text(row["Concepto"]?.ToString())
                                        .FontSize(9).FontColor(colorTexto);
                                    table.Cell().Background(fondo).Padding(5)
                                        .Text(string.Format("{0:N2}", monto))
                                        .FontSize(9).AlignRight().FontColor(colorTexto);
                                    i++;
                                }
                            }

                            // Total Patrimonio
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text("TOTAL PATRIMONIO")
                                .Bold().FontSize(10).FontColor("#FFFFFF");
                            table.Cell()
                                .Background("#003399").Padding(6)
                                .Text(string.Format("{0:N2}", totalPatrimonio))
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

        // ============================================================
        // MÉTODO AUXILIAR: Renderiza tabla con subtotales por categoría
        // ============================================================
        private void RenderTablaConSubtotales(
            QuestPDF.Fluent.TableDescriptor table,
            DataTable dt,
            decimal totalGeneral,
            bool mostrarTotalGeneral)
        {
            if (dt.Rows.Count == 0) return;

            int i = 0;
            string categoriaActual = "";
            decimal subtotal = 0;

            var lista = dt.AsEnumerable()
                .OrderBy(r => r["Categoria"]?.ToString())
                .ThenBy(r => r["Cuenta"]?.ToString())
                .ToList();

            for (int idx = 0; idx < lista.Count; idx++)
            {
                DataRow row = lista[idx];
                string categoria = row["Categoria"]?.ToString() ?? "";
                decimal saldo = Convert.ToDecimal(row["Saldo"]);

                if (categoria != categoriaActual)
                {
                    // Mostrar subtotal de categoría anterior
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
        }
    }
}