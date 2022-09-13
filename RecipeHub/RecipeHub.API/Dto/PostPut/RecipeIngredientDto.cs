using System.ComponentModel.DataAnnotations;

namespace RecipeHub.API.Dto.PostPut
{
    public class RecipeIngredientDto
    {
        [Required(ErrorMessage = "Ingredient is required")]
        public Guid IngredientId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
