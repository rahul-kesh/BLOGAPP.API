using BLOGAPP.API.Models.Domain;

namespace BLOGAPP.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<IEnumerable<BlogPost>> GetByUserIdAsync(int UserId);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost,int userId);

        Task<BlogPost?> DeleteAsync(Guid id, int userId);
    }
}
