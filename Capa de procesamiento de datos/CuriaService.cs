using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    public class CuriaService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public CuriaService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");
            _carpetaReportes = Path.Combine(baseReportsFolder, "ReportesCuria");
            Directory.CreateDirectory(_carpetaReportes);
        }

        public string GenerarInformeCuria(int usuarioId, DateTime desde, DateTime hasta)
        {
            DataSet ds = _repo.ObtenerDatosCuriaPorUsuario(usuarioId, desde, hasta);

            DataTable dtInfo = ds.Tables[0];
            DataTable dtEntradas = ds.Tables[1];
            DataTable dtSalidas = ds.Tables[2];
            DataTable dtTotales = ds.Tables[3];

            string nombreParroquia = dtInfo.Rows[0]["ParroquiaNombre"]?.ToString() ?? "";
            string nombreSacerdote = dtInfo.Rows[0]["SacerdoteNombre"]?.ToString() ?? "";

            // ✅ Usar los valores calculados por el SP, no recalcular en C#
            DataRow totales = dtTotales.Rows[0];
            decimal totalEntradas = Convert.ToDecimal(totales["TotalEntradas"]);
            decimal totalSalidas = Convert.ToDecimal(totales["TotalSalidas"]);
            decimal gananciaMes = Convert.ToDecimal(totales["GananciaMes"]);
            decimal docePorciento = Convert.ToDecimal(totales["DocePorciento"]);
            decimal subtotalCuria = Convert.ToDecimal(totales["SubtotalEntradasCuria"]);
            decimal totalALaCuria = Convert.ToDecimal(totales["TotalALaCuriaArzobispal"]);

            byte[] pdfBytes = GenerarPdfCuria(
                dtEntradas,
                dtSalidas,
                totalEntradas,
                totalSalidas,
                gananciaMes,
                docePorciento,
                subtotalCuria,
                totalALaCuria,
                nombreParroquia,
                desde,
                hasta,
                nombreSacerdote
            );

            string nombreArchivo = $"Curia_{nombreParroquia}_{desde:yyyyMM}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);
            return rutaCompleta;
        }

        private byte[] GenerarPdfCuria(
            DataTable dtEntradas,
            DataTable dtSalidas,
            decimal totalEntradas,
            decimal totalSalidas,
            decimal gananciaMes,
            decimal docePorciento,
            decimal subtotalCuria,
            decimal totalALaCuria,
            string parroquia,
            DateTime desde,
            DateTime hasta,
            string nombreSacerdote)
        {
            // ✅ Convertir a listas para iterar en paralelo
            var entradas = dtEntradas.AsEnumerable()
                .Select(r => new {
                    Nombre = r["NombreCuenta"]?.ToString() ?? "",
                    Monto = Convert.ToDecimal(r["Monto"])
                }).ToList();

            var salidas = dtSalidas.AsEnumerable()
                .Select(r => new {
                    Nombre = r["NombreCuenta"]?.ToString() ?? "",
                    Monto = Convert.ToDecimal(r["Monto"])
                }).ToList();

            // ✅ Iterar hasta cubrir la lista más larga
            int maxFilas = Math.Max(entradas.Count, salidas.Count);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Arquidiocesis de Tegucigalpa")
                            .FontSize(16).Bold().FontColor("#003399");

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Parroquia {parroquia}, Tegucigalpa");
                            row.ConstantItem(120).AlignRight().Text($"Año: {desde:yyyy}");
                        });
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);   // Concepto entrada
                                columns.ConstantColumn(90);  // Monto entrada
                                columns.RelativeColumn(3);   // Concepto salida
                                columns.ConstantColumn(90);  // Monto salida
                            });

                            void Celda(string texto, bool negrita = false, string colorFondo = "#FFFFFF", bool alinearDerecha = false)
                            {
                                var cell = table.Cell()
                                    .Border(0.5f)
                                    .Padding(2)
                                    .Background(colorFondo);

                                var t = alinearDerecha
                                    ? cell.AlignRight().Text(texto ?? string.Empty).FontSize(9)
                                    : cell.Text(texto ?? string.Empty).FontSize(9);

                                if (negrita) t.Bold();
                            }

                            // Encabezados
                            Celda("ENTRADAS PARA LA CURIA", true, "#D4AF37");
                            Celda("", true, "#D4AF37");
                            Celda("SALIDAS", true, "#D4AF37");
                            Celda("", true, "#D4AF37");

                            // ✅ Filas en paralelo: entrada[i] al lado de salida[i]
                            for (int i = 0; i < maxFilas; i++)
                            {
                                string eNombre = i < entradas.Count ? entradas[i].Nombre : "";
                                string eMonto = i < entradas.Count ? $"Lps {entradas[i].Monto:N2}" : "";
                                string sNombre = i < salidas.Count ? salidas[i].Nombre : "";
                                string sMonto = i < salidas.Count ? $"Lps {salidas[i].Monto:N2}" : "";

                                Celda(eNombre);
                                Celda(eMonto, false, "#FFFFFF", true);
                                Celda(sNombre);
                                Celda(sMonto, false, "#FFFFFF", true);
                            }

                            // Fila subtotal / 12%
                            Celda($"SUBTOTAL=", true, "#D4AF37");
                            Celda($"Lps {subtotalCuria:N2}", true, "#D4AF37", true);
                            Celda("X 12%", true, "#D4AF37");
                            Celda($"Lps {docePorciento:N2}", true, "#D4AF37", true);

                            // Fila total a la curia
                            Celda("", false);
                            Celda("", false);
                            Celda("A LA CURIA ARZOBISPAL", true, "#D4AF37");
                            Celda($"Lps {totalALaCuria:N2}", true, "#D4AF37", true);
                        });

                        col.Item().Text("");

                        // Tabla resumen
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(120);
                            });

                            void Celda2(string texto, string valor = "")
                            {
                                table.Cell().Border(0.5f).Padding(2).Text(texto).FontSize(9);
                                table.Cell().Border(0.5f).Padding(2).AlignRight().Text(valor).FontSize(9);
                            }

                            Celda2("Total entradas del mes",
                                totalEntradas == 0 ? "" : $"Lps {totalEntradas:N2}");
                            Celda2("Total salidas del mes",
                                totalSalidas == 0 ? "" : $"Lps {totalSalidas:N2}");
                            Celda2("Ganancias (+) o perdidas (-) del mes",
                                $"Lps {gananciaMes:N2}");
                        });

                        col.Item().Text("");

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(9);
                            row.RelativeItem().AlignRight().Text($"Sacerdote: {nombreSacerdote}").FontSize(9);
                        });

                        col.Item().Text("");

                        col.Item().Text(
                            "Recordamos que DEBEN ENTREGAR A LA CURIA, EL DOCE PORCIENTO (12%) sobre todas las entradas " +
                            "de la Parroquias, Iglesias o Capillas, y es de carácter obligatorio y nadie queda exento de esta obligacion."
                        ).FontSize(8);
                    });

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