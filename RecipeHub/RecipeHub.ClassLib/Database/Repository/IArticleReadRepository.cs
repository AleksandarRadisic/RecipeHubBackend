using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository
{
    public interface IArticleReadRepository : IBaseReadRepository<Guid, Article>
    {
    }
}
