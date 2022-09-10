using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return set.ToList();
            }
            return set.ToList();
        }

        public override Recipe GetById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            var set = GetSet();
            if (fetchType == FetchType.Eager)
            {
                return set.FirstOrDefault(c => c.Id == id);
            }
            return set.Find(id);
        }

    }
}
