using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository;
using RecipeHub.ClassLib.Exceptions;
using RecipeHub.ClassLib.Model;
using RecipeHub.ClassLib.Service.Implementation.Base;

namespace RecipeHub.ClassLib.Service.Implementation
{
    public class IngredientService : BaseService, IIngredientService
    {
        public IngredientService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public void SaveIngredient(Ingredient ingredient)
        {
            if (_uow.GetRepository<IIngredientReadRepository>().GetByName(ingredient.Name) != null)
                throw new AlreadyExistsException("Ingredient with name " + ingredient.Name + " already exists");
            _uow.GetRepository<IIngredientWriteRepository>().Add(ingredient);
        }

    }
}
