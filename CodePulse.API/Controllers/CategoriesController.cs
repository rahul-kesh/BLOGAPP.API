using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Models.DTO;
using BLOGAPP.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BLOGAPP.API.Controllers
{
    // https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var category = new Category
                {
                    Name = request.Name,

                };

                await categoryRepository.CreateAsync(category);

                // Domain model to DTO
                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,

                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: https://localhost:7226/api/Categories?query=html&sortBy=name&sortDirection=desc
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            try
            {
                var caterogies = await categoryRepository
                    .GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

                // Map Domain model to DTO

                var response = new List<CategoryDto>();
                foreach (var category in caterogies)
                {
                    response.Add(new CategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,

                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: https://localhost:7226/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            try
            {
                var existingCategory = await categoryRepository.GetById(id);

                if (existingCategory is null)
                {
                    return NotFound();
                }

                var response = new CategoryDto
                {
                    Id = existingCategory.Id,
                    Name = existingCategory.Name,

                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: https://localhost:7226/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            try
            {
                // Convert DTO to Domain Model
                var category = new Category
                {
                    Id = id,
                    Name = request.Name,

                };

                category = await categoryRepository.UpdateAsync(category);

                if (category == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,

                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: https://localhost:7226/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            try
            {
                var category = await categoryRepository.DeleteAsync(id);

                if (category is null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,

                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // GET: https://localhost:7226/api/categories/count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCategoriesTotal()
        {
            try
            {
                var count = await categoryRepository.GetCount();

                return Ok(count);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
