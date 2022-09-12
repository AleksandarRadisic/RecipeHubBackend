using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service.Implementation.Base;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class RecipeService : BaseService, IRecipeService
    {
        public RecipeService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper) {}

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
            _mapper.Map(recipe, recipeFromDatabase);
            _uow.GetRepository<IRecipeWriteRepository>().Update(recipeFromDatabase);
        }
    }
}
