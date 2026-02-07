namespace Capa_de_Presentación.ALERTA
{
    partial class ALERTA_SISTEMA
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
            LimiteDay = new NumericUpDown();
            label1 = new Label();
            Estado = new CheckBox();
            label2 = new Label();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)LimiteDay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // LimiteDay
            // 
            LimiteDay.Location = new Point(166, 36);
            LimiteDay.Name = "LimiteDay";
            LimiteDay.Size = new Size(180, 31);
            LimiteDay.TabIndex = 0;
            LimiteDay.ValueChanged += LimiteDay_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 42);
            label1.Name = "label1";
            label1.Size = new Size(98, 25);
            label1.TabIndex = 1;
            label1.Text = "Días Limite";
            // 
            // Estado
            // 
            Estado.AutoSize = true;
            Estado.Checked = true;
            Estado.CheckState = CheckState.Checked;
            Estado.Location = new Point(166, 94);
            Estado.Name = "Estado";
            Estado.Size = new Size(180, 29);
            Estado.TabIndex = 2;
            Estado.Text = "Activar/Desactivar";
            Estado.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 95);
            label2.Name = "label2";
            label2.Size = new Size(66, 25);
            label2.TabIndex = 3;
            label2.Text = "Estado";
            // 
            // button1
            // 
            button1.Location = new Point(79, 147);
            button1.Name = "button1";
            button1.Size = new Size(150, 56);
            button1.TabIndex = 4;
            button1.Text = "Modificar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(399, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(910, 94);
            dataGridView1.TabIndex = 5;
            // 
            // ALERTA_SISTEMA
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1321, 230);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(Estado);
            Controls.Add(label1);
            Controls.Add(LimiteDay);
            MaximizeBox = false;
            Name = "ALERTA_SISTEMA";
            Text = "ALERTA_SISTEMA";
            Load += ALERTA_SISTEMA_Load;
            ((System.ComponentModel.ISupportInitialize)LimiteDay).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown LimiteDay;
        private Label label1;
        private CheckBox Estado;
        private Label label2;
        private Button button1;
        private DataGridView dataGridView1;
    }
}