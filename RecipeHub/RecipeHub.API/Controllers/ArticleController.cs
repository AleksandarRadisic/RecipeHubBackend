using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Controllers.Base;
using RecipeHub.API.Dto.Get;
using RecipeHub.API.Dto.PostPut;
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
            return Ok(_articleService.GetArticles().OrderByDescending(a => a.Rating));
        }

        [HttpGet("{id:guid}")]
        public IActionResult getById(Guid id)
        {
            try
            {
                var article = _articleService.GetArticle(id);
                return Ok(new ArticleGetDto
                {
                    Article = article,
                    Pictures = _articleService.GetPictureAsBase64(article)
                });
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpGet("from-user/{id:guid}")]
        public IActionResult GetFromUser(Guid id)
        {
            try
            {
                var articles = _articleService.GetArticlesByUserId(id);
                return Ok(articles.OrderByDescending(a => a.Rating));
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
                var articles = _articleService.GetArticlesByUserId(GetUserIdFromContext());
                return Ok(articles.OrderByDescending(a => a.Rating));
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Regular")]
        public IActionResult PostArticle(NewArticleDto dto)
        {
            try
            {
                Article article = _mapper.Map<Article>(dto);
                article.UserId = GetUserIdFromContext();
                _articleService.AddArticle(article);
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
                _articleService.EditArticle(article);
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
                _articleService.AddPicture(file, id, userId);
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
                _articleService.DeletePicture(id, userId, picId);
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
                _articleService.AddComments(comment, id);
                return Ok("Comment added");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }

        }

        [HttpPost("{id:guid}/comments/{comId:guid}/report")]
        [Authorize(Roles = "Regular")]
        public IActionResult ReportComments(Guid id, Guid comId)
        {
            try
            {
                _articleService.ReportComment(id, GetUserIdFromContext(), comId);
                return Ok("Comment has been reported");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Regular")]
        public IActionResult DeleteArticle(Guid id)
        {
            try
            {
                _articleService.DeleteArticle(id, GetUserIdFromContext());
                return Ok("Article deleted");
            }
            catch (Exception ex)
            {
                return ReturnErrorResult(ex);
            }
        }
    }
}
