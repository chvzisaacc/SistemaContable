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
<<<<<<< HEAD
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
=======
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
>>>>>>> Arreglado lo del capital inicial
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Column(col =>
                    {
<<<<<<< HEAD
                        col.Item().Text("Arquidiócesis de Tegucigalpa").FontSize(14).Bold().FontColor("#003399");
=======
                        col.Item().Text("Arquidiocesis de Tegucigalpa")
                            .FontSize(16).Bold().FontColor("#003399");

>>>>>>> Arreglado lo del capital inicial
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
<<<<<<< HEAD
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
=======
                                columns.RelativeColumn(3);   // Concepto entrada
                                columns.ConstantColumn(90);  // Monto entrada
                                columns.RelativeColumn(3);   // Concepto salida
                                columns.ConstantColumn(90);  // Monto salida
                            });

                            void Celda(string texto, bool negrita = false, string colorFondo = "#FFFFFF", bool alinearDerecha = false)
>>>>>>> Arreglado lo del capital inicial
                            {
                                // Entradas
                                table.Cell().Border(0.5f).Padding(2).Text(entradasPrincipales[i]["NombreCuenta"].ToString()).FontSize(8);
                                table.Cell().Border(0.5f).Padding(2).AlignRight().Text(Convert.ToDecimal(entradasPrincipales[i]["Monto"]).ToString("N2")).FontSize(8);

<<<<<<< HEAD
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
=======
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
>>>>>>> Arreglado lo del capital inicial
                        });

                        col.Item().PaddingTop(20).Row(row =>
                        {
                            row.RelativeItem().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(9);
                            row.RelativeItem().AlignRight().Text($"Sacerdote: {nombreSacerdote}").FontSize(9);
                        });
<<<<<<< HEAD
=======

                        col.Item().Text("");

                        col.Item().Text(
                            "Recordamos que DEBEN ENTREGAR A LA CURIA, EL DOCE PORCIENTO (12%) sobre todas las entradas " +
                            "de la Parroquias, Iglesias o Capillas, y es de carácter obligatorio y nadie queda exento de esta obligacion."
                        ).FontSize(8);
>>>>>>> Arreglado lo del capital inicial
                    });
                });
            }).GeneratePdf();
        }
    }
}