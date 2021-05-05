using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entity
{
    public class Factura
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Codigo { get; set; }
        public decimal IVA { get; set; }
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        [NotMapped]
        public Detalle Detalle { get; set; }
        public List<Detalle> Detalles { get; set; }
        [NotMapped]
        public UsuarioInteresado UsuarioInteresado { get; set; }
        public string InteresadoId { get; set; }

        public Factura()
        {
            Detalles = new List<Detalle>();
        }

        public void AgregarDetalle(Detalle detalle)
        {
            Detalle = new Detalle
            {
                Descuento = detalle.Producto.Descuento,
                Fecha = DateTime.Now,
                IVA = detalle.Producto.IVA,
                Producto = detalle.Producto,
                ValorTotal = detalle.Producto.ValorTotal,
                ValorUnitario = detalle.Producto.ValorUnitario,
                ProductoId = detalle.Producto.Codigo,
                Cantidad = detalle.Cantidad
            };
            Detalles.Add(Detalle);
        }

        public decimal CalcularTotalDescuento()
        {
            return Descuento = Detalles.Sum(f => f.Descuento * f.Cantidad);
        }

        public decimal CalcularTotalIVA()
        {
            return IVA = Detalles.Sum(f => f.IVA * f.Cantidad);
        }

        public decimal CalcularCantidad()
        {
            return Cantidad = Detalles.Count;
        }

        public decimal CalcularTotal()
        {
            CalcularTotalDescuento();
            CalcularTotalIVA();
            CalcularCantidad();
            return Total = Detalles.Sum(f => f.ValorTotal * f.Cantidad);
        }
        
    }
}