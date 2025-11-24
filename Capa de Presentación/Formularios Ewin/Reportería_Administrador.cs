using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using Stimulsoft.Report;
using Stimulsoft.Report.Viewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Pdf;
using System.Drawing.Imaging;

namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class Reportería_Administrador : Form
    {

        private readonly GastosService _gastosService = new GastosService();
        private readonly EstadoResultadosService _estadoResultadosService = new EstadoResultadosService();
        private readonly IngresosService _ingresosService = new IngresosService();
        private readonly BalanceGeneralService _balanceGeneralService = new BalanceGeneralService();
        private ClsValidaciones Validaciones;


        private string ConstruirNombreReporteVisible(string tipoTexto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipoTexto} - {desde:MMMM-yyyy}";

            return $"{tipoTexto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
        }

        public Reportería_Administrador()
        {
            InitializeComponent();
            Validaciones = new ClsValidaciones();
        }

        private void FRM_PG10_Load(object sender, EventArgs e)
        {
            CargarParroquias();
            CargarReportes();
            cmbFormatoDescarga.Items.Clear();
            cmbFormatoDescarga.Items.Add("PDF");
            cmbFormatoDescarga.Items.Add("DOCX");
            cmbFormatoDescarga.Items.Add("JPG");

            // Tipos de reporte desde la BD
            DataTable dtTipos = _gastosService.ObtenerTiposReporte();

            cmbTipoReporte.DataSource = dtTipos;
            cmbTipoReporte.DisplayMember = "descripcion";     // lo que ve el usuario (Estado..., Balance..., etc.)
            cmbTipoReporte.ValueMember = "TipoReporte_id";    // el int 1,2,3,4
            cmbTipoReporte.SelectedIndex = -1;                // ninguno seleccionado al inicio

            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CargarParroquias()
        {

            DataTable dt = _gastosService.ObtenerParroquias();


            cmbParroquia.DataSource = null;
            cmbParroquia.Items.Clear();


            cmbParroquia.DisplayMember = "Parroquia_nombre";
            cmbParroquia.ValueMember = "Parroquia_ID";


            cmbParroquia.DataSource = dt;


            cmbParroquia.SelectedIndex = -1;
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

            int tipoReporteId = Convert.ToInt32(cmbTipoReporte.SelectedValue);
            int parroquiaId = Convert.ToInt32(cmbParroquia.SelectedValue);
            string parroquiaNombre = cmbParroquia.Text;

            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            if (!Validaciones.ComboSeleccionado(cmbParroquia))
            {
                MessageBox.Show("Seleccione una parroquia.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.ComboSeleccionado(cmbTipoReporte))
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.FechaRangoValido(dtpDesde.Value, dtpHasta.Value))
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }




            string rutaPdf = string.Empty;
            string nombreReporte = string.Empty;

            switch (tipoReporteId)
            {
                case 1:
                    rutaPdf = _estadoResultadosService.GenerarInformeEstadoResultados(
                              parroquiaId,
                              parroquiaNombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);

                    nombreReporte = "Estado de Resultados";
                    break;
                case 2:
                    rutaPdf = _balanceGeneralService.GenerarBalanceGeneral(
                              parroquiaId,
                              parroquiaNombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);

                    nombreReporte = "Balance General";
                    break;
                case 3:
                    rutaPdf = _ingresosService.GenerarReporteIngresos(
                              parroquiaId,
                              parroquiaNombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);
                    nombreReporte = "Ingresos";
                    break;
                case 4:
                    rutaPdf = _gastosService.GenerarInformeGastos(
                              parroquiaId,
                              parroquiaNombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);

                    nombreReporte = "Gastos";
                    break;

                default:
                    MessageBox.Show("Tipo de reporte no válido.");
                    return;
            }

            string nombreVisible = ConstruirNombreReporteVisible(
                nombreReporte,
                desde,
                hasta
            );

            var item = new ReporteUIItem
            {
                TipoReporteId = tipoReporteId,
                NombreVisible = nombreVisible,
                RutaPdf = rutaPdf,
                ParroquiaId = parroquiaId,
                Desde = desde,
                Hasta = hasta
            };

            lstReportes.Items.Add(item);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = rutaPdf,
                UseShellExecute = true
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!Validaciones.ListBoxSeleccionado(lstReportes))
            {
                MessageBox.Show("Seleccione un reporte generado de la lista.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.ComboSeleccionado(cmbFormatoDescarga))
            {
                MessageBox.Show("Seleccione un formato de descarga.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = (ReporteUIItem)lstReportes.SelectedItem;
            string formato = cmbFormatoDescarga.SelectedItem.ToString(); // "PDF", "DOCX" o "JPG"

            if (!File.Exists(item.RutaPdf))
            {
                MessageBox.Show("No se encontró el archivo del reporte en disco.");
                return;
            }

            string nombreSeguro = item.NombreVisible
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
                        sfd.FileName = nombreSeguro + ".pdf";
                        break;

                    case "DOCX":
                        sfd.Filter = "Documento Word|*.docx";
                        sfd.FileName = nombreSeguro + ".docx";
                        break;

                    case "JPG":
                        sfd.Filter = "Imagen JPG|*.jpg";
                        sfd.FileName = nombreSeguro + ".jpg";
                        break;
                }

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                if (formato == "PDF")
                {
                    File.Copy(item.RutaPdf, sfd.FileName, true);
                }
                else if (formato == "DOCX")
                {
                    PdfDocument pdf = new PdfDocument();
                    pdf.LoadFromFile(item.RutaPdf);
                    pdf.SaveToFile(sfd.FileName, FileFormat.DOCX);
                    pdf.Close();
                }
                else if (formato == "JPG")
                {
                    PdfDocument pdf = new PdfDocument();
                    pdf.LoadFromFile(item.RutaPdf);
                    var image = pdf.SaveAsImage(0);
                    image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    pdf.Close();
                }
                else
                {
                    // Stub por si el ing pregunta qué pasaría:
                    //MessageBox.Show($"La conversión a {formato} aún no está implementada.");
                }
            }
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTipoReporte_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cmbFormatoDescarga_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

