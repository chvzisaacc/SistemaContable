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
    DataTable dtEntradas, DataTable dtSalidas, decimal totalEntradas, decimal totalSalidas,
    decimal docePorciento, decimal totalALaCuria, string parroquia,
    DateTime desde, DateTime hasta, string nombreSacerdote)
        {
            // Entradas principales (las que NO son colectas NI confirmas NI dispensas NI donativo seminario)
            var entradasPrincipales = dtEntradas.AsEnumerable()
                .Where(r => !r.Field<string>("NombreCuenta").StartsWith("Colecta", StringComparison.OrdinalIgnoreCase) &&
                            !r.Field<string>("NombreCuenta").Contains("CONFIRMA", StringComparison.OrdinalIgnoreCase) &&
                            !r.Field<string>("NombreCuenta").Contains("DISPENSAS", StringComparison.OrdinalIgnoreCase) &&
                            !r.Field<string>("NombreCuenta").Contains("DONATIVO SEMINARIO", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Entradas especiales (Colectas + CONFIRMAS + DISPENSAS + DONATIVO SEMINARIO)
            var entradasEspeciales = dtEntradas.AsEnumerable()
                .Where(r => r.Field<string>("NombreCuenta").StartsWith("Colecta", StringComparison.OrdinalIgnoreCase) ||
                            r.Field<string>("NombreCuenta").Contains("CONFIRMA", StringComparison.OrdinalIgnoreCase) ||
                            r.Field<string>("NombreCuenta").Contains("DISPENSAS", StringComparison.OrdinalIgnoreCase) ||
                            r.Field<string>("NombreCuenta").Contains("DONATIVO SEMINARIO", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Salidas
            var salidasFiltradas = dtSalidas.AsEnumerable()
                .Where(r => !r.Field<string>("NombreCuenta").Contains("Colecta", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Recalcular Subtotal basado solo en entradas principales
            decimal subtotalEntradas = entradasPrincipales.Sum(r => Convert.ToDecimal(r["Monto"]));
            decimal impuesto12 = subtotalEntradas * 0.12m;

            // Total de entradas especiales
            decimal totalEntradasEspeciales = entradasEspeciales.Sum(r => Convert.ToDecimal(r["Monto"]));
            decimal totalEntradasGeneral = subtotalEntradas + totalEntradasEspeciales;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Column(col =>
                    {
                        col.Item().Text("Arquidiócesis de Tegucigalpa").FontSize(14).Bold().FontColor("#003399");
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Parroquia {parroquia}, Tegucigalpa").FontSize(10);
                            row.ConstantItem(100).AlignRight().Text($"Año: {desde:yyyy}").FontSize(10);
                        });
                        col.Item().PaddingVertical(5).LineHorizontal(1).LineColor("#D4AF37");
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); columns.ConstantColumn(85); // Entradas
                                columns.RelativeColumn(3); columns.ConstantColumn(85); // Salidas
                            });

                            // ENCABEZADOS
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("ENTRADAS PARA LA CURIA").Bold().FontSize(8);
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("").FontSize(8);
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("SALIDAS").Bold().FontSize(8);
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("").FontSize(8);

                            // FILAS SUPERIORES - SOLO SI HAY ENTRADAS PRINCIPALES
                            for (int i = 0; i < entradasPrincipales.Count; i++)
                            {
                                // Entradas
                                table.Cell().Border(0.5f).Padding(2).Text(entradasPrincipales[i]["NombreCuenta"].ToString()).FontSize(8);
                                table.Cell().Border(0.5f).Padding(2).AlignRight().Text(Convert.ToDecimal(entradasPrincipales[i]["Monto"]).ToString("N2")).FontSize(8);

                                // Salidas (paralelas)
                                if (i < salidasFiltradas.Count)
                                {
                                    table.Cell().Border(0.5f).Padding(2).Text(salidasFiltradas[i]["NombreCuenta"].ToString()).FontSize(8);
                                    table.Cell().Border(0.5f).Padding(2).AlignRight().Text(Convert.ToDecimal(salidasFiltradas[i]["Monto"]).ToString("N2")).FontSize(8);
                                }
                                else
                                {
                                    table.Cell().Border(0.5f).Text("");
                                    table.Cell().Border(0.5f).Text("");
                                }
                            }

                            // FILA DE SUBTOTAL Y 12% (SIEMPRE SE MUESTRA)
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("SUBTOTAL=").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).AlignRight().Text(subtotalEntradas.ToString("N2")).Bold().FontSize(8);
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("X 12%").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).AlignRight().Text(impuesto12.ToString("N2")).Bold().FontSize(8);

                            // FILAS INFERIORES - ENTRADAS ESPECIALES (SOLO SI HAY)
                            int salidasRestantesInicio = entradasPrincipales.Count;

                            for (int i = 0; i < entradasEspeciales.Count; i++)
                            {
                                // Entradas Especiales
                                table.Cell().Border(0.5f).Padding(2).Text(entradasEspeciales[i]["NombreCuenta"].ToString()).FontSize(8);
                                table.Cell().Border(0.5f).Padding(2).AlignRight().Text(Convert.ToDecimal(entradasEspeciales[i]["Monto"]).ToString("N2")).FontSize(8);

                                // Lado Derecho: "A LA CURIA ARZOBISPAL" solo en la primera fila de especiales
                                if (i == 0)
                                {
                                    table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("A LA CURIA ARZOBISPAL").Bold().FontSize(8);
                                    table.Cell().Border(0.5f).Text("");
                                }
                                else
                                {
                                    // Continuación de salidas si hay
                                    int idxSalida = salidasRestantesInicio + (i - 1);
                                    if (idxSalida < salidasFiltradas.Count)
                                    {
                                        table.Cell().Border(0.5f).Padding(2).Text(salidasFiltradas[idxSalida]["NombreCuenta"].ToString()).FontSize(8);
                                        table.Cell().Border(0.5f).Padding(2).AlignRight().Text(Convert.ToDecimal(salidasFiltradas[idxSalida]["Monto"]).ToString("N2")).FontSize(8);
                                    }
                                    else
                                    {
                                        table.Cell().Border(0.5f).Text("");
                                        table.Cell().Border(0.5f).Text("");
                                    }
                                }
                            }

                            // SALIDAS RESTANTES (si hay más salidas que entradas principales + especiales)
                            int totalFilasEntradas = entradasPrincipales.Count + entradasEspeciales.Count;
                            for (int i = totalFilasEntradas; i < salidasFiltradas.Count; i++)
                            {
                                // Espacio vacío en entradas
                                table.Cell().Border(0.5f).Text("");
                                table.Cell().Border(0.5f).Text("");

                                // Salidas restantes
                                table.Cell().Border(0.5f).Padding(2).Text(salidasFiltradas[i]["NombreCuenta"].ToString()).FontSize(8);
                                table.Cell().Border(0.5f).Padding(2).AlignRight().Text(Convert.ToDecimal(salidasFiltradas[i]["Monto"]).ToString("N2")).FontSize(8);
                            }

                            // TOTALES FINALES
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("TOTAL ENTRADAS =").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).AlignRight().Text(totalEntradasGeneral.ToString("N2")).Bold().FontSize(8);
                            table.Cell().Background("#D4AF37").Border(0.5f).Padding(2).Text("TOTAL SALIDAS =").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).AlignRight().Text(totalSalidas.ToString("N2")).Bold().FontSize(8);
                        });

                        // RESUMEN INFERIOR
                        col.Item().PaddingTop(15).Table(resTable =>
                        {
                            resTable.ColumnsDefinition(c => { c.RelativeColumn(); c.ConstantColumn(120); });
                            resTable.Cell().Border(0.5f).Padding(2).Text("Total entradas del mes").FontSize(9);
                            resTable.Cell().Border(0.5f).Padding(2).AlignRight().Text(totalEntradasGeneral.ToString("N2")).FontSize(9);
                            resTable.Cell().Border(0.5f).Padding(2).Text("Total salidas del mes").FontSize(9);
                            resTable.Cell().Border(0.5f).Padding(2).AlignRight().Text(totalSalidas.ToString("N2")).FontSize(9);
                            resTable.Cell().Border(0.5f).Padding(2).Text("Ganancias (+) o perdidas (-) del mes").FontSize(9).Bold();
                            resTable.Cell().Border(0.5f).Padding(2).AlignRight().Text((totalEntradasGeneral - totalSalidas).ToString("N2")).FontSize(9).Bold();
                        });

                        col.Item().PaddingTop(20).Row(row =>
                        {
                            row.RelativeItem().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(9);
                            row.RelativeItem().AlignRight().Text($"Sacerdote: {nombreSacerdote}").FontSize(9);
                        });
                    });
                });
            }).GeneratePdf();
        }
    }
}