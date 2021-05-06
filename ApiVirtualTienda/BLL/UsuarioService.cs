using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;

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
                    if(usuarioResponse == null)
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

        public ActualizarUsuarioResponse ActualizarDatos(Usuario usuario)
        {
            try
            {
                var response = _context.Usuarios.Find(usuario.Email);
                if(response != null)
                {
                    response.Nombres = usuario.Nombres;
                    response.Apellidos = usuario.Apellidos;
                    response.Sexo = usuario.Sexo;
                    response.Telefono = usuario.Telefono;
                    return new ActualizarUsuarioResponse(response);
                }
                return new ActualizarUsuarioResponse("No existe el usuario", "NoExiste");
            }
            catch(Exception e)
            {
                return new ActualizarUsuarioResponse($"Error en la aplicacion: {e.Message}", "Error");
            }
        }

        public ConsultarInteresadosResponse ConsultarInteresados()
        {
            try
            {
                var interesados = _context.Interesados.Include(i => i.Usuario).ToList();
                return new ConsultarInteresadosResponse(interesados);
            }
            catch(Exception e)
            {
                return new ConsultarInteresadosResponse($"Error en la aplicacion: {e.Message}", "Error");
            }
        }

        public ActualizarUsuarioResponse ActualizarEstado(string user, string estado)
        {
            try
            {
                var response = _context.Usuarios.Find(user);
                if(response != null)
                {
                    response.Estado = estado;
                    return new ActualizarUsuarioResponse(response);
                }
                return new ActualizarUsuarioResponse("No existe este usuario", "NoExiste");
            }
            catch(Exception e)
            {
                return new ActualizarUsuarioResponse($"Error en la aplicacion: {e.Message}", "Error");
            }
        }

        public class ConsultarInteresadosResponse
        {
            public ConsultarInteresadosResponse(List<UsuarioInteresado> interesados)
            {
                Error = false;
                Interesados = interesados;
            }
            public ConsultarInteresadosResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public List<UsuarioInteresado> Interesados { get; set; }
        }

        public class ActualizarUsuarioResponse
        {
            public ActualizarUsuarioResponse(Usuario usuario)
            {
                Error = false;
                Usuario = usuario;
            }
            public ActualizarUsuarioResponse(string mensaje, string estado)
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
