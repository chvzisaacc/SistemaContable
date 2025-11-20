namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class FRM_PG10
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG10));
            panel1 = new Panel();
            button1 = new Button();
            cmbFormatoDescarga = new ComboBox();
            label9 = new Label();
            panel3 = new Panel();
            lstReportes = new ListBox();
            label2 = new Label();
            panel2 = new Panel();
            button2 = new Button();
            cmbTipoReporte = new ComboBox();
            cmbParroquia = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            dtpHasta = new DateTimePicker();
            label5 = new Label();
            dtpDesde = new DateTimePicker();
            label3 = new Label();
            label6 = new Label();
            label4 = new Label();
            panel4 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(cmbFormatoDescarga);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(11, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1258, 695);
            panel1.TabIndex = 12;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.Location = new Point(1086, 489);
            button1.Name = "button1";
            button1.Size = new Size(137, 35);
            button1.TabIndex = 11;
            button1.Text = "Descargar";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // cmbFormatoDescarga
            // 
            cmbFormatoDescarga.FormattingEnabled = true;
            cmbFormatoDescarga.Items.AddRange(new object[] { "PDF", "DOCX", "JPG" });
            cmbFormatoDescarga.Location = new Point(947, 493);
            cmbFormatoDescarga.Name = "cmbFormatoDescarga";
            cmbFormatoDescarga.Size = new Size(124, 28);
            cmbFormatoDescarga.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(722, 492);
            label9.Name = "label9";
            label9.Size = new Size(190, 25);
            label9.TabIndex = 11;
            label9.Text = "Formato de descarga";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(251, 203, 51);
            panel3.Controls.Add(lstReportes);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(722, 192);
            panel3.Name = "panel3";
            panel3.Size = new Size(501, 288);
            panel3.TabIndex = 18;
            // 
            // lstReportes
            // 
            lstReportes.FormattingEnabled = true;
            lstReportes.Items.AddRange(new object[] { "Queso" });
            lstReportes.Location = new Point(3, 57);
            lstReportes.Name = "lstReportes";
            lstReportes.Size = new Size(495, 224);
            lstReportes.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(119, 15);
            label2.Name = "label2";
            label2.Size = new Size(280, 38);
            label2.TabIndex = 2;
            label2.Text = "Reportes Generados";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(141, 215, 247);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(cmbTipoReporte);
            panel2.Controls.Add(cmbParroquia);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(dtpHasta);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(dtpDesde);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(88, 192);
            panel2.Name = "panel2";
            panel2.Size = new Size(549, 288);
            panel2.TabIndex = 17;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(43, 56, 143);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Transparent;
            button2.ImageAlign = ContentAlignment.TopCenter;
            button2.Location = new Point(178, 201);
            button2.Name = "button2";
            button2.Size = new Size(201, 52);
            button2.TabIndex = 10;
            button2.Text = "Generar";
            button2.UseVisualStyleBackColor = false;
            button2.UseWaitCursor = true;
            button2.Click += button2_Click;
            // 
            // cmbTipoReporte
            // 
            cmbTipoReporte.FormattingEnabled = true;
            cmbTipoReporte.Location = new Point(334, 127);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(189, 28);
            cmbTipoReporte.TabIndex = 9;
            // 
            // cmbParroquia
            // 
            cmbParroquia.FormattingEnabled = true;
            cmbParroquia.Location = new Point(334, 84);
            cmbParroquia.Name = "cmbParroquia";
            cmbParroquia.Size = new Size(189, 28);
            cmbParroquia.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(223, 133);
            label7.Name = "label7";
            label7.Size = new Size(116, 20);
            label7.TabIndex = 7;
            label7.Text = "Tipo de reporte:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(257, 91);
            label8.Name = "label8";
            label8.Size = new Size(75, 20);
            label8.TabIndex = 6;
            label8.Text = "Parroquia:";
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(70, 125);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(146, 27);
            dtpHasta.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 131);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 4;
            label5.Text = "Hasta:";
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(70, 83);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(146, 27);
            dtpDesde.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 88);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 2;
            label3.Text = "Desde:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(213, 15);
            label6.Name = "label6";
            label6.Size = new Size(133, 38);
            label6.TabIndex = 1;
            label6.Text = "Servicios";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(1120, 13);
            label4.Name = "label4";
            label4.Size = new Size(131, 46);
            label4.TabIndex = 16;
            label4.Text = "Volver ";
            label4.Click += label4_Click;
            // 
            // panel4
            // 
            panel4.Location = new Point(175, 192);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(134, 39);
            label1.Name = "label1";
            label1.Size = new Size(221, 38);
            label1.TabIndex = 1;
            label1.Text = "Administrador";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(97, 88);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // FRM_PG10
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1282, 719);
            Controls.Add(panel1);
            Name = "FRM_PG10";
            Text = "FRM_PG10";
            Load += FRM_PG10_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Label label4;
        private Panel panel4;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label6;
        private ComboBox cmbTipoReporte;
        private ComboBox cmbParroquia;
        private Label label7;
        private Label label8;
        private DateTimePicker dtpHasta;
        private Label label5;
        private DateTimePicker dtpDesde;
        private Label label3;
        private Button button2;
        private Button button1;
        private ComboBox cmbFormatoDescarga;
        private Label label9;
        private ListBox lstReportes;
    }
}