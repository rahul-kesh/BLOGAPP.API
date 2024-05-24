using System.ComponentModel.DataAnnotations;

namespace BLOGAPP.API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
