namespace Capa_de_Presentación.Formularios_Luiss
{
    partial class FRM_PG46
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_PG46));
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            dgvBitacora = new DataGridView();
            lblConsulte = new Label();
            lblTitulo = new Label();
            cCodigo = new DataGridViewTextBoxColumn();
            cNombre = new DataGridViewTextBoxColumn();
            cCuenta = new DataGridViewTextBoxColumn();
            cTipoCuenta = new DataGridViewTextBoxColumn();
            cDetalle = new DataGridViewTextBoxColumn();
            cSaldo = new DataGridViewTextBoxColumn();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(67, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(131, 122);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(239, 42);
            label1.Name = "label1";
            label1.Size = new Size(150, 32);
            label1.TabIndex = 13;
            label1.Text = "SACERDOTE";
            // 
            // dgvBitacora
            // 
            dgvBitacora.BackgroundColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.MenuText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Columns.AddRange(new DataGridViewColumn[] { cCodigo, cNombre, cCuenta, cTipoCuenta, cDetalle, cSaldo });
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.GridColor = SystemColors.MenuText;
            dgvBitacora.Location = new Point(118, 244);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.Size = new Size(944, 392);
            dgvBitacora.TabIndex = 12;
            // 
            // lblConsulte
            // 
            lblConsulte.AutoSize = true;
            lblConsulte.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsulte.Location = new Point(23, 185);
            lblConsulte.Name = "lblConsulte";
            lblConsulte.Size = new Size(1197, 32);
            lblConsulte.TabIndex = 11;
            lblConsulte.Text = "Observa cada una de las cuentas y subcuentas que existen en el sistema y como su saldo se ve afectado\r\n";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(128, 142);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(890, 32);
            lblTitulo.TabIndex = 10;
            lblTitulo.Text = "CATÁLOGO DE CUENTAS, TODO LO QUE SE PODRA INGRESAR EN EL SISTEMA\r\n";
            // 
            // cCodigo
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cCodigo.DefaultCellStyle = dataGridViewCellStyle4;
            cCodigo.FillWeight = 300F;
            cCodigo.HeaderText = "Código";
            cCodigo.Name = "cCodigo";
            cCodigo.Width = 150;
            // 
            // cNombre
            // 
            cNombre.HeaderText = "Nombre";
            cNombre.Name = "cNombre";
            cNombre.Width = 150;
            // 
            // cCuenta
            // 
            cCuenta.HeaderText = "Cuenta";
            cCuenta.Name = "cCuenta";
            cCuenta.Width = 150;
            // 
            // cTipoCuenta
            // 
            cTipoCuenta.HeaderText = "Tipo de cuenta";
            cTipoCuenta.Name = "cTipoCuenta";
            cTipoCuenta.Width = 150;
            // 
            // cDetalle
            // 
            cDetalle.HeaderText = "Detalle";
            cDetalle.Name = "cDetalle";
            cDetalle.Width = 150;
            // 
            // cSaldo
            // 
            cSaldo.HeaderText = "Saldo";
            cSaldo.Name = "cSaldo";
            cSaldo.Width = 150;
            // 
            // btnVolver
            // 
            btnVolver.BackgroundImageLayout = ImageLayout.None;
            btnVolver.FlatAppearance.BorderColor = Color.White;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ControlText;
            btnVolver.Location = new Point(1096, 12);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(134, 38);
            btnVolver.TabIndex = 15;
            btnVolver.Text = "Volver Atrás";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // FRM_PG46
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 727);
            Controls.Add(btnVolver);
            Controls.Add(pictureBox3);
            Controls.Add(label1);
            Controls.Add(dgvBitacora);
            Controls.Add(lblConsulte);
            Controls.Add(lblTitulo);
            Name = "FRM_PG46";
            Text = "FRM_PG46cs";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox3;
        private Label label1;
        private DataGridView dgvBitacora;
        private Label lblConsulte;
        private Label lblTitulo;
        private DataGridViewTextBoxColumn cCodigo;
        private DataGridViewTextBoxColumn cNombre;
        private DataGridViewTextBoxColumn cCuenta;
        private DataGridViewTextBoxColumn cTipoCuenta;
        private DataGridViewTextBoxColumn cDetalle;
        private DataGridViewTextBoxColumn cSaldo;
        private Button btnVolver;
    }
}