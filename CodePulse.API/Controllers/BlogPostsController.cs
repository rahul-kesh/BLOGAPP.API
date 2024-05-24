using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Models.DTO;
using BLOGAPP.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BLOGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository,
            ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        // POST: {apibaseurl}/api/blogposts
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);
                // Convert DTO to DOmain
                var blogPost = new BlogPost
                {
                    UserId = userId,
                    Author = request.Author,
                    Content = request.Content,
                    FeaturedImageUrl = request.FeaturedImageUrl,
                    IsVisible = request.IsVisible,
                    PublishedDate = request.PublishedDate,
                    ShortDescription = request.ShortDescription,
                    Title = request.Title,
                    UrlHandle = request.UrlHandle,
                    Categories = new List<Category>()
                };


                foreach (var categoryGuid in request.Categories)
                {
                    var existingCategory = await categoryRepository.GetById(categoryGuid);
                    if (existingCategory is not null)
                    {
                        blogPost.Categories.Add(existingCategory);
                    }
                }

                blogPost = await blogPostRepository.CreateAsync(blogPost);

                // Convert Domain Model back to DTO
                var response = new BlogPostDto
                {
                    //UserId=blogPost.UserId,
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,

                    }).ToList()
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: {apibaseurl}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            try
            {
                var blogPosts = await blogPostRepository.GetAllAsync();

                // Convert Domain model to DTO
                var response = new List<BlogPostDto>();
                foreach (var blogPost in blogPosts)
                {
                    response.Add(new BlogPostDto
                    {
                        UserId = blogPost.UserId,
                        Id = blogPost.Id,
                        Author = blogPost.Author,
                        Content = blogPost.Content,
                        FeaturedImageUrl = blogPost.FeaturedImageUrl,
                        IsVisible = blogPost.IsVisible,
                        PublishedDate = blogPost.PublishedDate,
                        ShortDescription = blogPost.ShortDescription,
                        Title = blogPost.Title,
                        UrlHandle = blogPost.UrlHandle,
                        Categories = blogPost.Categories.Select(x => new CategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name,

                        }).ToList()
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
          
        }


        // GET: {apiBaseUrl}/api/blogposts/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            try
            {
                // Get the BlogPost from Repo
                var blogPost = await blogPostRepository.GetByIdAsync(id);

                if (blogPost is null)
                {
                    return NotFound();
                }

                // Convert Domain Model to DTO
                var response = new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,

                    }).ToList()
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: {apiBaseUrl}/api/blogposts/{id}
        [HttpGet]
        [Route("MyBlog")]
        [Authorize]
        public async Task<IActionResult> GetBlogPostByUserId()
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);

                // Get the BlogPost from Repo
                var blogPosts = await blogPostRepository.GetByUserIdAsync(userId);

                if (blogPosts is null)
                {
                    return NotFound();
                }

                // Convert Domain Model to DTO
                var response = new List<BlogPostDto>();
                foreach (var blogPost in blogPosts)
                {
                    response.Add(new BlogPostDto
                    {
                        UserId = blogPost.UserId,
                        Id = blogPost.Id,
                        Author = blogPost.Author,
                        Content = blogPost.Content,
                        FeaturedImageUrl = blogPost.FeaturedImageUrl,
                        IsVisible = blogPost.IsVisible,
                        PublishedDate = blogPost.PublishedDate,
                        ShortDescription = blogPost.ShortDescription,
                        Title = blogPost.Title,
                        UrlHandle = blogPost.UrlHandle,
                        Categories = blogPost.Categories.Select(x => new CategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name,

                        }).ToList()
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: {apibaseurl}/api/blogPosts/{urlhandle}
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            try
            {
                // Get blogpost details from repository
                var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

                if (blogPost is null)
                {
                    return NotFound();
                }

                // Convert Domain Model to DTO
                var response = new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,

                    }).ToList()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: {apibaseurl}/api/blogposts/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);
                // Convert DTO to Domain Model
                var blogPost = new BlogPost
                {
                    UserId = userId,
                    Id = id,
                    Author = request.Author,
                    Content = request.Content,
                    FeaturedImageUrl = request.FeaturedImageUrl,
                    IsVisible = request.IsVisible,
                    PublishedDate = request.PublishedDate,
                    ShortDescription = request.ShortDescription,
                    Title = request.Title,
                    UrlHandle = request.UrlHandle,
                    Categories = new List<Category>()
                };

                // Foreach 
                foreach (var categoryGuid in request.Categories)
                {
                    var existingCategory = await categoryRepository.GetById(categoryGuid);

                    if (existingCategory != null)
                    {
                        blogPost.Categories.Add(existingCategory);
                    }
                }


                // Call Repository To Update BlogPost Domain Model
                var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost, userId);

                if (updatedBlogPost == null)
                {
                    return NotFound();
                }

                // Convert Domain model back to DTO
                var response = new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        
                    }).ToList()
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: {apibaseurl}/api/blogposts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);
                var deletedBlogPost = await blogPostRepository.DeleteAsync(id, userId);


                if (deletedBlogPost == null)
                {
                    return NotFound();
                }


                // Convert Domain model to DTO
                var response = new BlogPostDto
                {
                    Id = deletedBlogPost.Id,
                    Author = deletedBlogPost.Author,
                    Content = deletedBlogPost.Content,
                    FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                    IsVisible = deletedBlogPost.IsVisible,
                    PublishedDate = deletedBlogPost.PublishedDate,
                    ShortDescription = deletedBlogPost.ShortDescription,
                    Title = deletedBlogPost.Title,
                    UrlHandle = deletedBlogPost.UrlHandle
                };

                return Ok(response);
        }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
    }
}
    }
}
