using System.Collections.Generic;
using Entity;

namespace TiendaWebApi.Models
{
    public class ProveedorInputModels
    {
        public string NIT { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        //public List<ProductoInputModels> Productos { get; set; }
    }

    public class ProveedorViewModels : ProveedorInputModels
    {
        public ProveedorViewModels(){}
        public ProveedorViewModels(Proveedor proveedor)
        {
            NIT = proveedor.NIT;
            Nombre = proveedor.Nombre;
            Telefono = proveedor.Telefono;
        }
    }
}