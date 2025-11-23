namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_BancosRetirarDinero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_BancosRetirarDinero));
            panel2 = new Panel();
            txtMonto = new TextBox();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            label7 = new Label();
            comboBox1 = new ComboBox();
            label6 = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(txtMonto);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(10, 9);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(454, 232);
            panel2.TabIndex = 10;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(137, 108);
            txtMonto.Margin = new Padding(3, 2, 3, 2);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(202, 25);
            txtMonto.TabIndex = 18;
            txtMonto.Text = "Ingrese monto";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(160, 164);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(144, 22);
            textBox2.TabIndex = 14;
            textBox2.Text = " Enviar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(137, 157);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(197, 38);
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
            textBox1.Location = new Point(160, 108);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(176, 25);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(43, 108);
            label7.Name = "label7";
            label7.Size = new Size(78, 25);
            label7.TabIndex = 10;
            label7.Text = "Monto:";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(251, 203, 51);
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Cuenta Ahorro", "Cuenta Cheque" });
            comboBox1.Location = new Point(137, 66);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(201, 33);
            comboBox1.TabIndex = 11;
            comboBox1.Text = "            Cuentas";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(130, 21);
            label6.Name = "label6";
            label6.Size = new Size(201, 30);
            label6.TabIndex = 10;
            label6.Text = "Enviar a caja chica";
            // 
            // FRM_BancosRetirarDinero
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(475, 250);
            Controls.Add(panel2);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FRM_BancosRetirarDinero";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRM_BancosRetirarDinero";
            Load += FRM_BancosRetirarDinero_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private TextBox txtMonto;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private Label label7;
        private ComboBox comboBox1;
        private Label label6;
    }
}