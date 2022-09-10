using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Service
{
    public interface IRecipeService
    {
        public Recipe getRecipe(Guid id);
        public void addRecipe(Recipe recipe);
    }
}
