using Entity;

namespace TiendaWebApi.Models
{
    public class InteresadoInputModels
    {
        public string NIT { get; set; }
        public UsuarioInputModel Usuario { get; set; }
    }

    public class InteresadoViewModel : InteresadoInputModels
    {
        public InteresadoViewModel(){}
        public InteresadoViewModel(UsuarioInteresado interesado)
        {
            NIT = interesado.NIT;
            Usuario = new UsuarioViewModel(interesado.Usuario);
        }
    }
}