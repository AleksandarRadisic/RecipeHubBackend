﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class RoleReadRepository : BaseReadRepository<Guid, Role>, IRoleReadRepository
    {
        public RoleReadRepository(AppDbContext context) : base(context)
        {
        }

        public Role getByName(string name)
        {
            return GetSet().FirstOrDefault(role => role.Name.Equals(name));
        }
    }
}
