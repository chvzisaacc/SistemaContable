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


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG5 : Form
    {
        ClsCerrar cerrar = new ClsCerrar();
        public FRM_PG5()
        {
            InitializeComponent();
            this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelUsuario);
            panelContenedor.Controls.Add(panelCatalogoCuentas);


            // Opcional: muestra uno por defecto
            MostrarSoloEstePanel(panel1);
        }

        private void MostrarSoloEstePanel(Panel panelAMostrar)
        {
            foreach (Control ctrl in panelContenedor.Controls)
            {
                if (ctrl is Panel)
                    ctrl.Visible = false;
            }
            panelAMostrar.Visible = true;
            panelAMostrar.BringToFront();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void FRM_PG5_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelUsuario);
        }

        private void btnCatalagoCuenta_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCatalogoCuentas);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
