using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    // Genera el informe PDF de remesas hacia la Curia Arzobispal usando QuestPDF
    public class CuriaService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        // Crea la carpeta de destino en Mis Documentos si no existe
        public CuriaService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");
            _carpetaReportes = Path.Combine(baseReportsFolder, "ReportesCuria");
            Directory.CreateDirectory(_carpetaReportes);
        }

        /// <summary>Genera el PDF del informe Curia y devuelve la ruta donde fue guardado.</summary>
        public string GenerarInformeCuria(int usuarioId, DateTime desde, DateTime hasta)
        {
            DataSet ds = _repo.ObtenerDatosCuriaPorUsuario(usuarioId, desde, hasta);

            // El SP devuelve 4 tablas en orden: info general, entradas, salidas, totales
            DataTable dtInfo = ds.Tables[0];
            DataTable dtEntradas = ds.Tables[1];
            DataTable dtSalidas = ds.Tables[2];
            DataTable dtTotales = ds.Tables[3];

            string nombreParroquia = dtInfo.Rows[0]["ParroquiaNombre"]?.ToString() ?? "";
            string nombreSacerdote = dtInfo.Rows[0]["SacerdoteNombre"]?.ToString() ?? "";

            DataRow totales = dtTotales.Rows[0];
            decimal totalEntradas = Convert.ToDecimal(totales["TotalEntradas"]);
            decimal totalSalidas = Convert.ToDecimal(totales["TotalSalidas"]);
            decimal gananciaMes = Convert.ToDecimal(totales["GananciaMes"]);
            decimal docePorciento = Convert.ToDecimal(totales["DocePorciento"]);
            decimal subtotalCuria = Convert.ToDecimal(totales["SubtotalEntradasCuria"]);
            decimal totalALaCuria = Convert.ToDecimal(totales["TotalALaCuriaArzobispal"]);

            byte[] pdfBytes = GenerarPdfCuria(
                dtEntradas, dtSalidas,
                totalEntradas, totalSalidas,
                gananciaMes, docePorciento,
                subtotalCuria, totalALaCuria,
                nombreParroquia, desde, hasta,
                nombreSacerdote
            );

            // Nombre único por parroquia y mes para evitar sobreescrituras accidentales
            string nombreArchivo = $"Curia_{nombreParroquia}_{desde:yyyyMM}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);
            return rutaCompleta;
        }

        /// <summary>Construye el documento PDF con QuestPDF y retorna sus bytes.</summary>
        private byte[] GenerarPdfCuria(
            DataTable dtEntradas, DataTable dtSalidas,
            decimal totalEntradas, decimal totalSalidas,
            decimal gananciaMes, decimal docePorciento,
            decimal subtotalCuria, decimal totalALaCuria,
            string parroquia, DateTime desde, DateTime hasta,
            string nombreSacerdote)
        {
            // Formato numérico con separador de miles en inglés (1,234.56)
            var formatoHnd = new System.Globalization.CultureInfo("en-US");

            string logoPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo_arqui.png"
            );

            var todasEntradas = dtEntradas.AsEnumerable()
                .Select(r => new {
                    Nombre = r["NombreCuenta"]?.ToString() ?? "",
                    Monto = Convert.ToDecimal(r["Monto"])
                }).ToList();

            var salidas = dtSalidas.AsEnumerable()
                .Select(r => new {
                    Nombre = r["NombreCuenta"]?.ToString() ?? "",
                    Monto = Convert.ToDecimal(r["Monto"])
                }).ToList();

            // Entradas normales van en la tabla principal; las especiales van debajo del subtotal
            var entradasNormales = todasEntradas.Where(e =>
                !e.Nombre.StartsWith("COLECTA", StringComparison.OrdinalIgnoreCase) &&
                !e.Nombre.StartsWith("DONATIVO", StringComparison.OrdinalIgnoreCase) &&
                !e.Nombre.Contains("SEMINARIO", StringComparison.OrdinalIgnoreCase) &&
                !e.Nombre.Contains("DISPENSA", StringComparison.OrdinalIgnoreCase) &&
                !e.Nombre.Contains("CONFIRMA", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            var entradasDebajo = todasEntradas.Where(e =>
                e.Nombre.StartsWith("COLECTA", StringComparison.OrdinalIgnoreCase) ||
                e.Nombre.StartsWith("DONATIVO", StringComparison.OrdinalIgnoreCase) ||
                e.Nombre.Contains("SEMINARIO", StringComparison.OrdinalIgnoreCase) ||
                e.Nombre.Contains("DISPENSA", StringComparison.OrdinalIgnoreCase) ||
                e.Nombre.Contains("CONFIRMA", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            // Las columnas de entradas y salidas deben tener el mismo número de filas
            int maxFilas = Math.Max(entradasNormales.Count, salidas.Count);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(left =>
                            {
                                left.Item().Text("Arquidiocesis de Tegucigalpa")
                                    .FontSize(16).Bold().FontColor("#003399");
                                left.Item().Text($"Parroquia {parroquia}, Tegucigalpa")
                                    .FontSize(11).FontColor("#444444");
                                left.Item().Text($"Año: {desde:yyyy}")
                                    .FontSize(10).FontColor("#666666");
                            });

                            if (File.Exists(logoPath))
                            {
                                row.ConstantItem(70).Height(60)
                                   .AlignRight().AlignTop()
                                   .Image(logoPath, ImageScaling.FitArea);
                            }
                        });

                        col.Item().PaddingTop(4).LineHorizontal(1).LineColor("#D4AF37");
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingTop(8).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(90);
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(90);
                            });

                            // Helper local para no repetir el estilo de cada celda
                            void Celda(string texto, bool negrita = false, string colorFondo = "#FFFFFF", bool alinearDerecha = false)
                            {
                                var cell = table.Cell().Border(0.5f).Padding(2).Background(colorFondo);
                                var t = alinearDerecha
                                    ? cell.AlignRight().Text(texto ?? string.Empty).FontSize(9)
                                    : cell.Text(texto ?? string.Empty).FontSize(9);
                                if (negrita) t.Bold();
                            }

                            Celda("ENTRADAS PARA LA CURIA", true, "#D4AF37");
                            Celda("", true, "#D4AF37");
                            Celda("SALIDAS", true, "#D4AF37");
                            Celda("", true, "#D4AF37");

                            // Filas de entradas y salidas en paralelo; celdas vacías si una lista es más corta
                            for (int i = 0; i < maxFilas; i++)
                            {
                                string eNombre = i < entradasNormales.Count ? entradasNormales[i].Nombre : "";
                                string eMonto = i < entradasNormales.Count ? $"L.{entradasNormales[i].Monto.ToString("N2", formatoHnd)}" : "";
                                string sNombre = i < salidas.Count ? salidas[i].Nombre : "";
                                string sMonto = i < salidas.Count ? $"L.{salidas[i].Monto.ToString("N2", formatoHnd)}" : "";

                                Celda(eNombre);
                                Celda(eMonto, false, "#FFFFFF", true);
                                Celda(sNombre);
                                Celda(sMonto, false, "#FFFFFF", true);
                            }

                            Celda("SUBTOTAL=", true, "#D4AF37");
                            Celda($"L.{subtotalCuria.ToString("N2", formatoHnd)}", true, "#D4AF37", true);
                            Celda("X 12%", true, "#D4AF37");
                            Celda($"L.{docePorciento.ToString("N2", formatoHnd)}", true, "#D4AF37", true);

                            // Colectas, donativos y similares aparecen después del subtotal
                            foreach (var entrada in entradasDebajo)
                            {
                                Celda(entrada.Nombre);
                                Celda($"L.{entrada.Monto.ToString("N2", formatoHnd)}", false, "#FFFFFF", true);
                                Celda("");
                                Celda("");
                            }

                            Celda("TOTAL ENTRADAS DEL MES", true, "#D4AF37");
                            Celda($"L.{totalEntradas.ToString("N2", formatoHnd)}", true, "#D4AF37", true);
                            Celda("TOTAL SALIDAS DEL MES", true, "#D4AF37");
                            Celda($"L.{totalSalidas.ToString("N2", formatoHnd)}", true, "#D4AF37", true);

                            Celda(""); Celda("");
                            Celda("TOTAL CURIA", true, "#D4AF37");
                            Celda($"L.{totalALaCuria.ToString("N2", formatoHnd)}", true, "#D4AF37", true);

                            Celda(""); Celda("");
                            Celda("A LA CURIA ARZOBISPAL", true, "#D4AF37");
                            Celda($"L.{totalALaCuria.ToString("N2", formatoHnd)}", true, "#D4AF37", true);
                        });

                        // Resumen de ganancias/pérdidas del mes
                        col.Item().PaddingVertical(8).Table(table =>
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

                            // Si el total es 0 se deja en blanco para no mostrar "L.0.00"
                            Celda2("Total entradas del mes",
                                totalEntradas == 0 ? "" : $"L.{totalEntradas.ToString("N2", formatoHnd)}");
                            Celda2("Total salidas del mes",
                                totalSalidas == 0 ? "" : $"L.{totalSalidas.ToString("N2", formatoHnd)}");
                            Celda2("Ganancias (+) o perdidas (-) del mes",
                                $"L.{gananciaMes.ToString("N2", formatoHnd)}");
                        });

                        col.Item().PaddingVertical(6).Row(row =>
                        {
                            row.RelativeItem().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(9);
                            row.RelativeItem().AlignRight().Text($"Sacerdote: {nombreSacerdote}").FontSize(9);
                        });

                        col.Item().PaddingTop(6).Text(
                            "Recordamos que DEBEN ENTREGAR A LA CURIA, EL DOCE PORCIENTO (12%) sobre todas las entradas " +
                            "de la Parroquias, Iglesias o Capillas, y es de carácter obligatorio y nadie queda exento de esta obligacion."
                        ).FontSize(8).FontColor("#444444");
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
    }
}