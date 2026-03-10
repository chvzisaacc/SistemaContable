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
            pbOcultar = new PictureBox();
            pbMostrar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbOcultar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMostrar).BeginInit();
            SuspendLayout();
            // 
            // txt_usuario
            // 
            txt_usuario.BackColor = Color.White;
            txt_usuario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_usuario.Location = new Point(178, 178);
            txt_usuario.Margin = new Padding(3, 2, 3, 2);
            txt_usuario.MaxLength = 20;
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(362, 29);
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
            txt_contraseña.Location = new Point(178, 223);
            txt_contraseña.Margin = new Padding(3, 2, 3, 2);
            txt_contraseña.MaxLength = 30;
            txt_contraseña.Name = "txt_contraseña";
            txt_contraseña.PasswordChar = '*';
            txt_contraseña.Size = new Size(330, 29);
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
            btn_iniciar_sesion.Location = new Point(237, 300);
            btn_iniciar_sesion.Margin = new Padding(3, 2, 3, 2);
            btn_iniciar_sesion.Name = "btn_iniciar_sesion";
            btn_iniciar_sesion.Size = new Size(232, 53);
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
            lbl_olvidaste_contrasena.Location = new Point(259, 355);
            lbl_olvidaste_contrasena.Name = "lbl_olvidaste_contrasena";
            lbl_olvidaste_contrasena.Size = new Size(172, 19);
            lbl_olvidaste_contrasena.TabIndex = 5;
            lbl_olvidaste_contrasena.Text = "¿Olvidaste tu contraseña?";
            lbl_olvidaste_contrasena.TextAlign = ContentAlignment.TopRight;
            lbl_olvidaste_contrasena.Click += label3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(337, 256);
            label1.Margin = new Padding(1, 0, 1, 0);
            label1.Name = "label1";
            label1.Size = new Size(10, 15);
            label1.TabIndex = 6;
            label1.Text = ".";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(202, 311);
            pictureBox1.Margin = new Padding(1, 2, 1, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 28);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pbOcultar
            // 
            pbOcultar.Image = Properties.Resources.esconder;
            pbOcultar.Location = new Point(512, 223);
            pbOcultar.Margin = new Padding(2, 2, 2, 2);
            pbOcultar.Name = "pbOcultar";
            pbOcultar.Size = new Size(27, 23);
            pbOcultar.SizeMode = PictureBoxSizeMode.Zoom;
            pbOcultar.TabIndex = 8;
            pbOcultar.TabStop = false;
            pbOcultar.Click += pbOcultar_Click;
            // 
            // pbMostrar
            // 
            pbMostrar.Image = Properties.Resources.vista;
            pbMostrar.Location = new Point(512, 223);
            pbMostrar.Margin = new Padding(2, 2, 2, 2);
            pbMostrar.Name = "pbMostrar";
            pbMostrar.Size = new Size(27, 23);
            pbMostrar.SizeMode = PictureBoxSizeMode.Zoom;
            pbMostrar.TabIndex = 9;
            pbMostrar.TabStop = false;
            pbMostrar.Click += pbMostrar_Click;
            // 
            // FRM_PG1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1121, 539);
            Controls.Add(pbMostrar);
            Controls.Add(pbOcultar);
            Controls.Add(txt_contraseña);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(txt_usuario);
            Controls.Add(lbl_olvidaste_contrasena);
            Controls.Add(btn_iniciar_sesion);
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "FRM_PG1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio de Sesión";
            FormClosing += FRM_PG1_FormClosing_1;
            Load += FRM_PG1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbOcultar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMostrar).EndInit();
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
        private PictureBox pbOcultar;
        private PictureBox pbMostrar;
    }
}