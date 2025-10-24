using Capa_de_acceso_de_datos;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Capa_de_Presentación.Formularios_Ewin
{
    public partial class FRM_PG3 : Form
    {
        private int _usuarioId;
        public FRM_PG3(int usuarioId)
        {
            InitializeComponent();
            _usuarioId = usuarioId;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FRM_PG3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string codigo = (txt1.Text + txt2.Text + txt3.Text + txt4.Text +
                            txt5.Text + txt6.Text + txt7.Text + txt8.Text)
                            .Replace(" ", "");

            if (codigo.Length != 8)
            {
                MessageBox.Show("Por favor ingrese el código completo.");
                return;
            }

            ClsVerificarCod verificador = new ClsVerificarCod();
            verificador.ProcesarCodigoRecuperacion(_usuarioId, codigo, this);
        }
    }
}
