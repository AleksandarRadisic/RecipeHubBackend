using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto.PostPut
{
    public class PersonalInfoUpdateDto
    {
        [Required(ErrorMessage = "Fist name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
    }
}
