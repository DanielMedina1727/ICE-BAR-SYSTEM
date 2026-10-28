using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public partial class FormDashboard : Form
    {
        private readonly Usuario _usuario;

        public FormDashboard(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Theme.StyleDataGridView(dgvBajoStock);
            Theme.StyleDataGridView(dgvUltimasVentas);

            this.Load += (s, e) => CargarMetricas();
        }

        public void CargarMetricas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    // 1. Ventas de Hoy y Tickets
                    string sqlVentas = @"
                        SELECT ISNULL(SUM(Total), 0) AS TotalVentas, COUNT(IdVenta) AS CantTickets 
                        FROM Ventas 
                        WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)";

                    using (SqlCommand cmd = new SqlCommand(sqlVentas, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal total = reader.GetDecimal(0);
                            int tickets = reader.GetInt32(1);
                            lblValVentasHoy.Text = total.ToString("C2");
                            lblValTickets.Text = tickets.ToString();
                        }
                    }

                    // 2. Total Productos
                    string sqlProd = "SELECT COUNT(*) FROM Productos WHERE Activo = 1";
                    using (SqlCommand cmd = new SqlCommand(sqlProd, conn))
                    {
                        object res = cmd.ExecuteScalar() ?? 0;
                        lblValProductos.Text = res.ToString();
                    }

                    // 3. Incidencias Activas
                    string sqlInc = "SELECT COUNT(*) FROM Incidencias WHERE Estado <> 'Resuelto'";
                    using (SqlCommand cmd = new SqlCommand(sqlInc, conn))
                    {
                        object res = cmd.ExecuteScalar() ?? 0;
                        lblValIncidencias.Text = res.ToString();
                    }

                    // 4. Productos con menor stock (Top 10)
                    string sqlStock = @"
                        SELECT TOP 10 Nombre AS [Producto], Categoria AS [Categoría], Precio AS [Precio], Stock AS [Stock] 
                        FROM Productos 
                        WHERE Activo = 1 
                        ORDER BY Stock ASC";
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlStock, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBajoStock.DataSource = dt;
                    }

                    // 5. Últimas ventas
                    string sqlUltimasVentas = @"
                        SELECT TOP 10 V.NumeroTicket AS [Ticket], U.NombreCompleto AS [Atendió], V.TipoPago AS [Pago], V.Total AS [Total], V.FechaHora AS [Hora]
                        FROM Ventas V
                        INNER JOIN Usuarios U ON V.IdUsuario = U.IdUsuario
                        ORDER BY V.FechaHora DESC";
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlUltimasVentas, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvUltimasVentas.DataSource = dt;
                    }
                }
            }
            catch (Exception)
            {
                // Manejar error silenciosamente o con mensaje si es la primera carga
                lblValVentasHoy.Text = "$0.00";
                lblValTickets.Text = "0";
            }
        }

        private void btnActualizar_Click(object? sender, EventArgs e)
        {
            CargarMetricas();
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
