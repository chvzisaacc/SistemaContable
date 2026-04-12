namespace Capa_de_Presentación.Formularios.Formularios_inicio_de_sesion_y_ventana_administrador
{
    partial class AgregarParroquiaNueva
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarParroquiaNueva));
            label1 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(114, 32);
            label1.Name = "label1";
            label1.Size = new Size(144, 32);
            label1.TabIndex = 0;
            label1.Text = "Parroquias:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(143, 94);
            label2.Name = "label2";
            label2.Size = new Size(115, 32);
            label2.TabIndex = 1;
            label2.Text = "Nombre:";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(251, 203, 51);
            comboBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(264, 32);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(321, 36);
            comboBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(43, 56, 143);
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(32, 214);
            button1.Name = "button1";
            button1.Size = new Size(131, 42);
            button1.TabIndex = 3;
            button1.Text = "AGREGAR";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(43, 56, 143);
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(253, 214);
            button2.Name = "button2";
            button2.Size = new Size(123, 42);
            button2.TabIndex = 4;
            button2.Text = "GUARDAR";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(43, 56, 143);
            button3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button3.ForeColor = Color.White;
            button3.Location = new Point(467, 214);
            button3.Name = "button3";
            button3.Size = new Size(118, 42);
            button3.TabIndex = 5;
            button3.Text = "EDITAR";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(32, 145);
            label3.Name = "label3";
            label3.Size = new Size(234, 32);
            label3.TabIndex = 6;
            label3.Text = "Correo electrónico:";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(251, 203, 51);
            textBox1.Enabled = false;
            textBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            textBox1.Location = new Point(264, 91);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(321, 34);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(251, 203, 51);
            textBox2.Enabled = false;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            textBox2.Location = new Point(264, 145);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(321, 34);
            textBox2.TabIndex = 8;
            // 
            // AgregarParroquiaNueva
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 301);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AgregarParroquiaNueva";
            Text = "AgregarParroquiaNueva";
            Load += AgregarParroquiaNueva_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}