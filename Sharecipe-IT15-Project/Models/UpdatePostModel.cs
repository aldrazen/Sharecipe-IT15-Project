namespace Sharecipe_IT15_Project.Models
{
    public class UpdatePostModel
    {
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string? postCaption { get; set; }

            public IFormFile? postImg { get; set; }

            public string? postIngredients { get; set; }

            public string? postDirections { get; set; }

            public string? prepTime { get; set; }

            public string? cookTime { get; set; }

            public string? serving {  get; set; }     
        }
    }
}
