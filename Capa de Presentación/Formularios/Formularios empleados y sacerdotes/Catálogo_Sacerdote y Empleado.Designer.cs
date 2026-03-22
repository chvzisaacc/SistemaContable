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
            lblConsulte = new Label();
            lblTitulo = new Label();
            btnVolver = new Button();
            panel2 = new Panel();
            dgvCatalogoUsuarios = new DataGridView();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoUsuarios).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(2, 3);
            pictureBox3.Margin = new Padding(4, 5, 4, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(111, 108);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            // 
            // lblConsulte
            // 
            lblConsulte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(109, 123);
            lblConsulte.Margin = new Padding(4, 0, 4, 0);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(1366, 38);
            lblConsulte.TabIndex = 11;
            lblConsulte.Text = "Observa cada una de las cuentas y subcuentas que existen en el sistema y como su saldo se ve afectado\r\n";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(157, 63);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1321, 48);
            lblTitulo.TabIndex = 10;
            lblTitulo.Text = "CATÁLOGO DE CUENTAS, TODO LO QUE SE PODRÁ INGRESAR EN EL SISTEMA\r\n";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1422, 14);
            btnVolver.Margin = new Padding(4, 5, 4, 5);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(176, 38);
            btnVolver.TabIndex = 15;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvCatalogoUsuarios);
            panel2.Location = new Point(0, 2);
            panel2.Margin = new Padding(1, 2, 1, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1270, 538);
            panel2.TabIndex = 17;
            panel2.Paint += panel2_Paint;
            // 
            // dgvCatalogoUsuarios
            // 
            dgvCatalogoUsuarios.AllowUserToAddRows = false;
            dgvCatalogoUsuarios.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCatalogoUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            dgvCatalogoUsuarios.Location = new Point(0, 5);
            dgvCatalogoUsuarios.Margin = new Padding(4, 5, 4, 5);
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
            dgvCatalogoUsuarios.Size = new Size(1267, 482);
            dgvCatalogoUsuarios.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(226, 192);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1271, 544);
            panel1.TabIndex = 16;
            // 
            // FRM_PG46
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1601, 746);
            Controls.Add(panel1);
            Controls.Add(btnVolver);
            Controls.Add(pictureBox3);
            Controls.Add(lblConsulte);
            Controls.Add(lblTitulo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "FRM_PG46";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Catálogo_Sacerdote y Empleado";
            Load += FRM_PG46_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoUsuarios).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox3;
        private Label lblConsulte;
        private Label lblTitulo;
        private Button btnVolver;
        private Panel panel2;
        private DataGridView dgvCatalogoUsuarios;
        private Panel panel1;
    }
}