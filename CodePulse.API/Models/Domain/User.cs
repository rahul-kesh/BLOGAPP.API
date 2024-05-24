using System.ComponentModel.DataAnnotations;

namespace BLOGAPP.API.Models.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
      public string ConfirmPassword {  get; set; }
    }
}
