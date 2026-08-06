
namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class Reportería_Administrador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportería_Administrador));
            panel1 = new Panel();
            label10 = new Label();
            label1 = new Label();
            btn_descargar = new Button();
            cmb_formato_descarga = new ComboBox();
            label9 = new Label();
            panel3 = new Panel();
            lst_reportes = new ListBox();
            label2 = new Label();
            panel2 = new Panel();
            btn_generar = new Button();
            cmb_tipo_reporte = new ComboBox();
            cmb_parroquia = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            dtp_hasta = new DateTimePicker();
            label5 = new Label();
            dtp_desde = new DateTimePicker();
            label3 = new Label();
            label6 = new Label();
            label4 = new Label();
            panel4 = new Panel();
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
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btn_descargar);
            panel1.Controls.Add(cmb_formato_descarga);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(14, 15);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1571, 693);
            panel1.TabIndex = 12;
            panel1.Paint += panel1_Paint;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label10.Location = new Point(466, 134);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(725, 45);
            label10.TabIndex = 30;
            label10.Text = "Genere los reportes que necesite en el instante.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(313, 50);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(1014, 45);
            label1.TabIndex = 29;
            label1.Text = " REPORTES DE INGRESOS, GASTOS, ESTADO DE RESULTADOS ETC.";
            // 
            // btn_descargar
            // 
            btn_descargar.BackColor = Color.FromArgb(43, 56, 143);
            btn_descargar.FlatStyle = FlatStyle.Flat;
            btn_descargar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_descargar.ForeColor = Color.White;
            btn_descargar.ImageAlign = ContentAlignment.TopCenter;
            btn_descargar.Location = new Point(1359, 612);
            btn_descargar.Margin = new Padding(4, 3, 4, 3);
            btn_descargar.Name = "btn_descargar";
            btn_descargar.Size = new Size(171, 43);
            btn_descargar.TabIndex = 11;
            btn_descargar.Text = "Descargar";
            btn_descargar.UseVisualStyleBackColor = false;
            btn_descargar.UseWaitCursor = true;
            btn_descargar.Click += button1_Click;
            // 
            // cmb_formato_descarga
            // 
            cmb_formato_descarga.FormattingEnabled = true;
            cmb_formato_descarga.Items.AddRange(new object[] { "PDF", "DOCX", "JPG" });
            cmb_formato_descarga.Location = new Point(1184, 617);
            cmb_formato_descarga.Margin = new Padding(4, 3, 4, 3);
            cmb_formato_descarga.Name = "cmb_formato_descarga";
            cmb_formato_descarga.Size = new Size(154, 33);
            cmb_formato_descarga.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(901, 615);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(232, 30);
            label9.TabIndex = 11;
            label9.Text = "Formato de descarga";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(251, 203, 51);
            panel3.Controls.Add(lst_reportes);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(901, 240);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(626, 360);
            panel3.TabIndex = 18;
            // 
            // lst_reportes
            // 
            lst_reportes.FormattingEnabled = true;
            lst_reportes.ItemHeight = 25;
            lst_reportes.Location = new Point(4, 72);
            lst_reportes.Margin = new Padding(4, 3, 4, 3);
            lst_reportes.Name = "lst_reportes";
            lst_reportes.Size = new Size(618, 279);
            lst_reportes.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(149, 18);
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
            panel2.Controls.Add(btn_generar);
            panel2.Controls.Add(cmb_tipo_reporte);
            panel2.Controls.Add(cmb_parroquia);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(dtp_hasta);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(dtp_desde);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(26, 240);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(835, 405);
            panel2.TabIndex = 17;
            // 
            // btn_generar
            // 
            btn_generar.BackColor = Color.FromArgb(43, 56, 143);
            btn_generar.FlatStyle = FlatStyle.Flat;
            btn_generar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_generar.ForeColor = Color.Transparent;
            btn_generar.ImageAlign = ContentAlignment.TopCenter;
            btn_generar.Location = new Point(234, 321);
            btn_generar.Margin = new Padding(4, 3, 4, 3);
            btn_generar.Name = "btn_generar";
            btn_generar.Size = new Size(251, 65);
            btn_generar.TabIndex = 10;
            btn_generar.Text = "Generar";
            btn_generar.UseVisualStyleBackColor = false;
            btn_generar.UseWaitCursor = true;
            btn_generar.Click += button2_Click;
            // 
            // cmb_tipo_reporte
            // 
            cmb_tipo_reporte.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_tipo_reporte.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_tipo_reporte.FormattingEnabled = true;
            cmb_tipo_reporte.IntegralHeight = false;
            cmb_tipo_reporte.Location = new Point(523, 129);
            cmb_tipo_reporte.Margin = new Padding(4, 3, 4, 3);
            cmb_tipo_reporte.MaxDropDownItems = 6;
            cmb_tipo_reporte.Name = "cmb_tipo_reporte";
            cmb_tipo_reporte.Size = new Size(294, 33);
            cmb_tipo_reporte.TabIndex = 9;
            cmb_tipo_reporte.SelectedIndexChanged += cmb_tipo_reporte_SelectedIndexChanged;
            // 
            // cmb_parroquia
            // 
            cmb_parroquia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_parroquia.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_parroquia.FormattingEnabled = true;
            cmb_parroquia.IntegralHeight = false;
            cmb_parroquia.Location = new Point(109, 214);
            cmb_parroquia.Margin = new Padding(4, 3, 4, 3);
            cmb_parroquia.MaxDropDownItems = 6;
            cmb_parroquia.Name = "cmb_parroquia";
            cmb_parroquia.Size = new Size(708, 33);
            cmb_parroquia.TabIndex = 8;
            cmb_parroquia.SelectedIndexChanged += cmb_parroquia_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(376, 132);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(139, 25);
            label7.TabIndex = 7;
            label7.Text = "Tipo de reporte:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 217);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(91, 25);
            label8.TabIndex = 6;
            label8.Text = "Parroquia:";
            // 
            // dtp_hasta
            // 
            dtp_hasta.Location = new Point(109, 156);
            dtp_hasta.Margin = new Padding(4, 3, 4, 3);
            dtp_hasta.Name = "dtp_hasta";
            dtp_hasta.Size = new Size(235, 31);
            dtp_hasta.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(39, 162);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(61, 25);
            label5.TabIndex = 4;
            label5.Text = "Hasta:";
            // 
            // dtp_desde
            // 
            dtp_desde.Location = new Point(109, 102);
            dtp_desde.Margin = new Padding(4, 3, 4, 3);
            dtp_desde.Name = "dtp_desde";
            dtp_desde.Size = new Size(237, 31);
            dtp_desde.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 109);
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
            label6.Location = new Point(266, 18);
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
            label4.Location = new Point(1383, 12);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(172, 38);
            label4.TabIndex = 16;
            label4.Text = "Volver atras";
            label4.Click += label4_Click;
            // 
            // panel4
            // 
            panel4.Location = new Point(219, 240);
            panel4.Margin = new Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(4, 3);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 92);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Reportería_Administrador
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1601, 723);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Reportería_Administrador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportería_Administrador";
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
        private PictureBox pictureBox1;
        private Label label2;
        private Label label6;
        private ComboBox cmb_tipo_reporte;
        private ComboBox cmb_parroquia;
        private Label label7;
        private Label label8;
        private DateTimePicker dtp_hasta;
        private Label label5;
        private DateTimePicker dtp_desde;
        private Label label3;
        private Button btn_generar;
        private Button btn_descargar;
        private ComboBox cmb_formato_descarga;
        private Label label9;
        private ListBox lst_reportes;
        private Label label1;
        private Label label10;
    }
}
