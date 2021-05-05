using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Usuario
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Usuario()
        {
            
        }
    }
}
