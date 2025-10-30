namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_PG69
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG69));
            label1 = new Label();
            dgvCajaChica = new DataGridView();
            cNombre = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            textBox2 = new TextBox();
            pibGuardarCerrar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvCajaChica).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pibGuardarCerrar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(186, 33);
            label1.Name = "label1";
            label1.Size = new Size(349, 41);
            label1.TabIndex = 0;
            label1.Text = "Registro para caja chica";
            // 
            // dgvCajaChica
            // 
            dgvCajaChica.BackgroundColor = SystemColors.Control;
            dgvCajaChica.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCajaChica.Columns.AddRange(new DataGridViewColumn[] { cNombre, cDetalle, cSaldo });
            dgvCajaChica.Location = new Point(78, 100);
            dgvCajaChica.Margin = new Padding(3, 4, 3, 4);
            dgvCajaChica.Name = "dgvCajaChica";
            dgvCajaChica.RowHeadersWidth = 51;
            dgvCajaChica.Size = new Size(563, 289);
            dgvCajaChica.TabIndex = 1;
            dgvCajaChica.CellContentClick += dataGridView1_CellContentClick;
            // 
            // cNombre
            // 
            cNombre.HeaderText = "Nombre";
            cNombre.MinimumWidth = 6;
            cNombre.Name = "cNombre";
            cNombre.Width = 150;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.MinimumWidth = 6;
            cDetalle.Name = "cDetalle";
            cDetalle.Width = 150;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.MinimumWidth = 6;
            cSaldo.Name = "cSaldo";
            cSaldo.Width = 150;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(265, 411);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(165, 27);
            textBox2.TabIndex = 16;
            textBox2.Text = "Guardar y cerrar";
            // 
            // pibGuardarCerrar
            // 
            pibGuardarCerrar.Image = (Image)resources.GetObject("pibGuardarCerrar.Image");
            pibGuardarCerrar.Location = new Point(216, 396);
            pibGuardarCerrar.Name = "pibGuardarCerrar";
            pibGuardarCerrar.Size = new Size(254, 67);
            pibGuardarCerrar.SizeMode = PictureBoxSizeMode.CenterImage;
            pibGuardarCerrar.TabIndex = 15;
            pibGuardarCerrar.TabStop = false;
            // 
            // FRM_PG69
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(722, 496);
            Controls.Add(textBox2);
            Controls.Add(pibGuardarCerrar);
            Controls.Add(dgvCajaChica);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FRM_PG69";
            Text = "FRM_PG69";
            Load += FRM_PG69_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCajaChica).EndInit();
            ((System.ComponentModel.ISupportInitialize)pibGuardarCerrar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvCajaChica;
        private DataGridViewTextBoxColumn cNombre;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private TextBox textBox2;
        private PictureBox pibGuardarCerrar;
    }
}