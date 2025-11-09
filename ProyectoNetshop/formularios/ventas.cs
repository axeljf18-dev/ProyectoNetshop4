using iTextSharp.text;
using iTextSharp.text.pdf;
using ProyectoNetshop.Cruds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoNetshop.formularios
{
    //ibBotonBuscarClienteVenta
    //ibBotonBuscarProductoVenta
    public partial class ventas : Form
    {
        //Vendedor
        private readonly int vendedorDni;
        private readonly string vendedorNombreCompleto;
        private readonly int vendedorId;


        //private readonly int clienteDni;
        //private readonly string clienteNombreCompleto;
        //private readonly int clienteId;
        //Cliente
        private ListBox lbClientesSugeridos = new ListBox();
        private Dictionary<string, Cliente_model> clientesMap = new Dictionary<string, Cliente_model>();

        //Producto
        private ListBox lbProductosSugeridos = new ListBox();
        private Dictionary<string, Producto_model> productosMap = new Dictionary<string, Producto_model>();
        private bool bloqueandoFiltroProducto = false;

        //Carrito
        //private int? indiceSeleccionadoCarrito = null;

        public ventas(int p_dni, string p_nombre, int p_vendedor_id //Vendedor
                                                                    //int p_dni_cliente, string p_nombre_cliente, int p_id_cliente //Cliente
                     )
        {
            InitializeComponent();

            //Vendedor
            vendedorDni = p_dni;
            vendedorNombreCompleto = p_nombre;
            vendedorId = p_vendedor_id;

            ////Cliente
            //clienteDni = p_dni_cliente;
            //clienteNombreCompleto = p_nombre_cliente;
            //clienteId = p_id_cliente;

            // Eventos para autocompletar cliente y ocultar lista
            tbNombreClienteVenta.TextChanged += tbClienteFiltro_TextChanged;
            tbDniClienteVenta.TextChanged += tbClienteFiltro_TextChanged;
            tbEmailClienteVenta.TextChanged += tbClienteFiltro_TextChanged;
            tbNombreClienteVenta.Leave += OcultarListaClientes;
            tbDniClienteVenta.Leave += OcultarListaClientes;
            tbEmailClienteVenta.Leave += OcultarListaClientes;

            //Ocular lista del filtro
            this.MouseDown += OcultarListaClientesPorClickGlobal;

            //NUEVO ESTO LO AGREGUE
            cbVentaProductoVendidos.CheckedChanged += cbVentaProductoVendidos_CheckedChanged;
            cbVentaProductoCancelados.CheckedChanged += cbVentaProductoCancelados_CheckedChanged;
            cbVentaProductoPendientes.CheckedChanged += cbVentaProductoPendientes_CheckedChanged;
            cbVentaProductoPendientes.Checked = true;

            //Producto
            tbIdProductoVenta.TextChanged += tbProductoFiltro_TextChanged;
            tbNombreProductoVenta.TextChanged += tbProductoFiltro_TextChanged;
            tbIdProductoVenta.Leave += OcultarListaProductos;
            tbNombreProductoVenta.Leave += OcultarListaProductos;
            lbProductosSugeridos.Click += LbProductosSugeridos_Click;

            this.Load += ventas_Load;

            tbDniClienteVenta.KeyPress += TextBox_OnlyDigits_KeyPress;
            tbNombreClienteVenta.KeyPress += TextBox_OnlyLetters_KeyPress;
            //ibBotonBuscarClienteVenta.Click += IbBotonBuscarClienteVenta_Click;

            tbNombreProductoVenta.KeyPress += TextBox_OnlyLetters_KeyPress;
            //tbDescripcionProductoVenta.KeyPress += TextBox_OnlyLetters_KeyPress;
            tbStockProductoVenta.KeyPress += TextBox_OnlyDigits_KeyPress;
            tbPrecioVtaProductoVenta.KeyPress += TextBox_OnlyDigits_KeyPress;
            tbCantidadProductoVenta.KeyPress += TextBox_OnlyDigits_KeyPress;
            tbCantidadProductoVenta.TextChanged += ValidarCantidadVsStock;

            //ibBotonBuscarProductoVenta.Click += IbBotonBuscarProductoVenta_Click;
            ibBotonAgregarProductoVenta.Click += IbBotonAgregarProductoVenta_Click;

            ibBotonAgregarProductoVenta.Enabled = false;

            dtpFechaVenta.ShowCheckBox = true;

            ibBotonGuardarVenta.Click -= ibBotonGuardarVenta_Click;
            ibBotonGuardarVenta.Click += ibBotonGuardarVenta_Click;
            ibBotonBorrarVenta.Click += ibBotonBorrarVenta_Click;

            dgvVentas.CellClick += dgvVentas_CellClick;
            dgvVentas.CellMouseMove += dgvVentas_CellMouseMove;
        }

        //private void tbClienteFiltro_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox campo = sender as TextBox;
        //    if (campo == null) return;

        //    string nombre = tbNombreClienteVenta.Text.Trim();
        //    string dni = tbDniClienteVenta.Text.Trim();
        //    string email = tbEmailClienteVenta.Text.Trim();

        //    var clientes = Cliente_controller.BuscarClientes(
        //        string.IsNullOrWhiteSpace(nombre) ? "" : nombre,
        //        string.IsNullOrWhiteSpace(dni) ? "" : dni,
        //        string.IsNullOrWhiteSpace(email) ? "" : email
        //    );

        //    lbClientesSugeridos.Items.Clear();

        //    if (campo == tbNombreClienteVenta)
        //    {
        //        foreach (var c in clientes)
        //            lbClientesSugeridos.Items.Add($"{c.nombre} {c.apellido}");
        //    }
        //    else if (campo == tbDniClienteVenta)
        //    {
        //        foreach (var c in clientes)
        //            lbClientesSugeridos.Items.Add(c.dni.ToString());
        //    }
        //    else if (campo == tbEmailClienteVenta)
        //    {
        //        foreach (var c in clientes)
        //            lbClientesSugeridos.Items.Add(c.email);
        //    }

        //    lbClientesSugeridos.Location = new Point(campo.Left, campo.Bottom);
        //    lbClientesSugeridos.Width = campo.Width;
        //    lbClientesSugeridos.BringToFront();
        //    lbClientesSugeridos.Visible = lbClientesSugeridos.Items.Count > 0;
        //}

        //private void tbClienteFiltro_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox campo = sender as TextBox;
        //    if (campo == null) return;

        //    string nombre = "";
        //    string dni = "";
        //    string email = "";

        //    if (campo == tbNombreClienteVenta)
        //        nombre = tbNombreClienteVenta.Text.Trim();
        //    else if (campo == tbDniClienteVenta)
        //        dni = tbDniClienteVenta.Text.Trim();
        //    else if (campo == tbEmailClienteVenta)
        //        email = tbEmailClienteVenta.Text.Trim();

        //    var clientes = Cliente_controller.BuscarClientes(nombre, dni, email);

        //    lbClientesSugeridos.Items.Clear();
        //    clientesMap.Clear();

        //    if (campo == tbNombreClienteVenta)
        //    {
        //        foreach (var c in clientes)
        //        {
        //            string clave = $"{c.nombre} {c.apellido}";
        //            lbClientesSugeridos.Items.Add(clave);
        //            clientesMap[clave] = c;
        //        }
        //    }
        //    else if (campo == tbDniClienteVenta)
        //    {
        //        foreach (var c in clientes)
        //        {
        //            string clave = c.dni.ToString();
        //            lbClientesSugeridos.Items.Add(clave);
        //            clientesMap[clave] = c;
        //        }
        //    }
        //    else if (campo == tbEmailClienteVenta)
        //    {
        //        foreach (var c in clientes)
        //        {
        //            string clave = c.email;
        //            lbClientesSugeridos.Items.Add(clave);
        //            clientesMap[clave] = c;
        //        }
        //    }

        //    lbClientesSugeridos.Location = new Point(campo.Left, campo.Bottom);
        //    lbClientesSugeridos.Width = campo.Width;
        //    lbClientesSugeridos.BringToFront();

        //    // Ocultar si el campo está vacío o no hay resultados
        //    if (string.IsNullOrWhiteSpace(campo.Text) || lbClientesSugeridos.Items.Count == 0)
        //    {
        //        lbClientesSugeridos.Visible = false;
        //    }
        //    else
        //    {
        //        lbClientesSugeridos.Visible = true;
        //    }
        //}

        private void tbClienteFiltro_TextChanged(object sender, EventArgs e)
        {
            TextBox campo = sender as TextBox;
            if (campo == null) return;

            string nombre = "";
            string dni = "";
            string email = "";

            if (campo == tbNombreClienteVenta)
                nombre = campo.Text.Trim();
            else if (campo == tbDniClienteVenta)
                dni = campo.Text.Trim();
            else if (campo == tbEmailClienteVenta)
                email = campo.Text.Trim();

            var clientes = Cliente_controller.BuscarClientes(nombre, dni, email);

            clientesMap.Clear();
            lbClientesSugeridos.Items.Clear();

            foreach (var c in clientes)
            {
                string clave = campo == tbNombreClienteVenta ? $"{c.nombre} {c.apellido}" :
                               campo == tbDniClienteVenta ? c.dni.ToString() :
                               c.email;

                lbClientesSugeridos.Items.Add(clave);
                clientesMap[clave] = c;
            }

            if (lbClientesSugeridos.Parent != this)
                this.Controls.Add(lbClientesSugeridos);

            // ✅ Ocultar, posicionar y mostrar dentro de BeginInvoke
            this.BeginInvoke(new Action(() =>
            {
                lbClientesSugeridos.Visible = false;

                Point posicionCampo = campo.PointToScreen(new Point(0, campo.Height));
                Point posicionEnFormulario = this.PointToClient(posicionCampo);
                lbClientesSugeridos.Location = posicionEnFormulario;
                lbClientesSugeridos.Width = campo.Width;
                lbClientesSugeridos.BringToFront();

                bool mostrar = !string.IsNullOrWhiteSpace(campo.Text) && lbClientesSugeridos.Items.Count > 0;
                lbClientesSugeridos.Visible = mostrar;
            }));
        }

        private void tbProductoFiltro_TextChanged(object sender, EventArgs e)
        {
            if (bloqueandoFiltroProducto) return; // ✅ Evitar ejecución durante autocompletado

            TextBox campo = sender as TextBox;
            if (campo == null) return;

            string nombre = "";
            string id = "";

            if (campo == tbNombreProductoVenta)
                nombre = campo.Text.Trim();
            else if (campo == tbIdProductoVenta)
                id = campo.Text.Trim();

            var productos = Producto_controller.BuscarProductos(nombre, id);

            productosMap.Clear();
            lbProductosSugeridos.Items.Clear();

            foreach (var p in productos)
            {
                string clave = campo == tbNombreProductoVenta ? p.nombre :
                               campo == tbIdProductoVenta ? p.id_producto.ToString() :
                               "";

                lbProductosSugeridos.Items.Add(clave);
                productosMap[clave] = p;
            }

            if (lbProductosSugeridos.Parent != this)
                this.Controls.Add(lbProductosSugeridos);

            this.BeginInvoke(new Action(() =>
            {
                lbProductosSugeridos.Visible = false;

                Point posicionCampo = campo.PointToScreen(new Point(0, campo.Height));
                Point posicionEnFormulario = this.PointToClient(posicionCampo);
                lbProductosSugeridos.Location = posicionEnFormulario;
                lbProductosSugeridos.Width = campo.Width;
                lbProductosSugeridos.BringToFront();

                lbProductosSugeridos.Visible = !string.IsNullOrWhiteSpace(campo.Text) && lbProductosSugeridos.Items.Count > 0;
            }));
        }

        private void LbProductosSugeridos_Click(object sender, EventArgs e)
        {
            if (lbProductosSugeridos.SelectedItem == null) return;

            string clave = lbProductosSugeridos.SelectedItem.ToString();
            if (!productosMap.ContainsKey(clave)) return;

            var producto = productosMap[clave];

            bloqueandoFiltroProducto = true; // ✅ Bloquear eventos de filtro

            tbIdProductoVenta.Text = producto.id_producto.ToString();
            tbNombreProductoVenta.Text = producto.nombre;
            tbCategoriaProductoVenta.Text = producto.id_categoria.ToString();
            tbMarcaProductoVenta.Text = producto.id_marca.ToString();
            tbPrecioVtaProductoVenta.Text = producto.precio_vta.ToString("0.00");
            tbStockProductoVenta.Text = producto.stock.ToString();

            pbImagenProductoVenta.Image?.Dispose();

            if (!string.IsNullOrWhiteSpace(producto.imagen) && File.Exists(producto.imagen))
            {
                pbImagenProductoVenta.Image = System.Drawing.Image.FromFile(producto.imagen);
            }
            else
            {
                pbImagenProductoVenta.Image = (Bitmap)Properties.Resources.producto_defecto.Clone();
            }
            pbImagenProductoVenta.SizeMode = PictureBoxSizeMode.StretchImage;

            bloqueandoFiltroProducto = false; // ✅ Reactivar eventos

            lbProductosSugeridos.Visible = false; // ✅ Ocultar inmediatamente
        }

        private void OcultarListaClientes(object sender, EventArgs e)
        {
            // Esperamos un breve momento para permitir que el usuario haga clic en el ListBox
            Task.Delay(100).ContinueWith(_ =>
            {
                this.Invoke(new Action(() =>
                {
                    if (!lbClientesSugeridos.Focused)
                        lbClientesSugeridos.Visible = false;
                }));
            });
        }

        private void ConectarOcultamientoEnControles(Control control)
        {
            control.MouseDown += OcultarListaClientesPorClickGlobal;

            foreach (Control hijo in control.Controls)
            {
                ConectarOcultamientoEnControles(hijo);
            }
        }

        private void OcultarListaClientesPorClickGlobal(object sender, MouseEventArgs e)
        {
            // Ocultamos si el clic no fue sobre el ListBox ni sobre los campos de cliente
            if (!tbNombreClienteVenta.Focused &&
                !tbDniClienteVenta.Focused &&
                !tbEmailClienteVenta.Focused &&
                !lbClientesSugeridos.Focused)
            {
                lbClientesSugeridos.Visible = false;
            }
        }

        private void OcultarListaProductos(object sender, EventArgs e)
        {
            Task.Delay(100).ContinueWith(_ =>
            {
                this.Invoke(new Action(() =>
                {
                    if (!lbProductosSugeridos.Focused)
                        lbProductosSugeridos.Visible = false;
                }));
            });
        }

        private void TextBox_OnlyDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TextBox_OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsLetter(e.KeyChar)
                && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        //private void cbVentaProductoPendientes_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbVentaProductoPendientes.Checked)
        //    {
        //        cbVentaProductoVendidos.Checked = false;
        //        cbVentaProductoCancelados.Checked = false;
        //        MostrarVentasPorEstadoEnGrid(1); // Pendiente
        //    }
        //}

        private void cbVentaProductoPendientes_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVentaProductoPendientes.Checked)
            {
                cbVentaProductoVendidos.Checked = false;
                cbVentaProductoCancelados.Checked = false;
                MostrarCarritoEnGrid();

                // ✅ Recalcular total del carrito y mostrar en formato argentino
                decimal totalCarrito = ObtenerTotalCarrito();
                CultureInfo culturaAR = new CultureInfo("es-AR");
                lbTotalVendidoVenta.Text = totalCarrito.ToString("C", culturaAR);
            }
            else if (!cbVentaProductoVendidos.Checked && !cbVentaProductoCancelados.Checked)
            {
                cbVentaProductoPendientes.Checked = true; // No permitir que todos estén desmarcados
            }
        }

        private void cbVentaProductoVendidos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVentaProductoVendidos.Checked)
            {
                cbVentaProductoPendientes.Checked = false;
                cbVentaProductoCancelados.Checked = false;
                MostrarVentasPorEstadoEnGrid(2); // Finalizado

                //decimal total = Venta_controller.ObtenerTotalVentasPorEstado(2);
                decimal total = Venta_controller.ObtenerTotalVentasPorEstadoYVendedor(2, vendedorId);
                CultureInfo culturaAR = new CultureInfo("es-AR");
                lbTotalVendidoVenta.Text = total.ToString("C", culturaAR);
                //lbTotalVendidoVenta.Text = $"${total:0.00}";
            }
            else if (!cbVentaProductoPendientes.Checked && !cbVentaProductoCancelados.Checked)
            {
                cbVentaProductoVendidos.Checked = true;
            }
        }

        private void cbVentaProductoCancelados_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVentaProductoCancelados.Checked)
            {
                cbVentaProductoPendientes.Checked = false;
                cbVentaProductoVendidos.Checked = false;
                MostrarVentasPorEstadoEnGrid(3); // Cancelado

                //decimal total = Venta_controller.ObtenerTotalVentasPorEstado(3);
                decimal total = Venta_controller.ObtenerTotalVentasPorEstadoYVendedor(3, vendedorId);
                CultureInfo culturaAR = new CultureInfo("es-AR");
                lbTotalVendidoVenta.Text = total.ToString("C", culturaAR);
            }
            else if (!cbVentaProductoPendientes.Checked && !cbVentaProductoVendidos.Checked)
            {
                cbVentaProductoCancelados.Checked = true;
            }
        }

        private void MostrarVentasPorEstadoEnGrid(int estado)
        {
            dgvVentas.Columns.Clear();
            dgvVentas.Rows.Clear();

            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.ReadOnly = true;

            // ✅ Columna oculta para ID interno
            var colIdOculta = new DataGridViewTextBoxColumn
            {
                Name = "colIdVenta",
                HeaderText = "ID Venta",
                Visible = false
            };
            dgvVentas.Columns.Add(colIdOculta);

            // ✅ Columnas visibles
            dgvVentas.Columns.Add("colNroFactura", "Nro Factura");
            dgvVentas.Columns.Add("colCliente", "Cliente");
            dgvVentas.Columns.Add("colVendedor", "Vendedor");
            dgvVentas.Columns.Add("colFecha", "Fecha");
            dgvVentas.Columns.Add("colTipoFactura", "Tipo Factura");
            dgvVentas.Columns.Add("colTotal", "Total");
            dgvVentas.Columns.Add("colEstado", "Estado");

            // ✅ Solo agregar botones si el estado es Finalizado
            if (estado == 2)
            {
                var colCancelar = new DataGridViewButtonColumn
                {
                    Name = "colCancelar",
                    HeaderText = "Cancelar",
                    Text = "Cancelar",
                    UseColumnTextForButtonValue = true
                };
                dgvVentas.Columns.Add(colCancelar);

                var colDescargarPDF = new DataGridViewButtonColumn
                {
                    Name = "colDescargarPDF",
                    HeaderText = "PDF",
                    Text = "Descargar",
                    UseColumnTextForButtonValue = true
                };
                dgvVentas.Columns.Add(colDescargarPDF);
            }

            //var ventas = Venta_controller.ObtenerVentasPorEstado(estado);
            var ventas = Venta_controller.ObtenerVentasPorEstadoYVendedor(estado, vendedorId); // ✅ filtrado por vendedor
            foreach (var v in ventas)
            {
                dgvVentas.Rows.Add(
                    v.id_venta, // ✅ columna oculta
                    v.nro_factura,
                    v.nombre_cliente,
                    v.nombre_vendedor,
                    v.fecha.ToString("dd/MM/yyyy"),
                    v.tipo_factura,
                    //$"${v.total_venta:0.00}",
                    v.total_venta.ToString("C", new CultureInfo("es-AR")),
                    estado == 2 ? "Finalizado" : "Cancelado"
                );
            }
        }

        //private void MostrarVentasRealizadasEnGrid()
        //{
        //    dgvVentas.Columns.Clear();
        //    dgvVentas.Rows.Clear();

        //    dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    dgvVentas.AllowUserToAddRows = false;
        //    dgvVentas.ReadOnly = true;

        //    dgvVentas.Columns.Add("colIdVenta", "ID Venta");
        //    dgvVentas.Columns.Add("colCliente", "Cliente");
        //    dgvVentas.Columns.Add("colVendedor", "Vendedor");
        //    dgvVentas.Columns.Add("colFecha", "Fecha");
        //    dgvVentas.Columns.Add("colTipoFactura", "Tipo Factura");
        //    dgvVentas.Columns.Add("colTotal", "Total");
        //    dgvVentas.Columns.Add("colEstado", "Estado");
        //    var colCancelar = new DataGridViewButtonColumn
        //    {
        //        Name = "colCancelar",
        //        HeaderText = "Cancelar",
        //        Text = "Cancelar",
        //        UseColumnTextForButtonValue = true
        //    };
        //    dgvVentas.Columns.Add(colCancelar);

        //    var ventas = Venta_controller.ObtenerVentasFinalizadas(); // Debés tener este método en tu controlador

        //    foreach (var v in ventas)
        //    {
        //        dgvVentas.Rows.Add(
        //            v.id_venta,
        //            v.nombre_cliente,
        //            v.nombre_vendedor,
        //            v.fecha.ToString("dd/MM/yyyy"),
        //            v.tipo_factura,
        //            $"${v.total_venta:0.00}",
        //            v.id_estado == 2 ? "Finalizado" : v.id_estado == 3 ? "Cancelado" : "Pendiente"
        //        );
        //    }
        //}




        //private void IbBotonBuscarClienteVenta_Click(object sender, EventArgs e)
        //{
        //    string dniText = tbDniClienteVenta.Text.Trim();
        //    string nombreTxt = tbNombreClienteVenta.Text.Trim();

        //    if (string.IsNullOrWhiteSpace(dniText) || string.IsNullOrWhiteSpace(nombreTxt))
        //    {
        //        MessageBox.Show("Debe ingresar el DNI y el nombre del cliente para buscar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //        if (string.IsNullOrWhiteSpace(dniText))
        //            tbDniClienteVenta.Focus();
        //        else
        //            tbNombreClienteVenta.Focus();

        //        return;
        //    }

        //    if (!Regex.IsMatch(dniText, @"^\d{8}$"))
        //    {
        //        MessageBox.Show("El DNI debe contener exactamente 8 dígitos numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbDniClienteVenta.Focus();
        //        return;
        //    }

        //    if (Regex.IsMatch(nombreTxt, @"\d"))
        //    {
        //        MessageBox.Show("El nombre no puede contener dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbNombreClienteVenta.Focus();
        //        return;
        //    }

        //    MessageBox.Show("Datos válidos. Iniciando búsqueda del cliente…", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void ventas_Load(object sender, EventArgs e)
        {
            //Vendedor
            tbDniVendedorVenta.Text = vendedorDni.ToString();
            tbNombreVendedorVenta.Text = vendedorNombreCompleto;
            tbIdVendedorVenta.Text = vendedorId.ToString();

            // Marcar como solo lectura para evitar edición (Vendedor)
            tbDniVendedorVenta.ReadOnly = true;
            tbNombreVendedorVenta.ReadOnly = true;
            tbIdVendedorVenta.ReadOnly = true;

            ////Cliente
            //tbDniClienteVenta.Text = clienteDni.ToString();
            //tbNombreClienteVenta.Text = clienteNombreCompleto;
            //tbIdClienteVenta.Text = clienteId.ToString();

            //// Marcar como solo lectura para evitar edición (Cliente)
            //tbDniClienteVenta.ReadOnly = true;
            //tbNombreClienteVenta.ReadOnly = true;
            //tbIdClienteVenta.ReadOnly = true;
            // Configuración del ListBox
            lbClientesSugeridos.Visible = false;
            lbClientesSugeridos.Width = 300;
            lbClientesSugeridos.Height = 300;

            // Si tbNombreClienteVenta está dentro de un GroupBox llamado groupBoxCliente:
            //groupBoxCliente.Controls.Add(lbClientesSugeridos);
            //this.Controls.Add(lbClientesSugeridos);
            lbClientesSugeridos.BringToFront();

            lbClientesSugeridos.Click += LbClientesSugeridos_Click;

            //Producto
            lbProductosSugeridos.Visible = false;
            lbProductosSugeridos.Width = 300;
            lbProductosSugeridos.Height = 300;
            this.Controls.Add(lbProductosSugeridos);
            lbProductosSugeridos.BringToFront();

            dtpFechaVenta.ShowCheckBox = true;
            dtpFechaVenta.Checked = true;
            dtpFechaVenta.Value = DateTime.Today;
            dtpFechaVenta.Checked = true;
            dtpFechaVenta.ValueChanged += dtpFechaVenta_ValueChanged;

            cbTipoFacturaVenta.SelectedIndex = 0;

            dgvVentas.Columns.Clear();
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvVentas.Columns.Add("colNombre", "Nombre Producto");
            dgvVentas.Columns.Add("colDescripcion", "Descripción");
            dgvVentas.Columns.Add("colCantidad", "Cantidad");
            dgvVentas.Columns.Add("colPrecioUnitario", "Precio Unitario");
            dgvVentas.Columns.Add("colTotal", "Total");
            dgvVentas.Columns.Add("colFecha", "Fecha");
            dgvVentas.Columns.Add("colEstado", "Estado");

            var colBorrar = new DataGridViewButtonColumn
            {
                Name = "colBorrar",
                HeaderText = "Acción",
                Text = "Borrar",
                UseColumnTextForButtonValue = true
            };
            dgvVentas.Columns.Add(colBorrar);

            // ✅ Ajuste de ancho proporcional
            dgvVentas.Columns["colDescripcion"].FillWeight = 200;
            dgvVentas.Columns["colNombre"].FillWeight = 100;
            dgvVentas.Columns["colCantidad"].FillWeight = 80;
            dgvVentas.Columns["colPrecioUnitario"].FillWeight = 80;
            dgvVentas.Columns["colTotal"].FillWeight = 80;
            dgvVentas.Columns["colFecha"].FillWeight = 100;
            dgvVentas.Columns["colEstado"].FillWeight = 100;
            dgvVentas.Columns["colBorrar"].FillWeight = 30;

            // ✅ BLOQUE NUEVO: desactivar edición en el grid excepto botón borrar
            dgvVentas.ReadOnly = true;
            dgvVentas.Columns["colBorrar"].ReadOnly = false;

            dgvVentas.AllowUserToAddRows = false;

            // ✅ Estilo visual del botón "Borrar"
            dgvVentas.CellPainting += dgvVentas_CellPainting;

            MostrarCarritoEnGrid();
            ActualizarTotalVendido();

            ConectarOcultamientoEnControles(this);

            var clickFilter = new ClickMessageFilter();
            clickFilter.ClickFueraDetectado += (clickeado) =>
            {
                if (clickeado != tbNombreClienteVenta &&
                    clickeado != tbDniClienteVenta &&
                    clickeado != tbEmailClienteVenta &&
                    clickeado != lbClientesSugeridos)
                {
                    lbClientesSugeridos.Visible = false;
                }
            };

            Application.AddMessageFilter(clickFilter);

            // ✅ NUEVO BLOQUE: bloquear cliente si ya hay productos en el carrito
            if (CarritoVentaSession.Carrito.Count > 0)
            {
                var primerDetalle = CarritoVentaSession.Carrito.First();

                tbNombreClienteVenta.Text = primerDetalle.nombre_cliente;
                tbDniClienteVenta.Text = primerDetalle.dni_cliente;
                tbEmailClienteVenta.Text = primerDetalle.email_cliente;
                tbIdClienteVenta.Text = primerDetalle.id_cliente.ToString();

                tbNombreClienteVenta.ReadOnly = true;
                tbDniClienteVenta.ReadOnly = true;
                tbEmailClienteVenta.ReadOnly = true;
                tbIdClienteVenta.ReadOnly = true;

                tbNombreClienteVenta.Enabled = false;
                tbDniClienteVenta.Enabled = false;
                tbEmailClienteVenta.Enabled = false;
                tbIdClienteVenta.Enabled = false;
            }

            //cbVentasFinalizadas.Checked = true;
            //cbVentasPendientes.Checked = false;

            //cbVentasFinalizadas.CheckedChanged += cbVentasFinalizadas_CheckedChanged;
            //cbVentasPendientes.CheckedChanged += cbVentasPendientes_CheckedChanged;

            //MostrarVentasFinalizadasEnGrid();
        }

        private void dtpFechaVenta_ValueChanged(object sender, EventArgs e)
        {
            // Si el usuario intenta desmarcar, lo volvemos a marcar
            if (!dtpFechaVenta.Checked)
            {
                dtpFechaVenta.Checked = true;
            }
        }

        private void dgvVentas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string nombreColumna = dgvVentas.Columns[e.ColumnIndex].Name;

            if (nombreColumna == "colBorrar" || nombreColumna == "colCancelar" || nombreColumna == "colDescargarPDF")
            {
                e.PaintBackground(e.CellBounds, true);

                int margen = 4;
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                    e.CellBounds.X + margen,
                    e.CellBounds.Y + margen,
                    e.CellBounds.Width - 2 * margen,
                    e.CellBounds.Height - 2 * margen
                );

                int radio = 8;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, radio, radio, 180, 90);
                    path.AddArc(rect.Right - radio, rect.Y, radio, radio, 270, 90);
                    path.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radio, radio, radio, 90, 90);
                    path.CloseFigure();

                    // ✅ Color según tipo de botón
                    Color colorFondo = nombreColumna switch
                    {
                        "colBorrar" => Color.Red,
                        "colCancelar" => Color.Red,
                        "colDescargarPDF" => Color.Blue,
                    };

                    using (Brush backColorBrush = new SolidBrush(colorFondo))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(backColorBrush, path);
                    }

                    // ✅ Texto según tipo de botón
                    string texto = nombreColumna switch
                    {
                        "colBorrar" => "Borrar",
                        "colCancelar" => "Cancelar",
                        "colDescargarPDF" => "Descargar",
                        _ => ""
                    };

                    TextRenderer.DrawText(
                        e.Graphics,
                        texto,
                        dgvVentas.Font,
                        rect,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }

                e.Handled = true;
            }
        }

        private void dgvVentas_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string nombreColumna = dgvVentas.Columns[e.ColumnIndex].Name;

                // ✅ Agregamos "colDescargarPDF" para que también muestre el cursor de mano
                if (nombreColumna == "colBorrar" || nombreColumna == "colCancelar" || nombreColumna == "colDescargarPDF")
                {
                    dgvVentas.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvVentas.Cursor = Cursors.Default;
                }
            }
        }

        //private void dgvVentas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.ColumnIndex >= 0 && dgvVentas.Columns[e.ColumnIndex].Name == "colBorrar" && e.RowIndex >= 0)
        //    {
        //        e.PaintBackground(e.CellBounds, true);

        //        // ✅ Margen interno
        //        int margen = 4;
        //        Rectangle rect = new Rectangle(
        //            e.CellBounds.X + margen,
        //            e.CellBounds.Y + margen,
        //            e.CellBounds.Width - 2 * margen,
        //            e.CellBounds.Height - 2 * margen
        //        );

        //        // ✅ Crear botón con esquinas redondeadas
        //        int radio = 8; // radio de las esquinas
        //        using (GraphicsPath path = new GraphicsPath())
        //        {
        //            path.AddArc(rect.X, rect.Y, radio, radio, 180, 90);
        //            path.AddArc(rect.Right - radio, rect.Y, radio, radio, 270, 90);
        //            path.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 0, 90);
        //            path.AddArc(rect.X, rect.Bottom - radio, radio, radio, 90, 90);
        //            path.CloseFigure();

        //            using (Brush backColorBrush = new SolidBrush(Color.Red))
        //            {
        //                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //                e.Graphics.FillPath(backColorBrush, path);
        //            }

        //            // ✅ Dibujar texto centrado
        //            TextRenderer.DrawText(
        //                e.Graphics,
        //                "Borrar",
        //                dgvVentas.Font,
        //                rect,
        //                Color.White,
        //                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        //            );
        //        }

        //        e.Handled = true;
        //    }
        //}

        //private void cbVentasFinalizadas_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbVentasFinalizadas.Checked)
        //    {
        //        cbVentasPendientes.Checked = false;
        //        MostrarVentasFinalizadasEnGrid();
        //    }
        //}

        //private void MostrarVentasFinalizadasEnGrid()
        //{
        //    dgvVentas.Columns.Clear();
        //    dgvVentas.Rows.Clear();

        //    dgvVentas.Columns.Add("colIdVenta", "ID Venta");
        //    dgvVentas.Columns.Add("colCliente", "Cliente");
        //    dgvVentas.Columns.Add("colVendedor", "Vendedor");
        //    dgvVentas.Columns.Add("colFecha", "Fecha");
        //    dgvVentas.Columns.Add("colTipoFactura", "Tipo Factura");
        //    dgvVentas.Columns.Add("colTotal", "Total");

        //    var ventas = Venta_controller.ObtenerVentasFinalizadas();

        //    foreach (var v in ventas)
        //    {
        //        dgvVentas.Rows.Add(
        //            v.id_venta,
        //            v.nombre_cliente,
        //            v.nombre_vendedor,
        //            v.fecha.ToString("dd/MM/yyyy"),
        //            v.tipo_factura,
        //            v.total_venta.ToString("0.00")
        //        );
        //    }
        //}

        //private void cbVentasFinalizadas_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbVentasFinalizadas.Checked)
        //    {
        //        cbVentasPendientes.Checked = false;
        //        MostrarVentasFinalizadasEnGrid();
        //    }
        //}

        //private void cbVentasPendientes_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbVentasPendientes.Checked)
        //    {
        //        cbVentasFinalizadas.Checked = false;
        //        MostrarCarritoEnGrid();
        //    }
        //}

        //NUEVO ACAAAAAAAA
        //private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Validar que el clic fue en una fila válida
        //    if (e.RowIndex < 0) return;

        //    // Validar que se hizo clic en la columna de borrar
        //    if (e.ColumnIndex >= 0 && dgvVentas.Columns[e.ColumnIndex].Name == "colBorrar")
        //    {
        //        // Validar que el índice existe en el carrito
        //        if (e.RowIndex < CarritoVentaSession.Carrito.Count)
        //        {
        //            CarritoVentaSession.Carrito.RemoveAt(e.RowIndex);
        //            MostrarCarritoEnGrid();
        //            ActualizarTotalVendido();
        //        }
        //    }
        //}

        //private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && dgvVentas.Columns[e.ColumnIndex].Name == "colCancelar")
        //    {
        //        int idVenta = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["colIdVenta"].Value);
        //        DialogResult confirm = MessageBox.Show("¿Está seguro que desea cancelar esta venta?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        //        if (confirm == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                bool exito = Venta_controller.CancelarVentaDuplicando(idVenta);
        //                if (exito)
        //                {
        //                    MessageBox.Show("Venta cancelada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    MostrarVentasPorEstadoEnGrid(2); // Refresca solo ventas finalizadas
        //                }
        //                else
        //                {
        //                    MessageBox.Show("La operación no se completó. Verificá si la venta tiene detalles o si el ID es válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error técnico: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //}

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string nombreColumna = dgvVentas.Columns[e.ColumnIndex].Name;

            // ✅ Botón "Cancelar" en ventas finalizadas
            if (nombreColumna == "colCancelar")
            {
                int idVenta = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["colIdVenta"].Value);

                // ✅ Validación: evitar cancelación duplicada
                if (Venta_controller.YaFueCancelada(idVenta))
                {
                    MessageBox.Show("Esta venta ya fue cancelada previamente.", "Cancelación duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show("¿Está seguro que desea cancelar esta venta?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        bool exito = Venta_controller.CancelarVentaDuplicando(idVenta);
                        if (exito)
                        {
                            MessageBox.Show("Venta cancelada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MostrarVentasPorEstadoEnGrid(2); // Refresca solo ventas finalizadas
                        }
                        else
                        {
                            MessageBox.Show("La operación no se completó. Verificá si la venta tiene detalles o si el ID es válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error técnico: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // ✅ Botón "Borrar" en el carrito (ventas pendientes)
            else if (nombreColumna == "colBorrar")
            {
                DialogResult confirm = MessageBox.Show("¿Deseás eliminar este producto del carrito?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                string nombreProducto = dgvVentas.Rows[e.RowIndex].Cells["colNombre"].Value?.ToString();
                var item = CarritoVentaSession.Carrito.FirstOrDefault(p => p.producto_nombre == nombreProducto);
                if (item != null)
                {
                    CarritoVentaSession.Carrito.Remove(item);
                    MostrarCarritoEnGrid();

                    // Actualizar total
                    decimal totalCarrito = CarritoVentaSession.Carrito.Sum(p => p.precio_unitario * p.cantidad);
                    CultureInfo culturaAR = new CultureInfo("es-AR");
                    lbTotalVendidoVenta.Text = totalCarrito.ToString("C", culturaAR);
                }
            }

            // ✅ Botón "Descargar" en ventas finalizadas
            else if (nombreColumna == "colDescargarPDF")
            {
                int idVenta = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["colIdVenta"].Value);

                try
                {
                    GenerarPdfVenta(idVenta); // ✅ Este método lo definís en tu formulario
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error técnico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarTotalVendido()
        {
            decimal total = CarritoVentaSession.Carrito
                .Sum(d => d.cantidad * d.precio_unitario);

            CultureInfo culturaAR = new CultureInfo("es-AR");
            lbTotalVendidoVenta.Text = total.ToString("C", culturaAR);
            //lbTotalVendidoVenta.Text = $"${total:0.00}";
        }

        private decimal ObtenerTotalCarrito()
        {
            return CarritoVentaSession.Carrito.Sum(p => p.precio_unitario * p.cantidad);
        }


        //private void LbClientesSugeridos_Click(object sender, EventArgs e)
        //{
        //    if (lbClientesSugeridos.SelectedItem == null) return;

        //    string seleccionado = lbClientesSugeridos.SelectedItem.ToString();
        //    Cliente_model cliente = null;

        //    if (tbNombreClienteVenta.Focused)
        //    {
        //        tbNombreClienteVenta.Text = seleccionado;
        //        cliente = Cliente_controller.BuscarClientes(seleccionado, "", "").FirstOrDefault();
        //    }
        //    else if (tbDniClienteVenta.Focused)
        //    {
        //        tbDniClienteVenta.Text = seleccionado;
        //        cliente = Cliente_controller.BuscarClientes("", seleccionado, "").FirstOrDefault();
        //    }
        //    else if (tbEmailClienteVenta.Focused)
        //    {
        //        tbEmailClienteVenta.Text = seleccionado;
        //        cliente = Cliente_controller.BuscarClientes("", "", seleccionado).FirstOrDefault();
        //    }

        //    if (cliente != null)
        //    {
        //        tbNombreClienteVenta.Text = $"{cliente.nombre} {cliente.apellido}";
        //        tbDniClienteVenta.Text = cliente.dni.ToString();
        //        tbEmailClienteVenta.Text = cliente.email;
        //        tbIdClienteVenta.Text = cliente.id_cliente.ToString();
        //    }

        //    lbClientesSugeridos.Visible = false;
        //}

        private void LbClientesSugeridos_Click(object sender, EventArgs e)
        {
            if (lbClientesSugeridos. SelectedItem == null) return;

            string clave = lbClientesSugeridos.SelectedItem.ToString();
            if (!clientesMap.ContainsKey(clave)) return;

            var cliente = clientesMap[clave];

            tbNombreClienteVenta.Text = $"{cliente.nombre} {cliente.apellido}";
            tbDniClienteVenta.Text = cliente.dni.ToString();
            tbEmailClienteVenta.Text = cliente.email;
            tbIdClienteVenta.Text = cliente.id_cliente.ToString();

            lbClientesSugeridos.Visible = false;
        }


        //private void IbBotonBuscarProductoVenta_Click(object sender, EventArgs e)
        //{
        //    string nombreProd = tbNombreProductoVenta.Text.Trim();
        //    //string descripcion = tbDescripcionProductoVenta.Text.Trim();
        //    string stockText = tbStockProductoVenta.Text.Trim();
        //    string precioText = tbPrecioVtaProductoVenta.Text.Trim();
        //    string cantidadText = tbCantidadProductoVenta.Text.Trim();

        //    if (string.IsNullOrWhiteSpace(nombreProd))
        //    {
        //        MessageBox.Show("El nombre del producto no puede quedar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbNombreProductoVenta.Focus();
        //        return;
        //    }

        //    //if (string.IsNullOrWhiteSpace(descripcion))
        //    //{
        //    //    MessageBox.Show("La descripción del producto no puede quedar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    //    tbDescripcionProductoVenta.Focus();
        //    //    return;
        //    //}

        //    if (string.IsNullOrWhiteSpace(stockText))
        //    {
        //        MessageBox.Show("Debe ingresar el stock del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbStockProductoVenta.Focus();
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(precioText))
        //    {
        //        MessageBox.Show("Debe ingresar el precio de venta del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbPrecioVtaProductoVenta.Focus();
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(cantidadText))
        //    {
        //        MessageBox.Show("Debe ingresar la cantidad a vender.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbCantidadProductoVenta.Focus();
        //        return;
        //    }

        //    if (!int.TryParse(stockText, out int stock) || stock < 0)
        //    {
        //        MessageBox.Show("El stock debe ser un número entero no negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbStockProductoVenta.Focus();
        //        return;
        //    }
        //    if (!decimal.TryParse(precioText, out decimal precio) || precio < 0)
        //    {
        //        MessageBox.Show("El precio debe ser un número no negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbPrecioVtaProductoVenta.Focus();
        //        return;
        //    }
        //    if (!int.TryParse(cantidadText, out int cantidad) || cantidad < 0)
        //    {
        //        MessageBox.Show("La cantidad debe ser un número entero no negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbCantidadProductoVenta.Focus();
        //        return;
        //    }

        //    if (cantidad > stock)
        //    {
        //        MessageBox.Show("La cantidad a vender no puede superar el stock disponible.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        tbCantidadProductoVenta.Focus();
        //        return;
        //    }

        //    MessageBox.Show("Datos válidos. Ya puede agregar el producto.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    ibBotonAgregarProductoVenta.Enabled = true;
        //}

        private void IbBotonAgregarProductoVenta_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (!int.TryParse(tbIdClienteVenta.Text, out int idCliente))
            {
                MessageBox.Show("Debe seleccionar un cliente válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbTipoFacturaVenta.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un tipo de factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaVenta = dtpFechaVenta.Checked ? dtpFechaVenta.Value.Date : DateTime.Today;
            if (fechaVenta > DateTime.Today)
            {
                MessageBox.Show("La fecha de venta no puede ser mayor al día de hoy.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(tbIdProductoVenta.Text, out int idProducto))
            {
                MessageBox.Show("Debe seleccionar un producto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(tbCantidadProductoVenta.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero positivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(tbPrecioVtaProductoVenta.Text, out decimal precioUnitario))
            {
                MessageBox.Show("El precio de venta no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreIngresado = tbNombreProductoVenta.Text.Trim();
            string idIngresado = tbIdProductoVenta.Text.Trim();

            Producto_model producto = null;

            // Buscar por nombre
            if (productosMap.ContainsKey(nombreIngresado))
            {
                producto = productosMap[nombreIngresado];
            }
            // Buscar por ID si no se encontró por nombre
            else if (int.TryParse(idIngresado, out int idProd))
            {
                producto = productosMap.Values.FirstOrDefault(p => p.id_producto == idProd);
            }

            if (producto == null)
            {
                MessageBox.Show("El producto ingresado no es válido. Verifique que haya seleccionado un producto de la lista.", "Error de producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //var producto = productosMap[claveProducto];

            string descripcion = string.IsNullOrWhiteSpace(producto.descripcion) ? null : producto.descripcion;
            string categoria = string.IsNullOrWhiteSpace(producto.descripcionCategoria) ? null : producto.descripcionCategoria;
            string marca = string.IsNullOrWhiteSpace(producto.descripcionMarca) ? null : producto.descripcionMarca;

            List<string> partes = new List<string>();

            if (!string.IsNullOrWhiteSpace(descripcion))
                partes.Add(descripcion);

            // ✅ Formato mejorado: "Categoría: Educación - Marca: HP"
            if (!string.IsNullOrWhiteSpace(categoria) || !string.IsNullOrWhiteSpace(marca))
            {
                string catMarca = $"Categoría: {categoria ?? "Sin categoría"} - Marca: {marca ?? "Sin marca"}";
                partes.Add($"({catMarca})");
            }

            string descripcionCompleta = partes.Count > 0 ? string.Join(" ", partes) : "Sin información";

            // Crear modelo temporal
            var detalle = new Venta_detalle_model
            {
                id_producto = producto.id_producto,
                cantidad = cantidad,
                precio_unitario = producto.precio_vta,
                producto_nombre = producto.nombre,
                //producto_descripcion = $"{producto.descripcion} ({producto.descripcionCategoria} - {producto.descripcionMarca})",
                producto_descripcion = descripcionCompleta,

                descripcionCategoria = producto.descripcionCategoria,
                descripcionMarca = producto.descripcionMarca,

                id_cliente = idCliente,
                nombre_cliente = tbNombreClienteVenta.Text,
                dni_cliente = tbDniClienteVenta.Text,
                email_cliente = tbEmailClienteVenta.Text,

                tipo_factura = cbTipoFacturaVenta.SelectedItem?.ToString(),
                fecha_venta = fechaVenta
            };

            //CarritoVentaSession.Carrito.Add(detalle);
            var existente = CarritoVentaSession.Carrito.FirstOrDefault(p => p.id_producto == detalle.id_producto);

            if (existente != null)
            {
                existente.cantidad += detalle.cantidad;
            }
            else
            {
                CarritoVentaSession.Carrito.Add(detalle);
            }

            // ✅ Bloquear campos de cliente si es el primer producto agregado
            if (CarritoVentaSession.Carrito.Count == 1)
            {
                tbNombreClienteVenta.ReadOnly = true;
                tbDniClienteVenta.ReadOnly = true;
                tbEmailClienteVenta.ReadOnly = true;
                tbIdClienteVenta.ReadOnly = true;

                tbNombreClienteVenta.Enabled = false;
                tbDniClienteVenta.Enabled = false;
                tbEmailClienteVenta.Enabled = false;
                tbIdClienteVenta.Enabled = false;
            }

            MostrarCarritoEnGrid();

            //CultureInfo culturaAR = new CultureInfo("es-AR");
            //decimal totalItem = detalle.cantidad * detalle.precio_unitario;

            decimal totalCarrito = CarritoVentaSession.Carrito.Sum(p => p.precio_unitario * p.cantidad);
            CultureInfo culturaAR = new CultureInfo("es-AR");
            lbTotalVendidoVenta.Text = totalCarrito.ToString("C", culturaAR);

            //dgvVentas.Rows.Add(
            //    detalle.producto_nombre,
            //    detalle.producto_descripcion,
            //    detalle.cantidad,
            //    detalle.precio_unitario.ToString("C", culturaAR), // ✅ Precio unitario con formato argentino
            //    totalItem.ToString("C", culturaAR),               // ✅ Total con formato argentino
            //    fechaVenta.ToString("dd/MM/yyyy"),
            //    "Pendiente"
            //);

            // Limpiar campos
            tbIdProductoVenta.Clear();
            tbNombreProductoVenta.Clear();
            tbCategoriaProductoVenta.Clear();
            tbMarcaProductoVenta.Clear();
            tbPrecioVtaProductoVenta.Clear();
            tbStockProductoVenta.Clear();
            tbCantidadProductoVenta.Clear();
            //pbImagenProductoVenta.Image = null;
            pbImagenProductoVenta.Image = (Bitmap)Properties.Resources.producto_defecto.Clone();
            ibBotonAgregarProductoVenta.Enabled = false;

            // Permitir edición para agregar un nuevo producto
            //DesbloquearCamposProducto();

            ActualizarTotalVendido();
        }

        private void MostrarCarritoEnGrid()
        {
            dgvVentas.Columns.Clear();
            dgvVentas.Rows.Clear();

            dgvVentas.Columns.Add("colNombre", "Nombre Producto");
            dgvVentas.Columns.Add("colDescripcion", "Descripción");
            dgvVentas.Columns.Add("colCantidad", "Cantidad");
            dgvVentas.Columns.Add("colPrecioUnitario", "Precio Unitario");
            dgvVentas.Columns.Add("colTotal", "Total");
            dgvVentas.Columns.Add("colFecha", "Fecha");
            dgvVentas.Columns.Add("colEstado", "Estado");

            var colBorrar = new DataGridViewButtonColumn();
            colBorrar.Name = "colBorrar";
            colBorrar.HeaderText = "Acción";
            colBorrar.Text = "Borrar";
            colBorrar.UseColumnTextForButtonValue = true;
            dgvVentas.Columns.Add(colBorrar);

            CultureInfo culturaAR = new CultureInfo("es-AR");

            foreach (var item in CarritoVentaSession.Carrito)
            {
                decimal totalItem = item.precio_unitario * item.cantidad;

                dgvVentas.Rows.Add(
                    item.producto_nombre,
                    item.producto_descripcion,
                    item.cantidad,
                    item.precio_unitario.ToString("C", culturaAR),
                    totalItem.ToString("C", culturaAR),
                    DateTime.Today.ToString("dd/MM/yyyy"),
                    "Pendiente"
                );
            }

            //foreach (var detalle in CarritoVentaSession.Carrito)
            //{
            //    dgvVentas.Rows.Add(
            //        detalle.producto_nombre,
            //        detalle.producto_descripcion,
            //        detalle.cantidad,
            //        //detalle.precio_unitario.ToString("0.00"),
            //        detalle.precio_unitario.ToString("C", culturaAR),
            //        //(detalle.cantidad * detalle.precio_unitario).ToString("0.00"),
            //        (detalle.cantidad * detalle.precio_unitario).ToString("C", culturaAR),
            //        detalle.fecha_venta.ToString("dd/MM/yyyy"),
            //        "Pendiente"
            //    );
            //}
        }

        private void ibBotonBuscarClienteVenta_Click(object sender, EventArgs e)
        {
            //XXX
        }

        //private void ibBotonGuardarVenta_Click(object sender, EventArgs e)
        //{
        //    if (cbTipoFacturaVenta.SelectedIndex < 0)
        //    {
        //        MessageBox.Show(
        //            "Debe seleccionar un tipo de factura.",
        //            "Validación",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);
        //        return;
        //    }

        //    if (dtpFechaVenta.ShowCheckBox && dtpFechaVenta.Checked)
        //    {
        //        DateTime fecha = dtpFechaVenta.Value.Date;
        //        if (fecha > DateTime.Now.Date)
        //        {
        //            MessageBox.Show("La fecha de venta no puede ser mayor al día de hoy.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }

        //    MessageBox.Show("Tipo de factura y fecha válidos. Procediendo a guardar la venta…", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        private void ibBotonGuardarVenta_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (CarritoVentaSession.Carrito.Count == 0)
            {
                MessageBox.Show("No hay productos en el carrito para guardar la venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(tbIdClienteVenta.Text, out int idCliente))
            {
                MessageBox.Show("Debe seleccionar un cliente válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbTipoFacturaVenta.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un tipo de factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaVenta = dtpFechaVenta.Checked ? dtpFechaVenta.Value.Date : DateTime.Today;
            if (fechaVenta > DateTime.Today)
            {
                MessageBox.Show("La fecha de venta no puede ser mayor al día de hoy.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipoFactura = cbTipoFacturaVenta.SelectedItem.ToString();

            // 🔍 Validar stock agrupado por producto
            var productosAgrupados = CarritoVentaSession.Carrito
                .GroupBy(d => d.id_producto)
                .Select(g => new
                {
                    id_producto = g.Key,
                    cantidadTotal = g.Sum(d => d.cantidad),
                    nombre = g.First().producto_nombre
                });

            foreach (var item in productosAgrupados)
            {
                int stockActual = Producto_controller.ObtenerStockActual(item.id_producto);
                if (item.cantidadTotal > stockActual)
                {
                    MessageBox.Show($"No hay suficiente stock para el producto '{item.nombre}'. Stock disponible: {stockActual}, solicitado: {item.cantidadTotal}.", "Error de stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // ✅ Calcular total de la venta
            decimal totalCalculado = CarritoVentaSession.Carrito
                .Sum(d => d.cantidad * d.precio_unitario);

            // ✅ Crear venta principal
            var venta = new Venta_model
            {
                id_cliente = idCliente,
                id_vendedor = vendedorId,
                fecha = fechaVenta,
                tipo_factura = tipoFactura,
                total_venta = totalCalculado,
                id_estado = 2 // ✅ Estado "Finalizado"
            };

            // ✅ Generar número de factura automáticamente
            string nroFacturaGenerado = Venta_controller.GenerarNroFactura(tipoFactura);
            venta.nro_factura = nroFacturaGenerado;

            int idVentaGenerada = Venta_controller.GuardarVenta(venta);

            if (idVentaGenerada <= 0)
            {
                MessageBox.Show("No se pudo guardar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Guardar detalles y actualizar stock
            foreach (var detalle in CarritoVentaSession.Carrito)
            {
                var detalleVenta = new Venta_detalle_model
                {
                    id_venta = idVentaGenerada,
                    id_producto = detalle.id_producto,
                    cantidad = detalle.cantidad,
                    precio_unitario = detalle.precio_unitario
                };

                Venta_controller.GuardarDetalleVenta(detalleVenta); // ✅ Este método debe insertar el detalle

                Producto_controller.ActualizarStock(detalle.id_producto, -detalle.cantidad); // ✅ Este método debe restar stock
            }

            MessageBox.Show("Venta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar todo
            CarritoVentaSession.Carrito.Clear();
            MostrarCarritoEnGrid();
            ActualizarTotalVendido();
            LimpiarCamposVenta();
        }


        //private void ibBotonBorrarVenta_Click(object sender, EventArgs e)
        //{
        //    //Cliente
        //    tbDniClienteVenta.Clear();
        //    tbNombreClienteVenta.Clear();
        //    tbEmailClienteVenta.Clear();
        //    tbIdClienteVenta.Clear();

        //    //Producto
        //    tbNombreProductoVenta.Clear();
        //    //tbDescripcionProductoVenta.Clear();
        //    tbStockProductoVenta.Clear();
        //    tbPrecioVtaProductoVenta.Clear();
        //    tbCantidadProductoVenta.Clear();

        //    //Tipo de Factura y Fecha
        //    cbTipoFacturaVenta.SelectedIndex = -1;
        //    dtpFechaVenta.Value = DateTime.Today;
        //    dtpFechaVenta.Checked = false;
        //}

        private void ibBotonBorrarVenta_Click(object sender, EventArgs e)
        {
            // Limpiar campos de producto
            tbIdProductoVenta.Clear();
            tbNombreProductoVenta.Clear();
            tbCategoriaProductoVenta.Clear();
            tbMarcaProductoVenta.Clear();
            tbPrecioVtaProductoVenta.Clear();
            tbStockProductoVenta.Clear();
            tbCantidadProductoVenta.Clear();
            pbImagenProductoVenta.Image = (Bitmap)Properties.Resources.producto_defecto.Clone();
            ibBotonAgregarProductoVenta.Enabled = false;

            // Limpiar campos de cliente
            tbIdClienteVenta.Clear();
            tbNombreClienteVenta.Clear();
            tbDniClienteVenta.Clear();
            tbEmailClienteVenta.Clear();

            // ✅ Restaurar campos de cliente para permitir nueva selección
            tbNombreClienteVenta.ReadOnly = false;
            tbDniClienteVenta.ReadOnly = false;
            tbEmailClienteVenta.ReadOnly = false;
            tbIdClienteVenta.ReadOnly = false;

            tbNombreClienteVenta.Enabled = true;
            tbDniClienteVenta.Enabled = true;
            tbEmailClienteVenta.Enabled = true;
            tbIdClienteVenta.Enabled = true;

            // Limpiar campos de factura
            cbTipoFacturaVenta.SelectedIndex = 0;
            dtpFechaVenta.Value = DateTime.Today;
            dtpFechaVenta.Checked = true;

            // ✅ Borrar todos los productos del carrito si hay alguno
            if (CarritoVentaSession.Carrito.Count > 0)
            {
                CarritoVentaSession.Carrito.Clear();
                MostrarCarritoEnGrid();
                ActualizarTotalVendido(); // ✅ Esta línea faltaba
            }
        }

        private void LimpiarCamposVenta()
        {
            tbIdProductoVenta.Clear();
            tbNombreProductoVenta.Clear();
            tbCategoriaProductoVenta.Clear();
            tbMarcaProductoVenta.Clear();
            tbPrecioVtaProductoVenta.Clear();
            tbStockProductoVenta.Clear();
            tbCantidadProductoVenta.Clear();
            //pbImagenProductoVenta.Image = null;
            pbImagenProductoVenta.Image = (Bitmap)Properties.Resources.producto_defecto.Clone();

            tbIdClienteVenta.Clear();
            tbNombreClienteVenta.Clear();
            tbDniClienteVenta.Clear();
            tbEmailClienteVenta.Clear();

            tbNombreClienteVenta.ReadOnly = false;
            tbDniClienteVenta.ReadOnly = false;
            tbEmailClienteVenta.ReadOnly = false;
            tbIdClienteVenta.ReadOnly = false;

            tbNombreClienteVenta.Enabled = true;
            tbDniClienteVenta.Enabled = true;
            tbEmailClienteVenta.Enabled = true;
            tbIdClienteVenta.Enabled = true;


            cbTipoFacturaVenta.SelectedIndex = 0;
            dtpFechaVenta.Value = DateTime.Today;
            dtpFechaVenta.Checked = true;

            ibBotonAgregarProductoVenta.Enabled = false;
        }

        private void tbPrecioVtaProductoVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDescripcionProductoVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tbNombreVendedorVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbNombreClienteVenta_TextChanged(object sender, EventArgs e)
        {
            string nombre = tbNombreClienteVenta.Text.Trim();
            string dni = tbDniClienteVenta.Text.Trim();
            string email = tbEmailClienteVenta.Text.Trim();

            var clientes = Cliente_controller.BuscarClientes(
                string.IsNullOrWhiteSpace(nombre) ? "" : nombre,
                string.IsNullOrWhiteSpace(dni) ? "" : dni,
                string.IsNullOrWhiteSpace(email) ? "" : email
            );

            lbClientesSugeridos.Items.Clear();
            foreach (var c in clientes)
            {
                lbClientesSugeridos.Items.Add($"{c.nombre} {c.apellido} | DNI: {c.dni} | Email: {c.email}");
            }

            lbClientesSugeridos.Location = new Point(tbNombreClienteVenta.Left, tbNombreClienteVenta.Bottom);
            lbClientesSugeridos.Visible = clientes.Count > 0;
        }

        private void ValidarCantidadVsStock(object sender, EventArgs e)
        {
            string cantidadText = tbCantidadProductoVenta.Text.Trim();
            string stockText = tbStockProductoVenta.Text.Trim();

            if (int.TryParse(cantidadText, out int cantidad) &&
                int.TryParse(stockText, out int stock) &&
                cantidad > 0 &&
                cantidad <= stock)
            {
                ibBotonAgregarProductoVenta.Enabled = true;
            }
            else
            {
                ibBotonAgregarProductoVenta.Enabled = false;
            }
        }

        //private void BloquearCamposProducto()
        //{
        //    tbIdProductoVenta.ReadOnly = true;
        //    tbNombreProductoVenta.ReadOnly = true;
        //    tbCategoriaProductoVenta.ReadOnly = true;
        //    tbMarcaProductoVenta.ReadOnly = true;
        //    tbPrecioVtaProductoVenta.ReadOnly = true;
        //    tbStockProductoVenta.ReadOnly = true;
        //    tbCantidadProductoVenta.ReadOnly = true;
        //}

        //private void DesbloquearCamposProducto()
        //{
        //    tbIdProductoVenta.ReadOnly = false;
        //    tbNombreProductoVenta.ReadOnly = false;
        //    tbCategoriaProductoVenta.ReadOnly = false;
        //    tbMarcaProductoVenta.ReadOnly = false;
        //    tbPrecioVtaProductoVenta.ReadOnly = false;
        //    tbStockProductoVenta.ReadOnly = false;
        //    tbCantidadProductoVenta.ReadOnly = false;
        //}


        private void ibBotonAgregarProductoVenta_Click_1(object sender, EventArgs e)
        {

        }

        private void lbTotalVendidoVenta_Click(object sender, EventArgs e)
        {

        }

        private void GenerarPdfVenta(int idVenta)
        {
            var venta = Venta_controller.ObtenerCabeceraVenta(idVenta);
            var detalles = Venta_controller.ObtenerDetalleVenta(idVenta);
            if (venta == null || detalles.Count == 0)
            {
                MessageBox.Show("No se pudo generar el PDF. La venta está vacía o no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Venta_{venta.nro_factura ?? idVenta.ToString()}.pdf");
            using (FileStream stream = new FileStream(ruta, FileMode.Create))
            {
                Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                var fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fuenteTabla = FontFactory.GetFont(FontFactory.HELVETICA, 9);
                CultureInfo culturaAR = new CultureInfo("es-AR");

                doc.Add(new Paragraph("Factura de Venta", fuenteTitulo));
                doc.Add(new Paragraph($"Fecha: {venta.fecha:dd/MM/yyyy}", fuenteNormal));
                doc.Add(new Paragraph($"Factura: {venta.nro_factura ?? "Sin número"}", fuenteNormal));
                doc.Add(new Paragraph($"Cliente: {venta.nombre_cliente}", fuenteNormal));
                doc.Add(new Paragraph($"Vendedor: {venta.nombre_vendedor}", fuenteNormal));
                doc.Add(new Paragraph($"Tipo: {venta.tipo_factura}", fuenteNormal));
                doc.Add(new Paragraph($"Estado: {(venta.id_estado == 2 ? "Finalizado" : "Cancelado")}", fuenteNormal));
                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(4);
                tabla.WidthPercentage = 100;
                tabla.AddCell("Producto");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Precio Unitario");
                tabla.AddCell("Subtotal");

                foreach (var d in detalles)
                {
                    tabla.AddCell(new Phrase(d.producto_nombre, fuenteTabla));
                    tabla.AddCell(new Phrase(d.cantidad.ToString(), fuenteTabla));
                    tabla.AddCell(new Phrase(d.precio_unitario.ToString("C", culturaAR), fuenteTabla));
                    tabla.AddCell(new Phrase((d.precio_unitario * d.cantidad).ToString("C", culturaAR), fuenteTabla));
                }

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));
                //doc.Add(new Paragraph($"TOTAL: {venta.total_venta.ToString("C", culturaAR)}", fuenteNormal));
                doc.Add(new Paragraph($"TOTAL: {venta.total_venta.ToString("C", culturaAR)}", fuenteTitulo));

                doc.Close();
                stream.Close();
            }

            MessageBox.Show("PDF generado correctamente en el escritorio.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
