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
            cmb_formato_descarga.Items.Clear();
            cmb_formato_descarga.Items.Add("PDF");
            cmb_formato_descarga.Items.Add("DOCX");
            cmb_formato_descarga.Items.Add("JPG");

            // Tipos de reporte desde la BD
            DataTable dtTipos = _gastosService.ObtenerTiposReporte();

            cmb_tipo_reporte.DataSource = dtTipos;
            cmb_tipo_reporte.DisplayMember = "descripcion";     // lo que ve el usuario (Estado..., Balance..., etc.)
            cmb_tipo_reporte.ValueMember = "TipoReporte_id";    // el int 1,2,3,4
            cmb_tipo_reporte.SelectedIndex = -1;                // ninguno seleccionado al inicio

            
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


            cmb_parroquia.DataSource = null;
            cmb_parroquia.Items.Clear();


            cmb_parroquia.DisplayMember = "Parroquia_nombre";
            cmb_parroquia.ValueMember = "Parroquia_ID";


            cmb_parroquia.DataSource = dt;


            cmb_parroquia.SelectedIndex = -1;
        }


        private void CargarReportes()
        {
            try
            {
                ClsAccionesDB db = new ClsAccionesDB();
                List<string> lista = db.ObtenerTipoReporte();

                cmb_tipo_reporte.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int tipoReporteId = Convert.ToInt32(cmb_tipo_reporte.SelectedValue);
            int parroquiaId = Convert.ToInt32(cmb_parroquia.SelectedValue);
            string parroquiaNombre = cmb_parroquia.Text;

            DateTime desde = dtp_desde.Value.Date;
            DateTime hasta = dtp_hasta.Value.Date;

            if (!Validaciones.ComboSeleccionado(cmb_parroquia))
            {
                MessageBox.Show("Seleccione una parroquia.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.ComboSeleccionado(cmb_tipo_reporte))
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.FechaRangoValido(dtp_desde.Value, dtp_hasta.Value))
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
                tipo_reporte_id = tipoReporteId,
                nombre_visible = nombreVisible,
                ruta_pdf = rutaPdf,
                parroquia_id = parroquiaId,
                desde = desde,
                hasta = hasta
            };

            lst_reportes.Items.Add(item);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = rutaPdf,
                UseShellExecute = true
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!Validaciones.ListBoxSeleccionado(lst_reportes))
            {
                MessageBox.Show("Seleccione un reporte generado de la lista.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.ComboSeleccionado(cmb_formato_descarga))
            {
                MessageBox.Show("Seleccione un formato de descarga.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = (ReporteUIItem)lst_reportes.SelectedItem;
            string formato = cmb_formato_descarga.SelectedItem.ToString(); // "PDF", "DOCX" o "JPG"

            if (!File.Exists(item.ruta_pdf))
            {
                MessageBox.Show("No se encontró el archivo del reporte en disco.");
                return;
            }

            string nombreSeguro = item.nombre_visible
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

