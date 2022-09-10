using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Enums;

namespace RecipeHub.ClassLib.Database.Repository.Base
{
    public class BaseReadRepository<TKey, TEntity> : IBaseReadRepository<TKey, TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _context;

        protected BaseReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual TEntity GetById(TKey id, FetchType fetchType = FetchType.Lazy)
        {
            var set = GetSet();
            return set.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll(FetchType fetchType = FetchType.Lazy)
        {
            return GetSet().ToList();
        }

        protected DbSet<TEntity> GetSet()
        {
            return _context.Set<TEntity>();
        }
    }
}
