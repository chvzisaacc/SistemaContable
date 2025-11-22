namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_SERVICIOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_SERVICIOS));
            label1 = new Label();
            textBox2 = new TextBox();
            pibCatalogoCuentas = new PictureBox();
            textBox1 = new TextBox();
            pibGenerarReportes = new PictureBox();
            textBox3 = new TextBox();
            pibBitacora = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pibCatalogoCuentas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pibGenerarReportes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pibBitacora).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(136, -1);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(198, 48);
            label1.TabIndex = 0;
            label1.Text = "SERVICIOS";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(118, 288);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(296, 38);
            textBox2.TabIndex = 18;
            textBox2.Text = "Catálogo de cuentas";
            textBox2.MouseClick += textBox2_MouseClick;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pibCatalogoCuentas
            // 
            pibCatalogoCuentas.Image = (Image)resources.GetObject("pibCatalogoCuentas.Image");
            pibCatalogoCuentas.Location = new Point(38, 248);
            pibCatalogoCuentas.Margin = new Padding(4);
            pibCatalogoCuentas.Name = "pibCatalogoCuentas";
            pibCatalogoCuentas.Size = new Size(459, 131);
            pibCatalogoCuentas.SizeMode = PictureBoxSizeMode.Zoom;
            pibCatalogoCuentas.TabIndex = 17;
            pibCatalogoCuentas.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(43, 56, 143);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(136, 411);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(275, 38);
            textBox1.TabIndex = 20;
            textBox1.Text = "Generar Reportes";
            textBox1.MouseClick += textBox1_MouseClick;
            // 
            // pibGenerarReportes
            // 
            pibGenerarReportes.Image = (Image)resources.GetObject("pibGenerarReportes.Image");
            pibGenerarReportes.Location = new Point(38, 370);
            pibGenerarReportes.Margin = new Padding(4);
            pibGenerarReportes.Name = "pibGenerarReportes";
            pibGenerarReportes.Size = new Size(459, 131);
            pibGenerarReportes.SizeMode = PictureBoxSizeMode.Zoom;
            pibGenerarReportes.TabIndex = 19;
            pibGenerarReportes.TabStop = false;
            pibGenerarReportes.Click += pibGenerarReportes_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(43, 56, 143);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox3.ForeColor = Color.White;
            textBox3.Location = new Point(188, 540);
            textBox3.Margin = new Padding(4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(161, 38);
            textBox3.TabIndex = 22;
            textBox3.Text = "Bitácora";
            textBox3.MouseClick += textBox3_MouseClick;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // pibBitacora
            // 
            pibBitacora.Image = (Image)resources.GetObject("pibBitacora.Image");
            pibBitacora.Location = new Point(38, 495);
            pibBitacora.Margin = new Padding(4);
            pibBitacora.Name = "pibBitacora";
            pibBitacora.Size = new Size(459, 131);
            pibBitacora.SizeMode = PictureBoxSizeMode.Zoom;
            pibBitacora.TabIndex = 21;
            pibBitacora.TabStop = false;
            pibBitacora.Click += pibBitacora_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(76, 55);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(359, 184);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // FRM_SERVICIOS
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 641);
            Controls.Add(textBox3);
            Controls.Add(pibBitacora);
            Controls.Add(textBox1);
            Controls.Add(pibGenerarReportes);
            Controls.Add(textBox2);
            Controls.Add(pibCatalogoCuentas);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FRM_SERVICIOS";
            Text = "FRM_SERVICIOS";
            Load += FRM_SERVICIOS_Load;
            ((System.ComponentModel.ISupportInitialize)pibCatalogoCuentas).EndInit();
            ((System.ComponentModel.ISupportInitialize)pibGenerarReportes).EndInit();
            ((System.ComponentModel.ISupportInitialize)pibBitacora).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox2;
        private PictureBox pibCatalogoCuentas;
        private TextBox textBox1;
        private PictureBox pibGenerarReportes;
        private TextBox textBox3;
        private PictureBox pibBitacora;
        private PictureBox pictureBox1;
    }
}