﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service;

namespace RecipeHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        public ArticleController(IUnitOfWork uow, IMapper mapper, IConfiguration config, IArticleService articleService) : base(uow, mapper, config)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_articleService.getArticles());
        }

        [HttpGet("{id:guid}")]
        public IActionResult getById(Guid id)
        {
            return Ok(_articleService.getArticle(id));
        }

        [HttpPost]
        [Authorize(Roles = "Regular")]
        public IActionResult PostAction(NewArticleDto dto)
        {
            try
            {
                Article article = _mapper.Map<Article>(dto);
                article.UserId = GetUserIdFromContext();
                _articleService.addArticle(article);
                return Ok("Article added");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }

        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Regular")]
        public IActionResult PutArticle(NewArticleDto dto, Guid id)
        {
            try
            {
                Article article = _mapper.Map<Article>(dto);
                article.UserId = GetUserIdFromContext();
                article.Id = id;
                _articleService.editArticle(article);
                return Ok("Article updated");
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
                _articleService.addPicture(file, id, userId);
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
                _articleService.deletePicture(id, userId, picId);
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
                _articleService.addComments(comment, id);
                return Ok("Comment added");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }

        }
    }
}