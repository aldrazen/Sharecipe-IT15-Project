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

        public async Task<Post> GetPostByIdAsync(string postId)
        {
            return await _dbContext.Posts.FindAsync(postId);
        }
        public async Task<int> IncrementPostLikes(string postId)
        {
            // Find the post by postId
            var post = await _dbContext.Posts.FindAsync(postId);

            if (post != null)
            {
                // Increment the like count
                post.postLikes++;
                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return post.postLikes; // Return the updated like count
            }
            else
            {
                return -1; // Return -1 if the post is not found
            }
        }
        public async Task<int> DecrementPostLikes(string postId)
        {
            // Find the post by postId
            var post = await _dbContext.Posts.FindAsync(postId);

            if (post != null)
            {
                // Decrement the like count
                post.postLikes = Math.Max(0, post.postLikes - 1); // Ensure likes do not go below 0
                                                                  // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return post.postLikes; // Return the updated like count
            }
            else
            {
                return -1; // Return -1 if the post is not found
            }
        }
        public async Task<IEnumerable<Post>> GetPostsByUserId(string userId)
        {
            return await _dbContext.Posts.Where(p => p.postUserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> SearchPostsAsync(string query)
        {
            string lowerQuery = query.ToLower();
            return await _dbContext.Posts
                .Where(p => p.postCaption.ToLower().Contains(lowerQuery))
                .ToListAsync();
        }
    }
}
