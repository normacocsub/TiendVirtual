using System.Collections.Generic;

namespace TiendaWebApi.Models
{
    public class ProveedorInputModels
    {
        public string NIT { get; set; }
        public string Nombre { get; set; }
        public List<ProductoInputModels> Productos { get; set; }
    }

    public class ProveedorViewModels : ProductoInputModels
    {

    }
}