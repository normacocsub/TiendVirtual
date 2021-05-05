namespace TiendaWebApi.Models
{
    public class UsuarioInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UsuarioViewModel : UsuarioInputModel
    {

    }
}