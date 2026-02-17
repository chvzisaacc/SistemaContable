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
                    total += Convert.ToDecimal(row["Monto"]); // <- asegurate que el SP lo llame Monto
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
        ("Otros (explicar)Tienda Parroquial", "Otros (explicar)Tienda Parroquial"),
        ("Otros", "Otros")
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
        ("Otros (explicar)", "Otros (explicar)")
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

                            int max1 = Math.Max(filasEntradas.Length, filasSalidas.Length);

                            for (int i = 0; i < max1; i++)
                            {
                                if (i < filasEntradas.Length)
                                {
                                    var f = filasEntradas[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtEntradas, f.Cuenta);
                                    Celda(f.Etiqueta, false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                    Celda(monto == 0 ? "" : monto.ToString("N2"), false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                }
                                else
                                {
                                    Celda("", false);
                                    Celda("", false);
                                }

                                if (i < filasSalidas.Length)
                                {
                                    var f = filasSalidas[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtSalidas, f.Cuenta);
                                    Celda(f.Etiqueta, false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                    Celda(monto == 0 ? "" : monto.ToString("N2"), false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                }
                                else
                                {
                                    Celda("", false);
                                    Celda("", false);
                                }
                            }

                            // fila subtotal / 12%
                            Celda("SUBTOTAL=", true, "#D4AF37");
                            Celda(subtotalEntradas == 0 ? "" : subtotalEntradas.ToString("N2"), true, "#D4AF37");
                            Celda("X 12%", true, "#D4AF37");
                            Celda(docePorciento == 0 ? "" : docePorciento.ToString("N2"), true, "#D4AF37");

                            // fila título sección curia arzobispal
                            Celda("", false);
                            Celda("", false);
                            Celda("A LA CURIA ARZOBISPAL", true, "#D4AF37");
                            Celda(totalALaCuria.ToString("N2"), true, "#D4AF37");

                            int max2 = Math.Max(filasColectas.Length, filasSalidas2.Length);

                            for (int i = 0; i < max2; i++)
                            {
                                if (i < filasColectas.Length)
                                {
                                    var f = filasColectas[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtEntradas, f.Cuenta);
                                    Celda(f.Etiqueta, false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                    Celda(monto == 0 ? "" : monto.ToString("N2"), false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                }
                                else
                                {
                                    Celda("", false);
                                    Celda("", false);
                                }

                                if (i < filasSalidas2.Length)
                                {
                                    var f = filasSalidas2[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtSalidas, f.Cuenta);
                                    Celda(f.Etiqueta, false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                    Celda(monto == 0 ? "" : monto.ToString("N2"), false, (i % 2 == 0) ? "#FFFFFF" : "#F5F5F5");
                                }
                                else
                                {
                                    Celda("", false);
                                    Celda("", false);
                                }
                            }

                            // fila total final
                            Celda("TOTAL ENTRADAS =", true, "#D4AF37");
                            Celda(totalEntradas == 0 ? "" : totalEntradas.ToString("N2"), true, "#D4AF37");
                            Celda("TOTAL SALIDAS =", true, "#D4AF37");
                            Celda(totalSalidas == 0 ? "" : totalSalidas.ToString("N2"), true, "#D4AF37");
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
                                table.Cell().Border(0.5f).Padding(2)
                                    .Text(texto).FontSize(9);
                                table.Cell().Border(0.5f).Padding(2)
                                    .Text(valor).FontSize(9);
                            }

                            Celda2("Total entradas del mes", totalEntradas.ToString("N2"));
                            Celda2("Total salidas del mes", totalSalidas.ToString("N2"));
                            Celda2("Ganancias (+) o perdidas (-) del mes", gananciaMes.ToString("N2"));
                        });

                        col.Item().Text("");

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}")
                                .FontSize(9);
                            row.RelativeItem().AlignRight()
                                .Text($"Sacerdote: {nombreSacerdote}")
                                .FontSize(9);
                        });

                        col.Item().Text("");

                        col.Item().Text(
                            @"Recordamos que DEBEN ENTREGAR A LA CURIA, EL DOCE PORCIENTO (12%) sobre todas las entradas de la Parroquias, Iglesias o Capillas, y es de carácter obligatorio y nadie queda exento de esta obligacion.")
                            .FontSize(8);
                    });

                    page.Footer()
            .Height(30)
            .AlignCenter()
            .Column(col =>
            {
                col.Item()
                    .LineHorizontal(1)
                    .LineColor("#D4AF37");

                col.Item()
                    .Text(text =>
                    {
                        text.Span("Generado el ")
                            .FontSize(9)
                            .FontColor("#666666");

                        text.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(9)
                            .FontColor("#666666");

                        text.Span("  |  Página ")
                            .FontSize(9)
                            .FontColor("#666666");

                        text.CurrentPageNumber()
                            .FontSize(9)
                            .FontColor("#666666");

                        text.Span(" de ")
                            .FontSize(9)
                            .FontColor("#666666");

                        text.TotalPages()
                            .FontSize(9)
                            .FontColor("#666666");

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

    // COLECTAS (ya las tenés en catálogo con nombre)
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

    // EL 12% NO ES CUENTA: sale del total entradas * 0.12 o de "DIEZMO CURIA" si lo registrás como gasto aparte
};

    }

}