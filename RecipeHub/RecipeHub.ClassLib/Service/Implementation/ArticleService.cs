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
    public class ArticleService : BaseService, IArticleService
    {
        public ArticleService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public IEnumerable<Article> getArticles()
        {
            return _uow.GetRepository<IArticleReadRepository>().GetAll();
        }

        public Article getArticle(Guid id)
        {
            return _uow.GetRepository<IArticleReadRepository>().GetById(id, FetchType.Eager);
        }

        public void addArticle(Article article)
        {
            _uow.GetRepository<IArticleWriteRepository>().Add(article);
        }

        public void editArticle(Article article)
        {
            Article articleFromDatabase = _uow.GetRepository<IArticleReadRepository>().GetById(article.Id);
            if (articleFromDatabase == null) throw new EntityNotFoundException("Article not found");
            if (articleFromDatabase.UserId == article.UserId)
                throw new UnauthorizedAccessException("Article not owned by logged user");
            _mapper.Map(article, articleFromDatabase);
            _uow.GetRepository<IArticleWriteRepository>().Update(articleFromDatabase);
        }

        public void addPicture(IFormFile file, Guid id, Guid userId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(id);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId != userId) throw new UnauthorizedAccessException("Article not owned");
            string pictureName = PictureUtility.savePicture(RecipePictureDestination, file);
            article.Pictures = new List<Picture> { new Picture { FileName = pictureName } };
            _uow.GetRepository<IArticleWriteRepository>().Update(article);
        }

        public void deletePicture(Guid articleId, Guid userId, Guid pictureId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(articleId, FetchType.Eager);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId != userId) throw new UnauthorizedAccessException("Article not owned");
            foreach (var pic in article.Pictures)
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
            throw new NotImplementedException();
        }

        public void addComments(Comment comment, Guid articleId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(articleId, FetchType.Eager);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId == comment.UserId) throw new ForbiddenException("Cannot post comment on owned article");
            foreach (var existingComment in article.Comments)
            {
                if (existingComment.UserId == comment.UserId)
                    throw new ForbiddenException("Already commented on this article");
            }

            article.Comments = new List<Comment>(article.Comments.Append(comment));
            _uow.GetRepository<IArticleWriteRepository>().Update(article);
        }
    }
}
