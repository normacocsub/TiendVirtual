using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Proveedor
    {
        [Key]
        public string NIT { get; set; }
        public string Nombre { get; set; }
        [NotMapped]
        public Producto Producto { get; set; }
        public string Telefono { get; set; }
        //public List<Producto> Productos { get; set; }

        public Proveedor()
        {
            //Productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            //Productos.Add(producto);
        }
    }
}