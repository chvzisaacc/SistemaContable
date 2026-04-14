using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using Spire.Pdf;
using System.Data;
using ClosedXML.Excel;

namespace Capa_de_Presentación.Formularios_Luiss
{
    /// <summary>
    /// Formulario de reportería que permite generar, listar y exportar informes (PDF, DOCX, JPG, XLSX).
    /// Contiene llamadas a servicios de generación de reportes y utilidades de exportación.
    /// Notas de sincronización: las operaciones que actualizan UI deben ejecutarse en el hilo UI;
    /// las operaciones costosas (generación o guardado de archivos) pueden ejecutarse en background y luego sincronizar resultados.
    /// </summary>
    public partial class FRM_PG49 : Form
    {
        /// <summary>
        /// Servicio para generar libro mayor.
        /// </summary>
        private readonly LibroMayorService _libroMayorService = new LibroMayorService();

        /// <summary>
        /// Servicio para generar gastos.
        /// </summary>
        private readonly GastosService _gastosService = new GastosService();

        /// <summary>
        /// Servicio para estado de resultados.
        /// </summary>
        private readonly EstadoResultadosService _estadoResultadosService = new EstadoResultadosService();

        /// <summary>
        /// Servicio para ingresos.
        /// </summary>
        private readonly IngresosService _ingresosService = new IngresosService();

        /// <summary>
        /// Servicio para datos de Curia.
        /// </summary>
        private readonly CuriaService _curiaService = new CuriaService();

        /// <summary>
        /// Repositorio de reportes para consultas de tablas/datos.
        /// </summary>
        private readonly ClsReportes _repo = new ClsReportes();

        /// <summary>
        /// Utilidades de validación para controles del formulario.
        /// </summary>
        private ClsValidaciones Validaciones;

        /// <summary>
        /// Constructor: inicializa componentes y utilidades.
        /// Las inicializaciones afectan controles UI y se asumen en el hilo de interfaz (UI thread).
        /// </summary>
        public FRM_PG49()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Validaciones = new ClsValidaciones();
        }

        /// <summary>
        /// Construye un nombre visible para el reporte según el rango de fechas.
        /// Devuelve un formato por mes/año o por rango de fechas.
        /// </summary>
        /// <param name="tipo_texto">Texto descriptivo del tipo de reporte.</param>
        /// <param name="desde">Fecha de inicio.</param>
        /// <param name="hasta">Fecha de fin.</param>
        /// <returns>Nombre legible del reporte para mostrar al usuario.</returns>
        private string Construirnombre_reporteVisible(string tipo_texto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipo_texto} - {desde:MMMM-yyyy}";

            return $"{tipo_texto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
        }

        /// <summary>
        /// Cierra el formulario al hacer click en la etiqueta correspondiente.
        /// Acción realizada en el hilo UI.
        /// </summary>
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Load del formulario: inicializa combos y límites de fecha.
        /// Nota de sincronización: las asignaciones a controles se realizan en el hilo UI.
        /// </summary>
        private void FRM_PG49_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            //CargarReportes();
            cmbFormatoDescarga.Items.Clear();
            cmbFormatoDescarga.Items.Add("PDF");
            cmbFormatoDescarga.Items.Add("DOCX");
            cmbFormatoDescarga.Items.Add("JPG");
            cmbFormatoDescarga.Items.Add("XLSX");

            DataTable dt_tipos = _gastosService.ObtenerTiposReporte();

            cmbTipoReporte.DataSource = dt_tipos;
            cmbTipoReporte.DisplayMember = "descripcion";
            cmbTipoReporte.ValueMember = "TipoReporte_id";
            cmbTipoReporte.SelectedIndex = -1;
            this.CenterToScreen();

            dtpDesde.MaxDate = DateTime.Now;
            dtpHasta.MaxDate = DateTime.Now;
        }

        /// <summary>
        /// Genera el reporte seleccionado (según tipo) y lo agrega a la lista de reportes generados.
        /// Valida controles y muestra el PDF resultante usando el proceso del sistema.
        /// Consideración: la generación puede ser costosa; si se ejecuta en background, sincronizar la adición a la lista con Invoke.
        /// </summary>
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
                    ruta_pdf = _ingresosService.GenerarReporteIngresos(
                              parroquia_id,
                              parroquia_nombre,
                              desde,
                              hasta,
                              Sesion1.usuario_id);
                    nombre_reporte = "Ingresos";
                    break;

                case 3:
                    ruta_pdf = _gastosService.GenerarInformeGastos(
                    parroquia_id,
                    parroquia_nombre,
                    desde,
                    hasta,
                    Sesion1.usuario_id);

                    nombre_reporte = "Gastos";
                    break;

                case 4:
                    ruta_pdf = _curiaService.GenerarInformeCuria(
                        Sesion1.usuario_id,
                        desde,
                        hasta
                    );

                    nombre_reporte = "Informe de Curia";
                    break;

                case 5:
                    ruta_pdf = _libroMayorService.GenerarInformeLibroMayor(
                               parroquia_id,
                               parroquia_nombre,
                               desde,
                               hasta
                               );

                    nombre_reporte = "Libro mayor";
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
                parroquia_nombre = parroquia_nombre,
                desde = desde,
                hasta = hasta
            };

            lstReportes.Items.Add(item);

            // Abrir el PDF generado con la aplicación por defecto del sistema
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = ruta_pdf,
                UseShellExecute = true
            });
        }

        /// <summary>
        /// Exporta o guarda el reporte seleccionado en el formato elegido (PDF, DOCX, JPG, XLSX).
        /// Valida selección y realiza operaciones de I/O; las operaciones de guardado bloquean el hilo actual,
        /// por lo que si se desea evitar bloquear la UI ejecutar la exportación en un hilo background.
        /// </summary>
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
                    case "XLSX":
                        sfd.Filter = "Archivo Excel|*.xlsx";
                        sfd.FileName = nombre_seguro + ".xlsx";
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
                else if (formato == "XLSX")
                {
                    DataTable dt = ObtenerDatosReporte(item);

                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Reporte");
                        ws.Cell(1, 1).InsertTable(dt);

                        // Aplicar formato de moneda a columnas numéricas
                        for (int c = 1; c <= dt.Columns.Count; c++)
                        {
                            string colName = dt.Columns[c - 1].ColumnName.ToLower();
                            if (colName.Contains("monto") || colName.Contains("debe") ||
                                colName.Contains("haber") || colName.Contains("saldo") ||
                                colName.Contains("total") || colName.Contains("ingreso") ||
                                colName.Contains("gasto"))
                            {
                                // Filas de datos (fila 2 en adelante, fila 1 es encabezado)
                                var rango = ws.Column(c).Cells(2, dt.Rows.Count + 1);
                                foreach (var cell in rango)
                                {
                                    if (decimal.TryParse(cell.Value.ToString(), out decimal valor))
                                    {
                                        cell.Value = valor;
                                        cell.Style.NumberFormat.Format = "\"L.\"#,##0.00";
                                    }
                                }
                            }
                        }

                        ws.Columns().AdjustToContents();
                        wb.SaveAs(sfd.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un DataTable listo para exportación según el tipo de reporte.
        /// Realiza limpieza de columnas no deseadas antes de devolver los datos.
        /// Nota: las consultas a la capa de datos deben ejecutarse en background si son costosas y luego devolver el DataTable al UI.
        /// </summary>
        /// <param name="item">Elemento UI con metadatos del reporte.</param>
        /// <returns>DataTable con los datos a exportar.</returns>
        private DataTable ObtenerDatosReporte(ReporteUIItem item)
        {
            switch (item.tipo_reporte_id)
            {
                case 1:
                    DataTable dtEstado = _repo.ObtenerEstadoResultados(item.parroquia_id, item.desde, item.hasta).Tables[1];
                    foreach (string col in new[] { "Parroquia_nombre", "id_transaccion", "usuario_nombre", "usuario_apellido" })
                        if (dtEstado.Columns.Contains(col)) dtEstado.Columns.Remove(col);
                    return dtEstado;

                case 2:
                    DataTable dtIngresos = _repo.ObtenerIngresosPorParroquia(item.parroquia_id, item.desde, item.hasta);
                    foreach (string col in new[] { "Parroquia_nombre", "id_transaccion", "usuario_nombre", "usuario_apellido" })
                        if (dtIngresos.Columns.Contains(col)) dtIngresos.Columns.Remove(col);
                    return dtIngresos;

                case 3:
                    DataTable dtGastos = _repo.ObtenerGastosPorParroquia(item.parroquia_id, item.desde, item.hasta);
                    foreach (string col in new[] { "Parroquia_nombre", "id_transaccion", "usuario_nombre", "usuario_apellido" })
                        if (dtGastos.Columns.Contains(col)) dtGastos.Columns.Remove(col);
                    return dtGastos;

                case 4:
                    DataSet ds = _repo.ObtenerDatosCuriaPorUsuario(Sesion1.usuario_id, item.desde, item.hasta);
                    DataTable dtCombinada = new DataTable();
                    dtCombinada.Columns.Add("Tipo");
                    dtCombinada.Columns.Add("NombreCuenta");
                    dtCombinada.Columns.Add("Monto");
                    foreach (DataRow r in ds.Tables[1].Rows)
                        dtCombinada.Rows.Add("Entrada", r["NombreCuenta"], r["Monto"]);
                    foreach (DataRow r in ds.Tables[2].Rows)
                        dtCombinada.Rows.Add("Salida", r["NombreCuenta"], r["Monto"]);
                    return dtCombinada;

                case 5:
                    DataTable dtLibro = new LibroMayor().ObtenerLibroMayor(item.parroquia_id, item.desde, item.hasta);
                    foreach (string col in new[] { "Parroquia_nombre", "id_transaccion", "usuario_nombre", "usuario_apellido" })
                        if (dtLibro.Columns.Contains(col)) dtLibro.Columns.Remove(col);
                    return dtLibro;

                default:
                    return new DataTable();
            }
        }
    }
}