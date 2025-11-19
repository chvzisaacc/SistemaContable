using Capa_de_acceso_de_datos;
using Capa_de_procesamiento_de_datos;
using Capa_de_Presentación.CLASES;
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
        public FRM_PG49()
        {
            InitializeComponent();
        }
        private string ConstruirNombreReporteVisible(string tipoTexto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipoTexto} - {desde:MMMM-yyyy}";

            return $"{tipoTexto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
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
            CargarReportes();
            cmbFormatoDescarga.Items.Clear();
            cmbFormatoDescarga.Items.Add("PDF");
            cmbFormatoDescarga.Items.Add("DOCX");
            cmbFormatoDescarga.Items.Add("JPG");

            DataTable dtTipos = _gastosService.ObtenerTiposReporte();

            cmbTipoReporte.DataSource = dtTipos;
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
            if (cmbTipoReporte.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un tipo de reporte.");
                return;
            }

            int tipoReporteId = Convert.ToInt32(cmbTipoReporte.SelectedValue);

            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser mayor que la fecha 'Hasta'.");
                return;
            }

            int parroquiaId = Sesion1.IdParroquia;
            string parroquiaNombre = _gastosService.ObtenerNombreParroquia(parroquiaId);

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
                      Sesion1.UsuarioID);

            nombreReporte = "Estado de Resultados";
            break;

               /* case 2: // Balance General
            /* rutaPdf = _balanceGeneralService.GenerarInformeBalanceGeneral(
                parroquiaId,
                parroquiaNombre,
                desde,
                hasta,
                Sesion1.UsuarioID);

             nombreReporte = "BalanceGeneral";
             break;

        

            /* case 3: // Ingresos
                 rutaPdf = _ingresosService.GenerarInformeIngresos(
                     parroquiaId,
                     parroquiaNombre,
                     desde,
                     hasta,
                     Sesion1.UsuarioID);

                 nombreReporte = "Ingresos";
                 break;*/

            case 4: // BALANCE GENERAL
                rutaPdf = _gastosService.GenerarInformeGastos(
                parroquiaId,
                parroquiaNombre,
                desde,
                hasta,
                Sesion1.UsuarioID);

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

            // Abrir automáticamente el PDF
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = rutaPdf,
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
            string formato = cmbFormatoDescarga.SelectedItem.ToString(); // "PDF", "DOCX" o "JPG"

            if (!File.Exists(item.RutaPdf))
            {
                MessageBox.Show("No se encontró el archivo del reporte en disco.");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                switch (formato)
                {
                    case "PDF":
                        sfd.Filter = "Archivo PDF|*.pdf";
                        sfd.FileName = item.NombreVisible + ".pdf";
                        break;

                    case "DOCX":
                        sfd.Filter = "Documento Word|*.docx";
                        sfd.FileName = item.NombreVisible + ".docx";
                        break;

                    case "JPG":
                        sfd.Filter = "Imagen JPG|*.jpg";
                        sfd.FileName = item.NombreVisible + ".jpg";
                        break;
                }

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                if (formato == "PDF")
                {
                    File.Copy(item.RutaPdf, sfd.FileName, true);
                }
                else
                {
                    // Stub por si el ing pregunta qué pasaría:
                    MessageBox.Show($"La conversión a {formato} aún no está implementada.");
                }
            }
        }

        private void lstReportes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

