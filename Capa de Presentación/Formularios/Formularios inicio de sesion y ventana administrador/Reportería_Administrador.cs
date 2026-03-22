using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_procesamiento_de_datos;
using ClosedXML.Excel;
using Spire.Pdf;
using System.Data;

namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class Reportería_Administrador : Form
    {
        private readonly GastosService _gastosService = new GastosService();
        private readonly EstadoResultadosService _estadoResultadosService = new EstadoResultadosService();
        private readonly IngresosService _ingresosService = new IngresosService();
        private ClsValidaciones Validaciones;
        private readonly CuriaService _curiaService = new CuriaService();
        private readonly ClsReportes _repo = new ClsReportes();
        private readonly LibroMayorService _libroMayorService = new LibroMayorService();

        private string ConstruirNombreReporteVisible(string tipoTexto, DateTime desde, DateTime hasta)
        {
            if (desde.Month == hasta.Month && desde.Year == hasta.Year)
                return $"{tipoTexto} - {desde:MMMM-yyyy}";
            return $"{tipoTexto} - {desde:dd/MM/yyyy} a {hasta:dd/MM/yyyy}";
        }

        public Reportería_Administrador()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Validaciones = new ClsValidaciones();
        }

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

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

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
                MessageBox.Show("Error al cargar los reportes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
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