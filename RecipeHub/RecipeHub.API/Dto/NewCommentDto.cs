using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto
{
    public class NewCommentDto
    {
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
        public uint Rating { get; set; }
    }
}
