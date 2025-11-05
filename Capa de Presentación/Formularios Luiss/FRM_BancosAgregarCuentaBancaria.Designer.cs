namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_BancosAgregarCuentaBancaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_BancosAgregarCuentaBancaria));
            panel2 = new Panel();
            checkBox1 = new CheckBox();
            txtTasaInteres = new TextBox();
            txtCuenta = new TextBox();
            textBox3 = new TextBox();
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
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(txtTasaInteres);
            panel2.Controls.Add(txtCuenta);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(10, 9);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(453, 232);
            panel2.TabIndex = 10;
            panel2.Paint += panel2_Paint;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox1.Location = new Point(274, 104);
            checkBox1.Margin = new Padding(3, 2, 3, 2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(108, 29);
            checkBox1.TabIndex = 20;
            checkBox1.Text = "Habilitar";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // txtTasaInteres
            // 
            txtTasaInteres.BackColor = Color.FromArgb(251, 203, 51);
            txtTasaInteres.BorderStyle = BorderStyle.None;
            txtTasaInteres.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTasaInteres.ForeColor = Color.Black;
            txtTasaInteres.Location = new Point(171, 106);
            txtTasaInteres.Margin = new Padding(3, 2, 3, 2);
            txtTasaInteres.Name = "txtTasaInteres";
            txtTasaInteres.Size = new Size(77, 25);
            txtTasaInteres.TabIndex = 19;
            txtTasaInteres.Text = "   1.00%";
            // 
            // txtCuenta
            // 
            txtCuenta.BackColor = Color.FromArgb(251, 203, 51);
            txtCuenta.BorderStyle = BorderStyle.None;
            txtCuenta.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCuenta.ForeColor = Color.Black;
            txtCuenta.Location = new Point(171, 69);
            txtCuenta.Margin = new Padding(3, 2, 3, 2);
            txtCuenta.Name = "txtCuenta";
            txtCuenta.Size = new Size(189, 25);
            txtCuenta.TabIndex = 17;
            txtCuenta.Text = "Ingrese una cuenta";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(251, 203, 51);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(171, 106);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(77, 22);
            textBox3.TabIndex = 16;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(5, 106);
            label9.Name = "label9";
            label9.Size = new Size(146, 25);
            label9.TabIndex = 15;
            label9.Text = "Tasa de interés:";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Enabled = false;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(171, 168);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(144, 22);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(143, 161);
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
            textBox1.Location = new Point(171, 69);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(176, 25);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(76, 67);
            label7.Name = "label7";
            label7.Size = new Size(80, 25);
            label7.TabIndex = 10;
            label7.Text = "Cuenta:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(94, 25);
            label6.Name = "label6";
            label6.Size = new Size(250, 30);
            label6.TabIndex = 10;
            label6.Text = "Nueva cuenta bancaria";
            // 
            // FRM_BancosAgregarCuentaBancaria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(474, 250);
            Controls.Add(panel2);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FRM_BancosAgregarCuentaBancaria";
            Text = "FRM_BancosAgregarCuentaBancaria";
            Load += FRM_BancosAgregarCuentaBancaria_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private CheckBox checkBox1;
        private TextBox txtTasaInteres;
        private TextBox txtCuenta;
        private TextBox textBox3;
        private Label label9;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private Label label7;
        private Label label6;
    }
}