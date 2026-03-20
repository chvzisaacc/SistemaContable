namespace Capa_de_Presentación
{
    partial class Bitacora_Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bitacora_Admin));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            lblPagina = new Label();
            btnSiguiente = new Button();
            btnAnterior = new Button();
            dtpFechaHasta = new DateTimePicker();
            lblFechaHasta = new Label();
            dtpFechaDesde = new DateTimePicker();
            lblFechaDesde = new Label();
            chkFiltrarFecha = new CheckBox();
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
            panel1.Controls.Add(lblPagina);
            panel1.Controls.Add(btnSiguiente);
            panel1.Controls.Add(btnAnterior);
            panel1.Controls.Add(dtpFechaHasta);
            panel1.Controls.Add(lblFechaHasta);
            panel1.Controls.Add(dtpFechaDesde);
            panel1.Controls.Add(lblFechaDesde);
            panel1.Controls.Add(chkFiltrarFecha);
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
            panel1.Location = new Point(13, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1137, 574);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // lblPagina
            // 
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(1000, 202);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(56, 15);
            lblPagina.TabIndex = 32;
            lblPagina.Text = "lblPagina";
            // 
            // btnSiguiente
            // 
            btnSiguiente.BackColor = Color.FromArgb(43, 56, 143);
            btnSiguiente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.ImageAlign = ContentAlignment.TopCenter;
            btnSiguiente.Location = new Point(1077, 349);
            btnSiguiente.Margin = new Padding(3, 2, 3, 2);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(30, 26);
            btnSiguiente.TabIndex = 31;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = false;
            btnSiguiente.UseWaitCursor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnAnterior
            // 
            btnAnterior.BackColor = Color.FromArgb(43, 56, 143);
            btnAnterior.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAnterior.ForeColor = Color.White;
            btnAnterior.ImageAlign = ContentAlignment.TopCenter;
            btnAnterior.Location = new Point(0, 349);
            btnAnterior.Margin = new Padding(3, 2, 3, 2);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(30, 26);
            btnAnterior.TabIndex = 30;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = false;
            btnAnterior.UseWaitCursor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // dtpFechaHasta
            // 
            dtpFechaHasta.Location = new Point(512, 167);
            dtpFechaHasta.Margin = new Padding(3, 2, 3, 2);
            dtpFechaHasta.Name = "dtpFechaHasta";
            dtpFechaHasta.Size = new Size(219, 23);
            dtpFechaHasta.TabIndex = 29;
            dtpFechaHasta.ValueChanged += dtpFechaHasta_ValueChanged;
            // 
            // lblFechaHasta
            // 
            lblFechaHasta.AutoSize = true;
            lblFechaHasta.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaHasta.Location = new Point(392, 167);
            lblFechaHasta.Name = "lblFechaHasta";
            lblFechaHasta.Size = new Size(105, 21);
            lblFechaHasta.TabIndex = 28;
            lblFechaHasta.Text = "Fecha Hasta:";
            // 
            // dtpFechaDesde
            // 
            dtpFechaDesde.Location = new Point(150, 167);
            dtpFechaDesde.Margin = new Padding(3, 2, 3, 2);
            dtpFechaDesde.Name = "dtpFechaDesde";
            dtpFechaDesde.Size = new Size(219, 23);
            dtpFechaDesde.TabIndex = 27;
            dtpFechaDesde.ValueChanged += dtpFechaDesde_ValueChanged;
            // 
            // lblFechaDesde
            // 
            lblFechaDesde.AutoSize = true;
            lblFechaDesde.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaDesde.Location = new Point(26, 166);
            lblFechaDesde.Name = "lblFechaDesde";
            lblFechaDesde.Size = new Size(109, 21);
            lblFechaDesde.TabIndex = 26;
            lblFechaDesde.Text = "Fecha Desde:";
            // 
            // chkFiltrarFecha
            // 
            chkFiltrarFecha.AutoSize = true;
            chkFiltrarFecha.Location = new Point(937, 138);
            chkFiltrarFecha.Margin = new Padding(3, 2, 3, 2);
            chkFiltrarFecha.Name = "chkFiltrarFecha";
            chkFiltrarFecha.Size = new Size(139, 19);
            chkFiltrarFecha.TabIndex = 10;
            chkFiltrarFecha.Text = "Activar filtro de fecha";
            chkFiltrarFecha.UseVisualStyleBackColor = true;
            chkFiltrarFecha.CheckedChanged += chkFiltrarFecha_CheckedChanged;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(-1, -1);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(101, 83);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label1.Location = new Point(105, 15);
            label1.Name = "label1";
            label1.Size = new Size(197, 30);
            label1.TabIndex = 8;
            label1.Text = "ADMINISTRADOR";
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(900, 22);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(169, 38);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // cmbUsuario
            // 
            cmbUsuario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUsuario.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUsuario.BackColor = Color.Gold;
            cmbUsuario.FlatStyle = FlatStyle.Popup;
            cmbUsuario.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmbUsuario.FormattingEnabled = true;
            cmbUsuario.IntegralHeight = false;
            cmbUsuario.Items.AddRange(new object[] { "", "Isaac Chavez", "Diego Muñoz" });
            cmbUsuario.Location = new Point(424, 134);
            cmbUsuario.MaxDropDownItems = 6;
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(158, 21);
            cmbUsuario.TabIndex = 6;
            cmbUsuario.Text = "Seleccionar";
            cmbUsuario.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lblRealizadopor
            // 
            lblRealizadopor.AutoSize = true;
            lblRealizadopor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRealizadopor.Location = new Point(290, 134);
            lblRealizadopor.Name = "lblRealizadopor";
            lblRealizadopor.Size = new Size(120, 21);
            lblRealizadopor.TabIndex = 5;
            lblRealizadopor.Text = "Realizado por:";
            lblRealizadopor.TextAlign = ContentAlignment.BottomCenter;
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvBitacora.DefaultCellStyle = dataGridViewCellStyle5;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.MenuText;
            dgvBitacora.Location = new Point(30, 220);
            dgvBitacora.Name = "dgvBitacora";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvBitacora.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.RowHeadersWidth = 51;
            dgvBitacora.Size = new Size(1047, 300);
            dgvBitacora.TabIndex = 4;
            dgvBitacora.CellContentClick += dataGridView1_CellContentClick;
            // 
            // lblParroquia
            // 
            lblParroquia.AutoSize = true;
            lblParroquia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblParroquia.Location = new Point(25, 130);
            lblParroquia.Name = "lblParroquia";
            lblParroquia.Size = new Size(89, 21);
            lblParroquia.TabIndex = 3;
            lblParroquia.Text = "Parroquia:";
            // 
            // cmbParroquia
            // 
            cmbParroquia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbParroquia.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbParroquia.BackColor = Color.Gold;
            cmbParroquia.FlatStyle = FlatStyle.Popup;
            cmbParroquia.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmbParroquia.FormattingEnabled = true;
            cmbParroquia.IntegralHeight = false;
            cmbParroquia.Items.AddRange(new object[] { "", "SCJ", "El Calvario" });
            cmbParroquia.Location = new Point(124, 134);
            cmbParroquia.MaxDropDownItems = 6;
            cmbParroquia.Name = "cmbParroquia";
            cmbParroquia.Size = new Size(159, 21);
            cmbParroquia.TabIndex = 2;
            cmbParroquia.Text = "Seleccionar";
            cmbParroquia.SelectedIndexChanged += cmbParroquia_SelectedIndexChanged;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblConsulte.Location = new Point(114, 90);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(791, 30);
            lblConsulte.TabIndex = 1;
            lblConsulte.Text = "Consulte en cualquier momento que hizo cada encargado de las parroquias";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblTitulo.Location = new Point(121, 63);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(770, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "BITÁCORA DEL SISTEMA, CADA ACCIÓN DEL SISTEMA REGISTRADA AQUÍ";
            // 
            // Bitacora_Admin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1121, 538);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "Bitacora_Admin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bitacora_Admin";
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
        private CheckBox chkFiltrarFecha;
        private DateTimePicker dtpFechaHasta;
        private Label lblFechaHasta;
        private DateTimePicker dtpFechaDesde;
        private Label lblFechaDesde;
        private Button button1;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Label lblPagina;
    }
}