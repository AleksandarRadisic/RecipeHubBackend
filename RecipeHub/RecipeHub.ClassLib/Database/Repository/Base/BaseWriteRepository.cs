using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.EfStructures;

namespace RecipeHub.ClassLib.Database.Repository.Base
{
    public class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _context;

        protected BaseWriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Add(entity);

            if (persist)
                _context.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Update(entity);

            if (persist)
                _context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity, bool persist = true)
        {
            _context.Set<TEntity>().Remove(entity);

            if (persist)
                _context.SaveChanges();
        }
    }
}
