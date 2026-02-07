namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class Partidas_Dobles
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
            dgvPartidas = new DataGridView();
            cNombre = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            Btncerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPartidas).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(269, 39);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(537, 48);
            label1.TabIndex = 0;
            label1.Text = "Partida doble de la transacción";
            label1.Click += label1_Click;
            // 
            // dgvPartidas
            // 
            dgvPartidas.AllowUserToAddRows = false;
            dgvPartidas.AllowUserToDeleteRows = false;
            dgvPartidas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPartidas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvPartidas.BackgroundColor = SystemColors.Window;
            dgvPartidas.BorderStyle = BorderStyle.None;
            dgvPartidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidas.Columns.AddRange(new DataGridViewColumn[] { cNombre, cDetalle, cSaldo });
            dgvPartidas.Location = new Point(57, 110);
            dgvPartidas.Margin = new Padding(4, 5, 4, 5);
            dgvPartidas.Name = "dgvPartidas";
            dgvPartidas.ReadOnly = true;
            dgvPartidas.RowHeadersWidth = 51;
            dgvPartidas.Size = new Size(956, 133);
            dgvPartidas.TabIndex = 1;
            dgvPartidas.CellContentClick += dataGridView1_CellContentClick;
            // 
            // cNombre
            // 
            cNombre.HeaderText = "Nombre";
            cNombre.MinimumWidth = 6;
            cNombre.Name = "cNombre";
            cNombre.ReadOnly = true;
            cNombre.Visible = false;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.MinimumWidth = 6;
            cDetalle.Name = "cDetalle";
            cDetalle.ReadOnly = true;
            cDetalle.Visible = false;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.MinimumWidth = 6;
            cSaldo.Name = "cSaldo";
            cSaldo.ReadOnly = true;
            cSaldo.Visible = false;
            // 
            // Btncerrar
            // 
            Btncerrar.BackColor = Color.FromArgb(43, 56, 143);
            Btncerrar.FlatStyle = FlatStyle.Flat;
            Btncerrar.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btncerrar.ForeColor = Color.White;
            Btncerrar.Location = new Point(423, 271);
            Btncerrar.Margin = new Padding(4, 5, 4, 5);
            Btncerrar.Name = "Btncerrar";
            Btncerrar.Size = new Size(211, 80);
            Btncerrar.TabIndex = 17;
            Btncerrar.Text = "Cerrar";
            Btncerrar.UseVisualStyleBackColor = false;
            Btncerrar.Click += Btncerrar_Click;
            // 
            // Partidas_Dobles
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 375);
            Controls.Add(Btncerrar);
            Controls.Add(dgvPartidas);
            Controls.Add(label1);
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Partidas_Dobles";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Partidas_Dobles";
            Load += FRM_PG69_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvPartidas;
        private Button Btncerrar;
        private DataGridViewTextBoxColumn cNombre;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
    }
}