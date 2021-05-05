using System;
using DAL;
using Entity;

namespace BLL
{
    public class UsuarioService
    {
        private readonly TiendaVirtualContext _context;
        public UsuarioService(TiendaVirtualContext context)
        {
            _context = context;
        }

        public GuardarUsuarioResponse GuardarUsuario(Usuario usuario)
        {
            try
            {
                var response = _context.Usuarios.Find(usuario.Email);
                if(response == null)
                {
                    _context.Usuarios.Add(usuario);
                    _context.SaveChanges();
                    return new GuardarUsuarioResponse(usuario);
                }
                else
                {
                    return new GuardarUsuarioResponse("Ya se encuentra registrado el usuario", "Registrado");
                }
            }
            catch(Exception e)
            {
                return new GuardarUsuarioResponse($"Error en la aplicacion: {e.Message}", "Error");
            }
        }

        public RegistrarInteresadoResponse RegistrarInteresado(UsuarioInteresado interesado)
        {
            try
            {
                var response = _context.Interesados.Find(interesado.NIT);
                if(response == null)
                {
                    var usuarioResponse = _context.Usuarios.Find(interesado.Usuario.Email);
                    if(usuarioResponse.Email == null)
                    {
                        _context.Interesados.Add(interesado);
                        _context.SaveChanges();
                        return new RegistrarInteresadoResponse(interesado);
                    }
                    else
                    {
                        return new RegistrarInteresadoResponse("Ya se encuentra registrado el usuario", "Registrado");
                    }
                }
                else
                {
                    return new RegistrarInteresadoResponse("Ya se encuentra registrado el interesado", "Registrado");
                }
            }
            catch(Exception e)
            {
                return new RegistrarInteresadoResponse($"Error en la aplicacion: {e.Message}", "Error");
            }
        }

        public class RegistrarInteresadoResponse
        {
            public RegistrarInteresadoResponse(UsuarioInteresado interesado)
            {
                Error = false;
                Interesado = interesado;
            }
            public RegistrarInteresadoResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public UsuarioInteresado Interesado { get; set; }
        }

        public class GuardarUsuarioResponse
        {
            public GuardarUsuarioResponse(Usuario usuario)
            {
                Error = false;
                Usuario = usuario;
            }
            public GuardarUsuarioResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public Usuario Usuario { get; set; }
        }
    }
}
