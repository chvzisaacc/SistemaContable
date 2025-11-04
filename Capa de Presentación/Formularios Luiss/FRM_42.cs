using Capa_de_acceso_de_datos;
using Capa_de_Presentación.CLASES;
using Capa_de_Presentación.Formularios_Diego;
using Microsoft.Data.SqlClient;
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
        private DataTable dtDatosIngresos = null;
        private clsCRUD_CatalogoCuentas crudCataloCuentas;
        private bool modoEdicion = false;
        private int cuentaBancoIDseleccionado = 0;
        private AutoCompleteStringCollection Subcuentas = new AutoCompleteStringCollection();
        private ClsAccionesDB objSubCuentas = new ClsAccionesDB();

        ClsCerrar cerrar = new ClsCerrar();
        public FRM_42()
        {
            InitializeComponent();
            InicializarDGVIngr();
            CargarDatosAutocompletado();
            Transacciones objtransa = new();
            objtransa.CargarComboBoxOrigen(cmbOrigen);
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

            //Muestra el ultimo saldo inicial agregado 
            ActualizarSaldo();
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

            chkSaldoInicial.Visible = false;

            if (txtSaldoActual.Text == "0.00")
            {
                chkSaldoInicial.Visible = true;
            }
        }
        private void MostrarSaldoActual()
        {
            Clsconexion objCon = new Clsconexion();

            try
            {
                objCon.Abrir();

                string query = "exec saldo_actual";
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

        //Actualiza el saldo que se muestra en pantalla
        private void ActualizarSaldo()
        {
            try
            {
                MostrarSaldoActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el saldo: " + ex.Message);
            }
        }

        private void txtSaldoActual_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbOrigen.Text = "Seleccionar";
            txtNoReferencia.Text = null;
            Transacciones transa = new();
            transa.Agregarfila(dtDatosIngresos, dataGridView1);


        }

        private void dgvIngresos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InicializarDGVIngr()
        {
            dataGridView1.Columns.Clear();
            dtDatosIngresos = new DataTable("Ingresos");
            dtDatosIngresos.Columns.Add("NombreCuenta", typeof(string));
            dtDatosIngresos.Columns.Add("Detalle", typeof(string));
            dtDatosIngresos.Columns.Add("Saldo", typeof(string));
            dataGridView1.DataSource = dtDatosIngresos;
            dataGridView1.AutoGenerateColumns = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Transacciones objtransa = new();
            objtransa.BloquearDesbloquearDataIngresos(dtDatosIngresos, dataGridView1, e.RowIndex);
        }

        private void panelIngresos_Paint(object sender, PaintEventArgs e)
        {

        }


        private void CargarDatosAutocompletado()
        {
            Subcuentas.Clear();

            try
            {
                DataTable dt = objSubCuentas.ObtenerCuentasIngreso();

                foreach (DataRow row in dt.Rows)
                {
                    Subcuentas.Add(row["Subcuentas"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "NombreCuenta")
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    ClsAccionesDB clsAccionesDB = new();
                    DataTable dtCuentas = clsAccionesDB.ObtenerCuentasIngreso();
                    AutoCompleteStringCollection nombresCuentas = new AutoCompleteStringCollection();

                    foreach (DataRow row in dtCuentas.Rows)
                    {
                        nombresCuentas.Add(row["Subcuentas"].ToString());
                    }

                    autoText.AutoCompleteCustomSource = nombresCuentas;
                }
            }
            else
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.None;
                    autoText.AutoCompleteSource = AutoCompleteSource.None;
                }
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            FRM_PG103 fRM_PG103 = new FRM_PG103();
            fRM_PG103.Show();
            this.Hide();
        }
    }
}




      

      


