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
        private readonly FacturaService _serviceFactura;
        public ProductoService(TiendaVirtualContext context)
        {
            _context = context;
            _serviceProveedor = new ProveedorService(context);
            _serviceFactura = new FacturaService(context, "");
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
                        if(responseProvedoor.Error)
                        {
                            if(responseProvedoor.Estado == "Error")
                            {
                                return new GuardarProductoResponse($"Error {responseProvedoor.Mensaje}", "Error");
                            }
                        }
                    }
                    Detalle detalle = new Detalle(){Producto = producto};
                    _serviceFactura.CrearFacturaCompraProducto(detalle, producto.ValorDescontado, producto.IVA);
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

        public BuscarProductoResponse BuscarProducto(string codigo)
        {
            try
            {
                var response = _context.Productos.Find(codigo);
                if(response != null)
                {
                    return new BuscarProductoResponse(response);
                }
                else
                {
                    return new BuscarProductoResponse("No existe el producto", "NoEiste");
                }
            }
            catch(Exception e)
            {
                return new BuscarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public EditarProductoResponse EditarProducto(Producto producto)
        {
            try
            {
                var response = _context.Productos.Find(producto.Codigo);
                if(response != null)
                {
                    response.Descripcion = producto.Descripcion;
                    response.Descuento = producto.Descuento;
                    response.ValorUnitario = producto.ValorUnitario;
                    response.CalcularTotal();
                    _context.Productos.Update(response);
                    _context.SaveChanges();
                    return new EditarProductoResponse(response);
                }
                else
                {
                    return new EditarProductoResponse("No existe el producto","NoExiste");
                }
            }
            catch(Exception e)
            {
                return new EditarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }
            
        }

        public EditarProductoResponse ActualizarCantidad(int cantidad, string codigo)
        {
            try
            {
                var result = _context.Productos.Find(codigo);
                if(result != null)
                {
                    result.Cantidad += cantidad;
                    _context.Productos.Update(result);
                    _context.SaveChanges();
                    return new EditarProductoResponse(result);
                }
                else
                {
                    return new EditarProductoResponse("No existe el producto", "NoExiste");
                }
            }
            catch(Exception e)
            {
                return new EditarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public EditarProductoResponse DescontarCantidad(int cantidad, string codigo)
        {
            try
            {
                var response = _context.Productos.Find(codigo);
                if(response != null)
                {
                    if(response.Cantidad >= cantidad )
                    {
                        response.Cantidad -= cantidad;
                        return new EditarProductoResponse(response);
                    }
                    else
                    {
                        return new EditarProductoResponse("No esta disponible la cantidad deseada", "Insuficiente");
                    }
                }
                else
                {
                    return new EditarProductoResponse("No existe el producto", "NoExiste");
                }
            }
            catch(Exception e)
            {
                return new EditarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public EditarProductoResponse ActualizarEstado(string codigo, string estado)
        {
            try
            {
                var response = _context.Productos.Find(codigo);
                if(response != null)
                {
                    response.Estado = estado;
                    _context.Productos.Update(response);
                    _context.SaveChanges();
                    return new EditarProductoResponse(response);
                }
                return new EditarProductoResponse("No existe el producto", "NoExiste");
            }
            catch(Exception e)
            {
                return new EditarProductoResponse($"Error en la aplicacion: {e.Message}","Error");
            }

        }

        public class EditarProductoResponse
        {
            public EditarProductoResponse(Producto producto )
            {
                Error = false;
                Producto = producto;
            }
            public EditarProductoResponse(string mensaje, string estado)
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

        public class BuscarProductoResponse
        {
            public BuscarProductoResponse(Producto producto )
            {
                Error = false;
                Producto = producto;
            }
            public BuscarProductoResponse(string mensaje, string estado)
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