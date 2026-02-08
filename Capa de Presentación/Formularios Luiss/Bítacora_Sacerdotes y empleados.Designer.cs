namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_PG51
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG51));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            dgvBitacora = new DataGridView();
            lblConsulte = new Label();
            lblTitulo = new Label();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(94, 2);
            pictureBox3.Margin = new Padding(4, 5, 4, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(189, 203);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 19;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(340, 72);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(222, 48);
            label1.TabIndex = 18;
            label1.Text = "SACERDOTE";
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.BackgroundColor = SystemColors.Control;
            dgvBitacora.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Gold;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvBitacora.DefaultCellStyle = dataGridViewCellStyle2;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.WindowText;
            dgvBitacora.Location = new Point(94, 387);
            dgvBitacora.Margin = new Padding(4, 5, 4, 5);
            dgvBitacora.Name = "dgvBitacora";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 14F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvBitacora.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.RowHeadersWidth = 51;
            dgvBitacora.Size = new Size(1469, 475);
            dgvBitacora.TabIndex = 17;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(94, 312);
            lblConsulte.Margin = new Padding(4, 0, 4, 0);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(645, 110);
            lblConsulte.TabIndex = 16;
            lblConsulte.Text = "Revisa tu actividad en el sistema\r\n\r\n";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(94, 232);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1441, 110);
            lblTitulo.TabIndex = 15;
            lblTitulo.Text = "BITÁCORA DEL SISTEMA, CADA ACCIÓN DEL SISTEMA REGISTRADA AQUÍ\r\n\r\n";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1331, 13);
            btnVolver.Margin = new Padding(4, 5, 4, 5);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(231, 63);
            btnVolver.TabIndex = 20;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FRM_PG51
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1601, 898);
            Controls.Add(btnVolver);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(dgvBitacora);
            Controls.Add(lblConsulte);
            Controls.Add(lblTitulo);
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "FRM_PG51";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bítacora_Sacerdotes y empleados";
            Load += FRM_PG51_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox3;
        private Label label1;
        private DataGridView dgvBitacora;
        private Label lblConsulte;
        private Label lblTitulo;
        private Button btnVolver;
    }
}