using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class UsuarioInteresado
    {
        [Key]
        public string NIT { get; set; }
        public Usuario Usuario { get; set; }

        public UsuarioInteresado()
        {
            
        }
    }
}