using System;
using System.Drawing;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormStockAdmin : UserControl
    {
        private DataGridView gridProductos = null!;
        private TextBox txtBuscar = null!;

        // Campos para Agregar / Editar
        private TextBox txtNombre = null!;
        private TextBox txtPrecio = null!;
        private TextBox txtStockInicial = null!;
        private ComboBox cmbCategoria = null!;
        private Button btnAgregar = null!;
        private Button btnGuardarCambios = null!;
        private Button btnEliminar = null!;
        private Button btnLimpiar = null!;

        // Campos para Actualizar Stock (Sumar cantidad a la anterior)
        private Label lblProductoSeleccionado = null!;
        private Label lblStockActual = null!;
        private NumericUpDown numCantidadSumar = null!;
        private Button btnSumarStock = null!;

        private Producto? _productoSeleccionado = null;

        public FormStockAdmin()
        {
            InitializeComponent();
            this.Load += (s, e) => CargarProductos();
        }

        private void InitializeComponent()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;

            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Theme.CardBg
            };
            panelTop.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelTop.Height - 1, panelTop.Width, panelTop.Height - 1);
                using var penAcc = new Pen(Color.FromArgb(80, Theme.PrimaryCyan), 2);
                e.Graphics.DrawLine(penAcc, 20, panelTop.Height - 1, 480, panelTop.Height - 1);
            };

            var lblTitulo = new Label
            {
                Text = "📊  Control de Stock y Catálogo",
                Font = Theme.TitleFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            panelTop.Controls.Add(lblTitulo);

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 440,
                BackColor = Theme.BgDark
            };

            // ==========================================
            // PANEL IZQUIERDO: FORMULARIOS DE GESTIÓN
            // ==========================================
            var panelLeft = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(15),
                BackColor = Theme.BgDark
            };

            // GRUPO 1: AGREGAR / EDITAR PRODUCTO
            var gbEdicion = new GroupBox
            {
                Text = " Datos del Producto (Agregar / Editar) ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Location = new Point(10, 10),
                Size = new Size(410, 315),
                BackColor = Theme.CardBg
            };

            var lblNom = new Label { Text = "Nombre y descripción (ej. Coca Cola 600ml):", ForeColor = Theme.TextWhite, Location = new Point(15, 25), AutoSize = true, Font = Theme.BodyFont };
            txtNombre = new TextBox { Location = new Point(15, 48), Size = new Size(380, 26) };
            Theme.StyleTextBox(txtNombre);

            var lblPre = new Label { Text = "Precio Unitario ($):", ForeColor = Theme.TextWhite, Location = new Point(15, 82), AutoSize = true, Font = Theme.BodyFont };
            txtPrecio = new TextBox { Location = new Point(15, 105), Size = new Size(180, 26) };
            Theme.StyleTextBox(txtPrecio);

            var lblCat = new Label { Text = "Categoría:", ForeColor = Theme.TextWhite, Location = new Point(210, 82), AutoSize = true, Font = Theme.BodyFont };
            cmbCategoria = new ComboBox { Location = new Point(210, 105), Size = new Size(185, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategoria.Items.AddRange(new object[] { "Refrescos", "Bebidas", "Cervezas", "Licores", "Cocteleria", "Snacks", "Otros" });
            cmbCategoria.SelectedIndex = 0;
            Theme.StyleComboBox(cmbCategoria);

            var lblStk = new Label { Text = "Stock Inicial (solo al agregar nuevo):", ForeColor = Theme.TextMuted, Location = new Point(15, 140), AutoSize = true, Font = Theme.SmallFont };
            txtStockInicial = new TextBox { Location = new Point(15, 162), Size = new Size(180, 26), Text = "0" };
            Theme.StyleTextBox(txtStockInicial);

            btnAgregar = new Button { Text = "➕ Agregar Producto", Location = new Point(15, 205), Size = new Size(180, 38) };
            Theme.StyleButton(btnAgregar, Theme.AccentSuccess, Color.White);
            btnAgregar.Click += BtnAgregar_Click;

            btnGuardarCambios = new Button { Text = "💾 Guardar Edición", Location = new Point(210, 205), Size = new Size(185, 38), Enabled = false };
            Theme.StyleButton(btnGuardarCambios, Theme.PrimaryCyan, Theme.BgDark);
            btnGuardarCambios.Click += BtnGuardarCambios_Click;

            btnEliminar = new Button { Text = "🗑️ Eliminar Producto", Location = new Point(15, 255), Size = new Size(180, 36), Enabled = false };
            Theme.StyleButton(btnEliminar, Theme.AccentDanger, Color.White);
            btnEliminar.Click += BtnEliminar_Click;

            btnLimpiar = new Button { Text = "🧹 Limpiar Campos", Location = new Point(210, 255), Size = new Size(185, 36) };
            Theme.StyleButton(btnLimpiar, Theme.CardDarker, Theme.TextMuted);
            btnLimpiar.Click += (s, e) => LimpiarFormulario();

            gbEdicion.Controls.Add(lblNom);
            gbEdicion.Controls.Add(txtNombre);
            gbEdicion.Controls.Add(lblPre);
            gbEdicion.Controls.Add(txtPrecio);
            gbEdicion.Controls.Add(lblCat);
            gbEdicion.Controls.Add(cmbCategoria);
            gbEdicion.Controls.Add(lblStk);
            gbEdicion.Controls.Add(txtStockInicial);
            gbEdicion.Controls.Add(btnAgregar);
            gbEdicion.Controls.Add(btnGuardarCambios);
            gbEdicion.Controls.Add(btnEliminar);
            gbEdicion.Controls.Add(btnLimpiar);

            // GRUPO 2: ACTUALIZAR STOCK (Sumar cantidad a la anterior)
            var gbStock = new GroupBox
            {
                Text = " Actualizar Stock (Sumar Cantidad Acumulativa) ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Location = new Point(10, 335),
                Size = new Size(410, 215),
                BackColor = Theme.CardBg
            };

            lblProductoSeleccionado = new Label
            {
                Text = "Selecciona un producto en la tabla de la derecha para sumar stock.",
                Font = Theme.BodyFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(15, 28),
                Size = new Size(380, 42)
            };

            lblStockActual = new Label
            {
                Text = "Stock Actual: -",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.AccentWarning,
                Location = new Point(15, 75),
                AutoSize = true
            };

            var lblSumarInfo = new Label
            {
                Text = "Cantidad a SUMAR al inventario:",
                Font = Theme.BodyFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(15, 105),
                AutoSize = true
            };

            numCantidadSumar = new NumericUpDown
            {
                Location = new Point(15, 130),
                Size = new Size(160, 28),
                Minimum = 1,
                Maximum = 10000,
                Value = 10,
                Font = Theme.BodyBoldFont,
                BackColor = Theme.CardDarker,
                ForeColor = Theme.BrightCyan
            };

            btnSumarStock = new Button
            {
                Text = "➕ Sumar al Stock Anterior",
                Location = new Point(190, 126),
                Size = new Size(205, 35),
                Enabled = false
            };
            Theme.StyleButton(btnSumarStock, Theme.PrimaryCyan, Theme.BgDark);
            btnSumarStock.Click += BtnSumarStock_Click;

            var lblNotaStock = new Label
            {
                Text = "💡 El nuevo stock será: (Stock Actual + Cantidad Ingresada).",
                Font = Theme.SmallFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(15, 175),
                Size = new Size(380, 25)
            };

            gbStock.Controls.Add(lblProductoSeleccionado);
            gbStock.Controls.Add(lblStockActual);
            gbStock.Controls.Add(lblSumarInfo);
            gbStock.Controls.Add(numCantidadSumar);
            gbStock.Controls.Add(btnSumarStock);
            gbStock.Controls.Add(lblNotaStock);

            panelLeft.Controls.Add(gbEdicion);
            panelLeft.Controls.Add(gbStock);
            split.Panel1.Controls.Add(panelLeft);

            // ==========================================
            // PANEL DERECHO: GRID Y BÚSQUEDA
            // ==========================================
            var panelRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 15, 20, 20),
                BackColor = Theme.BgDark
            };

            var panelBarraBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            var lblBuscar = new Label
            {
                Text = "🔍 Buscar en catálogo:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(5, 10),
                AutoSize = true
            };

            txtBuscar = new TextBox
            {
                Location = new Point(180, 8),
                Size = new Size(320, 26),
                Font = Theme.BodyFont
            };
            Theme.StyleTextBox(txtBuscar);
            txtBuscar.TextChanged += (s, e) => CargarProductos();

            panelBarraBusqueda.Controls.Add(lblBuscar);
            panelBarraBusqueda.Controls.Add(txtBuscar);

            gridProductos = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            Theme.StyleGrid(gridProductos);

            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "IdProducto", Width = 55 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto / Descripción", DataPropertyName = "Nombre", FillWeight = 38 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Categoría", DataPropertyName = "Categoria", Width = 110 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "Precio", Width = 95 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = "Stock", Width = 85 });
            gridProductos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "EstadoStock", Width = 95 });

            gridProductos.CellFormatting += GridProductos_CellFormatting;
            gridProductos.SelectionChanged += GridProductos_SelectionChanged;

            panelRight.Controls.Add(gridProductos);
            panelRight.Controls.Add(panelBarraBusqueda);
            split.Panel2.Controls.Add(panelRight);

            this.Controls.Add(split);
            this.Controls.Add(panelTop);
        }

        private void GridProductos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;
            if (gridProductos.Columns[e.ColumnIndex].HeaderText == "Precio" && e.Value is decimal precio)
            {
                e.Value = $"${precio:N2}";
                e.FormattingApplied = true;
            }
        }

        private void GridProductos_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridProductos.SelectedRows.Count > 0)
            {
                _productoSeleccionado = gridProductos.SelectedRows[0].DataBoundItem as Producto;
                if (_productoSeleccionado != null)
                {
                    txtNombre.Text = _productoSeleccionado.Nombre;
                    txtPrecio.Text = _productoSeleccionado.Precio.ToString("0.00");
                    txtStockInicial.Text = _productoSeleccionado.Stock.ToString();
                    txtStockInicial.Enabled = false; // El stock se actualiza en el panel específico

                    int idx = cmbCategoria.FindStringExact(_productoSeleccionado.Categoria);
                    cmbCategoria.SelectedIndex = idx >= 0 ? idx : 0;

                    btnGuardarCambios.Enabled = true;
                    btnEliminar.Enabled = true;

                    // Panel de Aumentar Stock
                    lblProductoSeleccionado.Text = $"Producto: {_productoSeleccionado.Nombre}";
                    lblProductoSeleccionado.ForeColor = Theme.BrightCyan;
                    lblStockActual.Text = $"Stock Actual en BD: {_productoSeleccionado.Stock} unidades";
                    btnSumarStock.Enabled = true;
                }
            }
        }

        private void LimpiarFormulario()
        {
            _productoSeleccionado = null;
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStockInicial.Text = "0";
            txtStockInicial.Enabled = true;
            cmbCategoria.SelectedIndex = 0;

            btnGuardarCambios.Enabled = false;
            btnEliminar.Enabled = false;

            lblProductoSeleccionado.Text = "Selecciona un producto en la tabla de la derecha para sumar stock.";
            lblProductoSeleccionado.ForeColor = Theme.TextMuted;
            lblStockActual.Text = "Stock Actual: -";
            btnSumarStock.Enabled = false;
            gridProductos.ClearSelection();
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Debes ingresar el nombre y descripción del producto (ej: Coca Cola 600ml).", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingresa un precio válido mayor o igual a 0.", "Precio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStockInicial.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Ingresa una cantidad de stock inicial válida.", "Stock Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseHelper.AgregarProducto(nombre, precio, stock, cmbCategoria.SelectedItem?.ToString() ?? "General");
                MessageBox.Show($"¡Producto '{nombre}' agregado exitosamente al catálogo!", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardarCambios_Click(object? sender, EventArgs e)
        {
            if (_productoSeleccionado == null) return;

            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del producto no puede quedar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingresa un precio válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseHelper.EditarProducto(_productoSeleccionado.IdProducto, nombre, precio, cmbCategoria.SelectedItem?.ToString() ?? "General");
                MessageBox.Show("Producto actualizado correctamente.", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (_productoSeleccionado == null) return;

            var r = MessageBox.Show($"¿Estás seguro de que deseas eliminar '{_productoSeleccionado.Nombre}' del catálogo?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.EliminarProducto(_productoSeleccionado.IdProducto);
                    MessageBox.Show("Producto eliminado.", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSumarStock_Click(object? sender, EventArgs e)
        {
            if (_productoSeleccionado == null) return;

            int cantidad = (int)numCantidadSumar.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("Ingresa una cantidad mayor a 0 para sumar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseHelper.AumentarStock(_productoSeleccionado.IdProducto, cantidad);
                int nuevoStock = _productoSeleccionado.Stock + cantidad;
                MessageBox.Show($"¡Stock actualizado con éxito!\nProducto: {_productoSeleccionado.Nombre}\nAnterior: {_productoSeleccionado.Stock}\nSumado: +{cantidad}\nNuevo Stock Total: {nuevoStock}", "ICE BAR - Stock Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int idActual = _productoSeleccionado.IdProducto;
                CargarProductos();

                // Mantener selección actualizada
                foreach (DataGridViewRow row in gridProductos.Rows)
                {
                    if (row.DataBoundItem is Producto p && p.IdProducto == idActual)
                    {
                        row.Selected = true;
                        GridProductos_SelectionChanged(this, EventArgs.Empty);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al sumar stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarProductos()
        {
            try
            {
                var lista = DatabaseHelper.ObtenerProductos(txtBuscar.Text.Trim());
                gridProductos.AutoGenerateColumns = false;
                gridProductos.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
