using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models;
using Sharecipe_IT15_Project.Models.Entities;
using Sharecipe_IT15_Project.Services;
using Sharecipe_IT15_Project.Views.Profile;
using System.Security.Claims;

namespace Sharecipe_IT15_Project.Controllers
{
    public class ProfileController : Controller

    {
        private readonly UserManager<SharecipeUser> _userManager;
        private readonly ProfileService _profileService;
        private readonly SharecipeIdentityDBContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(UserManager<SharecipeUser> userManager, ProfileService profileService, SharecipeIdentityDBContext dbContext,
            IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            this._userManager = userManager;
            this._profileService = profileService;
            this._dbContext = dbContext;
            this._environment = environment;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(AddPostModel addPostModel)
        {
            if (addPostModel.Input.postImage == null)
            {
                ModelState.AddModelError("postImage", "The image field is required");
            }

            if (ModelState.IsValid)
            {
                string newFileName = $"{DateTime.Now:yyyyMMdd}-{Guid.NewGuid()}{Path.GetExtension(addPostModel.Input.postImage!.FileName)}";
                string imagePath = Path.Combine(_environment.WebRootPath, "PostImages", newFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    addPostModel.Input.postImage.CopyTo(stream);
                }

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
                    postUserId = userId
                };

                await _dbContext.Posts.AddAsync(newPost);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Recipe Added!";
                return RedirectToAction("UserProfile");
            }

            return View(addPostModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditProfile(UpdateProfileModel updateProfileModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                if (updateProfileModel.Input.profilePic != null)
                {
                    string newFileName = $"{DateTime.Now:yyyyMMdd}-{Guid.NewGuid()}{Path.GetExtension(updateProfileModel.Input.profilePic.FileName)}";
                    string imagePath = Path.Combine(_environment.WebRootPath, "ProfileImages", newFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await updateProfileModel.Input.profilePic.CopyToAsync(stream);
                    }
                    user.ProfPIc = newFileName;
                }

                if (!string.IsNullOrEmpty(updateProfileModel.Input.fullname))
                {
                    user.FullName = updateProfileModel.Input.fullname;
                }

                if (!string.IsNullOrEmpty(updateProfileModel.Input.bio))
                {
                    user.Bio = updateProfileModel.Input.bio;
                }

                if (!string.IsNullOrEmpty(updateProfileModel.Input.address))
                {
                    user.Address = updateProfileModel.Input.address;
                }

                if (updateProfileModel.Input.birthdate.HasValue)
                {
                user.Birthdate = updateProfileModel.Input.birthdate.Value;
                }

                if (!string.IsNullOrEmpty(updateProfileModel.Input.address))
                {
                    user.Address = updateProfileModel.Input.address;
                }

                // Update the user's profile in the AspNetUsers table
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(updateProfileModel);
                }

                // Update the user's previous posts with the new name and profile picture
                var userPosts = _dbContext.Posts.Where(p => p.postUserId == userId).ToList();
                foreach (var post in userPosts)
                {
                    if (!string.IsNullOrEmpty(user.FullName))
                    {
                        post.UserName = user.FullName;
                    }
                    if (!string.IsNullOrEmpty(user.ProfPIc))
                    {
                        post.UserPfp = user.ProfPIc;
                    }
                }

                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";

                return RedirectToAction("UserProfile");
            }

            return View(updateProfileModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPost()
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userPost = await _dbContext.Posts.Where(p => p.postUserId == userID).OrderByDescending(p => p.PostTime).ToListAsync();
            return View(userPost);
        }

        public IActionResult EditProfilePartial()
        {
            var updateProfile = new UpdateProfileModel();
            return PartialView("EditProfile", updateProfile);
        }
    }
}
