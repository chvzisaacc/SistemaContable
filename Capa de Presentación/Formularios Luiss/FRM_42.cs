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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_42 : Form
    {

        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42()
        {
            InitializeComponent();
            this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            panelContenedor.Controls.Add(panel5);

            // Opcional: muestra uno por defecto
            MostrarSoloEstePanel(panelContenedor);
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



        private void btnGastos_Click(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelGastos2);
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelCajaChica2);
        }
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelIngresos);
        }

        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            MostrarSoloEstePanel(panelBancos2);
        }







        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FRM_SERVICIOS popup = new FRM_SERVICIOS();
            var buttonScreenPosition = pictureBox1.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox1.Height);
            popup.ShowDialog();
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FRM_CERRARSESION popup = new FRM_CERRARSESION();
            var buttonScreenPosition = pictureBox2.PointToScreen(Point.Empty);
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(buttonScreenPosition.X, buttonScreenPosition.Y + pictureBox2.Height);
            popup.ShowDialog();
        }



        private void btnDetalle_Click(object sender, EventArgs e)
        {
            using (var frm = new FRM_PG69())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void cmbCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cmbCuentas.SelectedIndex < 0) return;

            switch (cmbCuentas.SelectedIndex)
            {
                case 0: // Cuenta ahorro
                    {
                        var frm = new FRM_PG42BancosCuentaAhorro
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };
                        frm.ShowDialog();
                        break;
                    }
                case 1: // Cuenta cheques
                    {
                        var frm = new FRM_PG42BancosCuentaCheque
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
            }
        }

        private void cmbAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcciones.SelectedIndex < 0) return;

            switch (cmbAcciones.SelectedIndex)
            {
                case 0: // Agregar Saldo
                    {
                        var frm = new FRM_BancosAgregarSaldo
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };
                        frm.ShowDialog();
                        break;
                    }
                case 1: // Cuenta cheques
                    {
                        var frm = new FRM_BancosTransferenciaEntreCuentas
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
                case 2: // Cuenta cheques
                    {
                        var frm = new FRM_BancosAgregarCuentaBancaria
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
                case 3: // Cuenta cheques
                    {
                        var frm = new FRM_BancosRetirarDinero
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
            }
        }
    }
}

      

      


