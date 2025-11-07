namespace Capa_de_Presentación
{
    partial class FRM_PG38
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG38));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            btnVolver = new Button();
            cmbUsuario = new ComboBox();
            lblRealizadopor = new Label();
            dgvBitacora = new DataGridView();
            lblParroquia = new Label();
            cmbParroquia = new ComboBox();
            lblConsulte = new Label();
            lblTitulo = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.BackgroundImageLayout = ImageLayout.None;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(cmbUsuario);
            panel1.Controls.Add(lblRealizadopor);
            panel1.Controls.Add(dgvBitacora);
            panel1.Controls.Add(lblParroquia);
            panel1.Controls.Add(cmbParroquia);
            panel1.Controls.Add(lblConsulte);
            panel1.Controls.Add(lblTitulo);
            panel1.Location = new Point(14, 16);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1243, 690);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(45, 0);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(150, 163);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label1.Location = new Point(233, 40);
            label1.Name = "label1";
            label1.Size = new Size(255, 38);
            label1.TabIndex = 8;
            label1.Text = "ADMINISTRADOR";
            label1.Click += label1_Click_1;
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1028, 30);
            btnVolver.Margin = new Padding(3, 4, 3, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(193, 51);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // cmbUsuario
            // 
            cmbUsuario.BackColor = Color.Gold;
            cmbUsuario.FlatStyle = FlatStyle.Popup;
            cmbUsuario.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.Items.AddRange(new object[] { "", "Isaac Chavez", "Diego Muñoz" });
            cmbUsuario.Location = new Point(1029, 536);
            cmbUsuario.Margin = new Padding(3, 4, 3, 4);
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(180, 45);
            cmbUsuario.TabIndex = 6;
            cmbUsuario.Text = "Seleccionar";
            cmbUsuario.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lblRealizadopor
            // 
            lblRealizadopor.AutoSize = true;
            lblRealizadopor.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblRealizadopor.Location = new Point(1027, 478);
            lblRealizadopor.Name = "lblRealizadopor";
            lblRealizadopor.Size = new Size(204, 38);
            lblRealizadopor.TabIndex = 5;
            lblRealizadopor.Text = "Realizado por:";
            lblRealizadopor.TextAlign = ContentAlignment.BottomCenter;
            lblRealizadopor.Click += label1_Click;
            // 
            // dgvBitacora
            // 
            dgvBitacora.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.MenuText;
            dgvBitacora.Location = new Point(67, 272);
            dgvBitacora.Margin = new Padding(3, 4, 3, 4);
            dgvBitacora.Name = "dgvBitacora";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBitacora.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBitacora.RowHeadersWidth = 51;
            dgvBitacora.Size = new Size(954, 400);
            dgvBitacora.TabIndex = 4;
            dgvBitacora.CellContentClick += dataGridView1_CellContentClick;
            // 
            // lblParroquia
            // 
            lblParroquia.AutoSize = true;
            lblParroquia.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblParroquia.Location = new Point(1027, 310);
            lblParroquia.Name = "lblParroquia";
            lblParroquia.Size = new Size(152, 38);
            lblParroquia.TabIndex = 3;
            lblParroquia.Text = "Parroquia:";
            // 
            // cmbParroquia
            // 
            cmbParroquia.BackColor = Color.Gold;
            cmbParroquia.FlatStyle = FlatStyle.Popup;
            cmbParroquia.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbParroquia.FormattingEnabled = true;
            cmbParroquia.Items.AddRange(new object[] { "", "SCJ", "El Calvario" });
            cmbParroquia.Location = new Point(1028, 369);
            cmbParroquia.Margin = new Padding(3, 4, 3, 4);
            cmbParroquia.Name = "cmbParroquia";
            cmbParroquia.Size = new Size(181, 45);
            cmbParroquia.TabIndex = 2;
            cmbParroquia.Text = "Seleccionar";
            cmbParroquia.SelectedIndexChanged += cmbParroquia_SelectedIndexChanged;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblConsulte.Location = new Point(67, 231);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(1001, 38);
            lblConsulte.TabIndex = 1;
            lblConsulte.Text = "Consulte en cualquier momento que hizo cada encargado de las parroquias";
            lblConsulte.Click += lblConsulte_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblTitulo.Location = new Point(67, 167);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(991, 38);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "BITACORA DEL SISTEMA, CADA ACCIÓN DEL SISTEMA REGISTRADA AQUÍ";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // FRM_PG38
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1282, 719);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FRM_PG38";
            Text = "FRM_PG38";
            Load += FRM_PG38_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblTitulo;
        private Label lblParroquia;
        private ComboBox cmbParroquia;
        private Label lblConsulte;
        private DataGridView dgvBitacora;
        private Label lblRealizadopor;
        private ComboBox cmbUsuario;
        private Button btnVolver;
        private Label label1;
        private PictureBox pictureBox3;
    }
}