using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;
using IceBarSystem.Helpers;
using IceBarSystem.Models;

namespace IceBarSystem.Forms
{
    public partial class FormLogin : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        private bool isPlaceholderUsername = false;
        private bool isPlaceholderPassword = false;
        private const string PlaceholderUser = "Usuario";
        private const string PlaceholderPass = "Contraseña";

        public Usuario? UsuarioAutenticado { get; private set; }

        public FormLogin()
        {
            InitializeComponent();
            ConfigurarPlaceholders();
            CargarUsuarioRecordado();
        }

        private void MoverVentana_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void ConfigurarPlaceholders()
        {
            txtUsername.Text = PlaceholderUser;
            txtUsername.ForeColor = Color.Gray;
            isPlaceholderUsername = true;

            txtUsername.Enter += (s, e) =>
            {
                if (isPlaceholderUsername)
                {
                    txtUsername.Text = "";
                    txtUsername.ForeColor = Color.Black;
                    isPlaceholderUsername = false;
                }
            };

            txtUsername.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    txtUsername.Text = PlaceholderUser;
                    txtUsername.ForeColor = Color.Gray;
                    isPlaceholderUsername = true;
                }
            };

            txtPassword.Text = PlaceholderPass;
            txtPassword.ForeColor = Color.Gray;
            txtPassword.UseSystemPasswordChar = false;
            isPlaceholderPassword = true;

            txtPassword.Enter += (s, e) =>
            {
                if (isPlaceholderPassword)
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = Color.Black;
                    txtPassword.UseSystemPasswordChar = true;
                    isPlaceholderPassword = false;
                }
            };

            txtPassword.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.Text = PlaceholderPass;
                    txtPassword.ForeColor = Color.Gray;
                    txtPassword.UseSystemPasswordChar = false;
                    isPlaceholderPassword = true;
                }
            };
        }

        private void CargarUsuarioRecordado()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icebar_user.txt");
                if (System.IO.File.Exists(path))
                {
                    string user = System.IO.File.ReadAllText(path).Trim();
                    if (!string.IsNullOrEmpty(user))
                    {
                        txtUsername.Text = user;
                        txtUsername.ForeColor = Color.Black;
                        isPlaceholderUsername = false;
                        chkRecordar.Checked = true;
                        txtPassword.Focus();
                    }
                }
            }
            catch { }
        }

        private void GuardarUsuarioRecordado(string user)
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icebar_user.txt");
                if (chkRecordar.Checked)
                {
                    System.IO.File.WriteAllText(path, user);
                }
                else if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch { }
        }

        private string GetUsername()
        {
            return isPlaceholderUsername ? "" : txtUsername.Text.Trim();
        }

        private string GetPassword()
        {
            return isPlaceholderPassword ? "" : txtPassword.Text;
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";
            string username = GetUsername();
            string password = GetPassword();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Por favor, ingrese usuario y contraseña.";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "VERIFICANDO...";

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT IdUsuario, NombreCompleto, NombreUsuario, Rol, Activo 
                                     FROM Usuarios 
                                     WHERE NombreUsuario = @Usuario AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool activo = reader.GetBoolean(4);
                                if (!activo)
                                {
                                    lblError.Text = "El usuario se encuentra inactivo. Contacte al administrador.";
                                    btnLogin.Enabled = true;
                                    btnLogin.Text = "ACCEDER";
                                    return;
                                }

                                UsuarioAutenticado = new Usuario
                                {
                                    IdUsuario = reader.GetInt32(0),
                                    NombreCompleto = reader.GetString(1),
                                    NombreUsuario = reader.GetString(2),
                                    Rol = reader.GetString(3),
                                    Activo = activo
                                };

                                GuardarUsuarioRecordado(username);

                                AuditoriaHelper.RegistrarAccion(
                                    UsuarioAutenticado.IdUsuario,
                                    "Seguridad",
                                    "Login",
                                    $"Inicio de sesión exitoso de {UsuarioAutenticado.NombreCompleto} ({UsuarioAutenticado.Rol})");

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                                return;
                            }
                            else
                            {
                                lblError.Text = "Usuario o contraseña incorrectos.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error de conexión: " + ex.Message;
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "ACCEDER";
            }
        }

        private void txtUsername_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnConfigConexion_Click(object? sender, EventArgs e)
        {
            using (var configForm = new FormConfigConexion())
            {
                configForm.ShowDialog(this);
            }
        }

        private void btnMinimizar_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
