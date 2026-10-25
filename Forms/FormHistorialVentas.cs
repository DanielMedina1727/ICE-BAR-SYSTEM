using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormHistorialVentas : UserControl
    {
        private DateTimePicker dtpDesde = null!;
        private DateTimePicker dtpHasta = null!;
        private Button btnFiltrar = null!;
        private Button btnVerHoy = null!;
        private Label lblTotalRecaudado = null!;
        private Label lblCantidadVentas = null!;

        private DataGridView gridVentas = null!;
        private DataGridView gridDetalles = null!;
        private Label lblDetalleTicket = null!;

        public FormHistorialVentas()
        {
            InitializeComponent();
            this.Load += (s, e) => CargarHistorial();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;

            // ── Panel de cabecera con título y controles de filtro ──
            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Theme.CardBg
            };
            panelTop.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelTop.Height - 1, panelTop.Width, panelTop.Height - 1);
                using var penAcc = new Pen(Color.FromArgb(80, Theme.PrimaryCyan), 2);
                e.Graphics.DrawLine(penAcc, 20, panelTop.Height - 1, 400, panelTop.Height - 1);
            };

            var lblTitulo = new Label
            {
                Text = "📑  Historial y Auditoría de Ventas",
                Font = Theme.TitleFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(20, 12)
            };

            var lblDesde = new Label { Text = "Desde:", Location = new Point(20, 55), AutoSize = true, ForeColor = Theme.TextMuted, Font = Theme.BodyBoldFont };
            dtpDesde = new DateTimePicker { Location = new Point(78, 52), Format = DateTimePickerFormat.Short, Size = new Size(120, 28), Value = DateTime.Today.AddDays(-7) };
            Theme.StyleDatePicker(dtpDesde);

            var lblHasta = new Label { Text = "Hasta:", Location = new Point(212, 55), AutoSize = true, ForeColor = Theme.TextMuted, Font = Theme.BodyBoldFont };
            dtpHasta = new DateTimePicker { Location = new Point(268, 52), Format = DateTimePickerFormat.Short, Size = new Size(120, 28), Value = DateTime.Today };
            Theme.StyleDatePicker(dtpHasta);

            btnFiltrar = new Button { Text = "🔍  Filtrar", Location = new Point(403, 50), Size = new Size(100, 32) };
            Theme.StyleButton(btnFiltrar, Theme.PrimaryCyan, Theme.BgDark);
            btnFiltrar.Click += (s, e) => CargarHistorial();

            btnVerHoy = new Button { Text = "📅  Hoy", Location = new Point(513, 50), Size = new Size(90, 32) };
            Theme.StyleButton(btnVerHoy, Theme.CardDarker, Theme.BrightCyan);
            btnVerHoy.Click += (s, e) =>
            {
                dtpDesde.Value = DateTime.Today;
                dtpHasta.Value = DateTime.Today;
                CargarHistorial();
            };

            // KPIs como labels compactos
            lblCantidadVentas = new Label { Text = "─  ventas", Location = new Point(625, 55), AutoSize = true, ForeColor = Theme.AccentBlue, Font = Theme.BodyBoldFont };
            lblTotalRecaudado = new Label { Text = "$0.00  recaudado", Location = new Point(760, 55), AutoSize = true, ForeColor = Theme.AccentSuccess, Font = Theme.SubtitleFont };

            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(lblDesde);
            panelTop.Controls.Add(dtpDesde);
            panelTop.Controls.Add(lblHasta);
            panelTop.Controls.Add(dtpHasta);
            panelTop.Controls.Add(btnFiltrar);
            panelTop.Controls.Add(btnVerHoy);
            panelTop.Controls.Add(lblCantidadVentas);
            panelTop.Controls.Add(lblTotalRecaudado);

            // Split: Arriba lista de ventas, Abajo desglose de productos de la venta seleccionada
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 320,
                BackColor = Theme.BgDark
            };

            // Grid Ventas
            var panelVentas = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 15, 20, 10), BackColor = Theme.BgDark };
            gridVentas = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true };
            Theme.StyleGrid(gridVentas);

            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "IdVenta", Width = 55 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ticket", DataPropertyName = "NumeroTicket", Width = 160 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha y Hora", DataPropertyName = "FechaHora", Width = 150 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Atendido por", DataPropertyName = "NombreUsuario", Width = 140 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tipo de Pago", DataPropertyName = "TipoPago", Width = 120 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Venta", DataPropertyName = "Total", Width = 110 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Cancelado", DataPropertyName = "MontoPagado", Width = 120 });
            gridVentas.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cambio", DataPropertyName = "Cambio", Width = 95 });

            gridVentas.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                var col = gridVentas.Columns[e.ColumnIndex].HeaderText;
                if ((col == "Total Venta" || col == "Precio Cancelado" || col == "Cambio") && e.Value is decimal val)
                {
                    e.Value = $"${val:N2}";
                    e.FormattingApplied = true;
                }
                else if (col == "Fecha y Hora" && e.Value is DateTime dt)
                {
                    e.Value = dt.ToString("dd/MM/yyyy HH:mm:ss");
                    e.FormattingApplied = true;
                }
            };
            gridVentas.SelectionChanged += GridVentas_SelectionChanged;

            panelVentas.Controls.Add(gridVentas);
            split.Panel1.Controls.Add(panelVentas);

            // Detalle de la venta
            var panelDetalle = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 5, 20, 15), BackColor = Theme.BgDark };
            var gbDetalle = new GroupBox
            {
                Text = " Detalle de Productos del Ticket Seleccionado ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(12)
            };

            lblDetalleTicket = new Label
            {
                Text = "Selecciona una venta arriba para ver los productos vendidos.",
                Font = Theme.BodyFont,
                ForeColor = Theme.TextMuted,
                Dock = DockStyle.Top,
                Height = 24
            };

            gridDetalles = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true };
            Theme.StyleGrid(gridDetalles);

            gridDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = "NombreProducto", FillWeight = 45 });
            gridDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Unitario", DataPropertyName = "PrecioUnitario", Width = 130 });
            gridDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", DataPropertyName = "Cantidad", Width = 90 });
            gridDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 130 });

            gridDetalles.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                var col = gridDetalles.Columns[e.ColumnIndex].HeaderText;
                if ((col == "Precio Unitario" || col == "Subtotal") && e.Value is decimal val)
                {
                    e.Value = $"${val:N2}";
                    e.FormattingApplied = true;
                }
            };

            gbDetalle.Controls.Add(gridDetalles);
            gbDetalle.Controls.Add(lblDetalleTicket);
            panelDetalle.Controls.Add(gbDetalle);
            split.Panel2.Controls.Add(panelDetalle);

            this.Controls.Add(split);
            this.Controls.Add(panelTop);
        }

        private void GridVentas_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridVentas.SelectedRows.Count > 0)
            {
                var venta = gridVentas.SelectedRows[0].DataBoundItem as Venta;
                if (venta != null)
                {
                    lblDetalleTicket.Text = $"Ticket: {venta.NumeroTicket} | Fecha: {venta.FechaHora:dd/MM/yyyy HH:mm:ss} | Total: ${venta.Total:N2} | Pago: {venta.TipoPago}";
                    lblDetalleTicket.ForeColor = Theme.BrightCyan;

                    try
                    {
                        var detalles = DatabaseHelper.ObtenerDetallesDeVenta(venta.IdVenta);
                        gridDetalles.AutoGenerateColumns = false;
                        gridDetalles.DataSource = detalles;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void CargarHistorial()
        {
            try
            {
                var lista = DatabaseHelper.ObtenerHistorialVentas(dtpDesde.Value, dtpHasta.Value);
                gridVentas.AutoGenerateColumns = false;
                gridVentas.DataSource = lista;

                lblCantidadVentas.Text = $"{lista.Count}  ventas";
                lblTotalRecaudado.Text = $"${lista.Sum(x => x.Total):N2}  recaudado";

                if (lista.Count == 0)
                {
                    gridDetalles.DataSource = null;
                    lblDetalleTicket.Text = "No hay ventas registradas en el período seleccionado.";
                    lblDetalleTicket.ForeColor = Theme.TextMuted;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
