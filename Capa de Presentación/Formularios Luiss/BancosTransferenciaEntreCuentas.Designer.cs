namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class BancosTransferenciaEntreCuentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BancosTransferenciaEntreCuentas));
            panel2 = new Panel();
            txtMonto = new TextBox();
            cmbDestino = new ComboBox();
            textBox2 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            label7 = new Label();
            cmbOrigen = new ComboBox();
            label6 = new Label();
            label1 = new Label();
            label2 = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txtMonto);
            panel2.Controls.Add(cmbDestino);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(cmbOrigen);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(10, 9);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(419, 181);
            panel2.TabIndex = 10;
            panel2.Paint += panel2_Paint;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(161, 114);
            txtMonto.Margin = new Padding(3, 2, 3, 2);
            txtMonto.MaxLength = 9;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(216, 25);
            txtMonto.TabIndex = 18;
            txtMonto.TextChanged += txtMonto_TextChanged;
            // 
            // cmbDestino
            // 
            cmbDestino.BackColor = Color.FromArgb(251, 203, 51);
            cmbDestino.FlatStyle = FlatStyle.Flat;
            cmbDestino.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbDestino.FormattingEnabled = true;
            cmbDestino.Items.AddRange(new object[] { "Cuenta Ahorro", "Cuenta Cheque" });
            cmbDestino.Location = new Point(161, 75);
            cmbDestino.Margin = new Padding(3, 2, 3, 2);
            cmbDestino.Name = "cmbDestino";
            cmbDestino.Size = new Size(226, 33);
            cmbDestino.TabIndex = 15;
            cmbDestino.Text = "            Destino";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Enabled = false;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(170, 151);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(133, 22);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(139, 143);
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
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(161, 115);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(176, 22);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(76, 113);
            label7.Name = "label7";
            label7.Size = new Size(78, 25);
            label7.TabIndex = 10;
            label7.Text = "Monto:";
            // 
            // cmbOrigen
            // 
            cmbOrigen.BackColor = Color.FromArgb(251, 203, 51);
            cmbOrigen.FlatStyle = FlatStyle.Flat;
            cmbOrigen.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbOrigen.FormattingEnabled = true;
            cmbOrigen.Items.AddRange(new object[] { "Cuenta Cheque", "Cuenta Ahorro" });
            cmbOrigen.Location = new Point(161, 38);
            cmbOrigen.Margin = new Padding(3, 2, 3, 2);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(226, 33);
            cmbOrigen.TabIndex = 11;
            cmbOrigen.Text = "            Origen";
            cmbOrigen.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(60, 4);
            label6.Name = "label6";
            label6.Size = new Size(317, 30);
            label6.TabIndex = 10;
            label6.Text = "Transfiera de cuenta a cuenta";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(76, 46);
            label1.Name = "label1";
            label1.Size = new Size(78, 25);
            label1.TabIndex = 19;
            label1.Text = "Origen:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(76, 78);
            label2.Name = "label2";
            label2.Size = new Size(85, 25);
            label2.TabIndex = 20;
            label2.Text = "Destino:";
            // 
            // BancosTransferenciaEntreCuentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(438, 198);
            Controls.Add(panel2);
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "BancosTransferenciaEntreCuentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BancosTransferencia_Entre_Cuentas";
            Load += FRM_BancosTransferenciaEntreCuentas_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private TextBox txtMonto;
        private ComboBox cmbDestino;
        private TextBox textBox2;
        private PictureBox pictureBox2;
        private TextBox textBox1;
        private Label label7;
        private ComboBox cmbOrigen;
        private Label label6;
        private Label label2;
        private Label label1;
    }
}