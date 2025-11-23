namespace Capa_de_Presentación.Formularios_Diego
{
    partial class FRM_PG82
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG82));
            panel1 = new Panel();
            label8 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            cmbAcciones = new ComboBox();
            textBox3 = new TextBox();
            pictureBox3 = new PictureBox();
            cmbIntereses = new ComboBox();
            cmbCuentas = new ComboBox();
            panel3 = new Panel();
            panel2 = new Panel();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            label7 = new Label();
            comboBox1 = new ComboBox();
            label6 = new Label();
            panel4 = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel5 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label8);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox5);
            panel1.Controls.Add(cmbAcciones);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(cmbIntereses);
            panel1.Controls.Add(cmbCuentas);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel5);
            panel1.Location = new Point(29, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(845, 496);
            panel1.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(251, 203, 51);
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(43, 56, 143);
            label8.Location = new Point(756, 27);
            label8.Name = "label8";
            label8.Size = new Size(38, 28);
            label8.TabIndex = 19;
            label8.Text = "SD";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(733, 3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(95, 88);
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox4.TabIndex = 18;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(650, 3);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(77, 70);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 17;
            pictureBox5.TabStop = false;
            // 
            // cmbAcciones
            // 
            cmbAcciones.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbAcciones.FormattingEnabled = true;
            cmbAcciones.Items.AddRange(new object[] { "Agregar saldo", "Transferencia entre cuentas", "Agregar cuenta bancaria", "Retirar dinero" });
            cmbAcciones.Location = new Point(495, 228);
            cmbAcciones.Name = "cmbAcciones";
            cmbAcciones.Size = new Size(151, 36);
            cmbAcciones.TabIndex = 16;
            cmbAcciones.Text = "     Acciones";
            cmbAcciones.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(251, 203, 51);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(76, 386);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(162, 18);
            textBox3.TabIndex = 15;
            textBox3.Text = "Certificado de \r\ndepósito";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(20, 358);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(269, 82);
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // cmbIntereses
            // 
            cmbIntereses.BackColor = Color.FromArgb(251, 203, 51);
            cmbIntereses.FlatStyle = FlatStyle.Flat;
            cmbIntereses.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbIntereses.FormattingEnabled = true;
            cmbIntereses.Location = new Point(42, 297);
            cmbIntereses.Name = "cmbIntereses";
            cmbIntereses.Size = new Size(205, 33);
            cmbIntereses.TabIndex = 13;
            cmbIntereses.Text = " Intereses Bancarios";
            // 
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.FlatStyle = FlatStyle.Flat;
            cmbCuentas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Location = new Point(42, 215);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(205, 33);
            cmbCuentas.TabIndex = 12;
            cmbCuentas.Text = "            Cuentas";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(43, 56, 143);
            panel3.Enabled = false;
            panel3.Location = new Point(0, 173);
            panel3.Name = "panel3";
            panel3.Size = new Size(845, 3);
            panel3.TabIndex = 8;
            panel3.Paint += panel3_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(194, 209);
            panel2.Name = "panel2";
            panel2.Size = new Size(0, 0);
            panel2.TabIndex = 8;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(137, 177);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(164, 27);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(88, 162);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(254, 67);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 13;
            pictureBox2.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(137, 115);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(205, 31);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(38, 115);
            label7.Name = "label7";
            label7.Size = new Size(93, 31);
            label7.TabIndex = 10;
            label7.Text = "Monto:";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(251, 203, 51);
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(137, 61);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(205, 33);
            comboBox1.TabIndex = 11;
            comboBox1.Text = "      Cuentas";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(59, 10);
            label6.Name = "label6";
            label6.Size = new Size(307, 38);
            label6.TabIndex = 10;
            label6.Text = "Seleccione una cuenta";
            // 
            // panel4
            // 
            panel4.Location = new Point(175, 192);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(664, 138);
            label5.Name = "label5";
            label5.Size = new Size(90, 31);
            label5.TabIndex = 5;
            label5.Text = "Bancos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(425, 138);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 4;
            label4.Text = "Caja Chica";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(223, 138);
            label3.Name = "label3";
            label3.Size = new Size(85, 31);
            label3.TabIndex = 3;
            label3.Text = "Gastos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(20, 139);
            label2.Name = "label2";
            label2.Size = new Size(104, 31);
            label2.TabIndex = 2;
            label2.Text = "Ingresos";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(134, 39);
            label1.Name = "label1";
            label1.Size = new Size(155, 38);
            label1.TabIndex = 1;
            label1.Text = "Sacerdote";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(20, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(97, 88);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(43, 56, 143);
            panel5.Location = new Point(191, 206);
            panel5.Name = "panel5";
            panel5.Size = new Size(0, 0);
            panel5.TabIndex = 9;
            // 
            // FRM_PG82
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(907, 548);
            Controls.Add(panel1);
            Name = "FRM_PG82";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRM_PG82";
            Load += FRM_PG82_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private Label label7;
        private ComboBox comboBox1;
        private Label label6;
        private Panel panel4;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private Panel panel5;
        private Panel panel3;
        private ComboBox cmbIntereses;
        private ComboBox cmbCuentas;
        private PictureBox pictureBox3;
        private TextBox textBox3;
        private ComboBox cmbAcciones;
        private Label label8;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
    }
}