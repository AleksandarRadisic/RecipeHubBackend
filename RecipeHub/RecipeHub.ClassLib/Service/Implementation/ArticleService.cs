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
        protected const string ArticlePictureDestination = "Pictures/Articles";

        public ArticleService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public IEnumerable<Article> GetArticles()
        {
            return _uow.GetRepository<IArticleReadRepository>().GetAll();
        }

        public Article GetArticle(Guid id)
        {
            return _uow.GetRepository<IArticleReadRepository>().GetById(id, FetchType.Eager);
        }

        public void AddArticle(Article article)
        {
            _uow.GetRepository<IArticleWriteRepository>().Add(article);
        }

        public void EditArticle(Article article)
        {
            Article articleFromDatabase = _uow.GetRepository<IArticleReadRepository>().GetById(article.Id);
            if (articleFromDatabase == null) throw new EntityNotFoundException("Article not found");
            if (articleFromDatabase.UserId == article.UserId)
                throw new UnauthorizedAccessException("Article not owned by logged user");
            _mapper.Map(article, articleFromDatabase);
            _uow.GetRepository<IArticleWriteRepository>().Update(articleFromDatabase);
        }

        public void AddPicture(IFormFile file, Guid id, Guid userId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(id);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId != userId) throw new UnauthorizedAccessException("Article not owned");
            string pictureName = PictureUtility.savePicture(ArticlePictureDestination, file);
            article.Pictures = new List<Picture> { new Picture { FileName = pictureName } };
            _uow.GetRepository<IArticleWriteRepository>().Update(article);
        }

        public void DeletePicture(Guid articleId, Guid userId, Guid pictureId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(articleId, FetchType.Eager);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId != userId) throw new UnauthorizedAccessException("Article not owned");
            foreach (var pic in article.Pictures)
            {
                if (pic.Id == pictureId)
                {
                    PictureUtility.deletePicture(ArticlePictureDestination, pic.FileName);
                    _uow.GetRepository<IPictureWriteRepository>().Delete(pic);
                    return;
                }
            }

            throw new EntityNotFoundException("Picture not found in recipe");
        }

        public IEnumerable<PictureBase64> GetPictureAsBase64(Article article)
        {
            List<PictureBase64> pictures = new List<PictureBase64>();
            if (article.Pictures == null) return pictures;
            foreach (var pic in article.Pictures)
            {
                pictures.Add(new PictureBase64
                {
                    Id = pic.Id,
                    Data = PictureUtility.convertToBase64(ArticlePictureDestination, pic.FileName)
                });
            }
            return pictures;
        }

        public void AddComments(Comment comment, Guid articleId)
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
            article.CalculateRating();
            _uow.GetRepository<IArticleWriteRepository>().Update(article);
        }

        public void ReportComment(Guid articleId, Guid userId, Guid commentId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(articleId, FetchType.Eager);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId != userId) throw new ForbiddenException("Cannot report comments on unowned article");
            foreach (var existingComment in article.Comments)
            {
                if (existingComment.Id == commentId)
                {
                    if (existingComment.Report != null) throw new ForbiddenException("Comment already reported");
                    existingComment.Report = new Report();
                    _uow.GetRepository<ICommentWriteRepository>().Update(existingComment);
                    return;
                }

            }
            throw new EntityNotFoundException("Comment not found");
        }

        public void DeleteArticle(Guid articleId, Guid userId)
        {
            Article article = _uow.GetRepository<IArticleReadRepository>().GetById(articleId, false, FetchType.Eager);
            if (article == null) throw new EntityNotFoundException("Article not found");
            if (article.UserId != userId)
                if (_uow.GetRepository<IUserReadRepository>().GetById(userId, FetchType.Eager).Role.Name != "Admin")
                    throw new ForbiddenException("Only admin and article owners may delete article");
            foreach (var picture in article.Pictures)
            {
                PictureUtility.deletePicture(ArticlePictureDestination, picture.FileName);
            }
            _uow.GetRepository<IArticleWriteRepository>().Delete(article);
        }

        public IEnumerable<Article> GetArticlesByUserId(Guid userId)
        {
            return _uow.GetRepository<IArticleReadRepository>().GetByUserId(userId);
        }
    }
}
