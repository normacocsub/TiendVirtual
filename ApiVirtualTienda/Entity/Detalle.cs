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
        public decimal ValorUnitario { get; set; }
        [NotMapped]
        public Producto Producto { get; set; }
        public string ProductoId { get; set; }
    }
}