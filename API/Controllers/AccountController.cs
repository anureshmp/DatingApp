using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var userExists = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == registerDTO.Email);

            if (userExists != null) return BadRequest();

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

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDTO loginDTO)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (user == null) return Unauthorized("Invalid email address");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");

            }

            return user;
           
        }


    }
}
