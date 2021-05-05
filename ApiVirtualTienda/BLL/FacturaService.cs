using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;

namespace BLL
{
    public class FacturaService
    {
        private readonly TiendaVirtualContext _context;
        private readonly ProductoService _serviceProducto;
        public FacturaService(TiendaVirtualContext context)
        {
            _context = context;
            _serviceProducto = new ProductoService(context);
        }

        public FacturaService(TiendaVirtualContext context, string estado)
        {
            _context = context;
        }

        public GuardarFacturaResponse CrearFactura(Factura factura)
        {
            try
            {
                var response = _context.Facturas.Find(factura.Codigo);
                if(response == null)
                {
                    _context.Facturas.Add(factura);
                    bool estadoTransaction = false;
                    foreach (var item in factura.Detalles)
                    {
                        var result = _serviceProducto.DescontarCantidad(item.Cantidad, item.ProductoId);
                        if(result.Error)
                        {
                            estadoTransaction = true;
                            break;
                        }
                        _context.Productos.Update(result.Producto);
                    }
                    if(estadoTransaction) return new GuardarFacturaResponse("Error al crear la solicitud los detalles no se guardaron", "Error");
                    
                    _context.SaveChanges();
                    return new GuardarFacturaResponse(factura);
                }
                else
                {
                    return new GuardarFacturaResponse("La factura ya se encuentra registrada", "Registrada");
                }
            }
            catch(Exception e)
            {
                return new GuardarFacturaResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public GuardarFacturaResponse CrearFacturaCompraProducto(Detalle detalle, decimal descuento, decimal IVA)
        {
            try
            {
                Factura factura = new Factura();
                detalle.Producto.Codigo = (_context.Productos.ToList().Count + 1).ToString();
                detalle.Producto.CalcularTotal();
                _context.Productos.Add(detalle.Producto);
                detalle.Cantidad = detalle.Producto.Cantidad;
                
                factura.AgregarDetalle(detalle);
                factura.Descuento = descuento;
                factura.CalcularCantidad();
                factura.IVA = IVA;
                _context.Facturas.Add(factura);
                _context.SaveChanges();
                return new GuardarFacturaResponse(factura);
            }
            catch(Exception e)
            {
                return new GuardarFacturaResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public EditarFacturaResponse ActualizarEstadoFactura(string codigo, string estado)
        {
            try
            {
                var response = _context.Facturas.Find(codigo);
                if(response != null)
                {
                    response.Estado = estado;
                    _context.Facturas.Update(response);
                    _context.SaveChanges();
                    return new EditarFacturaResponse(response);
                }
                else
                {
                    return new EditarFacturaResponse("No Existe la factura", "NoExiste");
                }
            }
            catch(Exception e)
            {
                return new EditarFacturaResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }

        public ConsultarFacturasResponse Consultar()
        {
            try
            {
                var facturas = _context.Facturas.OrderBy(f => f.Estado).ToList();
                return new ConsultarFacturasResponse(facturas);
            }
            catch(Exception e)
            {
                return new ConsultarFacturasResponse($"Error en la aplicacion: {e.Message}","Error");
            }
        }


        public class ConsultarFacturasResponse
        {
            public ConsultarFacturasResponse(List<Factura> facturas)
            {
                Error = false;
                Facturas = facturas;
            }
            public ConsultarFacturasResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public List<Factura> Facturas { get; set; }
        }

        public class EditarFacturaResponse
        {
            public EditarFacturaResponse(Factura factura)
            {
                Error = false;
                Factura = factura;
            }
            public EditarFacturaResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public Factura Factura { get; set; }
        }

        public class GuardarFacturaResponse
        {
            public GuardarFacturaResponse(Factura factura)
            {
                Error = false;
                Factura = factura;
            }
            public GuardarFacturaResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public string Estado { get; set; }
            public Factura Factura { get; set; }
        }
    }
}