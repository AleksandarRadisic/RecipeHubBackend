﻿using System.Reflection.Metadata;
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
                recipe.UserId = GetUserIdFromContext();
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
                recipe.UserId = GetUserIdFromContext();
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

        [HttpPost("{id:guid}/Picture")]
        [Authorize(Roles = "Regular")]
        public IActionResult addPicture(IFormFile file, Guid id)
        {
            try
            {
                Guid userId = GetUserIdFromContext();
                _recipeService.addPicture(file, id, userId);
                return Ok("Picture added");
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case UnauthorizedAccessException: return Unauthorized(ex.Message);
                    case EntityNotFoundException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
            
        }

        [HttpDelete("{id:guid}/Pictures/{picId:guid}")]
        public IActionResult deletePicture(Guid id, Guid picId)
        {
            try
            {
                Guid userId = GetUserIdFromContext();
                _recipeService.deletePicture(id, userId, picId);
                return Ok("Picture deleted");
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case UnauthorizedAccessException: return Unauthorized(ex.Message);
                    case EntityNotFoundException: return NotFound(ex.Message);
                    default: return Problem("Oops, something went wrong. Try again");
                }
            }
        }

        [HttpPost("{id:guid}/comments")]
        [Authorize(Roles = "Regular")]
        public IActionResult PostComment(NewCommentDto dto, Guid id)
        {
            Comment comment = _mapper.Map<Comment>(dto);
            Guid userId = GetUserIdFromContext();
            comment.UserId = userId;
            return Ok();
        }
    }
}
