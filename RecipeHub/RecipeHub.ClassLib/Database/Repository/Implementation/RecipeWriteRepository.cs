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
    public class RecipeWriteRepository : BaseWriteRepository<Recipe>, IRecipeWriteRepository
    {
        public RecipeWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
