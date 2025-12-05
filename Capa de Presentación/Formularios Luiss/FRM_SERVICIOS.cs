using Capa_de_acceso_de_datos;
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FRM_SERVICIOS : Form
    {
        //private readonly int Id_Usuariologin;
        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_SERVICIOS"/> class.
        /// </summary>
        /// <param name="Id_Usuario">The identifier usuario.</param>
        public FRM_SERVICIOS(int Id_Usuario)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //Id_Usuariologin = Id_Usuario;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FRM_SERVICIOS"/> class.
        /// </summary>
        public FRM_SERVICIOS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the pibCataloCuentas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pibCataloCuentas_Click(object sender, EventArgs e)
        {
            FRM_PG46 frm = new FRM_PG46();

            frm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the pibGenerarReportes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pibGenerarReportes_Click(object sender, EventArgs e)
        {
            FRM_PG49 frm = new FRM_PG49();
            frm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the pibBitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pibBitacora_Click(object sender, EventArgs e)
        {
            var main = this.Owner as Form;

            try
            {
                main?.Hide();
                this.Hide();

                
                using (var frm = new FRM_PG51(Sesion1.usuario_id))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                this.Close();
                main?.Show();
            }
        }


        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the MouseClick event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrarNavegacion("Catálogo de Cuentas");
            var main = this.Owner as Form; // esto es el FRM_42

            try
            {
                // 1) oculta el 42 para que no se vea atrás
                main?.Hide();

                // 2) abre el 46 de forma modal
                this.Hide(); // oculta el popup mientras estoy en 46
                using (var frm = new FRM_PG46())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this); 
                }
            }
            finally
            {
                // 3) cierra el popup y vuelvo a mostrar el 42
                this.Close();
                main?.Show();
            }

        }

        /// <summary>
        /// Handles the MouseClick event of the textBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrarNavegacion("Reportes");
            var main = this.Owner as Form; // este es el FRM_42

            try
            {

                main?.Hide();


                this.Hide();
                using (var frm = new FRM_PG49())
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {

                this.Close();
                main?.Show();
            }

        }

        /// <summary>
        /// Handles the MouseClick event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            RegistrarNavegacion("Bitacora");
            var main = this.Owner as Form; // este es el FRM_42

            try
            {
                main?.Hide();
                this.Hide();

                using (var frm = new FRM_PG51(Sesion1.usuario_id))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            finally
            {
                this.Close();
                main?.Show();
            }
        }

        /// <summary>
        /// Handles the Load event of the FRM_SERVICIOS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FRM_SERVICIOS_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Registrars the navegacion.
        /// </summary>
        /// <param name="modulo">The modulo.</param>
        private void RegistrarNavegacion(string modulo)
        {
            try
            {
                
                clsCRUD_Historial historial = new clsCRUD_Historial();

                
                historial.RegistrarAccionUsuario(
                    Sesion1.usuario_id,
                    modulo,
                    "Navegación",
                    null,
                    $"Ingresó al módulo de {modulo}"
                );
            }
            catch { }
        }
    }
}

