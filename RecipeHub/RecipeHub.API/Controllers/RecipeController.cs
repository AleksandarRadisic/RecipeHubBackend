using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto.Get;
using RecipeHub.API.Dto.PostPut;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service;

namespace RecipeHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : BaseController
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IUnitOfWork uow, IMapper mapper, IConfiguration config, IRecipeService recipeService) : base(uow, mapper, config)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_recipeService.GetRecipes());
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var recipe = _recipeService.GetRecipe(id);
            return Ok(new RecipeGetDto
            {
                Recipe = recipe,
                Pictures = _recipeService.GetPicturesAsBase64(recipe)
            });
        }

        [HttpGet("from-user/{id:guid}")]
        public IActionResult GetFromUser(Guid id)
        {
            try
            {
                var recipes = _recipeService.GetRecipesByUserId(id);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpGet("logged-user")]
        [Authorize]
        public IActionResult GetFromLoggedInUser()
        {
            try
            {
                var recipes = _recipeService.GetRecipesByUserId(GetUserIdFromContext());
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Regular")]
        public IActionResult PostRecipe(NewRecipeDto dto)
        {
            try
            {
                Recipe recipe = _mapper.Map<Recipe>(dto);
                recipe.UserId = GetUserIdFromContext();
                _recipeService.AddRecipe(recipe);
                return Ok("Recipe added");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
            
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Regular")]
        public IActionResult PutRecipe(NewRecipeDto dto, Guid id)
        {
            try
            {
                Recipe recipe = _mapper.Map<Recipe>(dto);
                recipe.UserId = GetUserIdFromContext();
                recipe.Id = id;
                _recipeService.EditRecipe(recipe);
                return Ok("Recipe updated");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpPost("{id:guid}/Picture")]
        [Authorize(Roles = "Regular")]
        public IActionResult addPicture(IFormFile file, Guid id)
        {
            try
            {
                Guid userId = GetUserIdFromContext();
                _recipeService.AddPicture(file, id, userId);
                return Ok("Picture added");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
            
        }

        [HttpDelete("{id:guid}/Pictures/{picId:guid}")]
        public IActionResult deletePicture(Guid id, Guid picId)
        {
            try
            {
                Guid userId = GetUserIdFromContext();
                _recipeService.DeletePicture(id, userId, picId);
                return Ok("Picture deleted");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpPost("{id:guid}/comments")]
        [Authorize(Roles = "Regular")]
        public IActionResult PostComment(NewCommentDto dto, Guid id)
        {
            try
            {
                Comment comment = _mapper.Map<Comment>(dto);
                Guid userId = GetUserIdFromContext();
                comment.UserId = userId;
                _recipeService.AddComments(comment, id);
                return Ok("Comment added");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
            
        }

        [HttpPost("{id:guid}/comments{comId:guid}/report")]
        [Authorize(Roles = "Regular")]
        public IActionResult ReportComments(Guid id, Guid comId)
        {
            try
            {
                _recipeService.ReportComment(id, GetUserIdFromContext(), comId);
                return Ok("Comment has been reported");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Regular,Admin")]
        public IActionResult DeleteRecipe(Guid id)
        {
            try
            {
                _recipeService.DeleteRecipe(id, GetUserIdFromContext());
                return Ok("Recipe deleted");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }
    }
}
