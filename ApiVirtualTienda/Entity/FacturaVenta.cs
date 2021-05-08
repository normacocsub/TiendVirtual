using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entity
{
    public class FacturaVenta : Factura
    {
        
        [NotMapped]
        public UsuarioInteresado UsuarioInteresado { get; set; }
        public string InteresadoId { get; set; }
        [NotMapped]
        public Usuario Usuario { get; set; }
        public string UsuarioVentasId { get; set; }

        
        public override decimal CalcularSubTotal()
        {
            return SubTotal = ConsultarDetalles().Sum( d => d.ValorUnitario * d.Cantidad);
        }

        public override decimal CalcularTotal()
        {
            return Total = ConsultarDetalles().Sum( f => f.ValorTotal * f.Cantidad );
        }

        public override decimal CalcularTotalDescuento()
        {
            return Descuento = ConsultarDetalles().Sum( d => d.ValorDescuento * d.Cantidad);
        }

        public override decimal CalcularTotalIVA()
        {
            return IVA = ConsultarDetalles().Sum( d => d.IVA * d.Cantidad);
        }

        public override decimal CalcularValorSinDescuento()
        {
           return ValorSinDescuento = ConsultarDetalles().Sum( d => (d.IVA + d.ValorUnitario) * d.Cantidad);
        }

        public override string transaccionName()
        {
            return typeof(FacturaVenta).Name;
        }
    }
}