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

        private ClsCRUD_CuentasBancarias crudCuentasBancarias;

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

            crudCuentasBancarias = new ClsCRUD_CuentasBancarias();

            this.FormClosing += cerrar.CerrarApp;
            // Agrega los paneles secundarios dentro del panel contenedor
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            //panelContenedor.Controls.Add(panelMensaje);

            // Opcional: muestra uno por defecto
            MostrarSoloEstePanel(panel1);
        }

        private void FRM_42_Load(object sender, EventArgs e)
        {

            DateTime mesactual = DateTime.Now;
            DateTime mesactual1 = new DateTime(mesactual.Year, mesactual.Month, 1);
            dtpFecha.MinDate = mesactual1;
            dtpFecha.MaxDate = mesactual;
            MostrarSaldoActual();
            CargarDatos();
            CargarComboBoxes();
            CargarCuentasEnComboBox();
        }
        

        private void CargarDatos()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
                 
       private void CargarComboBoxes()
        {
            try
            {
                cmbCuentas.DataSource = crudCuentasBancarias.ObtenerCuentasBancarias();
                cmbCuentas.DisplayMember = "Nombre";
                cmbCuentas.ValueMember = "Id_cuentaBanco";

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar opciones: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCuentasEnComboBox()
        {
            try
            {
                
                DataTable dtCuentas = crudCuentasBancarias.ObtenerCuentasBancarias();

                
                cmbCuentas.DataSource = dtCuentas;
                cmbCuentas.DisplayMember = "Nombre";      
                cmbCuentas.ValueMember = "Id_Origen";     

                // 3. Deselecciona cualquier ítem por defecto para evitar que el evento se dispare al cargar
                cmbCuentas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las cuentas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelGastos2);
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelCajaChica2);
        }
        private void btnIngresos_Click_1(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            MostrarSoloEstePanel(panelIngresos);
        }

        private void btnBancos_Click_1(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
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


// Si no hay nada seleccionado (ej. al inicio), no hace nada.
        if (cmbCuentas.SelectedIndex < 0 || cmbCuentas.SelectedValue == null)
        {
            return;
        }

        // Obtiene el ID de la cuenta desde el valor seleccionado
        int idSeleccionado = Convert.ToInt32(cmbCuentas.SelectedValue);

        // Crea y muestra el formulario general, pasándole el ID
        var frm = new FRM_PG42BancosCuentaAhorro(idSeleccionado)
        {
            StartPosition = FormStartPosition.Manual,
            // Uso las coordenadas que has manejado antes para mantener la consistencia [memory:6]
            Location = new Point(414, 101) 
        };
        
        frm.ShowDialog(this);
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



        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
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




      

      


