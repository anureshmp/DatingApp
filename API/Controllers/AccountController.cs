using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        public readonly AppDbContext _dbContext;
        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("registerr")]
        public async Task<ActionResult<AppUser>> Register (string email, string displayName, string password)
        {
            
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                DisplayName = displayName,
                Email = email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;

        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key

            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
            
        }
    }
}
