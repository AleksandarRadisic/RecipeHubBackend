using System.ComponentModel.DataAnnotations;
using RecipeHub.ClassLib.Model.Enumerations;

namespace RecipeHub.API.Dto
{
    public class NewIngredientDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Calories per unit are required")]
        public int CaloriesPerUnit { get; set; }
        [Required(ErrorMessage = "Measure unit is required")]
        public MeasureUnit MeasureUnit { get; set; }
    }
}
