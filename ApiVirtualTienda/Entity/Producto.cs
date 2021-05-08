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
        public string Estado { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal Descuento { get; set; }
        [NotMapped]
        public Proveedor Proveedor { get; set; }
        [ForeignKey("ProveedorNIT")]
        public string ProveedorNIT { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal IVA { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal ValorDescontado { get; set; }
        [Column(TypeName = "decimal(17,4)")]
        public decimal ValorUnitario { get; set; }

        public Producto()
        {
            
        }


        
    }
}