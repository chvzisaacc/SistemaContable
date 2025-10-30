using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capa_de_Presentación.Formularios_Luiss
{
    public partial class FRM_42 : Form
    {
        private clsCRUD_CatalogoCuentas crudCataloCuentas;
        private bool modoEdicion = false;
        private int cuentaBancoIDseleccionado = 0;

        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42()
        {
            InitializeComponent();
            crudCataloCuentas = new clsCRUD_CatalogoCuentas();
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

        private void FRM_42_Load(object sender, EventArgs e)
        {
            //CargarDatos();
            //CargarComboBoxes();
            // LimpiarCampos();
            //HabilitarControles(false);
            MostrarSaldoActual();
        }

        /*private void CargarDatos()
        {
            try
            {
                dgvUsuarios.DataSource = crudUsuarios.ObtenerUsuarios();

                //aqui es para ocultar algunos campos (los ids y las contraseñas)
                /*
                if (dgvUsuarios.Columns["Contraseña"] != null)
                    dgvUsuarios.Columns["Contraseña"].Visible = false;

                if (dgvUsuarios.Columns["RolID"] != null)
                    dgvUsuarios.Columns["RolID"].Visible = false;
                if (dgvUsuarios.Columns["ParroquiaID"] != null)
                    dgvUsuarios.Columns["ParroquiaID"].Visible = false;
                if (dgvUsuarios.Columns["EstadoID"] != null)
                    dgvUsuarios.Columns["EstadoID"].Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

        /* private void CargarComboBoxes()
         {
             try
             {
                 cmbCuentas.DataSource = crudCataloCuentas.ObtenerRoles();
                 cmbCuentas.DisplayMember = "Rol_descripcion";
                 cmbCuentas.ValueMember = "Rol_Id";

                 cmbParroquia.DataSource = crudCataloCuentas.ObtenerParroquias();
                 cmbParroquia.DisplayMember = "Parroquia_nombre";
                 cmbParroquia.ValueMember = "Parroquia_id";

                 cmbEstado.DataSource = crudUsuarios.ObtenerEstados();
                 cmbEstado.DisplayMember = "descripcion";
                 cmbEstado.ValueMember = "Id_estado_cuenta";
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

         */

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
                case 1: // Transferencia entre cuentas
                    {
                        var frm = new FRM_BancosTransferenciaEntreCuentas
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
                case 2: // Agregar cuenta bancaria
                    {
                        var frm = new FRM_BancosAgregarCuentaBancaria
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(430, 450)
                        };

                        frm.ShowDialog();
                        break;
                    }
                case 3: // Retirar dinero
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDetalle_Click_1(object sender, EventArgs e)
        {
            //llamar form 69,
            FRM_PG69 obj_frm69 = new FRM_PG69();
            obj_frm69.ShowDialog();
        }

        private void chkSaldoInicial_CheckedChanged(object sender, EventArgs e)
        {
            FRM_CajaChicaMonto obcaja = new FRM_CajaChicaMonto();
            obcaja.ShowDialog();
        }
        private void MostrarSaldoActual()
        {
            Clsconexion objCon = new Clsconexion();

            try
            {
                objCon.Abrir();
                MessageBox.Show("Conexion abierta: " + objCon.sc.State.ToString());

                string query = "SELECT TOP 1 saldo FROM Cajachica ORDER BY Id_cajachica DESC";
                SqlCommand comando = new SqlCommand(query, objCon.sc);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    //convertir el valor string a decimal
                    decimal saldo = Convert.ToDecimal(lector["saldo"]);
                    //n2 formatea a dos digitos despues del "."
                    txtSaldoActual.Text = saldo.ToString("N2");
                }
                else
                {
                    MessageBox.Show("No hay registros en CajaChica.");
                    txtSaldoActual.Text = "0.00";
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                objCon.Cerrar();
            }
        }

        private void txtSaldoActual_TextChanged(object sender, EventArgs e)
        {

        }
    }

}


      

      


