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
    public class ProductoController : ControllerBase
    {   
        private readonly ProductoService _serviceProducto;
        public ProductoController(TiendaVirtualContext context)
        {
            _serviceProducto = new ProductoService(context);
        }

        [HttpPost]
        public ActionResult<ProductoViewModel> GuardarProducto(ProductoInputModels productoInput)
        {
            Producto producto = MapearProducto(productoInput);
            var response = _serviceProducto.GuardarProducto(producto);
            if(response.Error)
            {
                ModelState.AddModelError("Error al guardar el producto", response.Mensaje);
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
            return Ok(response.Producto);
        }

        [HttpPut]
        public ActionResult<ProductoViewModel> Actualizar(ProductoInputModels productoInput)
        {
            Producto producto = MapearProducto(productoInput);
            var response = _serviceProducto.EditarProducto(producto);
            if(response.Error)
            {
                ModelState.AddModelError("Error al editar el producto", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                if(response.Estado == "NoExiste")
                {
                    detallesproblemas.Status = StatusCodes.Status404NotFound;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(response.Producto);
        }

        [HttpPut("UpdateCantidad/{cantidad}/{codigo}")]
        public ActionResult<ProductoViewModel> ActualizarCantidad(int cantidad, string codigo)
        {
            var response = _serviceProducto.ActualizarCantidad(cantidad, codigo);
            if(response.Error)
            {
                ModelState.AddModelError("Error al editar el producto", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                if(response.Estado == "NoExiste")
                {
                    detallesproblemas.Status = StatusCodes.Status404NotFound;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(response.Producto);
        }

        [HttpGet]
        public ActionResult<ProductoViewModel> ConsultarProductos()
        {
            var response = _serviceProducto.ConsultarProductos();
            if(response.Error)
            {
                ModelState.AddModelError("Error al guardar el producto", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(response.Productos.Select(p => new ProductoViewModel(p)));
        }

        private Producto MapearProducto(ProductoInputModels productoInput)
        {
            var producto = new Producto
            {
                Codigo = productoInput.Codigo,
                Cantidad = productoInput.Cantidad,
                Descripcion = productoInput.Descripcion,
                Descuento = productoInput.Descuento,
                Fecha = productoInput.Fecha,
                ProveedorNIT = productoInput.IdProveedor,
                ValorUnitario = productoInput.ValorUnitario,
                IVA = productoInput.IVA,
                ValorDescontado = productoInput.ValorDescontado,
                Proveedor = new Proveedor{
                     NIT = productoInput.Proveedor.NIT,
                     Nombre = productoInput.Proveedor.Nombre,       
                }
            };
            return producto;
        }
    }
}