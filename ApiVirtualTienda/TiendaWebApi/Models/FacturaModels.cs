using System;
using System.Collections.Generic;
using Entity;

namespace TiendaWebApi.Models
{
    public class FacturaInputModels
    {
        public string Codigo { get; set; }
        public decimal IVA { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        public string EstadoTransaccion { get; set; }
        public decimal Total { get; set; }
        public List<DetalleInputModels> Detalles { get; set; }
        public string InteresadoId { get; set; }
        public string UsuarioVentasId { get; set; }
        public DateTime Fecha { get; set; }

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
            if(factura.transaccionName().Equals("FacturaCompra"))
            {
                EstadoTransaccion = "Compra";
            }
            else
            {
                EstadoTransaccion = "Venta";
            }
            Fecha = factura.Fecha;
            factura.ConsultarDetalles().ForEach(d => {
                var detalle = new DetalleViewModel(d);
                Detalles.Add(detalle);
            });

        }
    }
}