namespace Capa_de_Presentación.Formularios_Diego
{
    partial class Intereses_Por_Cds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Intereses_Por_Cds));
            panel1 = new Panel();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            Id_certificado = new DataGridViewTextBoxColumn();
            Nombre_Certificado = new DataGridViewTextBoxColumn();
            Depósito_inicial = new DataGridViewTextBoxColumn();
            Plazo = new DataGridViewTextBoxColumn();
            Tasa_de_interes = new DataGridViewTextBoxColumn();
            Ganancia = new DataGridViewTextBoxColumn();
            TotalAcumulado = new DataGridViewTextBoxColumn();
            textBox4 = new TextBox();
            pictureBox9 = new PictureBox();
            label6 = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
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
            panel1.Location = new Point(39, 32);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1056, 620);
            panel1.TabIndex = 4;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(pictureBox9);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(168, 68);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(716, 465);
            panel2.TabIndex = 8;
            panel2.Paint += panel2_Paint;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id_certificado, Nombre_Certificado, Depósito_inicial, Plazo, Tasa_de_interes, Ganancia, TotalAcumulado });
            dataGridView1.Location = new Point(31, 179);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(664, 225);
            dataGridView1.TabIndex = 32;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Id_certificado
            // 
            Id_certificado.HeaderText = "Id_certificado";
            Id_certificado.MinimumWidth = 8;
            Id_certificado.Name = "Id_certificado";
            Id_certificado.ReadOnly = true;
            Id_certificado.Visible = false;
            Id_certificado.Width = 150;
            // 
            // Nombre_Certificado
            // 
            Nombre_Certificado.HeaderText = "Nombre_Certificado";
            Nombre_Certificado.MinimumWidth = 8;
            Nombre_Certificado.Name = "Nombre_Certificado";
            Nombre_Certificado.ReadOnly = true;
            Nombre_Certificado.Width = 150;
            // 
            // Depósito_inicial
            // 
            Depósito_inicial.HeaderText = "Depósito_inicial";
            Depósito_inicial.MinimumWidth = 8;
            Depósito_inicial.Name = "Depósito_inicial";
            Depósito_inicial.ReadOnly = true;
            Depósito_inicial.Width = 150;
            // 
            // Plazo
            // 
            Plazo.HeaderText = "Plazo";
            Plazo.MinimumWidth = 8;
            Plazo.Name = "Plazo";
            Plazo.ReadOnly = true;
            Plazo.Visible = false;
            Plazo.Width = 150;
            // 
            // Tasa_de_interes
            // 
            Tasa_de_interes.HeaderText = "Tasa_de_interes";
            Tasa_de_interes.MinimumWidth = 8;
            Tasa_de_interes.Name = "Tasa_de_interes";
            Tasa_de_interes.ReadOnly = true;
            Tasa_de_interes.Visible = false;
            Tasa_de_interes.Width = 150;
            // 
            // Ganancia
            // 
            Ganancia.HeaderText = "Ganancia Generado";
            Ganancia.MinimumWidth = 8;
            Ganancia.Name = "Ganancia";
            Ganancia.ReadOnly = true;
            Ganancia.Width = 150;
            // 
            // TotalAcumulado
            // 
            TotalAcumulado.HeaderText = "Total Acumulado";
            TotalAcumulado.MinimumWidth = 8;
            TotalAcumulado.Name = "TotalAcumulado";
            TotalAcumulado.ReadOnly = true;
            TotalAcumulado.Width = 150;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(43, 56, 143);
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox4.ForeColor = Color.White;
            textBox4.Location = new Point(50, 42);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(50, 21);
            textBox4.TabIndex = 31;
            textBox4.Text = "Volver";
            textBox4.Click += textBox4_Click;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(4, 19);
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
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(152, 42);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(434, 45);
            label6.TabIndex = 10;
            label6.Text = "Intereses bancarios por CD";
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
            panel5.Location = new Point(164, 64);
            panel5.Margin = new Padding(4);
            panel5.Name = "panel5";
            panel5.Size = new Size(724, 472);
            panel5.TabIndex = 9;
            // 
            // Intereses_Por_Cds
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1134, 685);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "Intereses_Por_Cds";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Intereses_Por_Cds";
            Load += FRM_PG114_Load;
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
        private TextBox textBox4;
        private PictureBox pictureBox9;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Id_certificado;
        private DataGridViewTextBoxColumn Nombre_Certificado;
        private DataGridViewTextBoxColumn Depósito_inicial;
        private DataGridViewTextBoxColumn Plazo;
        private DataGridViewTextBoxColumn Tasa_de_interes;
        private DataGridViewTextBoxColumn Ganancia;
        private DataGridViewTextBoxColumn TotalAcumulado;
    }
}