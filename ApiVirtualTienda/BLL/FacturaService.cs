using System;
using DAL;
using Entity;

namespace BLL
{
    public class FacturaService
    {
        private readonly TiendaVirtualContext _context;
        public FacturaService(TiendaVirtualContext context)
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