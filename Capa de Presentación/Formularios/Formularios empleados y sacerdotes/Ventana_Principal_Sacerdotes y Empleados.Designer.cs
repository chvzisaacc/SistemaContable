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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_42));
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            panelContenedor = new Panel();
            panel5 = new Panel();
            panelGastos2 = new Panel();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            dateTimePicker2 = new DateTimePicker();
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
            panelBancos2 = new Panel();
            button5 = new Button();
            label6 = new Label();
            label5 = new Label();
            button3 = new Button();
            cmbAcciones = new ComboBox();
            cmbCuentas = new ComboBox();
            panelCajaChica2 = new Panel();
            panelIngresarCapital = new Panel();
            btnCancelarCapital = new Button();
            btnGuardarCapital = new Button();
            txtCapitalInicial = new TextBox();
            label8 = new Label();
            chkIngresarCapital = new CheckBox();
            chkSaldoInicial = new CheckBox();
            txtSaldoActual = new TextBox();
            lblCapitalInicial = new Label();
            label9 = new Label();
            pictureBox4 = new PictureBox();
            label7 = new Label();
            btnGastos = new Button();
            btnBancos = new Button();
            btnCajaChica = new Button();
            lblNoSeleccionado = new Label();
            btnIngresos = new Button();
            panel1 = new Panel();
            pnlAlertaDeslizante = new Panel();
            lblAlertaMensaje = new Label();
            panelLinea = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
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
            panelIngresarCapital.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel1.SuspendLayout();
            pnlAlertaDeslizante.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(263, 55);
            label1.Name = "label1";
            label1.Size = new Size(368, 37);
            label1.TabIndex = 0;
            label1.Text = "SACERDOTES - EMPLEADOS";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(920, 43);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 43);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1009, 43);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(57, 45);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(18, 35);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(141, 107);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panel5);
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelIngresos);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Location = new Point(3, 213);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1096, 301);
            panelContenedor.TabIndex = 8;
            // 
            // panel5
            // 
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(286, 15);
            panel5.Margin = new Padding(3, 1, 3, 1);
            panel5.Name = "panel5";
            panel5.Size = new Size(524, 221);
            panel5.TabIndex = 49;
            panel5.Paint += panel5_Paint;
            // 
            // panelGastos2
            // 
            panelGastos2.Controls.Add(pictureBox6);
            panelGastos2.Controls.Add(pictureBox7);
            panelGastos2.Controls.Add(dateTimePicker2);
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
            panelGastos2.Margin = new Padding(3, 1, 3, 1);
            panelGastos2.Name = "panelGastos2";
            panelGastos2.Size = new Size(1096, 301);
            panelGastos2.TabIndex = 43;
            panelGastos2.Paint += panelGastos2_Paint;
            // 
            // pictureBox6
            // 
            pictureBox6.BackgroundImage = Properties.Resources.ojo;
            pictureBox6.Image = Properties.Resources.ojo1;
            pictureBox6.Location = new Point(1000, 199);
            pictureBox6.Margin = new Padding(3, 1, 3, 1);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(94, 25);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 36;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(1000, 229);
            pictureBox7.Margin = new Padding(3, 1, 3, 1);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(94, 25);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 34;
            pictureBox7.TabStop = false;
            pictureBox7.Click += pictureBox7_Click;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CalendarFont = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dateTimePicker2.CalendarForeColor = SystemColors.ControlLightLight;
            dateTimePicker2.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Location = new Point(134, 71);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(243, 23);
            dateTimePicker2.TabIndex = 25;
            dateTimePicker2.Value = new DateTime(2025, 11, 28, 0, 0, 0, 0);
            dateTimePicker2.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BackgroundImageLayout = ImageLayout.Center;
            button4.Location = new Point(11, 269);
            button4.Margin = new Padding(1);
            button4.Name = "button4";
            button4.Size = new Size(15, 19);
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
            btnGuardar2.Location = new Point(1000, 263);
            btnGuardar2.Name = "btnGuardar2";
            btnGuardar2.Size = new Size(94, 29);
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
            txtNoReferencia2.Location = new Point(601, 23);
            txtNoReferencia2.MaxLength = 10;
            txtNoReferencia2.Multiline = true;
            txtNoReferencia2.Name = "txtNoReferencia2";
            txtNoReferencia2.Size = new Size(379, 19);
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
            label3.Location = new Point(31, 61);
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
            cmbOrigen2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbOrigen2.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbOrigen2.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen2.FlatStyle = FlatStyle.Flat;
            cmbOrigen2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen2.FormattingEnabled = true;
            cmbOrigen2.IntegralHeight = false;
            cmbOrigen2.Location = new Point(132, 23);
            cmbOrigen2.Margin = new Padding(3, 1, 3, 1);
            cmbOrigen2.MaxDropDownItems = 6;
            cmbOrigen2.Name = "cmbOrigen2";
            cmbOrigen2.Size = new Size(243, 21);
            cmbOrigen2.TabIndex = 22;
            cmbOrigen2.Text = "Seleccionar";
            // 
            // dgvGastos
            // 
            dgvGastos.AllowDrop = true;
            dgvGastos.AllowUserToAddRows = false;
            dgvGastos.AllowUserToDeleteRows = false;
            dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGastos.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvGastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvGastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGastos.Columns.AddRange(new DataGridViewColumn[] { Id_Transaccion1, dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvGastos.Location = new Point(31, 101);
            dgvGastos.Name = "dgvGastos";
            dgvGastos.ReadOnly = true;
            dgvGastos.RowHeadersWidth = 51;
            dgvGastos.Size = new Size(951, 191);
            dgvGastos.TabIndex = 27;
            dgvGastos.CellClick += dgvGastos_CellClick;
            dgvGastos.CellDoubleClick += dgvGastos_CellDoubleClick;
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
            panelIngresos.Size = new Size(1096, 301);
            panelIngresos.TabIndex = 44;
            panelIngresos.Paint += panelIngresos_Paint;
            // 
            // pictureBox8
            // 
            pictureBox8.BackgroundImage = Properties.Resources.ojo;
            pictureBox8.Image = Properties.Resources.ojo1;
            pictureBox8.Location = new Point(987, 205);
            pictureBox8.Margin = new Padding(3, 1, 3, 1);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(94, 25);
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
            pictureBox5.Margin = new Padding(3, 1, 3, 1);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(94, 27);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 33;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImageLayout = ImageLayout.Center;
            button2.Location = new Point(15, 277);
            button2.Margin = new Padding(1);
            button2.Name = "button2";
            button2.Size = new Size(15, 19);
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
            button1.Size = new Size(94, 29);
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
            dtpFecha.Location = new Point(134, 61);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(243, 23);
            dtpFecha.TabIndex = 21;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = SystemColors.Control;
            btnGuardar.Location = new Point(1079, 367);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(141, 48);
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
            txtNoReferencia.Size = new Size(395, 19);
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
            lblFecha.Location = new Point(15, 59);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(62, 25);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "Fecha";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigen.Location = new Point(15, 24);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(73, 25);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen";
            // 
            // cmbOrigen
            // 
            cmbOrigen.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbOrigen.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.IntegralHeight = false;
            cmbOrigen.Location = new Point(134, 25);
            cmbOrigen.Margin = new Padding(3, 1, 3, 1);
            cmbOrigen.MaxDropDownItems = 6;
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(243, 21);
            cmbOrigen.TabIndex = 13;
            cmbOrigen.Text = "Seleccionar";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(43, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(937, 199);
            dataGridView1.TabIndex = 19;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick_1;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            // 
            // panelBancos2
            // 
            panelBancos2.Controls.Add(button5);
            panelBancos2.Controls.Add(label6);
            panelBancos2.Controls.Add(label5);
            panelBancos2.Controls.Add(button3);
            panelBancos2.Controls.Add(cmbAcciones);
            panelBancos2.Controls.Add(cmbCuentas);
            panelBancos2.Dock = DockStyle.Fill;
            panelBancos2.Location = new Point(0, 0);
            panelBancos2.Name = "panelBancos2";
            panelBancos2.Size = new Size(1096, 301);
            panelBancos2.TabIndex = 41;
            panelBancos2.Visible = false;
            panelBancos2.Paint += panelBancos2_Paint;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(251, 203, 51);
            button5.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.Location = new Point(305, 61);
            button5.Margin = new Padding(1);
            button5.Name = "button5";
            button5.Size = new Size(280, 31);
            button5.TabIndex = 11;
            button5.Text = "Cuentas Bancarias";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click_1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(335, 113);
            label6.Margin = new Padding(1, 0, 1, 0);
            label6.Name = "label6";
            label6.Size = new Size(195, 21);
            label6.TabIndex = 10;
            label6.Text = "Certificados de depósito";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(360, 39);
            label5.Margin = new Padding(1, 0, 1, 0);
            label5.Name = "label5";
            label5.Size = new Size(148, 21);
            label5.TabIndex = 9;
            label5.Text = "Cuentas Bancarias";
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(251, 203, 51);
            button3.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(301, 137);
            button3.Margin = new Padding(1);
            button3.Name = "button3";
            button3.Size = new Size(280, 31);
            button3.TabIndex = 8;
            button3.Text = "Certificados de depósito";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // cmbAcciones
            // 
            cmbAcciones.BackColor = Color.FromArgb(43, 56, 143);
            cmbAcciones.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbAcciones.ForeColor = SystemColors.ControlLightLight;
            cmbAcciones.FormattingEnabled = true;
            cmbAcciones.Items.AddRange(new object[] { "Agregar Saldo", "Transferencia entre cuentas", "Agregar cuenta bancaria", "Salida de dinero", "Envío caja chica a banco" });
            cmbAcciones.Location = new Point(687, 55);
            cmbAcciones.Name = "cmbAcciones";
            cmbAcciones.Size = new Size(319, 29);
            cmbAcciones.TabIndex = 7;
            cmbAcciones.Text = "Acciones";
            cmbAcciones.SelectedIndexChanged += cmbAcciones_SelectedIndexChanged;
            // 
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Items.AddRange(new object[] { "Cuentas de Ahorro", "Cuenta de Cheques" });
            cmbCuentas.Location = new Point(305, 61);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(280, 38);
            cmbCuentas.TabIndex = 4;
            cmbCuentas.Text = "Cuentas de Ahorro";
            cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            // 
            // panelCajaChica2
            // 
            panelCajaChica2.Controls.Add(panelIngresarCapital);
            panelCajaChica2.Controls.Add(chkIngresarCapital);
            panelCajaChica2.Controls.Add(chkSaldoInicial);
            panelCajaChica2.Controls.Add(txtSaldoActual);
            panelCajaChica2.Controls.Add(lblCapitalInicial);
            panelCajaChica2.Controls.Add(label9);
            panelCajaChica2.Controls.Add(pictureBox4);
            panelCajaChica2.Controls.Add(label7);
            panelCajaChica2.Dock = DockStyle.Fill;
            panelCajaChica2.Location = new Point(0, 0);
            panelCajaChica2.Margin = new Padding(3, 1, 3, 1);
            panelCajaChica2.Name = "panelCajaChica2";
            panelCajaChica2.Size = new Size(1096, 301);
            panelCajaChica2.TabIndex = 42;
            panelCajaChica2.Paint += panelCajaChica2_Paint;
            // 
            // panelIngresarCapital
            // 
            panelIngresarCapital.BackColor = Color.WhiteSmoke;
            panelIngresarCapital.BorderStyle = BorderStyle.FixedSingle;
            panelIngresarCapital.Controls.Add(btnCancelarCapital);
            panelIngresarCapital.Controls.Add(btnGuardarCapital);
            panelIngresarCapital.Controls.Add(txtCapitalInicial);
            panelIngresarCapital.Controls.Add(label8);
            panelIngresarCapital.Location = new Point(617, 55);
            panelIngresarCapital.Margin = new Padding(3, 2, 3, 2);
            panelIngresarCapital.Name = "panelIngresarCapital";
            panelIngresarCapital.Size = new Size(465, 127);
            panelIngresarCapital.TabIndex = 42;
            panelIngresarCapital.Visible = false;
            // 
            // btnCancelarCapital
            // 
            btnCancelarCapital.BackColor = Color.FromArgb(43, 56, 143);
            btnCancelarCapital.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnCancelarCapital.FlatAppearance.BorderSize = 0;
            btnCancelarCapital.FlatStyle = FlatStyle.Flat;
            btnCancelarCapital.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarCapital.ForeColor = SystemColors.Control;
            btnCancelarCapital.Location = new Point(290, 76);
            btnCancelarCapital.Name = "btnCancelarCapital";
            btnCancelarCapital.Size = new Size(133, 37);
            btnCancelarCapital.TabIndex = 44;
            btnCancelarCapital.Text = "Cancelar";
            btnCancelarCapital.UseVisualStyleBackColor = false;
            btnCancelarCapital.Click += btnCancelarCapital_Click_1;
            // 
            // btnGuardarCapital
            // 
            btnGuardarCapital.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardarCapital.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardarCapital.FlatAppearance.BorderSize = 0;
            btnGuardarCapital.FlatStyle = FlatStyle.Flat;
            btnGuardarCapital.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarCapital.ForeColor = SystemColors.Control;
            btnGuardarCapital.Location = new Point(98, 75);
            btnGuardarCapital.Name = "btnGuardarCapital";
            btnGuardarCapital.Size = new Size(133, 37);
            btnGuardarCapital.TabIndex = 43;
            btnGuardarCapital.Text = "Guardar";
            btnGuardarCapital.UseVisualStyleBackColor = false;
            btnGuardarCapital.Click += btnGuardarCapital_Click_1;
            // 
            // txtCapitalInicial
            // 
            txtCapitalInicial.BackColor = Color.FromArgb(251, 203, 51);
            txtCapitalInicial.BorderStyle = BorderStyle.None;
            txtCapitalInicial.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCapitalInicial.Location = new Point(179, 23);
            txtCapitalInicial.Margin = new Padding(3, 1, 3, 1);
            txtCapitalInicial.Name = "txtCapitalInicial";
            txtCapitalInicial.PlaceholderText = "Ingresar capital inicial";
            txtCapitalInicial.Size = new Size(276, 32);
            txtCapitalInicial.TabIndex = 43;
            txtCapitalInicial.TextAlign = HorizontalAlignment.Right;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(27, 27);
            label8.Name = "label8";
            label8.Size = new Size(140, 25);
            label8.TabIndex = 0;
            label8.Text = "Capital Inicial: ";
            // 
            // chkIngresarCapital
            // 
            chkIngresarCapital.AutoSize = true;
            chkIngresarCapital.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkIngresarCapital.Location = new Point(736, 259);
            chkIngresarCapital.Margin = new Padding(3, 1, 3, 1);
            chkIngresarCapital.Name = "chkIngresarCapital";
            chkIngresarCapital.Size = new Size(260, 34);
            chkIngresarCapital.TabIndex = 41;
            chkIngresarCapital.Text = "Ingresar capital inicial";
            chkIngresarCapital.UseVisualStyleBackColor = true;
            chkIngresarCapital.CheckedChanged += chkIngresarCapital_CheckedChanged_1;
            // 
            // chkSaldoInicial
            // 
            chkSaldoInicial.AutoSize = true;
            chkSaldoInicial.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSaldoInicial.Location = new Point(342, 176);
            chkSaldoInicial.Margin = new Padding(3, 1, 3, 1);
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
            txtSaldoActual.Location = new Point(375, 116);
            txtSaldoActual.Margin = new Padding(3, 1, 3, 1);
            txtSaldoActual.Name = "txtSaldoActual";
            txtSaldoActual.ReadOnly = true;
            txtSaldoActual.Size = new Size(186, 32);
            txtSaldoActual.TabIndex = 38;
            // 
            // lblCapitalInicial
            // 
            lblCapitalInicial.AutoSize = true;
            lblCapitalInicial.Font = new Font("Segoe UI", 18.8F, FontStyle.Bold);
            lblCapitalInicial.Location = new Point(389, 247);
            lblCapitalInicial.Name = "lblCapitalInicial";
            lblCapitalInicial.Size = new Size(87, 36);
            lblCapitalInicial.TabIndex = 45;
            lblCapitalInicial.Text = "L.0.00";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14.8F, FontStyle.Bold);
            label9.Location = new Point(342, 211);
            label9.Name = "label9";
            label9.Size = new Size(228, 28);
            label9.TabIndex = 46;
            label9.Text = "Capital de la parroquia";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(328, 95);
            pictureBox4.Margin = new Padding(3, 1, 3, 1);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(276, 79);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 37;
            pictureBox4.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(94, 7);
            label7.Name = "label7";
            label7.Size = new Size(832, 45);
            label7.TabIndex = 31;
            label7.Text = "SALDO DE CAJA CHICA Y CAPITAL DE LA PARROQUIA";
            // 
            // btnGastos
            // 
            btnGastos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnGastos.Location = new Point(370, 161);
            btnGastos.Name = "btnGastos";
            btnGastos.Size = new Size(125, 37);
            btnGastos.TabIndex = 15;
            btnGastos.Text = "Gastos";
            btnGastos.UseVisualStyleBackColor = true;
            btnGastos.Click += btnGastos_Click;
            // 
            // btnBancos
            // 
            btnBancos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnBancos.Location = new Point(858, 161);
            btnBancos.Name = "btnBancos";
            btnBancos.Size = new Size(125, 37);
            btnBancos.TabIndex = 16;
            btnBancos.Text = "Bancos";
            btnBancos.UseVisualStyleBackColor = true;
            btnBancos.Click += btnBancos_Click_1;
            // 
            // btnCajaChica
            // 
            btnCajaChica.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCajaChica.Location = new Point(574, 161);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Size = new Size(182, 37);
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
            btnIngresos.Location = new Point(127, 161);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(139, 37);
            btnIngresos.TabIndex = 23;
            btnIngresos.Text = "Ingresos";
            btnIngresos.UseVisualStyleBackColor = true;
            btnIngresos.Click += btnIngresos_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pnlAlertaDeslizante);
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
            panel1.Location = new Point(13, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1099, 517);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // pnlAlertaDeslizante
            // 
            pnlAlertaDeslizante.BackColor = Color.Gold;
            pnlAlertaDeslizante.Controls.Add(lblAlertaMensaje);
            pnlAlertaDeslizante.Dock = DockStyle.Top;
            pnlAlertaDeslizante.Location = new Point(0, 0);
            pnlAlertaDeslizante.Margin = new Padding(1);
            pnlAlertaDeslizante.Name = "pnlAlertaDeslizante";
            pnlAlertaDeslizante.Size = new Size(1099, 30);
            pnlAlertaDeslizante.TabIndex = 26;
            pnlAlertaDeslizante.Paint += pnlAlertaDeslizante_Paint;
            // 
            // lblAlertaMensaje
            // 
            lblAlertaMensaje.AutoSize = true;
            lblAlertaMensaje.Dock = DockStyle.Top;
            lblAlertaMensaje.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAlertaMensaje.ForeColor = SystemColors.ActiveCaptionText;
            lblAlertaMensaje.Location = new Point(0, 0);
            lblAlertaMensaje.Margin = new Padding(1, 0, 1, 0);
            lblAlertaMensaje.Name = "lblAlertaMensaje";
            lblAlertaMensaje.Size = new Size(30, 25);
            lblAlertaMensaje.TabIndex = 25;
            lblAlertaMensaje.Text = "...";
            lblAlertaMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelLinea
            // 
            panelLinea.BackColor = Color.FromArgb(43, 56, 143);
            panelLinea.Location = new Point(0, 205);
            panelLinea.Name = "panelLinea";
            panelLinea.Size = new Size(1099, 1);
            panelLinea.TabIndex = 24;
            // 
            // FRM_42
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1121, 539);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "FRM_42";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ventana_Principal_Sacerdotes y Empleados";
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
            panelBancos2.PerformLayout();
            panelCajaChica2.ResumeLayout(false);
            panelCajaChica2.PerformLayout();
            panelIngresarCapital.ResumeLayout(false);
            panelIngresarCapital.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlAlertaDeslizante.ResumeLayout(false);
            pnlAlertaDeslizante.PerformLayout();
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
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private DateTimePicker dateTimePicker2;
        private PictureBox pictureBox5;
        private PictureBox pictureBox7;
        private DataGridViewTextBoxColumn Id_Transaccion1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private PictureBox pictureBox6;
        private PictureBox pictureBox8;
        private Label lblAlertaMensaje;
        private Panel pnlAlertaDeslizante;
        private System.Windows.Forms.Timer timer1;
        private Label label6;
        private Label label5;
        private Button button5;
        private Panel panelIngresarCapital;
        private Button btnCancelarCapital;
        private Button btnGuardarCapital;
        private TextBox txtCapitalInicial;
        private Label label8;
        private CheckBox chkIngresarCapital;
        private Label lblCapitalInicial;
        private Panel panel5;
        private Label label9;
    }
}