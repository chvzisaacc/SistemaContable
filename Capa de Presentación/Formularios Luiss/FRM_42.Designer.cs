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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btnIngresos = new Button();
            panelIngresos = new Panel();
            panelGastos = new Panel();
            panelCajaChica = new Panel();
            panelBancos = new Panel();
            lblNoSeleccionado = new Label();
            pibImage = new PictureBox();
            btnCajaChica = new Button();
            btnBancos = new Button();
            btnGastos = new Button();
            panel6 = new Panel();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            cmbOrigen = new ComboBox();
            lblOrigen = new Label();
            lblFecha = new Label();
            lblNoReferencia = new Label();
            txtNoReferencia = new TextBox();
            dgvIngresos = new DataGridView();
            cNombreCuenta = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            btnGuardar = new Button();
            dtpFecha = new DateTimePicker();
            panel1.SuspendLayout();
            panelIngresos.SuspendLayout();
            panelGastos.SuspendLayout();
            panelCajaChica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pibImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panelGastos);
            panel1.Controls.Add(btnIngresos);
            panel1.Controls.Add(panelIngresos);
            panel1.Controls.Add(lblNoSeleccionado);
            panel1.Controls.Add(pibImage);
            panel1.Controls.Add(btnCajaChica);
            panel1.Controls.Add(btnBancos);
            panel1.Controls.Add(btnGastos);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(28, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(1920, 1080);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnIngresos
            // 
            btnIngresos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresos.Location = new Point(300, 303);
            btnIngresos.Name = "btnIngresos";
            btnIngresos.Size = new Size(146, 57);
            btnIngresos.TabIndex = 23;
            btnIngresos.Text = "Ingresos";
            btnIngresos.UseVisualStyleBackColor = true;
            btnIngresos.Click += btnIngresos_Click;
            // 
            // panelIngresos
            // 
            panelIngresos.Controls.Add(dtpFecha);
            panelIngresos.Controls.Add(btnGuardar);
            panelIngresos.Controls.Add(dgvIngresos);
            panelIngresos.Controls.Add(txtNoReferencia);
            panelIngresos.Controls.Add(lblNoReferencia);
            panelIngresos.Controls.Add(lblFecha);
            panelIngresos.Controls.Add(lblOrigen);
            panelIngresos.Controls.Add(cmbOrigen);
            panelIngresos.Enabled = false;
            panelIngresos.Location = new Point(13, 397);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(1831, 580);
            panelIngresos.TabIndex = 21;
            // 
            // panelGastos
            // 
            panelGastos.Controls.Add(panelCajaChica);
            panelGastos.Enabled = false;
            panelGastos.Location = new Point(58, 974);
            panelGastos.Name = "panelGastos";
            panelGastos.Size = new Size(1831, 580);
            panelGastos.TabIndex = 22;
            // 
            // panelCajaChica
            // 
            panelCajaChica.Controls.Add(panelBancos);
            panelCajaChica.Enabled = false;
            panelCajaChica.Location = new Point(26, 292);
            panelCajaChica.Name = "panelCajaChica";
            panelCajaChica.Size = new Size(1831, 580);
            panelCajaChica.TabIndex = 22;
            // 
            // panelBancos
            // 
            panelBancos.Enabled = false;
            panelBancos.Location = new Point(68, 364);
            panelBancos.Name = "panelBancos";
            panelBancos.Size = new Size(1831, 580);
            panelBancos.TabIndex = 22;
            // 
            // lblNoSeleccionado
            // 
            lblNoSeleccionado.AutoSize = true;
            lblNoSeleccionado.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoSeleccionado.Location = new Point(721, 673);
            lblNoSeleccionado.Name = "lblNoSeleccionado";
            lblNoSeleccionado.Size = new Size(473, 74);
            lblNoSeleccionado.TabIndex = 20;
            lblNoSeleccionado.Text = "¡Aún no seleccionas algo que hacer!\r\n   Comienza cuando lo prefieras";
            lblNoSeleccionado.Click += label3_Click;
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
            // btnCajaChica
            // 
            btnCajaChica.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCajaChica.Location = new Point(1000, 303);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Size = new Size(213, 57);
            btnCajaChica.TabIndex = 17;
            btnCajaChica.Text = "Caja Chica";
            btnCajaChica.UseVisualStyleBackColor = true;
            btnCajaChica.Click += btnCajaChica_Click;
            // 
            // btnBancos
            // 
            btnBancos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBancos.Location = new Point(1450, 303);
            btnBancos.Name = "btnBancos";
            btnBancos.Size = new Size(140, 57);
            btnBancos.TabIndex = 16;
            btnBancos.Text = "Bancos";
            btnBancos.UseVisualStyleBackColor = true;
            btnBancos.Click += button3_Click;
            // 
            // btnGastos
            // 
            btnGastos.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGastos.Location = new Point(650, 303);
            btnGastos.Name = "btnGastos";
            btnGastos.Size = new Size(140, 57);
            btnGastos.TabIndex = 15;
            btnGastos.Text = "Gastos";
            btnGastos.UseVisualStyleBackColor = true;
            btnGastos.Click += btnGastos_Click;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(43, 56, 143);
            panel6.Enabled = false;
            panel6.Location = new Point(0, 365);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1878, 18);
            panel6.TabIndex = 14;
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
            // cmbOrigen
            // 
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Location = new Point(194, 86);
            cmbOrigen.Margin = new Padding(3, 2, 3, 2);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(135, 38);
            cmbOrigen.TabIndex = 13;
            cmbOrigen.Text = "Seleccionar";
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrigen.Location = new Point(74, 86);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(92, 32);
            lblOrigen.TabIndex = 15;
            lblOrigen.Text = "Origen";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(74, 156);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(78, 32);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "Fecha";
            // 
            // lblNoReferencia
            // 
            lblNoReferencia.AutoSize = true;
            lblNoReferencia.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNoReferencia.Location = new Point(543, 89);
            lblNoReferencia.Name = "lblNoReferencia";
            lblNoReferencia.Size = new Size(182, 32);
            lblNoReferencia.TabIndex = 17;
            lblNoReferencia.Text = "No. Referencia";
            // 
            // txtNoReferencia
            // 
            txtNoReferencia.BackColor = Color.FromArgb(251, 203, 51);
            txtNoReferencia.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNoReferencia.Location = new Point(763, 83);
            txtNoReferencia.Name = "txtNoReferencia";
            txtNoReferencia.Size = new Size(177, 35);
            txtNoReferencia.TabIndex = 18;
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
            dgvIngresos.Location = new Point(74, 230);
            dgvIngresos.Name = "dgvIngresos";
            dgvIngresos.Size = new Size(1091, 254);
            dgvIngresos.TabIndex = 19;
            // 
            // cNombreCuenta
            // 
            cNombreCuenta.HeaderText = "Nombre/Cuenta";
            cNombreCuenta.Name = "cNombreCuenta";
            cNombreCuenta.Width = 350;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.Name = "cDetalle";
            cDetalle.Width = 350;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.Name = "cSaldo";
            cSaldo.Width = 350;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(43, 56, 143);
            btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(43, 56, 143);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = SystemColors.Control;
            btnGuardar.Location = new Point(1224, 509);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(142, 48);
            btnGuardar.TabIndex = 20;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dtpFecha
            // 
            dtpFecha.CalendarFont = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFecha.CalendarForeColor = SystemColors.ControlLightLight;
            dtpFecha.CalendarTitleForeColor = SystemColors.ControlLightLight;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(194, 164);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(135, 23);
            dtpFecha.TabIndex = 21;
            // 
            // FRM_42
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(1904, 1041);
            Controls.Add(panel1);
            Name = "FRM_42";
            Text = "FRM_42";
            Load += FRM_42_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelIngresos.ResumeLayout(false);
            panelIngresos.PerformLayout();
            panelGastos.ResumeLayout(false);
            panelCajaChica.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pibImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
       // private Button btnIngresos;
        private PictureBox pictureBox3;
        private Panel panel6;
        private Button btnCajaChica;
        private Button btnBancos;
        private Button btnGastos;
        private Label lblNoSeleccionado;
        private PictureBox pibImage;
        private Panel panelIngresos;
        private Panel panelBancos;
        private Panel panelGastos;
        private Panel panelCajaChica;
        private Button btnIngresos;
        private Label lblNoReferencia;
        private Label lblFecha;
        private Label lblOrigen;
        private ComboBox cmbOrigen;
        private TextBox txtNoReferencia;
        private DataGridView dgvIngresos;
        private DataGridViewTextBoxColumn cNombreCuenta;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private Button btnGuardar;
        private DateTimePicker dtpFecha;
    }
}