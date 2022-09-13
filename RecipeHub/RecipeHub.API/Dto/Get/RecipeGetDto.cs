using RecipeHub.ClassLib.Model;

namespace RecipeHub.API.Dto.Get
{
    public class RecipeGetDto
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<PictureBase64> Pictures { get; set; }
    }
}
