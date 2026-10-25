using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IceBarSystem.Data;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormLogin : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private TextBox txtUsuario = null!;
        private TextBox txtPassword = null!;
        private Button btnIniciarSesion = null!;
        private Button btnConfigConexion = null!;
        private CheckBox chkMostrarPass = null!;
        private Label lblError = null!;
        private Label lblEstadoDb = null!;

        public Usuario? UsuarioAutenticado { get; private set; }

        public FormLogin()
        {
            InitializeComponent();
            this.Load += FormLogin_Load;
        }

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void InitializeComponent()
        {
            this.Text = "ICE BAR - Inicio de Sesión";
            this.Size = new Size(460, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;
            this.DoubleBuffered = true;

            // Borde perimetral con glow
            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Sombra exterior cian (glow)
                for (int i = 0; i < 3; i++)
                {
                    using var pen = new Pen(Color.FromArgb(18 - i * 5, Theme.PrimaryCyan), 1f);
                    g.DrawRectangle(pen, i, i, this.Width - 1 - i * 2, this.Height - 1 - i * 2);
                }
                using var penBorder = new Pen(Theme.BorderColor, 1);
                g.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
            };

            this.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            // ── Barra superior de control ──────────────────────────
            var panelTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                BackColor = Theme.CardBg
            };
            panelTopBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };
            panelTopBar.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelTopBar.Height - 1, panelTopBar.Width, panelTopBar.Height - 1);
            };

            var lblWinTitle = new Label
            {
                Text = "  ❄️  ICE BAR  ·  Sistema de Ventas",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0)
            };
            lblWinTitle.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var btnClose = Theme.CreateWindowControlButton("✕", Theme.AccentDanger, Color.White, (s, e) => Application.Exit());
            btnClose.Dock = DockStyle.Right;
            var btnMin = Theme.CreateWindowControlButton("—", Color.FromArgb(35, 50, 80), Color.White, (s, e) => this.WindowState = FormWindowState.Minimized);
            btnMin.Dock = DockStyle.Right;

            panelTopBar.Controls.Add(lblWinTitle);
            panelTopBar.Controls.Add(btnClose);
            panelTopBar.Controls.Add(btnMin);

            // ── Área central ────────────────────────────────────────
            var panelCenter = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(30, 20, 30, 20)
            };
            panelCenter.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            // Icono ❄️ grande (painted)
            var pnlLogo = new Panel
            {
                Size = new Size(398, 100),
                Location = new Point(0, 10),
                BackColor = Color.Transparent
            };
            pnlLogo.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Círculo de fondo con gradiente
                var rect = new Rectangle(153, 4, 90, 90);
                using var brush = new LinearGradientBrush(rect,
                    Color.FromArgb(30, Theme.PrimaryCyan),
                    Color.FromArgb(5, Theme.BrightCyan),
                    LinearGradientMode.ForwardDiagonal);
                g.FillEllipse(brush, rect);
                using var penCircle = new Pen(Color.FromArgb(50, Theme.PrimaryCyan), 1.5f);
                g.DrawEllipse(penCircle, rect);

                // Texto ❄️
                using var fontEmoji = new Font("Segoe UI Emoji", 32F);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("❄️", fontEmoji, new SolidBrush(Theme.BrightCyan), new RectangleF(0, 0, 398, 100), sf);
            };
            pnlLogo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            // Título ICE BAR
            var lblTitulo = new Label
            {
                Text = "ICE BAR",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Theme.BrightCyan,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(398, 40),
                Location = new Point(0, 118)
            };
            lblTitulo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var lblSub = new Label
            {
                Text = "SISTEMA PREMIUM DE CONTROL Y VENTAS",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(398, 20),
                Location = new Point(0, 160)
            };
            lblSub.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            // Separador decorativo
            var pnlSep = new Panel
            {
                Size = new Size(398, 2),
                Location = new Point(0, 190),
                BackColor = Color.Transparent
            };
            pnlSep.Paint += (s, e) =>
            {
                using var br = new LinearGradientBrush(pnlSep.ClientRectangle,
                    Color.Transparent, Theme.PrimaryCyan, LinearGradientMode.Horizontal);
                using var br2 = new LinearGradientBrush(new Rectangle(200, 0, 198, 2),
                    Theme.PrimaryCyan, Color.Transparent, LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(br, 0, 0, 200, 2);
                e.Graphics.FillRectangle(br2, 200, 0, 198, 2);
            };

            // Label usuario
            var lblUser = new Label
            {
                Text = "Usuario",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(2, 208),
                AutoSize = true
            };

            // TextBox Usuario con panel envolvente para efecto de borde
            txtUsuario = new TextBox
            {
                Location = new Point(0, 225),
                Size = new Size(398, 32),
                Font = Theme.BodyFont,
                Text = "admin"
            };
            Theme.StyleTextBox(txtUsuario);

            // Label contraseña
            var lblPass = new Label
            {
                Text = "Contraseña",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(2, 270),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Location = new Point(0, 287),
                Size = new Size(398, 32),
                Font = Theme.BodyFont,
                PasswordChar = '●',
                Text = "admin123"
            };
            Theme.StyleTextBox(txtPassword);

            chkMostrarPass = new CheckBox
            {
                Text = "Mostrar contraseña",
                Font = Theme.SmallFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(2, 328),
                AutoSize = true
            };
            chkMostrarPass.CheckedChanged += (s, e) =>
                txtPassword.PasswordChar = chkMostrarPass.Checked ? '\0' : '●';

            lblError = new Label
            {
                Text = "",
                Font = Theme.SmallFont,
                ForeColor = Theme.AccentDanger,
                Location = new Point(0, 355),
                Size = new Size(398, 28),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Botón principal con gradiente
            btnIniciarSesion = new Button
            {
                Text = "  ⚡  INICIAR SESIÓN",
                Location = new Point(0, 392),
                Size = new Size(398, 50),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Theme.BgDark,
                Cursor = Cursors.Hand
            };
            btnIniciarSesion.FlatAppearance.BorderSize = 0;
            Theme.StyleButtonGradient(btnIniciarSesion, Theme.PrimaryCyan, Theme.BrightCyan, Theme.BgDark);
            btnIniciarSesion.Click += BtnIniciarSesion_Click;

            btnConfigConexion = new Button
            {
                Text = "⚙️  Configurar conexión SQL Server",
                Location = new Point(0, 454),
                Size = new Size(398, 36)
            };
            Theme.StyleButton(btnConfigConexion, Theme.CardDarker, Theme.TextMuted);
            btnConfigConexion.Click += BtnConfigConexion_Click;

            lblEstadoDb = new Label
            {
                Text = "Verificando base de datos...",
                Font = Theme.SmallFont,
                ForeColor = Theme.TextMuted,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(398, 22),
                Location = new Point(0, 500)
            };

            panelCenter.Controls.Add(pnlLogo);
            panelCenter.Controls.Add(lblTitulo);
            panelCenter.Controls.Add(lblSub);
            panelCenter.Controls.Add(pnlSep);
            panelCenter.Controls.Add(lblUser);
            panelCenter.Controls.Add(txtUsuario);
            panelCenter.Controls.Add(lblPass);
            panelCenter.Controls.Add(txtPassword);
            panelCenter.Controls.Add(chkMostrarPass);
            panelCenter.Controls.Add(lblError);
            panelCenter.Controls.Add(btnIniciarSesion);
            panelCenter.Controls.Add(btnConfigConexion);
            panelCenter.Controls.Add(lblEstadoDb);

            this.Controls.Add(panelCenter);
            this.Controls.Add(panelTopBar);
            this.AcceptButton = btnIniciarSesion;
        }

        private void FormLogin_Load(object? sender, EventArgs e)
        {
            try
            {
                DatabaseHelper.EnsureDatabase();
                lblEstadoDb.Text = "🟢  Base de datos conectada correctamente";
                lblEstadoDb.ForeColor = Theme.AccentSuccess;
            }
            catch (Exception ex)
            {
                lblEstadoDb.Text = "🔴  No se pudo conectar a SQL Server";
                lblEstadoDb.ForeColor = Theme.AccentDanger;
                lblError.Text = "Verifica tu servidor SQL con el botón de abajo.";
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void BtnIniciarSesion_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "⚠️  Por favor ingresa usuario y contraseña.";
                return;
            }

            try
            {
                var user = DatabaseHelper.IniciarSesion(usuario, password);
                if (user != null)
                {
                    this.UsuarioAutenticado = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblError.Text = "❌  Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error de conexión:\n{ex.Message}";
            }
        }

        private void BtnConfigConexion_Click(object? sender, EventArgs e)
        {
            using var dlg = new FormConfigConexion();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FormLogin_Load(this, EventArgs.Empty);
            }
        }
    }
}
