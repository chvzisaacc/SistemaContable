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
            dgvBitacora = new DataGridView();
            lblConsulte = new Label();
            lblTitulo = new Label();
            btnVolver = new Button();
            lblFechaDesde = new Label();
            dtpFechaDesde = new DateTimePicker();
            dtpFechaHasta = new DateTimePicker();
            lblFechaHasta = new Label();
            btnFiltrar = new Button();
            btnLimpiarFiltro = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(3, 4);
            pictureBox3.Margin = new Padding(4, 5, 4, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(107, 89);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 19;
            pictureBox3.TabStop = false;
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
            dgvBitacora.GridColor = SystemColors.WindowText;
            dgvBitacora.Location = new Point(66, 220);
            dgvBitacora.Margin = new Padding(4, 5, 4, 5);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.ReadOnly = true;
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
            dgvBitacora.Size = new Size(1469, 647);
            dgvBitacora.TabIndex = 17;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(417, 91);
            lblConsulte.Margin = new Padding(4, 0, 4, 0);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(500, 45);
            lblConsulte.TabIndex = 16;
            lblConsulte.Text = "Revisa tu actividad en el sistema";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(134, 21);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1137, 45);
            lblTitulo.TabIndex = 15;
            lblTitulo.Text = "BITÁCORA DEL SISTEMA, CADA ACCIÓN DEL SISTEMA REGISTRADA AQUÍ";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            // lblFechaDesde
            // 
            lblFechaDesde.AutoSize = true;
            lblFechaDesde.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaDesde.Location = new Point(139, 158);
            lblFechaDesde.Margin = new Padding(4, 0, 4, 0);
            lblFechaDesde.Name = "lblFechaDesde";
            lblFechaDesde.Size = new Size(162, 32);
            lblFechaDesde.TabIndex = 22;
            lblFechaDesde.Text = "Fecha Desde:";
            // 
            // dtpFechaDesde
            // 
            dtpFechaDesde.Location = new Point(314, 159);
            dtpFechaDesde.Margin = new Padding(4, 3, 4, 3);
            dtpFechaDesde.Name = "dtpFechaDesde";
            dtpFechaDesde.Size = new Size(311, 31);
            dtpFechaDesde.TabIndex = 23;
            dtpFechaDesde.ValueChanged += dtpFechaDesde_ValueChanged;
            // 
            // dtpFechaHasta
            // 
            dtpFechaHasta.Location = new Point(1099, 160);
            dtpFechaHasta.Margin = new Padding(4, 3, 4, 3);
            dtpFechaHasta.Name = "dtpFechaHasta";
            dtpFechaHasta.Size = new Size(311, 31);
            dtpFechaHasta.TabIndex = 25;
            dtpFechaHasta.Value = new DateTime(2026, 3, 17, 0, 0, 0, 0);
            dtpFechaHasta.ValueChanged += dtpFechaHasta_ValueChanged;
            // 
            // lblFechaHasta
            // 
            lblFechaHasta.AutoSize = true;
            lblFechaHasta.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaHasta.Location = new Point(923, 159);
            lblFechaHasta.Margin = new Padding(4, 0, 4, 0);
            lblFechaHasta.Name = "lblFechaHasta";
            lblFechaHasta.Size = new Size(156, 32);
            lblFechaHasta.TabIndex = 24;
            lblFechaHasta.Text = "Fecha Hasta:";
            // 
            // btnFiltrar
            // 
            btnFiltrar.BackColor = Color.FromArgb(43, 56, 143);
            btnFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.ImageAlign = ContentAlignment.TopCenter;
            btnFiltrar.Location = new Point(1136, 78);
            btnFiltrar.Margin = new Padding(4, 3, 4, 3);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(171, 43);
            btnFiltrar.TabIndex = 26;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.UseWaitCursor = true;
            btnFiltrar.Visible = false;
            // 
            // btnLimpiarFiltro
            // 
            btnLimpiarFiltro.BackColor = Color.FromArgb(43, 56, 143);
            btnLimpiarFiltro.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiarFiltro.ForeColor = Color.White;
            btnLimpiarFiltro.ImageAlign = ContentAlignment.TopCenter;
            btnLimpiarFiltro.Location = new Point(1331, 78);
            btnLimpiarFiltro.Margin = new Padding(4, 3, 4, 3);
            btnLimpiarFiltro.Name = "btnLimpiarFiltro";
            btnLimpiarFiltro.Size = new Size(171, 43);
            btnLimpiarFiltro.TabIndex = 27;
            btnLimpiarFiltro.Text = "Limpiar Filtro";
            btnLimpiarFiltro.UseVisualStyleBackColor = false;
            btnLimpiarFiltro.UseWaitCursor = true;
            btnLimpiarFiltro.Visible = false;
            // 
            // FRM_PG51
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1601, 897);
            Controls.Add(btnLimpiarFiltro);
            Controls.Add(btnFiltrar);
            Controls.Add(dtpFechaHasta);
            Controls.Add(lblFechaHasta);
            Controls.Add(dtpFechaDesde);
            Controls.Add(lblFechaDesde);
            Controls.Add(btnVolver);
            Controls.Add(pictureBox3);
            Controls.Add(dgvBitacora);
            Controls.Add(lblConsulte);
            Controls.Add(lblTitulo);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Label lblConsulte;
        private Label lblTitulo;
        private Button btnVolver;
        private Label lblFechaDesde;
        private DateTimePicker dtpFechaDesde;
        private DateTimePicker dtpFechaHasta;
        private Label lblFechaHasta;
        private Button btnFiltrar;
        private Button btnLimpiarFiltro;
        private DataGridView dgvBitacora;
    }
}