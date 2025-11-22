namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class Olvidaste_tu_contraseña
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
            panel1 = new Panel();
            panel2 = new Panel();
            label4 = new Label();
            txtCorreo = new TextBox();
            button1 = new Button();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Yellow;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(498, 144);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(632, 562);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtCorreo);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(4, 4);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.No;
            panel2.Size = new Size(620, 550);
            panel2.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 523);
            label4.Name = "label4";
            label4.Size = new Size(61, 25);
            label4.TabIndex = 5;
            label4.Text = "Volver";
            label4.Click += label4_Click;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(70, 194);
            txtCorreo.Margin = new Padding(4);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(495, 31);
            txtCorreo.TabIndex = 2;
            txtCorreo.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.Location = new Point(81, 344);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(448, 74);
            button1.TabIndex = 4;
            button1.Text = "Restablecer Contraseña";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(70, 231);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(473, 50);
            label3.TabIndex = 3;
            label3.Text = "Al presionar “Restablecer Contraseña” se enviará un correo\r\ncon link para restablecer la contraseña.";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(81, 165);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(161, 25);
            label2.TabIndex = 1;
            label2.Text = "Correo Electrónico:";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(141, 76);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(342, 45);
            label1.TabIndex = 0;
            label1.Text = "Contraseña Olvidada";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Olvidaste_tu_contraseña
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_10_22_a_las_17_39_32_e87a041a;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1602, 899);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "Olvidaste_tu_contraseña";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Olvisdaste tu contraseña";
            Load += FRM_PG2_Load_1;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox txtCorreo;
        private Label label2;
        private Label label1;
        private Label label3;
        private Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label4;
    }
}