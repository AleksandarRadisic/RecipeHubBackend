using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.ClassLib.Model
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
