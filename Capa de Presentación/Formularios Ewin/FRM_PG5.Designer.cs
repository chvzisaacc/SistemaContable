namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class FRM_PG5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG5));
            panel1 = new Panel();
            btnCatalagoCuenta = new Button();
            btnUsuario = new Button();
            panelContenedor = new Panel();
            panelCatalogoCuentas = new Panel();
            cmbCuenta = new ComboBox();
            btnGuardarCuenta = new Button();
            cmbTipoCuenta = new ComboBox();
            btnNuevaCuenta = new Button();
            button1 = new Button();
            dgvCatalogoCuentas = new DataGridView();
            txtIdCuenta = new TextBox();
            txtNombreCuenta = new TextBox();
            txtDetalle = new TextBox();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            panel10 = new Panel();
            panelUsuario = new Panel();
            label20 = new Label();
            label3 = new Label();
            txtBuscar = new TextBox();
            label2 = new Label();
            btnGuardarUsuario = new Button();
            btnInhabilitarUsuario = new Button();
            btnHabilitar = new Button();
            btnModificarUsuario = new Button();
            btnAgregar = new Button();
            btnGuardar = new Button();
            btnInhabilitar = new Button();
            btnEliminar = new Button();
            btnModificar = new Button();
            dgvUsuarios = new DataGridView();
            cmbEstado = new ComboBox();
            cmbParroquia = new ComboBox();
            cmbRol = new ComboBox();
            txtId = new TextBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            txtContraseña = new TextBox();
            txtUsuario = new TextBox();
            txtCorreo = new TextBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            panel9 = new Panel();
            pictureBox3 = new PictureBox();
            label8 = new Label();
            pictureBox4 = new PictureBox();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panelMensaje = new Panel();
            panel1.SuspendLayout();
            panelContenedor.SuspendLayout();
            panelCatalogoCuentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoCuentas).BeginInit();
            panelUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnCatalagoCuenta);
            panel1.Controls.Add(btnUsuario);
            panel1.Controls.Add(panelContenedor);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panelMensaje);
            panel1.Location = new Point(11, 12);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1258, 694);
            panel1.TabIndex = 8;
            panel1.Paint += panel1_Paint;
            // 
            // btnCatalagoCuenta
            // 
            btnCatalagoCuenta.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCatalagoCuenta.Location = new Point(296, 126);
            btnCatalagoCuenta.Margin = new Padding(3, 4, 3, 4);
            btnCatalagoCuenta.Name = "btnCatalagoCuenta";
            btnCatalagoCuenta.Size = new Size(286, 44);
            btnCatalagoCuenta.TabIndex = 19;
            btnCatalagoCuenta.Text = "Cátalago de Cuentas";
            btnCatalagoCuenta.UseVisualStyleBackColor = true;
            btnCatalagoCuenta.Click += btnCatalagoCuenta_Click;
            // 
            // btnUsuario
            // 
            btnUsuario.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnUsuario.Location = new Point(117, 126);
            btnUsuario.Margin = new Padding(3, 4, 3, 4);
            btnUsuario.Name = "btnUsuario";
            btnUsuario.Size = new Size(139, 44);
            btnUsuario.TabIndex = 18;
            btnUsuario.Text = "Usuario";
            btnUsuario.UseVisualStyleBackColor = true;
            btnUsuario.Click += btnUsuario_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panelCatalogoCuentas);
            panelContenedor.Controls.Add(panelUsuario);
            panelContenedor.Location = new Point(3, 178);
            panelContenedor.Margin = new Padding(3, 4, 3, 4);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1251, 512);
            panelContenedor.TabIndex = 17;
            // 
            // panelCatalogoCuentas
            // 
            panelCatalogoCuentas.BackColor = Color.White;
            panelCatalogoCuentas.Controls.Add(cmbCuenta);
            panelCatalogoCuentas.Controls.Add(btnGuardarCuenta);
            panelCatalogoCuentas.Controls.Add(cmbTipoCuenta);
            panelCatalogoCuentas.Controls.Add(btnNuevaCuenta);
            panelCatalogoCuentas.Controls.Add(button1);
            panelCatalogoCuentas.Controls.Add(dgvCatalogoCuentas);
            panelCatalogoCuentas.Controls.Add(txtIdCuenta);
            panelCatalogoCuentas.Controls.Add(txtNombreCuenta);
            panelCatalogoCuentas.Controls.Add(txtDetalle);
            panelCatalogoCuentas.Controls.Add(label15);
            panelCatalogoCuentas.Controls.Add(label16);
            panelCatalogoCuentas.Controls.Add(label17);
            panelCatalogoCuentas.Controls.Add(label18);
            panelCatalogoCuentas.Controls.Add(label19);
            panelCatalogoCuentas.Controls.Add(panel10);
            panelCatalogoCuentas.Dock = DockStyle.Fill;
            panelCatalogoCuentas.Location = new Point(0, 0);
            panelCatalogoCuentas.Margin = new Padding(3, 2, 3, 2);
            panelCatalogoCuentas.Name = "panelCatalogoCuentas";
            panelCatalogoCuentas.Size = new Size(1251, 512);
            panelCatalogoCuentas.TabIndex = 20;
            // 
            // cmbCuenta
            // 
            cmbCuenta.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuenta.FlatStyle = FlatStyle.Flat;
            cmbCuenta.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbCuenta.FormattingEnabled = true;
            cmbCuenta.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbCuenta.Location = new Point(1042, 162);
            cmbCuenta.Margin = new Padding(3, 2, 3, 2);
            cmbCuenta.Name = "cmbCuenta";
            cmbCuenta.Size = new Size(179, 33);
            cmbCuenta.TabIndex = 47;
            // 
            // btnGuardarCuenta
            // 
            btnGuardarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarCuenta.ForeColor = Color.White;
            btnGuardarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnGuardarCuenta.Location = new Point(986, 388);
            btnGuardarCuenta.Margin = new Padding(3, 2, 3, 2);
            btnGuardarCuenta.Name = "btnGuardarCuenta";
            btnGuardarCuenta.Size = new Size(158, 38);
            btnGuardarCuenta.TabIndex = 46;
            btnGuardarCuenta.Text = "Guardar";
            btnGuardarCuenta.UseVisualStyleBackColor = false;
            btnGuardarCuenta.UseWaitCursor = true;
            btnGuardarCuenta.Click += button6_Click;
            // 
            // cmbTipoCuenta
            // 
            cmbTipoCuenta.BackColor = Color.FromArgb(251, 203, 51);
            cmbTipoCuenta.FlatStyle = FlatStyle.Flat;
            cmbTipoCuenta.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbTipoCuenta.FormattingEnabled = true;
            cmbTipoCuenta.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbTipoCuenta.Location = new Point(1042, 206);
            cmbTipoCuenta.Margin = new Padding(3, 2, 3, 2);
            cmbTipoCuenta.Name = "cmbTipoCuenta";
            cmbTipoCuenta.Size = new Size(179, 33);
            cmbTipoCuenta.TabIndex = 43;
            // 
            // btnNuevaCuenta
            // 
            btnNuevaCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnNuevaCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevaCuenta.ForeColor = Color.White;
            btnNuevaCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnNuevaCuenta.Location = new Point(1014, 6);
            btnNuevaCuenta.Margin = new Padding(3, 2, 3, 2);
            btnNuevaCuenta.Name = "btnNuevaCuenta";
            btnNuevaCuenta.Size = new Size(130, 38);
            btnNuevaCuenta.TabIndex = 42;
            btnNuevaCuenta.Text = "Nueva cuenta";
            btnNuevaCuenta.UseVisualStyleBackColor = false;
            btnNuevaCuenta.UseWaitCursor = true;
            btnNuevaCuenta.Click += btnNuevaCuenta_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.Location = new Point(1007, 536);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(158, 38);
            button1.TabIndex = 41;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            // 
            // dgvCatalogoCuentas
            // 
            dgvCatalogoCuentas.AllowUserToAddRows = false;
            dgvCatalogoCuentas.AllowUserToDeleteRows = false;
            dgvCatalogoCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCatalogoCuentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCatalogoCuentas.Location = new Point(27, 52);
            dgvCatalogoCuentas.Margin = new Padding(3, 2, 3, 2);
            dgvCatalogoCuentas.Name = "dgvCatalogoCuentas";
            dgvCatalogoCuentas.ReadOnly = true;
            dgvCatalogoCuentas.RowHeadersWidth = 51;
            dgvCatalogoCuentas.Size = new Size(825, 388);
            dgvCatalogoCuentas.TabIndex = 37;
            dgvCatalogoCuentas.CellContentClick += dgvCatalogoCuentas_CellContentClick;
            // 
            // txtIdCuenta
            // 
            txtIdCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtIdCuenta.BorderStyle = BorderStyle.None;
            txtIdCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdCuenta.Location = new Point(1042, 82);
            txtIdCuenta.Margin = new Padding(3, 2, 3, 2);
            txtIdCuenta.Name = "txtIdCuenta";
            txtIdCuenta.Size = new Size(179, 31);
            txtIdCuenta.TabIndex = 33;
            // 
            // txtNombreCuenta
            // 
            txtNombreCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtNombreCuenta.BorderStyle = BorderStyle.None;
            txtNombreCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombreCuenta.Location = new Point(1042, 120);
            txtNombreCuenta.Margin = new Padding(3, 2, 3, 2);
            txtNombreCuenta.Name = "txtNombreCuenta";
            txtNombreCuenta.Size = new Size(179, 31);
            txtNombreCuenta.TabIndex = 31;
            // 
            // txtDetalle
            // 
            txtDetalle.BackColor = Color.FromArgb(251, 203, 51);
            txtDetalle.BorderStyle = BorderStyle.None;
            txtDetalle.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDetalle.Location = new Point(1042, 248);
            txtDetalle.Margin = new Padding(3, 2, 3, 2);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(179, 31);
            txtDetalle.TabIndex = 26;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10.8F);
            label15.Location = new Point(902, 126);
            label15.Name = "label15";
            label15.Size = new Size(78, 25);
            label15.TabIndex = 20;
            label15.Text = "Nombre";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 10.8F);
            label16.Location = new Point(902, 166);
            label16.Name = "label16";
            label16.Size = new Size(67, 25);
            label16.TabIndex = 19;
            label16.Text = "Cuenta";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 10.8F);
            label17.Location = new Point(902, 206);
            label17.Name = "label17";
            label17.Size = new Size(129, 25);
            label17.TabIndex = 18;
            label17.Text = "Tipo de cuenta";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 10.8F);
            label18.Location = new Point(902, 248);
            label18.Name = "label18";
            label18.Size = new Size(66, 25);
            label18.TabIndex = 17;
            label18.Text = "Detalle";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 10.8F);
            label19.Location = new Point(902, 86);
            label19.Name = "label19";
            label19.Size = new Size(71, 25);
            label19.TabIndex = 16;
            label19.Text = "Codigo";
            // 
            // panel10
            // 
            panel10.Location = new Point(182, 6);
            panel10.Margin = new Padding(3, 2, 3, 2);
            panel10.Name = "panel10";
            panel10.Size = new Size(0, 0);
            panel10.TabIndex = 7;
            // 
            // panelUsuario
            // 
            panelUsuario.BackColor = Color.White;
            panelUsuario.Controls.Add(label20);
            panelUsuario.Controls.Add(label3);
            panelUsuario.Controls.Add(txtBuscar);
            panelUsuario.Controls.Add(label2);
            panelUsuario.Controls.Add(btnGuardarUsuario);
            panelUsuario.Controls.Add(btnInhabilitarUsuario);
            panelUsuario.Controls.Add(btnHabilitar);
            panelUsuario.Controls.Add(btnModificarUsuario);
            panelUsuario.Controls.Add(btnAgregar);
            panelUsuario.Controls.Add(btnGuardar);
            panelUsuario.Controls.Add(btnInhabilitar);
            panelUsuario.Controls.Add(btnEliminar);
            panelUsuario.Controls.Add(btnModificar);
            panelUsuario.Controls.Add(dgvUsuarios);
            panelUsuario.Controls.Add(cmbEstado);
            panelUsuario.Controls.Add(cmbParroquia);
            panelUsuario.Controls.Add(cmbRol);
            panelUsuario.Controls.Add(txtId);
            panelUsuario.Controls.Add(txtApellido);
            panelUsuario.Controls.Add(txtNombre);
            panelUsuario.Controls.Add(txtContraseña);
            panelUsuario.Controls.Add(txtUsuario);
            panelUsuario.Controls.Add(txtCorreo);
            panelUsuario.Controls.Add(label13);
            panelUsuario.Controls.Add(label12);
            panelUsuario.Controls.Add(label11);
            panelUsuario.Controls.Add(label10);
            panelUsuario.Controls.Add(label9);
            panelUsuario.Controls.Add(label7);
            panelUsuario.Controls.Add(label6);
            panelUsuario.Controls.Add(label5);
            panelUsuario.Controls.Add(label4);
            panelUsuario.Controls.Add(panel9);
            panelUsuario.Dock = DockStyle.Fill;
            panelUsuario.Location = new Point(0, 0);
            panelUsuario.Margin = new Padding(3, 2, 3, 2);
            panelUsuario.Name = "panelUsuario";
            panelUsuario.Size = new Size(1251, 512);
            panelUsuario.TabIndex = 19;
            panelUsuario.Paint += panelUsuario_Paint;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 10.8F);
            label20.Location = new Point(27, 414);
            label20.Name = "label20";
            label20.Size = new Size(66, 25);
            label20.TabIndex = 50;
            label20.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.8F);
            label3.Location = new Point(27, 372);
            label3.Name = "label3";
            label3.Size = new Size(87, 25);
            label3.TabIndex = 49;
            label3.Text = "Parroquia";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(613, 30);
            txtBuscar.Margin = new Padding(3, 2, 3, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(633, 27);
            txtBuscar.TabIndex = 48;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(549, 34);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 47;
            label2.Text = "Buscar: ";
            // 
            // btnGuardarUsuario
            // 
            btnGuardarUsuario.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardarUsuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarUsuario.ForeColor = Color.White;
            btnGuardarUsuario.ImageAlign = ContentAlignment.TopCenter;
            btnGuardarUsuario.Location = new Point(1039, 452);
            btnGuardarUsuario.Margin = new Padding(3, 2, 3, 2);
            btnGuardarUsuario.Name = "btnGuardarUsuario";
            btnGuardarUsuario.Size = new Size(158, 38);
            btnGuardarUsuario.TabIndex = 46;
            btnGuardarUsuario.Text = "Guardar";
            btnGuardarUsuario.UseVisualStyleBackColor = false;
            btnGuardarUsuario.UseWaitCursor = true;
            btnGuardarUsuario.Click += btnGuardarUsuario_Click;
            // 
            // btnInhabilitarUsuario
            // 
            btnInhabilitarUsuario.BackColor = Color.FromArgb(43, 56, 143);
            btnInhabilitarUsuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInhabilitarUsuario.ForeColor = Color.White;
            btnInhabilitarUsuario.ImageAlign = ContentAlignment.TopCenter;
            btnInhabilitarUsuario.Location = new Point(832, 452);
            btnInhabilitarUsuario.Margin = new Padding(3, 2, 3, 2);
            btnInhabilitarUsuario.Name = "btnInhabilitarUsuario";
            btnInhabilitarUsuario.Size = new Size(158, 38);
            btnInhabilitarUsuario.TabIndex = 45;
            btnInhabilitarUsuario.Text = "Inhabilitar";
            btnInhabilitarUsuario.UseVisualStyleBackColor = false;
            btnInhabilitarUsuario.UseWaitCursor = true;
            btnInhabilitarUsuario.Click += btnInhabilitarUsuario_Click;
            // 
            // btnHabilitar
            // 
            btnHabilitar.BackColor = Color.FromArgb(43, 56, 143);
            btnHabilitar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHabilitar.ForeColor = Color.White;
            btnHabilitar.ImageAlign = ContentAlignment.TopCenter;
            btnHabilitar.Location = new Point(613, 452);
            btnHabilitar.Margin = new Padding(3, 2, 3, 2);
            btnHabilitar.Name = "btnHabilitar";
            btnHabilitar.Size = new Size(158, 38);
            btnHabilitar.TabIndex = 44;
            btnHabilitar.Text = "Habilitar";
            btnHabilitar.UseVisualStyleBackColor = false;
            btnHabilitar.UseWaitCursor = true;
            btnHabilitar.Click += btnHabilitar_Click;
            // 
            // btnModificarUsuario
            // 
            btnModificarUsuario.BackColor = Color.FromArgb(43, 56, 143);
            btnModificarUsuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificarUsuario.ForeColor = Color.White;
            btnModificarUsuario.ImageAlign = ContentAlignment.TopCenter;
            btnModificarUsuario.Location = new Point(379, 452);
            btnModificarUsuario.Margin = new Padding(3, 2, 3, 2);
            btnModificarUsuario.Name = "btnModificarUsuario";
            btnModificarUsuario.Size = new Size(158, 38);
            btnModificarUsuario.TabIndex = 43;
            btnModificarUsuario.Text = "Modificar";
            btnModificarUsuario.UseVisualStyleBackColor = false;
            btnModificarUsuario.UseWaitCursor = true;
            btnModificarUsuario.Click += btnModificarCuentaUsuario_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(43, 56, 143);
            btnAgregar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.ImageAlign = ContentAlignment.TopCenter;
            btnAgregar.Location = new Point(352, 22);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(130, 38);
            btnAgregar.TabIndex = 42;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.UseWaitCursor = true;
            btnAgregar.Click += btnAgregar_Click_1;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.ImageAlign = ContentAlignment.TopCenter;
            btnGuardar.Location = new Point(1039, 620);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(158, 38);
            btnGuardar.TabIndex = 41;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.UseWaitCursor = true;
            // 
            // btnInhabilitar
            // 
            btnInhabilitar.BackColor = Color.FromArgb(43, 56, 143);
            btnInhabilitar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInhabilitar.ForeColor = Color.White;
            btnInhabilitar.ImageAlign = ContentAlignment.TopCenter;
            btnInhabilitar.Location = new Point(832, 620);
            btnInhabilitar.Margin = new Padding(3, 2, 3, 2);
            btnInhabilitar.Name = "btnInhabilitar";
            btnInhabilitar.Size = new Size(158, 38);
            btnInhabilitar.TabIndex = 40;
            btnInhabilitar.Text = "Inhabilitar";
            btnInhabilitar.UseVisualStyleBackColor = false;
            btnInhabilitar.UseWaitCursor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(43, 56, 143);
            btnEliminar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.ImageAlign = ContentAlignment.TopCenter;
            btnEliminar.Location = new Point(613, 620);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(158, 38);
            btnEliminar.TabIndex = 39;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.UseWaitCursor = true;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(43, 56, 143);
            btnModificar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificar.ForeColor = Color.White;
            btnModificar.ImageAlign = ContentAlignment.TopCenter;
            btnModificar.Location = new Point(379, 620);
            btnModificar.Margin = new Padding(3, 2, 3, 2);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(158, 38);
            btnModificar.TabIndex = 38;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.UseWaitCursor = true;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(352, 78);
            dgvUsuarios.Margin = new Padding(3, 2, 3, 2);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.Size = new Size(893, 338);
            dgvUsuarios.TabIndex = 37;
            // 
            // cmbEstado
            // 
            cmbEstado.BackColor = Color.FromArgb(251, 203, 51);
            cmbEstado.FlatStyle = FlatStyle.Flat;
            cmbEstado.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(141, 406);
            cmbEstado.Margin = new Padding(3, 2, 3, 2);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(158, 33);
            cmbEstado.TabIndex = 36;
            cmbEstado.Text = "        ";
            // 
            // cmbParroquia
            // 
            cmbParroquia.BackColor = Color.FromArgb(251, 203, 51);
            cmbParroquia.FlatStyle = FlatStyle.Flat;
            cmbParroquia.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbParroquia.FormattingEnabled = true;
            cmbParroquia.Location = new Point(141, 364);
            cmbParroquia.Margin = new Padding(3, 2, 3, 2);
            cmbParroquia.Name = "cmbParroquia";
            cmbParroquia.Size = new Size(158, 33);
            cmbParroquia.TabIndex = 35;
            cmbParroquia.SelectedIndexChanged += cmbParroquia_SelectedIndexChanged;
            // 
            // cmbRol
            // 
            cmbRol.BackColor = Color.FromArgb(251, 203, 51);
            cmbRol.FlatStyle = FlatStyle.Flat;
            cmbRol.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(141, 326);
            cmbRol.Margin = new Padding(3, 2, 3, 2);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(158, 33);
            cmbRol.TabIndex = 34;
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(251, 203, 51);
            txtId.BorderStyle = BorderStyle.None;
            txtId.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtId.Location = new Point(141, 78);
            txtId.Margin = new Padding(3, 2, 3, 2);
            txtId.Name = "txtId";
            txtId.Size = new Size(154, 31);
            txtId.TabIndex = 33;
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(251, 203, 51);
            txtApellido.BorderStyle = BorderStyle.None;
            txtApellido.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtApellido.Location = new Point(141, 162);
            txtApellido.Margin = new Padding(3, 2, 3, 2);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(154, 31);
            txtApellido.TabIndex = 32;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(251, 203, 51);
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.Location = new Point(141, 120);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(154, 31);
            txtNombre.TabIndex = 31;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // txtContraseña
            // 
            txtContraseña.BackColor = Color.FromArgb(251, 203, 51);
            txtContraseña.BorderStyle = BorderStyle.None;
            txtContraseña.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContraseña.Location = new Point(141, 286);
            txtContraseña.Margin = new Padding(3, 2, 3, 2);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(154, 31);
            txtContraseña.TabIndex = 27;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.FromArgb(251, 203, 51);
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.Location = new Point(141, 242);
            txtUsuario.Margin = new Padding(3, 2, 3, 2);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(154, 31);
            txtUsuario.TabIndex = 26;
            // 
            // txtCorreo
            // 
            txtCorreo.BackColor = Color.FromArgb(251, 203, 51);
            txtCorreo.BorderStyle = BorderStyle.None;
            txtCorreo.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCorreo.Location = new Point(141, 202);
            txtCorreo.Margin = new Padding(3, 2, 3, 2);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(154, 31);
            txtCorreo.TabIndex = 25;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10.8F);
            label13.Location = new Point(21, 524);
            label13.Name = "label13";
            label13.Size = new Size(87, 25);
            label13.TabIndex = 24;
            label13.Text = "Parroquia";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10.8F);
            label12.Location = new Point(21, 564);
            label12.Name = "label12";
            label12.Size = new Size(66, 25);
            label12.TabIndex = 23;
            label12.Text = "Estado";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10.8F);
            label11.Location = new Point(27, 294);
            label11.Name = "label11";
            label11.Size = new Size(101, 25);
            label11.TabIndex = 22;
            label11.Text = "Contraseña";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10.8F);
            label10.Location = new Point(27, 334);
            label10.Name = "label10";
            label10.Size = new Size(37, 25);
            label10.TabIndex = 21;
            label10.Text = "Rol";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.8F);
            label9.Location = new Point(27, 126);
            label9.Name = "label9";
            label9.Size = new Size(78, 25);
            label9.TabIndex = 20;
            label9.Text = "Nombre";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.8F);
            label7.Location = new Point(27, 168);
            label7.Name = "label7";
            label7.Size = new Size(78, 25);
            label7.TabIndex = 19;
            label7.Text = "Apellido";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F);
            label6.Location = new Point(27, 208);
            label6.Name = "label6";
            label6.Size = new Size(66, 25);
            label6.TabIndex = 18;
            label6.Text = "Correo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.8F);
            label5.Location = new Point(27, 248);
            label5.Name = "label5";
            label5.Size = new Size(72, 25);
            label5.TabIndex = 17;
            label5.Text = "Usuario";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.8F);
            label4.Location = new Point(27, 82);
            label4.Name = "label4";
            label4.Size = new Size(30, 25);
            label4.TabIndex = 16;
            label4.Text = "ID";
            // 
            // panel9
            // 
            panel9.Location = new Point(175, 192);
            panel9.Margin = new Padding(3, 2, 3, 2);
            panel9.Name = "panel9";
            panel9.Size = new Size(0, 0);
            panel9.TabIndex = 7;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(1061, 26);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(77, 66);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 15;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.FromArgb(251, 203, 51);
            label8.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(43, 56, 143);
            label8.Location = new Point(1175, 42);
            label8.Name = "label8";
            label8.Size = new Size(39, 25);
            label8.TabIndex = 14;
            label8.Text = "AD";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(1153, 18);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(95, 88);
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox4.TabIndex = 13;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(43, 56, 143);
            panel2.Enabled = false;
            panel2.Location = new Point(413, 174);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(845, 2);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(43, 56, 143);
            panel3.Enabled = false;
            panel3.Location = new Point(0, 174);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(845, 2);
            panel3.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.Location = new Point(175, 192);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(134, 38);
            label1.Name = "label1";
            label1.Size = new Size(221, 38);
            label1.TabIndex = 1;
            label1.Text = "Administrador";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 14);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(97, 88);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelMensaje
            // 
            panelMensaje.BackgroundImage = (Image)resources.GetObject("panelMensaje.BackgroundImage");
            panelMensaje.BackgroundImageLayout = ImageLayout.Stretch;
            panelMensaje.Location = new Point(320, 178);
            panelMensaje.Margin = new Padding(3, 2, 3, 2);
            panelMensaje.Name = "panelMensaje";
            panelMensaje.Size = new Size(619, 340);
            panelMensaje.TabIndex = 22;
            // 
            // FRM_PG5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1282, 718);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FRM_PG5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRM_PG5";
            Load += FRM_PG5_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelContenedor.ResumeLayout(false);
            panelCatalogoCuentas.ResumeLayout(false);
            panelCatalogoCuentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoCuentas).EndInit();
            panelUsuario.ResumeLayout(false);
            panelUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel panel4;
        private Label label1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Panel panel3;
        private Label label8;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private Panel panelContenedor;
        private Panel panelUsuario;
        private Button btnAgregar;
        private Button btnGuardar;
        private Button btnInhabilitar;
        private Button btnEliminar;
        private Button btnModificar;
        private DataGridView dgvUsuarios;
        private ComboBox cmbEstado;
        private ComboBox cmbParroquia;
        private ComboBox cmbRol;
        private TextBox txtId;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private TextBox txtContraseña;
        private TextBox txtUsuario;
        private TextBox txtCorreo;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Panel panel9;
        private Panel panelCatalogoCuentas;
        private ComboBox cmbTipoCuenta;
        private Button btnNuevaCuenta;
        private Button button1;
        private DataGridView dgvCatalogoCuentas;
        private TextBox txtIdCuenta;
        private TextBox txtNombreCuenta;
        private TextBox txtDetalle;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Panel panel10;
        private Button btnUsuario;
        private Button btnCatalagoCuenta;
        private Button btnGuardarUsuario;
        private Button btnInhabilitarUsuario;
        private Button btnHabilitar;
        private Button btnModificarUsuario;
        private Button btnGuardarCuenta;
        private Panel panelMensaje;
        private TextBox txtBuscar;
        private Label label2;
        private Label label20;
        private Label label3;
        private ComboBox cmbCuenta;
    }
}