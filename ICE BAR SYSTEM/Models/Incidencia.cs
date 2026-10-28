using System;

namespace IceBarSystem.Models
{
    public class Incidencia
    {
        public int IdIncidencia { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Reportado";
    }
}
