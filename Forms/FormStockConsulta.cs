using System;
using System.Drawing;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormStockConsulta : UserControl
    {
        private TextBox txtBuscar = null!;
        private DataGridView gridProductos = null!;
        private Label lblTotal = null!;
        private Button btnRefrescar = null!;

        public FormStockConsulta()
        {
            InitializeComponent();
            this.Load += (s, e) => CargarStock();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;

            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Theme.CardBg
            };
            panelTop.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelTop.Height - 1, panelTop.Width, panelTop.Height - 1);
                using var penAcc = new Pen(Color.FromArgb(80, Theme.PrimaryCyan), 2);
                e.Graphics.DrawLine(penAcc, 20, panelTop.Height - 1, 350, panelTop.Height - 1);
            };

            var lblTitulo = new Label
            {
                Text = "📦  Catálogo de Stock y Precios",
                Font = Theme.TitleFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(20, 12)
            };

            var lblBuscar = new Label
            {
                Text = "🔍  Buscar producto:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextMuted,
                AutoSize = true,
                Location = new Point(20, 55)
            };

            txtBuscar = new TextBox
            {
                Location = new Point(175, 52),
                Size = new Size(380, 30),
                Font = Theme.BodyFont
            };
            Theme.StyleTextBox(txtBuscar);
            txtBuscar.TextChanged += (s, e) => CargarStock();

            btnRefrescar = new Button
            {
                Text = "🔄  Actualizar",
                Location = new Point(568, 50),
                Size = new Size(120, 34)
            };
            Theme.StyleButton(btnRefrescar, Theme.PrimaryCyan, Theme.BgDark);
            btnRefrescar.Click += (s, e) => { txtBuscar.Text = ""; CargarStock(); };

            lblTotal = new Label
            {
                Text = "0 productos",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(705, 57)
            };

            panelTop.Controls.Add(lblTitulo);
            panelTop.Controls.Add(lblBuscar);
            panelTop.Controls.Add(txtBuscar);
            panelTop.Controls.Add(btnRefrescar);
            panelTop.Controls.Add(lblTotal);

            var panelCenter = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            gridProductos = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            Theme.StyleGrid(gridProductos);

            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "IdProducto", Width = 60, FillWeight = 10 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto / Descripción", DataPropertyName = "Nombre", FillWeight = 40 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Categoría", DataPropertyName = "Categoria", Width = 130, FillWeight = 18 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio Unitario", DataPropertyName = "Precio", Width = 120, FillWeight = 14 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock Actual", DataPropertyName = "Stock", Width = 110, FillWeight = 13 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "EstadoStock", Width = 110, FillWeight = 13 });

            gridProductos.CellFormatting += GridProductos_CellFormatting;

            panelCenter.Controls.Add(gridProductos);

            this.Controls.Add(panelCenter);
            this.Controls.Add(panelTop);
        }

        private void GridProductos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            // Formato de Moneda para Precio
            if (gridProductos.Columns[e.ColumnIndex].HeaderText == "Precio Unitario" && e.Value is decimal precio)
            {
                e.Value = $"${precio:N2}";
                e.FormattingApplied = true;
            }

            // Color del estado de stock
            if (gridProductos.Columns[e.ColumnIndex].HeaderText == "Estado")
            {
                string estado = e.Value.ToString() ?? "";
                if (estado == "Agotado")
                {
                    e.CellStyle.ForeColor = Theme.AccentDanger;
                    e.CellStyle.Font = Theme.BodyBoldFont;
                }
                else if (estado == "Bajo Stock")
                {
                    e.CellStyle.ForeColor = Theme.AccentWarning;
                    e.CellStyle.Font = Theme.BodyBoldFont;
                }
                else
                {
                    e.CellStyle.ForeColor = Theme.AccentSuccess;
                    e.CellStyle.Font = Theme.BodyBoldFont;
                }
            }
        }

        public void CargarStock()
        {
            try
            {
                var lista = DatabaseHelper.ObtenerProductos(txtBuscar.Text.Trim());
                gridProductos.AutoGenerateColumns = false;
                gridProductos.DataSource = lista;
                lblTotal.Text = $"{lista.Count}  en catálogo";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
