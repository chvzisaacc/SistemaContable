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
            panelIngresos = new Panel();
            button2 = new Button();
            button1 = new Button();
            dtpFecha = new DateTimePicker();
            btnGuardar = new Button();
            dataGridView1 = new DataGridView();
            NombreCuenta = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            txtNoReferencia = new TextBox();
            lblNoReferencia = new Label();
            lblFecha = new Label();
            lblOrigen = new Label();
            cmbOrigen = new ComboBox();
            panelCajaChica2 = new Panel();
            textBox1 = new TextBox();
            chkSaldoInicial = new CheckBox();
            txtSaldoActual = new TextBox();
            pictureBox4 = new PictureBox();
            btnDetalle = new Button();
            label7 = new Label();
            panelBancos2 = new Panel();
            button3 = new Button();
            cmbAcciones = new ComboBox();
            cmbInteresesBancarios = new ComboBox();
            cmbCuentas = new ComboBox();
            panel5 = new Panel();
            panelGastos2 = new Panel();
            btnGuardar2 = new Button();
            dgvGastos = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            txtNoReferencia2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            cmbOrigen2 = new ComboBox();
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
            panelIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelCajaChica2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panelBancos2.SuspendLayout();
            panelGastos2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(371, 82);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(255, 55);
            label1.TabIndex = 0;
            label1.Text = "SACERDOTE";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1274, 33);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 105);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1431, 33);
            pictureBox2.Margin = new Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(113, 105);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(47, 0);
            pictureBox3.Margin = new Padding(4, 5, 4, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(219, 253);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panelIngresos);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panel5);
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Location = new Point(4, 355);
            panelContenedor.Margin = new Padding(4, 5, 4, 5);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1566, 503);
            panelContenedor.TabIndex = 8;
            // 
            // panelIngresos
            // 
            panelIngresos.Controls.Add(button2);
            panelIngresos.Controls.Add(button1);
            panelIngresos.Controls.Add(dtpFecha);
            panelIngresos.Controls.Add(btnGuardar);
            panelIngresos.Controls.Add(dataGridView1);
            panelIngresos.Controls.Add(txtNoReferencia);
            panelIngresos.Controls.Add(lblNoReferencia);
            panelIngresos.Controls.Add(lblFecha);
            panelIngresos.Controls.Add(lblOrigen);
            panelIngresos.Controls.Add(cmbOrigen);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Location = new Point(0, 0);
            panelIngresos.Margin = new Padding(4, 5, 4, 5);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(1566, 503);
            panelIngresos.TabIndex = 44;
            panelIngresos.Paint += panelIngresos_Paint;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImageLayout = ImageLayout.Center;
            button2.Location = new Point(23, 463);
            button2.Name = "button2";
            button2.Size = new Size(21, 33);
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
            button1.Location = new Point(1409, 437);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(134, 47);
            button1.TabIndex = 29;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            //button1.Click += button1_Click_1;
            // 
            // dtpFecha
            // 
            dtpFecha.CalendarFont = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.CalendarForeColor = SystemColors.ControlLightLight;
            dtpFecha.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(191, 103);
            dtpFecha.Margin = new Padding(4, 5, 4, 5);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(345, 31);
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
            btnGuardar.Location = new Point(1543, 613);
            btnGuardar.Margin = new Padding(4, 5, 4, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(203, 80);
            btnGuardar.TabIndex = 20;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NombreCuenta, cDetalle, cSaldo });
            dataGridView1.Location = new Point(61, 165);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1340, 333);
            dataGridView1.TabIndex = 19;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dgvIngresos_CellContentClick;
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
            // txtNoReferencia
            // 
            txtNoReferencia.BackColor = Color.FromArgb(251, 203, 51);
            txtNoReferencia.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNoReferencia.Location = new Point(839, 38);
            txtNoReferencia.Margin = new Padding(4, 5, 4, 5);
            txtNoReferencia.Multiline = true;
            txtNoReferencia.Name = "txtNoReferencia";
            txtNoReferencia.Size = new Size(353, 29);
            txtNoReferencia.TabIndex = 18;
            // 
            // lblNoReferencia
            // 
            lblNoReferencia.AutoSize = true;
            lblNoReferencia.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoReferencia.Location = new Point(622, 31);
            lblNoReferencia.Margin = new Padding(4, 0, 4, 0);
            lblNoReferencia.Name = "lblNoReferencia";
            lblNoReferencia.Size = new Size(209, 38);
            lblNoReferencia.TabIndex = 17;
            lblNoReferencia.Text = "No. Referencia";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(23, 98);
            lblFecha.Margin = new Padding(4, 0, 4, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(92, 38);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "Fecha";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigen.Location = new Point(23, 40);
            lblOrigen.Margin = new Padding(4, 0, 4, 0);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(106, 38);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen";
            // 
            // cmbOrigen
            // 
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(191, 42);
            cmbOrigen.Margin = new Padding(4, 3, 4, 3);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(345, 29);
            cmbOrigen.TabIndex = 13;
            cmbOrigen.Text = "Seleccionar";
            cmbOrigen.SelectedIndexChanged += cmbOrigen_SelectedIndexChanged;
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
            panelCajaChica2.Margin = new Padding(4, 3, 4, 3);
            panelCajaChica2.Name = "panelCajaChica2";
            panelCajaChica2.Size = new Size(1566, 503);
            panelCajaChica2.TabIndex = 42;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(596, 223);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(31, 48);
            textBox1.TabIndex = 40;
            textBox1.Text = "L";
            // 
            // chkSaldoInicial
            // 
            chkSaldoInicial.AutoSize = true;
            chkSaldoInicial.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSaldoInicial.Location = new Point(523, 343);
            chkSaldoInicial.Margin = new Padding(4, 3, 4, 3);
            chkSaldoInicial.Name = "chkSaldoInicial";
            chkSaldoInicial.Size = new Size(363, 49);
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
            txtSaldoActual.Location = new Point(636, 223);
            txtSaldoActual.Margin = new Padding(4, 3, 4, 3);
            txtSaldoActual.Name = "txtSaldoActual";
            txtSaldoActual.Size = new Size(197, 48);
            txtSaldoActual.TabIndex = 38;
            txtSaldoActual.Text = "0.00";
            txtSaldoActual.TextChanged += txtSaldoActual_TextChanged;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(514, 187);
            pictureBox4.Margin = new Padding(4, 3, 4, 3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(394, 133);
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
            btnDetalle.Location = new Point(981, 223);
            btnDetalle.Margin = new Padding(4, 5, 4, 5);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(190, 63);
            btnDetalle.TabIndex = 36;
            btnDetalle.Text = "Detalle";
            btnDetalle.UseVisualStyleBackColor = false;
            btnDetalle.Click += btnDetalle_Click_1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(544, 53);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(382, 65);
            label7.TabIndex = 31;
            label7.Text = "SALDO ACTUAL";
            // 
            // panelBancos2
            // 
            panelBancos2.Controls.Add(button3);
            panelBancos2.Controls.Add(cmbAcciones);
            panelBancos2.Controls.Add(cmbInteresesBancarios);
            panelBancos2.Controls.Add(cmbCuentas);
            panelBancos2.Dock = DockStyle.Fill;
            panelBancos2.Location = new Point(0, 0);
            panelBancos2.Margin = new Padding(4, 5, 4, 5);
            panelBancos2.Name = "panelBancos2";
            panelBancos2.Size = new Size(1566, 503);
            panelBancos2.TabIndex = 41;
            panelBancos2.Visible = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(251, 203, 51);
            button3.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(433, 267);
            button3.Name = "button3";
            button3.Size = new Size(400, 53);
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
            cmbAcciones.Location = new Point(981, 67);
            cmbAcciones.Margin = new Padding(4, 5, 4, 5);
            cmbAcciones.Name = "cmbAcciones";
            cmbAcciones.Size = new Size(277, 53);
            cmbAcciones.TabIndex = 7;
            cmbAcciones.Text = "Acciones";
            cmbAcciones.SelectedIndexChanged += cmbAcciones_SelectedIndexChanged;
            // 
            // cmbInteresesBancarios
            // 
            cmbInteresesBancarios.BackColor = Color.FromArgb(251, 203, 51);
            cmbInteresesBancarios.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            cmbInteresesBancarios.FormattingEnabled = true;
            cmbInteresesBancarios.Location = new Point(433, 167);
            cmbInteresesBancarios.Margin = new Padding(4, 5, 4, 5);
            cmbInteresesBancarios.Name = "cmbInteresesBancarios";
            cmbInteresesBancarios.Size = new Size(398, 53);
            cmbInteresesBancarios.TabIndex = 5;
            cmbInteresesBancarios.Text = "Intereses Bancarios";
            // 
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Items.AddRange(new object[] { "Cuentas de Ahorro", "Cuenta de Cheques" });
            cmbCuentas.Location = new Point(433, 67);
            cmbCuentas.Margin = new Padding(4, 5, 4, 5);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(398, 53);
            cmbCuentas.TabIndex = 4;
            cmbCuentas.Text = "Cuentas de Ahorro";
            cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            // 
            // panel5
            // 
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(407, 25);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(749, 367);
            panel5.TabIndex = 49;
            // 
            // panelGastos2
            // 
            panelGastos2.Controls.Add(btnGuardar2);
            panelGastos2.Controls.Add(dgvGastos);
            panelGastos2.Controls.Add(txtNoReferencia2);
            panelGastos2.Controls.Add(label2);
            panelGastos2.Controls.Add(label3);
            panelGastos2.Controls.Add(label4);
            panelGastos2.Controls.Add(cmbOrigen2);
            panelGastos2.Dock = DockStyle.Fill;
            panelGastos2.Location = new Point(0, 0);
            panelGastos2.Margin = new Padding(4, 3, 4, 3);
            panelGastos2.Name = "panelGastos2";
            panelGastos2.Size = new Size(1566, 503);
            panelGastos2.TabIndex = 43;
            // 
            // btnGuardar2
            // 
            btnGuardar2.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar2.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardar2.FlatAppearance.BorderSize = 0;
            btnGuardar2.FlatStyle = FlatStyle.Flat;
            btnGuardar2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar2.ForeColor = SystemColors.Control;
            btnGuardar2.Location = new Point(1427, 437);
            btnGuardar2.Margin = new Padding(4, 5, 4, 5);
            btnGuardar2.Name = "btnGuardar2";
            btnGuardar2.Size = new Size(134, 47);
            btnGuardar2.TabIndex = 28;
            btnGuardar2.Text = "Guardar";
            btnGuardar2.UseVisualStyleBackColor = false;
            //btnGuardar2.Click += btnGuardar2_Click;
            // 
            // dgvGastos
            // 
            dgvGastos.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvGastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvGastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGastos.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvGastos.Location = new Point(44, 167);
            dgvGastos.Margin = new Padding(4, 5, 4, 5);
            dgvGastos.Name = "dgvGastos";
            dgvGastos.RowHeadersWidth = 51;
            dgvGastos.Size = new Size(1357, 318);
            dgvGastos.TabIndex = 27;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Nombre/Cuenta";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 350;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Detalle";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 350;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Saldo";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 350;
            // 
            // txtNoReferencia2
            // 
            txtNoReferencia2.BackColor = Color.FromArgb(251, 203, 51);
            txtNoReferencia2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNoReferencia2.Location = new Point(886, 32);
            txtNoReferencia2.Margin = new Padding(4, 5, 4, 5);
            txtNoReferencia2.Name = "txtNoReferencia2";
            txtNoReferencia2.Size = new Size(251, 49);
            txtNoReferencia2.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(574, 33);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(266, 48);
            label2.TabIndex = 25;
            label2.Text = "No. Referencia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(44, 103);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(116, 48);
            label3.TabIndex = 24;
            label3.Text = "Fecha";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(44, 28);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(134, 48);
            label4.TabIndex = 23;
            label4.Text = "Origen";
            // 
            // cmbOrigen2
            // 
            cmbOrigen2.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen2.FlatStyle = FlatStyle.Flat;
            cmbOrigen2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen2.FormattingEnabled = true;
            cmbOrigen2.Location = new Point(214, 28);
            cmbOrigen2.Margin = new Padding(4, 3, 4, 3);
            cmbOrigen2.Name = "cmbOrigen2";
            cmbOrigen2.Size = new Size(211, 46);
            cmbOrigen2.TabIndex = 22;
            cmbOrigen2.Text = "Seleccionar";
            // 
            // btnGastos
            // 
            btnGastos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnGastos.Location = new Point(527, 267);
            btnGastos.Margin = new Padding(4, 5, 4, 5);
            btnGastos.Name = "btnGastos";
            btnGastos.Size = new Size(179, 63);
            btnGastos.TabIndex = 15;
            btnGastos.Text = "Gastos";
            btnGastos.UseVisualStyleBackColor = true;
            btnGastos.Click += btnGastos_Click;
            // 
            // btnBancos
            // 
            btnBancos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnBancos.Location = new Point(1226, 267);
            btnBancos.Margin = new Padding(4, 5, 4, 5);
            btnBancos.Name = "btnBancos";
            btnBancos.Size = new Size(179, 63);
            btnBancos.TabIndex = 16;
            btnBancos.Text = "Bancos";
            btnBancos.UseVisualStyleBackColor = true;
            btnBancos.Click += btnBancos_Click_1;
            // 
            // btnCajaChica
            // 
            btnCajaChica.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCajaChica.Location = new Point(820, 267);
            btnCajaChica.Margin = new Padding(4, 5, 4, 5);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Size = new Size(260, 63);
            btnCajaChica.TabIndex = 17;
            btnCajaChica.Text = "Caja Chica";
            btnCajaChica.UseVisualStyleBackColor = true;
            btnCajaChica.Click += btnCajaChica_Click;
            // 
            // lblNoSeleccionado
            // 
            lblNoSeleccionado.AutoSize = true;
            lblNoSeleccionado.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoSeleccionado.Location = new Point(959, 1168);
            lblNoSeleccionado.Margin = new Padding(4, 0, 4, 0);
            lblNoSeleccionado.Name = "lblNoSeleccionado";
            lblNoSeleccionado.Size = new Size(711, 110);
            lblNoSeleccionado.TabIndex = 20;
            lblNoSeleccionado.Text = "¡Aún no seleccionas algo que hacer!\r\n   Comienza cuando lo prefieras";
            // 
            // btnIngresos
            // 
            btnIngresos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnIngresos.Location = new Point(183, 267);
            btnIngresos.Margin = new Padding(4, 5, 4, 5);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(199, 63);
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
            panel1.Location = new Point(17, 20);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1570, 863);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panelLinea
            // 
            panelLinea.BackColor = Color.FromArgb(43, 56, 143);
            panelLinea.Location = new Point(0, 342);
            panelLinea.Margin = new Padding(4, 5, 4, 5);
            panelLinea.Name = "panelLinea";
            panelLinea.Size = new Size(1570, 3);
            panelLinea.TabIndex = 24;
            // 
            // FRM_42
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1603, 898);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FRM_42";
            Text = "FRM_42";
            Load += FRM_42_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panelContenedor.ResumeLayout(false);
            panelIngresos.ResumeLayout(false);
            panelIngresos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelCajaChica2.ResumeLayout(false);
            panelCajaChica2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panelBancos2.ResumeLayout(false);
            panelGastos2.ResumeLayout(false);
            panelGastos2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).EndInit();
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
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
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
        private DataGridViewTextBoxColumn NombreCuenta;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
    }
}