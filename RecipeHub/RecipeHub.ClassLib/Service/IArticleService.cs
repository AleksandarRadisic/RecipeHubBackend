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
        public IEnumerable<Article> getArticles();
        public Article getArticle(Guid id);
        public void addArticle(Article article);
        public void editArticle(Article article);
        public void addPicture(IFormFile file, Guid id, Guid userId);
        public void deletePicture(Guid articleId, Guid userId, Guid pictureId);
        public string getPictureAsBase64();
        public void addComments(Comment comment, Guid articleId);
    }
}
