using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class IngredientReadRepository : BaseReadRepository<Guid, Ingredient>, IIngredientReadRepository
    {
        public IngredientReadRepository(AppDbContext context) : base(context)
        {
        }

        public Ingredient GetByName(string name)
        {
            return GetSet().FirstOrDefault(ingredient => ingredient.Name == name);
        }

        public IEnumerable<Ingredient> GetByIds(IEnumerable<Guid> ids)
        {
            var Ingredients = GetSet();
            return Ingredients.Where(i => ids.Contains(i.Id));
        }
    }
}
