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
        public IEnumerable<Recipe> GetRecipes();
        public Recipe GetRecipe(Guid id);
        public void AddRecipe(Recipe recipe);
        public void EditRecipe(Recipe recipe);
        public void AddPicture(IFormFile file, Guid id, Guid userId);
        public void DeletePicture(Guid recipeId, Guid userId, Guid pictureId);
        public IEnumerable<PictureBase64> GetPicturesAsBase64(Recipe recipe);
        public void AddComments(Comment comment, Guid recipeId);
        public void ReportComment(Guid recipeId, Guid userId, Guid commentId);
        public void DeleteRecipe(Guid recipeId, Guid userId);
        public IEnumerable<Recipe> GetRecipesByUserId(Guid recipeId);
    }
}
