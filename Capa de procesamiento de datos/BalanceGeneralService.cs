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
    public class BalanceGeneralService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public BalanceGeneralService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesBalanceGeneral");

            Directory.CreateDirectory(_carpetaReportes);
        }

        public string GenerarBalanceGeneral(
            int parroquia_id,
            string nombre_parroquia,
            DateTime desde,
            DateTime hasta,
            int usuario_id)
        {
            // 1. Traer datos del SP
            DataTable datos = _repo.ObtenerBalanceGeneral(parroquia_id, desde, hasta);

            // 2. Generar PDF
            byte[] pdfBytes = GenerarPdf(datos, nombre_parroquia, desde, hasta);

            // 3. Guardar archivo
            string nombre_archivo = $"BalanceGeneral_{nombre_parroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string ruta_completa = Path.Combine(_carpetaReportes, nombre_archivo);

            File.WriteAllBytes(ruta_completa, pdfBytes);

            return ruta_completa;
        }

        private byte[] GenerarPdf(DataTable datos, string parroquia, DateTime desde, DateTime hasta)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("BALANCE GENERAL").FontSize(16).Bold();
                        col.Item().Text(parroquia);
                        col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}");
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn();    // Cuenta
                            cols.RelativeColumn();    // Detalle
                            cols.ConstantColumn(100); // Saldo
                        });

                        table.Header(h =>
                        {
                            h.Cell().Text("Cuenta").Bold();
                            h.Cell().Text("Detalle").Bold();
                            h.Cell().Text("Saldo").Bold();
                        });

                        foreach (DataRow row in datos.Rows)
                        {
                            table.Cell().Text(row["Cuenta"]?.ToString());
                            table.Cell().Text(row["Detalle"]?.ToString());
                            table.Cell().Text(string.Format("{0:N2}", row["Saldo"]));
                        }
                    });

                    page.Footer().AlignRight()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            return document.GeneratePdf();
        }

    }
}
