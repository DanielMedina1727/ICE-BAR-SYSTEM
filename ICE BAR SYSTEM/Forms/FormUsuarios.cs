using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;
using IceBarSystem.Helpers;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public partial class FormUsuarios : Form
    {
        private readonly Usuario _usuarioActual;

        public FormUsuarios(Usuario usuarioActual)
        {
            InitializeComponent();
            _usuarioActual = usuarioActual;

            Theme.StyleDataGridView(dgvUsuarios);
            Theme.SetPlaceholder(txtBuscar, "Buscar por nombre o usuario...");

            if (cbRol.Items.Count > 0)
                cbRol.SelectedIndex = 0;

            this.Load += (s, e) => CargarUsuarios();
        }

        private void CargarUsuarios(string filtro = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT IdUsuario AS [ID], 
                               NombreCompleto AS [Nombre Completo], 
                               NombreUsuario AS [Usuario], 
                               Rol AS [Rol], 
                               Activo AS [Activo], 
                               FechaRegistro AS [Fecha Registro]
                        FROM Usuarios";

                    if (!string.IsNullOrWhiteSpace(filtro) && filtro != "Buscar por nombre o usuario...")
                    {
                        query += " WHERE (NombreCompleto LIKE @Filtro OR NombreUsuario LIKE @Filtro)";
                    }

                    query += " ORDER BY NombreCompleto ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(filtro) && filtro != "Buscar por nombre o usuario...")
                        {
                            da.SelectCommand.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                        }

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvUsuarios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvUsuarios.Rows.Count)
            {
                var row = dgvUsuarios.Rows[e.RowIndex];
                txtId.Text = row.Cells["ID"].Value?.ToString() ?? "";
                txtNombreCompleto.Text = row.Cells["Nombre Completo"].Value?.ToString() ?? "";
                txtUsername.Text = row.Cells["Usuario"].Value?.ToString() ?? "";
                cbRol.SelectedItem = row.Cells["Rol"].Value?.ToString() ?? "Trabajador";
                chkActivo.Checked = Convert.ToBoolean(row.Cells["Activo"].Value ?? true);
                txtPassword.Text = ""; // Por seguridad se deja en blanco al seleccionar
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombreCompleto.Focus();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombreCompleto.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string rol = cbRol.SelectedItem?.ToString() ?? "Trabajador";
            bool activo = chkActivo.Checked;
            bool esNuevo = string.IsNullOrWhiteSpace(txtId.Text);

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre completo del usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Ingrese el nombre de usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (esNuevo && string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Debe asignar una contraseña para el nuevo usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    if (esNuevo)
                    {
                        // Verificar que no exista el nombre de usuario
                        string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @Usuario";
                        using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@Usuario", username);
                            int existe = Convert.ToInt32(cmdCheck.ExecuteScalar());
                            if (existe > 0)
                            {
                                MessageBox.Show("El nombre de usuario ya se encuentra registrado. Elija otro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtUsername.Focus();
                                return;
                            }
                        }

                        string insertQuery = @"
                            INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Password, Rol, Activo, FechaRegistro)
                            VALUES (@Nombre, @Usuario, @Pass, @Rol, @Activo, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Usuario", username);
                            cmd.Parameters.AddWithValue("@Pass", password);
                            cmd.Parameters.AddWithValue("@Rol", rol);
                            cmd.Parameters.AddWithValue("@Activo", activo);
                            cmd.ExecuteNonQuery();
                        }

                        AuditoriaHelper.RegistrarAccion(_usuarioActual.IdUsuario, "Usuarios", "Crear", $"Usuario creado: {username} ({rol})");
                        MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int id = int.Parse(txtId.Text);

                        string updateQuery = @"
                            UPDATE Usuarios 
                            SET NombreCompleto = @Nombre, 
                                NombreUsuario = @Usuario, 
                                Rol = @Rol, 
                                Activo = @Activo" +
                                (string.IsNullOrEmpty(password) ? "" : ", Password = @Pass") +
                                " WHERE IdUsuario = @Id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Usuario", username);
                            cmd.Parameters.AddWithValue("@Rol", rol);
                            cmd.Parameters.AddWithValue("@Activo", activo);
                            if (!string.IsNullOrEmpty(password))
                            {
                                cmd.Parameters.AddWithValue("@Pass", password);
                            }
                            cmd.ExecuteNonQuery();
                        }

                        AuditoriaHelper.RegistrarAccion(_usuarioActual.IdUsuario, "Usuarios", "Actualizar", $"Usuario ID {id} modificado: {username}");
                        MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Seleccione un usuario de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            if (id == _usuarioActual.IdUsuario)
            {
                MessageBox.Show("No puede desactivar su propia cuenta activa de administrador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txtUsername.Text;
            if (MessageBox.Show($"¿Desea desactivar al usuario '{username}'?", "Confirmar Desactivación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    AuditoriaHelper.RegistrarAccion(_usuarioActual.IdUsuario, "Usuarios", "Desactivar", $"Usuario ID {id} desactivado: {username}");
                    MessageBox.Show("El usuario ha sido desactivado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al desactivar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            CargarUsuarios(txtBuscar.Text.Trim());
        }

        private void btnLimpiarBusqueda_Click(object? sender, EventArgs e)
        {
            txtBuscar.Text = "";
            Theme.SetPlaceholder(txtBuscar, "Buscar por nombre o usuario...");
            CargarUsuarios();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (txtBuscar.Text != "Buscar por nombre o usuario...")
            {
                CargarUsuarios(txtBuscar.Text.Trim());
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombreCompleto.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            if (cbRol.Items.Count > 0)
                cbRol.SelectedIndex = 0;
            chkActivo.Checked = true;
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
