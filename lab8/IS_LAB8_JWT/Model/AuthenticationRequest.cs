using System.ComponentModel.DataAnnotations;

namespace IS_LAB8_JWT.Model
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
