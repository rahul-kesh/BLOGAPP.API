using BLOGAPP.API.Data;
using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BLOGAPP.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id, int userId)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id && x.UserId==userId );

            if (existingBlogPost != null)
            {
                dbContext.BlogPosts.Remove(existingBlogPost);
                 dbContext.SaveChanges();
                return existingBlogPost;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BlogPost>> GetByUserIdAsync(int UserId)
        {
            
            return await dbContext.BlogPosts.Include(x => x.Categories).Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost,int userId)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id && x.UserId==userId);

            if (existingBlogPost == null)
            {
                return null;
            }

            // Update BlogPost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            // Update Categories
            existingBlogPost.Categories = blogPost.Categories;

            await dbContext.SaveChangesAsync();

            return blogPost;
        }
    }
}
