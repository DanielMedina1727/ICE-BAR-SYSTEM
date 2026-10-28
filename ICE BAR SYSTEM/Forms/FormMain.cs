using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IceBarSystem.Models;

namespace IceBarSystem.Forms
{
    public partial class FormMain : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private readonly Usuario _usuario;
        private readonly System.Windows.Forms.Timer _timerReloj;

        public FormMain(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            lblUsuarioInfo.Text = $"👤 {_usuario.NombreCompleto}  |  Rol: {_usuario.Rol}";

            _timerReloj = new System.Windows.Forms.Timer { Interval = 1000 };
            _timerReloj.Tick += (s, e) => { lblReloj.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); };
            _timerReloj.Start();
            lblReloj.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            ConfigurarPermisos();

            // Abrir Dashboard al iniciar
            AbrirFormInPanel(new FormDashboard(_usuario));
            lblModuloActual.Text = "Panel de Control / Dashboard";
        }

        public FormMain(int usuarioID, string nombreUsuario, string rolUsuario)
            : this(new Usuario { IdUsuario = usuarioID, NombreCompleto = nombreUsuario, NombreUsuario = nombreUsuario, Rol = rolUsuario, Activo = true })
        {
        }

        private void ConfigurarPermisos()
        {
            if (_usuario.Rol == "Trabajador")
            {
                // Un trabajador no puede gestionar usuarios
                btnUsuarios.Visible = false;
                SubmenuMantenedor.Height = 80;

                // Si se desea ocultar el mantenedor completo para trabajadores:
                // btnMantenedor.Visible = false;
                // SubmenuMantenedor.Visible = false;
            }
        }

        private void BarraTitulo_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        // ===== MÉTODO PARA ABRIR FORMULARIOS EN EL PANEL (Estilo xdnt123) =====
        public void AbrirFormInPanel(object Formhijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
            {
                var ctrl = this.panelContenedor.Controls[0];
                this.panelContenedor.Controls.RemoveAt(0);
                if (ctrl is Form prevForm)
                {
                    prevForm.Dispose();
                }
            }

            if (Formhijo is Form fh)
            {
                fh.TopLevel = false;
                fh.FormBorderStyle = FormBorderStyle.None;
                fh.Dock = DockStyle.Fill;
                this.panelContenedor.Controls.Add(fh);
                this.panelContenedor.Tag = fh;
                fh.Show();
            }
        }

        // ===== SUBMENÚS =====
        private void btnMantenedor_Click(object? sender, EventArgs e)
        {
            SubmenuMantenedor.Visible = !SubmenuMantenedor.Visible;
        }

        private void btnVentas_Click(object? sender, EventArgs e)
        {
            SubmenuVentas.Visible = !SubmenuVentas.Visible;
        }

        // ===== BOTONES DE NAVEGACIÓN =====
        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            lblModuloActual.Text = "Panel de Control / Dashboard";
            AbrirFormInPanel(new FormDashboard(_usuario));
        }

        private void btnProductos_Click(object? sender, EventArgs e)
        {
            SubmenuMantenedor.Visible = false;
            lblModuloActual.Text = "Productos e Inventario";
            AbrirFormInPanel(new FormStockAdmin(_usuario));
        }

        private void btnConsultaStock_Click(object? sender, EventArgs e)
        {
            SubmenuMantenedor.Visible = false;
            lblModuloActual.Text = "Consulta de Catálogo y Stock";
            AbrirFormInPanel(new FormStockConsulta(_usuario));
        }

        private void btnUsuarios_Click(object? sender, EventArgs e)
        {
            SubmenuMantenedor.Visible = false;
            lblModuloActual.Text = "Gestión de Usuarios del Sistema";
            AbrirFormInPanel(new FormUsuarios(_usuario));
        }

        private void btnRegistrarVenta_Click(object? sender, EventArgs e)
        {
            SubmenuVentas.Visible = false;
            lblModuloActual.Text = "Terminal Punto de Venta (POS)";
            AbrirFormInPanel(new FormVenta(_usuario));
        }

        private void btnHistorialVentas_Click(object? sender, EventArgs e)
        {
            SubmenuVentas.Visible = false;
            lblModuloActual.Text = "Historial de Ventas";
            AbrirFormInPanel(new FormHistorialVentas(_usuario));
        }

        private void btnIncidencias_Click(object? sender, EventArgs e)
        {
            lblModuloActual.Text = "Registro y Gestión de Incidencias";
            AbrirFormInPanel(new FormIncidencias(_usuario));
        }

        private void btnConfigConexion_Click(object? sender, EventArgs e)
        {
            using (var config = new FormConfigConexion())
            {
                config.ShowDialog(this);
            }
        }

        private void btnCerrSes_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Confirmar Salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _timerReloj?.Stop();
                this.Hide();

                using (var loginForm = new FormLogin())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK && loginForm.UsuarioAutenticado != null)
                    {
                        var nuevoMain = new FormMain(loginForm.UsuarioAutenticado);
                        nuevoMain.Show();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }

        // ===== CONTROLES DE VENTANA =====
        private void iconminimizar_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconmaximizar_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconmaximizar.Visible = false;
            iconrestaurar.Visible = true;
        }

        private void iconrestaurar_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconcerrar_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de salir del sistema ICE BAR?", "Confirmar Salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public Usuario ObtenerUsuario() => _usuario;
    }
}
