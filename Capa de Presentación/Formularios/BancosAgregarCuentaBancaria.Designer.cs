namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class BancosAgregarCuentaBancaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BancosAgregarCuentaBancaria));
            panel2 = new Panel();
            label1 = new Label();
            cmbCuenta = new ComboBox();
            txtMonto = new TextBox();
            txtCuenta = new TextBox();
            label9 = new Label();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label2 = new Label();
            cmbCuentaCatalogo = new ComboBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(cmbCuentaCatalogo);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(cmbCuenta);
            panel2.Controls.Add(txtMonto);
            panel2.Controls.Add(txtCuenta);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(11, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(479, 365);
            panel2.TabIndex = 10;
            panel2.Paint += panel2_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(114, 160);
            label1.Name = "label1";
            label1.Size = new Size(68, 31);
            label1.TabIndex = 20;
            label1.Text = "Tipo:";
            // 
            // cmbCuenta
            // 
            cmbCuenta.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            cmbCuenta.FormattingEnabled = true;
            cmbCuenta.Location = new Point(195, 160);
            cmbCuenta.Margin = new Padding(2, 2, 2, 2);
            cmbCuenta.Name = "cmbCuenta";
            cmbCuenta.Size = new Size(217, 39);
            cmbCuenta.TabIndex = 19;
            cmbCuenta.SelectedIndexChanged += cmbCuenta_SelectedIndexChanged;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(195, 119);
            txtMonto.MaxLength = 8;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(216, 31);
            txtMonto.TabIndex = 18;
            txtMonto.Text = "Ingrese un Monto";
            txtMonto.Click += txtMonto_Click_1;
            txtMonto.Leave += txtMonto_Leave_1;
            // 
            // txtCuenta
            // 
            txtCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtCuenta.BorderStyle = BorderStyle.None;
            txtCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCuenta.ForeColor = Color.Black;
            txtCuenta.Location = new Point(195, 77);
            txtCuenta.MaxLength = 40;
            txtCuenta.Name = "txtCuenta";
            txtCuenta.Size = new Size(216, 31);
            txtCuenta.TabIndex = 17;
            txtCuenta.Text = "Ingrese una cuenta";
            txtCuenta.Click += txtCuenta_Click;
            txtCuenta.TextChanged += txtCuenta_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(89, 119);
            label9.Name = "label9";
            label9.Size = new Size(93, 31);
            label9.TabIndex = 15;
            label9.Text = "Monto:";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(195, 309);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(128, 23);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(148, 296);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(226, 51);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 13;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(195, 77);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(201, 31);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(87, 76);
            label7.Name = "label7";
            label7.Size = new Size(95, 31);
            label7.TabIndex = 10;
            label7.Text = "Cuenta:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(104, 19);
            label6.Name = "label6";
            label6.Size = new Size(316, 38);
            label6.TabIndex = 10;
            label6.Text = "Nueva cuenta bancaria";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 218);
            label2.Name = "label2";
            label2.Size = new Size(229, 31);
            label2.TabIndex = 22;
            label2.Text = "Cuenta en Catalogo:";
            // 
            // cmbCuentaCatalogo
            // 
            cmbCuentaCatalogo.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuentaCatalogo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            cmbCuentaCatalogo.FormattingEnabled = true;
            cmbCuentaCatalogo.Location = new Point(240, 215);
            cmbCuentaCatalogo.Margin = new Padding(2);
            cmbCuentaCatalogo.Name = "cmbCuentaCatalogo";
            cmbCuentaCatalogo.Size = new Size(217, 39);
            cmbCuentaCatalogo.TabIndex = 21;
            // 
            // BancosAgregarCuentaBancaria
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(501, 389);
            Controls.Add(panel2);
            MaximizeBox = false;
            Name = "BancosAgregarCuentaBancaria";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agregar_Cuenta_Bancaria";
            Load += FRM_BancosAgregarCuentaBancaria_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private TextBox txtCuenta;
        private Label label9;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private Label label7;
        private Label label6;
        private TextBox txtMonto;
        private Label label1;
        private ComboBox cmbCuenta;
        private Label label2;
        private ComboBox cmbCuentaCatalogo;
    }
}