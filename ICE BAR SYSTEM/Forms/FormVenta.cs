using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;
using IceBarSystem.Helpers;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public partial class FormVenta : Form
    {
        private readonly Usuario _usuario;
        private readonly DataTable _dtCarrito;
        private readonly List<ProductoItem> _productosLista = new List<ProductoItem>();

        private class ProductoItem
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; } = "";
            public decimal Precio { get; set; }
            public int Stock { get; set; }

            public override string ToString() => $"{Nombre} (${Precio:N2})";
        }

        public FormVenta(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            _dtCarrito = new DataTable();
            _dtCarrito.Columns.Add("ID", typeof(int));
            _dtCarrito.Columns.Add("Producto", typeof(string));
            _dtCarrito.Columns.Add("Precio Unit.", typeof(decimal));
            _dtCarrito.Columns.Add("Cantidad", typeof(int));
            _dtCarrito.Columns.Add("Subtotal", typeof(decimal));

            dgvCarrito.DataSource = _dtCarrito;
            Theme.StyleDataGridView(dgvCarrito);

            if (cbTipoPago.Items.Count > 0)
                cbTipoPago.SelectedIndex = 0;

            this.Load += (s, e) => CargarProductosCombo();
        }

        private void CargarProductosCombo()
        {
            try
            {
                _productosLista.Clear();
                cbProducto.Items.Clear();

                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT IdProducto, Nombre, Precio, Stock FROM Productos WHERE Activo = 1 ORDER BY Nombre ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ProductoItem
                            {
                                IdProducto = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = reader.GetDecimal(2),
                                Stock = reader.GetInt32(3)
                            };
                            _productosLista.Add(item);
                            cbProducto.Items.Add(item);
                        }
                    }
                }

                if (cbProducto.Items.Count > 0)
                {
                    cbProducto.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbProducto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbProducto.SelectedItem is ProductoItem item)
            {
                lblStockInfo.Text = $"Stock actual: {item.Stock} | Precio: {item.Precio:C2}";
                lblStockInfo.ForeColor = item.Stock > 0 ? Color.FromArgb(100, 100, 100) : Color.Red;
            }
        }

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            if (cbProducto.SelectedItem is not ProductoItem item)
            {
                MessageBox.Show("Seleccione un producto para agregar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)txtCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calcular cantidad ya presente en el carrito
            int cantidadEnCarrito = 0;
            DataRow? filaExistente = null;
            foreach (DataRow row in _dtCarrito.Rows)
            {
                if ((int)row["ID"] == item.IdProducto)
                {
                    filaExistente = row;
                    cantidadEnCarrito = (int)row["Cantidad"];
                    break;
                }
            }

            if (cantidadEnCarrito + cantidad > item.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Solo hay {item.Stock} unidades disponibles de '{item.Nombre}'.",
                    "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (filaExistente != null)
            {
                int nuevaCantidad = cantidadEnCarrito + cantidad;
                filaExistente["Cantidad"] = nuevaCantidad;
                filaExistente["Subtotal"] = nuevaCantidad * item.Precio;
            }
            else
            {
                _dtCarrito.Rows.Add(item.IdProducto, item.Nombre, item.Precio, cantidad, item.Precio * cantidad);
            }

            txtCantidad.Value = 1;
            CalcularTotales();
        }

        private void btnQuitar_Click(object? sender, EventArgs e)
        {
            if (dgvCarrito.CurrentRow != null && dgvCarrito.CurrentRow.Index >= 0)
            {
                int index = dgvCarrito.CurrentRow.Index;
                _dtCarrito.Rows.RemoveAt(index);
                CalcularTotales();
            }
            else
            {
                MessageBox.Show("Seleccione un producto del carrito para quitarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiarCarrito_Click(object? sender, EventArgs e)
        {
            _dtCarrito.Rows.Clear();
            CalcularTotales();
        }

        private decimal ObtenerTotal()
        {
            decimal total = 0;
            foreach (DataRow row in _dtCarrito.Rows)
            {
                total += Convert.ToDecimal(row["Subtotal"]);
            }
            return total;
        }

        private void CalcularTotales()
        {
            decimal total = ObtenerTotal();
            lblTotal.Text = total.ToString("C2");
            CalcularCambio();
        }

        private void txtMontoPagado_TextChanged(object? sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void cbTipoPago_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string tipo = cbTipoPago.SelectedItem?.ToString() ?? "Efectivo";
            if (tipo != "Efectivo")
            {
                decimal total = ObtenerTotal();
                txtMontoPagado.Text = total.ToString("N2");
            }
            CalcularCambio();
        }

        private void CalcularCambio()
        {
            decimal total = ObtenerTotal();
            if (decimal.TryParse(txtMontoPagado.Text.Trim(), out decimal pagado))
            {
                decimal cambio = pagado - total;
                lblCambio.Text = cambio >= 0 ? cambio.ToString("C2") : "$0.00";
                lblCambio.ForeColor = cambio >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
            }
            else
            {
                lblCambio.Text = "$0.00";
            }
        }

        private void btnFinalizarVenta_Click(object? sender, EventArgs e)
        {
            if (_dtCarrito.Rows.Count == 0)
            {
                MessageBox.Show("El carrito de venta está vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = ObtenerTotal();
            if (!decimal.TryParse(txtMontoPagado.Text.Trim(), out decimal pagado) || pagado < total)
            {
                MessageBox.Show("El monto recibido debe ser mayor o igual al total a pagar.", "Validación de Pago",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoPagado.Focus();
                return;
            }

            decimal cambio = pagado - total;
            string tipoPago = cbTipoPago.SelectedItem?.ToString() ?? "Efectivo";
            string notas = txtNotas.Text.Trim();
            string ticket = $"TKT-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

            btnFinalizarVenta.Enabled = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Insertar Venta
                            string queryVenta = @"
                                INSERT INTO Ventas (NumeroTicket, IdUsuario, FechaHora, Total, MontoPagado, Cambio, TipoPago, Notas)
                                VALUES (@Ticket, @IdUsuario, GETDATE(), @Total, @MontoPagado, @Cambio, @TipoPago, @Notas);
                                SELECT SCOPE_IDENTITY();";

                            int idVenta;
                            using (SqlCommand cmdVenta = new SqlCommand(queryVenta, conn, tran))
                            {
                                cmdVenta.Parameters.AddWithValue("@Ticket", ticket);
                                cmdVenta.Parameters.AddWithValue("@IdUsuario", _usuario.IdUsuario);
                                cmdVenta.Parameters.AddWithValue("@Total", total);
                                cmdVenta.Parameters.AddWithValue("@MontoPagado", pagado);
                                cmdVenta.Parameters.AddWithValue("@Cambio", cambio);
                                cmdVenta.Parameters.AddWithValue("@TipoPago", tipoPago);
                                cmdVenta.Parameters.AddWithValue("@Notas", string.IsNullOrEmpty(notas) ? (object)DBNull.Value : notas);

                                idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                            }

                            // 2. Insertar Detalle de Ventas y Actualizar Stock
                            string queryDetalle = @"
                                INSERT INTO DetalleVentas (IdVenta, IdProducto, NombreProducto, PrecioUnitario, Cantidad, Subtotal)
                                VALUES (@IdVenta, @IdProducto, @Nombre, @Precio, @Cantidad, @Subtotal);";

                            string queryUpdateStock = @"
                                UPDATE Productos 
                                SET Stock = Stock - @Cantidad 
                                WHERE IdProducto = @IdProducto;";

                            foreach (DataRow row in _dtCarrito.Rows)
                            {
                                int idProd = Convert.ToInt32(row["ID"]);
                                string nomProd = row["Producto"].ToString() ?? "";
                                decimal preProd = Convert.ToDecimal(row["Precio Unit."]);
                                int cantProd = Convert.ToInt32(row["Cantidad"]);
                                decimal subProd = Convert.ToDecimal(row["Subtotal"]);

                                using (SqlCommand cmdDet = new SqlCommand(queryDetalle, conn, tran))
                                {
                                    cmdDet.Parameters.AddWithValue("@IdVenta", idVenta);
                                    cmdDet.Parameters.AddWithValue("@IdProducto", idProd);
                                    cmdDet.Parameters.AddWithValue("@Nombre", nomProd);
                                    cmdDet.Parameters.AddWithValue("@Precio", preProd);
                                    cmdDet.Parameters.AddWithValue("@Cantidad", cantProd);
                                    cmdDet.Parameters.AddWithValue("@Subtotal", subProd);
                                    cmdDet.ExecuteNonQuery();
                                }

                                using (SqlCommand cmdStock = new SqlCommand(queryUpdateStock, conn, tran))
                                {
                                    cmdStock.Parameters.AddWithValue("@Cantidad", cantProd);
                                    cmdStock.Parameters.AddWithValue("@IdProducto", idProd);
                                    cmdStock.ExecuteNonQuery();
                                }
                            }

                            tran.Commit();

                            AuditoriaHelper.RegistrarAccion(_usuario.IdUsuario, "Ventas", "Registrar",
                                $"Venta registrada Ticket #{ticket} por un total de {total:C2}");

                            MessageBox.Show($"¡Venta procesada con éxito!\n\nTicket: {ticket}\nTotal: {total:C2}\nPagado: {pagado:C2}\nCambio: {cambio:C2}",
                                "Venta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Limpiar carrito
                            _dtCarrito.Rows.Clear();
                            txtMontoPagado.Text = "";
                            txtNotas.Text = "";
                            CalcularTotales();
                            CargarProductosCombo();
                        }
                        catch (Exception exTran)
                        {
                            tran.Rollback();
                            throw new Exception("Error en la transacción de venta: " + exTran.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFinalizarVenta.Enabled = true;
            }
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
