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
    public class PostController : Controller
    {
        private readonly SharecipeIdentityDBContext _dbContext;

        public PostService PostService { get; }

        public PostController(SharecipeIdentityDBContext context, IHttpContextAccessor httpContextAccessor,
            UserManager<SharecipeUser> userManager, PostService postService)
        {
            this._dbContext = context;
            this.PostService = postService;
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
    }
}
