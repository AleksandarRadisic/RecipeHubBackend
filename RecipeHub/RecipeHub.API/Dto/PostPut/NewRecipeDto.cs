using System.ComponentModel.DataAnnotations;
using RecipeHub.API.Attributes;

namespace RecipeHub.API.Dto.PostPut
{
    public class NewRecipeDto
    {
        [Required(ErrorMessage = "Recipe name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Recipe descripiton is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Recipe instructions are required")]
        public string Instructions { get; set; }
        [Required(ErrorMessage = "Recipe requires at least one ingredient"), MinLength(1, ErrorMessage = "Recipe requires at least one ingredient")]
        public IEnumerable<RecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
