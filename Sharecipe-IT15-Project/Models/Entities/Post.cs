using Sharecipe_IT15_Project.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharecipe_IT15_Project.Models.Entities
{
    public class Post
    {

        [Key]
        public string? PostId { get; set; } = Guid.NewGuid().ToString(); // Auto-generate unique ID (nullable)

        [MaxLength(450)]
        public string? postCaption { get; set; } // nullable

        public string? postImage { get; set; } // nullable

        public string? postIngredients { get; set; } // nullable

        public string? postDirections { get; set; } // nullable

        public string? prepTime { get; set; } // nullable

        public string? cookTime { get; set; } // nullable

        public string? serving { get; set; } // nullable

        public DateTime PostTime { get; set; } = DateTime.UtcNow;

        [ForeignKey("SharecipeUser")]
        public string? postUserId { get; set; } // nullable

        public SharecipeUser User { get; set; }
        public string? UserPfp { get; set; } // nullable

        public string? UserName { get; set; }

    }
}
