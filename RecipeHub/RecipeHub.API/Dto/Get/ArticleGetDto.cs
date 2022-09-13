using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.Dto.Get
{
    public class ArticleGetDto
    {
        public Article Article { get; set; }
        public IEnumerable<PictureBase64> Pictures { get; set; }
    }
}
