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
            panelBancos2 = new Panel();
            cmbAcciones = new ComboBox();
            cmbCD = new ComboBox();
            cmbInteresesBancarios = new ComboBox();
            cmbCuentas = new ComboBox();
            lblBancos = new Label();
            panelIngresos = new Panel();
            panelGastos2 = new Panel();
            panelCajaChica2 = new Panel();
            chkSaldoInicial = new CheckBox();
            txtSaldoActual = new TextBox();
            pictureBox4 = new PictureBox();
            btnDetalle = new Button();
            label7 = new Label();
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
            dtpFecha = new DateTimePicker();
            btnGuardar = new Button();
            dgvIngresos = new DataGridView();
            cNombreCuenta = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            txtNoReferencia = new TextBox();
            lblNoReferencia = new Label();
            lblFecha = new Label();
            lblOrigen = new Label();
            cmbOrigen = new ComboBox();
            btnGastos = new Button();
            btnBancos = new Button();
            btnCajaChica = new Button();
            pibImage = new PictureBox();
            lblNoSeleccionado = new Label();
            btnIngresos = new Button();
            panel1 = new Panel();
            panelLinea = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panelContenedor.SuspendLayout();
            panelBancos2.SuspendLayout();
            panelIngresos.SuspendLayout();
            panelGastos2.SuspendLayout();
            panelCajaChica2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pibImage).BeginInit();
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
            pictureBox1.Location = new Point(1601, 23);
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
            pictureBox2.Location = new Point(1753, 23);
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
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelIngresos);
            panelContenedor.Location = new Point(3, 265);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1447, 558);
            panelContenedor.TabIndex = 8;
            // 
            // panelBancos2
            // 
            panelBancos2.Controls.Add(cmbAcciones);
            panelBancos2.Controls.Add(cmbCD);
            panelBancos2.Controls.Add(cmbInteresesBancarios);
            panelBancos2.Controls.Add(cmbCuentas);
            panelBancos2.Controls.Add(lblBancos);
            panelBancos2.Dock = DockStyle.Fill;
            panelBancos2.Location = new Point(0, 0);
            panelBancos2.Name = "panelBancos2";
            panelBancos2.Size = new Size(1447, 558);
            panelBancos2.TabIndex = 41;
            panelBancos2.Visible = false;
            // 
            // cmbAcciones
            // 
            cmbAcciones.BackColor = Color.FromArgb(43, 56, 143);
            cmbAcciones.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbAcciones.ForeColor = SystemColors.ControlLightLight;
            cmbAcciones.FormattingEnabled = true;
            cmbAcciones.Items.AddRange(new object[] { "Agregar Saldo", "Transferencia entre cuentas", "Agregar cuenta bancaria", "Salida de dinero" });
            cmbAcciones.Location = new Point(580, 98);
            cmbAcciones.Name = "cmbAcciones";
            cmbAcciones.Size = new Size(195, 38);
            cmbAcciones.TabIndex = 7;
            cmbAcciones.Text = "Acciones";
            // 
            // cmbCD
            // 
            cmbCD.BackColor = Color.FromArgb(251, 203, 51);
            cmbCD.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCD.FormattingEnabled = true;
            cmbCD.Location = new Point(90, 254);
            cmbCD.Name = "cmbCD";
            cmbCD.Size = new Size(195, 38);
            cmbCD.TabIndex = 6;
            cmbCD.Text = "CD";
            // 
            // cmbInteresesBancarios
            // 
            cmbInteresesBancarios.BackColor = Color.FromArgb(251, 203, 51);
            cmbInteresesBancarios.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbInteresesBancarios.FormattingEnabled = true;
            cmbInteresesBancarios.Location = new Point(90, 178);
            cmbInteresesBancarios.Name = "cmbInteresesBancarios";
            cmbInteresesBancarios.Size = new Size(195, 33);
            cmbInteresesBancarios.TabIndex = 5;
            cmbInteresesBancarios.Text = "Intereses Bancarios";
            // 
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Items.AddRange(new object[] { "Cuentas de Ahorro", "Cuenta de Cheques" });
            cmbCuentas.Location = new Point(90, 102);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(195, 38);
            cmbCuentas.TabIndex = 4;
            cmbCuentas.Text = "Cuentas";
            // 
            // lblBancos
            // 
            lblBancos.AutoSize = true;
            lblBancos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBancos.Location = new Point(84, 22);
            lblBancos.Name = "lblBancos";
            lblBancos.Size = new Size(115, 40);
            lblBancos.TabIndex = 3;
            lblBancos.Text = "Bancos";
            // 
            // panelIngresos
            // 
            panelIngresos.Controls.Add(panelGastos2);
            panelIngresos.Controls.Add(dtpFecha);
            panelIngresos.Controls.Add(btnGuardar);
            panelIngresos.Controls.Add(dgvIngresos);
            panelIngresos.Controls.Add(txtNoReferencia);
            panelIngresos.Controls.Add(lblNoReferencia);
            panelIngresos.Controls.Add(lblFecha);
            panelIngresos.Controls.Add(lblOrigen);
            panelIngresos.Controls.Add(cmbOrigen);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Enabled = false;
            panelIngresos.Location = new Point(0, 0);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(1447, 558);
            panelIngresos.TabIndex = 22;
            // 
            // panelGastos2
            // 
            panelGastos2.Controls.Add(panelCajaChica2);
            panelGastos2.Controls.Add(btnGuardar2);
            panelGastos2.Controls.Add(dgvGastos);
            panelGastos2.Controls.Add(txtNoReferencia2);
            panelGastos2.Controls.Add(label2);
            panelGastos2.Controls.Add(label3);
            panelGastos2.Controls.Add(label4);
            panelGastos2.Controls.Add(cmbOrigen2);
            panelGastos2.Dock = DockStyle.Fill;
            panelGastos2.Location = new Point(0, 0);
            panelGastos2.Margin = new Padding(3, 2, 3, 2);
            panelGastos2.Name = "panelGastos2";
            panelGastos2.Size = new Size(1447, 558);
            panelGastos2.TabIndex = 23;
            // 
            // panelCajaChica2
            // 
            panelCajaChica2.Controls.Add(chkSaldoInicial);
            panelCajaChica2.Controls.Add(txtSaldoActual);
            panelCajaChica2.Controls.Add(pictureBox4);
            panelCajaChica2.Controls.Add(btnDetalle);
            panelCajaChica2.Controls.Add(label7);
            panelCajaChica2.Dock = DockStyle.Fill;
            panelCajaChica2.Location = new Point(0, 0);
            panelCajaChica2.Margin = new Padding(3, 2, 3, 2);
            panelCajaChica2.Name = "panelCajaChica2";
            panelCajaChica2.Size = new Size(1447, 558);
            panelCajaChica2.TabIndex = 31;
            // 
            // chkSaldoInicial
            // 
            chkSaldoInicial.AutoSize = true;
            chkSaldoInicial.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSaldoInicial.Location = new Point(75, 200);
            chkSaldoInicial.Margin = new Padding(3, 2, 3, 2);
            chkSaldoInicial.Name = "chkSaldoInicial";
            chkSaldoInicial.Size = new Size(245, 34);
            chkSaldoInicial.TabIndex = 39;
            chkSaldoInicial.Text = "Ingresar saldo inicial";
            chkSaldoInicial.UseVisualStyleBackColor = true;
            // 
            // txtSaldoActual
            // 
            txtSaldoActual.BackColor = Color.FromArgb(251, 203, 51);
            txtSaldoActual.BorderStyle = BorderStyle.None;
            txtSaldoActual.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSaldoActual.Location = new Point(172, 130);
            txtSaldoActual.Margin = new Padding(3, 2, 3, 2);
            txtSaldoActual.Name = "txtSaldoActual";
            txtSaldoActual.Size = new Size(99, 32);
            txtSaldoActual.TabIndex = 38;
            txtSaldoActual.Text = "L 0.00";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(69, 107);
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
            btnDetalle.Location = new Point(396, 128);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(133, 38);
            btnDetalle.TabIndex = 36;
            btnDetalle.Text = "Detalle";
            btnDetalle.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(90, 27);
            label7.Name = "label7";
            label7.Size = new Size(257, 45);
            label7.TabIndex = 31;
            label7.Text = "SALDO ACTUAL";
            // 
            // btnGuardar2
            // 
            btnGuardar2.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar2.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardar2.FlatAppearance.BorderSize = 0;
            btnGuardar2.FlatStyle = FlatStyle.Flat;
            btnGuardar2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar2.ForeColor = SystemColors.Control;
            btnGuardar2.Location = new Point(1088, 257);
            btnGuardar2.Name = "btnGuardar2";
            btnGuardar2.Size = new Size(142, 48);
            btnGuardar2.TabIndex = 28;
            btnGuardar2.Text = "Guardar";
            btnGuardar2.UseVisualStyleBackColor = false;
            // 
            // dgvGastos
            // 
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
            dgvGastos.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvGastos.Location = new Point(88, 107);
            dgvGastos.Name = "dgvGastos";
            dgvGastos.RowHeadersWidth = 51;
            dgvGastos.Size = new Size(955, 212);
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
            txtNoReferencia2.Location = new Point(777, 22);
            txtNoReferencia2.Name = "txtNoReferencia2";
            txtNoReferencia2.Size = new Size(177, 35);
            txtNoReferencia2.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(557, 28);
            label2.Name = "label2";
            label2.Size = new Size(182, 32);
            label2.TabIndex = 25;
            label2.Text = "No. Referencia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(88, 70);
            label3.Name = "label3";
            label3.Size = new Size(78, 32);
            label3.TabIndex = 24;
            label3.Text = "Fecha";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(88, 25);
            label4.Name = "label4";
            label4.Size = new Size(92, 32);
            label4.TabIndex = 23;
            label4.Text = "Origen";
            // 
            // cmbOrigen2
            // 
            cmbOrigen2.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen2.FlatStyle = FlatStyle.Flat;
            cmbOrigen2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen2.FormattingEnabled = true;
            cmbOrigen2.Location = new Point(208, 25);
            cmbOrigen2.Margin = new Padding(3, 2, 3, 2);
            cmbOrigen2.Name = "cmbOrigen2";
            cmbOrigen2.Size = new Size(149, 38);
            cmbOrigen2.TabIndex = 22;
            cmbOrigen2.Text = "Seleccionar";
            // 
            // dtpFecha
            // 
            dtpFecha.CalendarFont = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.CalendarForeColor = SystemColors.ControlLightLight;
            dtpFecha.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(190, 116);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(135, 23);
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
            btnGuardar.Location = new Point(1080, 368);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(142, 48);
            btnGuardar.TabIndex = 20;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // dgvIngresos
            // 
            dgvIngresos.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvIngresos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvIngresos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIngresos.Columns.AddRange(new DataGridViewColumn[] { cNombreCuenta, cDetalle, cSaldo });
            dgvIngresos.Location = new Point(70, 149);
            dgvIngresos.Name = "dgvIngresos";
            dgvIngresos.RowHeadersWidth = 51;
            dgvIngresos.Size = new Size(966, 241);
            dgvIngresos.TabIndex = 19;
            // 
            // cNombreCuenta
            // 
            cNombreCuenta.HeaderText = "Nombre/Cuenta";
            cNombreCuenta.MinimumWidth = 6;
            cNombreCuenta.Name = "cNombreCuenta";
            cNombreCuenta.Width = 350;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.MinimumWidth = 6;
            cDetalle.Name = "cDetalle";
            cDetalle.Width = 350;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.MinimumWidth = 6;
            cSaldo.Name = "cSaldo";
            cSaldo.Width = 350;
            // 
            // txtNoReferencia
            // 
            txtNoReferencia.BackColor = Color.FromArgb(251, 203, 51);
            txtNoReferencia.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNoReferencia.Location = new Point(759, 34);
            txtNoReferencia.Name = "txtNoReferencia";
            txtNoReferencia.Size = new Size(177, 35);
            txtNoReferencia.TabIndex = 18;
            // 
            // lblNoReferencia
            // 
            lblNoReferencia.AutoSize = true;
            lblNoReferencia.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoReferencia.Location = new Point(539, 40);
            lblNoReferencia.Name = "lblNoReferencia";
            lblNoReferencia.Size = new Size(182, 32);
            lblNoReferencia.TabIndex = 17;
            lblNoReferencia.Text = "No. Referencia";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(70, 107);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(78, 32);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "Fecha";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigen.Location = new Point(70, 38);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(92, 32);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen";
            // 
            // cmbOrigen
            // 
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(190, 38);
            cmbOrigen.Margin = new Padding(3, 2, 3, 2);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(135, 38);
            cmbOrigen.TabIndex = 13;
            cmbOrigen.Text = "Seleccionar";
            // 
            // btnGastos
            // 
            btnGastos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGastos.Location = new Point(430, 182);
            btnGastos.Name = "btnGastos";
            btnGastos.Size = new Size(140, 57);
            btnGastos.TabIndex = 15;
            btnGastos.Text = "Gastos";
            btnGastos.UseVisualStyleBackColor = true;
            btnGastos.Click += btnGastos_Click;
            // 
            // btnBancos
            // 
            btnBancos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBancos.Location = new Point(1067, 182);
            btnBancos.Name = "btnBancos";
            btnBancos.Size = new Size(140, 57);
            btnBancos.TabIndex = 16;
            btnBancos.Text = "Bancos";
            btnBancos.UseVisualStyleBackColor = true;
            // 
            // btnCajaChica
            // 
            btnCajaChica.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCajaChica.Location = new Point(714, 182);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Size = new Size(213, 57);
            btnCajaChica.TabIndex = 17;
            btnCajaChica.Text = "Caja Chica";
            btnCajaChica.UseVisualStyleBackColor = true;
            btnCajaChica.Click += btnCajaChica_Click;
            // 
            // pibImage
            // 
            pibImage.Image = (Image)resources.GetObject("pibImage.Image");
            pibImage.Location = new Point(823, 427);
            pibImage.Name = "pibImage";
            pibImage.Size = new Size(258, 229);
            pibImage.SizeMode = PictureBoxSizeMode.CenterImage;
            pibImage.TabIndex = 18;
            pibImage.TabStop = false;
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
            lblNoSeleccionado.Click += label3_Click;
            // 
            // btnIngresos
            // 
            btnIngresos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresos.Location = new Point(140, 182);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(146, 57);
            btnIngresos.TabIndex = 23;
            btnIngresos.Text = "Ingresos";
            btnIngresos.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panelLinea);
            panel1.Controls.Add(panelContenedor);
            panel1.Controls.Add(btnIngresos);
            panel1.Controls.Add(lblNoSeleccionado);
            panel1.Controls.Add(pibImage);
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
            panel1.Size = new Size(1920, 1080);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panelLinea
            // 
            panelLinea.BackColor = Color.FromArgb(43, 56, 143);
            panelLinea.Location = new Point(0, 245);
            panelLinea.Name = "panelLinea";
            panelLinea.Size = new Size(1450, 14);
            panelLinea.TabIndex = 24;
            // 
            // FRM_42
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1474, 831);
            Controls.Add(panel1);
            Name = "FRM_42";
            Text = "FRM_42";
            Load += FRM_42_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panelContenedor.ResumeLayout(false);
            panelBancos2.ResumeLayout(false);
            panelBancos2.PerformLayout();
            panelIngresos.ResumeLayout(false);
            panelIngresos.PerformLayout();
            panelGastos2.ResumeLayout(false);
            panelGastos2.PerformLayout();
            panelCajaChica2.ResumeLayout(false);
            panelCajaChica2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresos).EndInit();
            ((System.ComponentModel.ISupportInitialize)pibImage).EndInit();
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
        private PictureBox pibImage;
        private Label lblNoSeleccionado;
        private Button btnIngresos;
       // private Panel panelGastos;
       // private Panel panelCajaChica;
       // private Panel panelBancos;
        private Panel panel1;
        private Panel panelBancos2;
        private ComboBox cmbAcciones;
        private ComboBox cmbCD;
        private ComboBox cmbInteresesBancarios;
        private ComboBox cmbCuentas;
        private Label lblBancos;
        private Panel panelIngresos;
        private Panel panelGastos2;
        private Panel panelCajaChica2;
        private CheckBox chkSaldoInicial;
        private TextBox txtSaldoActual;
        private PictureBox pictureBox4;
        private Button btnDetalle;
        private Label label7;
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
        private DateTimePicker dtpFecha;
        private Button btnGuardar;
        private DataGridView dgvIngresos;
        private DataGridViewTextBoxColumn cNombreCuenta;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private TextBox txtNoReferencia;
        private Label lblNoReferencia;
        private Label lblFecha;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private Panel panelLinea;
    }
}