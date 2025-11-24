namespace Capa_de_Presentación.RECONOCIMIENTO_FACIAL
{
    partial class RECONOCER
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
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(911, 420);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(12, 493);
            button1.Name = "button1";
            button1.Size = new Size(187, 60);
            button1.TabIndex = 11;
            button1.Text = "Encender Cámara";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(736, 493);
            button2.Name = "button2";
            button2.Size = new Size(187, 60);
            button2.TabIndex = 12;
            button2.Text = "Detener Cámara";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(297, 9);
            label2.Name = "label2";
            label2.Size = new Size(282, 32);
            label2.TabIndex = 14;
            label2.Text = "USUARIO DETECTADO:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(585, 9);
            label1.Name = "label1";
            label1.Size = new Size(38, 32);
            label1.TabIndex = 15;
            label1.Text = "...";
            label1.Click += label1_Click;
            // 
            // RECONOCER
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(935, 565);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "RECONOCER";
            Text = "RECONOCER";
            Load += RECONOCER_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Label label2;
        private Label label1;
    }
}