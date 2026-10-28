using System;

namespace IceBarSystem.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty; // Incluye descripción ej: "Coca Cola 600ml"
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; } = "General";
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;

        public string EstadoStock
        {
            get
            {
                if (Stock <= 0) return "Agotado";
                if (Stock <= 5) return "Bajo Stock";
                return "Disponible";
            }
        }
    }
}
