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
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(txtMonto);
            panel2.Controls.Add(cmbDestino);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(cmbOrigen);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(518, 309);
            panel2.TabIndex = 10;
            panel2.Paint += panel2_Paint;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(155, 170);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(215, 31);
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
            cmbDestino.Location = new Point(86, 112);
            cmbDestino.Name = "cmbDestino";
            cmbDestino.Size = new Size(354, 39);
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
            textBox2.Location = new Point(190, 238);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(152, 27);
            textBox2.TabIndex = 14;
            textBox2.Text = "Guardar y cerrar";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(155, 227);
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
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(155, 170);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(201, 27);
            textBox1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(31, 170);
            label7.Name = "label7";
            label7.Size = new Size(93, 31);
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
            cmbOrigen.Location = new Point(86, 67);
            cmbOrigen.Name = "cmbOrigen";
            cmbOrigen.Size = new Size(354, 39);
            cmbOrigen.TabIndex = 11;
            cmbOrigen.Text = "            Origen";
            cmbOrigen.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(64, 15);
            label6.Name = "label6";
            label6.Size = new Size(401, 38);
            label6.TabIndex = 10;
            label6.Text = "Transfiera de cuenta a cuenta";
            // 
            // BancosTransferenciaEntreCuentas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(542, 333);
            Controls.Add(panel2);
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
    }
}