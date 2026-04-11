
namespace Capa_de_Presentación.Formularios_Diego
{
    partial class Certificados_De_Depósito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Certificados_De_Depósito));
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            Id_Certificado = new DataGridViewTextBoxColumn();
            Nombre_certificado = new DataGridViewTextBoxColumn();
            ParroquiaID = new DataGridViewTextBoxColumn();
            FechaTransaccion = new DataGridViewTextBoxColumn();
            textBox4 = new TextBox();
            pictureBox9 = new PictureBox();
            pictureBox7 = new PictureBox();
            textBox3 = new TextBox();
            pictureBox6 = new PictureBox();
            textBox1 = new TextBox();
            pictureBox5 = new PictureBox();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            label6 = new Label();
            panel4 = new Panel();
            pictureBox1 = new PictureBox();
            panel5 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel5);
            panel1.Location = new Point(36, 27);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1056, 620);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(191, 48);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(685, 76);
            label1.TabIndex = 11;
            label1.Text = "Bienvenido a la gestión de certificados de depósito\r\n\r\n";
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(pictureBox9);
            panel2.Controls.Add(pictureBox7);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(pictureBox6);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(pictureBox5);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(32, 130);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(995, 461);
            panel2.TabIndex = 8;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Location = new Point(17, 407);
            button1.Name = "button1";
            button1.Size = new Size(41, 35);
            button1.TabIndex = 31;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id_Certificado, Nombre_certificado, ParroquiaID, FechaTransaccion });
            dataGridView1.Location = new Point(63, 98);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(828, 254);
            dataGridView1.TabIndex = 30;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            // 
            // Id_Certificado
            // 
            Id_Certificado.HeaderText = "Id_Certificado";
            Id_Certificado.MinimumWidth = 8;
            Id_Certificado.Name = "Id_Certificado";
            Id_Certificado.ReadOnly = true;
            Id_Certificado.Visible = false;
            // 
            // Nombre_certificado
            // 
            Nombre_certificado.HeaderText = "Nombre_certificado";
            Nombre_certificado.MinimumWidth = 8;
            Nombre_certificado.Name = "Nombre_certificado";
            Nombre_certificado.ReadOnly = true;
            // 
            // ParroquiaID
            // 
            ParroquiaID.HeaderText = "Parroquia";
            ParroquiaID.MinimumWidth = 8;
            ParroquiaID.Name = "ParroquiaID";
            ParroquiaID.ReadOnly = true;
            // 
            // FechaTransaccion
            // 
            FechaTransaccion.HeaderText = "Fecha";
            FechaTransaccion.MinimumWidth = 8;
            FechaTransaccion.Name = "FechaTransaccion";
            FechaTransaccion.ReadOnly = true;
            FechaTransaccion.Visible = false;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(43, 56, 143);
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox4.ForeColor = Color.White;
            textBox4.Location = new Point(87, 23);
            textBox4.Margin = new Padding(4, 3, 4, 3);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(72, 23);
            textBox4.TabIndex = 29;
            textBox4.Text = "Volver";
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(17, -3);
            pictureBox9.Margin = new Padding(4, 3, 4, 3);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(186, 77);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 28;
            pictureBox9.TabStop = false;
            pictureBox9.Click += pictureBox9_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(898, 194);
            pictureBox7.Margin = new Padding(4, 3, 4, 3);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(59, 60);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 26;
            pictureBox7.TabStop = false;
            pictureBox7.Click += pictureBox7_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(43, 56, 143);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox3.ForeColor = Color.White;
            textBox3.Location = new Point(706, 398);
            textBox3.Margin = new Padding(4, 3, 4, 3);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(80, 31);
            textBox3.TabIndex = 24;
            textBox3.Text = "Guardar";
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(626, 369);
            pictureBox6.Margin = new Padding(4, 3, 4, 3);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(240, 89);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 25;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(43, 56, 143);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(459, 398);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(83, 31);
            textBox1.TabIndex = 22;
            textBox1.Text = "Cancelar";
            textBox1.Click += textBox1_Click;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(378, 368);
            pictureBox5.Margin = new Padding(4, 3, 4, 3);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(240, 90);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 23;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(212, 398);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(81, 31);
            textBox2.TabIndex = 16;
            textBox2.Text = "Renovar";
            textBox2.Click += textBox2_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(130, 369);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(240, 89);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 21;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.FlatStyle = FlatStyle.Popup;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(284, 23);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.No;
            label6.Size = new Size(382, 45);
            label6.TabIndex = 10;
            label6.Text = "Certificado de depósito\r\n";
            // 
            // panel4
            // 
            panel4.Location = new Point(191, 210);
            panel4.Margin = new Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(90, 88);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(43, 56, 143);
            panel5.Location = new Point(30, 127);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(1003, 468);
            panel5.TabIndex = 9;
            // 
            // Certificados_De_Depósito
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1134, 685);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Certificados_De_Depósito";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Certificados_De_Depósito";
            Load += FRM_PG103_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label6;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Panel panel5;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox3;
        private PictureBox pictureBox6;
        private TextBox textBox1;
        private PictureBox pictureBox5;
        private TextBox textBox4;
        private PictureBox pictureBox9;
        private PictureBox pictureBox7;
        private DataGridView dataGridView1;
        private Button button1;
        private DataGridViewTextBoxColumn Id_Certificado;
        private DataGridViewTextBoxColumn Nombre_certificado;
        private DataGridViewTextBoxColumn ParroquiaID;
        private DataGridViewTextBoxColumn FechaTransaccion;
        private Label label1;
    }
}
