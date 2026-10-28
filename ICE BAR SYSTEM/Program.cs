using System;
using System.Windows.Forms;
using IceBarSystem.Forms;

namespace IceBarSystem
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación ICE BAR.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Abrir la pantalla de inicio de sesión de ICE BAR
            using var loginForm = new FormLogin();
            if (loginForm.ShowDialog() == DialogResult.OK && loginForm.UsuarioAutenticado != null)
            {
                Application.Run(new FormMain(loginForm.UsuarioAutenticado));
            }
        }
    }
}