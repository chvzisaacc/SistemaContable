using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using Spire.Pdf;
using System.Data;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Reportería_Administrador : Form
    {

        /// <summary>
        /// The gastos service
        /// </summary>
        private readonly GastosService _gastosService = new GastosService();
        /// <summary>
        /// The estado resultados service
        /// </summary>
        private readonly EstadoResultadosService _estadoResultadosService = new EstadoResultadosService();
        /// <summary>
        /// The ingresos service
        /// </summary>
        private readonly IngresosService _ingresosService = new IngresosService();
        /// <summary>
        /// The balance general service
        /// </summary>
        private readonly BalanceGeneralService _balanceGeneralService = new BalanceGeneralService();
        /// <summary>
        /// The validaciones
        /// </summary>
        private ClsValidaciones Validaciones;
        /// <summary>
        /// The curia service
        /// </summary>
        private readonly CuriaService _curiaService = new CuriaService();
        /// <summary>
        /// The repo
        /// </summary>
        private readonly ClsReportes _repo = new ClsReportes();


        /// <summary>
        /// Construirs the nombre reporte visible.
        /// </summary>
        /// <param name="tipoTexto">The tipo texto.</param>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        private string ConstruirNombreReporteVisible(string tipoTexto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipoTexto} - {desde:MMMM-yyyy}";

            return $"{tipoTexto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reportería_Administrador"/> class.
        /// </summary>
        public Reportería_Administrador()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
        }

        /// <summary>
        /// Handles the Load event of the FRM_PG10 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG10_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            //cargar los nombres de los reportes
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

        /// <summary>
        /// Handles the Click event of the label4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Cargars the parroquias.
        /// </summary>
        private void CargarParroquias()
        {
            //metodo para obtener de la bd las parroquias
            DataTable dt = _gastosService.ObtenerParroquias();


            cmb_parroquia.DataSource = null;
            cmb_parroquia.Items.Clear();


            cmb_parroquia.DisplayMember = "Parroquia_nombre";
            cmb_parroquia.ValueMember = "Parroquia_ID";


            cmb_parroquia.DataSource = dt;


            cmb_parroquia.SelectedIndex = -1;
        }


        /// <summary>
        /// Cargars the reportes.
        /// </summary>
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

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            //metodos para jalar de la base de datos 
            int tipo_reporte_id = Convert.ToInt32(cmb_tipo_reporte.SelectedValue);
            int parroquia_id = Convert.ToInt32(cmb_parroquia.SelectedValue);
            string parroquia_nombre = cmb_parroquia.Text;

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



            //metodos para crear los pdf 
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
                case 4:
                    ruta_pdf = _gastosService.GenerarInformeGastos(
                              parroquia_id,
                              parroquia_nombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);

                    nombre_reporte = "Gastos";
                    break;

                case 5:
                    // Obtener el nombre del sacerdote desde la base de datos
                    string nombreSacerdote = _repo.ObtenerNombreSacerdote(Sesion1.usuario_id);

                    ruta_pdf = _curiaService.GenerarInformeCuria(
                                  parroquia_id,
                                  parroquia_nombre,
                                  desde,
                                  hasta,
                                  Sesion1.usuario_id,
                                  nombreSacerdote);

                    nombre_reporte = "Informe de Curia";
                    break;

                default:
                    MessageBox.Show("Tipo de reporte no válido.");
                    return;
            }

            string nombreVisible = ConstruirNombreReporteVisible(
                nombre_reporte,
                desde,
                hasta
            );

            var item = new ReporteUIItem
            {
                //metodo que genera oo que llevara el reporte
                tipo_reporte_id = tipo_reporte_id,
                nombre_visible = nombreVisible,
                ruta_pdf = ruta_pdf,
                parroquia_id = parroquia_id,
                desde = desde,
                hasta = hasta
            };

            lst_reportes.Items.Add(item);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = ruta_pdf,
                UseShellExecute = true
            });
        }


        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbTipoReporte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbParroquia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the cmbTipoReporte_SelectedIndexChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTipoReporte_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbFormatoDescarga control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbFormatoDescarga_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

