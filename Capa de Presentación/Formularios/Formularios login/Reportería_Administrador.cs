using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using ClosedXML.Excel;
using Spire.Pdf;
using System.Data;

namespace Capa_de_Presentación.Formularios_Ewin
{
    /// <summary>
    /// Formulario para generar y descargar reportes administrativos.
    /// Permite seleccionar parroquia, tipo de reporte y rango de fechas, generar el informe y descargarlo en varios formatos.
    /// </summary>
    public partial class Reportería_Administrador : Form
    {
        /// <summary>Servicio de gastos y utilidades relacionadas a reportes de gastos.</summary>
        private readonly GastosService _gastosService = new GastosService();
        /// <summary>Servicio para generar estado de resultados.</summary>
        private readonly EstadoResultadosService _estadoResultadosService = new EstadoResultadosService();
        /// <summary>Servicio para generar reportes de ingresos.</summary>
        private readonly IngresosService _ingresosService = new IngresosService();
        /// <summary>Utilidad para validar UI (combos, fechas, listas).</summary>
        private ClsValidaciones Validaciones;
        /// <summary>Servicio para generar reportes de curia.</summary>
        private readonly CuriaService _curiaService = new CuriaService();
        /// <summary>Acceso a reportes combinados y utilidades para obtener DataTables.</summary>
        private readonly ClsReportes _repo = new ClsReportes();
        /// <summary>Servicio para generar el libro mayor.</summary>
        private readonly LibroMayorService _libroMayorService = new LibroMayorService();

        /// <summary>
        /// Construye un nombre legible para el reporte según el rango de fechas.
        /// Si el rango está dentro del mismo mes, muestra mes-año; en caso contrario muestra desde-hasta.
        /// </summary>
        private string ConstruirNombreReporteVisible(string tipoTexto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipoTexto} - {desde:MMMM-yyyy}";
            return $"{tipoTexto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
        }

        /// <summary>
        /// Constructor: inicializa componentes y utilidades de validación.
        /// </summary>
        public Reportería_Administrador()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
        }

        /// <summary>
        /// Evento Load del formulario: configura controles, llena combos y límites de fecha.
        /// </summary>
        private void FRM_PG10_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            CargarParroquias();
            CargarReportes();
            cmb_formato_descarga.Items.Clear();
            cmb_formato_descarga.Items.Add("PDF");
            cmb_formato_descarga.Items.Add("DOCX");
            cmb_formato_descarga.Items.Add("JPG");
            cmb_formato_descarga.Items.Add("XLSX");

            DataTable dtTipos = _gastosService.ObtenerTiposReporte();
            cmb_tipo_reporte.DataSource = dtTipos;
            cmb_tipo_reporte.DisplayMember = "descripcion";
            cmb_tipo_reporte.ValueMember = "TipoReporte_id";
            cmb_tipo_reporte.SelectedIndex = -1;

            dtp_desde.MaxDate = DateTime.Now;
            dtp_hasta.MaxDate = DateTime.Now;
        }

        /// <summary>
        /// Cierra el formulario.
        /// </summary>
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Controlador Paint reservado para el panel principal (sin lógica actual).
        /// </summary>
        private void panel1_Paint(object sender, PaintEventArgs e) { }

        /// <summary>
        /// Carga la lista de parroquias desde el servicio de gastos y la enlaza al combo.
        /// </summary>
        private void CargarParroquias()
        {
            DataTable dt = _gastosService.ObtenerParroquias();

            //Todas las parroquias al inicio
            DataRow filaTodas = dt.NewRow();
            filaTodas["Parroquia_ID"] = 0;
            filaTodas["Parroquia_nombre"] = "-- Todas las parroquias --";
            dt.Rows.InsertAt(filaTodas, 0);

            cmb_parroquia.DataSource = null;
            cmb_parroquia.Items.Clear();
            cmb_parroquia.DisplayMember = "Parroquia_nombre";
            cmb_parroquia.ValueMember = "Parroquia_ID";
            cmb_parroquia.DataSource = dt;
            cmb_parroquia.SelectedIndex = -1;
        }

        /// <summary>
        /// Carga la lista de tipos de reporte usando la capa de acceso a datos.
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
                MessageBox.Show("Error al cargar los reportes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera el reporte seleccionado según tipo, parroquia y rango de fechas.
        /// Agrega el reporte generado a la lista de UI y lo abre con la aplicación asociada.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            int tipo_reporte_id = Convert.ToInt32(cmb_tipo_reporte.SelectedValue);
            int parroquia_id = Convert.ToInt32(cmb_parroquia.SelectedValue);

            // Si parroquia_id == 0 => "Todas las parroquias"
            string parroquia_nombre = parroquia_id == 0
                ? "Todas las Parroquias"
                : cmb_parroquia.Text;

            DateTime desde = dtp_desde.Value.Date;
            DateTime hasta = dtp_hasta.Value.Date;

            // YA NO se valida que la parroquia sea obligatoria
            // Solo se valida tipo de reporte y rango de fechas
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

            string ruta_pdf = string.Empty;
            string nombre_reporte = string.Empty;

            switch (tipo_reporte_id)
            {
                case 1:
                    ruta_pdf = _estadoResultadosService.GenerarInformeEstadoResultados(
                        parroquia_id, parroquia_nombre, desde, hasta, Sesion1.usuario_id);
                    nombre_reporte = "Estado de Resultados";
                    break;
                case 2:
                    ruta_pdf = _ingresosService.GenerarReporteIngresos(
                        parroquia_id, parroquia_nombre, desde, hasta, Sesion1.usuario_id);
                    nombre_reporte = "Ingresos";
                    break;
                case 3:
                    ruta_pdf = _gastosService.GenerarInformeGastos(
                        parroquia_id, parroquia_nombre, desde, hasta, Sesion1.usuario_id);
                    nombre_reporte = "Gastos";
                    break;
                case 4:
                    if (parroquia_id == 0)
                    {
                        MessageBox.Show("El reporte de Curia requiere seleccionar una parroquia específica.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ruta_pdf = _curiaService.GenerarInformeCuria(parroquia_id, desde, hasta);
                    nombre_reporte = "Informe de Curia";
                    break;
                case 5:
                    ruta_pdf = _libroMayorService.GenerarInformeLibroMayor(
                        parroquia_id, parroquia_nombre, desde, hasta);
                    nombre_reporte = "Libro Mayor";
                    break;
                default:
                    MessageBox.Show("Tipo de reporte no válido.");
                    return;
            }

            string nombreVisible = ConstruirNombreReporteVisible(nombre_reporte, desde, hasta);

            var item = new ReporteUIItem
            {
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
        /// Gestiona la descarga del reporte en el formato seleccionado por el usuario (PDF, DOCX, JPG, XLSX).
        /// Para XLSX construye una hoja de cálculo a partir de un DataTable obtenido mediante <see cref="ObtenerDatosReporte"/>.
        /// </summary>
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
            string formato = cmb_formato_descarga.SelectedItem.ToString();

            if (formato != "XLSX" && !File.Exists(item.ruta_pdf))
            {
                MessageBox.Show("No se encontró el archivo del reporte en disco.");
                return;
            }

            string nombreSeguro = item.nombre_visible
                .Replace("/", "-").Replace("\\", "-").Replace(":", "-")
                .Replace("*", "-").Replace("?", "").Replace("\"", "")
                .Replace("<", "").Replace(">", "").Replace("|", "");

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
                    case "XLSX":
                        sfd.Filter = "Archivo Excel|*.xlsx";
                        sfd.FileName = nombreSeguro + ".xlsx";
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
        /// Obtiene el DataTable apropiado para exportar según el tipo de reporte.
        /// Realiza limpieza de columnas no deseadas antes de retornar los datos.
        /// </summary>
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

        private void cmb_tipo_reporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = cmb_tipo_reporte.SelectedItem?.ToString()?.ToLower() ?? "";

            if (tipo.Contains("curia"))
            {
                if (cmb_parroquia.SelectedValue != null &&
                    Convert.ToInt32(cmb_parroquia.SelectedValue) == 0)
                {
                    MessageBox.Show("El reporte de Curia requiere seleccionar una parroquia específica.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmb_parroquia.SelectedIndex = -1;
                }
            }
        }

        private void cmb_parroquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_parroquia.SelectedValue == null) return;

            int parroquia_id = Convert.ToInt32(cmb_parroquia.SelectedValue);
            string tipo = cmb_tipo_reporte.SelectedItem?.ToString()?.ToLower() ?? "";

            // Detecta "Informe de Curia" sin importar mayúsculas
            if (parroquia_id == 0 && tipo.Contains("curia"))
            {
                MessageBox.Show("El reporte de Curia requiere seleccionar una parroquia específica.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_parroquia.SelectedIndex = -1;
            }
        }
    }
}