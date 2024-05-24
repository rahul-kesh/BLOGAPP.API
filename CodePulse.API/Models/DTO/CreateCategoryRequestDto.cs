using System.ComponentModel.DataAnnotations;

namespace BLOGAPP.API.Models.DTO
{
    public class CreateCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
        
    }
}
