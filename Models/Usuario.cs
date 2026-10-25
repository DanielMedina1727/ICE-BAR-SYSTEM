using System;

namespace IceBarSystem.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "Trabajador"; // 'Administrador' o 'Trabajador'
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        public bool EsAdmin => Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
    }
}
