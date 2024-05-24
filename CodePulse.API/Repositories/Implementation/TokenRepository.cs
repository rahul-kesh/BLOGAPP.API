using BLOGAPP.API.Data;
using BLOGAPP.API.Models.Domain;
using BLOGAPP.API.Models.DTO;
using BLOGAPP.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLOGAPP.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public TokenRepository(IConfiguration configuration,ApplicationDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<User> Register(User user)
        {
            var userExist = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (userExist != null)
            {
                return null;
            }
            var result = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return result.Entity;


        }

        public LoginResponseDto Login(LoginRequestDto loginDto)
        {
            var userExist = context.Users.FirstOrDefault(t => t.Email == loginDto.Email && EF.Functions.Collate(t.Password, "SQL_Latin1_General_CP1_CS_AS") == loginDto.Password);
            if (userExist != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
             new Claim(ClaimTypes.Email,userExist.Email),
             new Claim("UserId",userExist.UserId.ToString()),
            
         };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var response = new LoginResponseDto
                {
                    Email=userExist.Email,
                    Token = jwtToken,
                };
                return response;
            }
            return null;
        }
    }
}
