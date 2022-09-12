using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class RecipeReadRepository : BaseReadRepository<Guid, Recipe>, IRecipeReadRepository
    {
        public RecipeReadRepository(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Recipe> GetAll(FetchType fetchType = FetchType.Lazy)
        {
            var set = GetSet();
            if (fetchType == FetchType.Eager)
            {
                return set
                    .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recIng => recIng.Ingredient)
                    .Include(rec => rec.Comments)
                    .Include(r => r.Pictures)
                    .ToList();
            }
            return set.ToList();
        }

        public override Recipe GetById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            var set = GetSet();
            if (fetchType == FetchType.Eager)
            {
                return set
                    .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recIng => recIng.Ingredient)
                    .Include(rec => rec.Comments)
                    .Include(r => r.Pictures)
                    .FirstOrDefault(r => r.Id == id);
            }
            return set.Find(id);
        }

    }
}
