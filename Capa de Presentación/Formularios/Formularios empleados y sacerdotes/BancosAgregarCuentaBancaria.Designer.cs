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
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
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
            panel2.Location = new Point(14, 15);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(599, 355);
            panel2.TabIndex = 10;
            panel2.Paint += panel2_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(141, 200);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(83, 38);
            label1.TabIndex = 20;
            label1.Text = "Tipo:";
            // 
            // cmbCuenta
            // 
            cmbCuenta.BackColor = Color.FromArgb(251, 203, 51);
            cmbCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            cmbCuenta.FormattingEnabled = true;
            cmbCuenta.Location = new Point(244, 200);
            cmbCuenta.Margin = new Padding(1, 2, 1, 2);
            cmbCuenta.Name = "cmbCuenta";
            cmbCuenta.Size = new Size(270, 46);
            cmbCuenta.TabIndex = 19;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(244, 148);
            txtMonto.Margin = new Padding(4, 3, 4, 3);
            txtMonto.MaxLength = 16;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(270, 37);
            txtMonto.TabIndex = 18;
            txtMonto.Text = "Ingrese un Monto";
            txtMonto.Click += txtMonto_Click_1;
            txtMonto.TextChanged += txtMonto_TextChanged;
            txtMonto.Leave += txtMonto_Leave_1;
            // 
            // txtCuenta
            // 
            txtCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtCuenta.BorderStyle = BorderStyle.None;
            txtCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCuenta.ForeColor = Color.Black;
            txtCuenta.Location = new Point(244, 97);
            txtCuenta.Margin = new Padding(4, 3, 4, 3);
            txtCuenta.MaxLength = 40;
            txtCuenta.Name = "txtCuenta";
            txtCuenta.Size = new Size(270, 37);
            txtCuenta.TabIndex = 17;
            txtCuenta.Text = "Ingrese una cuenta";
            txtCuenta.Click += txtCuenta_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(111, 148);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(114, 38);
            label9.TabIndex = 15;
            label9.Text = "Monto:";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(244, 288);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(160, 27);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(186, 273);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(281, 63);
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
            textBox1.Location = new Point(244, 97);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(251, 37);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(109, 95);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(117, 38);
            label7.TabIndex = 10;
            label7.Text = "Cuenta:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(130, 23);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(375, 45);
            label6.TabIndex = 10;
            label6.Text = "Nueva cuenta bancaria";
            // 
            // BancosAgregarCuentaBancaria
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(626, 387);
            Controls.Add(panel2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
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
    }
}