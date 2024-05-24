using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLOGAPP.API.Models.Domain
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        
        public int UserId {  get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }

        public Guid BlogId {  get; set; }

        [ForeignKey("BlogId")]
        public BlogPost BlogPosts { get; set; }

        public string UserName {  get; set; }
        public string CommentText {  get; set; }
    }
}
