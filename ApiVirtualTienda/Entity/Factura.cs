using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entity
{
    public abstract class Factura
    {
        [Key]
        public string Codigo { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal IVA { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal Descuento { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal Total { get; set; }
        [NotMapped]
        public Detalle Detalle { get; set; }
        [NotMapped]
        private List<Detalle> Detalles { get; set; }
        public DateTime Fecha { get; set; }
        [Column(TypeName = "decimal(17,4)")]
         public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal ValorSinDescuento { get; set; }

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
                ValorTotal = detalle.ValorTotal,
                ValorUnitario = detalle.Producto.ValorUnitario,
                ProductoId = detalle.Producto.Codigo,
                Cantidad = detalle.Cantidad,
            };
            Detalle.CalcularTotal();
            Detalles.Add(Detalle);
        }

        public List<Detalle> ConsultarDetalles()
        {
            return Detalles;
        }

        public abstract decimal CalcularTotalDescuento();

        public abstract decimal CalcularTotalIVA();
        public abstract decimal CalcularSubTotal();

        public abstract decimal CalcularValorSinDescuento();
        public decimal CalcularCantidad()
        {
            return Cantidad = Detalles.Sum(d => d.Cantidad);
        }

        public abstract decimal CalcularTotal();

        public void CalcularTotales()
        {
            CalcularValorSinDescuento();
            CalcularSubTotal();
            CalcularTotalDescuento();
            CalcularTotalIVA();
            CalcularCantidad();
            CalcularTotal();
        }

        public abstract string transaccionName();
        
    }
}