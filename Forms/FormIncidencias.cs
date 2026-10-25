using System;
using System.Drawing;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormIncidencias : UserControl
    {
        private readonly Usuario _usuarioActual;

        private TextBox txtTitulo = null!;
        private TextBox txtDescripcion = null!;
        private Button btnRegistrar = null!;
        private Button btnLimpiar = null!;

        private DataGridView gridIncidencias = null!;
        private Label lblTotalIncidencias = null!;

        public FormIncidencias(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;
            InitializeComponent();
            this.Load += (s, e) => CargarIncidencias();
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
                using var penAcc = new Pen(Color.FromArgb(80, Theme.AccentWarning), 2);
                e.Graphics.DrawLine(penAcc, 20, panelTop.Height - 1, 420, panelTop.Height - 1);
            };

            var lblTitulo = new Label
            {
                Text = "⚠️  Reportes de Incidencias del Bar",
                Font = Theme.TitleFont,
                ForeColor = Theme.AccentWarning,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            panelTop.Controls.Add(lblTitulo);

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 420,
                BackColor = Theme.BgDark
            };

            // ==========================================
            // PANEL IZQUIERDO: FORMULARIO DE REGISTRO
            // ==========================================
            var panelForm = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                BackColor = Theme.BgDark
            };

            var gbRegistro = new GroupBox
            {
                Text = " Registrar Nueva Incidencia ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(15)
            };

            var lblInfoUser = new Label
            {
                Text = $"Reporta: {_usuarioActual.NombreCompleto} ({_usuarioActual.Rol})",
                Font = Theme.SmallFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(15, 30),
                AutoSize = true
            };

            var lblAsunto = new Label
            {
                Text = "Título / Asunto de la Incidencia:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(15, 65),
                AutoSize = true
            };

            txtTitulo = new TextBox
            {
                Location = new Point(15, 90),
                Size = new Size(360, 28),
                Font = Theme.BodyFont
            };
            Theme.StyleTextBox(txtTitulo);

            var lblDesc = new Label
            {
                Text = "Descripción Detallada del Suceso:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(15, 130),
                AutoSize = true
            };

            txtDescripcion = new TextBox
            {
                Location = new Point(15, 155),
                Size = new Size(360, 160),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = Theme.BodyFont
            };
            Theme.StyleTextBox(txtDescripcion);

            btnRegistrar = new Button
            {
                Text = "🚨 REGISTRAR INCIDENCIA",
                Location = new Point(15, 330),
                Size = new Size(220, 42),
                Font = Theme.BodyBoldFont
            };
            Theme.StyleButton(btnRegistrar, Theme.AccentDanger, Color.White);
            btnRegistrar.Click += BtnRegistrar_Click;

            btnLimpiar = new Button
            {
                Text = "Limpiar",
                Location = new Point(245, 330),
                Size = new Size(130, 42)
            };
            Theme.StyleButton(btnLimpiar, Theme.CardDarker, Theme.TextMuted);
            btnLimpiar.Click += (s, e) => { txtTitulo.Text = ""; txtDescripcion.Text = ""; };

            gbRegistro.Controls.Add(lblInfoUser);
            gbRegistro.Controls.Add(lblAsunto);
            gbRegistro.Controls.Add(txtTitulo);
            gbRegistro.Controls.Add(lblDesc);
            gbRegistro.Controls.Add(txtDescripcion);
            gbRegistro.Controls.Add(btnRegistrar);
            gbRegistro.Controls.Add(btnLimpiar);

            panelForm.Controls.Add(gbRegistro);
            split.Panel1.Controls.Add(panelForm);

            // ==========================================
            // PANEL DERECHO: VISTA DE INCIDENCIAS
            // ==========================================
            var panelVista = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 15, 20, 20),
                BackColor = Theme.BgDark
            };

            var gbVista = new GroupBox
            {
                Text = " Historial de Reportes Registrados ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.BrightCyan,
                Dock = DockStyle.Fill,
                BackColor = Theme.CardBg,
                Padding = new Padding(12)
            };

            lblTotalIncidencias = new Label
            {
                Text = "Total Registros: 0",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextMuted,
                Dock = DockStyle.Top,
                Height = 25
            };

            gridIncidencias = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            Theme.StyleGrid(gridIncidencias);

            gridIncidencias.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "IdIncidencia", Width = 50 });
            gridIncidencias.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha / Hora", DataPropertyName = "FechaHora", Width = 140 });
            gridIncidencias.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Reportó", DataPropertyName = "NombreUsuario", Width = 130 });
            gridIncidencias.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Título", DataPropertyName = "Titulo", Width = 160 });
            gridIncidencias.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = "Descripcion", FillWeight = 45 });
            gridIncidencias.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", Width = 95 });

            gridIncidencias.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                if (gridIncidencias.Columns[e.ColumnIndex].HeaderText == "Fecha / Hora" && e.Value is DateTime dt)
                {
                    e.Value = dt.ToString("dd/MM/yyyy HH:mm");
                    e.FormattingApplied = true;
                }
            };

            gbVista.Controls.Add(gridIncidencias);
            gbVista.Controls.Add(lblTotalIncidencias);
            panelVista.Controls.Add(gbVista);
            split.Panel2.Controls.Add(panelVista);

            this.Controls.Add(split);
            this.Controls.Add(panelTop);
        }

        private void BtnRegistrar_Click(object? sender, EventArgs e)
        {
            string titulo = txtTitulo.Text.Trim();
            string desc = txtDescripcion.Text.Trim();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("Ingresa el título o asunto de la incidencia.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Ingresa la descripción del problema o suceso.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DatabaseHelper.RegistrarIncidencia(_usuarioActual.IdUsuario, titulo, desc);
                MessageBox.Show("¡Incidencia registrada correctamente en el sistema!", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTitulo.Text = "";
                txtDescripcion.Text = "";
                CargarIncidencias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar incidencia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarIncidencias()
        {
            try
            {
                var lista = DatabaseHelper.ObtenerIncidencias();
                gridIncidencias.AutoGenerateColumns = false;
                gridIncidencias.DataSource = lista;
                lblTotalIncidencias.Text = $"Total de reportes de incidencia: {lista.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar incidencias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
