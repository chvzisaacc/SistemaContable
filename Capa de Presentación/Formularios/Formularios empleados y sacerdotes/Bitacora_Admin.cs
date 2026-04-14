
using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación
{
    /// <summary>
    /// Formulario que muestra el historial (bitácora) filtrable por parroquia, usuario y rango de fechas.
    /// Contiene paginación y enlaces a la capa de acceso a datos para obtener registros.
    /// </summary>
    public partial class Bitacora_Admin : Form
    {
        /// <summary>
        /// Acceso a operaciones CRUD sobre historial.
        /// </summary>
        private clsCRUD_Historial crudHistorial;

        /// <summary>
        /// Acceso a operaciones CRUD sobre usuarios / parroquias.
        /// </summary>
        private clsCRUD_Usuarios crudUsuarios;

        /// <summary>
        /// Fuente de datos vinculada al DataGridView para facilitar paginado y refresco.
        /// </summary>
        private BindingSource bindingSource;

        /// <summary>
        /// Indicador temporal usado para evitar recargas mientras se inicializan controles.
        /// </summary>
        private bool isLoading = false;

        /// <summary>
        /// Página actual usada por la paginación.
        /// </summary>
        private int _paginaActual = 1;

        /// <summary>
        /// Tamaño de página (registros por página).
        /// </summary>
        private int _tamanoPagina = 50;

        /// <summary>
        /// Total de páginas calculado tras la consulta.
        /// </summary>
        private int _totalPaginas = 1;

        /// <summary>
        /// Constructor: inicializa componentes, dependencias y la BindingSource.
        /// Las inicializaciones tocan el UI y se asumen en el hilo de interfaz (UI thread).
        /// </summary>
        public Bitacora_Admin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            crudHistorial = new clsCRUD_Historial();
            crudUsuarios = new clsCRUD_Usuarios();
            bindingSource = new BindingSource();
        }

        /// <summary>
        /// Paint vacío del panel (placeholder para personalización visual).
        /// Mantener vacío si no se requiere dibujo personalizado.
        /// </summary>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Manejador que responde al cambio de parroquia seleccionada.
        /// Actualiza la lista de usuarios y recarga el historial.
        /// Se protege con <see cref="isLoading"/> para evitar recargas durante la inicialización.
        /// </summary>
        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            int parroquia_seleccionada = -1;
            if (cmbParroquia.SelectedValue != null &&
                !(cmbParroquia.SelectedValue is DataRowView))
                parroquia_seleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);

            int? parroquia_id = parroquia_seleccionada == -1 ? null : (int?)parroquia_seleccionada;

            CargarUsuarios(parroquia_id);
            _paginaActual = 1;
            CargarHistorial();
        }

        /// <summary>
        /// Placeholder para eventos de celda del DataGridView.
        /// Se deja vacío porque no requiere lógica adicional actualmente.
        /// </summary>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Manejador que responde al cambio de usuario seleccionado.
        /// Reinicia la paginación y recarga el historial.
        /// </summary>
        private void cmbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            _paginaActual = 1;
            CargarHistorial();
        }

        /// <summary>
        /// Manejador Load del formulario.
        /// Centra el formulario, carga parroquias y usuarios y configura límites de fecha.
        /// Se utiliza <see cref="isLoading"/> para evitar recargas intermedias.
        /// </summary>
        private void FRM_PG38_Load(object sender, EventArgs e)
        {
            isLoading = true;
            this.CenterToScreen();
            CargarParroquias();
            CargarUsuarios(null);
            isLoading = false;
            dtpFechaDesde.MaxDate = DateTime.Today;
            dtpFechaHasta.MaxDate = DateTime.Today;
            cmbUsuario.SelectedIndexChanged -= cmbUsuario_SelectedIndexChanged;
            cmbUsuario.SelectedIndexChanged += cmbUsuario_SelectedIndexChanged;
            CargarHistorial();
        }

        /// <summary>
        /// Carga la lista de parroquias en el combo con una opción para "Todas las Parroquias".
        /// Actualiza el control en el hilo UI; si se invoca desde background, aplicar cambios con Invoke.
        /// </summary>
        private void CargarParroquias()
        {
            try
            {
                DataTable dt_parroquias = crudUsuarios.ObtenerParroquias();

                DataTable dt_final = new DataTable();
                dt_final.Columns.Add("Parroquia_id", typeof(int));
                dt_final.Columns.Add("Parroquia_nombre", typeof(string));

                dt_final.Rows.Add(-1, "-- Todas las Parroquias --");

                foreach (DataRow row in dt_parroquias.Rows)
                    dt_final.Rows.Add(row["Parroquia_id"], row["Parroquia_nombre"]);

                cmbParroquia.DataSource = dt_final;
                cmbParroquia.DisplayMember = "Parroquia_nombre";
                cmbParroquia.ValueMember = "Parroquia_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar parroquias: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga usuarios filtrados por parroquia (o todos si el parámetro es nulo).
        /// Usa <see cref="isLoading"/> para evitar disparar eventos de cambio mientras se configura el combo.
        /// </summary>
        /// <param name="parroquia_id">Id de parroquia o null para todas.</param>
        private void CargarUsuarios(int? parroquia_id = null)
        {
            try
            {
                isLoading = true;

                DataTable dt_usuarios = crudHistorial.ObtenerUsuariosPorParroquia(parroquia_id);

                DataTable dt_final = new DataTable();
                dt_final.Columns.Add("Usuario_id", typeof(int));
                dt_final.Columns.Add("usuario", typeof(string));

                dt_final.Rows.Add(-1, "-- Todos los Usuarios --");

                foreach (DataRow row in dt_usuarios.Rows)
                    dt_final.Rows.Add(row["Usuario_id"], row["usuario"]);

                cmbUsuario.DataSource = dt_final;
                cmbUsuario.DisplayMember = "usuario";
                cmbUsuario.ValueMember = "Usuario_id";
                cmbUsuario.SelectedIndex = 0;

                isLoading = false;
            }
            catch (Exception ex)
            {
                isLoading = false;
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta y muestra el historial en la página actual aplicando filtros de parroquia, usuario y fechas.
        /// Actualiza la BindingSource y el DataGridView. Si la consulta se realiza en un hilo background,
        /// las asignaciones a controles deben realizarse mediante Invoke/BeginInvoke para sincronizar con la UI.
        /// </summary>
        private void CargarHistorial()
        {
            if (isLoading) return;

            try
            {
                int parroquia_seleccionada = -1;
                int usuario_seleccionado = -1;

                if (cmbParroquia.SelectedValue != null &&
                    !(cmbParroquia.SelectedValue is DataRowView))
                    parroquia_seleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);

                if (cmbUsuario.SelectedValue != null &&
                    !(cmbUsuario.SelectedValue is DataRowView))
                    usuario_seleccionado = Convert.ToInt32(cmbUsuario.SelectedValue);

                int? parroquia_id = parroquia_seleccionada == -1 ? null : (int?)parroquia_seleccionada;
                int? usuarioId = usuario_seleccionado == -1 ? null : (int?)usuario_seleccionado;

                DateTime? fechaDesde = chkFiltrarFecha.Checked ? dtpFechaDesde.Value.Date : (DateTime?)null;
                DateTime? fechaHasta = chkFiltrarFecha.Checked ? dtpFechaHasta.Value.Date : (DateTime?)null;

                int totalRegistros;
                DataTable dt = crudHistorial.ObtenerHistorial(
                    out totalRegistros,
                    parroquia_id, usuarioId,
                    fechaDesde, fechaHasta,
                    _paginaActual, _tamanoPagina);

                _totalPaginas = (int)Math.Ceiling((double)totalRegistros / _tamanoPagina);
                if (_totalPaginas == 0) _totalPaginas = 1;

                bindingSource.DataSource = dt;
                dgvBitacora.DataSource = bindingSource;

                lblPagina.Text = $"Página {_paginaActual} de {_totalPaginas}";

                btnAnterior.Enabled = _paginaActual > 1;
                btnSiguiente.Enabled = _paginaActual < _totalPaginas;

                if (dgvBitacora.Columns.Contains("Descripción"))
                    dgvBitacora.Columns["Descripción"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Alternativa de carga de parroquias que asigna directamente el DataSource.
        /// Conservado para usos puntuales del diseñador o flujo existente.
        /// </summary>
        private void CargarLasParroquias()
        {
            cmbParroquia.DataSource = crudUsuarios.ObtenerParroquias();
            cmbParroquia.DisplayMember = "Parroquia_nombre";
            cmbParroquia.ValueMember = "Parroquia_id";
        }

        /// <summary>
        /// Botón Volver: cierra el formulario.
        /// </summary>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Botón de página anterior: decrementa la página y recarga el historial.
        /// </summary>
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                CargarHistorial();
            }
        }

        /// <summary>
        /// Botón de página siguiente: incrementa la página y recarga el historial.
        /// </summary>
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                _paginaActual++;
                CargarHistorial();
            }
        }

        /// <summary>
        /// Maneja el cambio del checkbox de filtro por fecha.
        /// Habilita/deshabilita los DateTimePickers, reinicia la paginación y recarga.
        /// </summary>
        private void chkFiltrarFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            dtpFechaDesde.Enabled = chkFiltrarFecha.Checked;
            dtpFechaHasta.Enabled = chkFiltrarFecha.Checked;
            _paginaActual = 1;
            CargarHistorial();
        }

        /// <summary>
        /// Controla cambios en la fecha 'Desde': mantiene la consistencia con 'Hasta' y recarga historial.
        /// </summary>
        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date)
                dtpFechaHasta.Value = dtpFechaDesde.Value;
            _paginaActual = 1;
            CargarHistorial();
        }

        /// <summary>
        /// Controla cambios en la fecha 'Hasta': mantiene la consistencia con 'Desde' y recarga historial.
        /// </summary>
        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtpFechaHasta.Value.Date < dtpFechaDesde.Value.Date)
                dtpFechaDesde.Value = dtpFechaHasta.Value;
            _paginaActual = 1;
            CargarHistorial();
        }
    }
}