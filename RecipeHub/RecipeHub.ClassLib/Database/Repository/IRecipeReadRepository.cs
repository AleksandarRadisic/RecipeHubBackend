﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository
{
    public interface IRecipeReadRepository : IBaseReadRepository<Guid, Recipe>
    {
        public Recipe GetById(Guid id, bool ignoreBlockedComments = true, FetchType fetchType = FetchType.Lazy);
        public IEnumerable<Recipe> GetByUserId(Guid userId, FetchType fetchType = FetchType.Lazy);
    }
}
