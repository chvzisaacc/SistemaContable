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
            pbOcultar = new PictureBox();
            pbMostrar = new PictureBox();
            btnSincronizar = new Button();
            ((System.ComponentModel.ISupportInitialize)pbOcultar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMostrar).BeginInit();
            SuspendLayout();
            // 
            // txt_usuario
            // 
            txt_usuario.BackColor = Color.White;
            txt_usuario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_usuario.Location = new Point(254, 297);
            txt_usuario.Margin = new Padding(4, 3, 4, 3);
            txt_usuario.MaxLength = 20;
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(517, 39);
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
            txt_contraseña.Location = new Point(254, 372);
            txt_contraseña.Margin = new Padding(4, 3, 4, 3);
            txt_contraseña.MaxLength = 30;
            txt_contraseña.Name = "txt_contraseña";
            txt_contraseña.PasswordChar = '*';
            txt_contraseña.Size = new Size(470, 39);
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
            btn_iniciar_sesion.Location = new Point(339, 500);
            btn_iniciar_sesion.Margin = new Padding(4, 3, 4, 3);
            btn_iniciar_sesion.Name = "btn_iniciar_sesion";
            btn_iniciar_sesion.Size = new Size(331, 88);
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
            lbl_olvidaste_contrasena.Location = new Point(370, 592);
            lbl_olvidaste_contrasena.Margin = new Padding(4, 0, 4, 0);
            lbl_olvidaste_contrasena.Name = "lbl_olvidaste_contrasena";
            lbl_olvidaste_contrasena.Size = new Size(265, 30);
            lbl_olvidaste_contrasena.TabIndex = 5;
            lbl_olvidaste_contrasena.Text = "¿Olvidaste tu contraseña?";
            lbl_olvidaste_contrasena.TextAlign = ContentAlignment.TopRight;
            lbl_olvidaste_contrasena.Click += label3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(483, 427);
            label1.Name = "label1";
            label1.Size = new Size(16, 25);
            label1.TabIndex = 6;
            label1.Text = ".";
            // 
            // pbOcultar
            // 
            pbOcultar.Image = Properties.Resources.esconder;
            pbOcultar.Location = new Point(731, 372);
            pbOcultar.Name = "pbOcultar";
            pbOcultar.Size = new Size(39, 38);
            pbOcultar.SizeMode = PictureBoxSizeMode.Zoom;
            pbOcultar.TabIndex = 8;
            pbOcultar.TabStop = false;
            pbOcultar.Click += pbOcultar_Click;
            // 
            // pbMostrar
            // 
            pbMostrar.Image = Properties.Resources.vista;
            pbMostrar.Location = new Point(731, 372);
            pbMostrar.Name = "pbMostrar";
            pbMostrar.Size = new Size(39, 38);
            pbMostrar.SizeMode = PictureBoxSizeMode.Zoom;
            pbMostrar.TabIndex = 9;
            pbMostrar.TabStop = false;
            pbMostrar.Click += pbMostrar_Click;
            // 
            // btnSincronizar
            // 
            btnSincronizar.BackColor = Color.FromArgb(43, 56, 143);
            btnSincronizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSincronizar.ForeColor = Color.White;
            btnSincronizar.Location = new Point(3, 841);
            btnSincronizar.Name = "btnSincronizar";
            btnSincronizar.Size = new Size(195, 45);
            btnSincronizar.TabIndex = 10;
            btnSincronizar.Text = "Sincronización";
            btnSincronizar.UseVisualStyleBackColor = false;
            btnSincronizar.Click += btnSincronizar_Click;
            // 
            // FRM_PG1
            // 
            AcceptButton = btn_iniciar_sesion;
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1601, 898);
            Controls.Add(btnSincronizar);
            Controls.Add(pbMostrar);
            Controls.Add(pbOcultar);
            Controls.Add(txt_contraseña);
            Controls.Add(label1);
            Controls.Add(txt_usuario);
            Controls.Add(lbl_olvidaste_contrasena);
            Controls.Add(btn_iniciar_sesion);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FRM_PG1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio de Sesión";
            Load += FRM_PG1_Load;
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
        private PictureBox pbOcultar;
        private PictureBox pbMostrar;
        private Button btnSincronizar;
    }
}