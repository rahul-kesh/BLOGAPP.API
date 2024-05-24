using System.ComponentModel.DataAnnotations;

namespace BLOGAPP.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
