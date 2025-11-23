
namespace Capa_de_Presentación.Formularios_Diego
{
    partial class FRM_PG77
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG77));
            panel1 = new Panel();
            panel6 = new Panel();
            panel3 = new Panel();
            label8 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            panel2 = new Panel();
            txtMonto = new TextBox();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            label7 = new Label();
            cmbCuentas = new ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(pictureBox3);
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
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(43, 56, 143);
            panel6.Enabled = false;
            panel6.Location = new Point(0, 173);
            panel6.Name = "panel6";
            panel6.Size = new Size(845, 3);
            panel6.TabIndex = 13;
            // 
            // panel3
            // 
            panel3.Enabled = false;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(845, 3);
            panel3.TabIndex = 7;
            panel3.Paint += panel3_Paint;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(251, 203, 51);
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(43, 56, 143);
            label8.Location = new Point(756, 29);
            label8.Name = "label8";
            label8.Size = new Size(38, 28);
            label8.TabIndex = 12;
            label8.Text = "SD";
            label8.Click += label8_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(735, 3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(95, 88);
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox4.TabIndex = 11;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(651, 13);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(77, 67);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtMonto);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(cmbCuentas);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(223, 209);
            panel2.Name = "panel2";
            panel2.Size = new Size(406, 232);
            panel2.TabIndex = 8;
            panel2.Paint += panel2_Paint;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(186, 115);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(139, 24);
            txtMonto.TabIndex = 19;
            txtMonto.Text = "Ingrese monto";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(150, 184);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(130, 23);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(88, 163);
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
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.FlatStyle = FlatStyle.Flat;
            cmbCuentas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Location = new Point(137, 61);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(205, 33);
            cmbCuentas.TabIndex = 11;
            cmbCuentas.Text = "      Cuentas";
            cmbCuentas.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(59, 11);
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
            label5.Location = new Point(664, 139);
            label5.Name = "label5";
            label5.Size = new Size(90, 31);
            label5.TabIndex = 5;
            label5.Text = "Bancos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(425, 139);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 4;
            label4.Text = "Caja Chica";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(223, 139);
            label3.Name = "label3";
            label3.Size = new Size(85, 31);
            label3.TabIndex = 3;
            label3.Text = "Gastos";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(21, 139);
            label2.Name = "label2";
            label2.Size = new Size(104, 31);
            label2.TabIndex = 2;
            label2.Text = "Ingresos";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(135, 43);
            label1.Name = "label1";
            label1.Size = new Size(155, 38);
            label1.TabIndex = 1;
            label1.Text = "Sacerdote";
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
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(43, 56, 143);
            panel5.Location = new Point(219, 205);
            panel5.Name = "panel5";
            panel5.Size = new Size(415, 243);
            panel5.TabIndex = 9;
            // 
            // FRM_PG77
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(907, 548);
            Controls.Add(panel1);
            Name = "FRM_PG77";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRM_PG77";
            Load += FRM_PG77_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label3;
        private Label label2;
        private Label label5;
        private Label label4;
        private Panel panel3;
        private Panel panel2;
        private TextBox textBox1;
        private Label label7;
        private ComboBox cmbCuentas;
        private Label label6;
        private Panel panel4;
        private Panel panel5;
        private PictureBox pictureBox2;
        private TextBox textBox2;
        private PictureBox pictureBox3;
        private Label label8;
        private PictureBox pictureBox4;
        private Panel panel6;
        private TextBox txtMonto;
    }
}