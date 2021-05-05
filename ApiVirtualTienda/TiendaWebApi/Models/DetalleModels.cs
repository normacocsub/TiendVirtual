using System;

namespace TiendaWebApi.Models
{
    public class DetalleInputModels
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Cantidad { get; set; }
        public string CodigoProducto { get; set; }
    }

    public class DetalleViewModel : DetalleInputModels
    {

    }
}