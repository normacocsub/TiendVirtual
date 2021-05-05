using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class ProductoService
    {  
        private readonly TiendaVirtualContext _context;
        private readonly ProveedorService _serviceProveedor;
        public ProductoService(TiendaVirtualContext context)
        {
            _context = context;
            _serviceProveedor = new ProveedorService(context);
        }





        public GuardarProductoResponse GuardarProducto(Producto producto)
        {
            try
            {
                var response = _context.Productos.Find(producto.Codigo);
                if(response == null)
                {
                    var responseProvedoor = _serviceProveedor.RegistrarProveedor(producto.Proveedor);
                    if(responseProvedoor.Estado != "Registrado")
                    {
                        _context.Productos.Add(producto);
                    }
                    _context.SaveChanges();
                    return new GuardarProductoResponse(producto);
                }
                else
                {
                    return new GuardarProductoResponse("El producto ya se encuentro registrado", "Registrado");
                }
            }
            catch(Exception e)
            {
                return new GuardarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public ConsultarProductoResponse ConsultarProductos()
        {
            try
            {
                var productosResponse = _context.Productos.ToList();
                productosResponse.ForEach(p => {p.Proveedor = _context.Proveedores.Find(p.ProveedorNIT);});
                return new ConsultarProductoResponse(productosResponse);
            }
            catch(Exception e)
            {
                return new ConsultarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }


        public class ConsultarProductoResponse
        {
            public ConsultarProductoResponse(List<Producto> productos )
            {
                Error = false;
                Productos = productos;
            }
            public ConsultarProductoResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public List<Producto> Productos { get; set; }
        }

        public class GuardarProductoResponse
        {
            public GuardarProductoResponse(Producto producto)
            {
                Error = false;
                Producto = producto;
            }
            public GuardarProductoResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public Producto Producto { get; set; }
        }
    }
}