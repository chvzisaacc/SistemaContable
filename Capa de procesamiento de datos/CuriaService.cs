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
        /// <summary>
        /// The repo
        /// </summary>
        private readonly ClsReportes _repo = new ClsReportes();
        /// <summary>
        /// The carpeta reportes
        /// </summary>
        private readonly string _carpetaReportes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CuriaService"/> class.
        /// </summary>
        public CuriaService()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseReportsFolder = Path.Combine(documentsPath, "Sistema Contable - Reportes");

            _carpetaReportes = Path.Combine(baseReportsFolder, "ReportesCuria");

            // Crea el directorio (recursivamente, si es necesario)
            Directory.CreateDirectory(_carpetaReportes);
        }

        // Genera el PDF y devuelve la ruta
        /// <summary>
        /// Generars the informe curia.
        /// </summary>
        /// <param name="parroquiaId">The parroquia identifier.</param>
        /// <param name="nombreParroquia">The nombre parroquia.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <param name="usuarioId">The usuario identifier.</param>
        /// <param name="nombreSacerdote">The nombre sacerdote.</param>
        /// <returns></returns>
        public string GenerarInformeCuria(int usuarioId, DateTime desde, DateTime hasta)
        {
            DataSet ds = _repo.ObtenerDatosCuriaPorUsuario(usuarioId, desde, hasta);

            DataTable dtInfo = ds.Tables[0];
            DataTable dtEntradas = ds.Tables[1];
            DataTable dtSalidas = ds.Tables[2];
            DataTable dtTotales = ds.Tables[3];

            string nombreParroquia = dtInfo.Rows[0]["ParroquiaNombre"]?.ToString() ?? "";
            string nombreSacerdote = dtInfo.Rows[0]["SacerdoteNombre"]?.ToString() ?? "";

            byte[] pdfBytes = GenerarPdfCuria(
                dtEntradas,
                dtSalidas,
                dtTotales,
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


        /// <summary>
        /// Obteners the monto por cuenta.
        /// </summary>
        /// <param name="tabla">The tabla.</param>
        /// <param name="nombreCuenta">The nombre cuenta.</param>
        /// <returns></returns>
        private decimal ObtenerMontoPorCuenta(DataTable tabla, string etiquetaPlantilla)
        {
            if (tabla == null) return 0;

            if (!MapaCuentas.TryGetValue(etiquetaPlantilla, out string nombreBD))
                return 0;

            string bdNorm = NormalizarTexto(nombreBD);
            decimal total = 0;

            foreach (DataRow row in tabla.Rows)
            {
                string cuentaBD = row["NombreCuenta"]?.ToString() ?? "";
                string cuentaNorm = NormalizarTexto(cuentaBD);

                if (cuentaNorm == bdNorm)
                    total += Convert.ToDecimal(row["Monto"]); 
            }

            return total;
        }

        // SUMA TODO LO QUE NO ESTÉ EN EL MAPEO (ENTRADAS) Y TENGA VALOR
        private decimal ObtenerMontoOtrosEntradas(DataTable dtEntradas)
        {
            if (dtEntradas == null) return 0;

            // Normalizados de TODO el mapeo (entradas/colectas/salidas)
            // (así "DEPOSITOS" queda fuera si no está mapeado, y suma en Otros)
            var setMapeadas = new HashSet<string>(
                MapaCuentas.Values.Select(v => NormalizarTexto(v))
            );

            decimal total = 0;

            foreach (DataRow row in dtEntradas.Rows)
            {
                string nombre = row["NombreCuenta"]?.ToString() ?? "";
                string norm = NormalizarTexto(nombre);
                decimal monto = Convert.ToDecimal(row["Monto"]);

                if (monto > 0 && !setMapeadas.Contains(norm))
                    total += monto;
            }

            return total;
        }

        // SUMA TODO LO QUE NO ESTÉ EN EL MAPEO (SALIDAS) Y TENGA VALOR
        private decimal ObtenerMontoOtrosSalidas(DataTable dtSalidas)
        {
            if (dtSalidas == null) return 0;

            var setMapeadas = new HashSet<string>(
                MapaCuentas.Values.Select(v => NormalizarTexto(v))
            );

            decimal total = 0;

            foreach (DataRow row in dtSalidas.Rows)
            {
                string nombre = row["NombreCuenta"]?.ToString() ?? "";
                string norm = NormalizarTexto(nombre);
                decimal monto = Convert.ToDecimal(row["Monto"]);

                if (monto > 0 && !setMapeadas.Contains(norm))
                    total += monto;
            }

            return total;
        }


        // Normalizador universal
        /// <summary>
        /// Normalizars the texto.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <returns></returns>
        private string NormalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return "";

            texto = texto.ToLowerInvariant();

            // quitar "(ingreso)" "(gasto)" etc.
            texto = texto.Replace("(ingreso)", "")
                         .Replace("(gasto)", "")
                         .Replace("(", "")
                         .Replace(")", "");

            // quitar acentos
            texto = texto
                .Replace("á", "a").Replace("é", "e").Replace("í", "i")
                .Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n");

            // quitar comas y unificar espacios
            texto = texto.Replace(",", " ")
                         .Replace("  ", " ")
                         .Trim();

            return texto;
        }

        /// <summary>
        /// Generars the PDF curia.
        /// </summary>
        /// <param name="dtEntradas">The dt entradas.</param>
        /// <param name="dtSalidas">The dt salidas.</param>
        /// <param name="dtTotales">The dt totales.</param>
        /// <param name="parroquia">The parroquia.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <param name="nombreSacerdote">The nombre sacerdote.</param>
        /// <returns></returns>
        private byte[] GenerarPdfCuria(
    DataTable dtEntradas,
    DataTable dtSalidas,
    DataTable dtTotales,
    string parroquia,
    DateTime desde,
    DateTime hasta,
    string nombreSacerdote
)
        {
            decimal subtotalEntradas = 0;
            decimal totalEntradas = 0;
            decimal totalSalidas = 0;
            decimal gananciaMes = 0;
            decimal docePorciento = 0;
            decimal totalALaCuria = 0;

            if (dtTotales != null && dtTotales.Rows.Count > 0)
            {
                var rowT = dtTotales.Rows[0];

                totalEntradas = rowT["TotalEntradas"] != DBNull.Value ? Convert.ToDecimal(rowT["TotalEntradas"]) : 0;
                totalSalidas = rowT["TotalSalidas"] != DBNull.Value ? Convert.ToDecimal(rowT["TotalSalidas"]) : 0;
                gananciaMes = rowT["GananciaMes"] != DBNull.Value ? Convert.ToDecimal(rowT["GananciaMes"]) : 0;
                docePorciento = rowT["DocePorciento"] != DBNull.Value ? Convert.ToDecimal(rowT["DocePorciento"]) : 0;

                subtotalEntradas = rowT["SubtotalEntradasCuria"] != DBNull.Value ? Convert.ToDecimal(rowT["SubtotalEntradasCuria"]) : 0;
                totalALaCuria = rowT["TotalALaCuriaArzobispal"] != DBNull.Value ? Convert.ToDecimal(rowT["TotalALaCuriaArzobispal"]) : 0;

            }

            var filasEntradas = new (string Etiqueta, string Cuenta)[]
            {
                ("Bautismos", "Bautismos"),
                ("Misas, Fiestas, Funerales", "Misas, Fiestas, Funerales"),
                ("Matrimonios", "Matrimonios"),
                ("Donativos, Alcancias,Bendiciones", "Donativos, Alcancias,Bendiciones"),
                ("Colectas ordinarias", "Colectas ordinarias"),
                ("Permisos, Certificaciones", "Permisos, Certificaciones"),
                ("Profesorados, capellanias", "Profesorados, capellanias"),
                ("Otros (explicar)Tienda Parroquial", "TIENDA PARROQUIAL"),
                ("Otros", "__OTROS_ENTRADAS__") // aquí juntamos las cuentas no mapeadas
            };


            var filasSalidas = new (string Etiqueta, string Cuenta)[]
            {
                ("Administracion - Oficina", "Administracion - Oficina"),
                ("Agua", "Agua"),
                ("Carro - Transporte", "Carro - Transporte"),
                ("Comida - Cocina", "Comida - Cocina"),
                ("Luz", "Luz"),
                ("Mantenimiento - Limpieza", "Mantenimiento - Limpieza"),
                ("Sueldos", "Sueldos"),
                ("IHSS + Medicinas", "IHSS + Medicinas"),
                ("Internet y Servicio de Cable tv", "Internet y Servicio de Cable tv")
            };

            var filasColectas = new (string Etiqueta, string Cuenta)[]
            {
                ("Colecta de Adviento y Sta. Infancia", "Colecta de Adviento y Sta. Infancia"),
                ("Colecta de Cuaresma", "Colecta de Cuaresma"),
                ("Colecta de Viernes Santo", "Colecta de Viernes Santo"),
                ("Colecta de San Pedro", "Colecta de San Pedro"),
                ("Colecta de Vocaciones", "Colecta de Vocaciones"),
                ("Colecta Domund", "Colecta Domund"),
                ("Colectas Extraordinarias (Medios)", "Colectas Extraordinarias (Medios)"),
                ("Donativos Seminario", "Donativos Seminario"),
                ("Dispensas", "Dispensas"),
                ("Confirmas", "Confirmas")
            };

            var filasSalidas2 = new (string Etiqueta, string Cuenta)[]
            {
                ("Telefono", "Telefono"),
                ("Ayuda (Donativos, Limosnas)", "Ayuda (Donativos, Limosnas)"),
                ("Culto", "Culto"),
                ("Pastoral - Formacion", "Pastoral - Formacion"),
                ("Muebles - Enseres", "Muebles - Enseres"),
                ("Remuneracion Sacerdotes", "Remuneracion Sacerdotes"),
                ("Papel sellado", "Papel sellado"),
                ("Impuestos", "Impuestos"),
                ("Otros (explicar)", "__OTROS_SALIDAS__") //no mapeadas en salidas
            };


            


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
                                columns.RelativeColumn(3); // entradas concepto
                                columns.ConstantColumn(70); // entradas monto
                                columns.RelativeColumn(3); // salidas concepto
                                columns.ConstantColumn(70); // salidas monto
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

                            // encabezados con fondo dorado
                            Celda("ENTRADAS PARA LA CURIA", true, "#D4AF37");
                            Celda("", true);
                            Celda("SALIDAS", true, "#D4AF37");
                            Celda("", true);

                            // ===== SECCIÓN 1: dibujar SOLO filas con monto, sin huecos =====
                            var entradasVisibles = new List<(string Etiqueta, decimal Monto)>();
                            foreach (var f in filasEntradas)
                            {
                                decimal monto = f.Cuenta == "__OTROS_ENTRADAS__"
                                    ? ObtenerMontoOtrosEntradas(dtEntradas)
                                    : ObtenerMontoPorCuenta(dtEntradas, f.Cuenta);

                                if (monto > 0)
                                    entradasVisibles.Add((f.Etiqueta == "__OTROS_ENTRADAS__" ? "Otros" : f.Etiqueta, monto));
                            }

                            var salidasVisibles = new List<(string Etiqueta, decimal Monto)>();
                            foreach (var f in filasSalidas)
                            {
                                decimal monto = ObtenerMontoPorCuenta(dtSalidas, f.Cuenta);
                                if (monto > 0)
                                    salidasVisibles.Add((f.Etiqueta, monto));
                            }

                            int filas1 = Math.Max(entradasVisibles.Count, salidasVisibles.Count);
                            for (int r = 0; r < filas1; r++)
                            {
                                string bg = (r % 2 == 0) ? "#FFFFFF" : "#F5F5F5";

                                if (r < entradasVisibles.Count)
                                {
                                    var e = entradasVisibles[r];
                                    Celda(e.Etiqueta, false, bg);
                                    Celda($"Lps {e.Monto:N2}", false, bg);
                                }
                                else { Celda("", false, bg); Celda("", false, bg); }

                                if (r < salidasVisibles.Count)
                                {
                                    var s = salidasVisibles[r];
                                    Celda(s.Etiqueta, false, bg);
                                    Celda($"Lps {s.Monto:N2}", false, bg);
                                }
                                else { Celda("", false, bg); Celda("", false, bg); }
                            }

                            // Subtotal / 12%
                            Celda("SUBTOTAL=", true, "#D4AF37");
                            Celda(subtotalEntradas == 0 ? "" : $"Lps {subtotalEntradas:N2}", true, "#D4AF37");
                            Celda("X 12%", true, "#D4AF37");
                            Celda(docePorciento == 0 ? "" : $"Lps {docePorciento:N2}", true, "#D4AF37");

                            // Curia
                            Celda("", false);
                            Celda("", false);
                            Celda("A LA CURIA ARZOBISPAL", true, "#D4AF37");
                            Celda(totalALaCuria == 0 ? "" : $"Lps {totalALaCuria:N2}", true, "#D4AF37");

                            // ===== SECCIÓN 2: Colectas vs Salidas2 (solo monto, sin huecos) =====
                            var colectasVisibles = new List<(string Etiqueta, decimal Monto)>();
                            foreach (var f in filasColectas)
                            {
                                decimal monto = f.Cuenta == "__OTROS_ENTRADAS__"
                                    ? ObtenerMontoOtrosEntradas(dtEntradas)
                                    : ObtenerMontoPorCuenta(dtEntradas, f.Cuenta);

                                if (monto > 0)
                                    colectasVisibles.Add((f.Etiqueta == "__OTROS_ENTRADAS__" ? "Otros" : f.Etiqueta, monto));
                            }

                            var salidas2Visibles = new List<(string Etiqueta, decimal Monto)>();
                            foreach (var f in filasSalidas2)
                            {
                                decimal monto = f.Cuenta == "__OTROS_SALIDAS__"
                                    ? ObtenerMontoOtrosSalidas(dtSalidas)
                                    : ObtenerMontoPorCuenta(dtSalidas, f.Cuenta);

                                if (monto > 0)
                                    salidas2Visibles.Add((f.Etiqueta == "__OTROS_SALIDAS__" ? "Otros" : f.Etiqueta, monto));
                            }

                            int filas2 = Math.Max(colectasVisibles.Count, salidas2Visibles.Count);
                            for (int r = 0; r < filas2; r++)
                            {
                                string bg = (r % 2 == 0) ? "#FFFFFF" : "#F5F5F5";

                                if (r < colectasVisibles.Count)
                                {
                                    var e = colectasVisibles[r];
                                    Celda(e.Etiqueta, false, bg);
                                    Celda($"Lps {e.Monto:N2}", false, bg);
                                }
                                else { Celda("", false, bg); Celda("", false, bg); }

                                if (r < salidas2Visibles.Count)
                                {
                                    var s = salidas2Visibles[r];
                                    Celda(s.Etiqueta, false, bg);
                                    Celda($"Lps {s.Monto:N2}", false, bg);
                                }
                                else { Celda("", false, bg); Celda("", false, bg); }
                            }

                            // Totales
                            Celda("TOTAL ENTRADAS =", true, "#D4AF37");
                            Celda(totalEntradas == 0 ? "" : $"Lps {totalEntradas:N2}", true, "#D4AF37");
                            Celda("TOTAL SALIDAS =", true, "#D4AF37");
                            Celda(totalSalidas == 0 ? "" : $"Lps {totalSalidas:N2}", true, "#D4AF37");
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
                            Celda2("Ganancias (+) o perdidas (-) del mes", gananciaMes == 0 ? "" : $"Lps {gananciaMes:N2}");
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

        private readonly Dictionary<string, string> MapaCuentas = new()
        {
            // ENTRADAS
            { "Bautismos", "BAUTISMOS" },
            { "Misas, Fiestas, Funerales", "MISAS Y FUNERALES" },
            { "Matrimonios", "MATRIMONIOS" },
            { "Donativos, Alcancias,Bendiciones", "DONATIVOS" },
            { "Permisos, Certificaciones", "PERMISOS Y CERTIFICACIONES" },
            { "Confirmas", "CONFIRMACIONES" },

            // COLECTAS
            { "Colecta de Cuaresma", "COLECTA ESPECIAL (CUARESMA)" },
            { "Colecta de Viernes Santo", "COLECTA ESPECIAL (VIERNES SANTO)" },
            { "Colecta de San Pedro", "COLECTA ESPECIAL (OBULO S.P.)" },
            { "Colecta de Vocaciones", "COLECTA ESPECIAL (VOCACIONES)" },
            { "Colecta Domund", "COLECTA ESPECIAL (DOMUNI)" },
            { "Colectas Extraordinarias (Medios)", "COLECTA ESPECIAL (SUYAS/MEDIOS)" },

            // SALIDAS
            { "Administracion - Oficina", "ADMINISTRACION OFICINA" },
            { "Agua", "AGUA" },
            { "Carro - Transporte", "CARRO Y TRANSPORTE" },
            { "Comida - Cocina", "COMIDA Y COCINA" },
            { "Luz", "LUZ" },
            { "Mantenimiento - Limpieza", "MANTENIMIENTO Y LIMPIEZA" },
            { "Sueldos", "SUELDOS EMPLEADOS" },
            { "IHSS + Medicinas", "IHSS - MEDICINAS" },
            { "Internet y Servicio de Cable tv", "INTERNET - CABLE TV" },
            { "Telefono", "TELEFONO" },
            { "Ayuda (Donativos, Limosnas)", "DONATIVOS Y AYUDAS" },
            { "Culto", "CULTO" },
            { "Pastoral - Formacion", "PASTORAL Y FORMACION" },
            { "Muebles - Enseres", "MUEBLES Y ENSERES" },
            { "Remuneracion Sacerdotes", "REMUNERACION" },
            { "Impuestos", "IMPUESTOS" },
        };
    }
}



