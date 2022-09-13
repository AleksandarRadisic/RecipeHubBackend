using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository
{
    public interface ICommentReadRepository : IBaseReadRepository<Guid, Comment>
    {
        public IEnumerable<Comment> GetPendingReport(FetchType fetchType = FetchType.Lazy);
        public IEnumerable<Comment> GetBlockedComments();
    }
}
