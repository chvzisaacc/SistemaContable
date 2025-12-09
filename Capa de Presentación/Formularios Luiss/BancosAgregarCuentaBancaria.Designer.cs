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
            panel2.Controls.Add(txtMonto);
            panel2.Controls.Add(txtCuenta);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(10, 9);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(419, 182);
            panel2.TabIndex = 10;
            panel2.Paint += panel2_Paint;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(171, 89);
            txtMonto.Margin = new Padding(3, 2, 3, 2);
            txtMonto.MaxLength = 8;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(189, 25);
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
            txtCuenta.Location = new Point(171, 58);
            txtCuenta.Margin = new Padding(3, 2, 3, 2);
            txtCuenta.Name = "txtCuenta";
            txtCuenta.Size = new Size(189, 25);
            txtCuenta.TabIndex = 17;
            txtCuenta.Text = "Ingrese una cuenta";
            txtCuenta.Click += txtCuenta_Click;
            txtCuenta.TextChanged += txtCuenta_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(78, 89);
            label9.Name = "label9";
            label9.Size = new Size(78, 25);
            label9.TabIndex = 15;
            label9.Text = "Monto:";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(189, 144);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(112, 18);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(148, 134);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(198, 38);
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
            textBox1.Location = new Point(171, 58);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(176, 25);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(76, 57);
            label7.Name = "label7";
            label7.Size = new Size(80, 25);
            label7.TabIndex = 10;
            label7.Text = "Cuenta:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(91, 14);
            label6.Name = "label6";
            label6.Size = new Size(250, 30);
            label6.TabIndex = 10;
            label6.Text = "Nueva cuenta bancaria";
            // 
            // BancosAgregarCuentaBancaria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(438, 198);
            Controls.Add(panel2);
            Margin = new Padding(3, 2, 3, 2);
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
    }
}