using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IceBarSystem.Models;
using IceBarSystem.UI;

namespace IceBarSystem.Forms
{
    public class FormMain : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private readonly Usuario _usuario;

        private Panel panelSidebar = null!;
        private Panel panelTop = null!;
        private Panel panelContenido = null!;

        private Label lblTituloModulo = null!;
        private Label lblReloj = null!;
        private System.Windows.Forms.Timer timerReloj = null!;

        private Button btnMinimizar = null!;
        private Button btnMaximizar = null!;
        private Button btnCerrar = null!;

        private Button btnNavVentas = null!;
        private Button btnNavConsultaStock = null!;
        private Button btnNavControlStock = null!;
        private Button btnNavHistorial = null!;
        private Button btnNavIncidencias = null!;
        private Button btnNavUsuarios = null!;
        private Button btnCerrarSesion = null!;

        private Button? _botonActivo = null;

        public FormMain(Usuario usuario)
        {
            _usuario = usuario;
            InitializeComponent();
            ConfigurarPermisosPorRol();
            IniciarReloj();
            MostrarModulo(new FormVenta(_usuario), "Terminal de Ventas", btnNavVentas);
        }

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void InitializeComponent()
        {
            this.Text = $"ICE BAR - [{_usuario.Rol}: {_usuario.NombreCompleto}]";
            this.Size = new Size(1300, 780);
            this.MinimumSize = new Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.BackColor = Theme.BgDark;
            this.ForeColor = Theme.TextWhite;
            this.DoubleBuffered = true;

            // =====================================================
            // SIDEBAR  (izquierda, 250px)
            // =====================================================
            panelSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Theme.SidebarBg
            };

            // ── Cabecera / Logo ──
            var panelLogo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(6, 12, 28)
            };
            panelLogo.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Degradado sutil de fondo
                using var br = new LinearGradientBrush(panelLogo.ClientRectangle,
                    Color.FromArgb(6, 12, 28), Color.FromArgb(12, 22, 52), LinearGradientMode.ForwardDiagonal);
                g.FillRectangle(br, panelLogo.ClientRectangle);
                // Línea inferior cian
                using var pen = new Pen(Color.FromArgb(80, Theme.PrimaryCyan), 2);
                g.DrawLine(pen, 0, panelLogo.Height - 1, panelLogo.Width, panelLogo.Height - 1);
            };
            panelLogo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var lblLogo = new Label
            {
                Text = "❄️  ICE BAR",
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Theme.BrightCyan,
                Location = new Point(18, 14),
                AutoSize = true
            };
            lblLogo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var lblSub = new Label
            {
                Text = "PREMIUM BAR SYSTEM",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                Location = new Point(22, 50),
                AutoSize = true
            };
            lblSub.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            panelLogo.Controls.Add(lblLogo);
            panelLogo.Controls.Add(lblSub);

            // ── Perfil de Usuario ──
            var panelUser = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Theme.CardBg,
                Padding = new Padding(14, 10, 10, 10)
            };
            panelUser.Paint += (s, e) =>
            {
                // Línea inferior de borde
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelUser.Height - 1, panelUser.Width, panelUser.Height - 1);
            };

            // Avatar circulo con inicial
            var pnlAvatar = new Panel
            {
                Size = new Size(42, 42),
                Location = new Point(14, 15),
                BackColor = Color.Transparent
            };
            pnlAvatar.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using var br = new SolidBrush(_usuario.EsAdmin ? Theme.DeepCyan : Theme.AccentSuccess);
                g.FillEllipse(br, 0, 0, 40, 40);
                string inicial = _usuario.NombreCompleto.Length > 0 ? _usuario.NombreCompleto[0].ToString().ToUpper() : "U";
                using var font = new Font("Segoe UI", 14F, FontStyle.Bold);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(inicial, font, Brushes.White, new RectangleF(0, 0, 40, 40), sf);
            };

            var lblUserNombre = new Label
            {
                Text = _usuario.NombreCompleto,
                Font = Theme.BodyBoldFont,
                ForeColor = Theme.TextWhite,
                Location = new Point(62, 12),
                Size = new Size(178, 20),
                AutoEllipsis = true
            };
            var lblUserRol = new Label
            {
                Text = $"⬤  {_usuario.Rol.ToUpper()}",
                Font = Theme.SmallBoldFont,
                ForeColor = _usuario.EsAdmin ? Theme.BrightCyan : Theme.AccentSuccess,
                Location = new Point(62, 36),
                AutoSize = true
            };

            panelUser.Controls.Add(pnlAvatar);
            panelUser.Controls.Add(lblUserNombre);
            panelUser.Controls.Add(lblUserRol);

            // ── Sección MENÚ label ──
            var lblMenuSec = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Text = "  MENÚ PRINCIPAL",
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                BackColor = Theme.SidebarBg,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0)
            };

            // ── Botones de navegación ──
            btnNavVentas       = CrearBotonNav("🛒   Registrar Venta",       (s, e) => AbrirFormInPanel(new FormVenta(_usuario),        "Terminal de Ventas",               btnNavVentas));
            btnNavConsultaStock = CrearBotonNav("📦   Consultar Stock",        (s, e) => AbrirFormInPanel(new FormStockConsulta(),        "Consulta de Stock y Precios",      btnNavConsultaStock));
            btnNavControlStock  = CrearBotonNav("📊   Control Stock",          (s, e) => AbrirFormInPanel(new FormStockAdmin(),           "Control Total de Stock",           btnNavControlStock));
            btnNavHistorial     = CrearBotonNav("📑   Historial de Ventas",    (s, e) => AbrirFormInPanel(new FormHistorialVentas(),      "Historial y Auditoría de Ventas",  btnNavHistorial));
            btnNavIncidencias   = CrearBotonNav("⚠️   Incidencias",            (s, e) => AbrirFormInPanel(new FormIncidencias(_usuario),  "Reportes de Incidencias",          btnNavIncidencias));
            btnNavUsuarios      = CrearBotonNav("👥   Trabajadores",           (s, e) => AbrirFormInPanel(new FormUsuarios(),             "Administración de Trabajadores",    btnNavUsuarios));

            // ── Botón cerrar sesión ──
            btnCerrarSesion = new Button
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                Text = "  🚪   Cerrar Sesión",
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(18, 22, 40),
                ForeColor = Theme.AccentDanger,
                Font = Theme.BodyBoldFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btnCerrarSesion.MouseEnter += (s, e) => btnCerrarSesion.BackColor = Color.FromArgb(60, 20, 20);
            btnCerrarSesion.MouseLeave += (s, e) => btnCerrarSesion.BackColor = Color.FromArgb(18, 22, 40);
            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            // Línea separadora encima del cerrar sesión
            var sepBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = Theme.BorderColor
            };

            // Ensamblar sidebar (DockStyle.Top se apila en reversa)
            panelSidebar.Controls.Add(btnNavUsuarios);
            panelSidebar.Controls.Add(btnNavIncidencias);
            panelSidebar.Controls.Add(btnNavHistorial);
            panelSidebar.Controls.Add(btnNavControlStock);
            panelSidebar.Controls.Add(btnNavConsultaStock);
            panelSidebar.Controls.Add(btnNavVentas);
            panelSidebar.Controls.Add(lblMenuSec);
            panelSidebar.Controls.Add(panelUser);
            panelSidebar.Controls.Add(panelLogo);
            panelSidebar.Controls.Add(btnCerrarSesion);
            panelSidebar.Controls.Add(sepBottom);

            // =====================================================
            // TOP BAR
            // =====================================================
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 54,
                BackColor = Theme.CardBg
            };
            panelTop.Paint += (s, e) =>
            {
                using var pen = new Pen(Theme.BorderColor, 1);
                e.Graphics.DrawLine(pen, 0, panelTop.Height - 1, panelTop.Width, panelTop.Height - 1);
                // Línea cian de acento en la parte inferior izquierda
                using var penAccent = new Pen(Color.FromArgb(60, Theme.PrimaryCyan), 2);
                e.Graphics.DrawLine(penAccent, 0, panelTop.Height - 1, 300, panelTop.Height - 1);
            };
            panelTop.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };
            panelTop.DoubleClick += (s, e) => ToggleMaximizar();

            lblTituloModulo = new Label
            {
                Text = "Terminal de Ventas",
                Font = Theme.TitleFont,
                ForeColor = Theme.BrightCyan,
                AutoSize = true,
                Location = new Point(18, 14)
            };
            lblTituloModulo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            // Botones de ventana
            var panelWinBtns = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 126,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(3, 10, 3, 0)
            };

            btnMinimizar = Theme.CreateWindowControlButton("—", Color.FromArgb(35, 52, 85), Color.White,
                (s, e) => this.WindowState = FormWindowState.Minimized);
            btnMaximizar = Theme.CreateWindowControlButton("🗖", Color.FromArgb(35, 52, 85), Color.White,
                (s, e) => ToggleMaximizar());
            btnCerrar = Theme.CreateWindowControlButton("✕", Theme.AccentDanger, Color.White,
                (s, e) => CerrarAplicacion());

            panelWinBtns.Controls.Add(btnMinimizar);
            panelWinBtns.Controls.Add(btnMaximizar);
            panelWinBtns.Controls.Add(btnCerrar);

            lblReloj = new Label
            {
                Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss"),
                Font = Theme.SmallBoldFont,
                ForeColor = Theme.TextMuted,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Width = 185,
                Padding = new Padding(0, 0, 8, 0)
            };
            lblReloj.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            var lblStatusBd = new Label
            {
                Text = "🟢  SQL Server",
                Font = Theme.SmallFont,
                ForeColor = Theme.AccentSuccess,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Width = 118,
                Padding = new Padding(0, 0, 4, 0)
            };
            lblStatusBd.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) MoverVentana(); };

            panelTop.Controls.Add(lblTituloModulo);
            panelTop.Controls.Add(lblStatusBd);
            panelTop.Controls.Add(lblReloj);
            panelTop.Controls.Add(panelWinBtns);

            // =====================================================
            // CONTENEDOR DE MÓDULOS
            // =====================================================
            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.BgDark
            };

            this.Controls.Add(panelContenido);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelSidebar);
        }

        private Button CrearBotonNav(string texto, EventHandler onClick)
        {
            var btn = new Button
            {
                Dock = DockStyle.Top,
                Height = 46,
                Text = texto,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Theme.SidebarBg,
                ForeColor = Theme.TextSoft,
                Font = Theme.BodyFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            btn.Click += onClick;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != _botonActivo)
                {
                    btn.BackColor = Color.FromArgb(22, 36, 62);
                    btn.ForeColor = Theme.TextWhite;
                }
            };
            btn.MouseLeave += (s, e) =>
            {
                if (btn != _botonActivo)
                {
                    btn.BackColor = Theme.SidebarBg;
                    btn.ForeColor = Theme.TextSoft;
                }
            };

            return btn;
        }

        private void ConfigurarPermisosPorRol()
        {
            if (!_usuario.EsAdmin)
            {
                btnNavControlStock.Visible = false;
                btnNavHistorial.Visible = false;
                btnNavUsuarios.Visible = false;
            }
        }

        private void ToggleMaximizar()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                if (btnMaximizar != null) btnMaximizar.Text = "🗖";
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                if (btnMaximizar != null) btnMaximizar.Text = "🗗";
            }
        }

        private void CerrarAplicacion()
        {
            var r = MessageBox.Show("¿Está seguro de salir del sistema ICE BAR?", "Confirmar Salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes) Application.Exit();
        }

        public void AbrirFormInPanel(object formHijo, string titulo, Button boton)
        {
            if (_botonActivo != null)
            {
                _botonActivo.BackColor = Theme.SidebarBg;
                _botonActivo.ForeColor = Theme.TextSoft;
                _botonActivo.Font = Theme.BodyFont;
            }

            _botonActivo = boton;
            _botonActivo.BackColor = Theme.SidebarActive;
            _botonActivo.ForeColor = Theme.BrightCyan;
            _botonActivo.Font = Theme.BodyBoldFont;

            lblTituloModulo.Text = titulo;

            panelContenido.Controls.Clear();

            if (formHijo is Form fh)
            {
                fh.TopLevel = false;
                fh.FormBorderStyle = FormBorderStyle.None;
                fh.Dock = DockStyle.Fill;
                panelContenido.Controls.Add(fh);
                panelContenido.Tag = fh;
                fh.Show();
            }
            else if (formHijo is Control c)
            {
                c.Dock = DockStyle.Fill;
                panelContenido.Controls.Add(c);
            }
        }

        private void MostrarModulo(UserControl control, string titulo, Button boton)
        {
            AbrirFormInPanel(control, titulo, boton);
        }

        private void IniciarReloj()
        {
            timerReloj = new System.Windows.Forms.Timer { Interval = 1000 };
            timerReloj.Tick += (s, e) =>
                lblReloj.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            timerReloj.Start();
        }

        private void BtnCerrarSesion_Click(object? sender, EventArgs e)
        {
            var r = MessageBox.Show("¿Deseas cerrar tu sesión en ICE BAR?", "Confirmar Salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                timerReloj.Stop();
                this.Hide();

                using var login = new FormLogin();
                if (login.ShowDialog() == DialogResult.OK && login.UsuarioAutenticado != null)
                {
                    var nuevoMain = new FormMain(login.UsuarioAutenticado);
                    nuevoMain.ShowDialog();
                }
                this.Close();
            }
        }
    }
}
