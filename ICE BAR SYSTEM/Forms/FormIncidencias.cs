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
    public partial class FormIncidencias : Form
    {
        private readonly Usuario _usuario;

        public FormIncidencias(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            Theme.StyleDataGridView(dgvIncidencias);

            if (cbEstado.Items.Count > 0)
                cbEstado.SelectedIndex = 0;

            this.Load += (s, e) => CargarIncidencias();
        }

        private void CargarIncidencias()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT I.IdIncidencia AS [ID],
                               I.Titulo AS [Título],
                               I.Descripcion AS [Descripción],
                               U.NombreCompleto AS [Reportado Por],
                               I.FechaHora AS [Fecha / Hora],
                               I.Estado AS [Estado]
                        FROM Incidencias I
                        INNER JOIN Usuarios U ON I.IdUsuario = U.IdUsuario
                        ORDER BY I.FechaHora DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvIncidencias.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar incidencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvIncidencias_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvIncidencias.Rows.Count)
            {
                var row = dgvIncidencias.Rows[e.RowIndex];
                txtId.Text = row.Cells["ID"].Value?.ToString() ?? "";
                txtTituloIncidencia.Text = row.Cells["Título"].Value?.ToString() ?? "";
                txtDescripcion.Text = row.Cells["Descripción"].Value?.ToString() ?? "";
                cbEstado.SelectedItem = row.Cells["Estado"].Value?.ToString() ?? "Reportado";
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtTituloIncidencia.Focus();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string titulo = txtTituloIncidencia.Text.Trim();
            string desc = txtDescripcion.Text.Trim();
            string estado = cbEstado.SelectedItem?.ToString() ?? "Reportado";
            bool esNuevo = string.IsNullOrWhiteSpace(txtId.Text);

            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("Ingrese un título para la incidencia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTituloIncidencia.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Ingrese la descripción de lo ocurrido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    if (esNuevo)
                    {
                        string query = @"
                            INSERT INTO Incidencias (IdUsuario, Titulo, Descripcion, FechaHora, Estado)
                            VALUES (@IdUsuario, @Titulo, @Desc, GETDATE(), @Estado)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@IdUsuario", _usuario.IdUsuario);
                            cmd.Parameters.AddWithValue("@Titulo", titulo);
                            cmd.Parameters.AddWithValue("@Desc", desc);
                            cmd.Parameters.AddWithValue("@Estado", estado);
                            cmd.ExecuteNonQuery();
                        }

                        AuditoriaHelper.RegistrarAccion(_usuario.IdUsuario, "Incidencias", "Reportar", $"Incidencia reportada: {titulo}");
                        MessageBox.Show("Incidencia registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int id = int.Parse(txtId.Text);
                        string query = @"
                            UPDATE Incidencias 
                            SET Titulo = @Titulo, Descripcion = @Desc, Estado = @Estado
                            WHERE IdIncidencia = @Id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Titulo", titulo);
                            cmd.Parameters.AddWithValue("@Desc", desc);
                            cmd.Parameters.AddWithValue("@Estado", estado);
                            cmd.ExecuteNonQuery();
                        }

                        AuditoriaHelper.RegistrarAccion(_usuario.IdUsuario, "Incidencias", "Actualizar", $"Incidencia ID {id} actualizada a estado: {estado}");
                        MessageBox.Show("Incidencia actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CargarIncidencias();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la incidencia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtTituloIncidencia.Text = "";
            txtDescripcion.Text = "";
            if (cbEstado.Items.Count > 0)
                cbEstado.SelectedIndex = 0;
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
