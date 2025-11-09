namespace ProyectoNetshop.formularios
{
    partial class Reportes
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
            BGenerarTotalVentasGerente = new Button();
            panel1 = new Panel();
            label1 = new Label();
            label7 = new Label();
            fechaHastaGerente = new DateTimePicker();
            fechaDesdeGerente = new DateTimePicker();
            BGenerarVentasPorVendedorGerente = new Button();
            BGenerarProductosVendidosGerente = new Button();
            panel2 = new Panel();
            clbVendedoresReporteGerente = new CheckedListBox();
            label2 = new Label();
            dgvReporteGerente = new DataGridView();
            lTotalInfoReporteGerente = new Label();
            lbTotalVendidoReporteGerente = new Label();
            cbVentasFinalizadasReporteGerentes = new CheckBox();
            cbVentasCanceladasReporteGerentes = new CheckBox();
            tbBusquedaPrecioMaxReporteG = new TextBox();
            tbBusquedaPrecioMinReporteG = new TextBox();
            tbBusquedaNroFacturaReporteG = new TextBox();
            tbBusquedaClienteReporteG = new TextBox();
            tbBusquedaProductoReporteG = new TextBox();
            lBuscarPorReporteGerente = new Label();
            bGenerarPdfReporteGerente = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporteGerente).BeginInit();
            SuspendLayout();
            // 
            // BGenerarTotalVentasGerente
            // 
            BGenerarTotalVentasGerente.Cursor = Cursors.Hand;
            BGenerarTotalVentasGerente.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BGenerarTotalVentasGerente.Location = new Point(13, 560);
            BGenerarTotalVentasGerente.Name = "BGenerarTotalVentasGerente";
            BGenerarTotalVentasGerente.Size = new Size(226, 40);
            BGenerarTotalVentasGerente.TabIndex = 0;
            BGenerarTotalVentasGerente.Text = "Total de Ventas";
            BGenerarTotalVentasGerente.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 64);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(fechaHastaGerente);
            panel1.Controls.Add(fechaDesdeGerente);
            panel1.Controls.Add(BGenerarVentasPorVendedorGerente);
            panel1.Controls.Add(BGenerarProductosVendidosGerente);
            panel1.Controls.Add(BGenerarTotalVentasGerente);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(252, 600);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(103, 138);
            label1.Name = "label1";
            label1.Size = new Size(44, 22);
            label1.TabIndex = 20;
            label1.Text = "Desde";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ButtonFace;
            label7.Location = new Point(103, 226);
            label7.Name = "label7";
            label7.Size = new Size(42, 22);
            label7.TabIndex = 19;
            label7.Text = "Hasta";
            // 
            // fechaHastaGerente
            // 
            fechaHastaGerente.Location = new Point(35, 251);
            fechaHastaGerente.Name = "fechaHastaGerente";
            fechaHastaGerente.Size = new Size(183, 23);
            fechaHastaGerente.TabIndex = 18;
            // 
            // fechaDesdeGerente
            // 
            fechaDesdeGerente.Location = new Point(35, 163);
            fechaDesdeGerente.Name = "fechaDesdeGerente";
            fechaDesdeGerente.Size = new Size(183, 23);
            fechaDesdeGerente.TabIndex = 17;
            // 
            // BGenerarVentasPorVendedorGerente
            // 
            BGenerarVentasPorVendedorGerente.Cursor = Cursors.Hand;
            BGenerarVentasPorVendedorGerente.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BGenerarVentasPorVendedorGerente.Location = new Point(13, 481);
            BGenerarVentasPorVendedorGerente.Name = "BGenerarVentasPorVendedorGerente";
            BGenerarVentasPorVendedorGerente.Size = new Size(226, 40);
            BGenerarVentasPorVendedorGerente.TabIndex = 2;
            BGenerarVentasPorVendedorGerente.Text = "Ventas por Vendedor";
            BGenerarVentasPorVendedorGerente.UseVisualStyleBackColor = true;
            // 
            // BGenerarProductosVendidosGerente
            // 
            BGenerarProductosVendidosGerente.Cursor = Cursors.Hand;
            BGenerarProductosVendidosGerente.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BGenerarProductosVendidosGerente.Location = new Point(13, 520);
            BGenerarProductosVendidosGerente.Name = "BGenerarProductosVendidosGerente";
            BGenerarProductosVendidosGerente.Size = new Size(226, 40);
            BGenerarProductosVendidosGerente.TabIndex = 1;
            BGenerarProductosVendidosGerente.Text = "Productos Vendidos";
            BGenerarProductosVendidosGerente.UseVisualStyleBackColor = true;
            BGenerarProductosVendidosGerente.Click += BGenerarReporteGerente_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 0, 64);
            panel2.Controls.Add(clbVendedoresReporteGerente);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(252, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(798, 100);
            panel2.TabIndex = 2;
            // 
            // clbVendedoresReporteGerente
            // 
            clbVendedoresReporteGerente.FormattingEnabled = true;
            clbVendedoresReporteGerente.Location = new Point(135, 16);
            clbVendedoresReporteGerente.Margin = new Padding(3, 2, 3, 2);
            clbVendedoresReporteGerente.Name = "clbVendedoresReporteGerente";
            clbVendedoresReporteGerente.Size = new Size(340, 4);
            clbVendedoresReporteGerente.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(57, 16);
            label2.Name = "label2";
            label2.Size = new Size(64, 22);
            label2.TabIndex = 21;
            label2.Text = "Vendedor";
            // 
            // dgvReporteGerente
            // 
            dgvReporteGerente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporteGerente.Location = new Point(275, 150);
            dgvReporteGerente.Margin = new Padding(3, 2, 3, 2);
            dgvReporteGerente.Name = "dgvReporteGerente";
            dgvReporteGerente.RowHeadersWidth = 51;
            dgvReporteGerente.Size = new Size(754, 376);
            dgvReporteGerente.TabIndex = 26;
            dgvReporteGerente.Visible = false;
            dgvReporteGerente.CellContentClick += dgvReporteGerente_CellContentClick;
            // 
            // lTotalInfoReporteGerente
            // 
            lTotalInfoReporteGerente.AutoSize = true;
            lTotalInfoReporteGerente.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            lTotalInfoReporteGerente.ForeColor = SystemColors.ButtonFace;
            lTotalInfoReporteGerente.Location = new Point(811, 528);
            lTotalInfoReporteGerente.Name = "lTotalInfoReporteGerente";
            lTotalInfoReporteGerente.Size = new Size(64, 27);
            lTotalInfoReporteGerente.TabIndex = 27;
            lTotalInfoReporteGerente.Text = "TOTAL:";
            lTotalInfoReporteGerente.Visible = false;
            // 
            // lbTotalVendidoReporteGerente
            // 
            lbTotalVendidoReporteGerente.AutoSize = true;
            lbTotalVendidoReporteGerente.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            lbTotalVendidoReporteGerente.ForeColor = SystemColors.ButtonFace;
            lbTotalVendidoReporteGerente.Location = new Point(887, 528);
            lbTotalVendidoReporteGerente.Name = "lbTotalVendidoReporteGerente";
            lbTotalVendidoReporteGerente.Size = new Size(51, 27);
            lbTotalVendidoReporteGerente.TabIndex = 28;
            lbTotalVendidoReporteGerente.Text = "$0,00";
            lbTotalVendidoReporteGerente.Visible = false;
            // 
            // cbVentasFinalizadasReporteGerentes
            // 
            cbVentasFinalizadasReporteGerentes.AutoSize = true;
            cbVentasFinalizadasReporteGerentes.Checked = true;
            cbVentasFinalizadasReporteGerentes.CheckState = CheckState.Checked;
            cbVentasFinalizadasReporteGerentes.Cursor = Cursors.Hand;
            cbVentasFinalizadasReporteGerentes.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            cbVentasFinalizadasReporteGerentes.ForeColor = SystemColors.ButtonFace;
            cbVentasFinalizadasReporteGerentes.Location = new Point(275, 527);
            cbVentasFinalizadasReporteGerentes.Margin = new Padding(3, 2, 3, 2);
            cbVentasFinalizadasReporteGerentes.Name = "cbVentasFinalizadasReporteGerentes";
            cbVentasFinalizadasReporteGerentes.Size = new Size(81, 26);
            cbVentasFinalizadasReporteGerentes.TabIndex = 29;
            cbVentasFinalizadasReporteGerentes.Text = "Vendidos";
            cbVentasFinalizadasReporteGerentes.UseVisualStyleBackColor = true;
            cbVentasFinalizadasReporteGerentes.Visible = false;
            // 
            // cbVentasCanceladasReporteGerentes
            // 
            cbVentasCanceladasReporteGerentes.AutoSize = true;
            cbVentasCanceladasReporteGerentes.Cursor = Cursors.Hand;
            cbVentasCanceladasReporteGerentes.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            cbVentasCanceladasReporteGerentes.ForeColor = SystemColors.ButtonFace;
            cbVentasCanceladasReporteGerentes.Location = new Point(377, 527);
            cbVentasCanceladasReporteGerentes.Margin = new Padding(3, 2, 3, 2);
            cbVentasCanceladasReporteGerentes.Name = "cbVentasCanceladasReporteGerentes";
            cbVentasCanceladasReporteGerentes.Size = new Size(91, 26);
            cbVentasCanceladasReporteGerentes.TabIndex = 30;
            cbVentasCanceladasReporteGerentes.Text = "Cancelados";
            cbVentasCanceladasReporteGerentes.UseVisualStyleBackColor = true;
            cbVentasCanceladasReporteGerentes.Visible = false;
            cbVentasCanceladasReporteGerentes.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // tbBusquedaPrecioMaxReporteG
            // 
            tbBusquedaPrecioMaxReporteG.Location = new Point(898, 125);
            tbBusquedaPrecioMaxReporteG.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaPrecioMaxReporteG.Name = "tbBusquedaPrecioMaxReporteG";
            tbBusquedaPrecioMaxReporteG.PlaceholderText = "Precio maximo";
            tbBusquedaPrecioMaxReporteG.Size = new Size(132, 23);
            tbBusquedaPrecioMaxReporteG.TabIndex = 31;
            tbBusquedaPrecioMaxReporteG.Visible = false;
            // 
            // tbBusquedaPrecioMinReporteG
            // 
            tbBusquedaPrecioMinReporteG.Location = new Point(898, 100);
            tbBusquedaPrecioMinReporteG.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaPrecioMinReporteG.Name = "tbBusquedaPrecioMinReporteG";
            tbBusquedaPrecioMinReporteG.PlaceholderText = "Precio minimo";
            tbBusquedaPrecioMinReporteG.Size = new Size(132, 23);
            tbBusquedaPrecioMinReporteG.TabIndex = 32;
            tbBusquedaPrecioMinReporteG.Visible = false;
            // 
            // tbBusquedaNroFacturaReporteG
            // 
            tbBusquedaNroFacturaReporteG.Location = new Point(751, 125);
            tbBusquedaNroFacturaReporteG.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaNroFacturaReporteG.Name = "tbBusquedaNroFacturaReporteG";
            tbBusquedaNroFacturaReporteG.PlaceholderText = "Numero de factura";
            tbBusquedaNroFacturaReporteG.Size = new Size(132, 23);
            tbBusquedaNroFacturaReporteG.TabIndex = 33;
            tbBusquedaNroFacturaReporteG.Visible = false;
            // 
            // tbBusquedaClienteReporteG
            // 
            tbBusquedaClienteReporteG.Location = new Point(751, 100);
            tbBusquedaClienteReporteG.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaClienteReporteG.Name = "tbBusquedaClienteReporteG";
            tbBusquedaClienteReporteG.PlaceholderText = "Nombre del cliente";
            tbBusquedaClienteReporteG.Size = new Size(132, 23);
            tbBusquedaClienteReporteG.TabIndex = 34;
            tbBusquedaClienteReporteG.Visible = false;
            // 
            // tbBusquedaProductoReporteG
            // 
            tbBusquedaProductoReporteG.Location = new Point(594, 125);
            tbBusquedaProductoReporteG.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaProductoReporteG.Name = "tbBusquedaProductoReporteG";
            tbBusquedaProductoReporteG.PlaceholderText = "Nombre del producto";
            tbBusquedaProductoReporteG.Size = new Size(140, 23);
            tbBusquedaProductoReporteG.TabIndex = 35;
            tbBusquedaProductoReporteG.Visible = false;
            // 
            // lBuscarPorReporteGerente
            // 
            lBuscarPorReporteGerente.AutoSize = true;
            lBuscarPorReporteGerente.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            lBuscarPorReporteGerente.ForeColor = SystemColors.ButtonFace;
            lBuscarPorReporteGerente.Location = new Point(594, 100);
            lBuscarPorReporteGerente.Name = "lBuscarPorReporteGerente";
            lBuscarPorReporteGerente.Size = new Size(73, 22);
            lBuscarPorReporteGerente.TabIndex = 36;
            lBuscarPorReporteGerente.Text = "Buscar por:";
            lBuscarPorReporteGerente.Visible = false;
            // 
            // bGenerarPdfReporteGerente
            // 
            bGenerarPdfReporteGerente.Cursor = Cursors.Hand;
            bGenerarPdfReporteGerente.Font = new Font("Dubai", 12F, FontStyle.Bold | FontStyle.Italic);
            bGenerarPdfReporteGerente.ForeColor = SystemColors.ActiveCaptionText;
            bGenerarPdfReporteGerente.Location = new Point(275, 112);
            bGenerarPdfReporteGerente.Margin = new Padding(3, 2, 3, 2);
            bGenerarPdfReporteGerente.Name = "bGenerarPdfReporteGerente";
            bGenerarPdfReporteGerente.Size = new Size(119, 34);
            bGenerarPdfReporteGerente.TabIndex = 37;
            bGenerarPdfReporteGerente.Text = "Generar PDF";
            bGenerarPdfReporteGerente.UseVisualStyleBackColor = true;
            bGenerarPdfReporteGerente.Visible = false;
            // 
            // Reportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(1050, 600);
            Controls.Add(bGenerarPdfReporteGerente);
            Controls.Add(lBuscarPorReporteGerente);
            Controls.Add(tbBusquedaProductoReporteG);
            Controls.Add(tbBusquedaClienteReporteG);
            Controls.Add(tbBusquedaNroFacturaReporteG);
            Controls.Add(tbBusquedaPrecioMinReporteG);
            Controls.Add(tbBusquedaPrecioMaxReporteG);
            Controls.Add(cbVentasCanceladasReporteGerentes);
            Controls.Add(cbVentasFinalizadasReporteGerentes);
            Controls.Add(lbTotalVendidoReporteGerente);
            Controls.Add(lTotalInfoReporteGerente);
            Controls.Add(dgvReporteGerente);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Reportes";
            Text = "Reportes";
            Load += Reportes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporteGerente).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BGenerarTotalVentasGerente;
        private Panel panel1;
        private Button BGenerarVentasPorVendedorGerente;
        private Button BGenerarProductosVendidosGerente;
        private DateTimePicker fechaDesdeGerente;
        private DateTimePicker fechaHastaGerente;
        private Label label1;
        private Label label7;
        private Panel panel2;
        private Label label2;
        private CheckedListBox clbVendedoresReporteGerente;
        private DataGridView dgvReporteGerente;
        private Label lTotalInfoReporteGerente;
        private Label lbTotalVendidoReporteGerente;
        private CheckBox cbVentasFinalizadasReporteGerentes;
        private CheckBox cbVentasCanceladasReporteGerentes;
        private TextBox tbBusquedaPrecioMaxReporteG;
        private TextBox tbBusquedaPrecioMinReporteG;
        private TextBox tbBusquedaNroFacturaReporteG;
        private TextBox tbBusquedaClienteReporteG;
        private TextBox tbBusquedaProductoReporteG;
        private Label lBuscarPorReporteGerente;
        private Button bGenerarPdfReporteGerente;
    }
}