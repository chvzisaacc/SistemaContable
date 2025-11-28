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
            dataGridView1 = new DataGridView();
            LimiteDay = new NumericUpDown();
            label1 = new Label();
            Estado = new CheckBox();
            label2 = new Label();
            btnModificar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LimiteDay).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(420, 73);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Size = new Size(944, 65);
            dataGridView1.TabIndex = 0;
            // 
            // LimiteDay
            // 
            LimiteDay.Location = new Point(178, 37);
            LimiteDay.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            LimiteDay.Name = "LimiteDay";
            LimiteDay.Size = new Size(180, 31);
            LimiteDay.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 2;
            label1.Text = "Días límite";
            // 
            // Estado
            // 
            Estado.AutoSize = true;
            Estado.Location = new Point(178, 83);
            Estado.Name = "Estado";
            Estado.Size = new Size(144, 29);
            Estado.TabIndex = 3;
            Estado.Text = "Alarma activa";
            Estado.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(154, 25);
            label2.TabIndex = 4;
            label2.Text = "Activar/Desactivar";
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(90, 139);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(147, 49);
            btnModificar.TabIndex = 5;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += button1_Click;
            // 
            // ALERTA_SISTEMA
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1388, 200);
            Controls.Add(btnModificar);
            Controls.Add(label2);
            Controls.Add(Estado);
            Controls.Add(label1);
            Controls.Add(LimiteDay);
            Controls.Add(dataGridView1);
            Name = "ALERTA_SISTEMA";
            Text = "ALERTA_SISTEMA";
            Load += ALERTA_SISTEMA_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)LimiteDay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private NumericUpDown LimiteDay;
        private Label label1;
        private CheckBox Estado;
        private Label label2;
        private Button btnModificar;
    }
}