using System;
using System.Collections.Generic;
using System.Linq;

namespace Entity
{
    public class FacturaCompra : Factura
    {
        public DateTime FechaCompra { get; set; }

        public FacturaCompra(DateTime fecha)
        {
            FechaCompra = fecha;
        }
        public override decimal CalcularSubTotal()
        {
            return SubTotal = ConsultarDetalles().Sum(d => d.ValorUnitario);
        }

        public override decimal CalcularTotal()
        {
            return Total = ConsultarDetalles().Sum( f => f.ValorTotal);
        }

        public override decimal CalcularTotalDescuento()
        {
            return Descuento = ConsultarDetalles().Sum(d => d.ValorDescuento);
        }

        public override decimal CalcularTotalIVA()
        {
            return IVA = ConsultarDetalles().Sum( d => d.IVA);
        }

        public override decimal CalcularValorSinDescuento()
        {
            return ValorSinDescuento = ConsultarDetalles().Sum(d => (d.IVA + d.ValorUnitario));
        }

        public override string transaccionName()
        {
            return typeof(FacturaCompra).Name;
        }
    }
}