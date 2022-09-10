using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _uow;
        public RecipeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Recipe getRecipe(Guid id)
        {
            return _uow.GetRepository<IRecipeReadRepository>().GetById(id);
        }

        public void addRecipe(Recipe recipe)
        {
            _uow.GetRepository<IRecipeWriteRepository>().Add(recipe);
        }
    }
}
