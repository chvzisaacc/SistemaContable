using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Bitacora_Admin : Form
    {
        /// <summary>
        /// The crud historial
        /// </summary>
        private clsCRUD_Historial crudHistorial;
        /// <summary>
        /// The crud usuarios
        /// </summary>
        private clsCRUD_Usuarios crudUsuarios;
        /// <summary>
        /// The binding source
        /// </summary>
        private BindingSource bindingSource;
        /// <summary>
        /// The is loading
        /// </summary>
        private bool isLoading = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bitacora_Admin"/> class.
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
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the lblTitulo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbParroquia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*cmbParroquia.Items.AddRange(new string[] { "Seleccionar", "SCJ", "El Calvario" });
            cmbParroquia.SelectedIndex = 0;
            cmbParroquia.BackColor = Color.Beige;
            cmbParroquia.Font = new Font("Segoe UI", 10);*/

            /*
            if (cmbParroquia.SelectedValue == null) return;

            int parroquia_seleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
            int? parroquia_id = parroquia_seleccionada == -1 ? null : (int?)parroquia_seleccionada;

            CargarUsuarios(parroquia_id);
            CargarHistorial();
            */

            /*
            if (cmbParroquia.SelectedValue == null || cmbParroquia.SelectedValue is DataRowView)
                return;

            int parroquia_seleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
            int? parroquia_id = parroquia_seleccionada == -1 ? null : (int?)parroquia_seleccionada;

            CargarUsuarios(parroquia_id);
            CargarHistorial();*/

            if (isLoading) return;

            // Obtener la parroquia seleccionada
            int parroquia_seleccionada = -1;
            if (cmbParroquia.SelectedValue != null &&
                !(cmbParroquia.SelectedValue is DataRowView))
            {
                parroquia_seleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
            }

            int? parroquia_id = parroquia_seleccionada == -1 ? null : (int?)parroquia_seleccionada;

            // Recargar usuarios según la parroquia seleccionada
            CargarUsuarios(parroquia_id);

            // Recargar historial
            CargarHistorial();
        }

        /// <summary>
        /// Handles the CellContentClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            dgvBitacora.Rows.Add("Inicio de sesión", "Ingreso al sistema", "Isaac C.", "06 Jun 2025", "7:00", "Se ha iniciado sesión exitosamente");
            dgvBitacora.Rows.Add("Registro de ingresos", "Ingresos", "Diego M.", "15 Jun 2025", "20:30", "Ingreso registrado");
            */
        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        /// <summary>
        /// Handles the Click event of the lblConsulte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lblConsulte_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the 1 event of the label1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the FRM_PG38 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_PG38_Load(object sender, EventArgs e)
        {
            isLoading = true;
            this.CenterToScreen();
            CargarParroquias();
            CargarUsuarios(null); // Cargar todos los usuarios inicialmente
            CargarHistorial();
            isLoading = false;
        }

        /// <summary>
        /// Cargars the parroquias.
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
                {
                    dt_final.Rows.Add(row["Parroquia_id"], row["Parroquia_nombre"]);
                }

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
        /// Cargars the usuarios.
        /// </summary>
        /// <param name="parroquia_id">The parroquia identifier.</param>
        private void CargarUsuarios(int? parroquia_id = null)
        {
            try
            {
                isLoading = true; // Evitar que el evento SelectedIndexChanged se dispare

                DataTable dt_usuarios = crudHistorial.ObtenerUsuariosPorParroquia(parroquia_id);

                // Crear nuevo DataTable
                DataTable dt_final = new DataTable();
                dt_final.Columns.Add("Usuario_id", typeof(int));
                dt_final.Columns.Add("usuario", typeof(string));

                // Agregar fila "Todos"
                dt_final.Rows.Add(-1, "-- Todos los Usuarios --");

                // Copiar las demás filas
                foreach (DataRow row in dt_usuarios.Rows)
                {
                    dt_final.Rows.Add(row["Usuario_id"], row["usuario"]);
                }

                cmbUsuario.DataSource = dt_final;
                cmbUsuario.DisplayMember = "usuario";
                cmbUsuario.ValueMember = "Usuario_id";
                cmbUsuario.SelectedIndex = 0; // Seleccionar "Todos"

                isLoading = false;
            }
            catch (Exception ex)
            {
                isLoading = false;
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        /// <summary>
        /// Cargars the historial.
        /// </summary>
        private void CargarHistorial()
        {
            if (isLoading) return; // No cargar durante la inicialización

            try
            {
                // Validar que SelectedValue no sea null ni DataRowView
                int parroquia_seleccionada = -1;
                int usuario_seleccionado = -1;

                if (cmbParroquia.SelectedValue != null &&
                    !(cmbParroquia.SelectedValue is DataRowView))
                {
                    parroquia_seleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
                }

                if (cmbUsuario.SelectedValue != null &&
                    !(cmbUsuario.SelectedValue is DataRowView))
                {
                    usuario_seleccionado = Convert.ToInt32(cmbUsuario.SelectedValue);
                }

                int? parroquia_id = parroquia_seleccionada == -1 ? null : (int?)parroquia_seleccionada;
                int? usuarioId = usuario_seleccionado == -1 ? null : (int?)usuario_seleccionado;

                DataTable dt = crudHistorial.ObtenerHistorial(parroquia_id, usuarioId);
                bindingSource.DataSource = dt;
                dgvBitacora.DataSource = bindingSource;

                // Opcional: Ajustar columnas
                if (dgvBitacora.Columns.Count > 0)
                {
                    dgvBitacora.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Cargars the las parroquias.
        /// </summary>
        private void CargarLasParroquias()
        {
            cmbParroquia.DataSource = crudUsuarios.ObtenerParroquias();
            cmbParroquia.DisplayMember = "Parroquia_nombre";
            cmbParroquia.ValueMember = "Parroquia_id";
        }

        /// <summary>
        /// Handles the Click event of the btnVolver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
