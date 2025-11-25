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
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id)
        {
            // 1. Traer datos del SP de ingresos
            DataTable datos = _repo.ObtenerIngresosPorParroquia(parroquia_id, desde, hasta);

            // 2. Generar PDF
            byte[] pdfBytes = GenerarPdf(datos, nombre_parroquia, desde, hasta);

            // 3. Guardar archivo
            string nombre_archivo = $"Ingresos_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);

            File.WriteAllBytes(ruta_completa, pdfBytes);

            return ruta_completa;
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
                        decimal total_ingresos = 0;

                        foreach (DataRow row in datos.Rows)
                        {
                            table.Cell().Text(Convert.ToDateTime(row["fecha_transaccion"]).ToString("dd/MM/yyyy"));
                            table.Cell().Text(row["descripcion"]?.ToString());
                            table.Cell().Text(row["NombreCuenta"]?.ToString());

                            decimal monto = Convert.ToDecimal(row["Monto"]);
                            table.Cell().Text(string.Format("{0:N2}", monto));

                            total_ingresos += monto;
                        }

                        // Fila de total
                        table.Cell().Text("").Bold();
                        table.Cell().Text("").Bold();
                        table.Cell().Text("TOTAL").Bold();
                        table.Cell().Text(string.Format("{0:N2}", total_ingresos)).Bold();
                    });

                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            return document.GeneratePdf();
        }

        public string ObtenerNombreParroquia(int parroquia_id)
        {
            return _repo.ObtenerNombreParroquia(parroquia_id);
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

