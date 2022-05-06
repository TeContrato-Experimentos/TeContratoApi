using System.ComponentModel.DataAnnotations;

namespace TeContrato.API.Domain.Services.Communications
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}