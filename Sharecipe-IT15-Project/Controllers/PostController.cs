using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sharecipe.Models;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models;
using System.Net;
using System.Security.Claims;

namespace Sharecipe_IT15_Project.Controllers
{
    public class PostController : Controller
    {

        private readonly SharecipeIdentityDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<SharecipeUser> _userManager; // Inject UserManager

        public PostController(SharecipeIdentityDBContext context, IHttpContextAccessor httpContextAccessor, UserManager<SharecipeUser> userManager)
        {
            this._dbContext = context;
            this._httpContextAccessor = httpContextAccessor;
            _userManager = userManager; // Initialize UserManager
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostModel addPostModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            var newPost = new Post
            {
                postCaption = addPostModel.Input.caption,
                postIngredients = addPostModel.Input.ingredients,
                postDirections = addPostModel.Input.direction,
                prepTime = addPostModel.Input.prepTime,
                cookTime = addPostModel.Input.cookTime,
                serving = addPostModel.Input.serving,
                PostTime = DateTime.Now,
                UserName = user.FullName,
               
                postUserId = userId
            };

            await _dbContext.Posts.AddAsync(newPost);
            await _dbContext.SaveChangesAsync();

            return View(); // Or redirect to a different action
        }
    }
}
