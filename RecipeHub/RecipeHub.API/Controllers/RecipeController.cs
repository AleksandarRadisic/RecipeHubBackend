using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto;
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

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_recipeService.getRecipe(id));
        }

        [HttpPost]
        [Authorize(Roles = "Regular")]
        public IActionResult PostRecipe(NewRecipeDto dto)
        {
            try
            {
                Recipe recipe = _mapper.Map<Recipe>(dto);
                foreach (var claim in HttpContext.User.Claims)
                {
                    if (claim.Type == "id")
                    {
                        recipe.UserId = Guid.Parse(claim.Value);
                        break;
                    }
                }

                _recipeService.addRecipe(recipe);
                return Ok("Recipe added");
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: return BadRequest(ex.Message);
                    case EntityNotFoundException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Regular")]
        public IActionResult PutRecipe(NewRecipeDto dto, Guid id)
        {
            try
            {
                Recipe recipe = _mapper.Map<Recipe>(dto);
                foreach (var claim in HttpContext.User.Claims)
                {
                    if (claim.Type == "id")
                    {
                        recipe.UserId = Guid.Parse(claim.Value);
                        break;
                    }
                }
                recipe.Id = id;
                _recipeService.editRecipe(recipe);
                return Ok("Recipe updated");
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case ArgumentException: return BadRequest(ex.Message);
                    case EntityNotFoundException: return NotFound(ex.Message);
                    case UnauthorizedAccessException: return Unauthorized(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            
        }
    }
}
