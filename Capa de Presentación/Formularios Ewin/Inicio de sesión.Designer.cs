namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class FRM_PG1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG1));
            txt_usuario = new TextBox();
            txt_contraseña = new TextBox();
            btn_iniciar_sesion = new Button();
            lbl_olvidaste_contrasena = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txt_usuario
            // 
            txt_usuario.BackColor = Color.White;
            txt_usuario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_usuario.Location = new Point(203, 237);
            txt_usuario.MaxLength = 30;
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(414, 34);
            txt_usuario.TabIndex = 0;
            txt_usuario.Text = "Usuario";
            txt_usuario.Click += txtUsuario_Click;
            txt_usuario.KeyPress += txt_usuario_KeyPress;
            txt_usuario.Leave += txtUsuario_Leave;
            // 
            // txt_contraseña
            // 
            txt_contraseña.BackColor = Color.White;
            txt_contraseña.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_contraseña.Location = new Point(203, 297);
            txt_contraseña.MaxLength = 30;
            txt_contraseña.Name = "txt_contraseña";
            txt_contraseña.Size = new Size(414, 34);
            txt_contraseña.TabIndex = 2;
            txt_contraseña.Text = "Contraseña";
            txt_contraseña.Click += txtContraseña_Click;
            txt_contraseña.KeyPress += txt_contraseña_KeyPress;
            txt_contraseña.Leave += txtContraseña_Leave;
            // 
            // btn_iniciar_sesion
            // 
            btn_iniciar_sesion.BackColor = Color.FromArgb(43, 56, 143);
            btn_iniciar_sesion.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_iniciar_sesion.ForeColor = Color.Transparent;
            btn_iniciar_sesion.Location = new Point(271, 400);
            btn_iniciar_sesion.Name = "btn_iniciar_sesion";
            btn_iniciar_sesion.Size = new Size(265, 71);
            btn_iniciar_sesion.TabIndex = 4;
            btn_iniciar_sesion.Text = "Iniciar Sesión";
            btn_iniciar_sesion.UseVisualStyleBackColor = false;
            btn_iniciar_sesion.Click += button1_Click;
            // 
            // lbl_olvidaste_contrasena
            // 
            lbl_olvidaste_contrasena.AutoSize = true;
            lbl_olvidaste_contrasena.BackColor = Color.Transparent;
            lbl_olvidaste_contrasena.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbl_olvidaste_contrasena.ForeColor = Color.Transparent;
            lbl_olvidaste_contrasena.Location = new Point(296, 473);
            lbl_olvidaste_contrasena.Name = "lbl_olvidaste_contrasena";
            lbl_olvidaste_contrasena.Size = new Size(213, 23);
            lbl_olvidaste_contrasena.TabIndex = 5;
            lbl_olvidaste_contrasena.Text = "¿Olvidaste tu contraseña?";
            lbl_olvidaste_contrasena.TextAlign = ContentAlignment.TopRight;
            lbl_olvidaste_contrasena.Click += label3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(386, 341);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(12, 20);
            label1.TabIndex = 6;
            label1.Text = ".";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(230, 415);
            pictureBox1.Margin = new Padding(2, 3, 2, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(37, 37);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // FRM_PG1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_10_21_a_las_22_22_16_14a2ddba;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1281, 719);
            Controls.Add(txt_contraseña);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(txt_usuario);
            Controls.Add(lbl_olvidaste_contrasena);
            Controls.Add(btn_iniciar_sesion);
            MaximizeBox = false;
            Name = "FRM_PG1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio de Sesión";
            Load += FRM_PG1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_usuario;
        private TextBox txt_contraseña;
        private Button btn_iniciar_sesion;
        private Label lbl_olvidaste_contrasena;
        private Label label1;
        private PictureBox pictureBox1;
    }
}