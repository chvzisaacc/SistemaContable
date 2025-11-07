namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_CERRARSESION
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_CERRARSESION));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(115, 150);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(306, 45);
            label1.TabIndex = 0;
            label1.Text = "Correo Electronico";
            label1.Click += label1_Click;
            // 
            // textBox2
            // 
            /*textBox2.BackColor = Color.FromArgb(43, 56, 143);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.White;
            textBox2.Location = new Point(179, 246);
            textBox2.Margin = new Padding(4, 4, 4, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(206, 42);
            textBox2.TabIndex = 16;
            textBox2.Text = "Cerrar Sesión";
            textBox2.TextChanged += textBox2_TextChanged;*/
            // 
            // pictureBox2
            // 
            /*pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(120, 231);
            pictureBox2.Margin = new Padding(4, 4, 4, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(318, 84);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 15;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click_1;*/
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(192, -10);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(165, 155);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(92, 177);
            button1.Name = "button1";
            button1.Size = new Size(265, 71);
            button1.TabIndex = 18;
            button1.Text = "Cerrar Sesión";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FRM_CERRARSESION
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 281);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FRM_CERRARSESION";
            Text = "FRM_CERRARSESION";
            Load += FRM_CERRARSESION_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Button button1;
    }
}