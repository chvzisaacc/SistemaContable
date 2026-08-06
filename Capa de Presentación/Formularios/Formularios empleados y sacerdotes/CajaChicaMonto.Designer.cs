namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class CajaChicaMonto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CajaChicaMonto));
            txtMonto = new TextBox();
            textBox1 = new TextBox();
            label7 = new Label();
            panel1 = new Panel();
            lblTitulo = new Label();
            Btnguardar = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(251, 203, 51);
            txtMonto.BorderStyle = BorderStyle.None;
            txtMonto.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMonto.ForeColor = Color.Black;
            txtMonto.Location = new Point(230, 80);
            txtMonto.Margin = new Padding(4, 3, 4, 3);
            txtMonto.MaxLength = 16;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(256, 37);
            txtMonto.TabIndex = 26;
            txtMonto.Text = "Ingrese monto";
            txtMonto.Click += txtMonto_Click;
            txtMonto.TextChanged += txtMonto_TextChanged;
            txtMonto.Leave += txtMonto_Leave;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(230, 82);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(256, 37);
            textBox1.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(111, 80);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(114, 38);
            label7.TabIndex = 20;
            label7.Text = "Monto:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(lblTitulo);
            panel1.Controls.Add(Btnguardar);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtMonto);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(17, 20);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(596, 297);
            panel1.TabIndex = 27;
            panel1.Paint += panel1_Paint;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(194, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(18, 25);
            lblTitulo.TabIndex = 28;
            lblTitulo.Text = ".";
            // 
            // Btnguardar
            // 
            Btnguardar.BackColor = Color.FromArgb(43, 56, 143);
            Btnguardar.FlatStyle = FlatStyle.Flat;
            Btnguardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btnguardar.ForeColor = SystemColors.ButtonHighlight;
            Btnguardar.Location = new Point(111, 172);
            Btnguardar.Margin = new Padding(4, 5, 4, 5);
            Btnguardar.Name = "Btnguardar";
            Btnguardar.Size = new Size(374, 100);
            Btnguardar.TabIndex = 27;
            Btnguardar.Text = "Guardar y cerrar";
            Btnguardar.UseVisualStyleBackColor = false;
            Btnguardar.Click += button1_Click;
            // 
            // CajaChicaMonto
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(43, 56, 143);
            ClientSize = new Size(626, 330);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "CajaChicaMonto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Caja_Chica_Agregar_Monto";
            Load += FRM_CajaChicaMonto_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtMonto;
        private TextBox textBox1;
        private Label label7;
        private Panel panel1;
        private Button Btnguardar;
        private Label lblTitulo;
    }
}