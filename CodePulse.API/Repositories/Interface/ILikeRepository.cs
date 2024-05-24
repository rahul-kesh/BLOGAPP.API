using BLOGAPP.API.Models.Domain;

namespace BLOGAPP.API.Repositories.Interface
{
    public interface ILikeRepository
    {
        void LikeStatusUpdate(Guid blogId,bool isActive,int userId);

        bool IsLiked(Guid blogId, int userId);

        int GetLikeCount(Guid blogId);

        IEnumerable<Comment> GetAllComments(Guid blogId);

        void AddComment(Guid blogId, string comment, int userId,string userName);
    }
}
