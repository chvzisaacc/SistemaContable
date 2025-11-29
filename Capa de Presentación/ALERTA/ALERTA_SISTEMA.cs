using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_de_procesamiento_de_datos;

namespace Capa_de_Presentación.ALERTA
{
    public partial class ALERTA_SISTEMA : Form
    {
        private readonly Alerta objalerta = new();
        public ALERTA_SISTEMA()
        {
            InitializeComponent();


        }

        private void ALERTA_SISTEMA_Load(object sender, EventArgs e)
        {
            CargarDatosAlerta();

        }

        private void CargarDatosAlerta()
        {
            try
            {
                DataTable datosalerta = objalerta.CargarAlerta();

                this.dataGridView1.DataSource = datosalerta;

                this.dataGridView1.Refresh();
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro al cargar alerta" + ex.Message,
                                "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SOLO ADMITE DIAS - ES EL METODO FINAL
        private void button1_Click(object sender, EventArgs e)
        {
            int diasLimite = (int)LimiteDay.Value;
            bool alarmaActiva = Estado.Checked;

            try
            {
                objalerta.ModificarConfiguracionAlerta(diasLimite, alarmaActiva);
                CargarDatosAlerta();

                MessageBox.Show(" alerta modificada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }
    }
}
