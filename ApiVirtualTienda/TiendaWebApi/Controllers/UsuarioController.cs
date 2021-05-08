using System.Linq;
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
                var key = Seguridad.RandomString(16);
                _context.Usuarios.Add(new Usuario(){Email = "lider@gmail.com",Estado = "Activo", KeyDesEncriptarPassword = key, Password = Seguridad.Encriptar("adminABC", key), Role = "LIDER"});
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
            return Ok(new UsuarioViewModel(response.Usuario));
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
            return Ok(new InteresadoViewModel(response.Interesado));
        }

        [HttpPut("ActualizarInfo")]
        public ActionResult<UsuarioViewModel> ActualizarInformacionUsuario(UsuarioInputModel usuarioInput)
        {
            Usuario usuario = MapearInfoUsuario(usuarioInput);
            var response = _serviceUsuario.ActualizarDatos(usuario);
            if(response.Error)
            {
                ModelState.AddModelError("Error al actualizar el usuario", response.Mensaje);
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
            return Ok(new UsuarioViewModel(response.Usuario));
        }

        [HttpGet("{email}")]
        public ActionResult<UsuarioViewModel> BuscarUsuario(string email)
        {
            var response = _serviceUsuario.BuscarUsuario(email);
            if(response.Error)
            {
                ModelState.AddModelError("Error al buscar el usuario", response.Mensaje);
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
            return Ok(response.Usuario);
        }

        [HttpGet("Interesados")]
        public ActionResult<InteresadoViewModel> ConsultarInteresados()
        {
            var response = _serviceUsuario.ConsultarInteresados();
            if(response.Error)
            {
                ModelState.AddModelError("Error al consultar los interesados", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(response.Interesados.Select(i => new InteresadoViewModel(i)));
        }

        [HttpGet]
        public ActionResult<UsuarioViewModel> ConsultarUsuarios()
        {
            var response = _serviceUsuario.ConsultarUsuarios();
            if(response.Error)
            {
                ModelState.AddModelError("Error al consultar los usuarios", response.Mensaje);
                var detallesproblemas = new ValidationProblemDetails(ModelState);

                if(response.Estado == "Error")
                {
                    detallesproblemas.Status = StatusCodes.Status500InternalServerError;
                }
                return BadRequest(detallesproblemas);
            }
            return Ok(response.Usuarios.Select(u => new UsuarioViewModel(u)));
        }

        [HttpPut("UsuarioEstado/{user}/{estado}")]
        public ActionResult<UsuarioViewModel> ActualizarEstadoUsuario(string user, string estado)
        {
            var response = _serviceUsuario.ActualizarEstado(user, estado);
            if(response.Error)
            {
                ModelState.AddModelError("Error al actualizar el usuario", response.Mensaje);
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
            return Ok(new UsuarioViewModel(response.Usuario));
        }
        [HttpGet("login/{user}/{password}")]
        public ActionResult<UsuarioViewModel> VerificarLogin(string user, string password)
        {
            var response = _serviceUsuario.ValidarUsuario(user, password);
            if (response == null)
            {
                ModelState.AddModelError("Acceso Denegado", "Usuario y/o contraseña incorrectos");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized,
                };
                return Unauthorized(problemDetails);
            }
            return Ok(new UsuarioViewModel(response));
        }

        private Usuario MapearInfoUsuario(UsuarioInputModel usuarioInput)
        {
            var usuario = new Usuario
            {
                Email = usuarioInput.Email,
                Password = usuarioInput.Password,
                Role = usuarioInput.Role,
                Apellidos = usuarioInput.Apellidos,
                Nombres = usuarioInput.Nombres,
                Sexo = usuarioInput.Sexo,
                Telefono = usuarioInput.Telefono,
                Estado = usuarioInput.Estado
            };
            return usuario;
        }
        private Usuario MapearUsuario(UsuarioInputModel usuarioInput)
        {
            var usuario = new Usuario
            {
                Email = usuarioInput.Email,
                Password = usuarioInput.Password,
                Role = "Ventas",
                Apellidos = usuarioInput.Apellidos,
                Nombres = usuarioInput.Nombres,
                Sexo = usuarioInput.Sexo,
                Telefono = usuarioInput.Telefono,
                Estado = "Activo"
            };
            return usuario;
        }

        private UsuarioInteresado MapearInteresado(InteresadoInputModels interesadoInput)
        {
            var key = Seguridad.RandomString(16);
            var interesado = new UsuarioInteresado
            {
                NIT = interesadoInput.NIT,
                Usuario = new Usuario{
                    Email = interesadoInput.Usuario.Email,
                    Password = Seguridad.Encriptar(interesadoInput.Usuario.Password, key),
                    KeyDesEncriptarPassword = key,
                    Role = "Interesado",
                    Apellidos = interesadoInput.Usuario.Apellidos,
                    Nombres = interesadoInput.Usuario.Nombres,
                    Sexo = interesadoInput.Usuario.Sexo,
                    Telefono = interesadoInput.Usuario.Telefono,
                    Estado = "Activo"
                }
            };
            return interesado;
        }
    }
}