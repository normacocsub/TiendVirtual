using System.Collections.Generic;

namespace TiendaWebApi.Models
{
    public class FacturaInputModels
    {
        public string Codigo { get; set; }
        public decimal IVA { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DetalleInputModels Detalle { get; set; }
        public List<DetalleInputModels> Detalles { get; set; }
        public InteresadoInputModels Interesado { get; set; }
    }

    public class FacturaViewModels : FacturaInputModels
    {
        
    }
}