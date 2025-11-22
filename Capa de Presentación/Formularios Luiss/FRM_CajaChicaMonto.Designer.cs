namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_CajaChicaMonto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_CajaChicaMonto));
            txtMonto = new TextBox();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            label7 = new Label();
            panel1 = new Panel();
            Btnguardar = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(196, 140);
            txtMonto.Margin = new Padding(4, 3, 4, 3);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(256, 37);
            txtMonto.TabIndex = 26;
            txtMonto.Text = "Ingrese monto";
            txtMonto.TextChanged += textBox4_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(206, 260);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(214, 32);
            textBox2.TabIndex = 25;
            textBox2.Text = "Guardar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(156, 240);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(317, 83);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 24;
            pictureBox2.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(196, 140);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(256, 37);
            textBox1.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(47, 140);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(114, 38);
            label7.TabIndex = 20;
            label7.Text = "Monto:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(Btnguardar);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtMonto);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(pictureBox2);
            panel1.Location = new Point(17, 20);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(643, 377);
            panel1.TabIndex = 27;
            // 
            // Btnguardar
            // 
            Btnguardar.BackColor = Color.FromArgb(43, 56, 143);
            Btnguardar.FlatStyle = FlatStyle.Flat;
            Btnguardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btnguardar.ForeColor = SystemColors.ButtonHighlight;
            Btnguardar.Location = new Point(116, 223);
            Btnguardar.Margin = new Padding(4, 5, 4, 5);
            Btnguardar.Name = "Btnguardar";
            Btnguardar.Size = new Size(374, 100);
            Btnguardar.TabIndex = 27;
            Btnguardar.Text = "Guardar y cerrar";
            Btnguardar.UseVisualStyleBackColor = false;
            Btnguardar.Click += button1_Click;
            // 
            // FRM_CajaChicaMonto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(677, 417);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FRM_CajaChicaMonto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FRM_CajaChicaMonto";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtMonto;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private Label label7;
        private Panel panel1;
        private Button Btnguardar;
    }
}