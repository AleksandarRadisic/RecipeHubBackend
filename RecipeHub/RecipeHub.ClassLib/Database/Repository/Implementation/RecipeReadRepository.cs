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
                    .Include(rec => rec.Comments.Where(c => c.Report == null || !c.Report.BlockApproved))
                    .Include(r => r.Pictures)
                    .ToList();
            }
            return set.ToList();
        }

        public Recipe GetById(Guid id, bool ignoreBlockedComments = true, FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager && !ignoreBlockedComments)
            {
                return GetSet()
                    .Include(r => r.Comments)
                    .Include(r => r.User)
                    .Include(r => r.Pictures)
                    .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recIng => recIng.Ingredient)
                    .FirstOrDefault(r => r.Id == id);
            }

            return GetById(id, fetchType);
        }

        public IEnumerable<Recipe> GetByUserId(Guid userId, FetchType fetchType = FetchType.Lazy)
        {
            var set = GetSet();
            if (fetchType == FetchType.Eager)
            {
                return set
                    .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recIng => recIng.Ingredient)
                    .Include(rec => rec.Comments.Where(c => c.Report == null || !c.Report.BlockApproved))
                    .Include(r => r.Pictures)
                    .Include(r => r.User)
                    .Where(r => r.UserId == userId)
                    .ToList();
            }
            return set.Where(r => r.UserId == userId).ToList();
        }

        public override Recipe GetById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            var set = GetSet();
            if (fetchType == FetchType.Eager)
            {
                return set
                    .Include(recipe => recipe.RecipeIngredients)
                    .ThenInclude(recIng => recIng.Ingredient)
                    .Include(rec => rec.Comments.Where(c => c.Report == null || !c.Report.BlockApproved))
                    .Include(r => r.Pictures)
                    .Include(r => r.User)
                    .FirstOrDefault(r => r.Id == id);
            }
            return set.Find(id);
        }

    }
}
