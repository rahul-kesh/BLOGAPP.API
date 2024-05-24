using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Models.DTO;
using BLOGAPP.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BLOGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly ITokenRepository tokenRepository;

        public AuthController(
            ITokenRepository tokenRepository)
        {
            
            this.tokenRepository = tokenRepository;
        }

        // POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Check Email
            try
            {
                var result = tokenRepository.Login(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
            return BadRequest(ex.Message);
            }
        }
          

        // POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {

            // Create IdentityUser object
            try
            {
                var user = new User
                {
                    Email = request.Email?.Trim(),
                    Password = request.Password?.Trim(),
                    ConfirmPassword = request.Password?.Trim()
                };

                // Create User
                var result = await tokenRepository.Register(user);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("User Already Exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

    }
}
