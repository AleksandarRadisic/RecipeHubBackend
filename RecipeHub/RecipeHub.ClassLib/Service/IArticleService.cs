using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Service
{
    public interface IArticleService
    {
        public IEnumerable<Article> GetArticles();
        public Article GetArticle(Guid id);
        public void AddArticle(Article article);
        public void EditArticle(Article article);
        public void AddPicture(IFormFile file, Guid id, Guid userId);
        public void DeletePicture(Guid articleId, Guid userId, Guid pictureId);
        public IEnumerable<PictureBase64> GetPictureAsBase64(Article article);
        public void AddComments(Comment comment, Guid articleId);
        public void ReportComment(Guid articleId, Guid userId, Guid commentId);
        public void DeleteArticle(Guid articleId, Guid userId);
        public IEnumerable<Article>GetArticlesByUserId(Guid userId);
    }
}
