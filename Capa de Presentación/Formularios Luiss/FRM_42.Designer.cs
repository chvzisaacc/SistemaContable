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
            panelIngresos = new Panel();
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
            panelBancos2 = new Panel();
            cmbAcciones = new ComboBox();
            cmbCD = new ComboBox();
            cmbInteresesBancarios = new ComboBox();
            cmbCuentas = new ComboBox();
            lblBancos = new Label();
            panel5 = new Panel();
            panelCajaChica2 = new Panel();
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
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panelContenedor.SuspendLayout();
            panelGastos2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).BeginInit();
            panelIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIngresos).BeginInit();
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
            label1.Location = new Point(297, 65);
            label1.Name = "label1";
            label1.Size = new Size(211, 46);
            label1.TabIndex = 0;
            label1.Text = "SACERDOTE";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1020, 27);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(104, 84);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1145, 27);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(90, 84);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(38, 0);
            pictureBox3.Margin = new Padding(3, 4, 3, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(175, 203);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panelBancos2);
            panelContenedor.Controls.Add(panelCajaChica2);
            panelContenedor.Controls.Add(panelGastos2);
            panelContenedor.Controls.Add(panelIngresos);
            panelContenedor.Controls.Add(panel5);
            panelContenedor.Location = new Point(3, 284);
            panelContenedor.Margin = new Padding(3, 4, 3, 4);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1253, 402);
            panelContenedor.TabIndex = 8;
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
            panelGastos2.Name = "panelGastos2";
            panelGastos2.Size = new Size(1253, 402);
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
            btnGuardar2.Location = new Point(1142, 350);
            btnGuardar2.Margin = new Padding(3, 4, 3, 4);
            btnGuardar2.Name = "btnGuardar2";
            btnGuardar2.Size = new Size(108, 38);
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
            dgvGastos.Location = new Point(35, 133);
            dgvGastos.Margin = new Padding(3, 4, 3, 4);
            dgvGastos.Name = "dgvGastos";
            dgvGastos.RowHeadersWidth = 51;
            dgvGastos.Size = new Size(1086, 255);
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
            txtNoReferencia2.Location = new Point(709, 25);
            txtNoReferencia2.Margin = new Padding(3, 4, 3, 4);
            txtNoReferencia2.Name = "txtNoReferencia2";
            txtNoReferencia2.Size = new Size(202, 42);
            txtNoReferencia2.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(459, 27);
            label2.Name = "label2";
            label2.Size = new Size(224, 41);
            label2.TabIndex = 25;
            label2.Text = "No. Referencia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(35, 83);
            label3.Name = "label3";
            label3.Size = new Size(98, 41);
            label3.TabIndex = 24;
            label3.Text = "Fecha";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(35, 23);
            label4.Name = "label4";
            label4.Size = new Size(115, 41);
            label4.TabIndex = 23;
            label4.Text = "Origen";
            // 
            // cmbOrigen2
            // 
            cmbOrigen2.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen2.FlatStyle = FlatStyle.Flat;
            cmbOrigen2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen2.FormattingEnabled = true;
            cmbOrigen2.Location = new Point(172, 23);
            cmbOrigen2.Name = "cmbOrigen2";
            cmbOrigen2.Size = new Size(170, 39);
            cmbOrigen2.TabIndex = 22;
            cmbOrigen2.Text = "Seleccionar";
            // 
            // panelIngresos
            // 
            panelIngresos.Controls.Add(button1);
            panelIngresos.Controls.Add(dtpFecha);
            panelIngresos.Controls.Add(btnGuardar);
            panelIngresos.Controls.Add(dgvIngresos);
            panelIngresos.Controls.Add(txtNoReferencia);
            panelIngresos.Controls.Add(lblNoReferencia);
            panelIngresos.Controls.Add(lblFecha);
            panelIngresos.Controls.Add(lblOrigen);
            panelIngresos.Controls.Add(cmbOrigen);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Location = new Point(0, 0);
            panelIngresos.Margin = new Padding(3, 4, 3, 4);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(1253, 402);
            panelIngresos.TabIndex = 44;
            // 
            // dtpFecha
            // 
            dtpFecha.CalendarFont = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.CalendarForeColor = SystemColors.ControlLightLight;
            dtpFecha.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(155, 82);
            dtpFecha.Margin = new Padding(3, 4, 3, 4);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(187, 27);
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
            btnGuardar.Location = new Point(1234, 491);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(162, 64);
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
            dgvIngresos.Location = new Point(18, 131);
            dgvIngresos.Margin = new Padding(3, 4, 3, 4);
            dgvIngresos.Name = "dgvIngresos";
            dgvIngresos.RowHeadersWidth = 51;
            dgvIngresos.Size = new Size(1072, 267);
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
            txtNoReferencia.Location = new Point(686, 21);
            txtNoReferencia.Margin = new Padding(3, 4, 3, 4);
            txtNoReferencia.Name = "txtNoReferencia";
            txtNoReferencia.Size = new Size(202, 42);
            txtNoReferencia.TabIndex = 18;
            // 
            // lblNoReferencia
            // 
            lblNoReferencia.AutoSize = true;
            lblNoReferencia.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoReferencia.Location = new Point(441, 20);
            lblNoReferencia.Name = "lblNoReferencia";
            lblNoReferencia.Size = new Size(224, 41);
            lblNoReferencia.TabIndex = 17;
            lblNoReferencia.Text = "No. Referencia";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(18, 70);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(98, 41);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "Fecha";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigen.Location = new Point(18, 20);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(115, 41);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen";
            // 
            // cmbOrigen
            // 
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(155, 20);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(187, 39);
            cmbOrigen.TabIndex = 13;
            cmbOrigen.Text = "Seleccionar";
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
            panelBancos2.Margin = new Padding(3, 4, 3, 4);
            panelBancos2.Name = "panelBancos2";
            panelBancos2.Size = new Size(1253, 402);
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
            cmbAcciones.Location = new Point(771, 103);
            cmbAcciones.Margin = new Padding(3, 4, 3, 4);
            cmbAcciones.Name = "cmbAcciones";
            cmbAcciones.Size = new Size(222, 44);
            cmbAcciones.TabIndex = 7;
            cmbAcciones.Text = "Acciones";
            cmbAcciones.SelectedIndexChanged += cmbAcciones_SelectedIndexChanged;
            // 
            // cmbCD
            // 
            cmbCD.BackColor = Color.FromArgb(251, 203, 51);
            cmbCD.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCD.FormattingEnabled = true;
            cmbCD.Location = new Point(340, 257);
            cmbCD.Margin = new Padding(3, 4, 3, 4);
            cmbCD.Name = "cmbCD";
            cmbCD.Size = new Size(319, 44);
            cmbCD.TabIndex = 6;
            cmbCD.Text = "Certificado de Depósito";
            // 
            // cmbInteresesBancarios
            // 
            cmbInteresesBancarios.BackColor = Color.FromArgb(251, 203, 51);
            cmbInteresesBancarios.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            cmbInteresesBancarios.FormattingEnabled = true;
            cmbInteresesBancarios.Location = new Point(340, 182);
            cmbInteresesBancarios.Margin = new Padding(3, 4, 3, 4);
            cmbInteresesBancarios.Name = "cmbInteresesBancarios";
            cmbInteresesBancarios.Size = new Size(319, 44);
            cmbInteresesBancarios.TabIndex = 5;
            cmbInteresesBancarios.Text = "Intereses Bancarios";
            // 
            // cmbCuentas
            // 
            cmbCuentas.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentas.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuentas.FormattingEnabled = true;
            cmbCuentas.Items.AddRange(new object[] { "Cuentas de Ahorro", "Cuenta de Cheques" });
            cmbCuentas.Location = new Point(340, 101);
            cmbCuentas.Margin = new Padding(3, 4, 3, 4);
            cmbCuentas.Name = "cmbCuentas";
            cmbCuentas.Size = new Size(319, 44);
            cmbCuentas.TabIndex = 4;
            cmbCuentas.Text = "Cuentas de Ahorro";
            cmbCuentas.SelectedIndexChanged += cmbCuentas_SelectedIndexChanged;
            // 
            // lblBancos
            // 
            lblBancos.AutoSize = true;
            lblBancos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBancos.Location = new Point(414, 21);
            lblBancos.Name = "lblBancos";
            lblBancos.Size = new Size(145, 50);
            lblBancos.TabIndex = 3;
            lblBancos.Text = "Bancos";
            // 
            // panel5
            // 
            panel5.BackgroundImage = (Image)resources.GetObject("panel5.BackgroundImage");
            panel5.BackgroundImageLayout = ImageLayout.Stretch;
            panel5.Location = new Point(326, 20);
            panel5.Name = "panel5";
            panel5.Size = new Size(599, 293);
            panel5.TabIndex = 49;
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
            panelCajaChica2.Name = "panelCajaChica2";
            panelCajaChica2.Size = new Size(1253, 402);
            panelCajaChica2.TabIndex = 42;
            // 
            // chkSaldoInicial
            // 
            chkSaldoInicial.AutoSize = true;
            chkSaldoInicial.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSaldoInicial.Location = new Point(418, 274);
            chkSaldoInicial.Name = "chkSaldoInicial";
            chkSaldoInicial.Size = new Size(308, 42);
            chkSaldoInicial.TabIndex = 39;
            chkSaldoInicial.Text = "Ingresar saldo inicial";
            chkSaldoInicial.UseVisualStyleBackColor = true;
            // 
            // txtSaldoActual
            // 
            txtSaldoActual.BackColor = Color.FromArgb(251, 203, 51);
            txtSaldoActual.BorderStyle = BorderStyle.None;
            txtSaldoActual.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSaldoActual.Location = new Point(529, 180);
            txtSaldoActual.Name = "txtSaldoActual";
            txtSaldoActual.Size = new Size(113, 40);
            txtSaldoActual.TabIndex = 38;
            txtSaldoActual.Text = "L 0.00";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(411, 150);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(315, 107);
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
            btnDetalle.Location = new Point(785, 178);
            btnDetalle.Margin = new Padding(3, 4, 3, 4);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(152, 51);
            btnDetalle.TabIndex = 36;
            btnDetalle.Text = "Detalle";
            btnDetalle.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(435, 43);
            label7.Name = "label7";
            label7.Size = new Size(315, 54);
            label7.TabIndex = 31;
            label7.Text = "SALDO ACTUAL";
            // 
            // btnGastos
            // 
            btnGastos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnGastos.Location = new Point(422, 214);
            btnGastos.Margin = new Padding(3, 4, 3, 4);
            btnGastos.Name = "btnGastos";
            btnGastos.Size = new Size(143, 51);
            btnGastos.TabIndex = 15;
            btnGastos.Text = "Gastos";
            btnGastos.UseVisualStyleBackColor = true;
            btnGastos.Click += btnGastos_Click;
            // 
            // btnBancos
            // 
            btnBancos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnBancos.Location = new Point(981, 214);
            btnBancos.Margin = new Padding(3, 4, 3, 4);
            btnBancos.Name = "btnBancos";
            btnBancos.Size = new Size(143, 51);
            btnBancos.TabIndex = 16;
            btnBancos.Text = "Bancos";
            btnBancos.UseVisualStyleBackColor = true;
            btnBancos.Click += btnBancos_Click_1;
            // 
            // btnCajaChica
            // 
            btnCajaChica.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCajaChica.Location = new Point(656, 214);
            btnCajaChica.Margin = new Padding(3, 4, 3, 4);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Size = new Size(208, 51);
            btnCajaChica.TabIndex = 17;
            btnCajaChica.Text = "Caja Chica";
            btnCajaChica.UseVisualStyleBackColor = true;
            btnCajaChica.Click += btnCajaChica_Click;
            // 
            // lblNoSeleccionado
            // 
            lblNoSeleccionado.AutoSize = true;
            lblNoSeleccionado.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoSeleccionado.Location = new Point(767, 935);
            lblNoSeleccionado.Name = "lblNoSeleccionado";
            lblNoSeleccionado.Size = new Size(589, 92);
            lblNoSeleccionado.TabIndex = 20;
            lblNoSeleccionado.Text = "¡Aún no seleccionas algo que hacer!\r\n   Comienza cuando lo prefieras";
            // 
            // btnIngresos
            // 
            btnIngresos.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnIngresos.Location = new Point(146, 214);
            btnIngresos.Margin = new Padding(3, 4, 3, 4);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(159, 51);
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
            panel1.Location = new Point(14, 16);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1256, 690);
            panel1.TabIndex = 0;
            // 
            // panelLinea
            // 
            panelLinea.BackColor = Color.FromArgb(43, 56, 143);
            panelLinea.Location = new Point(0, 273);
            panelLinea.Margin = new Padding(3, 4, 3, 4);
            panelLinea.Name = "panelLinea";
            panelLinea.Size = new Size(1256, 3);
            panelLinea.TabIndex = 24;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(1127, 350);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(108, 38);
            button1.TabIndex = 29;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            // 
            // FRM_42
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1282, 719);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FRM_42";
            Text = "FRM_42";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panelContenedor.ResumeLayout(false);
            panelGastos2.ResumeLayout(false);
            panelGastos2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).EndInit();
            panelIngresos.ResumeLayout(false);
            panelIngresos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIngresos).EndInit();
            panelBancos2.ResumeLayout(false);
            panelBancos2.PerformLayout();
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
        private ComboBox cmbCD;
        private ComboBox cmbInteresesBancarios;
        private ComboBox cmbCuentas;
        private Label lblBancos;
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
        private DataGridView dgvIngresos;
        private DataGridViewTextBoxColumn cNombreCuenta;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private TextBox txtNoReferencia;
        private Label lblNoReferencia;
        private Label lblFecha;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private Panel panel5;
        private Button button1;
    }
}