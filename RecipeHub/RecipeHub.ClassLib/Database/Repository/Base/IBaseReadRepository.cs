using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Repository.Enums;

namespace RecipeHub.ClassLib.Database.Repository.Base
{
    public interface IBaseReadRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id, FetchType type = FetchType.Lazy);
        IEnumerable<TEntity> GetAll(FetchType type = FetchType.Lazy);
    }
}
