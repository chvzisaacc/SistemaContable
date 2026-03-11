using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using System.Data;

namespace Capa_de_procesamiento_de_datos
{
    /// <summary>
    /// 
    /// </summary>
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
            DataTable dtEntradas = ds.Tables[1];  // Entradas de la BD
            DataTable dtSalidas = ds.Tables[2];   // Salidas de la BD
            DataTable dtTotales = ds.Tables[3];   // Totales de la BD

            string nombreParroquia = dtInfo.Rows[0]["ParroquiaNombre"]?.ToString() ?? "";
            string nombreSacerdote = dtInfo.Rows[0]["SacerdoteNombre"]?.ToString() ?? "";

            // Calcular los totales
            decimal totalEntradas = ObtenerMontoEntradas(dtEntradas);
            decimal totalSalidas = ObtenerMontoSalidas(dtSalidas);

            decimal subtotalEntradas = totalEntradas;
            decimal docePorciento = totalEntradas * 0.12m;
            decimal totalALaCuria = docePorciento;

            byte[] pdfBytes = GenerarPdfCuria(
                dtEntradas,
                dtSalidas,
                totalEntradas,
                totalSalidas,
                docePorciento,
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

        private decimal ObtenerMontoEntradas(DataTable dtEntradas)
        {
            decimal total = 0;
            foreach (DataRow row in dtEntradas.Rows)
            {
                total += Convert.ToDecimal(row["Monto"]);
            }
            return total;
        }

        private decimal ObtenerMontoSalidas(DataTable dtSalidas)
        {
            decimal total = 0;
            foreach (DataRow row in dtSalidas.Rows)
            {
                total += Convert.ToDecimal(row["Monto"]);
            }
            return total;
        }

        private byte[] GenerarPdfCuria(
            DataTable dtEntradas,
            DataTable dtSalidas,
            decimal totalEntradas,
            decimal totalSalidas,
            decimal docePorciento,
            decimal totalALaCuria,
            string parroquia,
            DateTime desde,
            DateTime hasta,
            string nombreSacerdote)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Arquidiocesis de Tegucigalpa")
                            .FontSize(16)
                            .Bold()
                            .FontColor("#003399");

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
                                columns.RelativeColumn(3); // Concepto de entradas
                                columns.ConstantColumn(70); // Monto de entradas
                                columns.RelativeColumn(3); // Concepto de salidas
                                columns.ConstantColumn(70); // Monto de salidas
                            });

                            void Celda(string texto, bool negrita = false, string colorFondo = "#FFFFFF")
                            {
                                var cell = table.Cell()
                                    .Border(0.5f)
                                    .Padding(2)
                                    .Background(colorFondo);

                                var t = cell.Text(texto ?? string.Empty)
                                    .FontSize(9);

                                if (negrita)
                                    t.Bold();
                            }

                            // Encabezados con fondo dorado
                            Celda("ENTRADAS PARA LA CURIA", true, "#D4AF37");
                            Celda("", true);
                            Celda("SALIDAS", true, "#D4AF37");
                            Celda("", true);

                            // Filas de entradas (con datos de la BD)
                            foreach (DataRow row in dtEntradas.Rows)
                            {
                                string nombreCuenta = row["NombreCuenta"]?.ToString() ?? "";
                                decimal monto = Convert.ToDecimal(row["Monto"]);
                                Celda(nombreCuenta, false, "#FFFFFF");
                                Celda($"Lps {monto:N2}", false, "#FFFFFF");
                            }

                            // Filas de salidas (con datos de la BD)
                            foreach (DataRow row in dtSalidas.Rows)
                            {
                                string nombreCuenta = row["NombreCuenta"]?.ToString() ?? "";
                                decimal monto = Convert.ToDecimal(row["Monto"]);
                                Celda(nombreCuenta, false, "#FFFFFF");
                                Celda($"Lps {monto:N2}", false, "#FFFFFF");
                            }

                            // Mostrar el 12% y el total a la curia
                            // Subtotal / 12%
                            Celda("SUBTOTAL=", true, "#D4AF37");
                            Celda($"Lps {totalEntradas:N2}", true, "#D4AF37");
                            Celda("X 12%", true, "#D4AF37");
                            Celda($"Lps {docePorciento:N2}", true, "#D4AF37");

                            // A la Curia
                            Celda("", false);
                            Celda("", false);
                            Celda("A LA CURIA ARZOBISPAL", true, "#D4AF37");
                            Celda($"Lps {totalALaCuria:N2}", true, "#D4AF37");

                        });



                        col.Item().Text("");

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
                                table.Cell().Border(0.5f).Padding(2).Text(valor).FontSize(9);
                            }

                            Celda2("Total entradas del mes", totalEntradas == 0 ? "" : $"Lps {totalEntradas:N2}");
                            Celda2("Total salidas del mes", totalSalidas == 0 ? "" : $"Lps {totalSalidas:N2}");
                            Celda2("Ganancias (+) o perdidas (-) del mes", $"Lps {totalEntradas - totalSalidas:N2}");
                        });

                        col.Item().Text("");

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(9);
                            row.RelativeItem().AlignRight().Text($"Sacerdote: {nombreSacerdote}").FontSize(9);
                        });

                        col.Item().Text("");

                        col.Item().Text(
                            @"Recordamos que DEBEN ENTREGAR A LA CURIA, EL DOCE PORCIENTO (12%) sobre todas las entradas de la Parroquias, Iglesias o Capillas, y es de carácter obligatorio y nadie queda exento de esta obligacion."
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



