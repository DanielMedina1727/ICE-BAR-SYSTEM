using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormConfigConexion : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private TextBox txtCadena = null!;
        private Button btnProbar = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;
        private Label lblEstado = null!;
        private RadioButton rbLocalDb = null!;
        private RadioButton rbLocalhost = null!;
        private RadioButton rbSqlExpress = null!;

        public FormConfigConexion()
        {
            InitializeComponent();
        }

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void InitializeComponent()
        {
            this.Text = "ICE BAR - Configuración de Conexión SQL Server";
            this.Size = new Size(580, 445);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;
            this.DoubleBuffered = true;

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                for (int i = 0; i < 2; i++)
                {
                    using var pen = new Pen(Color.FromArgb(20 - i * 8, Theme.PrimaryCyan), 1f);
                    g.DrawRectangle(pen, i, i, this.Width - 1 - i * 2, this.Height - 1 - i * 2);
                }
                using var penBorder = new Pen(Theme.BorderColor, 1);
                g.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
            };

            // Barra superior
            var panelTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                BackColor = Theme.CardBg
            };
            panelTopBar.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, 35, panelTopBar.Width, 35);
            };
            panelTopBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var lblBarTitle = new Label
            {
                Text = "  ⚙️  ICE BAR  ·  Configuración SQL Server",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblBarTitle.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var btnCerrar = Theme.CreateWindowControlButton("✕", Theme.AccentDanger, Color.White, (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            });
            btnCerrar.Dock = DockStyle.Right;

            panelTopBar.Controls.Add(lblBarTitle);
            panelTopBar.Controls.Add(btnCerrar);
            this.Controls.Add(panelTopBar);

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24)
            };
            panel.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var lblTitulo = new Label
            {
                Text = "Configurar Conexión a SQL Server",
                Font = Theme.TitleFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(24, 20)
            };

            var lblDesc = new Label
            {
                Text = "Selecciona el tipo de servidor o escribe tu cadena de conexión personalizada:",
                Font = Theme.BodyFont,
                ForeColor = Theme.TextMuted,
                AutoSize = true,
                Location = new Point(24, 55)
            };

            var gbPreajustes = new GroupBox
            {
                Text = " Servidores comunes ",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.PrimaryCyan,
                Location = new Point(24, 90),
                Size = new Size(515, 80)
            };

            rbLocalDb = new RadioButton
            {
                Text = "LocalDB (MSSQLLocalDB)",
                Location = new Point(16, 30),
                AutoSize = true,
                ForeColor = Theme.TextWhite
            };
            rbLocalDb.CheckedChanged += (s, e) =>
            {
                if (rbLocalDb.Checked)
                    txtCadena.Text = "Server=(localdb)\\MSSQLLocalDB;Database=IceBarDB;Integrated Security=True;TrustServerCertificate=True;";
            };

            rbSqlExpress = new RadioButton
            {
                Text = "SQL Server Express (.s\\SQLEXPRESS)",
                Location = new Point(210, 30),
                AutoSize = true,
                ForeColor = Theme.TextWhite
            };
            rbSqlExpress.CheckedChanged += (s, e) =>
            {
                if (rbSqlExpress.Checked)
                    txtCadena.Text = "Server=.\\SQLEXPRESS;Database=IceBarDB;Integrated Security=True;TrustServerCertificate=True;";
            };

            rbLocalhost = new RadioButton
            {
                Text = "Localhost (Por defecto)",
                Location = new Point(370, 30),
                AutoSize = true,
                ForeColor = Theme.TextWhite
            };
            rbLocalhost.CheckedChanged += (s, e) =>
            {
                if (rbLocalhost.Checked)
                    txtCadena.Text = "Server=localhost;Database=IceBarDB;Integrated Security=True;TrustServerCertificate=True;";
            };

            gbPreajustes.Controls.Add(rbLocalDb);
            gbPreajustes.Controls.Add(rbSqlExpress);
            gbPreajustes.Controls.Add(rbLocalhost);

            var lblCadena = new Label
            {
                Text = "Cadena de Conexión:",
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(24, 185),
                AutoSize = true
            };

            txtCadena = new TextBox
            {
                Location = new Point(24, 210),
                Size = new Size(515, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Text = DatabaseHelper.ConnectionString
            };
            Theme.StyleTextBox(txtCadena);

            lblEstado = new Label
            {
                Text = "Estado: Listo para probar conexión.",
                Font = Theme.SmallFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(24, 280),
                Size = new Size(515, 35)
            };

            btnProbar = new Button
            {
                Text = " Probar Conexión",
                Location = new Point(24, 325),
                Size = new Size(150, 38)
            };
            Theme.StyleButton(btnProbar, Theme.CardBg, Theme.BrightCyan);
            btnProbar.Click += BtnProbar_Click;

            btnGuardar = new Button
            {
                Text = "Guardar y Usar",
                Location = new Point(275, 325),
                Size = new Size(130, 38)
            };
            Theme.StyleButton(btnGuardar, Theme.AccentSuccess, Color.White);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(415, 325),
                Size = new Size(120, 38)
            };
            Theme.StyleButton(btnCancelar, Theme.CardBg, Theme.TextMuted);
            btnCancelar.Click += (s, e) => this.Close();

            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(lblDesc);
            panel.Controls.Add(gbPreajustes);
            panel.Controls.Add(lblCadena);
            panel.Controls.Add(txtCadena);
            panel.Controls.Add(lblEstado);
            panel.Controls.Add(btnProbar);
            panel.Controls.Add(btnGuardar);
            panel.Controls.Add(btnCancelar);

            this.Controls.Add(panel);
        }

        private void BtnProbar_Click(object? sender, EventArgs e)
        {
            lblEstado.Text = "Probando conexión...";
            lblEstado.ForeColor = Theme.AccentWarning;
            Application.DoEvents();

            bool ok = DatabaseHelper.TestConnection(txtCadena.Text.Trim(), out string error);
            if (ok)
            {
                lblEstado.Text = " Conexión exitosa a SQL Server.";
                lblEstado.ForeColor = Theme.AccentSuccess;
            }
            else
            {
                lblEstado.Text = $" Error: {error}";
                lblEstado.ForeColor = Theme.AccentDanger;
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            DatabaseHelper.ConnectionString = txtCadena.Text.Trim();
            try
            {
                DatabaseHelper.EnsureDatabase();
                MessageBox.Show("¡Cadena guardada y base de datos 'IceBarDB' verificada exitosamente!", "ICE BAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"La cadena fue guardada, pero al inicializar la base de datos ocurrió un error:\n{ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
