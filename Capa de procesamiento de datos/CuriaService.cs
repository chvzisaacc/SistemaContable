using Capa_de_acceso_de_datos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_procesamiento_de_datos
{
    public class CuriaService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public CuriaService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesCuria");

            Directory.CreateDirectory(_carpetaReportes);
        }

        // Genera el PDF y devuelve la ruta
        public string GenerarInformeCuria(
            int parroquiaId,
            string nombreParroquia,
            DateTime desde,
            DateTime hasta,
            int usuarioId,
            string nombreSacerdote
        )
        {
            DataSet ds = _repo.ObtenerDatosCuria(parroquiaId, desde, hasta);
            DataTable dtEntradas = ds.Tables[0];
            DataTable dtSalidas = ds.Tables[1];
            DataTable dtTotales = ds.Tables[2];

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

        private decimal ObtenerMontoPorCuenta(DataTable tabla, string nombreCuenta)
        {
            if (tabla == null || tabla.Rows.Count == 0)
                return 0;

            // Normalizar plantilla
            string clave = NormalizarTexto(nombreCuenta);

            decimal total = 0;

            foreach (DataRow row in tabla.Rows)
            {
                string cuentaBD = NormalizarTexto(row["NombreCuenta"]?.ToString() ?? "");

                // Si la cuenta BD contiene o empieza con la plantilla, cuenta
                if (cuentaBD.StartsWith(clave) || cuentaBD.Contains(clave))
                    total += Convert.ToDecimal(row["Monto"]);
            }

            return total;
        }

        // Normalizador universal
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
            decimal totalEntradas = 0;
            decimal totalSalidas = 0;
            decimal gananciaMes = 0;
            decimal docePorciento = 0;

            if (dtTotales != null && dtTotales.Rows.Count > 0)
            {
                var rowT = dtTotales.Rows[0];
                totalEntradas = rowT.Field<decimal?>("TotalEntradas") ?? 0;
                totalSalidas = rowT.Field<decimal?>("TotalSalidas") ?? 0;
                gananciaMes = rowT.Field<decimal?>("GananciaMes") ?? 0;
                docePorciento = rowT.Field<decimal?>("DocePorciento") ?? 0;
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
                            .Bold();

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"Parroquia {parroquia}, Tegucigalpa");
                            row.ConstantItem(120).AlignRight().Text($"Año: {desde:yyyy}");
                        });
                    });

                    page.Content().Column(col =>
                    {
                        // tabla principal con líneas tipo hoja de cálculo
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // entradas concepto
                                columns.ConstantColumn(70); // entradas monto
                                columns.RelativeColumn(3); // salidas concepto
                                columns.ConstantColumn(70); // salidas monto
                            });

                            void Celda(string texto, bool negrita = false)
                            {
                                var cell = table.Cell()
                                    .Border(0.5f)
                                    .Padding(2);

                                var t = cell.Text(texto ?? string.Empty)
                                    .FontSize(9);

                                if (negrita)
                                    t.Bold();
                            }

                            // encabezados
                            Celda("ENTRADAS PARA LA CURIA", true);
                            Celda("", true);
                            Celda("SALIDAS", true);
                            Celda("", true);

                            int max1 = Math.Max(filasEntradas.Length, filasSalidas.Length);

                            for (int i = 0; i < max1; i++)
                            {
                                if (i < filasEntradas.Length)
                                {
                                    var f = filasEntradas[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtEntradas, f.Cuenta);
                                    Celda(f.Etiqueta);
                                    Celda(monto == 0 ? "" : monto.ToString("N2"));
                                }
                                else
                                {
                                    Celda("");
                                    Celda("");
                                }

                                if (i < filasSalidas.Length)
                                {
                                    var f = filasSalidas[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtSalidas, f.Cuenta);
                                    Celda(f.Etiqueta);
                                    Celda(monto == 0 ? "" : monto.ToString("N2"));
                                }
                                else
                                {
                                    Celda("");
                                    Celda("");
                                }
                            }

                            // fila subtotal / 12%
                            Celda("SUBTOTAL=", true);
                            Celda(totalEntradas == 0 ? "" : totalEntradas.ToString("N2"), true);
                            Celda("X 12%", true);
                            Celda(docePorciento == 0 ? "" : docePorciento.ToString("N2"), true);

                            // fila título sección curia arzobispal
                            Celda("", false);
                            Celda("", false);
                            Celda("A LA CURIA ARZOBISPAL", true);
                            Celda("", false);

                            int max2 = Math.Max(filasColectas.Length, filasSalidas2.Length);

                            for (int i = 0; i < max2; i++)
                            {
                                if (i < filasColectas.Length)
                                {
                                    var f = filasColectas[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtEntradas, f.Cuenta);
                                    Celda(f.Etiqueta);
                                    Celda(monto == 0 ? "" : monto.ToString("N2"));
                                }
                                else
                                {
                                    Celda("");
                                    Celda("");
                                }

                                if (i < filasSalidas2.Length)
                                {
                                    var f = filasSalidas2[i];
                                    decimal monto = ObtenerMontoPorCuenta(dtSalidas, f.Cuenta);
                                    Celda(f.Etiqueta);
                                    Celda(monto == 0 ? "" : monto.ToString("N2"));
                                }
                                else
                                {
                                    Celda("");
                                    Celda("");
                                }
                            }

                            // fila total final
                            Celda("TOTAL ENTRADAS =", true);
                            Celda(totalEntradas == 0 ? "" : totalEntradas.ToString("N2"), true);
                            Celda("TOTAL SALIDAS =", true);
                            Celda(totalSalidas == 0 ? "" : totalSalidas.ToString("N2"), true);
                        });

                        // resumen inferior
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
                            Celda2("Ganancias (+) o perdidad (-) del mes", gananciaMes.ToString("N2"));
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

                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            return document.GeneratePdf();
        }
    }
}