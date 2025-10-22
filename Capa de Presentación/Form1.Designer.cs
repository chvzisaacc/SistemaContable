namespace Capa_de_Presentación
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn1 = new Button();
            button2 = new Button();
            treeView1 = new TreeView();
            btnSalir = new Button();
            button1 = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.Location = new Point(511, 224);
            btn1.Margin = new Padding(2, 3, 2, 3);
            btn1.Name = "btn1";
            btn1.Size = new Size(86, 36);
            btn1.TabIndex = 0;
            btn1.Text = "button1";
            btn1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(134, 224);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(353, 73);
            button2.TabIndex = 1;
            button2.Text = "ISAAC SELLACO";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(243, 87);
            treeView1.Margin = new Padding(2, 3, 2, 3);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(147, 117);
            treeView1.TabIndex = 2;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(511, 267);
            btnSalir.Margin = new Padding(3, 4, 3, 4);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(86, 31);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += button3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(514, 159);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 4;
            button1.Text = "Quesito_rico";
            button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(522, 69);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(101, 24);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(678, 385);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(btnSalir);
            Controls.Add(treeView1);
            Controls.Add(button2);
            Controls.Add(btn1);
            Margin = new Padding(2, 3, 2, 3);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn1;
        private Button button2;
        private TreeView treeView1;
        private Button btnSalir;
        private Button button1;
        private CheckBox checkBox1;
    }
}
