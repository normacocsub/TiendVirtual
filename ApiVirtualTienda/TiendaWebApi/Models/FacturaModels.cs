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
            factura.Detalles.ForEach(d => {
                var detalle = new DetalleInputModels{
                    Codigo = d.Codigo,
                    Cantidad = d.Cantidad,
                    Descuento = d.Descuento,
                    Fecha = d.Fecha,
                    IVA = d.IVA,
                    CodigoProducto = d.ProductoId,
                    ValorTotal = d.ValorTotal,
                    ValorUnitario = d.ValorUnitario
                };
                Detalles.Add(detalle);
            });

        }
    }
}