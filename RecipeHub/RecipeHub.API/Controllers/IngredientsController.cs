using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service;

namespace RecipeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : BaseController
    {
        private readonly IIngredientService _ingredientService;
        public IngredientsController(IUnitOfWork uow, IMapper mapper, IConfiguration config, IIngredientService ingredientService) : base(uow, mapper, config)
        {
            _ingredientService = ingredientService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNewIngredient(NewIngredientDto dto)
        {
            try
            {
                _ingredientService.SaveIngredient(_mapper.Map<Ingredient>(dto));
                return Ok("Ingredient added successfully");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllIngredients()
        {
            return Ok(_uow.GetRepository<IIngredientReadRepository>().GetAll());
        }

    }
}
