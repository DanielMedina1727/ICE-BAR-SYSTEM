using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public partial class FormHistorialVentas : Form
    {
        private readonly Usuario _usuario;

        public FormHistorialVentas(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Theme.StyleDataGridView(dgvVentas);
            Theme.StyleDataGridView(dgvDetalle);
            Theme.SetPlaceholder(txtBuscarTicket, "Buscar por número de ticket...");

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            this.Load += (s, e) => CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT V.IdVenta AS [ID],
                               V.NumeroTicket AS [Ticket],
                               U.NombreCompleto AS [Cajero / Atendió],
                               V.FechaHora AS [Fecha y Hora],
                               V.TipoPago AS [Forma Pago],
                               V.Total AS [Total ($)],
                               V.MontoPagado AS [Pagado ($)],
                               V.Cambio AS [Cambio ($)],
                               V.Notas AS [Notas]
                        FROM Ventas V
                        INNER JOIN Usuarios U ON V.IdUsuario = U.IdUsuario
                        WHERE CAST(V.FechaHora AS DATE) >= @Desde 
                          AND CAST(V.FechaHora AS DATE) <= @Hasta";

                    string ticketFiltro = txtBuscarTicket.Text.Trim();
                    bool filtrarTicket = !string.IsNullOrWhiteSpace(ticketFiltro) && ticketFiltro != "Buscar por número de ticket...";
                    if (filtrarTicket)
                    {
                        query += " AND V.NumeroTicket LIKE @Ticket";
                    }

                    query += " ORDER BY V.FechaHora DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Desde", dtpDesde.Value.Date);
                        da.SelectCommand.Parameters.AddWithValue("@Hasta", dtpHasta.Value.Date);
                        if (filtrarTicket)
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Ticket", "%" + ticketFiltro + "%");
                        }

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvVentas.DataSource = dt;
                    }
                }

                // Limpiar tabla de detalle
                dgvDetalle.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial de ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVentas_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvVentas.Rows.Count)
            {
                var row = dgvVentas.Rows[e.RowIndex];
                if (int.TryParse(row.Cells["ID"].Value?.ToString(), out int idVenta))
                {
                    CargarDetalleVenta(idVenta);
                }
            }
        }

        private void CargarDetalleVenta(int idVenta)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT D.NombreProducto AS [Producto],
                               D.PrecioUnitario AS [Precio Unit. ($)],
                               D.Cantidad AS [Cantidad],
                               D.Subtotal AS [Subtotal ($)]
                        FROM DetalleVentas D
                        WHERE D.IdVenta = @IdVenta";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvDetalle.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalle del ticket: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;
            txtBuscarTicket.Text = "";
            Theme.SetPlaceholder(txtBuscarTicket, "Buscar por número de ticket...");
            CargarHistorial();
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
