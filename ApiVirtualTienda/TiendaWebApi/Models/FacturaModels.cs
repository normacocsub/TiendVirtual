using System.Collections.Generic;
using Entity;

namespace TiendaWebApi.Models
{
    public class FacturaInputModels
    {
        public int Codigo { get; set; }
        public decimal IVA { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public string EstadoTransaccion { get; set; }
        public decimal Total { get; set; }
        public List<DetalleInputModels> Detalles { get; set; }
        public string InteresadoId { get; set; }
        public string UsuarioVentasId { get; set; }

        public FacturaInputModels()
        {
            Detalles = new List<DetalleInputModels>();
        }
    }

    public class FacturaViewModels : FacturaInputModels
    {
        public FacturaViewModels(){}
        public FacturaViewModels(Factura factura)
        {
            Codigo = factura.Codigo;
            IVA = factura.IVA;
            Descuento = factura.Descuento;
            Cantidad = factura.Cantidad;
            Estado = factura.Estado;
            Total = factura.Total;
            InteresadoId = factura.InteresadoId;
            UsuarioVentasId = factura.UsuarioVentasId;
            EstadoTransaccion = factura.EstadoTransaccion;
            factura.Detalles.ForEach(d => {
                var detalle = new DetalleViewModel(d);
                Detalles.Add(detalle);
            });

        }
    }
}