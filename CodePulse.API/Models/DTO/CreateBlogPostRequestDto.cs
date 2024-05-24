using System.ComponentModel.DataAnnotations;

namespace BLOGAPP.API.Models.DTO
{
    public class CreateBlogPostRequestDto
    {
        [Required]
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        [Required]
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        [Required]
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        [Required]
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        public Guid[] Categories { get; set; }
    }
}
