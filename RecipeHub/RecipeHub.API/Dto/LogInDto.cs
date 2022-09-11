using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto
{
    public class LogInDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
