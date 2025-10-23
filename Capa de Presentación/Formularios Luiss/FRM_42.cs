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
    public partial class FRM_42 : Form
    {
        public FRM_42()
        {
            InitializeComponent();
        }

        //Método para mostrar el panel seleccionado y ocultar los demás
        private void MostrarPanel(Panel panelActivo)
        {
            // Oculta todos los paneles
            panelIngresos.Visible = false;
            panelGastos.Visible = false;
            panelCajaChica.Visible = false;
            panelBancos.Visible = false;
            // Muestra solo el panel seleccionado}
            pibImage.Visible = true;
            lblNoSeleccionado.Visible = true;
            panelActivo.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS();
            var buttonScreenPosition = pictureBox1.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox1.Height);
            popup.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FRM_42_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FRM_CERRARSESION popup = new FRM_CERRARSESION();
            var buttonScreenPosition = pictureBox2.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox2.Height);
            popup.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MostrarPanel(panelBancos);
            pibImage.Visible = false;
            lblNoSeleccionado.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnIngresos_Click(object sender, EventArgs e)
        {
            MostrarPanel(panelIngresos);
            pibImage.Visible = false;
            lblNoSeleccionado.Visible = false;
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            MostrarPanel(panelGastos);
            pibImage.Visible = false;
            lblNoSeleccionado.Visible = false;
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            MostrarPanel(panelCajaChica);
            pibImage.Visible = false;
            lblNoSeleccionado.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }
    }
}

