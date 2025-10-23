namespace Capa_de_Presentación.Formularios_Diego
{
    partial class FRM_PG114
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG114));
            panel1 = new Panel();
            panel2 = new Panel();
            label6 = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
            dataGridView1 = new DataGridView();
            Nombre = new DataGridViewTextBoxColumn();
            ClmDeposito = new DataGridViewTextBoxColumn();
            ClmPlazos = new DataGridViewTextBoxColumn();
            clmTasa = new DataGridViewTextBoxColumn();
            textBox4 = new TextBox();
            pictureBox9 = new PictureBox();
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
            panel1.Location = new Point(31, 26);
            panel1.Name = "panel1";
            panel1.Size = new Size(845, 496);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(pictureBox9);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(134, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(573, 372);
            panel2.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(122, 34);
            label6.Name = "label6";
            label6.Size = new Size(366, 38);
            label6.TabIndex = 10;
            label6.Text = "Intereses bancarios por CD";
            // 
            // panel4
            // 
            panel4.Location = new Point(175, 192);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(251, 203, 51);
            panel5.Location = new Point(131, 51);
            panel5.Name = "panel5";
            panel5.Size = new Size(579, 378);
            panel5.TabIndex = 9;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Nombre, ClmDeposito, ClmPlazos, clmTasa });
            dataGridView1.Location = new Point(10, 105);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(553, 162);
            dataGridView1.TabIndex = 13;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            Nombre.Width = 125;
            // 
            // ClmDeposito
            // 
            ClmDeposito.HeaderText = "Depósito Inicial";
            ClmDeposito.MinimumWidth = 6;
            ClmDeposito.Name = "ClmDeposito";
            ClmDeposito.ReadOnly = true;
            ClmDeposito.Width = 125;
            // 
            // ClmPlazos
            // 
            ClmPlazos.HeaderText = "Plazo (Meses)";
            ClmPlazos.MinimumWidth = 6;
            ClmPlazos.Name = "ClmPlazos";
            ClmPlazos.ReadOnly = true;
            ClmPlazos.Width = 125;
            // 
            // clmTasa
            // 
            clmTasa.HeaderText = "Tasa";
            clmTasa.MinimumWidth = 6;
            clmTasa.Name = "clmTasa";
            clmTasa.ReadOnly = true;
            clmTasa.Width = 125;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(43, 56, 143);
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox4.ForeColor = Color.White;
            textBox4.Location = new Point(40, 34);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(40, 18);
            textBox4.TabIndex = 31;
            textBox4.Text = "Volver";
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(3, 15);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(119, 57);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 30;
            pictureBox9.TabStop = false;
            // 
            // FRM_PG114
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(907, 548);
            Controls.Add(panel1);
            Name = "FRM_PG114";
            Text = "FRM_PG114";
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
        private Label label6;
        private Panel panel4;
        private Panel panel5;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn ClmDeposito;
        private DataGridViewTextBoxColumn ClmPlazos;
        private DataGridViewTextBoxColumn clmTasa;
        private TextBox textBox4;
        private PictureBox pictureBox9;
    }
}