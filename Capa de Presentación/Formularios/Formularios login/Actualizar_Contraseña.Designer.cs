namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class Actualizar_Contraseña
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Actualizar_Contraseña));
            panel1 = new Panel();
            panel2 = new Panel();
            lbl_volver = new Label();
            btn_confirmar = new Button();
            txt_confirmar_contrasena = new TextBox();
            label3 = new Label();
            btn_cancelar = new Button();
            txt_nueva_contrasena = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Yellow;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(496, 163);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(808, 402);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lbl_volver);
            panel2.Controls.Add(btn_confirmar);
            panel2.Controls.Add(txt_confirmar_contrasena);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(btn_cancelar);
            panel2.Controls.Add(txt_nueva_contrasena);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(4, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.No;
            panel2.Size = new Size(796, 390);
            panel2.TabIndex = 0;
            // 
            // lbl_volver
            // 
            lbl_volver.AutoSize = true;
            lbl_volver.Location = new Point(3, 363);
            lbl_volver.Name = "lbl_volver";
            lbl_volver.Size = new Size(61, 25);
            lbl_volver.TabIndex = 9;
            lbl_volver.Text = "Volver";
            lbl_volver.Click += label4_Click;
            // 
            // btn_confirmar
            // 
            btn_confirmar.BackColor = Color.FromArgb(43, 56, 143);
            btn_confirmar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_confirmar.ForeColor = Color.White;
            btn_confirmar.ImageAlign = ContentAlignment.TopCenter;
            btn_confirmar.Location = new Point(626, 337);
            btn_confirmar.Margin = new Padding(4, 3, 4, 3);
            btn_confirmar.Name = "btn_confirmar";
            btn_confirmar.Size = new Size(166, 48);
            btn_confirmar.TabIndex = 7;
            btn_confirmar.Text = "Confirmar";
            btn_confirmar.UseVisualStyleBackColor = false;
            btn_confirmar.UseWaitCursor = true;
            btn_confirmar.Click += btnConfirmar_Click;
            // 
            // txt_confirmar_contrasena
            // 
            txt_confirmar_contrasena.BorderStyle = BorderStyle.FixedSingle;
            txt_confirmar_contrasena.Location = new Point(70, 272);
            txt_confirmar_contrasena.Margin = new Padding(4, 3, 4, 3);
            txt_confirmar_contrasena.MaxLength = 30;
            txt_confirmar_contrasena.Name = "txt_confirmar_contrasena";
            txt_confirmar_contrasena.Size = new Size(606, 31);
            txt_confirmar_contrasena.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(81, 243);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(182, 25);
            label3.TabIndex = 5;
            label3.Text = "Confirmar contraseña";
            // 
            // btn_cancelar
            // 
            btn_cancelar.BackColor = Color.FromArgb(43, 56, 143);
            btn_cancelar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_cancelar.ForeColor = Color.White;
            btn_cancelar.ImageAlign = ContentAlignment.TopCenter;
            btn_cancelar.Location = new Point(439, 337);
            btn_cancelar.Margin = new Padding(4, 3, 4, 3);
            btn_cancelar.Name = "btn_cancelar";
            btn_cancelar.Size = new Size(179, 48);
            btn_cancelar.TabIndex = 8;
            btn_cancelar.Text = "Cancelar";
            btn_cancelar.UseVisualStyleBackColor = false;
            btn_cancelar.UseWaitCursor = true;
            btn_cancelar.Click += button1_Click;
            // 
            // txt_nueva_contrasena
            // 
            txt_nueva_contrasena.BorderStyle = BorderStyle.FixedSingle;
            txt_nueva_contrasena.Location = new Point(70, 193);
            txt_nueva_contrasena.Margin = new Padding(4, 3, 4, 3);
            txt_nueva_contrasena.MaxLength = 30;
            txt_nueva_contrasena.Name = "txt_nueva_contrasena";
            txt_nueva_contrasena.Size = new Size(606, 31);
            txt_nueva_contrasena.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(81, 165);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(153, 25);
            label2.TabIndex = 1;
            label2.Text = "Nueva contraseña";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(204, 43);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(384, 45);
            label1.TabIndex = 0;
            label1.Text = "Restablecer Contraseña";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Actualizar_Contraseña
            // 
            AcceptButton = btn_confirmar;
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_10_22_a_las_17_39_32_e87a041a;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1601, 898);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Actualizar_Contraseña";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Actualizar_Contraseña";
            Load += Actualizar_Contraseña_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox txt_confirmar_contrasena;
        private Label label3;
        private Button btn_cancelar;
        private TextBox txt_nueva_contrasena;
        private Label label2;
        private Label label1;
        private Button btn_confirmar;
        private Label lbl_volver;
    }
}