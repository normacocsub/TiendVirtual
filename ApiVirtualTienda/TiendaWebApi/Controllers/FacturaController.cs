using System.Linq;
using BLL;
using DAL;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaWebApi.Models;

namespace TiendaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _serviceFactura;
        private readonly ProductoService _serviceProducto;

        public FacturaController(TiendaVirtualContext context)
        {
            _serviceFactura = new FacturaService(context);
            _serviceProducto = new ProductoService(context);
        }

        [HttpPost]
        public ActionResult<FacturaInputModels> CrearFactura(FacturaInputModels facturaInput)
        {
            Factura factura = MapearFactura(facturaInput);
            var response = _serviceFactura.CrearFactura(factura);
            if(response.Error)
            {
                ModelState.AddModelError("Error al crear la factura", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                if(response.Estado == "Registrado")
                {
                    detallesproblemas.Status = StatusCodes.Status409Conflict;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(new FacturaViewModels(response.Factura));
        }

        [HttpPut("Update/{codigo}/{estado}")]
        public ActionResult<FacturaViewModels> ActualizarEstado(string codigo, string estado)
        {
            var result = _serviceFactura.ActualizarEstadoFactura(codigo,estado);
            if(result.Error)
            {
                ModelState.AddModelError("Error al actualizar la factura", result.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(result.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                if(result.Estado == "NoExiste")
                {
                    detallesproblemas.Status = StatusCodes.Status404NotFound;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(new FacturaViewModels(result.Factura));
        }

        [HttpGet]
        public ActionResult<FacturaViewModels> ConsultarFacturas()
        {
            var result = _serviceFactura.Consultar();
            if(result.Error)
            {
                ModelState.AddModelError("Error al consultar las factura", result.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);
                detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                return BadRequest(detallesproblemas);
            }
            return Ok(result.Facturas.Select(f => new FacturaViewModels(f)));
        }

        private Factura MapearFactura(FacturaInputModels facturaInput)
        {
            var factura = new FacturaVenta();
            facturaInput.Detalles.ForEach(d => {
                var detalle = new Detalle
                {
                    Cantidad = d.Cantidad,
                    Producto = new Producto{
                        Cantidad = d.Producto.Cantidad,
                        Codigo = d.Producto.Codigo,
                        Descripcion = d.Producto.Descripcion,
                        Descuento = d.Producto.Descuento,
                        Estado = d.Producto.Estado,
                        Fecha = d.Producto.Fecha,
                        IVA = d.Producto.IVA,   
                        ProveedorNIT = d.Producto.IdProveedor,
                        ValorDescontado = d.Producto.ValorDescontado,
                        ValorUnitario = d.Producto.ValorUnitario
                    },
                };
                factura.AgregarDetalle(detalle);
            });
            factura.Estado = "Activo";
            factura.InteresadoId = facturaInput.InteresadoId;
            factura.UsuarioVentasId = facturaInput.UsuarioVentasId;
            factura.CalcularTotales();
            return factura;
        }

    }
}