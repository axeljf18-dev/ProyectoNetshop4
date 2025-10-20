namespace vistaDeProyectoC
{
    partial class FReporteUsuario
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            LReporteDeUsuarios = new Label();
            BGenerarReporteUsuario = new Button();
            panel1 = new Panel();
            tbVendedores = new TextBox();
            tbGerentes = new TextBox();
            tbAdministradores = new TextBox();
            lbVendedores = new Label();
            lbGerentes = new Label();
            lbAdministradores = new Label();
            lbBuscadorUsuario = new Label();
            tbBusquedaNombreUsuario = new TextBox();
            tbBusquedaDniUsuario = new TextBox();
            cbInactivosUsuarios = new CheckBox();
            cbActivosUsuarios = new CheckBox();
            dgvUsuarios = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // LReporteDeUsuarios
            // 
            LReporteDeUsuarios.AutoSize = true;
            LReporteDeUsuarios.Font = new Font("Dubai", 20F, FontStyle.Bold | FontStyle.Italic);
            LReporteDeUsuarios.ForeColor = SystemColors.ButtonFace;
            LReporteDeUsuarios.Location = new Point(374, 35);
            LReporteDeUsuarios.Name = "LReporteDeUsuarios";
            LReporteDeUsuarios.Size = new Size(255, 45);
            LReporteDeUsuarios.TabIndex = 0;
            LReporteDeUsuarios.Text = "Reporte de Usuarios";
            // 
            // BGenerarReporteUsuario
            // 
            BGenerarReporteUsuario.Cursor = Cursors.Hand;
            BGenerarReporteUsuario.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            BGenerarReporteUsuario.Location = new Point(432, 101);
            BGenerarReporteUsuario.Name = "BGenerarReporteUsuario";
            BGenerarReporteUsuario.Size = new Size(175, 38);
            BGenerarReporteUsuario.TabIndex = 1;
            BGenerarReporteUsuario.Text = "Generar Reporte";
            BGenerarReporteUsuario.UseVisualStyleBackColor = true;
            BGenerarReporteUsuario.Click += BGenerarReporteUsuario_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 64);
            panel1.Controls.Add(tbVendedores);
            panel1.Controls.Add(tbGerentes);
            panel1.Controls.Add(tbAdministradores);
            panel1.Controls.Add(lbVendedores);
            panel1.Controls.Add(lbGerentes);
            panel1.Controls.Add(lbAdministradores);
            panel1.Controls.Add(lbBuscadorUsuario);
            panel1.Controls.Add(tbBusquedaNombreUsuario);
            panel1.Controls.Add(tbBusquedaDniUsuario);
            panel1.Controls.Add(cbInactivosUsuarios);
            panel1.Controls.Add(cbActivosUsuarios);
            panel1.Controls.Add(dgvUsuarios);
            panel1.Controls.Add(LReporteDeUsuarios);
            panel1.Controls.Add(BGenerarReporteUsuario);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1050, 600);
            panel1.TabIndex = 3;
            // 
            // tbVendedores
            // 
            tbVendedores.Location = new Point(256, 548);
            tbVendedores.Name = "tbVendedores";
            tbVendedores.ReadOnly = true;
            tbVendedores.Size = new Size(100, 23);
            tbVendedores.TabIndex = 45;
            // 
            // tbGerentes
            // 
            tbGerentes.Location = new Point(256, 503);
            tbGerentes.Name = "tbGerentes";
            tbGerentes.ReadOnly = true;
            tbGerentes.Size = new Size(100, 23);
            tbGerentes.TabIndex = 44;
            // 
            // tbAdministradores
            // 
            tbAdministradores.Location = new Point(256, 456);
            tbAdministradores.Name = "tbAdministradores";
            tbAdministradores.ReadOnly = true;
            tbAdministradores.Size = new Size(100, 23);
            tbAdministradores.TabIndex = 43;
            // 
            // lbVendedores
            // 
            lbVendedores.AutoSize = true;
            lbVendedores.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            lbVendedores.ForeColor = SystemColors.ButtonFace;
            lbVendedores.Location = new Point(26, 544);
            lbVendedores.Name = "lbVendedores";
            lbVendedores.Size = new Size(193, 27);
            lbVendedores.TabIndex = 42;
            lbVendedores.Text = "Total vendedores activos: ";
            // 
            // lbGerentes
            // 
            lbGerentes.AutoSize = true;
            lbGerentes.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            lbGerentes.ForeColor = SystemColors.ButtonFace;
            lbGerentes.Location = new Point(26, 499);
            lbGerentes.Name = "lbGerentes";
            lbGerentes.Size = new Size(172, 27);
            lbGerentes.TabIndex = 41;
            lbGerentes.Text = "Total gerentes activos: ";
            // 
            // lbAdministradores
            // 
            lbAdministradores.AutoSize = true;
            lbAdministradores.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            lbAdministradores.ForeColor = SystemColors.ButtonFace;
            lbAdministradores.Location = new Point(23, 456);
            lbAdministradores.Name = "lbAdministradores";
            lbAdministradores.Size = new Size(225, 27);
            lbAdministradores.TabIndex = 40;
            lbAdministradores.Text = "Total administradores activos: ";
            // 
            // lbBuscadorUsuario
            // 
            lbBuscadorUsuario.AutoSize = true;
            lbBuscadorUsuario.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            lbBuscadorUsuario.ForeColor = SystemColors.ButtonFace;
            lbBuscadorUsuario.Location = new Point(706, 147);
            lbBuscadorUsuario.Name = "lbBuscadorUsuario";
            lbBuscadorUsuario.Size = new Size(73, 22);
            lbBuscadorUsuario.TabIndex = 39;
            lbBuscadorUsuario.Text = "Buscar por:";
            // 
            // tbBusquedaNombreUsuario
            // 
            tbBusquedaNombreUsuario.Location = new Point(767, 171);
            tbBusquedaNombreUsuario.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaNombreUsuario.Multiline = true;
            tbBusquedaNombreUsuario.Name = "tbBusquedaNombreUsuario";
            tbBusquedaNombreUsuario.PlaceholderText = "Busqueda por Nombre";
            tbBusquedaNombreUsuario.Size = new Size(183, 21);
            tbBusquedaNombreUsuario.TabIndex = 38;
            // 
            // tbBusquedaDniUsuario
            // 
            tbBusquedaDniUsuario.Location = new Point(543, 171);
            tbBusquedaDniUsuario.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaDniUsuario.Multiline = true;
            tbBusquedaDniUsuario.Name = "tbBusquedaDniUsuario";
            tbBusquedaDniUsuario.PlaceholderText = "Busqueda por DNI";
            tbBusquedaDniUsuario.Size = new Size(183, 21);
            tbBusquedaDniUsuario.TabIndex = 37;
            // 
            // cbInactivosUsuarios
            // 
            cbInactivosUsuarios.AutoSize = true;
            cbInactivosUsuarios.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            cbInactivosUsuarios.ForeColor = SystemColors.ButtonFace;
            cbInactivosUsuarios.Location = new Point(119, 171);
            cbInactivosUsuarios.Name = "cbInactivosUsuarios";
            cbInactivosUsuarios.Size = new Size(79, 26);
            cbInactivosUsuarios.TabIndex = 36;
            cbInactivosUsuarios.Text = "Inactivos";
            cbInactivosUsuarios.UseVisualStyleBackColor = true;
            // 
            // cbActivosUsuarios
            // 
            cbActivosUsuarios.AutoSize = true;
            cbActivosUsuarios.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            cbActivosUsuarios.ForeColor = SystemColors.ButtonFace;
            cbActivosUsuarios.Location = new Point(23, 171);
            cbActivosUsuarios.Name = "cbActivosUsuarios";
            cbActivosUsuarios.Size = new Size(71, 26);
            cbActivosUsuarios.TabIndex = 35;
            cbActivosUsuarios.Text = "Activos";
            cbActivosUsuarios.UseVisualStyleBackColor = true;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsuarios.BackgroundColor = Color.SlateGray;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvUsuarios.Location = new Point(23, 198);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(1005, 255);
            dgvUsuarios.TabIndex = 34;
            // 
            // FReporteUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(1050, 600);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FReporteUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reporte Usuario";
            Load += FReporteUsuario_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LReporteDeUsuarios;
        private System.Windows.Forms.Button BGenerarReporteUsuario;
        private Panel panel1;
        private Label lbBuscadorUsuario;
        private TextBox tbBusquedaNombreUsuario;
        private TextBox tbBusquedaDniUsuario;
        private CheckBox cbInactivosUsuarios;
        private CheckBox cbActivosUsuarios;
        private DataGridView dgvUsuarios;
        private Label lbAdministradores;
        private Label lbVendedores;
        private Label lbGerentes;
        private TextBox tbVendedores;
        private TextBox tbGerentes;
        private TextBox tbAdministradores;
    }
}