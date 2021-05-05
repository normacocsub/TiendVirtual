namespace TiendaWebApi.Models
{
    public class UsuarioInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Role { get; set; }
    }

    public class UsuarioViewModel : UsuarioInputModel
    {

    }
}