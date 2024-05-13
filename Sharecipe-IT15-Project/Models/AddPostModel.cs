using Sharecipe_IT15_Project.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sharecipe_IT15_Project.Models
{
    public class AddPostModel
    {

        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name ="Caption")]
            public string caption { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Ingredients")]
            public string ingredients { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Directions")]
            public string direction { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Prep Time")]
            public string prepTime { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Cook Time")]
            public string cookTime { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Serving")]
            public string serving { get; set; }
        }

    }
}
