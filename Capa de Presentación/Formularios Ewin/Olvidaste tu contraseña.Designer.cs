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
            txt_correo_electronico = new TextBox();
            btn_restablecer_contrasena = new Button();
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
            panel1.Location = new Point(398, 115);
            panel1.Name = "panel1";
            panel1.Size = new Size(506, 450);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txt_correo_electronico);
            panel2.Controls.Add(btn_restablecer_contrasena);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.No;
            panel2.Size = new Size(496, 440);
            panel2.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(2, 418);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 5;
            label4.Text = "Volver";
            label4.Click += label4_Click;
            // 
            // txt_correo_electronico
            // 
            txt_correo_electronico.Location = new Point(56, 155);
            txt_correo_electronico.Name = "txt_correo_electronico";
            txt_correo_electronico.Size = new Size(397, 27);
            txt_correo_electronico.TabIndex = 2;
            txt_correo_electronico.TextChanged += textBox1_TextChanged;
            // 
            // btn_restablecer_contrasena
            // 
            btn_restablecer_contrasena.BackColor = Color.FromArgb(43, 56, 143);
            btn_restablecer_contrasena.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_restablecer_contrasena.ForeColor = Color.White;
            btn_restablecer_contrasena.ImageAlign = ContentAlignment.TopCenter;
            btn_restablecer_contrasena.Location = new Point(65, 275);
            btn_restablecer_contrasena.Name = "btn_restablecer_contrasena";
            btn_restablecer_contrasena.Size = new Size(358, 59);
            btn_restablecer_contrasena.TabIndex = 4;
            btn_restablecer_contrasena.Text = "Restablecer Contraseña";
            btn_restablecer_contrasena.UseVisualStyleBackColor = false;
            btn_restablecer_contrasena.UseWaitCursor = true;
            btn_restablecer_contrasena.Click += button1_Click_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 185);
            label3.Name = "label3";
            label3.Size = new Size(397, 40);
            label3.TabIndex = 3;
            label3.Text = "Al presionar “Restablecer Contraseña” se enviará un correo\r\ncon link para restablecer la contraseña.";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 132);
            label2.Name = "label2";
            label2.Size = new Size(135, 20);
            label2.TabIndex = 1;
            label2.Text = "Correo Electrónico:";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(113, 61);
            label1.Name = "label1";
            label1.Size = new Size(288, 38);
            label1.TabIndex = 0;
            label1.Text = "Contraseña Olvidada";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Olvidaste_tu_contraseña
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_10_22_a_las_17_39_32_e87a041a;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1282, 719);
            Controls.Add(panel1);
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
        private TextBox txt_correo_electronico;
        private Label label2;
        private Label label1;
        private Label label3;
        private Button btn_restablecer_contrasena;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label4;
    }
}