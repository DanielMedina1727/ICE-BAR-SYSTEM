using System;
using System.Collections.Generic;

namespace IceBarSystem.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public string NumeroTicket { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public decimal MontoPagado { get; set; } // Precio cancelado / pagado por el cliente
        public decimal Cambio { get; set; }
        public string TipoPago { get; set; } = "Efectivo"; // 'Efectivo', 'Tarjeta', 'Transferencia'
        public string? Notas { get; set; }

        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }

    public class DetalleVenta
    {
        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
