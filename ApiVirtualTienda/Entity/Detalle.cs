using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Detalle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal ValorTotal { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorUnitario { get; set; }
        [NotMapped]
        public Producto Producto { get; set; }
        public string ProductoId { get; set; }

        public Detalle()
        {
            
        }

        public decimal CalcularDescuento()
        {
            return ValorDescuento = ( ValorUnitario * ( Descuento / 100 ));
        }

        public decimal CalcularIVA()
        {
            return IVA = ( ValorUnitario * 0.19m );
        }

        public decimal CalcularTotal()
        {
            CalcularDescuento();
            CalcularIVA();
            if(Descuento == 100)
            {
                return ValorTotal = 0;
            }
            return ValorTotal = ( IVA + (ValorUnitario - ValorDescuento ));
        }
    }
}