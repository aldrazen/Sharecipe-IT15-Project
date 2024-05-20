using Microsoft.EntityFrameworkCore;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models.Entities;
using System.Threading.Tasks;

namespace Sharecipe_IT15_Project.Services
{
    public class PostService
    {
        private readonly SharecipeIdentityDBContext _dbContext;

        public PostService(SharecipeIdentityDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> PostList()
        {
            return await _dbContext.Posts.OrderByDescending(p => p.PostTime).ToListAsync();
        }

       
    }
}
