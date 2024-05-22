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

        public async Task<bool> DeleteUserPost(String postID)
        {
            var post = await _dbContext.Posts.FindAsync(postID);
            if (post == null)
            {
                return false;
            }
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateUserPost(Post updatedPost)
        {
            var post = await _dbContext.Posts.FindAsync(updatedPost.PostId);
            if (post == null)
            {
                return false;
            }

            post.postCaption = updatedPost.postCaption;
            post.postIngredients = updatedPost.postIngredients;
            post.postDirections = updatedPost.postDirections;
            post.prepTime = updatedPost.prepTime;
            post.cookTime = updatedPost.cookTime;
            post.serving = updatedPost.serving;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
