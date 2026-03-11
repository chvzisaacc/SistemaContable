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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            dgvCatalogoUsuarios = new DataGridView();
            lblConsulte = new Label();
            lblTitulo = new Label();
            btnVolver = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoUsuarios).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(32, 10);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(105, 116);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(208, 58);
            label1.Name = "label1";
            label1.Size = new Size(189, 41);
            label1.TabIndex = 13;
            label1.Text = "SACERDOTE";
            // 
            // dgvCatalogoUsuarios
            // 
            dgvCatalogoUsuarios.AllowUserToAddRows = false;
            dgvCatalogoUsuarios.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCatalogoUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvCatalogoUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvCatalogoUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCatalogoUsuarios.BackgroundColor = SystemColors.Control;
            dgvCatalogoUsuarios.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.MenuText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvCatalogoUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCatalogoUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvCatalogoUsuarios.DefaultCellStyle = dataGridViewCellStyle3;
            dgvCatalogoUsuarios.EnableHeadersVisualStyles = false;
            dgvCatalogoUsuarios.GridColor = SystemColors.MenuText;
            dgvCatalogoUsuarios.Location = new Point(0, 0);
            dgvCatalogoUsuarios.Margin = new Padding(3, 4, 3, 4);
            dgvCatalogoUsuarios.Name = "dgvCatalogoUsuarios";
            dgvCatalogoUsuarios.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvCatalogoUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvCatalogoUsuarios.RowHeadersVisible = false;
            dgvCatalogoUsuarios.RowHeadersWidth = 51;
            dgvCatalogoUsuarios.Size = new Size(1017, 434);
            dgvCatalogoUsuarios.TabIndex = 12;
            dgvCatalogoUsuarios.CellContentClick += dgvBitacora_CellContentClick;
            // 
            // lblConsulte
            // 
            lblConsulte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(90, 193);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(1197, 32);
            lblConsulte.TabIndex = 11;
            lblConsulte.Text = "Observa cada una de las cuentas y subcuentas que existen en el sistema y como su saldo se ve afectado\r\n";
            lblConsulte.Click += lblConsulte_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(126, 145);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1111, 41);
            lblTitulo.TabIndex = 10;
            lblTitulo.Text = "CATÁLOGO DE CUENTAS, TODO LO QUE SE PODRÁ INGRESAR EN EL SISTEMA\r\n";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1118, 10);
            btnVolver.Margin = new Padding(3, 4, 3, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(153, 50);
            btnVolver.TabIndex = 15;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(dgvCatalogoUsuarios);
            panel1.Location = new Point(182, 274);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1017, 434);
            panel1.TabIndex = 16;
            // 
            // panel2
            // 
            panel2.Location = new Point(231, 290);
            panel2.Margin = new Padding(1, 2, 1, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(951, 370);
            panel2.TabIndex = 17;
            // 
            // FRM_PG46
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 727);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(btnVolver);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(lblConsulte);
            Controls.Add(lblTitulo);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FRM_PG46";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Catálogo_Sacerdote y Empleado";
            Load += FRM_PG46_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoUsuarios).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox3;
        private Label label1;
        private DataGridView dgvCatalogoUsuarios;
        private Label lblConsulte;
        private Label lblTitulo;
        private Button btnVolver;
        private Panel panel1;
        private Panel panel2;
    }
}