using System.Windows.Forms;

namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_42
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_42));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            panelContenedor = new Panel();
            panelGastos2 = new Panel();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            dateTimePicker1 = new DateTimePicker();
            button4 = new Button();
            btnGuardar2 = new Button();
            txtNoReferencia2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbOrigen2 = new ComboBox();
            dgvGastos = new DataGridView();
            Id_Transaccion1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            panelIngresos = new Panel();
            pictureBox8 = new PictureBox();
            pictureBox5 = new PictureBox();
            button2 = new Button();
            button1 = new Button();
            dtpFecha = new DateTimePicker();
            btnGuardar = new Button();
            txtNoReferencia = new TextBox();
            lblNoReferencia = new Label();
            lblFecha = new Label();
            lblOrigen = new Label();
            cmbOrigen = new ComboBox();
            dataGridView1 = new DataGridView();
            NombreCuenta = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            panelBancos2 = new Panel();
            button3 = new Button();
            cmbAcciones = new ComboBox();
            cmbInteresesBancarios = new ComboBox();
            cmbCuentas = new ComboBox();
            panel5 = new Panel();
            panelCajaChica2 = new Panel();
            textBox1 = new TextBox();
            chkSaldoInicial = new CheckBox();
            txtSaldoActual = new TextBox();
            pictureBox4 = new PictureBox();
            btnDetalle = new Button();
            label7 = new Label();
            btnGastos = new Button();
            btnBancos = new Button();
            btnCajaChica = new Button();
            lblNoSeleccionado = new Label();
            btnIngresos = new Button();
            panel1 = new Panel();
            panelLinea = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panelContenedor.SuspendLayout();
            panelGastos2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).BeginInit();
            panelIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelBancos2.SuspendLayout();
            panelCajaChica2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(260, 49);
            label1.Name = "label1";
            label1.Size = new Size(169, 37);
            label1.TabIndex = 0;
            label1.Text = "SACERDOTE";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(892, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(91, 63);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1002, 20);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(79, 63);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(33, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(153, 152);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelIngresos);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panel5);
            panelContenedor.Location = new Point(3, 213);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1096, 302);
            panelContenedor.TabIndex = 8;
            // 
            // panelGastos2
            // 
            panelGastos2.Controls.Add(pictureBox6);
            panelGastos2.Controls.Add(pictureBox7);
            panelGastos2.Controls.Add(dateTimePicker1);
            panelGastos2.Controls.Add(button4);
            panelGastos2.Controls.Add(btnGuardar2);
            panelGastos2.Controls.Add(txtNoReferencia2);
            panelGastos2.Controls.Add(label2);
            panelGastos2.Controls.Add(label3);
            panelGastos2.Controls.Add(label4);
            panelGastos2.Controls.Add(cmbOrigen2);
            panelGastos2.Controls.Add(dgvGastos);
            panelGastos2.Dock = DockStyle.Fill;
            panelGastos2.Location = new Point(0, 0);
            panelGastos2.Margin = new Padding(3, 2, 3, 2);
            panelGastos2.Name = "panelGastos2";
            panelGastos2.Size = new Size(1096, 302);
            panelGastos2.TabIndex = 43;
            panelGastos2.Paint += panelGastos2_Paint;
            // 
            // pictureBox6
            // 
            pictureBox6.BackgroundImage = Properties.Resources.ojo;
            pictureBox6.Image = Properties.Resources.ojo1;
            pictureBox6.Location = new Point(999, 199);
            pictureBox6.Margin = new Padding(3, 2, 3, 2);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(94, 26);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 36;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(999, 230);
            pictureBox7.Margin = new Padding(3, 2, 3, 2);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(94, 26);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 34;
            pictureBox7.TabStop = false;
            pictureBox7.Click += pictureBox7_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dateTimePicker1.CalendarForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(134, 70);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(243, 23);
            dateTimePicker1.TabIndex = 25;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BackgroundImageLayout = ImageLayout.Center;
            button4.Location = new Point(11, 269);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(15, 20);
            button4.TabIndex = 33;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click_1;
            // 
            // btnGuardar2
            // 
            btnGuardar2.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar2.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardar2.FlatAppearance.BorderSize = 0;
            btnGuardar2.FlatStyle = FlatStyle.Flat;
            btnGuardar2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar2.ForeColor = SystemColors.Control;
            btnGuardar2.Location = new Point(999, 262);
            btnGuardar2.Name = "btnGuardar2";
            btnGuardar2.Size = new Size(94, 28);
            btnGuardar2.TabIndex = 28;
            btnGuardar2.Text = "Guardar";
            btnGuardar2.UseVisualStyleBackColor = false;
            btnGuardar2.Click += btnGuardar2_Click;
            // 
            // txtNoReferencia2
            // 
            txtNoReferencia2.BackColor = Color.FromArgb(251, 203, 51);
            txtNoReferencia2.BorderStyle = BorderStyle.None;
            txtNoReferencia2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNoReferencia2.Location = new Point(601, 22);
            txtNoReferencia2.Multiline = true;
            txtNoReferencia2.Name = "txtNoReferencia2";
            txtNoReferencia2.Size = new Size(380, 19);
            txtNoReferencia2.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(407, 19);
            label2.Name = "label2";
            label2.Size = new Size(147, 25);
            label2.TabIndex = 25;
            label2.Text = "No. Referencia:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(31, 62);
            label3.Name = "label3";
            label3.Size = new Size(67, 25);
            label3.TabIndex = 24;
            label3.Text = "Fecha:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(31, 17);
            label4.Name = "label4";
            label4.Size = new Size(78, 25);
            label4.TabIndex = 23;
            label4.Text = "Origen:";
            // 
            // cmbOrigen2
            // 
            cmbOrigen2.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen2.FlatStyle = FlatStyle.Flat;
            cmbOrigen2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen2.FormattingEnabled = true;
            cmbOrigen2.Location = new Point(132, 23);
            cmbOrigen2.Margin = new Padding(3, 2, 3, 2);
            cmbOrigen2.Name = "cmbOrigen2";
            cmbOrigen2.Size = new Size(243, 21);
            cmbOrigen2.TabIndex = 22;
            cmbOrigen2.Text = "Seleccionar";
            cmbOrigen2.SelectedIndexChanged += cmbOrigen2_SelectedIndexChanged_1;
            // 
            // dgvGastos
            // 
            dgvGastos.AllowDrop = true;
            dgvGastos.AllowUserToAddRows = false;
            dgvGastos.AllowUserToDeleteRows = false;
            dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGastos.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvGastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGastos.Columns.AddRange(new DataGridViewColumn[] { Id_Transaccion1, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvGastos.Location = new Point(31, 100);
            dgvGastos.Name = "dgvGastos";
            dgvGastos.ReadOnly = true;
            dgvGastos.RowHeadersWidth = 51;
            dgvGastos.Size = new Size(950, 191);
            dgvGastos.TabIndex = 27;
            dgvGastos.CellClick += dgvGastos_CellClick;
            dgvGastos.CellContentClick += dgvGastos_CellContentClick;
            dgvGastos.EditingControlShowing += dgvGastos_EditingControlShowing;
            // 
            // Id_Transaccion1
            // 
            Id_Transaccion1.HeaderText = "IdTransaccion";
            Id_Transaccion1.MinimumWidth = 8;
            Id_Transaccion1.Name = "Id_Transaccion1";
            Id_Transaccion1.ReadOnly = true;
            Id_Transaccion1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Nombre/Cuenta";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Detalle";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Saldo";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // panelIngresos
            // 
            panelIngresos.Controls.Add(pictureBox8);
            panelIngresos.Controls.Add(pictureBox5);
            panelIngresos.Controls.Add(button2);
            panelIngresos.Controls.Add(button1);
            panelIngresos.Controls.Add(dtpFecha);
            panelIngresos.Controls.Add(btnGuardar);
            panelIngresos.Controls.Add(txtNoReferencia);
            panelIngresos.Controls.Add(lblNoReferencia);
            panelIngresos.Controls.Add(lblFecha);
            panelIngresos.Controls.Add(lblOrigen);
            panelIngresos.Controls.Add(cmbOrigen);
            panelIngresos.Controls.Add(dataGridView1);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Location = new Point(0, 0);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(1096, 302);
            panelIngresos.TabIndex = 44;
            panelIngresos.Paint += panelIngresos_Paint;
            // 
            // pictureBox8
            // 
            pictureBox8.BackgroundImage = Properties.Resources.ojo;
            pictureBox8.Image = Properties.Resources.ojo1;
            pictureBox8.Location = new Point(987, 206);
            pictureBox8.Margin = new Padding(3, 2, 3, 2);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(94, 26);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 37;
            pictureBox8.TabStop = false;
            pictureBox8.Click += pictureBox8_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.BorderStyle = BorderStyle.FixedSingle;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(986, 239);
            pictureBox5.Margin = new Padding(3, 2, 3, 2);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(95, 27);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 33;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImageLayout = ImageLayout.Center;
            button2.Location = new Point(16, 278);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(15, 20);
            button2.TabIndex = 32;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(986, 271);
            button1.Name = "button1";
            button1.Size = new Size(94, 28);
            button1.TabIndex = 29;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // dtpFecha
            // 
            dtpFecha.CalendarFont = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.CalendarForeColor = SystemColors.ControlLightLight;
            dtpFecha.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(134, 62);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(243, 23);
            dtpFecha.TabIndex = 21;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = SystemColors.Control;
            btnGuardar.Location = new Point(1080, 368);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(142, 48);
            btnGuardar.TabIndex = 20;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // txtNoReferencia
            // 
            txtNoReferencia.BackColor = Color.FromArgb(251, 203, 51);
            txtNoReferencia.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNoReferencia.Location = new Point(587, 23);
            txtNoReferencia.Multiline = true;
            txtNoReferencia.Name = "txtNoReferencia";
            txtNoReferencia.Size = new Size(394, 19);
            txtNoReferencia.TabIndex = 18;
            // 
            // lblNoReferencia
            // 
            lblNoReferencia.AutoSize = true;
            lblNoReferencia.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoReferencia.Location = new Point(435, 19);
            lblNoReferencia.Name = "lblNoReferencia";
            lblNoReferencia.Size = new Size(142, 25);
            lblNoReferencia.TabIndex = 17;
            lblNoReferencia.Text = "No. Referencia";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(16, 59);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(62, 25);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "Fecha";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigen.Location = new Point(16, 24);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(73, 25);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen";
            // 
            // cmbOrigen
            // 
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(134, 25);
            cmbOrigen.Margin = new Padding(3, 2, 3, 2);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(243, 21);
            cmbOrigen.TabIndex = 13;
            cmbOrigen.Text = "Seleccionar";
            cmbOrigen.SelectedIndexChanged += cmbOrigen_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NombreCuenta, cDetalle, cSaldo });
            dataGridView1.Location = new Point(43, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(937, 200);
            dataGridView1.TabIndex = 19;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dgvIngresos_CellContentClick;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            // 
            // NombreCuenta
            // 
            NombreCuenta.HeaderText = "Nombre/Cuenta";
            NombreCuenta.MinimumWidth = 6;
            NombreCuenta.Name = "NombreCuenta";
            NombreCuenta.ReadOnly = true;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.MinimumWidth = 6;
            cDetalle.Name = "cDetalle";
            cDetalle.ReadOnly = true;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.MinimumWidth = 6;
            cSaldo.Name = "cSaldo";
            cSaldo.ReadOnly = true;
            // 
            // panelBancos2
            // 
            panelBancos2.Controls.Add(button3);
            panelBancos2.Controls.Add(cmbAcciones);
            panelBancos2.Controls.Add(cmbInteresesBancarios);
            panelBancos2.Controls.Add(cmbCuentas);
            panelBancos2.Dock = DockStyle.Fill;
            panelBancos2.Location = new Point(0, 0);
            panelBancos2.Name = "panelBancos2";
            panelBancos2.Size = new Size(1096, 302);
            panelBancos2.TabIndex = 41;
            panelBancos2.Visible = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(251, 203, 51);
            button3.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(303, 160);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(280, 32);
            button3.TabIndex = 8;
            button3.Text = "Certificados de depósito";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // cmbAcciones
            // 
            cmbAcciones.BackColor = Color.FromArgb(43, 56, 143);
            cmbAcciones.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbAcciones.ForeColor = SystemColors.ControlLightLight;
            cmbAcciones.FormattingEnabled = true;
            cmbAcciones.Items.AddRange(new object[] { "Agregar Saldo", "Transferencia entre cuentas", "Agregar cuenta bancaria", "Salida de dinero" });
            cmbAcciones.Location = new Point(687, 40);
            cmbAcciones.Name = "cmbAcciones";
            cmbAcciones.Size = new Size(195, 38);
            cmbAcciones.TabIndex = 7;
            cmbAcciones.Text = "Acciones";
            cmbAcciones.SelectedIndexChanged += cmbAcciones_SelectedIndexChanged;
            // 
            // cmbInteresesBancarios
            // 
            cmbInteresesBancarios.BackColor = Color.FromArgb(251, 203, 51);
            cmbInteresesBancarios.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            cmbInteresesBancarios.FormattingEnabled = true;
            cmbInteresesBancarios.Items.AddRange(new object[] { "Cuentas Bancarias", "Certificados de deposito" });
            cmbInteresesBancarios.Location = new Point(303, 100);
            cmbInteresesBancarios.Name = "cmbInteresesBancarios";
            cmbInteresesBancarios.Size = new Size(280, 38);
            cmbInteresesBancarios.TabIndex = 5;
            cmbInteresesBancarios.Text = "Intereses Bancarios";
            cmbInteresesBancarios.SelectedIndexChanged += cmbInteresesBancarios_SelectedIndexChanged;
            // 
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Items.AddRange(new object[] { "Cuentas de Ahorro", "Cuenta de Cheques" });
            cmbCuentas.Location = new Point(303, 40);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(280, 38);
            cmbCuentas.TabIndex = 4;
            cmbCuentas.Text = "Cuentas de Ahorro";
            cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            // 
            // panel5
            // 
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(285, 15);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(524, 220);
            panel5.TabIndex = 49;
            // 
            // panelCajaChica2
            // 
            panelCajaChica2.Controls.Add(textBox1);
            panelCajaChica2.Controls.Add(chkSaldoInicial);
            panelCajaChica2.Controls.Add(txtSaldoActual);
            panelCajaChica2.Controls.Add(pictureBox4);
            panelCajaChica2.Controls.Add(btnDetalle);
            panelCajaChica2.Controls.Add(label7);
            panelCajaChica2.Dock = DockStyle.Fill;
            panelCajaChica2.Location = new Point(0, 0);
            panelCajaChica2.Margin = new Padding(3, 2, 3, 2);
            panelCajaChica2.Name = "panelCajaChica2";
            panelCajaChica2.Size = new Size(1096, 302);
            panelCajaChica2.TabIndex = 42;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(417, 134);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(22, 32);
            textBox1.TabIndex = 40;
            textBox1.Text = "L";
            // 
            // chkSaldoInicial
            // 
            chkSaldoInicial.AutoSize = true;
            chkSaldoInicial.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSaldoInicial.Location = new Point(366, 206);
            chkSaldoInicial.Margin = new Padding(3, 2, 3, 2);
            chkSaldoInicial.Name = "chkSaldoInicial";
            chkSaldoInicial.Size = new Size(245, 34);
            chkSaldoInicial.TabIndex = 39;
            chkSaldoInicial.Text = "Ingresar saldo inicial";
            chkSaldoInicial.UseVisualStyleBackColor = true;
            chkSaldoInicial.CheckedChanged += chkSaldoInicial_CheckedChanged;
            // 
            // txtSaldoActual
            // 
            txtSaldoActual.BackColor = Color.FromArgb(251, 203, 51);
            txtSaldoActual.BorderStyle = BorderStyle.None;
            txtSaldoActual.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSaldoActual.Location = new Point(445, 134);
            txtSaldoActual.Margin = new Padding(3, 2, 3, 2);
            txtSaldoActual.Name = "txtSaldoActual";
            txtSaldoActual.Size = new Size(138, 32);
            txtSaldoActual.TabIndex = 38;
            txtSaldoActual.Text = "0.00";
            txtSaldoActual.TextChanged += txtSaldoActual_TextChanged;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(360, 112);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(276, 80);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 37;
            pictureBox4.TabStop = false;
            // 
            // btnDetalle
            // 
            btnDetalle.BackColor = Color.FromArgb(43, 56, 143);
            btnDetalle.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnDetalle.FlatAppearance.BorderSize = 0;
            btnDetalle.FlatStyle = FlatStyle.Flat;
            btnDetalle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDetalle.ForeColor = SystemColors.Control;
            btnDetalle.Location = new Point(687, 134);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(133, 38);
            btnDetalle.TabIndex = 36;
            btnDetalle.Text = "Detalle";
            btnDetalle.UseVisualStyleBackColor = false;
            btnDetalle.Click += btnDetalle_Click_1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(381, 32);
            label7.Name = "label7";
            label7.Size = new Size(257, 45);
            label7.TabIndex = 31;
            label7.Text = "SALDO ACTUAL";
            // 
            // btnGastos
            // 
            btnGastos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnGastos.Location = new Point(369, 160);
            btnGastos.Name = "btnGastos";
            btnGastos.Size = new Size(125, 38);
            btnGastos.TabIndex = 15;
            btnGastos.Text = "Gastos";
            btnGastos.UseVisualStyleBackColor = true;
            btnGastos.Click += btnGastos_Click;
            // 
            // btnBancos
            // 
            btnBancos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnBancos.Location = new Point(858, 160);
            btnBancos.Name = "btnBancos";
            btnBancos.Size = new Size(125, 38);
            btnBancos.TabIndex = 16;
            btnBancos.Text = "Bancos";
            btnBancos.UseVisualStyleBackColor = true;
            btnBancos.Click += btnBancos_Click_1;
            // 
            // btnCajaChica
            // 
            btnCajaChica.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCajaChica.Location = new Point(574, 160);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Size = new Size(182, 38);
            btnCajaChica.TabIndex = 17;
            btnCajaChica.Text = "Caja Chica";
            btnCajaChica.UseVisualStyleBackColor = true;
            btnCajaChica.Click += btnCajaChica_Click;
            // 
            // lblNoSeleccionado
            // 
            lblNoSeleccionado.AutoSize = true;
            lblNoSeleccionado.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoSeleccionado.Location = new Point(671, 701);
            lblNoSeleccionado.Name = "lblNoSeleccionado";
            lblNoSeleccionado.Size = new Size(473, 74);
            lblNoSeleccionado.TabIndex = 20;
            lblNoSeleccionado.Text = "¡Aún no seleccionas algo que hacer!\r\n   Comienza cuando lo prefieras";
            // 
            // btnIngresos
            // 
            btnIngresos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnIngresos.Location = new Point(128, 160);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(139, 38);
            btnIngresos.TabIndex = 23;
            btnIngresos.Text = "Ingresos";
            btnIngresos.UseVisualStyleBackColor = true;
            btnIngresos.Click += btnIngresos_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panelLinea);
            panel1.Controls.Add(panelContenedor);
            panel1.Controls.Add(btnIngresos);
            panel1.Controls.Add(lblNoSeleccionado);
            panel1.Controls.Add(btnCajaChica);
            panel1.Controls.Add(btnBancos);
            panel1.Controls.Add(btnGastos);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1099, 518);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panelLinea
            // 
            panelLinea.BackColor = Color.FromArgb(43, 56, 143);
            panelLinea.Location = new Point(0, 205);
            panelLinea.Name = "panelLinea";
            panelLinea.Size = new Size(1099, 2);
            panelLinea.TabIndex = 24;
            // 
            // FRM_42
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1122, 539);
            Controls.Add(panel1);
            Name = "FRM_42";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRM_42";
            Load += FRM_42_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panelContenedor.ResumeLayout(false);
            panelGastos2.ResumeLayout(false);
            panelGastos2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).EndInit();
            panelIngresos.ResumeLayout(false);
            panelIngresos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelBancos2.ResumeLayout(false);
            panelCajaChica2.ResumeLayout(false);
            panelCajaChica2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        // private Panel panel6;
        private Panel panelContenedor;
        private Button btnGastos;
        private Button btnBancos;
        private Button btnCajaChica;
        private Label lblNoSeleccionado;
        private Button btnIngresos;
        // private Panel panelGastos;
        // private Panel panelCajaChica;
        // private Panel panelBancos;
        private Panel panel1;
        private Panel panelBancos2;
        private ComboBox cmbAcciones;
        private ComboBox cmbInteresesBancarios;
        private ComboBox cmbCuentas;
        private Panel panelLinea;
        private Panel panelGastos2;
        private Button btnGuardar2;
        private DataGridView dgvGastos;
        private TextBox txtNoReferencia2;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox cmbOrigen2;
        private Panel panelCajaChica2;
        private CheckBox chkSaldoInicial;
        private TextBox txtSaldoActual;
        private PictureBox pictureBox4;
        private Button btnDetalle;
        private Label label7;
        private Panel panelIngresos;
        private DateTimePicker dtpFecha;
        private Button btnGuardar;
        private DataGridView dataGridView1;
        private TextBox txtNoReferencia;
        private Label lblNoReferencia;
        private Label lblFecha;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private Panel panel5;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private Button button4;
        private DateTimePicker dateTimePicker1;
        private PictureBox pictureBox5;
        private PictureBox pictureBox7;
        private DataGridViewTextBoxColumn Id_Transaccion1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn NombreCuenta;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private PictureBox pictureBox6;
        private PictureBox pictureBox8;
    }
}