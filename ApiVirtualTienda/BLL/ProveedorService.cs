using System;
using DAL;
using Entity;

namespace BLL
{
    public class ProveedorService
    {
        private readonly TiendaVirtualContext _context;
        public ProveedorService(TiendaVirtualContext context)
        {
            _context = context;
        }


        public RegistrarProveedorResponse RegistrarProveedor(Proveedor proveedor)
        {
            try
            {
                var response = _context.Proveedores.Find(proveedor.NIT);
                if(response == null)
                {
                    _context.Proveedores.Add(proveedor);
                    _context.SaveChanges();
                    return new RegistrarProveedorResponse(proveedor);
                }
                else
                {
                    return new RegistrarProveedorResponse("Ya se encuentra registrado","Registrado");
                }
            }
            catch(Exception e)
            {
                return new RegistrarProveedorResponse($"Error en la aplicacion: {e.Message}", "Error");
            }
        }
        public class RegistrarProveedorResponse
        {
            public RegistrarProveedorResponse(Proveedor proveedor)
            {
                Error = false;
                Proveedor = proveedor;
            }
            public RegistrarProveedorResponse(string mensaje, string estado)
            {
                Error = true;
                Mensaje = mensaje;
                Estado = estado;
            }
            public bool Error { get; set; }
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public Proveedor Proveedor { get; set; }
        }
    }
}