using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_PG49 : Form
    {

        private readonly GastosService _gastosService = new GastosService();
        private readonly EstadoResultadosService _estadoResultadosService = new EstadoResultadosService();
        private readonly IngresosService _ingresosService = new IngresosService();
        private readonly BalanceGeneralService _balanceGeneralService = new BalanceGeneralService();
        private ClsValidaciones Validaciones;
        public FRM_PG49()
        {
            InitializeComponent();
            Validaciones = new ClsValidaciones();
        }
        private string Construirnombre_reporteVisible(string tipo_texto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipo_texto} - {desde:MMMM-yyyy}";

            return $"{tipo_texto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FRM_PG49_Load(object sender, EventArgs e)
        {

            Sesion1.id_parroquia = 1;
            CargarReportes();
            cmbFormatoDescarga.Items.Clear();
            cmbFormatoDescarga.Items.Add("PDF");
            cmbFormatoDescarga.Items.Add("DOCX");
            cmbFormatoDescarga.Items.Add("JPG");

            DataTable dt_tipos = _gastosService.ObtenerTiposReporte();

            cmbTipoReporte.DataSource = dt_tipos;
            cmbTipoReporte.DisplayMember = "descripcion";
            cmbTipoReporte.ValueMember = "TipoReporte_id";
            cmbTipoReporte.SelectedIndex = -1;
        }

        private void CargarReportes()
        {
            try
            {
                ClsAccionesDB db = new ClsAccionesDB();
                List<string> lista = db.ObtenerTipoReporte();

                cmbTipoReporte.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int tipo_reporte_id = Convert.ToInt32(cmbTipoReporte.SelectedValue);
            int parroquia_id = Sesion1.id_parroquia;
            string parroquia_nombre = _gastosService.ObtenerNombreParroquia(parroquia_id);

            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;


            if (!Validaciones.ComboSeleccionado(cmbTipoReporte))
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoReporte.Focus();
                return;
            }


            if (!Validaciones.FechaRangoValido(desde, hasta))
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDesde.Focus();
                return;
            }





            string ruta_pdf = string.Empty;
            string nombre_reporte = string.Empty;

            switch (tipo_reporte_id)
            {
                case 1:
                    ruta_pdf = _estadoResultadosService.GenerarInformeEstadoResultados(
                              parroquia_id,
                              parroquia_nombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);

                    nombre_reporte = "Estado de Resultados";
                    break;

                case 2:
                    ruta_pdf = _balanceGeneralService.GenerarBalanceGeneral(
                              parroquia_id,
                              parroquia_nombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);

                    nombre_reporte = "Balance General";
                    break;
                case 3:
                    ruta_pdf = _ingresosService.GenerarReporteIngresos(
                              parroquia_id,
                              parroquia_nombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);
                    nombre_reporte = "Ingresos";
                    break;



                case 4: // Gastos
                    ruta_pdf = _gastosService.GenerarInformeGastos(
                    parroquia_id,
                    parroquia_nombre,
                    desde,
                    hasta,
                    Sesion1.usuario_id);

                    nombre_reporte = "Gastos";
                    break;

                default:
                    MessageBox.Show("Tipo de reporte no válido.");
                    return;
            }


            string nombre_visible = Construirnombre_reporteVisible(
                nombre_reporte,
                desde,
                hasta
            );

            var item = new ReporteUIItem
            {
                tipo_reporte_id = tipo_reporte_id,
                nombre_visible = nombre_visible,
                ruta_pdf = ruta_pdf,
                parroquia_id = parroquia_id,
                desde = desde,
                hasta = hasta
            };

            lstReportes.Items.Add(item);

            // Abrir automáticamente el PDF
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = ruta_pdf,
                UseShellExecute = true
            });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstReportes.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un reporte generado.");
                return;
            }

            if (cmbFormatoDescarga.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un formato de descarga (PDF, DOCX o JPG).");
                return;
            }

            var item = (ReporteUIItem)lstReportes.SelectedItem;
            string formato = cmbFormatoDescarga.SelectedItem.ToString();

            if (!File.Exists(item.ruta_pdf))
            {
                MessageBox.Show("No se encontró el archivo del reporte en disco.");
                return;
            }

            string nombre_seguro = item.nombre_visible
                .Replace("/", "-")
                .Replace("\\", "-")
                .Replace(":", "-")
                .Replace("*", "-")
                .Replace("?", "")
                .Replace("\"", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");

            using (var sfd = new SaveFileDialog())
            {
                switch (formato)
                {
                    case "PDF":
                        sfd.Filter = "Archivo PDF|*.pdf";
                        sfd.FileName = nombre_seguro + ".pdf";
                        break;

                    case "DOCX":
                        sfd.Filter = "Documento Word|*.docx";
                        sfd.FileName = nombre_seguro + ".docx";
                        break;

                    case "JPG":
                        sfd.Filter = "Imagen JPG|*.jpg";
                        sfd.FileName = nombre_seguro + ".jpg";
                        break;
                }

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                if (formato == "PDF")
                {
                    File.Copy(item.ruta_pdf, sfd.FileName, true);
                }
                else if (formato == "DOCX")
                {
                    PdfDocument pdf = new PdfDocument();
                    pdf.LoadFromFile(item.ruta_pdf);
                    pdf.SaveToFile(sfd.FileName, FileFormat.DOCX);
                    pdf.Close();
                }
                else if (formato == "JPG")
                {
                    PdfDocument pdf = new PdfDocument();
                    pdf.LoadFromFile(item.ruta_pdf);
                    var image = pdf.SaveAsImage(0);
                    image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    pdf.Close();
                }
            }
        }

        private void lstReportes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}