namespace Capa_de_Presentación.Formularios.Formularios_empleados_y_sacerdotes
{
    partial class EdiciónEspecialAcceso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdiciónEspecialAcceso));
            panel1 = new Panel();
            panel2 = new Panel();
            txtUsuario = new TextBox();
            label5 = new Label();
            label4 = new Label();
            txt_correo_electronico = new TextBox();
            btn_solicitarcorreo = new Button();
            label3 = new Label();
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
            panel1.Location = new Point(3, 2);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 561);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(txtUsuario);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txt_correo_electronico);
            panel2.Controls.Add(btn_solicitarcorreo);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(4, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.No;
            panel2.Size = new Size(671, 549);
            panel2.TabIndex = 0;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(70, 182);
            txtUsuario.Margin = new Padding(4, 3, 4, 3);
            txtUsuario.MaxLength = 20;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(495, 31);
            txtUsuario.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(81, 153);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(76, 25);
            label5.TabIndex = 6;
            label5.Text = "Usuario:";
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
            // txt_correo_electronico
            // 
            txt_correo_electronico.Location = new Point(70, 245);
            txt_correo_electronico.Margin = new Padding(4, 3, 4, 3);
            txt_correo_electronico.MaxLength = 40;
            txt_correo_electronico.Name = "txt_correo_electronico";
            txt_correo_electronico.Size = new Size(495, 31);
            txt_correo_electronico.TabIndex = 2;
            // 
            // btn_solicitarcorreo
            // 
            btn_solicitarcorreo.BackColor = Color.FromArgb(43, 56, 143);
            btn_solicitarcorreo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_solicitarcorreo.ForeColor = Color.White;
            btn_solicitarcorreo.ImageAlign = ContentAlignment.TopCenter;
            btn_solicitarcorreo.Location = new Point(81, 377);
            btn_solicitarcorreo.Margin = new Padding(4, 3, 4, 3);
            btn_solicitarcorreo.Name = "btn_solicitarcorreo";
            btn_solicitarcorreo.Size = new Size(447, 73);
            btn_solicitarcorreo.TabIndex = 4;
            btn_solicitarcorreo.Text = "Solicitar Código";
            btn_solicitarcorreo.UseVisualStyleBackColor = false;
            btn_solicitarcorreo.UseWaitCursor = true;
            btn_solicitarcorreo.Click += btn_restablecer_contrasena_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(106, 292);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(416, 50);
            label3.TabIndex = 3;
            label3.Text = "Al presionar “Solicitar Código” se enviará un correo\r\ncon el código para acceder a la edición especial.";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(81, 217);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(161, 25);
            label2.TabIndex = 1;
            label2.Text = "Correo Electrónico:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(106, 37);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(459, 45);
            label1.TabIndex = 0;
            label1.Text = "Acceso para edición especial";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // EdiciónEspecialAcceso
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 564);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EdiciónEspecialAcceso";
            Text = "EdiciónEspecialAcceso";
            Load += EdiciónEspecialAcceso_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox txtUsuario;
        private Label label5;
        private Label label4;
        private TextBox txt_correo_electronico;
        private Button btn_solicitarcorreo;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}