using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;

namespace IceBarSystem.Forms
{
    public partial class FormConfigConexion : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hwnd, int wmsg, int wparam, int lparam);

        public FormConfigConexion()
        {
            InitializeComponent();
            CargarConfiguracionActual();
        }

        private void MoverVentana_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void CargarConfiguracionActual()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(DatabaseHelper.ConnectionString);
                txtServidor.Text = builder.DataSource;
                txtBaseDatos.Text = builder.InitialCatalog;
                chkWindowsAuth.Checked = builder.IntegratedSecurity;
                txtUsuarioSql.Text = builder.UserID;
                txtPasswordSql.Text = builder.Password;

                chkWindowsAuth_CheckedChanged(this, EventArgs.Empty);
            }
            catch
            {
                txtServidor.Text = ".";
                txtBaseDatos.Text = "IceBarDB";
                chkWindowsAuth.Checked = true;
            }
        }

        private string ConstruirConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServidor.Text.Trim(),
                InitialCatalog = txtBaseDatos.Text.Trim(),
                TrustServerCertificate = true
            };

            if (chkWindowsAuth.Checked)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = txtUsuarioSql.Text.Trim();
                builder.Password = txtPasswordSql.Text;
            }

            return builder.ConnectionString;
        }

        private void chkWindowsAuth_CheckedChanged(object? sender, EventArgs e)
        {
            bool sqlAuth = !chkWindowsAuth.Checked;
            txtUsuarioSql.Enabled = sqlAuth;
            txtPasswordSql.Enabled = sqlAuth;
        }

        private void btnProbar_Click(object? sender, EventArgs e)
        {
            string connStr = ConstruirConnectionString();
            lblEstado.Text = "Probando conexión con el servidor SQL...";
            lblEstado.ForeColor = Color.Blue;
            btnProbar.Enabled = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                }

                lblEstado.Text = "✓ Conexión establecida exitosamente con la base de datos.";
                lblEstado.ForeColor = Color.FromArgb(46, 204, 113);
                MessageBox.Show("¡Conexión exitosa con el servidor SQL Server!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblEstado.Text = "✗ Error al conectar: " + ex.Message;
                lblEstado.ForeColor = Color.FromArgb(231, 76, 60);
                MessageBox.Show("No se pudo conectar: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProbar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            string connStr = ConstruirConnectionString();
            DatabaseHelper.ConnectionString = connStr;

            lblEstado.Text = "✓ Cadena de conexión guardada.";
            lblEstado.ForeColor = Color.FromArgb(46, 204, 113);
            MessageBox.Show("La configuración de conexión se guardó exitosamente.", "Configuración Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnInicializarBD_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea crear las tablas y datos iniciales en la base de datos especificada?",
                "Inicializar Base de Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            btnInicializarBD.Enabled = false;
            lblEstado.Text = "Inicializando tablas y catálogo del Bar...";
            lblEstado.ForeColor = Color.Blue;

            try
            {
                string connStr = ConstruirConnectionString();
                DatabaseHelper.EnsureDatabaseSetup(connStr);

                lblEstado.Text = "✓ Tablas y datos iniciales creados exitosamente.";
                lblEstado.ForeColor = Color.FromArgb(46, 204, 113);
                MessageBox.Show("Base de datos y tablas inicializadas correctamente con productos y usuarios de prueba.",
                    "Inicialización Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblEstado.Text = "✗ Error en inicialización: " + ex.Message;
                lblEstado.ForeColor = Color.FromArgb(231, 76, 60);
                MessageBox.Show("Error al inicializar la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnInicializarBD.Enabled = true;
            }
        }

        private void btnCerrarHeader_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
