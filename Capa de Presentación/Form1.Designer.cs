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
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.Location = new Point(447, 168);
            btn1.Margin = new Padding(2, 2, 2, 2);
            btn1.Name = "btn1";
            btn1.Size = new Size(75, 27);
            btn1.TabIndex = 0;
            btn1.Text = "button1";
            btn1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(117, 168);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(309, 55);
            button2.TabIndex = 1;
            button2.Text = "helio";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(213, 65);
            treeView1.Margin = new Padding(2, 2, 2, 2);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(129, 89);
            treeView1.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(447, 200);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(btnSalir);
            Controls.Add(treeView1);
            Controls.Add(button2);
            Controls.Add(btn1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btn1;
        private Button button2;
        private TreeView treeView1;
        private Button btnSalir;
    }
}
