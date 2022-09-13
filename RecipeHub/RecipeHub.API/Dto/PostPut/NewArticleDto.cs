using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto.PostPut
{
    public class NewArticleDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }
    }
}
