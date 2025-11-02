namespace ProyectoNetshop.formularios
{
    partial class ReportesV
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
            panel2 = new Panel();
            tbBusquedaPrecioMinProductoDF = new TextBox();
            tbBusquedaNroFProductoDF = new TextBox();
            tbNombreVendedorReporte = new TextBox();
            tbBusquedaPrecioMaxProductoDF = new TextBox();
            label2 = new Label();
            tbBusquedaNombreProductoDF = new TextBox();
            tbDniVendedorReporte = new TextBox();
            label3 = new Label();
            panel1 = new Panel();
            label1 = new Label();
            label7 = new Label();
            fechaHastaVendedor = new DateTimePicker();
            fechaDesdeVendedor = new DateTimePicker();
            dgvReporteVentaVendedor = new DataGridView();
            nroFactura = new DataGridViewTextBoxColumn();
            fechaVenta = new DataGridViewTextBoxColumn();
            clienteVenta = new DataGridViewTextBoxColumn();
            tipoFactura = new DataGridViewTextBoxColumn();
            cantidad = new DataGridViewTextBoxColumn();
            precioUnitario = new DataGridViewTextBoxColumn();
            totalVenta = new DataGridViewTextBoxColumn();
            estado = new DataGridViewTextBoxColumn();
            producto = new DataGridViewTextBoxColumn();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporteVentaVendedor).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 0, 64);
            panel2.Controls.Add(tbBusquedaPrecioMinProductoDF);
            panel2.Controls.Add(tbBusquedaNroFProductoDF);
            panel2.Controls.Add(tbNombreVendedorReporte);
            panel2.Controls.Add(tbBusquedaPrecioMaxProductoDF);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(tbBusquedaNombreProductoDF);
            panel2.Controls.Add(tbDniVendedorReporte);
            panel2.Controls.Add(label3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(252, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(827, 149);
            panel2.TabIndex = 4;
            // 
            // tbBusquedaPrecioMinProductoDF
            // 
            tbBusquedaPrecioMinProductoDF.Location = new Point(512, 76);
            tbBusquedaPrecioMinProductoDF.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaPrecioMinProductoDF.Name = "tbBusquedaPrecioMinProductoDF";
            tbBusquedaPrecioMinProductoDF.PlaceholderText = "Precio minimo";
            tbBusquedaPrecioMinProductoDF.Size = new Size(111, 23);
            tbBusquedaPrecioMinProductoDF.TabIndex = 15;
            // 
            // tbBusquedaNroFProductoDF
            // 
            tbBusquedaNroFProductoDF.Location = new Point(128, 103);
            tbBusquedaNroFProductoDF.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaNroFProductoDF.Name = "tbBusquedaNroFProductoDF";
            tbBusquedaNroFProductoDF.PlaceholderText = "Numero de factura";
            tbBusquedaNroFProductoDF.Size = new Size(111, 23);
            tbBusquedaNroFProductoDF.TabIndex = 14;
            // 
            // tbNombreVendedorReporte
            // 
            tbNombreVendedorReporte.Location = new Point(266, 38);
            tbNombreVendedorReporte.Name = "tbNombreVendedorReporte";
            tbNombreVendedorReporte.ReadOnly = true;
            tbNombreVendedorReporte.Size = new Size(221, 23);
            tbNombreVendedorReporte.TabIndex = 22;
            tbNombreVendedorReporte.Text = "Nombre completo del vendedor";
            // 
            // tbBusquedaPrecioMaxProductoDF
            // 
            tbBusquedaPrecioMaxProductoDF.Location = new Point(512, 103);
            tbBusquedaPrecioMaxProductoDF.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaPrecioMaxProductoDF.Name = "tbBusquedaPrecioMaxProductoDF";
            tbBusquedaPrecioMaxProductoDF.PlaceholderText = "Precio maximo";
            tbBusquedaPrecioMaxProductoDF.Size = new Size(111, 23);
            tbBusquedaPrecioMaxProductoDF.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(57, 39);
            label2.Name = "label2";
            label2.Size = new Size(64, 22);
            label2.TabIndex = 21;
            label2.Text = "Vendedor";
            // 
            // tbBusquedaNombreProductoDF
            // 
            tbBusquedaNombreProductoDF.Location = new Point(266, 103);
            tbBusquedaNombreProductoDF.Margin = new Padding(3, 2, 3, 2);
            tbBusquedaNombreProductoDF.Name = "tbBusquedaNombreProductoDF";
            tbBusquedaNombreProductoDF.PlaceholderText = "Nombre del producto";
            tbBusquedaNombreProductoDF.Size = new Size(217, 23);
            tbBusquedaNombreProductoDF.TabIndex = 12;
            // 
            // tbDniVendedorReporte
            // 
            tbDniVendedorReporte.Location = new Point(128, 38);
            tbDniVendedorReporte.Name = "tbDniVendedorReporte";
            tbDniVendedorReporte.ReadOnly = true;
            tbDniVendedorReporte.Size = new Size(111, 23);
            tbDniVendedorReporte.TabIndex = 2;
            tbDniVendedorReporte.Text = "DNI del Vendedor";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Dubai", 9.749999F, FontStyle.Bold | FontStyle.Italic);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(48, 103);
            label3.Name = "label3";
            label3.Size = new Size(73, 22);
            label3.TabIndex = 11;
            label3.Text = "Buscar por:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 64);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(fechaHastaVendedor);
            panel1.Controls.Add(fechaDesdeVendedor);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(252, 582);
            panel1.TabIndex = 3;
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
            // fechaHastaVendedor
            // 
            fechaHastaVendedor.Location = new Point(35, 251);
            fechaHastaVendedor.Name = "fechaHastaVendedor";
            fechaHastaVendedor.Size = new Size(183, 23);
            fechaHastaVendedor.TabIndex = 18;
            // 
            // fechaDesdeVendedor
            // 
            fechaDesdeVendedor.Location = new Point(35, 163);
            fechaDesdeVendedor.Name = "fechaDesdeVendedor";
            fechaDesdeVendedor.Size = new Size(183, 23);
            fechaDesdeVendedor.TabIndex = 17;
            // 
            // dgvReporteVentaVendedor
            // 
            dgvReporteVentaVendedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporteVentaVendedor.Columns.AddRange(new DataGridViewColumn[] { nroFactura, fechaVenta, clienteVenta, tipoFactura, cantidad, precioUnitario, totalVenta, estado, producto });
            dgvReporteVentaVendedor.Dock = DockStyle.Fill;
            dgvReporteVentaVendedor.Location = new Point(252, 149);
            dgvReporteVentaVendedor.Margin = new Padding(3, 2, 3, 2);
            dgvReporteVentaVendedor.Name = "dgvReporteVentaVendedor";
            dgvReporteVentaVendedor.RowHeadersWidth = 51;
            dgvReporteVentaVendedor.Size = new Size(827, 433);
            dgvReporteVentaVendedor.TabIndex = 5;
            // 
            // nroFactura
            // 
            nroFactura.HeaderText = "Nro de Factura";
            nroFactura.MinimumWidth = 6;
            nroFactura.Name = "nroFactura";
            nroFactura.Width = 125;
            // 
            // fechaVenta
            // 
            fechaVenta.HeaderText = "Fecha de Venta";
            fechaVenta.MinimumWidth = 6;
            fechaVenta.Name = "fechaVenta";
            fechaVenta.Width = 125;
            // 
            // clienteVenta
            // 
            clienteVenta.HeaderText = "Cliente";
            clienteVenta.MinimumWidth = 6;
            clienteVenta.Name = "clienteVenta";
            clienteVenta.Width = 125;
            // 
            // tipoFactura
            // 
            tipoFactura.HeaderText = "Tipo de Factura";
            tipoFactura.MinimumWidth = 6;
            tipoFactura.Name = "tipoFactura";
            tipoFactura.Width = 125;
            // 
            // cantidad
            // 
            cantidad.HeaderText = "Cantidad";
            cantidad.Name = "cantidad";
            // 
            // precioUnitario
            // 
            precioUnitario.HeaderText = "Precio Unitario";
            precioUnitario.Name = "precioUnitario";
            // 
            // totalVenta
            // 
            totalVenta.HeaderText = "Total";
            totalVenta.MinimumWidth = 6;
            totalVenta.Name = "totalVenta";
            totalVenta.Width = 125;
            // 
            // estado
            // 
            estado.HeaderText = "Estado";
            estado.Name = "estado";
            // 
            // producto
            // 
            producto.HeaderText = "Producto";
            producto.Name = "producto";
            // 
            // ReportesV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(1079, 582);
            Controls.Add(dgvReporteVentaVendedor);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "ReportesV";
            Text = "Reporte del Vendedor";
            Load += ReportesV_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporteVentaVendedor).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private TextBox tbNombreVendedorReporte;
        private Label label2;
        private TextBox tbDniVendedorReporte;
        private Panel panel1;
        private Label label1;
        private Label label7;
        private DateTimePicker fechaHastaVendedor;
        private DateTimePicker fechaDesdeVendedor;
        private DataGridView dgvReporteVentaVendedor;
        private DataGridViewTextBoxColumn nroFactura;
        private DataGridViewTextBoxColumn fechaVenta;
        private DataGridViewTextBoxColumn clienteVenta;
        private DataGridViewTextBoxColumn tipoFactura;
        private DataGridViewTextBoxColumn cantidad;
        private DataGridViewTextBoxColumn precioUnitario;
        private DataGridViewTextBoxColumn totalVenta;
        private DataGridViewTextBoxColumn estado;
        private DataGridViewTextBoxColumn producto;
        private TextBox tbBusquedaPrecioMinProductoDF;
        private TextBox tbBusquedaNroFProductoDF;
        private TextBox tbBusquedaPrecioMaxProductoDF;
        private TextBox tbBusquedaNombreProductoDF;
        private Label label3;
    }
}