using System;
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
    public partial class FormStockAdmin : Form
    {
        private readonly Usuario _usuario;

        public FormStockAdmin(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Theme.StyleDataGridView(dgvProductos);
            Theme.SetPlaceholder(txtBuscar, "Buscar por nombre o categoría...");

            if (cbCategoria.Items.Count > 0)
                cbCategoria.SelectedIndex = 0;

            this.Load += (s, e) => CargarProductos();
        }

        private void CargarProductos(string filtro = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT IdProducto AS [ID], Nombre AS [Producto], Categoria AS [Categoría], 
                               Precio AS [Precio ($)], Stock AS [Stock], Activo AS [Activo], FechaRegistro AS [Registro]
                        FROM Productos";

                    if (!string.IsNullOrWhiteSpace(filtro) && filtro != "Buscar por nombre o categoría...")
                    {
                        query += " WHERE (Nombre LIKE @Filtro OR Categoria LIKE @Filtro)";
                    }

                    query += " ORDER BY Nombre ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(filtro) && filtro != "Buscar por nombre o categoría...")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                        }

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvProductos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProductos.Rows.Count)
            {
                var row = dgvProductos.Rows[e.RowIndex];
                txtId.Text = row.Cells["ID"].Value?.ToString() ?? "";
                txtNombre.Text = row.Cells["Producto"].Value?.ToString() ?? "";
                cbCategoria.SelectedItem = row.Cells["Categoría"].Value?.ToString() ?? "General";
                txtPrecio.Text = row.Cells["Precio ($)"].Value?.ToString() ?? "";
                txtStock.Text = row.Cells["Stock"].Value?.ToString() ?? "";
                chkActivo.Checked = Convert.ToBoolean(row.Cells["Activo"].Value ?? true);
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string categoria = cbCategoria.SelectedItem?.ToString() ?? "General";

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingrese un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Ingrese una cantidad de stock válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            bool activo = chkActivo.Checked;
            bool esNuevo = string.IsNullOrWhiteSpace(txtId.Text);

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    if (esNuevo)
                    {
                        string query = @"
                            INSERT INTO Productos (Nombre, Categoria, Precio, Stock, Activo, FechaRegistro)
                            VALUES (@Nombre, @Categoria, @Precio, @Stock, @Activo, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Categoria", categoria);
                            cmd.Parameters.AddWithValue("@Precio", precio);
                            cmd.Parameters.AddWithValue("@Stock", stock);
                            cmd.Parameters.AddWithValue("@Activo", activo);
                            cmd.ExecuteNonQuery();
                        }

                        AuditoriaHelper.RegistrarAccion(_usuario.IdUsuario, "Productos", "Crear", $"Nuevo producto registrado: {nombre}");
                        MessageBox.Show("Producto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int id = int.Parse(txtId.Text);
                        string query = @"
                            UPDATE Productos 
                            SET Nombre = @Nombre, Categoria = @Categoria, Precio = @Precio, Stock = @Stock, Activo = @Activo
                            WHERE IdProducto = @Id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Categoria", categoria);
                            cmd.Parameters.AddWithValue("@Precio", precio);
                            cmd.Parameters.AddWithValue("@Stock", stock);
                            cmd.Parameters.AddWithValue("@Activo", activo);
                            cmd.ExecuteNonQuery();
                        }

                        AuditoriaHelper.RegistrarAccion(_usuario.IdUsuario, "Productos", "Actualizar", $"Producto ID {id} actualizado: {nombre}");
                        MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CargarProductos();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Seleccione un producto de la tabla para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            string nombre = txtNombre.Text;

            if (MessageBox.Show($"¿Está seguro de eliminar el producto '{nombre}'?", "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();

                        // Desactivar producto para mantener integridad referencial con ventas anteriores
                        string query = "UPDATE Productos SET Activo = 0 WHERE IdProducto = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    AuditoriaHelper.RegistrarAccion(_usuario.IdUsuario, "Productos", "Eliminar", $"Producto ID {id} desactivado: {nombre}");
                    MessageBox.Show("El producto ha sido desactivado del catálogo.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            CargarProductos(txtBuscar.Text.Trim());
        }

        private void btnLimpiarBusqueda_Click(object? sender, EventArgs e)
        {
            txtBuscar.Text = "";
            Theme.SetPlaceholder(txtBuscar, "Buscar por nombre o categoría...");
            CargarProductos();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (txtBuscar.Text != "Buscar por nombre o categoría...")
            {
                CargarProductos(txtBuscar.Text.Trim());
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            if (cbCategoria.Items.Count > 0)
                cbCategoria.SelectedIndex = 0;
            txtPrecio.Text = "";
            txtStock.Text = "";
            chkActivo.Checked = true;
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
