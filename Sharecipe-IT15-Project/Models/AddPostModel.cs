using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sharecipe_IT15_Project.Models
{
    public class AddPostModel
    {
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Caption is required.")]
            [Display(Name = "Caption")]
            public string caption { get; set; }

            [Required(ErrorMessage = "Ingredients are required.")]
            [Display(Name = "Ingredients")]
            public string ingredients { get; set; }

            [Required(ErrorMessage = "Directions are required.")]
            [Display(Name = "Directions")]
            public string directions { get; set; }

            [Required(ErrorMessage = "Prep Time is required.")]
            [Display(Name = "Prep Time")]
            public string prepTime { get; set; }

            [Required(ErrorMessage = "Cook Time is required.")]
            [Display(Name = "Cook Time")]
            public string cookTime { get; set; }

            [Required(ErrorMessage = "Serving information is required.")]
            [Display(Name = "Serving")]
            public string serving { get; set; }

            public string? UserPfp { get; set; }

            [Display(Name = "Post Image")]
            public IFormFile postImage { get; set; }
        }
    }
}
