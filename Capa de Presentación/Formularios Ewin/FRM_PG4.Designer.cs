namespace Capa_de_Presentación.Formularios_Ewin
{
    partial class FRM_PG4
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
            btnConfirmar = new Button();
            txtConfirmarContraseña = new TextBox();
            label3 = new Label();
            button1 = new Button();
            txtNuevaContraseña = new TextBox();
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
            panel1.Location = new Point(496, 162);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(808, 403);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnConfirmar);
            panel2.Controls.Add(txtConfirmarContraseña);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(txtNuevaContraseña);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(4, 4);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.No;
            panel2.Size = new Size(796, 391);
            panel2.TabIndex = 0;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.FromArgb(43, 56, 143);
            btnConfirmar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.ImageAlign = ContentAlignment.TopCenter;
            btnConfirmar.Location = new Point(475, 179);
            btnConfirmar.Margin = new Padding(4);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(284, 49);
            btnConfirmar.TabIndex = 7;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.UseWaitCursor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // txtConfirmarContraseña
            // 
            txtConfirmarContraseña.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmarContraseña.Location = new Point(70, 271);
            txtConfirmarContraseña.Margin = new Padding(4);
            txtConfirmarContraseña.Name = "txtConfirmarContraseña";
            txtConfirmarContraseña.Size = new Size(386, 31);
            txtConfirmarContraseña.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(81, 242);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(182, 25);
            label3.TabIndex = 5;
            label3.Text = "Confirmar contraseña";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.ImageAlign = ContentAlignment.TopCenter;
            button1.Location = new Point(475, 256);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(284, 49);
            button1.TabIndex = 4;
            button1.Text = "Cancelar";
            button1.UseVisualStyleBackColor = false;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // txtNuevaContraseña
            // 
            txtNuevaContraseña.BorderStyle = BorderStyle.FixedSingle;
            txtNuevaContraseña.Location = new Point(70, 194);
            txtNuevaContraseña.Margin = new Padding(4);
            txtNuevaContraseña.Name = "txtNuevaContraseña";
            txtNuevaContraseña.Size = new Size(386, 31);
            txtNuevaContraseña.TabIndex = 2;
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
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(204, 42);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(384, 45);
            label1.TabIndex = 0;
            label1.Text = "Restablecer Contraseña";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // FRM_PG4
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_10_22_a_las_17_39_32_e87a041a;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1602, 899);
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "FRM_PG4";
            Text = "FRM_PG4";
            Load += FRM_PG4_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TextBox txtConfirmarContraseña;
        private Label label3;
        private Button button1;
        private TextBox txtNuevaContraseña;
        private Label label2;
        private Label label1;
        private Button btnConfirmar;
    }
}