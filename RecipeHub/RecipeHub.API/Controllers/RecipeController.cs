using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Database.Infrastructure;
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
        public IActionResult PostRecipe(NewRecipeDto dto)
        {
            _recipeService.addRecipe(_mapper.Map<Recipe>(dto));
            return Ok("Recipe added");
        }
    }
}
