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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
            panel1.Location = new Point(15, 16);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1299, 764);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // lblPagina
            // 
            lblPagina.AutoSize = true;
            lblPagina.Location = new Point(1143, 269);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(70, 20);
            lblPagina.TabIndex = 32;
            lblPagina.Text = "lblPagina";
            // 
            // btnSiguiente
            // 
            btnSiguiente.BackColor = Color.FromArgb(43, 56, 143);
            btnSiguiente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.ImageAlign = ContentAlignment.TopCenter;
            btnSiguiente.Location = new Point(1231, 465);
            btnSiguiente.Margin = new Padding(3, 2, 3, 2);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(34, 34);
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
            btnAnterior.Location = new Point(0, 465);
            btnAnterior.Margin = new Padding(3, 2, 3, 2);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(34, 34);
            btnAnterior.TabIndex = 30;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = false;
            btnAnterior.UseWaitCursor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // dtpFechaHasta
            // 
            dtpFechaHasta.Location = new Point(585, 223);
            dtpFechaHasta.Name = "dtpFechaHasta";
            dtpFechaHasta.Size = new Size(250, 27);
            dtpFechaHasta.TabIndex = 29;
            dtpFechaHasta.ValueChanged += dtpFechaHasta_ValueChanged;
            // 
            // lblFechaHasta
            // 
            lblFechaHasta.AutoSize = true;
            lblFechaHasta.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaHasta.Location = new Point(448, 223);
            lblFechaHasta.Name = "lblFechaHasta";
            lblFechaHasta.Size = new Size(131, 28);
            lblFechaHasta.TabIndex = 28;
            lblFechaHasta.Text = "Fecha Hasta:";
            // 
            // dtpFechaDesde
            // 
            dtpFechaDesde.Location = new Point(171, 223);
            dtpFechaDesde.Name = "dtpFechaDesde";
            dtpFechaDesde.Size = new Size(250, 27);
            dtpFechaDesde.TabIndex = 27;
            dtpFechaDesde.ValueChanged += dtpFechaDesde_ValueChanged;
            // 
            // lblFechaDesde
            // 
            lblFechaDesde.AutoSize = true;
            lblFechaDesde.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaDesde.Location = new Point(30, 221);
            lblFechaDesde.Name = "lblFechaDesde";
            lblFechaDesde.Size = new Size(135, 28);
            lblFechaDesde.TabIndex = 26;
            lblFechaDesde.Text = "Fecha Desde:";
            // 
            // chkFiltrarFecha
            // 
            chkFiltrarFecha.AutoSize = true;
            chkFiltrarFecha.Location = new Point(1071, 184);
            chkFiltrarFecha.Name = "chkFiltrarFecha";
            chkFiltrarFecha.Size = new Size(174, 24);
            chkFiltrarFecha.TabIndex = 10;
            chkFiltrarFecha.Text = "Activar filtro de fecha";
            chkFiltrarFecha.UseVisualStyleBackColor = true;
            chkFiltrarFecha.CheckedChanged += chkFiltrarFecha_CheckedChanged;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(-1, -1);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(115, 111);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label1.Location = new Point(120, 20);
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
            btnVolver.Location = new Point(1029, 30);
            btnVolver.Margin = new Padding(3, 4, 3, 4);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(193, 50);
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
            cmbUsuario.Location = new Point(484, 178);
            cmbUsuario.Margin = new Padding(3, 4, 3, 4);
            cmbUsuario.MaxDropDownItems = 6;
            cmbUsuario.Name = "cmbUsuario";
            cmbUsuario.Size = new Size(180, 25);
            cmbUsuario.TabIndex = 6;
            cmbUsuario.Text = "Seleccionar";
            cmbUsuario.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lblRealizadopor
            // 
            lblRealizadopor.AutoSize = true;
            lblRealizadopor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRealizadopor.Location = new Point(332, 178);
            lblRealizadopor.Name = "lblRealizadopor";
            lblRealizadopor.Size = new Size(148, 28);
            lblRealizadopor.TabIndex = 5;
            lblRealizadopor.Text = "Realizado por:";
            lblRealizadopor.TextAlign = ContentAlignment.BottomCenter;
            lblRealizadopor.Click += label1_Click;
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBitacora.DefaultCellStyle = dataGridViewCellStyle2;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.MenuText;
            dgvBitacora.Location = new Point(34, 293);
            dgvBitacora.Margin = new Padding(3, 4, 3, 4);
            dgvBitacora.Name = "dgvBitacora";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvBitacora.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.RowHeadersWidth = 51;
            dgvBitacora.Size = new Size(1197, 400);
            dgvBitacora.TabIndex = 4;
            dgvBitacora.CellContentClick += dataGridView1_CellContentClick;
            // 
            // lblParroquia
            // 
            lblParroquia.AutoSize = true;
            lblParroquia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblParroquia.Location = new Point(29, 173);
            lblParroquia.Name = "lblParroquia";
            lblParroquia.Size = new Size(109, 28);
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
            cmbParroquia.Location = new Point(142, 178);
            cmbParroquia.Margin = new Padding(3, 4, 3, 4);
            cmbParroquia.MaxDropDownItems = 6;
            cmbParroquia.Name = "cmbParroquia";
            cmbParroquia.Size = new Size(181, 25);
            cmbParroquia.TabIndex = 2;
            cmbParroquia.Text = "Seleccionar";
            cmbParroquia.SelectedIndexChanged += cmbParroquia_SelectedIndexChanged;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblConsulte.Location = new Point(130, 120);
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
            lblTitulo.Location = new Point(138, 84);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(991, 38);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "BITÁCORA DEL SISTEMA, CADA ACCIÓN DEL SISTEMA REGISTRADA AQUÍ";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // Bitacora_Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1281, 718);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
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