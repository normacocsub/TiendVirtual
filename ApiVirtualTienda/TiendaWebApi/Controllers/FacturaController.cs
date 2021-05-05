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
            return Ok(response.Factura);
        }

        private Factura MapearFactura(FacturaInputModels facturaInput)
        {
            var factura = new Factura();
            facturaInput.Detalles.ForEach(d => {
                var detalle = new Detalle
                {
                    Cantidad = d.Cantidad,
                    Producto = _serviceProducto.BuscarProducto(d.Producto.Codigo).Producto,
                };
                factura.AgregarDetalle(detalle);
            });
            factura.CalcularTotal();
            return factura;
        }

    }
}