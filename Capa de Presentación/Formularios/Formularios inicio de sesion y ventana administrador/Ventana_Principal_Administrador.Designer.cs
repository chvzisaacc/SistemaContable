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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana_Principal_Administrador));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            cmbNivel2 = new ComboBox();
            btn_catalago_cuenta = new Button();
            cmbNivel3 = new ComboBox();
            btn_usuario = new Button();
            panelContenedor = new Panel();
            panelCatalogoCuentas = new Panel();
            txtBuscarCuenta = new TextBox();
            label17 = new Label();
            dgvCatalogoCuentas = new DataGridView();
            btnModificarCuenta = new Button();
            cmbEstadoCuenta = new ComboBox();
            label14 = new Label();
            btnInhabilitarCuenta = new Button();
            btnHabilitarCuenta = new Button();
            btnGuardarCuenta = new Button();
            btnNuevaCuenta = new Button();
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
            panelMensaje = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelContenedor.SuspendLayout();
            panelCatalogoCuentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoCuentas).BeginInit();
            panelUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_usuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(cmbNivel2);
            panel1.Controls.Add(btn_catalago_cuenta);
            panel1.Controls.Add(cmbNivel3);
            panel1.Controls.Add(btn_usuario);
            panel1.Controls.Add(panelContenedor);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(panelMensaje);
            panel1.Location = new Point(14, 15);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1571, 866);
            panel1.TabIndex = 8;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(10, 4);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(109, 96);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // cmbNivel2
            // 
            cmbNivel2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNivel2.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbNivel2.BackColor = Color.FromArgb(251, 203, 51);
            cmbNivel2.FlatStyle = FlatStyle.Flat;
            cmbNivel2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbNivel2.FormattingEnabled = true;
            cmbNivel2.IntegralHeight = false;
            cmbNivel2.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbNivel2.Location = new Point(79, 61);
            cmbNivel2.Margin = new Padding(4);
            cmbNivel2.MaxDropDownItems = 6;
            cmbNivel2.Name = "cmbNivel2";
            cmbNivel2.Size = new Size(10, 29);
            cmbNivel2.TabIndex = 54;
            // 
            // btn_catalago_cuenta
            // 
            btn_catalago_cuenta.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btn_catalago_cuenta.Location = new Point(360, 116);
            btn_catalago_cuenta.Margin = new Padding(4, 5, 4, 5);
            btn_catalago_cuenta.Name = "btn_catalago_cuenta";
            btn_catalago_cuenta.Size = new Size(394, 55);
            btn_catalago_cuenta.TabIndex = 19;
            btn_catalago_cuenta.Text = "Catálogo de Cuentas";
            btn_catalago_cuenta.UseVisualStyleBackColor = true;
            btn_catalago_cuenta.Click += btnCatalagoCuenta_Click;
            // 
            // cmbNivel3
            // 
            cmbNivel3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNivel3.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbNivel3.BackColor = Color.FromArgb(251, 203, 51);
            cmbNivel3.FlatStyle = FlatStyle.Flat;
            cmbNivel3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbNivel3.ForeColor = Color.Transparent;
            cmbNivel3.FormattingEnabled = true;
            cmbNivel3.IntegralHeight = false;
            cmbNivel3.Items.AddRange(new object[] { "Ingresos", "Egresos" });
            cmbNivel3.Location = new Point(79, 25);
            cmbNivel3.Margin = new Padding(4);
            cmbNivel3.MaxDropDownItems = 6;
            cmbNivel3.Name = "cmbNivel3";
            cmbNivel3.Size = new Size(10, 29);
            cmbNivel3.TabIndex = 53;
            // 
            // btn_usuario
            // 
            btn_usuario.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btn_usuario.Location = new Point(147, 116);
            btn_usuario.Margin = new Padding(4, 5, 4, 5);
            btn_usuario.Name = "btn_usuario";
            btn_usuario.Size = new Size(174, 55);
            btn_usuario.TabIndex = 18;
            btn_usuario.Text = "Usuario";
            btn_usuario.UseVisualStyleBackColor = true;
            btn_usuario.Click += btnUsuario_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(panelCatalogoCuentas);
            panelContenedor.Controls.Add(panelUsuario);
            panelContenedor.Location = new Point(-1, 185);
            panelContenedor.Margin = new Padding(4, 5, 4, 5);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1575, 679);
            panelContenedor.TabIndex = 17;
            // 
            // panelCatalogoCuentas
            // 
            panelCatalogoCuentas.BackColor = Color.White;
            panelCatalogoCuentas.Controls.Add(txtBuscarCuenta);
            panelCatalogoCuentas.Controls.Add(label17);
            panelCatalogoCuentas.Controls.Add(dgvCatalogoCuentas);
            panelCatalogoCuentas.Controls.Add(btnModificarCuenta);
            panelCatalogoCuentas.Controls.Add(cmbEstadoCuenta);
            panelCatalogoCuentas.Controls.Add(label14);
            panelCatalogoCuentas.Controls.Add(btnInhabilitarCuenta);
            panelCatalogoCuentas.Controls.Add(btnHabilitarCuenta);
            panelCatalogoCuentas.Controls.Add(btnGuardarCuenta);
            panelCatalogoCuentas.Controls.Add(btnNuevaCuenta);
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
            panelCatalogoCuentas.Dock = DockStyle.Fill;
            panelCatalogoCuentas.Location = new Point(0, 0);
            panelCatalogoCuentas.Margin = new Padding(4);
            panelCatalogoCuentas.Name = "panelCatalogoCuentas";
            panelCatalogoCuentas.Size = new Size(1575, 679);
            panelCatalogoCuentas.TabIndex = 20;
            panelCatalogoCuentas.Paint += panelCatalogoCuentas_Paint;
            // 
            // txtBuscarCuenta
            // 
            txtBuscarCuenta.Location = new Point(590, 115);
            txtBuscarCuenta.Margin = new Padding(4);
            txtBuscarCuenta.Name = "txtBuscarCuenta";
            txtBuscarCuenta.Size = new Size(335, 31);
            txtBuscarCuenta.TabIndex = 54;
            txtBuscarCuenta.TextChanged += txtBuscarCuenta_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(498, 115);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(72, 25);
            label17.TabIndex = 53;
            label17.Text = "Buscar: ";
            // 
            // dgvCatalogoCuentas
            // 
            dgvCatalogoCuentas.AllowUserToAddRows = false;
            dgvCatalogoCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCatalogoCuentas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvCatalogoCuentas.BackgroundColor = SystemColors.Window;
            dgvCatalogoCuentas.BorderStyle = BorderStyle.None;
            dgvCatalogoCuentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCatalogoCuentas.DefaultCellStyle = dataGridViewCellStyle1;
            dgvCatalogoCuentas.Location = new Point(9, 156);
            dgvCatalogoCuentas.Margin = new Padding(4);
            dgvCatalogoCuentas.Name = "dgvCatalogoCuentas";
            dgvCatalogoCuentas.ReadOnly = true;
            dgvCatalogoCuentas.RowHeadersWidth = 51;
            dgvCatalogoCuentas.RowTemplate.Height = 50;
            dgvCatalogoCuentas.ScrollBars = ScrollBars.Vertical;
            dgvCatalogoCuentas.Size = new Size(1546, 465);
            dgvCatalogoCuentas.TabIndex = 37;
            // 
            // btnModificarCuenta
            // 
            btnModificarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnModificarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificarCuenta.ForeColor = Color.White;
            btnModificarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnModificarCuenta.Location = new Point(630, 629);
            btnModificarCuenta.Margin = new Padding(4);
            btnModificarCuenta.Name = "btnModificarCuenta";
            btnModificarCuenta.Size = new Size(199, 46);
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
            cmbEstadoCuenta.Location = new Point(1214, 11);
            cmbEstadoCuenta.Margin = new Padding(4);
            cmbEstadoCuenta.Name = "cmbEstadoCuenta";
            cmbEstadoCuenta.Size = new Size(335, 29);
            cmbEstadoCuenta.TabIndex = 37;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label14.Location = new Point(1146, 19);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(61, 21);
            label14.TabIndex = 50;
            label14.Text = "Estado";
            // 
            // btnInhabilitarCuenta
            // 
            btnInhabilitarCuenta.BackColor = Color.FromArgb(43, 56, 143);
            btnInhabilitarCuenta.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInhabilitarCuenta.ForeColor = Color.White;
            btnInhabilitarCuenta.ImageAlign = ContentAlignment.TopCenter;
            btnInhabilitarCuenta.Location = new Point(421, 629);
            btnInhabilitarCuenta.Margin = new Padding(4);
            btnInhabilitarCuenta.Name = "btnInhabilitarCuenta";
            btnInhabilitarCuenta.Size = new Size(199, 46);
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
            btnHabilitarCuenta.Location = new Point(214, 629);
            btnHabilitarCuenta.Margin = new Padding(4);
            btnHabilitarCuenta.Name = "btnHabilitarCuenta";
            btnHabilitarCuenta.Size = new Size(199, 46);
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
            btnGuardarCuenta.Location = new Point(9, 629);
            btnGuardarCuenta.Margin = new Padding(4);
            btnGuardarCuenta.Name = "btnGuardarCuenta";
            btnGuardarCuenta.Size = new Size(199, 46);
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
            btnNuevaCuenta.Location = new Point(9, 6);
            btnNuevaCuenta.Margin = new Padding(4);
            btnNuevaCuenta.Name = "btnNuevaCuenta";
            btnNuevaCuenta.Size = new Size(161, 46);
            btnNuevaCuenta.TabIndex = 42;
            btnNuevaCuenta.Text = "Nueva cuenta";
            btnNuevaCuenta.UseVisualStyleBackColor = false;
            btnNuevaCuenta.UseWaitCursor = true;
            btnNuevaCuenta.Click += btnNuevaCuenta_Click_1;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.Location = new Point(1259, 683);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(199, 33);
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
            cmbNivel1.Location = new Point(759, 15);
            cmbNivel1.Margin = new Padding(4);
            cmbNivel1.MaxDropDownItems = 6;
            cmbNivel1.Name = "cmbNivel1";
            cmbNivel1.Size = new Size(335, 29);
            cmbNivel1.TabIndex = 33;
            cmbNivel1.SelectedIndexChanged += cmbCuenta_SelectedIndexChanged;
            // 
            // panel10
            // 
            panel10.Location = new Point(229, 6);
            panel10.Margin = new Padding(4);
            panel10.Name = "panel10";
            panel10.Size = new Size(0, 0);
            panel10.TabIndex = 7;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label19.Location = new Point(229, 19);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(65, 21);
            label19.TabIndex = 16;
            label19.Text = "Código";
            // 
            // txtDetalle
            // 
            txtDetalle.BackColor = Color.FromArgb(251, 203, 51);
            txtDetalle.BorderStyle = BorderStyle.None;
            txtDetalle.Font = new Font("Segoe UI", 8F);
            txtDetalle.Location = new Point(759, 74);
            txtDetalle.Margin = new Padding(4);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(791, 22);
            txtDetalle.TabIndex = 36;
            txtDetalle.KeyPress += txtDetalle_KeyPress;
            // 
            // txtIdCuenta
            // 
            txtIdCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtIdCuenta.BorderStyle = BorderStyle.None;
            txtIdCuenta.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdCuenta.Location = new Point(309, 19);
            txtIdCuenta.Margin = new Padding(4);
            txtIdCuenta.MaxLength = 5;
            txtIdCuenta.Name = "txtIdCuenta";
            txtIdCuenta.Size = new Size(271, 22);
            txtIdCuenta.TabIndex = 31;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label18.Location = new Point(668, 76);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(65, 21);
            label18.TabIndex = 17;
            label18.Text = "Detalle";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label15.Location = new Point(229, 74);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(73, 21);
            label15.TabIndex = 20;
            label15.Text = "Nombre";
            // 
            // txtNombreCuenta
            // 
            txtNombreCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtNombreCuenta.BorderStyle = BorderStyle.None;
            txtNombreCuenta.Font = new Font("Segoe UI", 8F);
            txtNombreCuenta.Location = new Point(310, 75);
            txtNombreCuenta.Margin = new Padding(4);
            txtNombreCuenta.MaxLength = 25;
            txtNombreCuenta.Name = "txtNombreCuenta";
            txtNombreCuenta.Size = new Size(271, 22);
            txtNombreCuenta.TabIndex = 32;
            txtNombreCuenta.KeyPress += txtNombreCuenta_KeyPress;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label16.Location = new Point(630, 16);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(102, 21);
            label16.TabIndex = 19;
            label16.Text = "Típo Cuenta";
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
            panelUsuario.Margin = new Padding(4);
            panelUsuario.Name = "panelUsuario";
            panelUsuario.Size = new Size(1575, 679);
            panelUsuario.TabIndex = 19;
            panelUsuario.Paint += panelUsuario_Paint;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label20.Location = new Point(1146, 131);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(61, 21);
            label20.TabIndex = 50;
            label20.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label3.Location = new Point(1131, 80);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(85, 21);
            label3.TabIndex = 49;
            label3.Text = "Parroquia";
            // 
            // txt_buscar
            // 
            txt_buscar.Location = new Point(501, 156);
            txt_buscar.Margin = new Padding(4);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(335, 31);
            txt_buscar.TabIndex = 48;
            txt_buscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(409, 156);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(72, 25);
            label2.TabIndex = 47;
            label2.Text = "Buscar: ";
            // 
            // btn_guardar
            // 
            btn_guardar.BackColor = Color.FromArgb(43, 56, 143);
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_guardar.ForeColor = Color.White;
            btn_guardar.ImageAlign = ContentAlignment.TopCenter;
            btn_guardar.Location = new Point(41, 96);
            btn_guardar.Margin = new Padding(4);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(161, 46);
            btn_guardar.TabIndex = 46;
            btn_guardar.Text = "Guardar";
            btn_guardar.UseVisualStyleBackColor = false;
            btn_guardar.UseWaitCursor = true;
            btn_guardar.Click += btnGuardarUsuario_Click;
            // 
            // btn_inhabilitar
            // 
            btn_inhabilitar.BackColor = Color.FromArgb(43, 56, 143);
            btn_inhabilitar.FlatStyle = FlatStyle.Flat;
            btn_inhabilitar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_inhabilitar.ForeColor = Color.White;
            btn_inhabilitar.ImageAlign = ContentAlignment.TopCenter;
            btn_inhabilitar.Location = new Point(1409, 510);
            btn_inhabilitar.Margin = new Padding(4);
            btn_inhabilitar.Name = "btn_inhabilitar";
            btn_inhabilitar.Size = new Size(129, 46);
            btn_inhabilitar.TabIndex = 45;
            btn_inhabilitar.Text = "Inhabilitar";
            btn_inhabilitar.UseVisualStyleBackColor = false;
            btn_inhabilitar.UseWaitCursor = true;
            btn_inhabilitar.Click += btnInhabilitarUsuario_Click;
            // 
            // btn_habilitar
            // 
            btn_habilitar.BackColor = Color.FromArgb(43, 56, 143);
            btn_habilitar.FlatStyle = FlatStyle.Flat;
            btn_habilitar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_habilitar.ForeColor = Color.White;
            btn_habilitar.ImageAlign = ContentAlignment.TopCenter;
            btn_habilitar.Location = new Point(1409, 394);
            btn_habilitar.Margin = new Padding(4);
            btn_habilitar.Name = "btn_habilitar";
            btn_habilitar.Size = new Size(129, 46);
            btn_habilitar.TabIndex = 44;
            btn_habilitar.Text = "Habilitar";
            btn_habilitar.UseVisualStyleBackColor = false;
            btn_habilitar.UseWaitCursor = true;
            btn_habilitar.Click += btnHabilitar_Click;
            // 
            // btn_modificar
            // 
            btn_modificar.BackColor = Color.FromArgb(43, 56, 143);
            btn_modificar.FlatStyle = FlatStyle.Flat;
            btn_modificar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_modificar.ForeColor = Color.White;
            btn_modificar.ImageAlign = ContentAlignment.TopCenter;
            btn_modificar.Location = new Point(1409, 279);
            btn_modificar.Margin = new Padding(4);
            btn_modificar.Name = "btn_modificar";
            btn_modificar.Size = new Size(129, 46);
            btn_modificar.TabIndex = 43;
            btn_modificar.Text = "Modificar";
            btn_modificar.UseVisualStyleBackColor = false;
            btn_modificar.UseWaitCursor = true;
            btn_modificar.Click += btnModificarCuentaUsuario_Click;
            // 
            // btn_agregar
            // 
            btn_agregar.BackColor = Color.FromArgb(43, 56, 143);
            btn_agregar.FlatStyle = FlatStyle.Flat;
            btn_agregar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_agregar.ForeColor = Color.White;
            btn_agregar.ImageAlign = ContentAlignment.TopCenter;
            btn_agregar.Location = new Point(41, 31);
            btn_agregar.Margin = new Padding(4);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(161, 46);
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
            btnGuardar.Location = new Point(1299, 775);
            btnGuardar.Margin = new Padding(4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(199, 46);
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
            btnInhabilitar.Location = new Point(1040, 775);
            btnInhabilitar.Margin = new Padding(4);
            btnInhabilitar.Name = "btnInhabilitar";
            btnInhabilitar.Size = new Size(199, 46);
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
            btnEliminar.Location = new Point(766, 775);
            btnEliminar.Margin = new Padding(4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(199, 46);
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
            btnModificar.Location = new Point(474, 775);
            btnModificar.Margin = new Padding(4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(199, 46);
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
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgv_usuarios.DefaultCellStyle = dataGridViewCellStyle2;
            dgv_usuarios.Location = new Point(6, 206);
            dgv_usuarios.Margin = new Padding(4);
            dgv_usuarios.Name = "dgv_usuarios";
            dgv_usuarios.RowHeadersVisible = false;
            dgv_usuarios.RowHeadersWidth = 51;
            dgv_usuarios.ScrollBars = ScrollBars.Vertical;
            dgv_usuarios.Size = new Size(1389, 475);
            dgv_usuarios.TabIndex = 37;
            // 
            // cmb_estado
            // 
            cmb_estado.BackColor = Color.FromArgb(251, 203, 51);
            cmb_estado.FlatStyle = FlatStyle.Flat;
            cmb_estado.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmb_estado.FormattingEnabled = true;
            cmb_estado.Location = new Point(1221, 121);
            cmb_estado.Margin = new Padding(4);
            cmb_estado.Name = "cmb_estado";
            cmb_estado.Size = new Size(168, 29);
            cmb_estado.TabIndex = 39;
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
            cmb_parroquia.Location = new Point(1221, 75);
            cmb_parroquia.Margin = new Padding(4);
            cmb_parroquia.MaxDropDownItems = 6;
            cmb_parroquia.Name = "cmb_parroquia";
            cmb_parroquia.Size = new Size(168, 29);
            cmb_parroquia.TabIndex = 38;
            // 
            // cmb_rol
            // 
            cmb_rol.BackColor = Color.FromArgb(251, 203, 51);
            cmb_rol.FlatStyle = FlatStyle.Flat;
            cmb_rol.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            cmb_rol.FormattingEnabled = true;
            cmb_rol.Location = new Point(1221, 26);
            cmb_rol.Margin = new Padding(4);
            cmb_rol.Name = "cmb_rol";
            cmb_rol.Size = new Size(168, 29);
            cmb_rol.TabIndex = 37;
            // 
            // txt_id
            // 
            txt_id.BackColor = Color.FromArgb(251, 203, 51);
            txt_id.BorderStyle = BorderStyle.None;
            txt_id.Font = new Font("Segoe UI", 9F);
            txt_id.Location = new Point(309, 39);
            txt_id.Margin = new Padding(4);
            txt_id.Name = "txt_id";
            txt_id.Size = new Size(169, 24);
            txt_id.TabIndex = 31;
            // 
            // txt_apellido
            // 
            txt_apellido.BackColor = Color.FromArgb(251, 203, 51);
            txt_apellido.BorderStyle = BorderStyle.None;
            txt_apellido.Font = new Font("Segoe UI", 9F);
            txt_apellido.Location = new Point(581, 39);
            txt_apellido.Margin = new Padding(4);
            txt_apellido.MaxLength = 25;
            txt_apellido.Name = "txt_apellido";
            txt_apellido.Size = new Size(169, 24);
            txt_apellido.TabIndex = 33;
            txt_apellido.KeyPress += txt_apellido_KeyPress;
            // 
            // txt_nombreCuenta
            // 
            txt_nombreCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txt_nombreCuenta.BorderStyle = BorderStyle.None;
            txt_nombreCuenta.Font = new Font("Segoe UI", 9F);
            txt_nombreCuenta.Location = new Point(309, 91);
            txt_nombreCuenta.Margin = new Padding(4);
            txt_nombreCuenta.MaxLength = 25;
            txt_nombreCuenta.Name = "txt_nombreCuenta";
            txt_nombreCuenta.Size = new Size(169, 24);
            txt_nombreCuenta.TabIndex = 32;
            txt_nombreCuenta.KeyPress += txtNombre_KeyPress;
            // 
            // txt_contraseña
            // 
            txt_contraseña.BackColor = Color.FromArgb(251, 203, 51);
            txt_contraseña.BorderStyle = BorderStyle.None;
            txt_contraseña.Font = new Font("Segoe UI", 9F);
            txt_contraseña.Location = new Point(926, 90);
            txt_contraseña.Margin = new Padding(4);
            txt_contraseña.MaxLength = 30;
            txt_contraseña.Name = "txt_contraseña";
            txt_contraseña.Size = new Size(169, 24);
            txt_contraseña.TabIndex = 36;
            txt_contraseña.KeyPress += txtContraseña_KeyPress;
            // 
            // txt_usuario
            // 
            txt_usuario.BackColor = Color.FromArgb(251, 203, 51);
            txt_usuario.BorderStyle = BorderStyle.None;
            txt_usuario.Font = new Font("Segoe UI", 9F);
            txt_usuario.Location = new Point(926, 34);
            txt_usuario.Margin = new Padding(4);
            txt_usuario.MaxLength = 20;
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(169, 24);
            txt_usuario.TabIndex = 35;
            txt_usuario.KeyPress += txt_usuario_KeyPress;
            // 
            // txt_correo
            // 
            txt_correo.BackColor = Color.FromArgb(251, 203, 51);
            txt_correo.BorderStyle = BorderStyle.None;
            txt_correo.Font = new Font("Segoe UI", 9F);
            txt_correo.Location = new Point(581, 91);
            txt_correo.Margin = new Padding(4);
            txt_correo.MaxLength = 80;
            txt_correo.Name = "txt_correo";
            txt_correo.Size = new Size(200, 24);
            txt_correo.TabIndex = 34;
            txt_correo.KeyPress += txt_correo_KeyPress;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10.8F);
            label13.Location = new Point(26, 655);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(105, 30);
            label13.TabIndex = 24;
            label13.Text = "Parroquia";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10.8F);
            label12.Location = new Point(26, 705);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(77, 30);
            label12.TabIndex = 23;
            label12.Text = "Estado";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label11.Location = new Point(821, 96);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(96, 21);
            label11.TabIndex = 22;
            label11.Text = "Contraseña";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label10.Location = new Point(1171, 31);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(35, 21);
            label10.TabIndex = 21;
            label10.Text = "Rol";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label9.Location = new Point(229, 95);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(73, 21);
            label9.TabIndex = 20;
            label9.Text = "Nombre";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label7.Location = new Point(501, 40);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(75, 21);
            label7.TabIndex = 19;
            label7.Text = "Apellido";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label6.Location = new Point(511, 96);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(61, 21);
            label6.TabIndex = 18;
            label6.Text = "Correo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            label5.Location = new Point(850, 35);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(69, 21);
            label5.TabIndex = 17;
            label5.Text = "Usuario";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(270, 40);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(27, 21);
            label4.TabIndex = 16;
            label4.Text = "ID";
            // 
            // panel9
            // 
            panel9.Location = new Point(219, 240);
            panel9.Margin = new Padding(4);
            panel9.Name = "panel9";
            panel9.Size = new Size(0, 0);
            panel9.TabIndex = 7;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(1324, 28);
            pictureBox3.Margin = new Padding(4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(96, 84);
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
            label8.Location = new Point(1469, 56);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(44, 30);
            label8.TabIndex = 14;
            label8.Text = "AD";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(1441, 24);
            pictureBox4.Margin = new Padding(4);
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
            panel2.Location = new Point(515, 180);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1057, 40);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(43, 56, 143);
            panel3.Enabled = false;
            panel3.Location = new Point(0, 180);
            panel3.Margin = new Padding(4);
            panel3.Name = "panel3";
            panel3.Size = new Size(1056, 40);
            panel3.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.Location = new Point(219, 240);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(147, 28);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(257, 45);
            label1.TabIndex = 1;
            label1.Text = "Administrador";
            // 
            // panelMensaje
            // 
            panelMensaje.BackgroundImage = (Image)resources.GetObject("panelMensaje.BackgroundImage");
            panelMensaje.BackgroundImageLayout = ImageLayout.Stretch;
            panelMensaje.Location = new Point(400, 224);
            panelMensaje.Margin = new Padding(4);
            panelMensaje.Name = "panelMensaje";
            panelMensaje.Size = new Size(774, 425);
            panelMensaje.TabIndex = 22;
            // 
            // Ventana_Principal_Administrador
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1601, 896);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Ventana_Principal_Administrador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ventana_Principal_Administrador";
            Load += FRM_PG5_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelContenedor.ResumeLayout(false);
            panelCatalogoCuentas.ResumeLayout(false);
            panelCatalogoCuentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogoCuentas).EndInit();
            panelUsuario.ResumeLayout(false);
            panelUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_usuarios).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
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
        private Button btnNuevaCuenta;
        private Button button1;
        private DataGridView dgvCatalogoCuentas;
        private TextBox txtIdCuenta;
        private TextBox txtNombreCuenta;
        private TextBox txtDetalle;
        private Label label15;
        private Label label16;
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
        private ComboBox cmbNivel2;
        private ComboBox cmbNivel3;
        private TextBox txtBuscarCuenta;
        private Label label17;
        private Label label13;
    }
}