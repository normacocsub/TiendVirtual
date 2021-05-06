using System;
using Entity;

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
        public DetalleViewModel(){}
        public DetalleViewModel(Detalle detalle)
        {
            Codigo = detalle.Codigo;
            Fecha = detalle.Fecha;
            Descuento = detalle.Descuento;
            IVA = detalle.IVA;
            ValorTotal = detalle.ValorTotal;
            ValorUnitario = detalle.ValorUnitario;
            Cantidad = detalle.Cantidad;
            CodigoProducto = detalle.ProductoId;
        }
    }
}