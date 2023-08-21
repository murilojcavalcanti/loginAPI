using System.ComponentModel.DataAnnotations;

namespace usuarioApi.Data.Dtos
{
    public class LoginUsuarioDTO
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
