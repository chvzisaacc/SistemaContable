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
    public class GastosService
    {
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly string _carpetaReportes;

        public GastosService()
        {
            _carpetaReportes = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ReportesGastos");

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



        public string GenerarInformeGastos(
            int parroquiaId,
            string nombreParroquia,
            DateTime desde,
            DateTime hasta,
            int usuarioId)
        {
            // 1. Traer datos
            DataTable datos = _repo.ObtenerGastosPorParroquia(parroquiaId, desde, hasta);

            // 2. Crear PDF en memoria
            byte[] pdfBytes = GenerarPdf(datos, nombreParroquia, desde, hasta);

            // 3. Guardar archivo
            string nombreArchivo = $"Gastos_{nombreParroquia}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.pdf";
            string rutaCompleta = Path.Combine(_carpetaReportes, nombreArchivo);

            File.WriteAllBytes(rutaCompleta, pdfBytes);

            // 4. Aquí podrías registrar en reportes_generados si luego lo necesitas

            return rutaCompleta;
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
                        col.Item().Text("INFORME DE GASTOS").FontSize(16).Bold();
                        col.Item().Text(parroquia);
                        col.Item().Text($"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}");
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(80);  // Fecha
                            cols.RelativeColumn();    // Cuenta / Descripción
                            cols.ConstantColumn(80);  // Debe
                            cols.ConstantColumn(80);  // Haber
                        });

                        table.Header(h =>
                        {
                            h.Cell().Text("Fecha").Bold();
                            h.Cell().Text("Cuenta / Movimiento").Bold();
                            h.Cell().Text("Debe").Bold();
                            h.Cell().Text("Haber").Bold();
                        });

                        foreach (DataRow row in datos.Rows)
                        {
                            DateTime fecha = Convert.ToDateTime(row["fecha_transaccion"]);
                            string cuenta = row["CuentaContable"]?.ToString();
                            string desc = row["DescripcionMovimiento"]?.ToString();

                            table.Cell().Text(fecha.ToString("dd/MM/yyyy"));
                            table.Cell().Text($"{cuenta}\n{desc}");
                            table.Cell().Text(string.Format("{0:N2}", row["Debe"]));
                            table.Cell().Text(string.Format("{0:N2}", row["Haber"]));
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

