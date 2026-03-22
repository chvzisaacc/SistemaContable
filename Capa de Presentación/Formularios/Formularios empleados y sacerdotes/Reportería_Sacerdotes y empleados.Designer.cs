namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_PG49
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG49));
            button1 = new Button();
            cmbFormatoDescarga = new ComboBox();
            label9 = new Label();
            panel3 = new Panel();
            lstReportes = new ListBox();
            label2 = new Label();
            panel2 = new Panel();
            button2 = new Button();
            cmbTipoReporte = new ComboBox();
            label7 = new Label();
            dtpHasta = new DateTimePicker();
            label5 = new Label();
            dtpDesde = new DateTimePicker();
            label3 = new Label();
            label6 = new Label();
            label4 = new Label();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            label8 = new Label();
            label10 = new Label();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.Location = new Point(1395, 602);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(171, 43);
            button1.TabIndex = 22;
            button1.Text = "Descargar";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // cmbFormatoDescarga
            // 
            cmbFormatoDescarga.BackColor = Color.FromArgb(251, 203, 51);
            cmbFormatoDescarga.FlatStyle = FlatStyle.Flat;
            cmbFormatoDescarga.FormattingEnabled = true;
            cmbFormatoDescarga.Items.AddRange(new object[] { "PDF", "DOCX", "JPG" });
            cmbFormatoDescarga.Location = new Point(1204, 608);
            cmbFormatoDescarga.Margin = new Padding(4, 3, 4, 3);
            cmbFormatoDescarga.Name = "cmbFormatoDescarga";
            cmbFormatoDescarga.Size = new Size(154, 33);
            cmbFormatoDescarga.TabIndex = 23;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(944, 607);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(232, 30);
            label9.TabIndex = 24;
            label9.Text = "Formato de descarga";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(251, 203, 51);
            panel3.Controls.Add(lstReportes);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(944, 203);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(626, 386);
            panel3.TabIndex = 27;
            // 
            // lstReportes
            // 
            lstReportes.FormattingEnabled = true;
            lstReportes.ItemHeight = 25;
            lstReportes.Location = new Point(4, 73);
            lstReportes.Margin = new Padding(4, 3, 4, 3);
            lstReportes.Name = "lstReportes";
            lstReportes.Size = new Size(618, 304);
            lstReportes.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(149, 17);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(333, 45);
            label2.TabIndex = 2;
            label2.Text = "Reportes Generados";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(141, 215, 247);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(cmbTipoReporte);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(dtpHasta);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(dtpDesde);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(100, 203);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(754, 434);
            panel2.TabIndex = 26;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(43, 56, 143);
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.ImageAlign = ContentAlignment.TopCenter;
            button2.Location = new Point(202, 275);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(251, 65);
            button2.TabIndex = 10;
            button2.Text = "Generar";
            button2.UseVisualStyleBackColor = false;
            button2.UseWaitCursor = true;
            button2.Click += button2_Click;
            // 
            // cmbTipoReporte
            // 
            cmbTipoReporte.FormattingEnabled = true;
            cmbTipoReporte.Location = new Point(493, 130);
            cmbTipoReporte.Margin = new Padding(4, 3, 4, 3);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(246, 33);
            cmbTipoReporte.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(346, 133);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(139, 25);
            label7.TabIndex = 7;
            label7.Text = "Tipo de reporte:";
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(93, 187);
            dtpHasta.Margin = new Padding(4, 3, 4, 3);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(238, 31);
            dtpHasta.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(23, 193);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(61, 25);
            label5.TabIndex = 4;
            label5.Text = "Hasta:";
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(93, 133);
            dtpDesde.Margin = new Padding(4, 3, 4, 3);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(238, 31);
            dtpDesde.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 140);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(66, 25);
            label3.TabIndex = 2;
            label3.Text = "Desde:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(266, 17);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(158, 45);
            label6.TabIndex = 1;
            label6.Text = "Servicios";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(1416, 8);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(172, 38);
            label4.TabIndex = 25;
            label4.Text = "Volver atras";
            label4.Click += label4_Click;
            // 
            // panel4
            // 
            panel4.Location = new Point(231, 257);
            panel4.Margin = new Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 21;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(13, 8);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(115, 105);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label8.Location = new Point(319, 54);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(1014, 45);
            label8.TabIndex = 28;
            label8.Text = " REPORTES DE INGRESOS, GASTOS, ESTADO DE RESULTADOS ETC.";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label10.Location = new Point(479, 121);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(725, 45);
            label10.TabIndex = 29;
            label10.Text = "Genere los reportes que necesite en el instante.";
            // 
            // FRM_PG49
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1601, 670);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(button1);
            Controls.Add(cmbFormatoDescarga);
            Controls.Add(label9);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(label4);
            Controls.Add(panel4);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "FRM_PG49";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportería_Sacerdotes y empleados";
            Load += FRM_PG49_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox cmbFormatoDescarga;
        private Label label9;
        private Panel panel3;
        private ListBox lstReportes;
        private Label label2;
        private Panel panel2;
        private Button button2;
        private ComboBox cmbTipoReporte;
        private Label label7;
        private DateTimePicker dtpHasta;
        private Label label5;
        private DateTimePicker dtpDesde;
        private Label label3;
        private Label label6;
        private Label label4;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Label label8;
        private Label label10;
    }
}