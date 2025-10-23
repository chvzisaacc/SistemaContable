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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            btnVolver = new Button();
            comboBox1 = new ComboBox();
            lblRealizadopor = new Label();
            dgvBitacora = new DataGridView();
            cTarea = new DataGridViewTextBoxColumn();
            cModulo = new DataGridViewTextBoxColumn();
            cRealizadopor = new DataGridViewTextBoxColumn();
            cFecha = new DataGridViewTextBoxColumn();
            cHora = new DataGridViewTextBoxColumn();
            cDescripcion = new DataGridViewTextBoxColumn();
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
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(lblRealizadopor);
            panel1.Controls.Add(dgvBitacora);
            panel1.Controls.Add(lblParroquia);
            panel1.Controls.Add(cmbParroquia);
            panel1.Controls.Add(lblConsulte);
            panel1.Controls.Add(lblTitulo);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1260, 703);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(39, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(131, 122);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(204, 30);
            label1.Name = "label1";
            label1.Size = new Size(219, 32);
            label1.TabIndex = 8;
            label1.Text = "ADMINISTRADOR";
            label1.Click += label1_Click_1;
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1072, 26);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(134, 38);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.Gold;
            comboBox1.FlatStyle = FlatStyle.Popup;
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "", "Isaac Chavez", "Diego Muñoz" });
            comboBox1.Location = new Point(1057, 388);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 29);
            comboBox1.TabIndex = 6;
            comboBox1.Text = "Seleccionar";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lblRealizadopor
            // 
            lblRealizadopor.AutoSize = true;
            lblRealizadopor.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRealizadopor.Location = new Point(1057, 352);
            lblRealizadopor.Name = "lblRealizadopor";
            lblRealizadopor.Size = new Size(118, 21);
            lblRealizadopor.TabIndex = 5;
            lblRealizadopor.Text = "Realizado por:";
            lblRealizadopor.Click += label1_Click;
            // 
            // dgvBitacora
            // 
            dgvBitacora.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 113, 187);
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Columns.AddRange(new DataGridViewColumn[] { cTarea, cModulo, cRealizadopor, cFecha, cHora, cDescripcion });
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.MenuText;
            dgvBitacora.Location = new Point(59, 220);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.Size = new Size(944, 392);
            dgvBitacora.TabIndex = 4;
            dgvBitacora.CellContentClick += dataGridView1_CellContentClick;
            // 
            // cTarea
            // 
            cTarea.HeaderText = "Tarea";
            cTarea.Name = "cTarea";
            cTarea.Width = 150;
            // 
            // cModulo
            // 
            cModulo.HeaderText = "Módulo";
            cModulo.Name = "cModulo";
            cModulo.Width = 150;
            // 
            // cRealizadopor
            // 
            cRealizadopor.HeaderText = "Realizado por";
            cRealizadopor.Name = "cRealizadopor";
            cRealizadopor.Width = 150;
            // 
            // cFecha
            // 
            cFecha.HeaderText = "Fecha";
            cFecha.Name = "cFecha";
            cFecha.Width = 150;
            // 
            // cHora
            // 
            cHora.HeaderText = "Hora";
            cHora.Name = "cHora";
            cHora.Width = 150;
            // 
            // cDescripcion
            // 
            cDescripcion.HeaderText = "Descripción";
            cDescripcion.Name = "cDescripcion";
            cDescripcion.Width = 150;
            // 
            // lblParroquia
            // 
            lblParroquia.AutoSize = true;
            lblParroquia.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblParroquia.Location = new Point(1057, 220);
            lblParroquia.Name = "lblParroquia";
            lblParroquia.Size = new Size(86, 21);
            lblParroquia.TabIndex = 3;
            lblParroquia.Text = "Parroquia:";
            // 
            // cmbParroquia
            // 
            cmbParroquia.BackColor = Color.Gold;
            cmbParroquia.FlatStyle = FlatStyle.Popup;
            cmbParroquia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbParroquia.FormattingEnabled = true;
            cmbParroquia.Items.AddRange(new object[] { "", "SCJ", "El Calvario" });
            cmbParroquia.Location = new Point(1057, 262);
            cmbParroquia.Name = "cmbParroquia";
            cmbParroquia.Size = new Size(121, 29);
            cmbParroquia.TabIndex = 2;
            cmbParroquia.Text = "Seleccionar";
            cmbParroquia.SelectedIndexChanged += cmbParroquia_SelectedIndexChanged;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(59, 173);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(752, 30);
            lblConsulte.TabIndex = 1;
            lblConsulte.Text = "Consulte en cualquier momento que hizo cada encargado de las parroquias";
            lblConsulte.Click += lblConsulte_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(59, 125);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(745, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "BITACORA DEL SISTEMA, CADA ACCIÓN DEL SISTEMA REGISTRADA AQUÍ";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // FRM_PG38
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1284, 727);
            Controls.Add(panel1);
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
        private DataGridViewTextBoxColumn cTarea;
        private DataGridViewTextBoxColumn cModulo;
        private DataGridViewTextBoxColumn cRealizadopor;
        private DataGridViewTextBoxColumn cFecha;
        private DataGridViewTextBoxColumn cHora;
        private DataGridViewTextBoxColumn cDescripcion;
        private ComboBox comboBox1;
        private Button btnVolver;
        private Label label1;
        private PictureBox pictureBox3;
    }
}