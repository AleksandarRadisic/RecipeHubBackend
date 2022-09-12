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

        public void addPicture(IFormFile file, Guid id, Guid userId)
        {
            Recipe recipe = _uow.GetRepository<IRecipeReadRepository>().GetById(id);
            if (recipe == null) throw new EntityNotFoundException("Recipe not found");
            if (recipe.UserId != userId) throw new UnauthorizedAccessException("Recipe not owned");
            string pictureName = PictureUtility.savePicture(RecipePictureDestination, file);
            recipe.Pictures = new List<Picture>{new Picture{FileName = pictureName}};
            _uow.GetRepository<IRecipeWriteRepository>().Update(recipe);
        }

        public void deletePicture(Guid recipeId, Guid userId, Guid pictureId)
        {
            Recipe recipe = _uow.GetRepository<IRecipeReadRepository>().GetById(recipeId, FetchType.Eager);
            if (recipe == null) throw new EntityNotFoundException("Recipe not found");
            if (recipe.UserId != userId) throw new UnauthorizedAccessException("Recipe not owned");
            foreach (var pic in recipe.Pictures)
            {
                if (pic.Id == pictureId)
                {
                    PictureUtility.deletePicture(RecipePictureDestination, pic.FileName);
                    _uow.GetRepository<IPictureWriteRepository>().Delete(pic);
                    return;
                }
            }

            throw new EntityNotFoundException("Picture not found in recipe");
        }

        public string getPictureAsBase64()
        {
            return PictureUtility.convertToBase64(RecipePictureDestination, "21f9d746-8d61-4d79-9cc7-d040c2cd3b4e.jpg");
        }

        public void addComments(Comment comment, Guid recipeId)
        {
            Recipe recipe = _uow.GetRepository<IRecipeReadRepository>().GetById(recipeId, FetchType.Eager);
            if (recipe == null) throw new EntityNotFoundException("Recipe not found");
            if (recipe.UserId == comment.UserId) throw new ForbiddenException("Cannot post comment on owned recipe");
            foreach (var existingComment in recipe.Comments)
            {
                if (existingComment.UserId == comment.UserId)
                    throw new ForbiddenException("Already commented on this recipe");
            }
            recipe.Comments.Append(comment);
            _uow.GetRepository<IRecipeWriteRepository>().Update(recipe);
        }
    }
}
