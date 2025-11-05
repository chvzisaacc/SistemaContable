namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_PG46
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG46));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            dgvBitacora = new DataGridView();
            lblConsulte = new Label();
            lblTitulo = new Label();
            btnVolver = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(77, 0);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(150, 163);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(273, 56);
            label1.Name = "label1";
            label1.Size = new Size(189, 41);
            label1.TabIndex = 13;
            label1.Text = "SACERDOTE";
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBitacora.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Dock = DockStyle.Fill;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.MenuText;
            dgvBitacora.Location = new Point(0, 0);
            dgvBitacora.Margin = new Padding(3, 4, 3, 4);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.ReadOnly = true;
            dgvBitacora.RowHeadersWidth = 51;
            dgvBitacora.Size = new Size(1071, 596);
            dgvBitacora.TabIndex = 12;
            dgvBitacora.CellContentClick += dgvBitacora_CellContentClick;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(26, 247);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(1458, 41);
            lblConsulte.TabIndex = 11;
            lblConsulte.Text = "Observa cada una de las cuentas y subcuentas que existen en el sistema y como su saldo se ve afectado\r\n";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(146, 189);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1111, 41);
            lblTitulo.TabIndex = 10;
            lblTitulo.Text = "CATÁLOGO DE CUENTAS, TODO LO QUE SE PODRA INGRESAR EN EL SISTEMA\r\n";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1253, 16);
            btnVolver.Margin = new Padding(3, 4, 3, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(153, 51);
            btnVolver.TabIndex = 15;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(dgvBitacora);
            panel1.Location = new Point(209, 313);
            panel1.Name = "panel1";
            panel1.Size = new Size(1071, 596);
            panel1.TabIndex = 16;
            // 
            // FRM_PG46
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1467, 969);
            Controls.Add(btnVolver);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(lblConsulte);
            Controls.Add(lblTitulo);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FRM_PG46";
            Text = "FRM_PG46cs";
            Load += FRM_PG46_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            panel1.ResumeLayout(false);
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
        private Panel panel1;
    }
}