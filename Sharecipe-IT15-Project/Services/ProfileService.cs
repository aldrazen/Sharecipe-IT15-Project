using Microsoft.EntityFrameworkCore;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models.Entities;
using System.Security.Claims;

namespace Sharecipe_IT15_Project.Services
{
    public class ProfileService
    {
        private readonly SharecipeIdentityDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService (SharecipeIdentityDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Post>> GetUserPost()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _dbContext.Posts.Where(p => p.postUserId == userID).OrderByDescending(p => p.PostTime).ToListAsync();
        }

       
    }
}
