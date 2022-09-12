using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Service
{
    public interface IRecipeService
    {
        public IEnumerable<Recipe> getRecipes();
        public Recipe getRecipe(Guid id);
        public void addRecipe(Recipe recipe);
        public void editRecipe(Recipe recipe);
        public void addPicture(IFormFile file);
        public void deletePicture();
        public string getPictureAsBase64();
    }
}
