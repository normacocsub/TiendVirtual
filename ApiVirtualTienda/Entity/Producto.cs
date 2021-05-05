using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Producto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Descuento { get; set; }
        [NotMapped]
        public Proveedor Proveedor { get; set; }
        [ForeignKey("ProveedorNIT")]
        public string ProveedorNIT { get; set; }
        public decimal IVA { get; set; }
        public decimal ValorDescontado { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        public Producto()
        {
            
        }

        public decimal CalcularDescuento()
        {
            return ValorDescontado = ( ValorUnitario * ( Descuento / 100 ));
        }

        public decimal CalcularIVA()
        {
            return IVA = ( ValorUnitario * 0.19m );
        }

        public decimal CalcularTotal()
        {
            CalcularDescuento();
            CalcularIVA();
            return ValorTotal = ( IVA + (ValorUnitario - ValorDescontado ));
        }
    }
}