using System;
using System.Drawing;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormUsuarios : UserControl
    {
        private TextBox txtNombreCompleto = null!;
        private TextBox txtUsuario = null!;
        private TextBox txtPassword = null!;
        private Button btnRegistrarTrabajador = null!;

        private DataGridView gridUsuarios = null!;
        private Button btnCambiarEstado = null!;
        private Button btnEliminarTrabajador = null!;

        public FormUsuarios()
        {
            InitializeComponent();
            this.Load += (s, e) => CargarUsuarios();
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
                using var penAcc = new Pen(Color.FromArgb(80, Theme.AccentPurple), 2);
                e.Graphics.DrawLine(penAcc, 20, panelTop.Height - 1, 500, panelTop.Height - 1);
            };

            var lblTitulo = new Label
            {
                Text = "👥  Administración de Usuarios y Trabajadores",
                Font = Theme.TitleFont,
                ForeColor = Theme.AccentPurple,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            panelTop.Controls.Add(lblTitulo);

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 380,
                BackColor = Theme.BgDark
            };

            // ==========================================
            // PANEL IZQUIERDO: AGREGAR TRABAJADOR
            // ==========================================
            var panelForm = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Theme.BgDark
            };

            var gbNuevo = new GroupBox
            {
                Text = " Registrar Cuenta de Trabajador ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(15)
            };

            var lblAvisoAdmin = new Label
            {
                Text = "ℹ️ Regla del Negocio: Solo existe un Administrador principal creado antes de todo. Desde este módulo el Administrador da de alta y administra a los trabajadores del bar.",
                Font = Theme.SmallFont,
                ForeColor = Theme.BrightCyan,
                Location = new Point(15, 30),
                Size = new Size(320, 60)
            };

            var lblNom = new Label { Text = "Nombre Completo del Trabajador:", Location = new Point(15, 100), AutoSize = true, ForeColor = Theme.TextWhite, Font = Theme.BodyFont };
            txtNombreCompleto = new TextBox { Location = new Point(15, 125), Size = new Size(320, 26) };
            Theme.StyleTextBox(txtNombreCompleto);

            var lblUser = new Label { Text = "Nombre de Usuario (Login):", Location = new Point(15, 160), AutoSize = true, ForeColor = Theme.TextWhite, Font = Theme.BodyFont };
            txtUsuario = new TextBox { Location = new Point(15, 185), Size = new Size(320, 26) };
            Theme.StyleTextBox(txtUsuario);

            var lblPass = new Label { Text = "Contraseña de Acceso:", Location = new Point(15, 220), AutoSize = true, ForeColor = Theme.TextWhite, Font = Theme.BodyFont };
            txtPassword = new TextBox { Location = new Point(15, 245), Size = new Size(320, 26) };
            Theme.StyleTextBox(txtPassword);

            btnRegistrarTrabajador = new Button
            {
                Text = "➕ REGISTRAR TRABAJADOR",
                Location = new Point(15, 290),
                Size = new Size(320, 42),
                Font = Theme.BodyBoldFont
            };
            Theme.StyleButton(btnRegistrarTrabajador, Theme.AccentSuccess, Color.White);
            btnRegistrarTrabajador.Click += BtnRegistrarTrabajador_Click;

            gbNuevo.Controls.Add(lblAvisoAdmin);
            gbNuevo.Controls.Add(lblNom);
            gbNuevo.Controls.Add(txtNombreCompleto);
            gbNuevo.Controls.Add(lblUser);
            gbNuevo.Controls.Add(txtUsuario);
            gbNuevo.Controls.Add(lblPass);
            gbNuevo.Controls.Add(txtPassword);
            gbNuevo.Controls.Add(btnRegistrarTrabajador);

            panelForm.Controls.Add(gbNuevo);
            split.Panel1.Controls.Add(panelForm);

            // ==========================================
            // PANEL DERECHO: LISTA DE USUARIOS
            // ==========================================
            var panelGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 15, 20, 20),
                BackColor = Theme.BgDark
            };

            var gbLista = new GroupBox
            {
                Text = " Cuentas del Sistema ICE BAR ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(12)
            };

            gridUsuarios = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            Theme.StyleGrid(gridUsuarios);

            gridUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "IdUsuario", Width = 45 });
            gridUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre Completo", DataPropertyName = "NombreCompleto", FillWeight = 35 });
            gridUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario", DataPropertyName = "NombreUsuario", Width = 110 });
            gridUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Rol", DataPropertyName = "Rol", Width = 110 });
            gridUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha Alta", DataPropertyName = "FechaRegistro", Width = 100 });
            gridUsuarios.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Activo", DataPropertyName = "Activo", Width = 65 });

            gridUsuarios.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                if (gridUsuarios.Columns[e.ColumnIndex].HeaderText == "Fecha Alta" && e.Value is DateTime dt)
                {
                    e.Value = dt.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
                else if (gridUsuarios.Columns[e.ColumnIndex].HeaderText == "Rol")
                {
                    string rol = e.Value.ToString() ?? "";
                    if (rol == "Administrador")
                    {
                        e.CellStyle.ForeColor = Theme.BrightCyan;
                        e.CellStyle.Font = Theme.BodyBoldFont;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Theme.TextWhite;
                    }
                }
            };

            var panelAcciones = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(5)
            };

            btnCambiarEstado = new Button
            {
                Text = "⚡ Activar / Desactivar Trabajador",
                Location = new Point(5, 8),
                Size = new Size(230, 34)
            };
            Theme.StyleButton(btnCambiarEstado, Theme.CardDarker, Theme.AccentWarning);
            btnCambiarEstado.Click += BtnCambiarEstado_Click;

            btnEliminarTrabajador = new Button
            {
                Text = "🗑️ Eliminar Trabajador",
                Location = new Point(245, 8),
                Size = new Size(180, 34)
            };
            Theme.StyleButton(btnEliminarTrabajador, Theme.CardDarker, Theme.AccentDanger);
            btnEliminarTrabajador.Click += BtnEliminarTrabajador_Click;

            panelAcciones.Controls.Add(btnCambiarEstado);
            panelAcciones.Controls.Add(btnEliminarTrabajador);

            gbLista.Controls.Add(gridUsuarios);
            gbLista.Controls.Add(panelAcciones);
            panelGrid.Controls.Add(gbLista);
            split.Panel2.Controls.Add(panelGrid);

            this.Controls.Add(split);
            this.Controls.Add(panelTop);
        }

        private void BtnRegistrarTrabajador_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombreCompleto.Text.Trim();
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Por favor completa todos los campos del trabajador.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseHelper.RegistrarTrabajador(nombre, user, pass);
                MessageBox.Show($"¡Trabajador '{nombre}' registrado exitosamente con usuario '{user}'!", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombreCompleto.Text = "";
                txtUsuario.Text = "";
                txtPassword.Text = "";
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar trabajador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCambiarEstado_Click(object? sender, EventArgs e)
        {
            if (gridUsuarios.SelectedRows.Count > 0)
            {
                var u = gridUsuarios.SelectedRows[0].DataBoundItem as Usuario;
                if (u == null) return;

                if (u.EsAdmin)
                {
                    MessageBox.Show("No se puede desactivar la cuenta del Administrador principal.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    bool nuevoEstado = !u.Activo;
                    DatabaseHelper.CambiarEstadoUsuario(u.IdUsuario, nuevoEstado);
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEliminarTrabajador_Click(object? sender, EventArgs e)
        {
            if (gridUsuarios.SelectedRows.Count > 0)
            {
                var u = gridUsuarios.SelectedRows[0].DataBoundItem as Usuario;
                if (u == null) return;

                if (u.EsAdmin)
                {
                    MessageBox.Show("El Administrador principal está protegido y no se puede eliminar.", "Acción Denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var r = MessageBox.Show($"¿Deseas eliminar la cuenta del trabajador '{u.NombreCompleto}' ({u.NombreUsuario})?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    try
                    {
                        DatabaseHelper.EliminarTrabajador(u.IdUsuario);
                        MessageBox.Show("Trabajador eliminado.", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar trabajador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void CargarUsuarios()
        {
            try
            {
                var lista = DatabaseHelper.ObtenerUsuarios();
                gridUsuarios.AutoGenerateColumns = false;
                gridUsuarios.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
