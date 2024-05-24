using BLOGAPP.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BLOGAPP.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
