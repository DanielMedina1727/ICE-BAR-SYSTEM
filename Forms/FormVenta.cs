using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormVenta : UserControl
    {
        private readonly Usuario _usuarioActual;

        // Catálogo de productos
        private TextBox txtBuscarProducto = null!;
        private DataGridView gridCatalogo = null!;
        private NumericUpDown numCantidad = null!;
        private Button btnAgregarAlCarrito = null!;

        // Carrito / Detalle de la venta actual
        private DataGridView gridCarrito = null!;
        private Button btnQuitarItem = null!;
        private Button btnVaciarCarrito = null!;

        // Panel de pago
        private Label lblTotal = null!;
        private TextBox txtPrecioCancelado = null!;
        private Label lblCambio = null!;
        private ComboBox cmbTipoPago = null!;
        private TextBox txtNotas = null!;
        private Button btnRealizarVenta = null!;

        private readonly List<DetalleVenta> _carrito = new List<DetalleVenta>();
        private Producto? _productoSeleccionado = null;

        public FormVenta(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;
            InitializeComponent();
            this.Load += (s, e) => CargarCatalogo();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;

            // Encabezado
            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 62,
                BackColor = Theme.CardBg
            };
            panelTop.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelTop.Height - 1, panelTop.Width, panelTop.Height - 1);
                using var penAcc = new Pen(Color.FromArgb(80, Theme.PrimaryCyan), 2);
                e.Graphics.DrawLine(penAcc, 20, panelTop.Height - 1, 550, panelTop.Height - 1);
            };

            var lblTitulo = new Label
            {
                Text = "🛒  Terminal de Ventas — ICE BAR",
                Font = Theme.TitleFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(20, 16)
            };

            var lblVendedor = new Label
            {
                Text = $"⚡  {_usuarioActual.NombreCompleto}  |  {_usuarioActual.Rol}",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextMuted,
                AutoSize = true,
                Location = new Point(500, 22)
            };

            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(lblVendedor);

            // Split principal: Izquierda Catálogo de productos, Derecha Carrito y Cobro
            var splitPrincipal = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 540,
                BackColor = Theme.BgDark
            };

            // ==========================================
            // LADO IZQUIERDO: CATÁLOGO DE PRODUCTOS
            // ==========================================
            var panelIzquierdo = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Theme.BgDark
            };

            var gbCatalogo = new GroupBox
            {
                Text = " 1. Seleccionar Producto del Bar ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(12)
            };

            var panelFiltro = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            var lblBuscar = new Label
            {
                Text = "🔍 Buscar:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(8, 12),
                AutoSize = true
            };

            txtBuscarProducto = new TextBox
            {
                Location = new Point(85, 9),
                Size = new Size(400, 26),
                Font = Theme.BodyFont
            };
            Theme.StyleTextBox(txtBuscarProducto);
            txtBuscarProducto.TextChanged += (s, e) => CargarCatalogo();

            panelFiltro.Controls.Add(lblBuscar);
            panelFiltro.Controls.Add(txtBuscarProducto);

            gridCatalogo = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            Theme.StyleGrid(gridCatalogo);

            gridCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "IdProducto", Width = 45 });
            gridCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "Nombre", FillWeight = 45 });
            gridCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Unit.", DataPropertyName = "Precio", Width = 95 });
            gridCatalogo.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", Width = 70 });

            gridCatalogo.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                if (gridCatalogo.Columns[e.ColumnIndex].HeaderText == "Precio Unit." && e.Value is decimal precio)
                {
                    e.Value = $"${precio:N2}";
                    e.FormattingApplied = true;
                }
            };
            gridCatalogo.SelectionChanged += GridCatalogo_SelectionChanged;

            // Panel inferior para agregar al carrito
            var panelAgregar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                Padding = new Padding(8),
                BackColor = Theme.CardDarker
            };

            var lblCant = new Label
            {
                Text = "Cantidad:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(12, 20),
                AutoSize = true
            };

            numCantidad = new NumericUpDown
            {
                Location = new Point(90, 18),
                Size = new Size(80, 26),
                Minimum = 1,
                Maximum = 500,
                Value = 1,
                Font = Theme.BodyBoldFont,
                BackColor = Theme.CardBg,
                ForeColor = Theme.BrightCyan
            };

            btnAgregarAlCarrito = new Button
            {
                Text = "➕ Agregar al Carrito",
                Location = new Point(190, 14),
                Size = new Size(220, 36),
                Enabled = false
            };
            Theme.StyleButton(btnAgregarAlCarrito, Theme.PrimaryCyan, Theme.BgDark);
            btnAgregarAlCarrito.Click += BtnAgregarAlCarrito_Click;

            panelAgregar.Controls.Add(lblCant);
            panelAgregar.Controls.Add(numCantidad);
            panelAgregar.Controls.Add(btnAgregarAlCarrito);

            gbCatalogo.Controls.Add(gridCatalogo);
            gbCatalogo.Controls.Add(panelAgregar);
            gbCatalogo.Controls.Add(panelFiltro);
            panelIzquierdo.Controls.Add(gbCatalogo);
            splitPrincipal.Panel1.Controls.Add(panelIzquierdo);

            // ==========================================
            // LADO DERECHO: CARRITO Y COBRO
            // ==========================================
            var panelDerecho = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Theme.BgDark
            };

            var splitDerecho = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 290,
                BackColor = Theme.BgDark
            };

            // 1. Grid del Carrito
            var gbCarrito = new GroupBox
            {
                Text = " 2. Productos en la Venta ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(10)
            };

            gridCarrito = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            Theme.StyleGrid(gridCarrito);

            gridCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "NombreProducto", FillWeight = 42 });
            gridCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Unit.", DataPropertyName = "PrecioUnitario", Width = 95 });
            gridCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cant.", DataPropertyName = "Cantidad", Width = 65 });
            gridCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 95 });

            gridCarrito.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                if ((gridCarrito.Columns[e.ColumnIndex].HeaderText == "Precio Unit." || gridCarrito.Columns[e.ColumnIndex].HeaderText == "Subtotal") && e.Value is decimal val)
                {
                    e.Value = $"${val:N2}";
                    e.FormattingApplied = true;
                }
            };

            var panelBotonesCarrito = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                Padding = new Padding(6)
            };

            btnQuitarItem = new Button
            {
                Text = "❌ Quitar Seleccionado",
                Location = new Point(6, 7),
                Size = new Size(180, 32)
            };
            Theme.StyleButton(btnQuitarItem, Theme.CardDarker, Theme.AccentDanger);
            btnQuitarItem.Click += BtnQuitarItem_Click;

            btnVaciarCarrito = new Button
            {
                Text = "🗑️ Vaciar Carrito",
                Location = new Point(200, 7),
                Size = new Size(140, 32)
            };
            Theme.StyleButton(btnVaciarCarrito, Theme.CardDarker, Theme.TextMuted);
            btnVaciarCarrito.Click += (s, e) => { _carrito.Clear(); ActualizarVistaCarrito(); };

            panelBotonesCarrito.Controls.Add(btnQuitarItem);
            panelBotonesCarrito.Controls.Add(btnVaciarCarrito);

            gbCarrito.Controls.Add(gridCarrito);
            gbCarrito.Controls.Add(panelBotonesCarrito);
            splitDerecho.Panel1.Controls.Add(gbCarrito);

            // 2. Panel de Cobro
            var gbCobro = new GroupBox
            {
                Text = " 3. Total y Liquidación ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(15)
            };

            // Gran total
            var lblTotalEtiqueta = new Label
            {
                Text = "TOTAL DE PRECIO:",
                Font = Theme.SubtitleFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(20, 25),
                AutoSize = true
            };

            lblTotal = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Theme.BrightCyan,
                Location = new Point(20, 50),
                AutoSize = true
            };

            // Precio cancelado (Monto pagado)
            var lblPrecioCancelado = new Label
            {
                Text = "Precio Cancelado (Paga con):",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(220, 25),
                AutoSize = true
            };

            txtPrecioCancelado = new TextBox
            {
                Location = new Point(220, 55),
                Size = new Size(160, 32),
                Font = Theme.PriceFont,
                Text = "0.00"
            };
            Theme.StyleTextBox(txtPrecioCancelado);
            txtPrecioCancelado.TextChanged += TxtPrecioCancelado_TextChanged;

            // Cambio devuelto
            var lblCambioEtiqueta = new Label
            {
                Text = "Cambio a entregar:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(400, 25),
                AutoSize = true
            };

            lblCambio = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Theme.AccentSuccess,
                Location = new Point(400, 52),
                AutoSize = true
            };

            // Tipo de pago
            var lblTipoPago = new Label
            {
                Text = "Tipo de Pago:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(20, 110),
                AutoSize = true
            };

            cmbTipoPago = new ComboBox
            {
                Location = new Point(130, 107),
                Size = new Size(170, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTipoPago.Items.AddRange(new object[] { "Efectivo", "Tarjeta de Débito", "Tarjeta de Crédito", "Transferencia" });
            cmbTipoPago.SelectedIndex = 0;
            Theme.StyleComboBox(cmbTipoPago);

            // Notas
            var lblNotas = new Label
            {
                Text = "Notas / Mesa:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(320, 110),
                AutoSize = true
            };

            txtNotas = new TextBox
            {
                Location = new Point(430, 107),
                Size = new Size(180, 26),
                Font = Theme.BodyFont
            };
            Theme.StyleTextBox(txtNotas);

            // Botón Realizar Venta con gradiente premium
            btnRealizarVenta = new Button
            {
                Text = "  ⚡  REALIZAR VENTA",
                Location = new Point(20, 155),
                Size = new Size(420, 52),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold)
            };
            Theme.StyleButtonGradient(btnRealizarVenta, Theme.AccentSuccess, Color.FromArgb(20, 220, 140), Color.White);
            btnRealizarVenta.Click += BtnRealizarVenta_Click;

            gbCobro.Controls.Add(lblTotalEtiqueta);
            gbCobro.Controls.Add(lblTotal);
            gbCobro.Controls.Add(lblPrecioCancelado);
            gbCobro.Controls.Add(txtPrecioCancelado);
            gbCobro.Controls.Add(lblCambioEtiqueta);
            gbCobro.Controls.Add(lblCambio);
            gbCobro.Controls.Add(lblTipoPago);
            gbCobro.Controls.Add(cmbTipoPago);
            gbCobro.Controls.Add(lblNotas);
            gbCobro.Controls.Add(txtNotas);
            gbCobro.Controls.Add(btnRealizarVenta);

            splitDerecho.Panel2.Controls.Add(gbCobro);
            panelDerecho.Controls.Add(splitDerecho);
            splitPrincipal.Panel2.Controls.Add(panelDerecho);

            this.Controls.Add(splitPrincipal);
            this.Controls.Add(panelTop);
        }

        private void GridCatalogo_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridCatalogo.SelectedRows.Count > 0)
            {
                _productoSeleccionado = gridCatalogo.SelectedRows[0].DataBoundItem as Producto;
                btnAgregarAlCarrito.Enabled = _productoSeleccionado != null && _productoSeleccionado.Stock > 0;
                if (_productoSeleccionado != null)
                {
                    numCantidad.Maximum = Math.Max(1, _productoSeleccionado.Stock);
                    numCantidad.Value = 1;
                }
            }
        }

        private void BtnAgregarAlCarrito_Click(object? sender, EventArgs e)
        {
            if (_productoSeleccionado == null) return;

            int cantidad = (int)numCantidad.Value;
            if (cantidad <= 0) return;

            // Verificar si ya está en el carrito
            var existente = _carrito.FirstOrDefault(x => x.IdProducto == _productoSeleccionado.IdProducto);
            int cantidadTotalEnCarrito = (existente?.Cantidad ?? 0) + cantidad;

            if (cantidadTotalEnCarrito > _productoSeleccionado.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Solo hay {_productoSeleccionado.Stock} disponibles de '{_productoSeleccionado.Nombre}'.", "Stock Máximo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (existente != null)
            {
                existente.Cantidad += cantidad;
                existente.Subtotal = existente.Cantidad * existente.PrecioUnitario;
            }
            else
            {
                _carrito.Add(new DetalleVenta
                {
                    IdProducto = _productoSeleccionado.IdProducto,
                    NombreProducto = _productoSeleccionado.Nombre,
                    PrecioUnitario = _productoSeleccionado.Precio,
                    Cantidad = cantidad,
                    Subtotal = _productoSeleccionado.Precio * cantidad
                });
            }

            ActualizarVistaCarrito();
        }

        private void BtnQuitarItem_Click(object? sender, EventArgs e)
        {
            if (gridCarrito.SelectedRows.Count > 0)
            {
                var item = gridCarrito.SelectedRows[0].DataBoundItem as DetalleVenta;
                if (item != null)
                {
                    _carrito.Remove(item);
                    ActualizarVistaCarrito();
                }
            }
        }

        private void ActualizarVistaCarrito()
        {
            gridCarrito.DataSource = null;
            gridCarrito.DataSource = _carrito.ToList();

            decimal total = _carrito.Sum(x => x.Subtotal);
            lblTotal.Text = $"${total:N2}";

            // Sugerir el total en el monto cancelado si aún está en 0
            if (decimal.TryParse(txtPrecioCancelado.Text.Trim(), out decimal cancelado) && (cancelado == 0 || cancelado < total))
            {
                txtPrecioCancelado.Text = total.ToString("0.00");
            }

            CalcularCambio();
        }

        private void TxtPrecioCancelado_TextChanged(object? sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void CalcularCambio()
        {
            decimal total = _carrito.Sum(x => x.Subtotal);
            if (decimal.TryParse(txtPrecioCancelado.Text.Trim(), out decimal cancelado))
            {
                decimal cambio = cancelado - total;
                if (cambio >= 0)
                {
                    lblCambio.Text = $"${cambio:N2}";
                    lblCambio.ForeColor = Theme.AccentSuccess;
                }
                else
                {
                    lblCambio.Text = $"-${Math.Abs(cambio):N2} (Falta)";
                    lblCambio.ForeColor = Theme.AccentDanger;
                }
            }
            else
            {
                lblCambio.Text = "$0.00";
            }
        }

        private void BtnRealizarVenta_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito de venta está vacío. Agrega al menos un producto.", "Venta Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _carrito.Sum(x => x.Subtotal);
            if (!decimal.TryParse(txtPrecioCancelado.Text.Trim(), out decimal precioCancelado) || precioCancelado < total)
            {
                MessageBox.Show($"El precio cancelado (${precioCancelado:N2}) es menor al total de la venta (${total:N2}).", "Monto Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal cambio = precioCancelado - total;
            string tipoPago = cmbTipoPago.SelectedItem?.ToString() ?? "Efectivo";
            string ticketNum = "TICK-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

            var venta = new Venta
            {
                NumeroTicket = ticketNum,
                IdUsuario = _usuarioActual.IdUsuario,
                FechaHora = DateTime.Now,
                Total = total,
                MontoPagado = precioCancelado,
                Cambio = cambio,
                TipoPago = tipoPago,
                Notas = string.IsNullOrWhiteSpace(txtNotas.Text) ? null : txtNotas.Text.Trim(),
                Detalles = _carrito
            };

            var res = DatabaseHelper.RegistrarVenta(venta);
            if (res.Exito)
            {
                // Mensaje tipo recibo / ticket
                string resumenTicket = $"========================================\n" +
                                       $"           ❄️ ICE BAR ❄️\n" +
                                       $"       COMPROBANTE DE VENTA\n" +
                                       $"========================================\n" +
                                       $"Ticket: {ticketNum}\n" +
                                       $"Fecha/Hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                                       $"Atendido por: {_usuarioActual.NombreCompleto}\n" +
                                       $"Método de Pago: {tipoPago}\n" +
                                       $"----------------------------------------\n";

                foreach (var d in _carrito)
                {
                    resumenTicket += $"{d.Cantidad}x {d.NombreProducto.PadRight(25)} ${d.Subtotal:N2}\n";
                }

                resumenTicket += $"----------------------------------------\n" +
                                 $"TOTAL:            ${total:N2}\n" +
                                 $"PRECIO CANCELADO: ${precioCancelado:N2}\n" +
                                 $"CAMBIO:           ${cambio:N2}\n" +
                                 $"========================================\n" +
                                 $"¡Venta registrada y stock actualizado!";

                MessageBox.Show(resumenTicket, "Ticket de Venta Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar venta
                _carrito.Clear();
                ActualizarVistaCarrito();
                txtNotas.Text = "";
                txtPrecioCancelado.Text = "0.00";
                CargarCatalogo();
            }
            else
            {
                MessageBox.Show($"No se pudo registrar la venta:\n{res.Mensaje}", "Error en Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarCatalogo()
        {
            try
            {
                var lista = DatabaseHelper.ObtenerProductos(txtBuscarProducto.Text.Trim());
                gridCatalogo.AutoGenerateColumns = false;
                gridCatalogo.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar catálogo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
