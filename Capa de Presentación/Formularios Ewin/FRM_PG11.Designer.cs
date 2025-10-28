namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class FRM_PG11
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG11));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            panel4 = new Panel();
            label4 = new Label();
            label9 = new Label();
            dataGridView1 = new DataGridView();
            Tarea = new DataGridViewTextBoxColumn();
            Modulo = new DataGridViewTextBoxColumn();
            Realizada_por = new DataGridViewTextBoxColumn();
            Fecha = new DataGridViewTextBoxColumn();
            Hora = new DataGridViewTextBoxColumn();
            Descripción = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            label2 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
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
            // panel4
            // 
            panel4.Location = new Point(175, 192);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(1124, 0);
            label4.Name = "label4";
            label4.Size = new Size(131, 46);
            label4.TabIndex = 16;
            label4.Text = "Volver ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(895, 186);
            label9.Name = "label9";
            label9.Size = new Size(118, 31);
            label9.TabIndex = 20;
            label9.Text = "Parroquia";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Tarea, Modulo, Realizada_por, Fecha, Hora, Descripción });
            dataGridView1.Location = new Point(20, 156);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(857, 388);
            dataGridView1.TabIndex = 37;
            // 
            // Tarea
            // 
            Tarea.HeaderText = "Tarea";
            Tarea.MinimumWidth = 6;
            Tarea.Name = "Tarea";
            // 
            // Modulo
            // 
            Modulo.HeaderText = "Modulo";
            Modulo.MinimumWidth = 6;
            Modulo.Name = "Modulo";
            // 
            // Realizada_por
            // 
            Realizada_por.HeaderText = "Realizada por";
            Realizada_por.MinimumWidth = 6;
            Realizada_por.Name = "Realizada_por";
            // 
            // Fecha
            // 
            Fecha.HeaderText = "Fecha";
            Fecha.MinimumWidth = 6;
            Fecha.Name = "Fecha";
            // 
            // Hora
            // 
            Hora.HeaderText = "Hora";
            Hora.MinimumWidth = 6;
            Hora.Name = "Hora";
            // 
            // Descripción
            // 
            Descripción.HeaderText = "Descripción";
            Descripción.MinimumWidth = 6;
            Descripción.Name = "Descripción";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1258, 695);
            panel1.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(895, 330);
            label2.Name = "label2";
            label2.Size = new Size(160, 31);
            label2.TabIndex = 45;
            label2.Text = "Realizado por";
            // 
            // comboBox2
            // 
            comboBox2.BackColor = Color.FromArgb(251, 203, 51);
            comboBox2.FlatStyle = FlatStyle.Flat;
            comboBox2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "SCJ", "El calvario" });
            comboBox2.Location = new Point(895, 220);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(255, 33);
            comboBox2.TabIndex = 44;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(251, 203, 51);
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "ISaac Chavez", "RAPHI", "LUISIÑHO" });
            comboBox1.Location = new Point(895, 364);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(255, 33);
            comboBox1.TabIndex = 43;
            // 
            // FRM_PG11
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1282, 719);
            Controls.Add(panel1);
            Name = "FRM_PG11";
            Text = "FRM_PG10";
            Load += FRM_PG11_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Panel panel4;
        private Label label4;
        private Label label9;
        private DataGridView dataGridView1;
        private Panel panel1;
        private DataGridViewTextBoxColumn Tarea;
        private DataGridViewTextBoxColumn Modulo;
        private DataGridViewTextBoxColumn Realizada_por;
        private DataGridViewTextBoxColumn Fecha;
        private DataGridViewTextBoxColumn Hora;
        private DataGridViewTextBoxColumn Descripción;
        private Label label2;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
    }
}