using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;
using System.Text;
using System.Threading.Tasks;


namespace Capa_de_procesamiento_de_datos
{
    public class EstadoResultadosService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public EstadoResultadosService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesEstadoResultados");

            Directory.CreateDirectory(_carpetaReportes);
        }

        public string ObtenerNombreParroquia(int parroquiaId)
        {
            return _repo.ObtenerNombreParroquia(parroquiaId);
        }

        public DataTable ObtenerTiposReporte()
        {
            return _repo.ObtenerTiposReporte();
        }

        public DataTable ObtenerParroquias()
        {
            return _repo.ObtenerParroquias();
        }




        public string GenerarInformeEstadoResultados(
     int parroquiaId,
     string nombreParroquia,
     DateTime desde,
     DateTime hasta,
     int usuarioId)
        {
            DataSet datos = _repo.ObtenerEstadoResultados(parroquiaId, desde, hasta);

            DataTable totales = datos.Tables[0];
            DataTable detalle = datos.Tables[1];

            byte[] pdfBytes = GenerarPdf(totales, detalle, nombreParroquia, desde, hasta);

            string nombreArchivo = $"EstadoResultados_{nombreParroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);

            return rutaCompleta;
        }




        public byte[] GenerarPdf(
    DataTable totales,
    DataTable detalle,
    string nombreParroquia,
    DateTime desde,
    DateTime hasta)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(25);

                    // ENCABEZADO
                    page.Header().Column(col =>
                    {
                        col.Item().Text("ESTADO DE RESULTADOS").FontSize(18).Bold();
                        col.Item().Text(nombreParroquia).FontSize(12);
                        col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}");
                    });

                    // CONTENIDO
                    page.Content().Column(col =>
                    {
                        // ----------------------------
                        // RESUMEN DE TOTALES
                        // ----------------------------
                        DataRow t = totales.Rows[0];

                        col.Item().PaddingBottom(10).Text("RESUMEN").Bold().FontSize(13);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn();
                                cols.ConstantColumn(120);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Text("Descripción").Bold();
                                h.Cell().Text("Monto").Bold().AlignRight();
                            });

                            table.Cell().Text("Total Ingresos");
                            table.Cell().Text($"{Convert.ToDecimal(t["totalIngresos"]):N2}").AlignRight();

                            table.Cell().Text("Total Gastos");
                            table.Cell().Text($"{Convert.ToDecimal(t["totalGastos"]):N2}").AlignRight();

                            table.Cell().Text("Resultado Final");
                            table.Cell().Text($"{Convert.ToDecimal(t["resultadoFinal"]):N2}").AlignRight();
                        });

                        col.Item().PaddingVertical(15).LineHorizontal(1);

                        // ----------------------------
                        // DETALLE DE MOVIMIENTOS
                        // ----------------------------
                        col.Item().Text("DETALLE DE TRANSACCIONES").Bold().FontSize(13);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(70);   // Tipo
                                cols.ConstantColumn(90);   // Fecha
                                cols.RelativeColumn();     // Cuenta + Detalle
                                cols.ConstantColumn(100);  // Monto
                            });

                            table.Header(h =>
                            {
                                h.Cell().Text("Tipo").Bold();
                                h.Cell().Text("Fecha").Bold();
                                h.Cell().Text("Cuenta / Detalle").Bold();
                                h.Cell().Text("Monto").Bold().AlignRight();
                            });

                            foreach (DataRow row in detalle.Rows)
                            {
                                string tipo = row["tipo"]?.ToString();
                                string fecha = Convert.ToDateTime(row["fecha"]).ToString("dd/MM/yyyy");
                                string cuenta = row["cuenta"]?.ToString();
                                string det = row["detalle"]?.ToString();
                                decimal monto = Convert.ToDecimal(row["monto"]);

                                table.Cell().Text(tipo);
                                table.Cell().Text(fecha);
                                table.Cell().Text($"{cuenta}\n{det}");
                                table.Cell().Text($"{monto:N2}").AlignRight();
                            }
                        });
                    });

                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            return document.GeneratePdf();
        }

    }
}
