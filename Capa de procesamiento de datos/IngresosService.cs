using Capa_de_acceso_de_datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Capa_de_procesamiento_de_datos
{
    public class IngresosService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public IngresosService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesIngresos");

            Directory.CreateDirectory(_carpetaReportes);
        }

        public string GenerarReporteIngresos(
            int parroquiaId,
            string nombreParroquia,
            DateTime desde,
            DateTime hasta,
            int usuarioId)
        {
            // 1. Traer datos del SP de ingresos
            DataTable datos = _repo.ObtenerIngresosPorParroquia(parroquiaId, desde, hasta);

            // 2. Generar PDF
            byte[] pdfBytes = GenerarPdf(datos, nombreParroquia, desde, hasta);

            // 3. Guardar archivo
            string nombreArchivo = $"Ingresos_{nombreParroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);

            return rutaCompleta;
        }

        public byte[] GenerarPdf(DataTable datos, string parroquia, DateTime desde, DateTime hasta)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("REPORTE DE INGRESOS").FontSize(16).Bold();
                        col.Item().Text(parroquia);
                        col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}");
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1);    // Fecha
                            cols.RelativeColumn(2);    // Descripción
                            cols.RelativeColumn(1);    // Cuenta
                            cols.ConstantColumn(100);  // Monto
                        });

                        table.Header(h =>
                        {
                            h.Cell().Text("Fecha").Bold();
                            h.Cell().Text("Descripción").Bold();
                            h.Cell().Text("Cuenta").Bold();
                            h.Cell().Text("Monto").Bold();
                        });

                        // Variables para calcular el total
                        decimal totalIngresos = 0;

                        foreach (DataRow row in datos.Rows)
                        {
                            table.Cell().Text(Convert.ToDateTime(row["fecha_transaccion"]).ToString("dd/MM/yyyy"));
                            table.Cell().Text(row["descripcion"]?.ToString());
                            table.Cell().Text(row["NombreCuenta"]?.ToString());

                            decimal monto = Convert.ToDecimal(row["Monto"]);
                            table.Cell().Text(string.Format("{0:N2}", monto));

                            totalIngresos += monto;
                        }

                        // Fila de total
                        table.Cell().Text("").Bold();
                        table.Cell().Text("").Bold();
                        table.Cell().Text("TOTAL").Bold();
                        table.Cell().Text(string.Format("{0:N2}", totalIngresos)).Bold();
                    });

                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            return document.GeneratePdf();
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

    }
}

