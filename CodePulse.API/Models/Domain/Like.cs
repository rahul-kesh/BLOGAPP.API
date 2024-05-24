using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLOGAPP.API.Models.Domain
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        
        public Guid BlogId { get; set; }

        [ForeignKey("BlogId")]
        public BlogPost BlogPosts { get; set; }

        public int UserId {  get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }

        public bool IsActive {  get; set; }
    }
}
