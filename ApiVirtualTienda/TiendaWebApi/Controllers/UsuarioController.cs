using BLL;
using DAL;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaWebApi.Models;

namespace TiendaWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly TiendaVirtualContext _context;
        private readonly UsuarioService _serviceUsuario;

        public UsuarioController(TiendaVirtualContext context)
        {
            _context = context;
            var usuarioResponse = _context.Usuarios.Find("lider@gmail.com");

            if(usuarioResponse == null)
            {
                _context.Usuarios.Add(new Usuario(){Email = "lider@gmail.com", Password = "adminABC", Role = "LIDER"});
                _context.SaveChanges();
            }

            _serviceUsuario = new UsuarioService(context);
        }

        [HttpPost]
        public ActionResult<UsuarioViewModel> GuardarUsuario(UsuarioInputModel usuarioInput)
        {
            Usuario usuario = MapearUsuario(usuarioInput);
            var response = _serviceUsuario.GuardarUsuario(usuario);
            if(response.Error)
            {
                ModelState.AddModelError("Error al guardar el usuario", response.Mensaje);
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
            return Ok(response.Usuario);
        }

        [HttpPost("Interesado")]
        public ActionResult<InteresadoViewModel> RegistrarInteresado(InteresadoInputModels interesadoInput)
        {
            UsuarioInteresado interesado = MapearInteresado(interesadoInput);
            var response = _serviceUsuario.RegistrarInteresado(interesado);
            if(response.Error)
            {
                ModelState.AddModelError("Error al guardar el usuario", response.Mensaje);
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
            return Ok(response.Interesado);
        }
        private Usuario MapearUsuario(UsuarioInputModel usuarioInput)
        {
            var usuario = new Usuario
            {
                Email = usuarioInput.Email,
                Password = usuarioInput.Password,
            };
            return usuario;
        }

        private UsuarioInteresado MapearInteresado(InteresadoInputModels interesadoInput)
        {
            var interesado = new UsuarioInteresado
            {
                NIT = interesadoInput.NIT,
                Usuario = new Usuario{
                    Email = interesadoInput.Usuario.Email,
                    Password = interesadoInput.Usuario.Password,
                    Role = interesadoInput.Usuario.Role
                }
            };
            return interesado;
        }
    }
}