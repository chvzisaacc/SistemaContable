using Capa_de_acceso_de_datos;
using System.Data;

namespace Capa_de_Presentación
{
    public partial class Bitacora_Admin : Form
    {
        private clsCRUD_Historial crudHistorial;
        private clsCRUD_Usuarios crudUsuarios;
        private BindingSource bindingSource;
        private bool isLoading = false;

        public Bitacora_Admin()
        {
            InitializeComponent();
            crudHistorial = new clsCRUD_Historial();
            crudUsuarios = new clsCRUD_Usuarios();
            bindingSource = new BindingSource();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void cmbParroquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*cmbParroquia.Items.AddRange(new string[] { "Seleccionar", "SCJ", "El Calvario" });
            cmbParroquia.SelectedIndex = 0;
            cmbParroquia.BackColor = Color.Beige;
            cmbParroquia.Font = new Font("Segoe UI", 10);*/

            /*
            if (cmbParroquia.SelectedValue == null) return;

            int parroquiaSeleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
            int? parroquiaId = parroquiaSeleccionada == -1 ? null : (int?)parroquiaSeleccionada;

            CargarUsuarios(parroquiaId);
            CargarHistorial();
            */

            /*
            if (cmbParroquia.SelectedValue == null || cmbParroquia.SelectedValue is DataRowView)
                return;

            int parroquiaSeleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
            int? parroquiaId = parroquiaSeleccionada == -1 ? null : (int?)parroquiaSeleccionada;

            CargarUsuarios(parroquiaId);
            CargarHistorial();*/

            if (isLoading) return;

            // Obtener la parroquia seleccionada
            int parroquiaSeleccionada = -1;
            if (cmbParroquia.SelectedValue != null &&
                !(cmbParroquia.SelectedValue is DataRowView))
            {
                parroquiaSeleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
            }

            int? parroquiaId = parroquiaSeleccionada == -1 ? null : (int?)parroquiaSeleccionada;

            // Recargar usuarios según la parroquia seleccionada
            CargarUsuarios(parroquiaId);

            // Recargar historial
            CargarHistorial();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            dgvBitacora.Rows.Add("Inicio de sesión", "Ingreso al sistema", "Isaac C.", "06 Jun 2025", "7:00", "Se ha iniciado sesión exitosamente");
            dgvBitacora.Rows.Add("Registro de ingresos", "Ingresos", "Diego M.", "15 Jun 2025", "20:30", "Ingreso registrado");
            */
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void lblConsulte_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void FRM_PG38_Load(object sender, EventArgs e)
        {
            isLoading = true;
            CargarParroquias();
            CargarUsuarios(null); // Cargar todos los usuarios inicialmente
            CargarHistorial();
            isLoading = false;
        }

        private void CargarParroquias()
        {

            try
            {
                DataTable dtParroquias = crudUsuarios.ObtenerParroquias();

                // Crear un nuevo DataTable con la estructura correcta
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("Parroquia_id", typeof(int));
                dtFinal.Columns.Add("Parroquia_nombre", typeof(string));

                // Agregar fila "Todas"
                dtFinal.Rows.Add(-1, "-- Todas las Parroquias --");

                // Copiar las demás filas
                foreach (DataRow row in dtParroquias.Rows)
                {
                    dtFinal.Rows.Add(row["Parroquia_id"], row["Parroquia_nombre"]);
                }

                cmbParroquia.DataSource = dtFinal;
                cmbParroquia.DisplayMember = "Parroquia_nombre";
                cmbParroquia.ValueMember = "Parroquia_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar parroquias: " + ex.Message);
            }
        }

        private void CargarUsuarios(int? parroquiaId = null)
        {
            try
            {
                isLoading = true; // Evitar que el evento SelectedIndexChanged se dispare

                DataTable dtUsuarios = crudHistorial.ObtenerUsuariosPorParroquia(parroquiaId);

                // Crear nuevo DataTable
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("Usuario_id", typeof(int));
                dtFinal.Columns.Add("usuario", typeof(string));

                // Agregar fila "Todos"
                dtFinal.Rows.Add(-1, "-- Todos los Usuarios --");

                // Copiar las demás filas
                foreach (DataRow row in dtUsuarios.Rows)
                {
                    dtFinal.Rows.Add(row["Usuario_id"], row["usuario"]);
                }

                cmbUsuario.DataSource = dtFinal;
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

        private void CargarHistorial()
        {
            if (isLoading) return; // No cargar durante la inicialización

            try
            {
                // Validar que SelectedValue no sea null ni DataRowView
                int parroquiaSeleccionada = -1;
                int usuarioSeleccionado = -1;

                if (cmbParroquia.SelectedValue != null &&
                    !(cmbParroquia.SelectedValue is DataRowView))
                {
                    parroquiaSeleccionada = Convert.ToInt32(cmbParroquia.SelectedValue);
                }

                if (cmbUsuario.SelectedValue != null &&
                    !(cmbUsuario.SelectedValue is DataRowView))
                {
                    usuarioSeleccionado = Convert.ToInt32(cmbUsuario.SelectedValue);
                }

                int? parroquiaId = parroquiaSeleccionada == -1 ? null : (int?)parroquiaSeleccionada;
                int? usuarioId = usuarioSeleccionado == -1 ? null : (int?)usuarioSeleccionado;

                DataTable dt = crudHistorial.ObtenerHistorial(parroquiaId, usuarioId);
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

        private void CargarLasParroquias()
        {
            cmbParroquia.DataSource = crudUsuarios.ObtenerParroquias();
            cmbParroquia.DisplayMember = "Parroquia_nombre";
            cmbParroquia.ValueMember = "Parroquia_id";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
