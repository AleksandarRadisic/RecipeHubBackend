using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Repository.Base;
using RecipeHub.ClassLib.Database.Repository.Enums;
using RecipeHub.ClassLib.Model;

namespace RecipeHub.ClassLib.Database.Repository.Implementation
{
    public class CommentReadRepository : BaseReadRepository<Guid, Comment>, ICommentReadRepository
    {
        public CommentReadRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Comment> GetPendingReport(FetchType fetchType = FetchType.Lazy)
        {
            if (fetchType == FetchType.Eager)
            {
                return GetSet()
                    .Include(c => c.Article)
                    .Include(c => c.Recipe)
                    .Include(c => c.User)
                    .Where(c => c.Report != null && !c.Report.AdminConfirmed)
                    .ToList();
            }
            return GetSet().Where(c => c.Report != null && !c.Report.AdminConfirmed).ToList();
        }

        public IEnumerable<Comment> GetBlockedComments()
        {
            return GetSet()
                .Include(c => c.User)
                .Where(c => c.Report != null && c.Report.BlockApproved)
                .ToList();
        }
    }
}
