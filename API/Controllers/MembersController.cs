using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   
    public class MembersController : BaseApiController
    {
        public readonly AppDbContext _dbcontext;
        public MembersController(AppDbContext dbContext)
        {
            this._dbcontext = dbContext;       
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await _dbcontext.Users.ToListAsync();

            return members;
            
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await _dbcontext.Users.FindAsync(id);

            if (member == null) return NotFound();
            return member;
            
        }


    }
}
