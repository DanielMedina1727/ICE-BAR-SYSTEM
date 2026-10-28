using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public partial class FormStockConsulta : Form
    {
        private readonly Usuario _usuario;

        public FormStockConsulta(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Theme.StyleDataGridView(dgvStock);
            Theme.SetPlaceholder(txtBuscar, "Buscar por nombre de bebida o producto...");

            if (cbCategoria.Items.Count > 0)
                cbCategoria.SelectedIndex = 0;

            this.Load += (s, e) => ConsultarStock();
        }

        private void ConsultarStock()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT IdProducto AS [Código], 
                               Nombre AS [Producto], 
                               Categoria AS [Categoría], 
                               Precio AS [Precio ($)], 
                               Stock AS [Stock Disponible],
                               CASE WHEN Stock > 10 THEN 'Disponible'
                                    WHEN Stock > 0 THEN 'Stock Bajo'
                                    ELSE 'Agotado' END AS [Disponibilidad]
                        FROM Productos
                        WHERE Activo = 1";

                    string busqueda = txtBuscar.Text.Trim();
                    bool aplicarBusqueda = !string.IsNullOrWhiteSpace(busqueda) && busqueda != "Buscar por nombre de bebida o producto...";
                    if (aplicarBusqueda)
                    {
                        query += " AND Nombre LIKE @Busqueda";
                    }

                    string cat = cbCategoria.SelectedItem?.ToString() ?? "Todas las Categorías";
                    if (cat != "Todas las Categorías")
                    {
                        query += " AND Categoria = @Categoria";
                    }

                    query += " ORDER BY Categoria ASC, Nombre ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        if (aplicarBusqueda)
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");
                        }
                        if (cat != "Todas las Categorías")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Categoria", cat);
                        }

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvStock.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            ConsultarStock();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Text = "";
            Theme.SetPlaceholder(txtBuscar, "Buscar por nombre de bebida o producto...");
            cbCategoria.SelectedIndex = 0;
            ConsultarStock();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (txtBuscar.Text != "Buscar por nombre de bebida o producto...")
            {
                ConsultarStock();
            }
        }

        private void cbCategoria_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ConsultarStock();
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
