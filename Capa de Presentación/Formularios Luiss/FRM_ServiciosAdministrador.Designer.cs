namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_ServiciosAdministrador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_ServiciosAdministrador));
            textBox3 = new TextBox();
            pibBitacora = new PictureBox();
            textBox1 = new TextBox();
            pibGenerarReportes = new PictureBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pibBitacora).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pibGenerarReportes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(43, 56, 143);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox3.ForeColor = Color.White;
            textBox3.Location = new Point(166, 366);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(129, 32);
            textBox3.TabIndex = 30;
            textBox3.Text = "Bitácora";
            textBox3.MouseDoubleClick += textBox3_MouseDoubleClick;
            // 
            // pibBitacora
            // 
            pibBitacora.Image = (Image)resources.GetObject("pibBitacora.Image");
            pibBitacora.Location = new Point(46, 330);
            pibBitacora.Name = "pibBitacora";
            pibBitacora.Size = new Size(367, 105);
            pibBitacora.SizeMode = PictureBoxSizeMode.Zoom;
            pibBitacora.TabIndex = 29;
            pibBitacora.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(43, 56, 143);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(125, 263);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(220, 32);
            textBox1.TabIndex = 28;
            textBox1.Text = "Generar Reportes";
            textBox1.MouseClick += textBox1_MouseClick;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // pibGenerarReportes
            // 
            pibGenerarReportes.Image = (Image)resources.GetObject("pibGenerarReportes.Image");
            pibGenerarReportes.Location = new Point(46, 230);
            pibGenerarReportes.Name = "pibGenerarReportes";
            pibGenerarReportes.Size = new Size(367, 105);
            pibGenerarReportes.SizeMode = PictureBoxSizeMode.Zoom;
            pibGenerarReportes.TabIndex = 27;
            pibGenerarReportes.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(77, 54);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(287, 147);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(125, 9);
            label1.Name = "label1";
            label1.Size = new Size(170, 41);
            label1.TabIndex = 23;
            label1.Text = "SERVICIOS";
            // 
            // FRM_ServiciosAdministrador
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 480);
            Controls.Add(textBox3);
            Controls.Add(pibBitacora);
            Controls.Add(textBox1);
            Controls.Add(pibGenerarReportes);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "FRM_ServiciosAdministrador";
            Text = "FRM_ServiciosAdministrador";
            ((System.ComponentModel.ISupportInitialize)pibBitacora).EndInit();
            ((System.ComponentModel.ISupportInitialize)pibGenerarReportes).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox3;
        private PictureBox pibBitacora;
        private TextBox textBox1;
        private PictureBox pibGenerarReportes;
        private PictureBox pictureBox1;
        private Label label1;
    }
}