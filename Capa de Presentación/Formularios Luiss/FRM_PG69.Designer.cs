namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_PG69
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
            label1 = new Label();
            dgvCajaChica = new DataGridView();
            cNombre = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            Btncerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCajaChica).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(225, 25);
            label1.Name = "label1";
            label1.Size = new Size(284, 32);
            label1.TabIndex = 0;
            label1.Text = "Registro para caja chica";
            // 
            // dgvCajaChica
            // 
            dgvCajaChica.BackgroundColor = SystemColors.Control;
            dgvCajaChica.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCajaChica.Columns.AddRange(new DataGridViewColumn[] { cNombre, cDetalle, cSaldo });
            dgvCajaChica.Location = new Point(139, 75);
            dgvCajaChica.Name = "dgvCajaChica";
            dgvCajaChica.RowHeadersWidth = 51;
            dgvCajaChica.Size = new Size(503, 314);
            dgvCajaChica.TabIndex = 1;
            dgvCajaChica.CellContentClick += dataGridView1_CellContentClick;
            // 
            // cNombre
            // 
            cNombre.HeaderText = "Nombre";
            cNombre.MinimumWidth = 6;
            cNombre.Name = "cNombre";
            cNombre.Width = 150;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.MinimumWidth = 6;
            cDetalle.Name = "cDetalle";
            cDetalle.Width = 150;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.MinimumWidth = 6;
            cSaldo.Name = "cSaldo";
            cSaldo.Width = 150;
            // 
            // Btncerrar
            // 
            Btncerrar.BackColor = Color.FromArgb(43, 56, 143);
            Btncerrar.FlatStyle = FlatStyle.Flat;
            Btncerrar.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btncerrar.ForeColor = Color.White;
            Btncerrar.Location = new Point(318, 417);
            Btncerrar.Name = "Btncerrar";
            Btncerrar.Size = new Size(148, 48);
            Btncerrar.TabIndex = 17;
            Btncerrar.Text = "Cerrar";
            Btncerrar.UseVisualStyleBackColor = false;
            // 
            // FRM_PG69
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 488);
            Controls.Add(Btncerrar);
            Controls.Add(dgvCajaChica);
            Controls.Add(label1);
            Name = "FRM_PG69";
            Text = "FRM_PG69";
            Load += FRM_PG69_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCajaChica).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvCajaChica;
        private DataGridViewTextBoxColumn cNombre;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private Button Btncerrar;
    }
}