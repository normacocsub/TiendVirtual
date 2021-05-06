using System;
using Entity;

namespace TiendaWebApi.Models
{
    public class ProductoInputModels
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal ValorDescontado { get; set; }
        public decimal ValorTotal { get; set; }
        public ProveedorInputModels Proveedor { get; set; }
        public string IdProveedor { get; set; }
        public decimal ValorUnitario { get; set; }
        public string Estado { get; set; }
    }

    public class ProductoViewModel : ProductoInputModels
    {
        public ProductoViewModel(){}

        public ProductoViewModel(Producto producto)
        {
            Codigo = producto.Codigo;
            Descripcion = producto.Descripcion;
            Cantidad = producto.Cantidad;
            Fecha = producto.Fecha;
            Descuento = producto.Descuento;
            Proveedor = new ProveedorViewModels(producto.Proveedor);
            IdProveedor = producto.ProveedorNIT;
            ValorUnitario = producto.ValorUnitario;
            Estado = producto.Estado;
        }
    }
}