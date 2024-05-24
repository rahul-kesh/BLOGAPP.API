using BLOGAPP.API.Data;
using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Repositories.Interface;

namespace BLOGAPP.API.Repositories.Implementation
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext context;
        public LikeRepository(ApplicationDbContext context) { 
            this.context = context;
        }

       public void LikeStatusUpdate(Guid blogId, bool isActive, int userId)
        {
            var blogExist=context.Likes.FirstOrDefault(t=>t.BlogId==blogId && t.UserId==userId);
            if (blogExist!=null)
            {
                blogExist.IsActive = isActive;
            }
            else
            {
                var like = new Like
                {
                    BlogId = blogId,
                    UserId = userId,
                    IsActive = isActive
                };
                context.Likes.Add(like);
                   
            }
            context.SaveChanges();

        }

        public bool IsLiked(Guid blogId, int userId)
        {
            bool isLiked = false;
            var blogExist = context.Likes.FirstOrDefault(t => t.BlogId == blogId && t.UserId == userId);
            if(blogExist!=null)
            {
                isLiked= blogExist.IsActive;
            }
            return isLiked;
        }

        public int GetLikeCount(Guid blogId)
        {
            var result=context.Likes.Where(t=>t.BlogId==blogId && t.IsActive==true).Count();
            return result;
        }

        public IEnumerable<Comment> GetAllComments(Guid blogId)
        {
            var result = context.Comments.Where(t => t.BlogId == blogId);
            return result;
        }

        public void AddComment(Guid blogId, string comment, int userId, string userName)
        {
            var commentObj = new Comment
            {
                UserId = userId,
                BlogId = blogId,
                CommentText = comment,
                UserName=userName

            };
            context.Comments.Add(commentObj);
            context.SaveChanges();

        }
    }
}
