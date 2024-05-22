using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models;
using Sharecipe_IT15_Project.Models.Entities;
using Sharecipe_IT15_Project.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace Sharecipe_IT15_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SharecipeIdentityDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<SharecipeUser> _userManager; // Inject UserManager
        private readonly IWebHostEnvironment _environment;
        private readonly ProfileService _profileService;

        public HomeController(ILogger<HomeController> logger, SharecipeIdentityDBContext context, IHttpContextAccessor httpContextAccessor,
            UserManager<SharecipeUser> userManager, IWebHostEnvironment environment, ProfileService profileService)
        {
            _logger = logger;
            this._dbContext = context;
            this._httpContextAccessor = httpContextAccessor;
            _userManager = userManager; // Initialize UserManager
            this._environment = environment;
            this._profileService = profileService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddPostModel addPostModel)
        {
            if (addPostModel.Input.postImage == null)
            {
                ModelState.AddModelError("postImage", "The image field is required");
                return View(addPostModel);
            }

            string newFileName = $"{DateTime.Now:yyyyMMdd}-{Guid.NewGuid()}";
            newFileName += Path.GetExtension(addPostModel.Input.postImage!.FileName);

            string imagePath = Path.Combine(_environment.WebRootPath, "PostImages", newFileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                addPostModel.Input.postImage.CopyTo(stream);
            }

            if (ModelState.IsValid) // Check if model state is valid
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                var newPost = new Post
                {
                    
                    postCaption = addPostModel.Input.caption,
                    postIngredients = addPostModel.Input.ingredients,
                    postDirections = addPostModel.Input.directions,
                    prepTime = addPostModel.Input.prepTime,
                    cookTime = addPostModel.Input.cookTime,
                    serving = addPostModel.Input.serving,
                    PostTime = DateTime.Now,
                    postImage = newFileName,
                    UserName = user.FullName,
                    UserPfp = user.ProfPIc,
                    postUserId = userId,
                    postLikes = 0
                };

                await _dbContext.Posts.AddAsync(newPost);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Recipe Added!";

                // Clear form fields after saving the post
                addPostModel.Input.caption = "";
                addPostModel.Input.ingredients = "";
                addPostModel.Input.directions = "";
                addPostModel.Input.prepTime = "";
                addPostModel.Input.cookTime = "";
                addPostModel.Input.serving = "";

                // Redirect to the same action to refresh the page
                return RedirectToAction("Index");
            }

            // If model state is not valid, return the view with validation errors
            return View(addPostModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
