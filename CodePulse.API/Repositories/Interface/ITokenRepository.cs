using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Models.DTO;

namespace BLOGAPP.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        //string CreateJwtToken(RegisterRequestDto user);

         Task<User> Register(User user);
        LoginResponseDto Login(LoginRequestDto loginDto);
    }
}
