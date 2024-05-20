namespace Sharecipe_IT15_Project.Models
{
    public class UpdateProfileModel
    {

        public InputModel Input { get; set; }
        

        public class InputModel
        {
            public string? fullname { get; set; }

            public string? bio { get; set; }

            public DateOnly? birthdate { get; set; }


            public string? address { get; set; }

            public IFormFile? profilePic { get; set; }
        }

    }
}
