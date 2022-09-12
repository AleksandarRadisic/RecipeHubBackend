using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service.Implementation.Base;
using RecipeHub.ClassLib.Utilities;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class RecipeService : BaseService, IRecipeService
    {
        public RecipeService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) {}

        public IEnumerable<Recipe> getRecipes()
        {
            return _uow.GetRepository<IRecipeReadRepository>().GetAll();
        }

        public Recipe getRecipe(Guid id)
        {
            return _uow.GetRepository<IRecipeReadRepository>().GetById(id, FetchType.Eager);
        }

        public void addRecipe(Recipe recipe)
        {
            List<Guid> IngredientIds = new List<Guid>();
            foreach (var recipeIngr in recipe.RecipeIngredients)
            {
                if (IngredientIds.Contains(recipeIngr.IngredientId))
                    throw new ArgumentException("Cannot add same ingredient multiple times");
                IngredientIds.Add(recipeIngr.IngredientId);
            }

            if (_uow.GetRepository<IIngredientReadRepository>().GetByIds(IngredientIds).Count() != IngredientIds.Count)
                throw new EntityNotFoundException("Ingredient not found");
            _uow.GetRepository<IRecipeWriteRepository>().Add(recipe);
        }

        public void editRecipe(Recipe recipe)
        {
            Recipe recipeFromDatabase = _uow.GetRepository<IRecipeReadRepository>().GetById(recipe.Id, FetchType.Eager);
            if (recipeFromDatabase == null) throw new EntityNotFoundException("Recipe not found");
            if (recipeFromDatabase.UserId != recipe.UserId) throw new UnauthorizedAccessException("Recipe not owned by logged user");
            List<Guid> IngredientIds = new List<Guid>();
            foreach (var recipeIngr in recipe.RecipeIngredients)
            {
                if (IngredientIds.Contains(recipeIngr.IngredientId))
                    throw new ArgumentException("Cannot add same ingredient multiple times");
                IngredientIds.Add(recipeIngr.IngredientId);
            }

            if (_uow.GetRepository<IIngredientReadRepository>().GetByIds(IngredientIds).Count() != IngredientIds.Count)
                throw new EntityNotFoundException("Ingredient not found");
            _uow.GetRepository<IRecipeIngredientWriteRepository>().DeleteMany(recipeFromDatabase.RecipeIngredients);
            _mapper.Map(recipe, recipeFromDatabase);
            _uow.GetRepository<IRecipeWriteRepository>().Update(recipeFromDatabase);
        }

        public void addPicture(IFormFile file)
        {
            PictureUtility.savePicture(RecipePictureDestination, file);
        }

        public void deletePicture()
        {
            PictureUtility.deletePicture(RecipePictureDestination, "7033e572-461c-498e-9b47-5d37a0e7519b.jpg");
        }

        public string getPictureAsBase64()
        {
            return PictureUtility.convertToBase64(RecipePictureDestination, "21f9d746-8d61-4d79-9cc7-d040c2cd3b4e.jpg");
        }
    }
}
