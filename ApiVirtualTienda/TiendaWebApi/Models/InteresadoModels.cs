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
            Usuario = new UsuarioInputModel
            {
                Email = interesado.Usuario.Email,
                Password = "",
                Apellidos = interesado.Usuario.Apellidos,
                Nombres = interesado.Usuario.Nombres,
                Role = interesado.Usuario.Role,
                Sexo = interesado.Usuario.Sexo,
                Telefono = interesado.Usuario.Telefono,
            };
        }
    }
}