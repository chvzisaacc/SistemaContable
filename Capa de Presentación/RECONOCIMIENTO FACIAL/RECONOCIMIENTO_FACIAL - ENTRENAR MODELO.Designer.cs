namespace Capa_de_Presentación.RECONOCIMIENTO_FACIAL
{
    partial class RECONOCIMIENTO_FACIAL
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
            components = new System.ComponentModel.Container();
            button1 = new Button();
            button2 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            button4 = new Button();
            comboBox1 = new ComboBox();
            label3 = new Label();
            button5 = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(12, 311);
            button1.Name = "button1";
            button1.Size = new Size(187, 60);
            button1.TabIndex = 5;
            button1.Text = "Encender Cámara";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(43, 56, 143);
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(12, 377);
            button2.Name = "button2";
            button2.Size = new Size(187, 60);
            button2.TabIndex = 6;
            button2.Text = "Detener Cámara";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(215, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(573, 417);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click_1;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(43, 56, 143);
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = SystemColors.Control;
            button4.Location = new Point(12, 245);
            button4.Name = "button4";
            button4.Size = new Size(187, 60);
            button4.TabIndex = 10;
            button4.Text = "Entrenar existentes";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 98);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(187, 33);
            comboBox1.TabIndex = 11;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 58);
            label3.Name = "label3";
            label3.Size = new Size(80, 25);
            label3.TabIndex = 13;
            label3.Text = "Usuarios";
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(43, 56, 143);
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = SystemColors.Control;
            button5.Location = new Point(12, 179);
            button5.Name = "button5";
            button5.Size = new Size(187, 60);
            button5.TabIndex = 14;
            button5.Text = "Borrar existentes";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 15;
            label1.Text = "Volver";
            label1.Click += label1_Click;
            // 
            // RECONOCIMIENTO_FACIAL
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(button5);
            Controls.Add(label3);
            Controls.Add(comboBox1);
            Controls.Add(button4);
            Controls.Add(pictureBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "RECONOCIMIENTO_FACIAL";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RECONOCIMIENTO_FACIAL";
            Load += RECONOCIMIENTO_FACIAL_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox1;
        private Button button4;
        private ComboBox comboBox1;
        private Label label3;
        private Button button5;
        private Label label1;
    }
}