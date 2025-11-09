using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ProyectoNetshop.BD;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProyectoNetshop.formularios
{
    public partial class ReportesV : Form
    {
        private readonly int dni;
        private readonly string nombreCompleto;

        // Lista en memoria para filtrar sin volver a consultar la BD
        private List<(string nroFactura, DateTime fecha, string cliente, string tipoFactura, string producto, int cantidad, decimal precioUnitario, decimal totalPorProducto, string estado)> reporteOriginal;

        private readonly ErrorProvider errorProviderPrecios = new();

        public ReportesV(int p_dni, string p_nombreCompleto)
        {
            InitializeComponent();
            dni = p_dni;
            nombreCompleto = p_nombreCompleto;
            this.Load += ReportesV_Load;
        }

        private void ReportesV_Load(object sender, EventArgs e)
        {
            tbDniVendedorReporte.Text = dni.ToString();
            tbNombreVendedorReporte.Text = nombreCompleto;

            // Preparar grilla inicialmente oculta
            dgvReporteVentaVendedor.Visible = false;

            // Enlazar TextChanged para filtrado inmediato (si existen los TextBox)
            WireSearchTextBoxes();

            // Enlazar validaciones para los TextBox de precio
            WireValidationForPrecioTextBoxes();

            // Enlazar cambios de fecha para recargar filtro por rango
            WireDatePickers();

            // Cargar reporte al abrir el formulario
            CargarReporte();
        }

        // Busca y enlaza TextChanged en los TextBox de búsqueda para filtrar en cada carácter
        private void WireSearchTextBoxes()
        {
            string[] nombres = new[]
            {
                "tbBusquedaNroFProductoDF",
                "tbBusquedaNombreProductoDF",
                "tbBusquedaPrecioMinProductoDF",
                "tbBusquedaPrecioMaxProductoDF"
            };

            foreach (var name in nombres)
            {
                var encontrados = this.Controls.Find(name, true);
                if (encontrados.Length > 0 && encontrados[0] is TextBox tb)
                {
                    tb.TextChanged -= SearchTextBox_TextChanged;
                    tb.TextChanged += SearchTextBox_TextChanged;
                }
            }
        }

        private void SearchTextBox_TextChanged(object? sender, EventArgs e)
        {
            // Aplicar filtros en cada cambio de texto
            AplicarFiltrosReporteV();
        }

        private void CargarReporte()
        {
            // Si rango de fechas inválido, no mostrar nada y avisar
            DateTime desdeCheck = fechaDesdeVendedor.Value.Date;
            DateTime hastaCheck = fechaHastaVendedor.Value.Date;
            if (desdeCheck > hastaCheck)
            {
                // mostrar mensaje de error y vaciar la grilla
                MessageBox.Show("Fecha no válida. La fecha \"Desde\" no puede ser posterior a la fecha \"Hasta\".", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reporteOriginal = new List<(string, DateTime, string, string, string, int, decimal, decimal, string)>();
                dgvReporteVentaVendedor.Rows.Clear();
                dgvReporteVentaVendedor.Visible = false;
                // actualizar chart y total vacío
                ActualizarChartDesdeDatos(Enumerable.Empty<(string, DateTime, string, string, string, int, decimal, decimal, string)>());
                return;
            }

            dgvReporteVentaVendedor.Rows.Clear();
            dgvReporteVentaVendedor.Visible = true;
            dgvReporteVentaVendedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporteVentaVendedor.AllowUserToAddRows = false;
            dgvReporteVentaVendedor.ReadOnly = true;
            dgvReporteVentaVendedor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            DateTime desde = desdeCheck;
            DateTime hasta = hastaCheck.AddDays(1); // incluir todo el día "hasta"

            var culturaAR = new CultureInfo("es-AR");
            int filasAgregadas = 0;

            // Inicializar lista original
            reporteOriginal = new List<(string, DateTime, string, string, string, int, decimal, decimal, string)>();

            try
            {
                using var conexion = BaseDeDatos.obtenerConexion();
                using var cmd = conexion.CreateCommand();

                cmd.CommandText = @"
                SELECT
                    vc.nro_factura,
                    vc.fecha,
                    c.nombre + ' ' + c.apellido AS cliente,
                    vc.tipo_factura,
                    p.nombre AS producto,
                    vd.cantidad,
                    vd.precio_unitario,
                    (vd.cantidad * vd.precio_unitario) AS total_por_producto,
                    ev.descripcion AS estado
                FROM venta_detalle vd
                JOIN venta_cabecera vc ON vd.id_venta = vc.id_venta
                JOIN producto p ON vd.id_producto = p.id_producto
                JOIN cliente c ON vc.id_cliente = c.id_cliente
                JOIN estado_venta ev ON vc.id_estado = ev.id_estado
                JOIN usuario u ON vc.id_usuario = u.id_usuario
                WHERE u.dni = @dni
                    AND vc.nro_factura IS NOT NULL
                    AND vc.fecha >= @desde AND vc.fecha < @hasta
                ORDER BY vc.fecha, vc.nro_factura;";

                cmd.Parameters.AddWithValue("@dni", dni);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nroFactura = reader.IsDBNull(0) ? "" : reader.GetString(0);
                    DateTime fecha = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                    string cliente = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    string tipoFactura = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    string producto = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    int cantidad = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                    decimal precioUnitario = reader.IsDBNull(6) ? 0m : reader.GetDecimal(6);
                    decimal totalPorProducto = reader.IsDBNull(7) ? 0m : reader.GetDecimal(7);
                    string estado = reader.IsDBNull(8) ? "" : reader.GetString(8);

                    // Agregar a lista en memoria
                    reporteOriginal.Add((nroFactura, fecha, cliente, tipoFactura, producto, cantidad, precioUnitario, totalPorProducto, estado));
                    filasAgregadas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error técnico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvReporteVentaVendedor.Visible = false;
                return;
            }

            if (filasAgregadas == 0)
            {
                // No mostrar nada si no hay resultados
                dgvReporteVentaVendedor.Rows.Clear();
                dgvReporteVentaVendedor.Visible = false;
                // actualizar chart y total vacío
                ActualizarChartDesdeDatos(Enumerable.Empty<(string, DateTime, string, string, string, int, decimal, decimal, string)>());
                return;
            }

            // Mostrar inicialmente todos los registros (o aplicar filtros si ya hay texto)
            AplicarFiltrosReporteV();
        }

        // Aplica filtros a reporteOriginal y muestra en la grilla
        private void AplicarFiltrosReporteV()
        {
            if (reporteOriginal == null)
                return;

            // Si rango de fechas inválido, no mostrar nada (sin mensaje para no duplicar)
            DateTime desdeCheck = fechaDesdeVendedor.Value.Date;
            DateTime hastaCheck = fechaHastaVendedor.Value.Date;
            if (desdeCheck > hastaCheck)
            {
                dgvReporteVentaVendedor.Rows.Clear();
                dgvReporteVentaVendedor.Visible = false;
                // actualizar chart y total vacío
                ActualizarChartDesdeDatos(Enumerable.Empty<(string, DateTime, string, string, string, int, decimal, decimal, string)>());
                return;
            }

            string filtroNroFactura = GetTextBoxText("tbBusquedaNroFProductoDF").Trim().ToLower();
            string filtroProducto = GetTextBoxText("tbBusquedaNombreProductoDF").Trim().ToLower();
            string filtroMin = GetTextBoxText("tbBusquedaPrecioMinProductoDF").Trim();
            string filtroMax = GetTextBoxText("tbBusquedaPrecioMaxProductoDF").Trim();

            var cultura = new CultureInfo("es-AR");
            decimal? precioMin = null;
            decimal? precioMax = null;

            // Validar y parsear min
            if (!string.IsNullOrEmpty(filtroMin))
            {
                if (decimal.TryParse(filtroMin, NumberStyles.Number, cultura, out var minVal))
                    precioMin = minVal;
                else
                {
                    // mostrar error visual si existe textbox
                    SetErrorOnTextBox("tbBusquedaPrecioMinProductoDF", "Número inválido (ej: 1234,56)");
                }
            }
            else
            {
                ClearErrorOnTextBox("tbBusquedaPrecioMinProductoDF");
            }

            // Validar y parsear max
            if (!string.IsNullOrEmpty(filtroMax))
            {
                if (decimal.TryParse(filtroMax, NumberStyles.Number, cultura, out var maxVal))
                    precioMax = maxVal;
                else
                {
                    SetErrorOnTextBox("tbBusquedaPrecioMaxProductoDF", "Número inválido (ej: 1234,56)");
                }
            }
            else
            {
                ClearErrorOnTextBox("tbBusquedaPrecioMaxProductoDF");
            }

            // Filtrar sin aplicar rango de precio si el valor es inválido (se ignorará ese filtro)
            var filtrados = reporteOriginal.Where(r =>
                (string.IsNullOrEmpty(filtroNroFactura) || (r.nroFactura ?? "").ToLower().Contains(filtroNroFactura)) &&
                (string.IsNullOrEmpty(filtroProducto) || (r.producto ?? "").ToLower().Contains(filtroProducto)) &&
                (!precioMin.HasValue || r.precioUnitario >= precioMin.Value) &&
                (!precioMax.HasValue || r.precioUnitario <= precioMax.Value)
            ).ToList();

            MostrarEnGrilla(filtrados);
        }

        private void MostrarEnGrilla(IEnumerable<(string nroFactura, DateTime fecha, string cliente, string tipoFactura, string producto, int cantidad, decimal precioUnitario, decimal totalPorProducto, string estado)> datos)
        {
            dgvReporteVentaVendedor.Rows.Clear();
            var culturaAR = new CultureInfo("es-AR");

            foreach (var d in datos)
            {
                dgvReporteVentaVendedor.Rows.Add(
                    d.nroFactura,
                    d.fecha == DateTime.MinValue ? "" : d.fecha.ToString("dd/MM/yyyy"),
                    d.cliente,
                    d.tipoFactura,
                    d.cantidad,
                    d.precioUnitario.ToString("C", culturaAR),
                    d.totalPorProducto.ToString("C", culturaAR),
                    d.estado,
                    d.producto
                );
            }

            dgvReporteVentaVendedor.Visible = dgvReporteVentaVendedor.Rows.Count > 0;

            // Actualizar chart y total cada vez que cambiamos lo que se muestra en la grilla
            ActualizarChartDesdeDatos(datos);
        }

        // Actualiza chReporteVendedor usando los datos actualmente mostrados (agrega las cantidades por producto)
        private void ActualizarChartDesdeDatos(IEnumerable<(string nroFactura, DateTime fecha, string cliente, string tipoFactura, string producto, int cantidad, decimal precioUnitario, decimal totalPorProducto, string estado)> datos)
        {
            try
            {
                if (this.chReporteVendedor == null)
                    return;

                // Agrupar por producto y sumar cantidades
                var agrupado = datos
                    .Where(d => !string.IsNullOrEmpty(d.producto))
                    .GroupBy(d => d.producto)
                    .Select(g => new { Producto = g.Key, Cantidad = g.Sum(x => x.cantidad) })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(3) // <= solo los 3 con mayor cantidad
                    .ToList();

                chReporteVendedor.Series.Clear();
                chReporteVendedor.Titles.Clear();

                // Asegurar ChartArea
                if (chReporteVendedor.ChartAreas.Count == 0)
                    chReporteVendedor.ChartAreas.Add(new ChartArea("Default"));

                // Mostrar título
                chReporteVendedor.Titles.Add("Top 3 productos por cantidad");

                var serie = new Series("Cantidad")
                {
                    ChartType = SeriesChartType.Radar,
                    IsValueShownAsLabel = true
                };

                chReporteVendedor.Series.Add(serie);

                // Si no hay datos, ocultar chart
                if (agrupado.Count == 0)
                {
                    chReporteVendedor.Visible = false;
                    // actualizar total a 0
                    ActualizarTotalVendidoDesdeDatos(Enumerable.Empty<(string, DateTime, string, string, string, int, decimal, decimal, string)>());
                    return;
                }

                // Añadir puntos: máximo 3 productos
                foreach (var item in agrupado)
                {
                    int idx = serie.Points.AddXY(item.Producto, item.Cantidad);
                    var dataPoint = serie.Points[idx];
                    dataPoint.ToolTip = $"{item.Producto}: {item.Cantidad}";
                    dataPoint.Label = item.Cantidad.ToString();
                }

                // Ajustes estéticos
                var area = chReporteVendedor.ChartAreas[0];
                area.AxisX.LabelStyle.Angle = -30;
                //area.AxisX.Interval = 1;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = true;

                chReporteVendedor.Visible = true;

                // Actualizar total mostrado en label usando los datos originales visibles
                ActualizarTotalVendidoDesdeDatos(datos);
            }
            catch
            {
                // No hacer nada si el chart no está disponible o hay error visual
            }
        }

        // Actualiza lbTotalVendidoVendedor con la suma de totalPorProducto de los datos proporcionados
        private void ActualizarTotalVendidoDesdeDatos(IEnumerable<(string nroFactura, DateTime fecha, string cliente, string tipoFactura, string producto, int cantidad, decimal precioUnitario, decimal totalPorProducto, string estado)> datos)
        {
            try
            {
                decimal total = datos?.Sum(d => d.totalPorProducto) ?? 0m;
                var cultura = new CultureInfo("es-AR");
                if (this.lbTotalVendidoVendedor != null)
                {
                    lbTotalVendidoVendedor.Text = total.ToString("C", cultura);
                }
            }
            catch
            {
                // ignorar errores de UI
            }
        }

        private string GetTextBoxText(string name)
        {
            var encontrados = this.Controls.Find(name, true);
            if (encontrados.Length > 0 && encontrados[0] is TextBox tb)
                return tb.Text;
            return string.Empty;
        }

        // Validaciones para precios
        private void WireValidationForPrecioTextBoxes()
        {
            string[] nombres = new[]
            {
                "tbBusquedaPrecioMinProductoDF",
                "tbBusquedaPrecioMaxProductoDF"
            };

            foreach (var name in nombres)
            {
                var encontrados = this.Controls.Find(name, true);
                if (encontrados.Length > 0 && encontrados[0] is TextBox tb)
                {
                    tb.KeyPress -= PrecioTextBox_KeyPress;
                    tb.KeyPress += PrecioTextBox_KeyPress;

                    tb.TextChanged -= PrecioTextBox_OnTextChanged;
                    tb.TextChanged += PrecioTextBox_OnTextChanged;
                }
            }
        }

        // Conecta los DateTimePickers para recargar el reporte cuando cambie el rango
        private void WireDatePickers()
        {
            try
            {
                fechaDesdeVendedor.ValueChanged -= FechaVendedor_ValueChanged;
                fechaDesdeVendedor.ValueChanged += FechaVendedor_ValueChanged;

                fechaHastaVendedor.ValueChanged -= FechaVendedor_ValueChanged;
                fechaHastaVendedor.ValueChanged += FechaVendedor_ValueChanged;
            }
            catch
            {
                // Ignorar si los controles no existen
            }
        }

        // Maneja cambio en cualquiera de los DateTimePickers; valida y recarga
        private void FechaVendedor_ValueChanged(object? sender, EventArgs e)
        {
            DateTime desde = fechaDesdeVendedor.Value.Date;
            DateTime hasta = fechaHastaVendedor.Value.Date;

            if (desde > hasta)
            {
                // Si rango inválido, mostrar mensaje Y no mostrar datos
                MessageBox.Show("Fecha no válida. La fecha \"Desde\" no puede ser posterior a la fecha \"Hasta\".", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reporteOriginal = new List<(string, DateTime, string, string, string, int, decimal, decimal, string)>();
                dgvReporteVentaVendedor.Rows.Clear();
                dgvReporteVentaVendedor.Visible = false;
                ActualizarChartDesdeDatos(Enumerable.Empty<(string, DateTime, string, string, string, int, decimal, decimal, string)>());
                return;
            }

            // Volvemos a consultar la BD para obtener datos dentro del nuevo rango
            CargarReporte();
        }

        // Permite dígitos, control y un separador decimal; normaliza '.' a ','
        private void PrecioTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            char ch = e.KeyChar;

            if (char.IsControl(ch))
                return;

            if (char.IsDigit(ch))
                return;

            if (ch == '.' || ch == ',')
            {
                // Si ya tiene separador, bloquear
                if (tb.Text.Contains(',') || tb.Text.Contains('.'))
                {
                    e.Handled = true;
                    return;
                }

                e.KeyChar = ','; // normalizar
                return;
            }

            e.Handled = true; // bloquear cualquier otro carácter
        }

        // Cada vez que cambia el texto limpiamos el error si ahora es válido y aplicamos filtros
        private void PrecioTextBox_OnTextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            var cultura = new CultureInfo("es-AR");
            if (string.IsNullOrEmpty(tb.Text))
            {
                ClearErrorOnTextBox(tb.Name);
            }
            else if (decimal.TryParse(tb.Text, NumberStyles.Number, cultura, out var _))
            {
                ClearErrorOnTextBox(tb.Name);
            }
            else
            {
                SetErrorOnTextBox(tb.Name, "Número inválido (ej: 1234,56)");
            }

            // Al cambiar min/max aplicamos filtros inmediatamente
            AplicarFiltrosReporteV();
        }

        private void SetErrorOnTextBox(string name, string message)
        {
            var encontrados = this.Controls.Find(name, true);
            if (encontrados.Length > 0 && encontrados[0] is Control c)
                errorProviderPrecios.SetError(c, message);
        }

        private void ClearErrorOnTextBox(string name)
        {
            var encontrados = this.Controls.Find(name, true);
            if (encontrados.Length > 0 && encontrados[0] is Control c)
                errorProviderPrecios.SetError(c, string.Empty);
        }
    }
}
