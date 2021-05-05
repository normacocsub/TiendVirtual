using DAL;

namespace BLL
{
    public class FacturaService
    {
        private readonly TiendaVirtualContext _context;
        public FacturaService(TiendaVirtualContext context)
        {
            _context = context;
        }


        public class GuardarFacturaResponse
        {
            
        }
    }
}