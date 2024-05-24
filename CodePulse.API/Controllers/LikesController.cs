using Microsoft.AspNetCore.Mvc;
using BLOGAPP.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;


namespace BLOGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeRepository likeRepository;

        public LikesController(ILikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        // GET: api/Likes
        [HttpGet()]
        [Authorize]
        public IActionResult GetLikes(Guid blogId)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);
                var result = likeRepository.IsLiked(blogId, userId);
                return Ok(result);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]

        public IActionResult GetCount(Guid blogId)
        {
            try
            {
                var result = likeRepository.GetLikeCount(blogId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("comments")]

        public IActionResult GetAllComments(Guid blogId)
        {
            try
            {
                var result = likeRepository.GetAllComments(blogId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("comment")]
        [Authorize]
        public IActionResult AddComment(Guid blogId,string comment,string userName)
        {
            var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);
            try
            {
                this.likeRepository.AddComment(blogId, comment, userId,userName);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Likes/5
        [HttpPost("{blogId}")]
        [Authorize]
        public IActionResult PostLike(Guid blogId, bool isActive)
        {
            var userId = int.Parse(HttpContext.User.FindFirst("UserId").Value);
            try
            {
                this.likeRepository.LikeStatusUpdate(blogId, isActive,userId);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
