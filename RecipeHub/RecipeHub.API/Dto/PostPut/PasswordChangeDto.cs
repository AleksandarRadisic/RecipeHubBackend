using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto.PostPut
{
    public class PasswordChangeDto
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must have at least 5 characters")]
        public string Password { get; set; }
    }
}
