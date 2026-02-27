namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class Ventana_Principal_Administrador
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Principal_Administrador));
            panel1 = new Panel();
            btn_catalago_cuenta = new Button();
            btn_usuario = new Button();
            panelContenedor = new Panel();
            panelCatalogoCuentas = new Panel();
            label21 = new Label();
            cmbNivel3 = new ComboBox();
            btnModificarCuenta = new Button();
            cmbEstadoCuenta = new ComboBox();
            label14 = new Label();
            btnInhabilitarCuenta = new Button();
            btnHabilitarCuenta = new Button();
            btnGuardarCuenta = new Button();
            btnNuevaCuenta = new Button();
            cmbNivel2 = new ComboBox();
            button1 = new Button();
            cmbNivel1 = new ComboBox();
            panel10 = new Panel();
            label19 = new Label();
            txtDetalle = new TextBox();
            txtIdCuenta = new TextBox();
            label18 = new Label();
            label15 = new Label();
            txtNombreCuenta = new TextBox();
            label16 = new Label();
            label17 = new Label();
            dgvCatalogoCuentas = new DataGridView();
            panelUsuario = new Panel();
            label20 = new Label();
            label3 = new Label();
            txt_buscar = new TextBox();
            label2 = new Label();
            btn_guardar = new Button();
            btn_inhabilitar = new Button();
            btn_habilitar = new Button();
            btn_modificar = new Button();
            btn_agregar = new Button();
            btnGuardar = new Button();
            btnInhabilitar = new Button();
            btnEliminar = new Button();
            btnModificar = new Button();
            dgv_usuarios = new DataGridView();
            cmb_estado = new ComboBox();
            cmb_parroquia = new ComboBox();
            cmb_rol = new ComboBox();
            txt_id = new TextBox();
            txt_apellido = new TextBox();
            txt_nombreCuenta = new TextBox();
            txt_contraseña = new TextBox();
            txt_usuario = new TextBox();
            txt_correo = new TextBox();
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
            ((System.ComponentModel.ISupportInitialize)dgv_usuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btn_catalago_cuenta);
            panel1.Controls.Add(btn_usuario);
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
            // btn_catalago_cuenta
            // 
            btn_catalago_cuenta.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btn_catalago_cuenta.Location = new Point(296, 126);
            btn_catalago_cuenta.Margin = new Padding(3, 4, 3, 4);
            btn_catalago_cuenta.Name = "btn_catalago_cuenta";
            btn_catalago_cuenta.Size = new Size(315, 44);
            btn_catalago_cuenta.TabIndex = 19;
            btn_catalago_cuenta.Text = "Catálogo de Cuentas";
            btn_catalago_cuenta.UseVisualStyleBackColor = true;
            btn_catalago_cuenta.Click += btnCatalagoCuenta_Click;
            // 
            // btn_usuario
            // 
            btn_usuario.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btn_usuario.Location = new Point(117, 126);
            btn_usuario.Margin = new Padding(3, 4, 3, 4);
            btn_usuario.Name = "btn_usuario";
            btn_usuario.Size = new Size(139, 44);
            btn_usuario.TabIndex = 18;
            btn_usuario.Text = "Usuario";
            btn_usuario.UseVisualStyleBackColor = true;
            btn_usuario.Click += btnUsuario_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panelUsuario);
            panelContenedor.Controls.Add(panelCatalogoCuentas);
            panelContenedor.Location = new Point(3, 178);
            panelContenedor.Margin = new Padding(3, 4, 3, 4);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1251, 512);
            panelContenedor.TabIndex = 17;
            // 
            // panelCatalogoCuentas
            // 
            panelCatalogoCuentas.BackColor = Color.White;
            panelCatalogoCuentas.Controls.Add(dgvCatalogoCuentas);
            panelCatalogoCuentas.Controls.Add(label21);
            panelCatalogoCuentas.Controls.Add(cmbNivel3);
            panelCatalogoCuentas.Controls.Add(btnModificarCuenta);
            panelCatalogoCuentas.Controls.Add(cmbEstadoCuenta);
            panelCatalogoCuentas.Controls.Add(label14);
            panelCatalogoCuentas.Controls.Add(btnInhabilitarCuenta);
            panelCatalogoCuentas.Controls.Add(btnHabilitarCuenta);
            panelCatalogoCuentas.Controls.Add(btnGuardarCuenta);
            panelCatalogoCuentas.Controls.Add(btnNuevaCuenta);
            panelCatalogoCuentas.Controls.Add(cmbNivel2);
            panelCatalogoCuentas.Controls.Add(button1);
            panelCatalogoCuentas.Controls.Add(cmbNivel1);
            panelCatalogoCuentas.Controls.Add(panel10);
            panelCatalogoCuentas.Controls.Add(label19);
            panelCatalogoCuentas.Controls.Add(txtDetalle);
            panelCatalogoCuentas.Controls.Add(txtIdCuenta);
            panelCatalogoCuentas.Controls.Add(label18);
            panelCatalogoCuentas.Controls.Add(label15);
            panelCatalogoCuentas.Controls.Add(txtNombreCuenta);
            panelCatalogoCuentas.Controls.Add(label16);
            panelCatalogoCuentas.Controls.Add(label17);
            panelCatalogoCuentas.Dock = DockStyle.Fill;
            panelCatalogoCuentas.Location = new Point(0, 0);
            panelCatalogoCuentas.Margin = new Padding(3, 2, 3, 2);
            panelCatalogoCuentas.Name = "panelCatalogoCuentas";
            panelCatalogoCuentas.Size = new Size(1251, 512);
            panelCatalogoCuentas.TabIndex = 20;
            panelCatalogoCuentas.Paint += panelCatalogoCuentas_Paint;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label21.Location = new Point(566, 81);
            label21.Name = "label21";
            label21.Size = new Size(55, 19);
            label21.TabIndex = 54;
            label21.Text = "Grupo:";
            // 
            // cmbNivel3
            // 
            cmbNivel3.BackColor = Color.FromArgb(251, 203, 51);
            cmbNivel3.FlatStyle = FlatStyle.Flat;
            cmbNivel3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbNivel3.FormattingEnabled = true;
            cmbNivel3.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbNivel3.Location = new Point(627, 78);
            cmbNivel3.Margin = new Padding(3, 2, 3, 2);
            cmbNivel3.Name = "cmbNivel3";
            cmbNivel3.Size = new Size(270, 25);
            cmbNivel3.TabIndex = 53;
            cmbNivel3.SelectedIndexChanged += cmbNivel3_SelectedIndexChanged;
            // 
            // btnModificarCuenta
            // 
            btnModificarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnModificarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificarCuenta.ForeColor = Color.White;
            btnModificarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnModificarCuenta.Location = new Point(504, 464);
            btnModificarCuenta.Margin = new Padding(3, 2, 3, 2);
            btnModificarCuenta.Name = "btnModificarCuenta";
            btnModificarCuenta.Size = new Size(158, 38);
            btnModificarCuenta.TabIndex = 52;
            btnModificarCuenta.Text = "Modificar";
            btnModificarCuenta.UseVisualStyleBackColor = false;
            btnModificarCuenta.UseWaitCursor = true;
            btnModificarCuenta.Click += btnModificarCuenta_Click;
            // 
            // cmbEstadoCuenta
            // 
            cmbEstadoCuenta.BackColor = Color.FromArgb(251, 203, 51);
            cmbEstadoCuenta.FlatStyle = FlatStyle.Flat;
            cmbEstadoCuenta.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbEstadoCuenta.FormattingEnabled = true;
            cmbEstadoCuenta.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbEstadoCuenta.Location = new Point(978, 49);
            cmbEstadoCuenta.Margin = new Padding(3, 2, 3, 2);
            cmbEstadoCuenta.Name = "cmbEstadoCuenta";
            cmbEstadoCuenta.Size = new Size(270, 25);
            cmbEstadoCuenta.TabIndex = 51;
            cmbEstadoCuenta.SelectedIndexChanged += cmbEstadoCuenta_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label14.Location = new Point(918, 51);
            label14.Name = "label14";
            label14.Size = new Size(53, 19);
            label14.TabIndex = 50;
            label14.Text = "Estado";
            // 
            // btnInhabilitarCuenta
            // 
            btnInhabilitarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnInhabilitarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInhabilitarCuenta.ForeColor = Color.White;
            btnInhabilitarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnInhabilitarCuenta.Location = new Point(337, 464);
            btnInhabilitarCuenta.Margin = new Padding(3, 2, 3, 2);
            btnInhabilitarCuenta.Name = "btnInhabilitarCuenta";
            btnInhabilitarCuenta.Size = new Size(158, 38);
            btnInhabilitarCuenta.TabIndex = 49;
            btnInhabilitarCuenta.Text = "Inhabilitar";
            btnInhabilitarCuenta.UseVisualStyleBackColor = false;
            btnInhabilitarCuenta.UseWaitCursor = true;
            btnInhabilitarCuenta.Click += btnInhabilitarCuenta_Click;
            // 
            // btnHabilitarCuenta
            // 
            btnHabilitarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnHabilitarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHabilitarCuenta.ForeColor = Color.White;
            btnHabilitarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnHabilitarCuenta.Location = new Point(171, 464);
            btnHabilitarCuenta.Margin = new Padding(3, 2, 3, 2);
            btnHabilitarCuenta.Name = "btnHabilitarCuenta";
            btnHabilitarCuenta.Size = new Size(158, 38);
            btnHabilitarCuenta.TabIndex = 48;
            btnHabilitarCuenta.Text = "Habilitar";
            btnHabilitarCuenta.UseVisualStyleBackColor = false;
            btnHabilitarCuenta.UseWaitCursor = true;
            btnHabilitarCuenta.Click += btnHabilitarCuenta_Click;
            // 
            // btnGuardarCuenta
            // 
            btnGuardarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarCuenta.ForeColor = Color.White;
            btnGuardarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnGuardarCuenta.Location = new Point(7, 464);
            btnGuardarCuenta.Margin = new Padding(3, 2, 3, 2);
            btnGuardarCuenta.Name = "btnGuardarCuenta";
            btnGuardarCuenta.Size = new Size(158, 38);
            btnGuardarCuenta.TabIndex = 46;
            btnGuardarCuenta.Text = "Guardar";
            btnGuardarCuenta.UseVisualStyleBackColor = false;
            btnGuardarCuenta.UseWaitCursor = true;
            btnGuardarCuenta.Click += button6_Click;
            // 
            // btnNuevaCuenta
            // 
            btnNuevaCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnNuevaCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevaCuenta.ForeColor = Color.White;
            btnNuevaCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnNuevaCuenta.Location = new Point(7, 6);
            btnNuevaCuenta.Margin = new Padding(3, 2, 3, 2);
            btnNuevaCuenta.Name = "btnNuevaCuenta";
            btnNuevaCuenta.Size = new Size(130, 38);
            btnNuevaCuenta.TabIndex = 42;
            btnNuevaCuenta.Text = "Nueva cuenta";
            btnNuevaCuenta.UseVisualStyleBackColor = false;
            btnNuevaCuenta.UseWaitCursor = true;
            btnNuevaCuenta.Click += btnNuevaCuenta_Click;
            // 
            // cmbNivel2
            // 
            cmbNivel2.BackColor = Color.FromArgb(251, 203, 51);
            cmbNivel2.FlatStyle = FlatStyle.Flat;
            cmbNivel2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbNivel2.FormattingEnabled = true;
            cmbNivel2.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbNivel2.Location = new Point(627, 48);
            cmbNivel2.Margin = new Padding(3, 2, 3, 2);
            cmbNivel2.Name = "cmbNivel2";
            cmbNivel2.Size = new Size(270, 25);
            cmbNivel2.TabIndex = 43;
            cmbNivel2.SelectedIndexChanged += cmbTipoCuenta_SelectedIndexChanged;
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
            // cmbNivel1
            // 
            cmbNivel1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNivel1.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbNivel1.BackColor = Color.FromArgb(251, 203, 51);
            cmbNivel1.FlatStyle = FlatStyle.Flat;
            cmbNivel1.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbNivel1.FormattingEnabled = true;
            cmbNivel1.IntegralHeight = false;
            cmbNivel1.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbNivel1.Location = new Point(627, 17);
            cmbNivel1.Margin = new Padding(3, 2, 3, 2);
            cmbNivel1.MaxDropDownItems = 6;
            cmbNivel1.Name = "cmbNivel1";
            cmbNivel1.Size = new Size(270, 25);
            cmbNivel1.TabIndex = 47;
            cmbNivel1.SelectedIndexChanged += cmbCuenta_SelectedIndexChanged;
            // 
            // panel10
            // 
            panel10.Location = new Point(182, 6);
            panel10.Margin = new Padding(3, 2, 3, 2);
            panel10.Name = "panel10";
            panel10.Size = new Size(0, 0);
            panel10.TabIndex = 7;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label19.Location = new Point(216, 14);
            label19.Name = "label19";
            label19.Size = new Size(58, 19);
            label19.TabIndex = 16;
            label19.Text = "Código";
            // 
            // txtDetalle
            // 
            txtDetalle.BackColor = Color.FromArgb(251, 203, 51);
            txtDetalle.BorderStyle = BorderStyle.None;
            txtDetalle.Font = new Font("Segoe UI", 8F);
            txtDetalle.Location = new Point(978, 21);
            txtDetalle.Margin = new Padding(3, 2, 3, 2);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(270, 18);
            txtDetalle.TabIndex = 26;
            txtDetalle.KeyPress += txtDetalle_KeyPress;
            // 
            // txtIdCuenta
            // 
            txtIdCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtIdCuenta.BorderStyle = BorderStyle.None;
            txtIdCuenta.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdCuenta.Location = new Point(277, 14);
            txtIdCuenta.Margin = new Padding(3, 2, 3, 2);
            txtIdCuenta.MaxLength = 5;
            txtIdCuenta.Name = "txtIdCuenta";
            txtIdCuenta.Size = new Size(218, 18);
            txtIdCuenta.TabIndex = 33;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label18.Location = new Point(915, 17);
            label18.Name = "label18";
            label18.Size = new Size(56, 19);
            label18.TabIndex = 17;
            label18.Text = "Detalle";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label15.Location = new Point(210, 46);
            label15.Name = "label15";
            label15.Size = new Size(65, 19);
            label15.TabIndex = 20;
            label15.Text = "Nombre";
            // 
            // txtNombreCuenta
            // 
            txtNombreCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtNombreCuenta.BorderStyle = BorderStyle.None;
            txtNombreCuenta.Font = new Font("Segoe UI", 8F);
            txtNombreCuenta.Location = new Point(277, 48);
            txtNombreCuenta.Margin = new Padding(3, 2, 3, 2);
            txtNombreCuenta.MaxLength = 25;
            txtNombreCuenta.Name = "txtNombreCuenta";
            txtNombreCuenta.Size = new Size(218, 18);
            txtNombreCuenta.TabIndex = 31;
            txtNombreCuenta.KeyPress += txtNombreCuenta_KeyPress;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label16.Location = new Point(528, 18);
            label16.Name = "label16";
            label16.Size = new Size(93, 19);
            label16.TabIndex = 19;
            label16.Text = "Típo Cuenta:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(506, 50);
            label17.Name = "label17";
            label17.Size = new Size(115, 19);
            label17.TabIndex = 18;
            label17.Text = "Suptipo Cuenta:";
            // 
            // dgvCatalogoCuentas
            // 
            dgvCatalogoCuentas.AllowUserToAddRows = false;
            dgvCatalogoCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCatalogoCuentas.BackgroundColor = SystemColors.Window;
            dgvCatalogoCuentas.BorderStyle = BorderStyle.None;
            dgvCatalogoCuentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvCatalogoCuentas.DefaultCellStyle = dataGridViewCellStyle2;
            dgvCatalogoCuentas.Location = new Point(7, 126);
            dgvCatalogoCuentas.Margin = new Padding(3, 2, 3, 2);
            dgvCatalogoCuentas.Name = "dgvCatalogoCuentas";
            dgvCatalogoCuentas.ReadOnly = true;
            dgvCatalogoCuentas.RowHeadersWidth = 51;
            dgvCatalogoCuentas.RowTemplate.Height = 50;
            dgvCatalogoCuentas.ScrollBars = ScrollBars.Vertical;
            dgvCatalogoCuentas.Size = new Size(1236, 334);
            dgvCatalogoCuentas.TabIndex = 37;
            dgvCatalogoCuentas.CellContentClick += dgvCatalogoCuentas_CellContentClick;
            // 
            // panelUsuario
            // 
            panelUsuario.BackColor = Color.White;
            panelUsuario.Controls.Add(label20);
            panelUsuario.Controls.Add(label3);
            panelUsuario.Controls.Add(txt_buscar);
            panelUsuario.Controls.Add(label2);
            panelUsuario.Controls.Add(btn_guardar);
            panelUsuario.Controls.Add(btn_inhabilitar);
            panelUsuario.Controls.Add(btn_habilitar);
            panelUsuario.Controls.Add(btn_modificar);
            panelUsuario.Controls.Add(btn_agregar);
            panelUsuario.Controls.Add(btnGuardar);
            panelUsuario.Controls.Add(btnInhabilitar);
            panelUsuario.Controls.Add(btnEliminar);
            panelUsuario.Controls.Add(btnModificar);
            panelUsuario.Controls.Add(dgv_usuarios);
            panelUsuario.Controls.Add(cmb_estado);
            panelUsuario.Controls.Add(cmb_parroquia);
            panelUsuario.Controls.Add(cmb_rol);
            panelUsuario.Controls.Add(txt_id);
            panelUsuario.Controls.Add(txt_apellido);
            panelUsuario.Controls.Add(txt_nombreCuenta);
            panelUsuario.Controls.Add(txt_contraseña);
            panelUsuario.Controls.Add(txt_usuario);
            panelUsuario.Controls.Add(txt_correo);
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
            label20.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label20.Location = new Point(7, 414);
            label20.Name = "label20";
            label20.Size = new Size(53, 19);
            label20.TabIndex = 50;
            label20.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label3.Location = new Point(7, 370);
            label3.Name = "label3";
            label3.Size = new Size(76, 19);
            label3.TabIndex = 49;
            label3.Text = "Parroquia";
            label3.Click += label3_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Location = new Point(534, 42);
            txt_buscar.Margin = new Padding(3, 2, 3, 2);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(270, 27);
            txt_buscar.TabIndex = 48;
            txt_buscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(470, 42);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 47;
            label2.Text = "Buscar: ";
            // 
            // btn_guardar
            // 
            btn_guardar.BackColor = Color.FromArgb(43, 56, 143);
            btn_guardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_guardar.ForeColor = Color.White;
            btn_guardar.ImageAlign = ContentAlignment.TopCenter;
            btn_guardar.Location = new Point(1088, 448);
            btn_guardar.Margin = new Padding(3, 2, 3, 2);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(158, 38);
            btn_guardar.TabIndex = 46;
            btn_guardar.Text = "Guardar";
            btn_guardar.UseVisualStyleBackColor = false;
            btn_guardar.UseWaitCursor = true;
            btn_guardar.Click += btnGuardarUsuario_Click;
            // 
            // btn_inhabilitar
            // 
            btn_inhabilitar.BackColor = Color.FromArgb(43, 56, 143);
            btn_inhabilitar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_inhabilitar.ForeColor = Color.White;
            btn_inhabilitar.ImageAlign = ContentAlignment.TopCenter;
            btn_inhabilitar.Location = new Point(802, 448);
            btn_inhabilitar.Margin = new Padding(3, 2, 3, 2);
            btn_inhabilitar.Name = "btn_inhabilitar";
            btn_inhabilitar.Size = new Size(158, 38);
            btn_inhabilitar.TabIndex = 45;
            btn_inhabilitar.Text = "Inhabilitar";
            btn_inhabilitar.UseVisualStyleBackColor = false;
            btn_inhabilitar.UseWaitCursor = true;
            btn_inhabilitar.Click += btnInhabilitarUsuario_Click;
            // 
            // btn_habilitar
            // 
            btn_habilitar.BackColor = Color.FromArgb(43, 56, 143);
            btn_habilitar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_habilitar.ForeColor = Color.White;
            btn_habilitar.ImageAlign = ContentAlignment.TopCenter;
            btn_habilitar.Location = new Point(515, 448);
            btn_habilitar.Margin = new Padding(3, 2, 3, 2);
            btn_habilitar.Name = "btn_habilitar";
            btn_habilitar.Size = new Size(158, 38);
            btn_habilitar.TabIndex = 44;
            btn_habilitar.Text = "Habilitar";
            btn_habilitar.UseVisualStyleBackColor = false;
            btn_habilitar.UseWaitCursor = true;
            btn_habilitar.Click += btnHabilitar_Click;
            // 
            // btn_modificar
            // 
            btn_modificar.BackColor = Color.FromArgb(43, 56, 143);
            btn_modificar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_modificar.ForeColor = Color.White;
            btn_modificar.ImageAlign = ContentAlignment.TopCenter;
            btn_modificar.Location = new Point(235, 448);
            btn_modificar.Margin = new Padding(3, 2, 3, 2);
            btn_modificar.Name = "btn_modificar";
            btn_modificar.Size = new Size(158, 38);
            btn_modificar.TabIndex = 43;
            btn_modificar.Text = "Modificar";
            btn_modificar.UseVisualStyleBackColor = false;
            btn_modificar.UseWaitCursor = true;
            btn_modificar.Click += btnModificarCuentaUsuario_Click;
            // 
            // btn_agregar
            // 
            btn_agregar.BackColor = Color.FromArgb(43, 56, 143);
            btn_agregar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_agregar.ForeColor = Color.White;
            btn_agregar.ImageAlign = ContentAlignment.TopCenter;
            btn_agregar.Location = new Point(293, 30);
            btn_agregar.Margin = new Padding(3, 2, 3, 2);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(130, 38);
            btn_agregar.TabIndex = 42;
            btn_agregar.Text = "Agregar";
            btn_agregar.UseVisualStyleBackColor = false;
            btn_agregar.UseWaitCursor = true;
            btn_agregar.Click += btnAgregar_Click_1;
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
            // dgv_usuarios
            // 
            dgv_usuarios.AllowUserToAddRows = false;
            dgv_usuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_usuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv_usuarios.BackgroundColor = SystemColors.Window;
            dgv_usuarios.BorderStyle = BorderStyle.None;
            dgv_usuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv_usuarios.DefaultCellStyle = dataGridViewCellStyle1;
            dgv_usuarios.Location = new Point(233, 78);
            dgv_usuarios.Margin = new Padding(3, 2, 3, 2);
            dgv_usuarios.Name = "dgv_usuarios";
            dgv_usuarios.RowHeadersVisible = false;
            dgv_usuarios.RowHeadersWidth = 51;
            dgv_usuarios.ScrollBars = ScrollBars.Vertical;
            dgv_usuarios.Size = new Size(1013, 353);
            dgv_usuarios.TabIndex = 37;
            dgv_usuarios.DataBindingComplete += dgv_usuarios_DataBindingComplete;
            // 
            // cmb_estado
            // 
            cmb_estado.BackColor = Color.FromArgb(251, 203, 51);
            cmb_estado.FlatStyle = FlatStyle.Flat;
            cmb_estado.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmb_estado.FormattingEnabled = true;
            cmb_estado.Location = new Point(82, 406);
            cmb_estado.Margin = new Padding(3, 2, 3, 2);
            cmb_estado.Name = "cmb_estado";
            cmb_estado.Size = new Size(135, 25);
            cmb_estado.TabIndex = 36;
            cmb_estado.Text = "        ";
            // 
            // cmb_parroquia
            // 
            cmb_parroquia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_parroquia.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_parroquia.BackColor = Color.FromArgb(251, 203, 51);
            cmb_parroquia.FlatStyle = FlatStyle.Flat;
            cmb_parroquia.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmb_parroquia.FormattingEnabled = true;
            cmb_parroquia.IntegralHeight = false;
            cmb_parroquia.Location = new Point(82, 362);
            cmb_parroquia.Margin = new Padding(3, 2, 3, 2);
            cmb_parroquia.MaxDropDownItems = 6;
            cmb_parroquia.Name = "cmb_parroquia";
            cmb_parroquia.Size = new Size(135, 25);
            cmb_parroquia.TabIndex = 35;
            cmb_parroquia.SelectedIndexChanged += cmbParroquia_SelectedIndexChanged;
            // 
            // cmb_rol
            // 
            cmb_rol.BackColor = Color.FromArgb(251, 203, 51);
            cmb_rol.FlatStyle = FlatStyle.Flat;
            cmb_rol.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmb_rol.FormattingEnabled = true;
            cmb_rol.Location = new Point(82, 322);
            cmb_rol.Margin = new Padding(3, 2, 3, 2);
            cmb_rol.Name = "cmb_rol";
            cmb_rol.Size = new Size(135, 25);
            cmb_rol.TabIndex = 34;
            // 
            // txt_id
            // 
            txt_id.BackColor = Color.FromArgb(251, 203, 51);
            txt_id.BorderStyle = BorderStyle.None;
            txt_id.Font = new Font("Segoe UI", 9F);
            txt_id.Location = new Point(82, 90);
            txt_id.Margin = new Padding(3, 2, 3, 2);
            txt_id.Name = "txt_id";
            txt_id.Size = new Size(134, 20);
            txt_id.TabIndex = 33;
            // 
            // txt_apellido
            // 
            txt_apellido.BackColor = Color.FromArgb(251, 203, 51);
            txt_apellido.BorderStyle = BorderStyle.None;
            txt_apellido.Font = new Font("Segoe UI", 9F);
            txt_apellido.Location = new Point(82, 170);
            txt_apellido.Margin = new Padding(3, 2, 3, 2);
            txt_apellido.MaxLength = 25;
            txt_apellido.Name = "txt_apellido";
            txt_apellido.Size = new Size(134, 20);
            txt_apellido.TabIndex = 32;
            txt_apellido.KeyPress += txt_apellido_KeyPress;
            // 
            // txt_nombreCuenta
            // 
            txt_nombreCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txt_nombreCuenta.BorderStyle = BorderStyle.None;
            txt_nombreCuenta.Font = new Font("Segoe UI", 9F);
            txt_nombreCuenta.Location = new Point(82, 126);
            txt_nombreCuenta.Margin = new Padding(3, 2, 3, 2);
            txt_nombreCuenta.MaxLength = 25;
            txt_nombreCuenta.Name = "txt_nombreCuenta";
            txt_nombreCuenta.Size = new Size(134, 20);
            txt_nombreCuenta.TabIndex = 31;
            txt_nombreCuenta.KeyPress += txtNombre_KeyPress;
            // 
            // txt_contraseña
            // 
            txt_contraseña.BackColor = Color.FromArgb(251, 203, 51);
            txt_contraseña.BorderStyle = BorderStyle.None;
            txt_contraseña.Font = new Font("Segoe UI", 9F);
            txt_contraseña.Location = new Point(82, 282);
            txt_contraseña.Margin = new Padding(3, 2, 3, 2);
            txt_contraseña.MaxLength = 30;
            txt_contraseña.Name = "txt_contraseña";
            txt_contraseña.Size = new Size(134, 20);
            txt_contraseña.TabIndex = 27;
            txt_contraseña.KeyPress += txtContraseña_KeyPress;
            // 
            // txt_usuario
            // 
            txt_usuario.BackColor = Color.FromArgb(251, 203, 51);
            txt_usuario.BorderStyle = BorderStyle.None;
            txt_usuario.Font = new Font("Segoe UI", 9F);
            txt_usuario.Location = new Point(82, 248);
            txt_usuario.Margin = new Padding(3, 2, 3, 2);
            txt_usuario.MaxLength = 20;
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(134, 20);
            txt_usuario.TabIndex = 26;
            txt_usuario.KeyPress += txt_usuario_KeyPress;
            // 
            // txt_correo
            // 
            txt_correo.BackColor = Color.FromArgb(251, 203, 51);
            txt_correo.BorderStyle = BorderStyle.None;
            txt_correo.Font = new Font("Segoe UI", 9F);
            txt_correo.Location = new Point(82, 206);
            txt_correo.Margin = new Padding(3, 2, 3, 2);
            txt_correo.MaxLength = 50;
            txt_correo.Name = "txt_correo";
            txt_correo.Size = new Size(134, 20);
            txt_correo.TabIndex = 25;
            txt_correo.KeyPress += txt_correo_KeyPress;
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
            label11.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label11.Location = new Point(3, 282);
            label11.Name = "label11";
            label11.Size = new Size(84, 19);
            label11.TabIndex = 22;
            label11.Text = "Contraseña";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label10.Location = new Point(7, 334);
            label10.Name = "label10";
            label10.Size = new Size(31, 19);
            label10.TabIndex = 21;
            label10.Text = "Rol";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label9.Location = new Point(7, 126);
            label9.Name = "label9";
            label9.Size = new Size(65, 19);
            label9.TabIndex = 20;
            label9.Text = "Nombre";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label7.Location = new Point(7, 166);
            label7.Name = "label7";
            label7.Size = new Size(66, 19);
            label7.TabIndex = 19;
            label7.Text = "Apellido";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label6.Location = new Point(7, 206);
            label6.Name = "label6";
            label6.Size = new Size(56, 19);
            label6.TabIndex = 18;
            label6.Text = "Correo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label5.Location = new Point(7, 248);
            label5.Name = "label5";
            label5.Size = new Size(60, 19);
            label5.TabIndex = 17;
            label5.Text = "Usuario";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(7, 90);
            label4.Name = "label4";
            label4.Size = new Size(23, 19);
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
            label8.Click += label8_Click;
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
            // Ventana_Principal_Administrador
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1282, 718);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Ventana_Principal_Administrador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ventana_Principal_Administrador";
            Load += FRM_PG5_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelContenedor.ResumeLayout(false);
            panelCatalogoCuentas.ResumeLayout(false);
            panelCatalogoCuentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoCuentas).EndInit();
            panelUsuario.ResumeLayout(false);
            panelUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_usuarios).EndInit();
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
        private Button btn_agregar;
        private Button btnGuardar;
        private Button btnInhabilitar;
        private Button btnEliminar;
        private Button btnModificar;
        private DataGridView dgv_usuarios;
        private ComboBox cmb_estado;
        private ComboBox cmb_parroquia;
        private ComboBox cmb_rol;
        private TextBox txt_id;
        private TextBox txt_apellido;
        private TextBox txt_nombreCuenta;
        private TextBox txt_contraseña;
        private TextBox txt_usuario;
        private TextBox txt_correo;
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
        private ComboBox cmbNivel2;
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
        private Button btn_usuario;
        private Button btn_catalago_cuenta;
        private Button btn_guardar;
        private Button btn_inhabilitar;
        private Button btn_habilitar;
        private Button btn_modificar;
        private Button btnGuardarCuenta;
        private Panel panelMensaje;
        private TextBox txt_buscar;
        private Label label2;
        private Label label20;
        private Label label3;
        private ComboBox cmbNivel1;
        private Button btnInhabilitarCuenta;
        private Button btnHabilitarCuenta;
        private ComboBox cmbEstadoCuenta;
        private Label label14;
        private Button btnModificarCuenta;
        private Label label21;
        private ComboBox cmbNivel3;
    }
}