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
            label1.Location = new Point(109, -1);
            label1.Name = "label1";
            label1.Size = new Size(134, 32);
            label1.TabIndex = 0;
            label1.Text = "SERVICIOS";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(103, 178);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(175, 22);
            textBox2.TabIndex = 18;
            textBox2.Text = "Catálogo de cuentas";
            textBox2.MouseClick += textBox2_MouseClick;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pibCatalogoCuentas
            // 
            pibCatalogoCuentas.Image = (Image)resources.GetObject("pibCatalogoCuentas.Image");
            pibCatalogoCuentas.Location = new Point(27, 149);
            pibCatalogoCuentas.Margin = new Padding(3, 2, 3, 2);
            pibCatalogoCuentas.Name = "pibCatalogoCuentas";
            pibCatalogoCuentas.Size = new Size(321, 79);
            pibCatalogoCuentas.SizeMode = PictureBoxSizeMode.Zoom;
            pibCatalogoCuentas.TabIndex = 17;
            pibCatalogoCuentas.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(43, 56, 143);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(109, 251);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(158, 22);
            textBox1.TabIndex = 20;
            textBox1.Text = "Generar Reportes";
            textBox1.MouseClick += textBox1_MouseClick;
            // 
            // pibGenerarReportes
            // 
            pibGenerarReportes.Image = (Image)resources.GetObject("pibGenerarReportes.Image");
            pibGenerarReportes.Location = new Point(27, 222);
            pibGenerarReportes.Margin = new Padding(3, 2, 3, 2);
            pibGenerarReportes.Name = "pibGenerarReportes";
            pibGenerarReportes.Size = new Size(321, 79);
            pibGenerarReportes.SizeMode = PictureBoxSizeMode.Zoom;
            pibGenerarReportes.TabIndex = 19;
            pibGenerarReportes.TabStop = false;
            pibGenerarReportes.Click += pibGenerarReportes_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(43, 56, 143);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox3.ForeColor = Color.White;
            textBox3.Location = new Point(144, 325);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(74, 22);
            textBox3.TabIndex = 22;
            textBox3.Text = "Bitácora";
            textBox3.MouseClick += textBox3_MouseClick;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // pibBitacora
            // 
            pibBitacora.Image = (Image)resources.GetObject("pibBitacora.Image");
            pibBitacora.Location = new Point(27, 297);
            pibBitacora.Margin = new Padding(3, 2, 3, 2);
            pibBitacora.Name = "pibBitacora";
            pibBitacora.Size = new Size(321, 79);
            pibBitacora.SizeMode = PictureBoxSizeMode.Zoom;
            pibBitacora.TabIndex = 21;
            pibBitacora.TabStop = false;
            pibBitacora.Click += pibBitacora_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(93, 34);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(165, 110);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // FRM_SERVICIOS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(365, 385);
            Controls.Add(textBox3);
            Controls.Add(pibBitacora);
            Controls.Add(textBox1);
            Controls.Add(pibGenerarReportes);
            Controls.Add(textBox2);
            Controls.Add(pibCatalogoCuentas);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            MinimizeBox = false;
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