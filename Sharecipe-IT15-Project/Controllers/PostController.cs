using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models;
using Sharecipe_IT15_Project.Models.Entities;
using Sharecipe_IT15_Project.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sharecipe_IT15_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly SharecipeIdentityDBContext _dbContext;
        private readonly UserManager<SharecipeUser> _userManager;

        public PostService PostService { get; }

        public PostController(SharecipeIdentityDBContext context, IHttpContextAccessor httpContextAccessor,
            UserManager<SharecipeUser> userManager, PostService postService)
        {
            this._dbContext = context;
            this.PostService = postService;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> PostList()
        {
            var posts = await _dbContext.Posts.OrderByDescending(p => p.PostTime).ToListAsync();
            return View(posts);
        }
        public IActionResult PostListPartial()
        {
            var postModel = new Post();
            return PartialView("PostList", postModel);
        }

        [HttpGet("GetPost/{postId}")]
        public async Task<IActionResult> GetPost(string postId)
        {
            var post = await PostService.GetPostByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        [HttpPost]
        [Route("LikePost")]
        public async Task<IActionResult> LikePost([FromBody] LikePostRequest request)
        {
            int updatedLikes;
            if (request.IsLiked)
            {
                // Increment post likes if the post is being liked
                updatedLikes = await PostService.IncrementPostLikes(request.PostId);
            }
            else
            {
                // Decrement post likes if the post is being unliked
                updatedLikes = await PostService.DecrementPostLikes(request.PostId);
            }

            return Ok(new { likes = updatedLikes });
        }

        public class LikePostRequest
        {
            public string PostId { get; set; }
            public bool IsLiked { get; set; }
        }

        [HttpGet("/Post/SearchResults")]
        public async Task<IActionResult> SearchResults(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("SearchResults", Enumerable.Empty<Post>());
            }

            var posts = await PostService.SearchPostsAsync(query);
            return View("SearchResults", posts);
        }


    }
}
