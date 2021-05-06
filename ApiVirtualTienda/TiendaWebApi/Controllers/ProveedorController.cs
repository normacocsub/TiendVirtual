using System.Linq;
using BLL;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaWebApi.Models;

namespace TiendaWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService _serviceProveedor;
        public ProveedorController(TiendaVirtualContext context)
        {
            _serviceProveedor = new ProveedorService(context);
        }

        [HttpGet]
        public ActionResult<ProveedorViewModels> ConsultarProveedores()
        {
            var response = _serviceProveedor.Consultar();
            if(response.Error)
            {
                ModelState.AddModelError("Error al consultar los proveedores", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(response.Proveedores.Select(p => new ProveedorViewModels(p)));
        }
    }
}