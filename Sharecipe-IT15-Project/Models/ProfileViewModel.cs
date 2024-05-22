using Sharecipe_IT15_Project.Areas.Identity.Data;
using Sharecipe_IT15_Project.Models.Entities;

namespace Sharecipe_IT15_Project.Models
{
    public class ProfileViewModel
    {
        public SharecipeUser User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
