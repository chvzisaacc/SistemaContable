namespace Capa_de_Presentación.Formularios_Diego
{
    partial class FRM116
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM116));
            panel1 = new Panel();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            textBox4 = new TextBox();
            pictureBox9 = new PictureBox();
            label6 = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
            Nombre = new DataGridViewTextBoxColumn();
            Saldo = new DataGridViewTextBoxColumn();
            Interés = new DataGridViewTextBoxColumn();
            GananciaGenerada = new DataGridViewTextBoxColumn();
            TotalAcumulado = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel5);
            panel1.Location = new Point(39, 33);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1056, 620);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(pictureBox9);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(83, 68);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(898, 465);
            panel2.TabIndex = 8;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nombre, Saldo, Interés, GananciaGenerada, TotalAcumulado });
            dataGridView1.Location = new Point(41, 159);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(814, 225);
            dataGridView1.TabIndex = 32;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(43, 56, 143);
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox4.ForeColor = Color.White;
            textBox4.Location = new Point(51, 25);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(50, 21);
            textBox4.TabIndex = 31;
            textBox4.Text = "Volver";
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(4, 0);
            pictureBox9.Margin = new Padding(4);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(149, 71);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 30;
            pictureBox9.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(199, 76);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(535, 38);
            label6.TabIndex = 10;
            label6.Text = "Intereses bancarios por cuenta bancaria";
            // 
            // panel4
            // 
            panel4.Location = new Point(219, 240);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(251, 203, 51);
            panel5.Location = new Point(79, 64);
            panel5.Margin = new Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new Size(906, 472);
            panel5.TabIndex = 9;
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nombre.HeaderText = "Nombre de la Cuenta";
            Nombre.MinimumWidth = 8;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            // 
            // Saldo
            // 
            Saldo.HeaderText = "Saldo de la Cuenta";
            Saldo.MinimumWidth = 8;
            Saldo.Name = "Saldo";
            Saldo.ReadOnly = true;
            Saldo.Width = 140;
            // 
            // Interés
            // 
            Interés.HeaderText = "Tasa";
            Interés.MinimumWidth = 8;
            Interés.Name = "Interés";
            Interés.ReadOnly = true;
            Interés.Width = 120;
            // 
            // GananciaGenerada
            // 
            GananciaGenerada.HeaderText = "Ganancia Generada";
            GananciaGenerada.MinimumWidth = 8;
            GananciaGenerada.Name = "GananciaGenerada";
            GananciaGenerada.ReadOnly = true;
            GananciaGenerada.Width = 140;
            // 
            // TotalAcumulado
            // 
            TotalAcumulado.HeaderText = "Total Acumulado";
            TotalAcumulado.MinimumWidth = 8;
            TotalAcumulado.Name = "TotalAcumulado";
            TotalAcumulado.ReadOnly = true;
            TotalAcumulado.Width = 140;
            // 
            // FRM116
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1134, 686);
            Controls.Add(panel1);
            Name = "FRM116";
            Text = "FRM116";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox textBox4;
        private PictureBox pictureBox9;
        private Label label6;
        private Panel panel4;
        private Panel panel5;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Saldo;
        private DataGridViewTextBoxColumn Interés;
        private DataGridViewTextBoxColumn GananciaGenerada;
        private DataGridViewTextBoxColumn TotalAcumulado;
    }
}